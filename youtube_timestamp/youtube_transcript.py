import re
from typing import List
from youtube_transcript_api import YouTubeTranscriptApi
import os
import json

def fetch_youtube_transcript(url: str, lang: str = 'en') -> List[str]:
    """
    Extract transcript with timestamps from a YouTube video URL and format it for LLM consumption

    Args:
        url (str): YouTube video URL
        lang (str, optional): Language code for transcript. Defaults to 'en'.

    Returns:
        List[str]: Formatted transcript with timestamps, where each entry is on a new line
             in the format: "[MM:SS] Text"
    """
    # Extract video ID from URL
    video_id_pattern = r'(?:v=|\/)([0-9A-Za-z_-]{11}).*'
    video_id_match = re.search(video_id_pattern, url)

    if not video_id_match:
        raise ValueError("Invalid YouTube URL")

    video_id = video_id_match.group(1)
    # print(video_id)

    try:
        transcript = YouTubeTranscriptApi.get_transcript(video_id)
    except Exception as ParseError:
        raise RuntimeError("Failed to fetch transcript") from ParseError

    # Format each entry with timestamp and text
    formatted_entries = []
    for entry in transcript:
        # Convert seconds to MM:SS format
        minutes = int(entry['start'] // 60)
        seconds = int(entry['start'] % 60)
        # timestamp = f"[{minutes:02d}:{seconds:02d}] {entry['text']}"
        

        entry = {"minutes":minutes, "seconds": seconds, "transcript":entry['text'], "url": video_id }
        formatted_entries.append(entry)
    # Join all entries with newlines
    return formatted_entries
    
def extract_youtube_link(url, timestamp):
    '''
    This method takes a youtube url as an input along with a particular timestamp that is in MM:SS format and returns 
    a complete link that takes the user to that youtube video timestamp.
    '''
    # Extract video ID from URL
    video_id_pattern = r'(?:v=|\/)([0-9A-Za-z_-]{11}).*'
    video_id_match = re.search(video_id_pattern, url)
    if not video_id_match:
        raise ValueError("Invalid YouTube URL")
    video_id = video_id_match.group(1)

    # Convert MM:SS to seconds
    try:
        minutes, seconds = map(int, timestamp.split(":"))
        start_seconds = minutes * 60 + seconds
    except Exception:
        raise ValueError("Timestamp must be in MM:SS format")

    # Build the YouTube link with timestamp
    return f"https://www.youtube.com/watch?v={video_id}&t={start_seconds}s"


if __name__ == '__main__':
    youtube_url = "https://www.youtube.com/watch?v=zxQyTK8quyY"
    # Fetch transcript entries
    # transcript_entries = fetch_youtube_transcript(youtube_url)
    print(extract_youtube_link(youtube_url,"27:13"))


    # # Ensure transcripts directory exists
    # transcripts_dir = "transcripts"
    # os.makedirs(transcripts_dir, exist_ok=True)

    # # Save to JSON file
    # filepath = os.path.join(transcripts_dir, "transformers.json")
    # with open(filepath, "w", encoding="utf-8") as f:
    #     json.dump(transcript_entries, f, ensure_ascii=False, indent=2)

    # print(f"Transcript saved to {filepath}")