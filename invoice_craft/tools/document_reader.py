from langchain.tools import Tool
from typing import Union
import os

# PDF and DOCX parsing
import fitz  # PyMuPDF
import docx2txt

# OCR for images
import pytesseract
from PIL import Image

def extract_text_from_pdf(file_path: str) -> str:
    text = ""
    with fitz.open(file_path) as doc:
        for page in doc:
            text += page.get_text()
    return text

def extract_text_from_docx(file_path: str) -> str:
    return docx2txt.process(file_path)

def extract_text_from_image(file_path: str) -> str:
    image = Image.open(file_path)
    return pytesseract.image_to_string(image)

def read_document(file_path: str) -> str:
    ext = os.path.splitext(file_path)[1].lower()
    if ext == ".pdf":
        return extract_text_from_pdf(file_path)
    elif ext == ".docx":
        return extract_text_from_docx(file_path)
    elif ext in [".png", ".jpg", ".jpeg"]:
        return extract_text_from_image(file_path)
    else:
        raise ValueError(f"Unsupported file type: {ext}")

# LangChain Tool wrapper
document_reader_tool = Tool(
    name="Document Reader",
    func=read_document,
    description="Reads a document (PDF, DOCX, or image) and returns raw text."
)
