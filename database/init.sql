CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL,
    email TEXT NOT NULL,
    password_hash TEXT NOT NULL
);

CREATE TABLE movies (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT NOT NULL,
    file_path TEXT NOT NULL,
    poster_url TEXT,
    duration INT
);

CREATE TABLE users_movies (
    user_id INT REFERENCES users(id),
    movie_id INT REFERENCES movies(id),
    added_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, movie_id)
);