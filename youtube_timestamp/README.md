# YouTube Timestamp Agent

The **YouTube Timestamp Agent** is a personal project that leverages [LangChain](https://python.langchain.com/) and [OpenAI](https://openai.com/) to help users find the exact timestamps in a YouTube video where a specific topic is discussed. It also provides concise summaries of topics from YouTube video transcripts. The project uses vector search and LLMs to enable semantic search and summarization over video transcripts.

---

## Features

- **Find Timestamps by Topic:**  
  Enter a YouTube video URL and a topic/question; the app returns the timestamps where that topic is discussed.
- **Semantic Search:**  
  Uses vector embeddings and similarity search to find the most relevant transcript sections.
- **Summarization:**  
  Summarizes the content of a video or a specific topic within the video.
- **Direct Links:**  
  Provides clickable YouTube links that start at the relevant timestamp.

---

## Project Structure

```
youtube_timestamp/
│
├── main.py                # Streamlit app entry point
├── vector_store.py        # Handles transcript storage and vector search
├── youtube_transcript.py  # Fetches and processes YouTube transcripts
├── summary.py             # Summarization utilities
├── prompt.py              # Prompt templates and formatting
├── times_stamp.py         # Timestamp extraction and validation
├── chains.py              # Chains for parallel processing
├── chroma_db/             # Chroma vector database files
├── transcripts/           # Cached transcripts (JSON)
├── README.md              # This file
└── ... (other scripts and notebooks)
```

---

## Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/your-repo.git
cd your-repo/youtube_timestamp
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
pip install langchain openai python-dotenv streamlit youtube-transcript-api chromadb
```

### 4. Set Up Environment Variables

Create a `.env` file in the project directory and add your OpenAI API key:
```
OPENAI_API_KEY=your-openai-key
```

---

## Usage

### 1. Run the Streamlit App

```bash
streamlit run main.py
```

### 2. Using the App

- **Find Timestamps:**  
  - Enter a YouTube video URL.
  - Enter a topic or question (e.g., "What is self-attention?").
  - Click "Find Timestamps" to get a list of timestamps and direct links to those moments in the video.

- **Summarize:**  
  - Click "Summary" to get a summary of the video or the selected topic.

---

## Example

**Input:**
- URL: `https://www.youtube.com/watch?v=zxQyTK8quyY`
- Query: `What is positional encoding?`

**Output:**
- Timestamps:  
  - `08:16`  
  - `10:03`  
- Links:  
  - [https://www.youtube.com/watch?v=zxQyTK8quyY&t=496s](https://www.youtube.com/watch?v=zxQyTK8quyY&t=496s)
  - [https://www.youtube.com/watch?v=zxQyTK8quyY&t=603s](https://www.youtube.com/watch?v=zxQyTK8quyY&t=603s)

- **Summary:**  
  "Positional encoding is used in transformers to provide information about the order of words in the input sequence..."

---

## How It Works

1. **Transcript Extraction:**  
   The app fetches the transcript for the given YouTube video.
2. **Vector Store:**  
   The transcript is split into chunks and stored in a Chroma vector database with timestamp metadata.
3. **Semantic Search:**  
   User queries are embedded and used to search for the most relevant transcript chunks.
4. **Timestamp Extraction:**  
   The app extracts and displays the timestamps for the relevant transcript sections.
5. **Summarization:**  
   The app can summarize the transcript or the answer to the user's question using an LLM.

---

## Extending the Project

- **Add support for other video platforms.**
- **Improve chunking and context retrieval for better accuracy.**
- **Integrate with other LLM providers or vector stores.**

---

## License

MIT

---

## Acknowledgements

- [LangChain](https://python.langchain.com/)
- [OpenAI](https://openai.com/)
- [ChromaDB](https://www.trychroma.com/)
- [youtube-transcript-api](https://github.com/jdepoix/youtube-transcript-api)