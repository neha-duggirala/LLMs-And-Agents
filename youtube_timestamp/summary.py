from youtube_transcript import fetch_youtube_transcript

def get_full_transcript(input):
    url = input["url"]
    transcript = fetch_youtube_transcript(url)
    full_transcript = [each['transcript'] for each in transcript]
    return " ".join(full_transcript)

    

# url= 'https://www.youtube.com/watch?v=XpoKB3usmKc'
# print(get_full_transcript(url))
