import os
import json
from dotenv import load_dotenv
from typing import List
from pydantic import BaseModel, Field, ValidationError

from langchain_core.prompts import PromptTemplate
from langchain_core.output_parsers import PydanticOutputParser
from langchain_openai import ChatOpenAI

# Load environment variables (e.g., OpenAI API key)
load_dotenv()

# Define the Pydantic model for a single QA item
class QAItem(BaseModel):
    question: str = Field(description="A question about the C# code")
    answer: str = Field(description="A plain text answer without code snippets")
    code: str = Field(description="The C# code snippet the question and answer are about")

class QAList(BaseModel):
    items: List[QAItem]

# Optional: Validate a list of QA items
def validate_qa_dataset(data: List[dict]) -> bool:
    try:
        validated_items = [QAItem(**item) for item in data]
        print("✅ Dataset is valid.")
        return True
    except ValidationError as e:
        print("❌ Dataset validation failed:")
        print(e)
        return False

# Initialize the LLM
llm = ChatOpenAI(model="gpt-3.5-turbo")  # or "gpt-4" if available

# Create the output parser
parser = PydanticOutputParser(pydantic_object=QAList)

# Create the prompt template
prompt = PromptTemplate(
    template="""
Given a code in C#, generate 5 questions and 5 plain-text answers that help a user understand the code better.
The answer should be in plain text and must not include any code snippets.

{output_struct_instructions}

CODE:
{code}
""",
    input_variables=["code"],
    partial_variables={"output_struct_instructions": parser.get_format_instructions()},
)

# Directory containing code snippets
SNIPPETS_DIR = "code_explanator/data_generator/code_snippets/"
dataset = []

for filename in os.listdir(SNIPPETS_DIR):
    if filename.endswith(".txt"):
        file_path = os.path.join(SNIPPETS_DIR, filename)
        with open(file_path, "r") as f:
            code = f.read()
        # Run the chain
        try:
            result = (prompt | llm | parser).invoke({"code": code})
            for qa in result.items:
                dataset.append({
                    "question": qa.question,
                    "answer": qa.answer,
                    "code": code
                })
            print(f"✅ Processed {filename}")
        except Exception as e:
            print(f"❌ Error processing {filename}: {e}")

# Save the dataset to a JSON file
with open("code_explanator/data_generator/dataset.json", "w") as f:
    json.dump(dataset, f, indent=2)

print("Dataset saved to code_explanator/data_generator/dataset.json")