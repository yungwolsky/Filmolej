FROM jrottenberg/ffmpeg:6.0-ubuntu

WORKDIR /app
COPY ./services/transcoder .

CMD ["bash"]