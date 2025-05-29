import re
from youtube_transcript import fetch_youtube_transcript
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_core.documents import Document
from langchain_community.vectorstores import Chroma
from langchain_openai import OpenAIEmbeddings

def youtube_url_to_documents(url: str, chunk_size: int = 100, chunk_overlap: int = 0):
    """
    Given a YouTube URL, fetch the transcript and return a list of Document objects
    with transcript text and timestamp metadata, ready for vector store insertion.
    If the URL is already present in the vector store, do not insert again, just return the vector store.
    """
    # Initialize vector store
    vector_store = Chroma(
        embedding_function=OpenAIEmbeddings(),
        persist_directory='chroma_db',
        collection_name='youtube_transcripts'
    )
    # Extract video ID from URL
    video_id_pattern = r'(?:v=|\/)([0-9A-Za-z_-]{11}).*'
    video_id_match = re.search(video_id_pattern, url)

    video_id = video_id_match.group(1)

    # Check if the url is already present in the vector store metadata
    existing = vector_store.get(include=['metadatas'])
    for meta in existing.get('metadatas', []):
        if isinstance(meta, dict) and meta.get('url') == video_id:
            # URL already present, return the vector store as is
            return vector_store

    # If not present, fetch transcript and insert
    data = fetch_youtube_transcript(url)
    docs = [
        Document(
            page_content=entry['transcript'],
            metadata={
                "timestamp": f"{entry['minutes']:02d}:{entry['seconds']:02d}",
                "url": entry["url"]
            }
        )
        for entry in data
    ]
    vector_store.add_documents(docs)
    return vector_store


# def insert_documents_into_vector_store(docs, persist_directory='chroma_db', collection_name='youtube_transcripts'):
#     """
#     Inserts a list of Document objects into a Chroma vector store.
#     """
#     vector_store = Chroma(
#         embedding_function=OpenAIEmbeddings(),
#         persist_directory=persist_directory,
#         collection_name=collection_name
#     )
#     vector_store.add_documents(docs)
#     return vector_store

if __name__ == "__main__":
    url = "https://www.youtube.com/watch?v=PSs6nxngL6k"
    vector_store = youtube_url_to_documents(url)
   