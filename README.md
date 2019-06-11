# Docker-Compose-Redis-AspNetCore
Running Docker ASP.net Core and Redis

## Purpose
An example of working with ASP.net Core and Redis in Docker containers

## Use
- Clone the Repo
- If required change port bindings in `docker-compose.yml`
- Execute from the root folder `docker-compose build`
- Start the everything up `docker-compose up`
- Populate the Redis cache with dummy data by performing a `Http Post` action to `localhost/api/RedisExample`
- Access the dummy data using  `Http Get` to access `localhost/api/RedisExample`
- Finish by cleaning up... `docker-compose down`
