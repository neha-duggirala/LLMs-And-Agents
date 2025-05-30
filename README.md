# Personal AI Projects: LangChain & Hugging Face

This repository contains a collection of personal projects built using [LangChain](https://python.langchain.com/) and [Hugging Face Transformers](https://huggingface.co/docs/transformers/index) in Python. Each project demonstrates different capabilities of modern LLMs, vector stores, prompt engineering, and dataset generation.

---

## Table of Contents

- [Project List](#project-list)
- [Project Details](#project-details)
- [Contributing](#contributing)
- [License](#license)

---

## Project List

- **YouTube Transcript Agent**  
  Extracts, processes, and queries YouTube video transcripts using LLMs and vector stores.

- **Code Explainer & Dataset Generator**  
  Generates question-answer pairs for code snippets using LLMs, and prepares datasets for fine-tuning.

- **Fine-tuning & Tokenization**  
  Prepares and tokenizes datasets for fine-tuning Hugging Face models on custom Q&A tasks.

---


## Project Details

### 1. YouTube Transcript Agent

- **Description:**  
  Extracts transcripts from YouTube videos, stores them in a vector database, and allows semantic search with timestamped results.
- **Technologies:**  
  LangChain, youtube-transcript-api, ChromaDB, OpenAI API
- **How to Run:**  
  See [`youtube_timestamp/`](youtube_timestamp/) for code and notebooks.

### 2. Code Explainer & Dataset Generator

- **Description:**  
  Reads code snippets, generates Q&A pairs using LLMs, and saves them in a dataset for further use or fine-tuning.
- **Technologies:**  
  LangChain, Hugging Face Transformers, Pydantic
- **How to Run:**  
  See [`code_explanator/data_generator/`](code_explanator/data_generator/) for scripts and dataset.

### 3. Fine-tuning & Tokenization

- **Description:**  
  Prepares datasets and tokenizes them for fine-tuning Hugging Face models on custom tasks.
- **Technologies:**  
  Hugging Face Datasets, Transformers
- **How to Run:**  
  See [`code_explanator/fine_tune/`](code_explanator/fine_tune/) for tokenization and fine-tuning notebooks.

---

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

---

## License

[MIT](LICENSE)