from langchain_core.runnables import RunnableParallel, RunnableLambda
from langchain_core.prompts import PromptTemplate
from langchain_openai import ChatOpenAI
from langchain_core.output_parsers import StrOutputParser
from summary import get_full_transcript

from dotenv import load_dotenv

load_dotenv()

llm = ChatOpenAI(model="gpt-4o")
summary_template = PromptTemplate(
    template="you are a knowlege assitant, that takes the question and answers from only the context. \n" \
    "question from the user :{user_inp}.\n context: {transcript}",
    input_variables=["user_inp", "transcript"],
)

op_parser = StrOutputParser()

url = "'https://www.youtube.com/watch?v=XpoKB3usmKc'"
transcript = get_full_transcript(url)
summary_chain = summary_template | llm | op_parser

ip = {"user_inp":"what is the ingredient 1?", "transcript": transcript}
op = summary_chain.invoke(ip)
print(op)