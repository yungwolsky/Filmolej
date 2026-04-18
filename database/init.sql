CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL,
    email TEXT NOT NULL,
    password_hash TEXT NOT NULL
);

CREATE TABLE movies (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT,
    director TEXT,
    release_date TIMESTAMP,
    genre TEXT,

    original_file_path TEXT,
    stream_path TEXT,

    status TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);