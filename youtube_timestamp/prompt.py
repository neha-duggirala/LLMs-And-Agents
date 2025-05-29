def format_context_for_llm(context):
    """
    Formats a list of Document objects into a readable string for LLM prompts,
    showing the timestamp and a summary of each chunk.
    """
    formatted = []
    for doc in context:
        timestamp = doc.metadata.get("timestamp", "")
        text = doc.page_content
        # Optionally, you can truncate or summarize text if it's too long
        snippet = text[:200] + ("..." if len(text) > 200 else "")
        formatted.append(f"Timestamp: {timestamp}\nContent: {snippet}\n")
    return "\n".join(formatted)

