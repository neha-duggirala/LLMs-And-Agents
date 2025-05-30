# code_explanator

`code_explanator` is a personal project for generating question-answer (QA) datasets and fine-tuning language models to understand and explain C# code. It leverages [LangChain](https://python.langchain.com/), [Hugging Face Transformers](https://huggingface.co/docs/transformers/index), and [datasets](https://huggingface.co/docs/datasets/index) to automate the process of code comprehension, dataset creation, and model fine-tuning.

---

## Project Structure

```
code_explanator/
│
├── README.md
├── data_generator/
│   ├── convert_cs_to_txt.py
│   ├── dataset.json
│   ├── finetuning_to_understand_c_.ipynb
│   ├── generate_code_data.py
│   └── code_snippets/
│       ├── ElevatorDesign_ElevatorDesign_Controllers_FloorController.cs.txt
│       ├── ElevatorDesign_ElevatorDesign_Entities_Button.cs.txt
│       ├── ...
├── LLD-Questions-master/
│   ├── ParkingLot.txt
│   ├── ElevatorDesign/
│   ├── LoggingFramework/
│   ├── ParkingLot/
│   └── RestaurantManagementSystem/
```

---

## Features

- **Automatic QA Pair Generation:**  
  Reads C# code snippets and generates multiple question-answer pairs for each snippet using an LLM.
- **Dataset Creation:**  
  Aggregates all generated QA pairs (with code context) into a single JSON dataset for downstream tasks.
- **Fine-tuning Pipeline:**  
  Provides scripts and notebooks to tokenize, preprocess, and fine-tune Hugging Face models (e.g., CodeBERT) on the generated dataset.
- **Extensible:**  
  Easily add new code snippets or extend the pipeline for other programming languages.

---

## Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/your-repo.git
cd your-repo/code_explanator
```

### 2. Create a Virtual Environment

```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

### 3. Install Dependencies

```bash
pip install -r requirements.txt
```
> If you don't have a `requirements.txt`, install the main dependencies manually:
```bash
pip install langchain openai python-dotenv pydantic transformers datasets
```

### 4. Set Up Environment Variables

Create a `.env` file in the root directory and add your OpenAI or Hugging Face API keys:
```
OPENAI_API_KEY=your-openai-key
HUGGINGFACEHUB_API_TOKEN=your-huggingface-key
```

---

## Usage

### 1. Generate QA Dataset from Code

- Place your `.cs.txt` code files in `data_generator/code_snippets/`.
- Run the dataset generation script:
    ```bash
    cd data_generator
    python generate_code_data.py
    ```
- This will create or update `dataset.json` with QA pairs and code context.

### 2. Fine-tune a Model

- Open `finetuning_to_understand_c_.ipynb` in Jupyter or VS Code.
- The notebook demonstrates:
    - Loading the dataset
    - Formatting and tokenizing the data
    - Setting up a Hugging Face model (e.g., CodeBERT)
    - Training and evaluation steps

### 3. Convert C# Files to Text (Optional)

If you have raw `.cs` files, use `convert_cs_to_txt.py` to convert them to `.txt` format for processing.

---

## Example Dataset Entry

```json
{
  "question": "What is the purpose of the FloorController class in the ElevatorDesign project?",
  "answer": "The FloorController class is responsible for handling button clicks on different floors of the elevator system.",
  "code": "// Original Path: LLD-Questions-master/ElevatorDesign/ElevatorDesign/Controllers/FloorController.cs\n\nusing System; ..."
}
```

---

## Extending the Project

- **Add More Code:**  
  Drop new `.cs.txt` files into `code_snippets/` and rerun the generator.
- **Change Prompting:**  
  Edit the prompt in `generate_code_data.py` to customize the style or depth of generated questions and answers.
- **Support Other Languages:**  
  Adapt the pipeline to process code in other languages by updating the prompt and file handling logic.

---

## License

MIT

---

## Acknowledgements

- [LangChain](https://python.langchain.com/)
- [Hugging Face Transformers](https://huggingface.co/docs/transformers/index)
- [OpenAI](https://openai.com/)