# Filmolej - Video Streaming Platform

A full-stack web application for uploading, managing and streaming video content

## Features

- User authentication with JWT
- Chunked video upload (large file support)
- Automatic video processing & HLS streaming
- Movie collection management
- Adaptive video playback using HLS (.m3u8, .ts)



## How It Works
  1. User uploads a video
  2. Backend reconstructs the file
  3. Background worker transcodes video to HLS format:
      - stream.m3u8
      - .ts segments
  4. Files are stored in /storage
  5. IIS serves video files directly
  6. Frontend plays video using HLS.js


## Running with Docker  
  1. Clone repository
```bash
    git clone https://github.com/your-username/filmolej.git
    cd filmolej
```

  2. Configure enviroment  

  Create .env file  

    DB_HOST=  
    DB_NAME=  
    DB_USER=  
    DB_PASSWORD=  

    MEDIA_PATH=/media  
    STREAM_PATH=/streams

  3. Run continers
```bash
    docker compose up --build
```

  4. Access application  
    - Frontend: http://localhost:3000  
    - Backend: http://localhost:5000
