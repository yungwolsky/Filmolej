CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL
);

CREATE TABLE movies (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    plot TEXT,
    director TEXT,
    release_date TEXT,
    genre TEXT,
    poster_url TEXT,
    duration TEXT,
    user_id INT REFERENCES users(id) ON DELETE CASCADE,

    original_file_path TEXT,
    stream_path TEXT,

    status TEXT NOT NULL CHECK (status IN ('pending', 'processing', 'ready', 'failed')),
    created_at TIMESTAMP DEFAULT NOW()
);