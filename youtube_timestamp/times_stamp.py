from typing import List
from pydantic import Field, field_validator, BaseModel
import re

class TimeStamp(BaseModel):
    timestamp: List[str] = Field(description='This has a list of timestamps in MM:SS format like ["20:53", "20:58"]')

    @field_validator("timestamp")
    def validate_timestamp(cls, v):
        for ts in v:
            if not re.match(r"^\d{1,2}:\d{2}$", ts):
                raise ValueError("Timestamp must be in MM:SS format")
            minutes, seconds = map(int, ts.split(":"))
            if not (0 <= minutes and 0 <= seconds < 60):
                raise ValueError("Minutes must be >= 0 and seconds must be between 0 and 59")
        return v
