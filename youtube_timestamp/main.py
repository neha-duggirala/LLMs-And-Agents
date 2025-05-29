import streamlit as st
from langchain_openai import ChatOpenAI
from dotenv import load_dotenv
from langchain_core.output_parsers import PydanticOutputParser, StrOutputParser
from langchain_core.messages import SystemMessage
from langchain_core.prompts import PromptTemplate
from vector_store import youtube_url_to_documents
from prompt import format_context_for_llm
from langchain_core.runnables import RunnableParallel, RunnableLambda
from times_stamp import TimeStamp
from youtube_transcript import extract_youtube_link
from summary import get_full_transcript

load_dotenv()

llm = ChatOpenAI(model="gpt-4o")

system_prompt = SystemMessage(
    "The user watches a video, and after sometime he wants to comeback to a particular point in the video discussing about some topic. "
    "You are a YouTube assistant that helps users find the timestamp in a video. When given a transcript and its timestamps, provide the timestamp(s) for the requested topic."
)

parser = PydanticOutputParser(pydantic_object=TimeStamp)
op_parser = StrOutputParser()

timestamps_template = PromptTemplate(
    template="{system_prompt}\n use the following context and timestamps\n{context}\n Find all the timestamps for the following user request\n{input}\n in this format \n{format_instruction}",
    input_variables=["system_prompt", "context", "input"],
    partial_variables={"format_instruction": parser.get_format_instructions()},
)

summary_template = PromptTemplate(
    template="you are a knowlege assitant, that takes the question and answers from only the context. \n" \
    "question from the user :{user_inp}.\n context: {transcript}",
    input_variables=["user_inp", "transcript"],
)

def retrieve_context(inputs):
    vector_store = youtube_url_to_documents(inputs["url"])
    context = vector_store.similarity_search(query=inputs["query"], k=10)
    return {"context": format_context_for_llm(context)}


def build_prompt(template, inputs):
    return template.invoke(
      inputs
    )

summary_chain = (
    RunnableParallel(
        context = RunnableLambda(get_full_transcript), query=lambda x: x["url"]
    )
    | RunnableLambda(
        lambda inputs: {
            "prompt": build_prompt(
                summary_template,
                {
                    "transcript": inputs["context"],
                    "user_inp": inputs["query"],
                },
            )
        }
    )
    | RunnableLambda(lambda inputs: llm.invoke(inputs["prompt"]))
    |op_parser
)


parallel_chain_time_stamp = (
    RunnableParallel(
        context=RunnableLambda(retrieve_context), query=lambda x: x["query"]
    )
    | RunnableLambda(
        lambda inputs: {
            "prompt": build_prompt(
                timestamps_template,
                {
                    "system_prompt": system_prompt.content,
                    "context": inputs["context"],
                    "input": inputs["query"],
                },
            )
        }
    )
    | RunnableLambda(lambda inputs: llm.invoke(inputs["prompt"]))
    | parser
)

# --- Streamlit App ---
st.title("YouTube Timestamp Finder")

user_url_input = st.text_input("Enter YouTube Video URL:")
user_input = st.text_input("What are you searching for in the video?")
inputs = {"url": user_url_input, "query": user_input}

if st.button("Find Timestamps"):
    if user_url_input and user_input:
        with st.spinner("Processing..."):
            
            try:
                results = parallel_chain_time_stamp.invoke(inputs)
                st.success("Timestamps found!")
                st.write(results)
                for result in results.timestamp:
                    time_stamp_url = extract_youtube_link(inputs["url"], result)
                    st.write(time_stamp_url)
            except Exception as e:
                st.error(f"Error: {e}")
    else:
        st.warning("Please enter both a YouTube URL and a search query.")

if st.button("Summary"):
    op = summary_chain.invoke(inputs)
    st.write(op)