# Documenting the REST APIs

We have 3 REST APIs:

- Microsoft Bot Connector API - v3.0
- Bot Connector - Direct Line API - V1.1
- Bot Connector - Direct Line API - v3.0

All languages of the Bot Builder SDK, and both versions (v3 and v4), are built on top of the Bot Connector v3 REST API.

## Swagger files

The current source of truth (circa March 2020) for the REST APIs are their associated Swagger files is located in the [botframework-obi](https://github.com/microsoft/botframework-obi) repo, under [protocols/botframework-protocol/](https://github.com/microsoft/botframework-obi/tree/master/protocols/botframework-protocol).

Copies of these _should_ propagate to the public [botframework-sdk](https://github.com/microsoft/botframework-sdk) repo, under [specs/botframework-protocol/](https://github.com/microsoft/botframework-sdk/tree/master/specs/botframework-protocol).

## Swagger-ui

Swagger UI consumes Swagger files and presents them in a much more readable format.

Follow instructions at [Swagger-ui setup](https://github.com/swagger-api/swagger-ui/blob/master/docs/development/setting-up.md) to set up and run Swagger-ui.

Swagger UI runs on its own [local] server.

### Inspecting JSON Swagger files

You need to host the Swagger files to access them from Swagger UI. You can use a basic Node.js server for this.

From a directory containing the swagger files you want to inspect:

    1. Install the http-server [only necessary to install or update the server package] `npm i -g http-server`.
    2. Run with CORS, `http-server --cors`.
    3. Use `http://localhost:8080/<rel-path-to-file>` to access the Swagger file from the Swagger-ui page.
