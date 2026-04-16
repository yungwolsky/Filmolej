 create table Users (
    id int primary key,
    username text not null,
    email text not null,
    password_hash text not null
)

create table Movies (
    id int primary key,
    title text not null,
    descritpion text not null,
    file_path text not null,
    poster_url text,
    duration int 
)

create table Users_Movies (
    user_id int references users(id),
    movie_id int references movies(id),
    added_at datetime,
    primary key (user_id, movie_id)
)