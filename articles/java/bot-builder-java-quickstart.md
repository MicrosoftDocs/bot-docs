---
title: Create a bot using Bot Builder SDK for Java | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for Java.
keywords: Bot Builder SDK, create a bot, quickstart, java, getting started
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 05/02/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot with the Bot Builder SDK for Java
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

The Bot Builder SDK for Java provides a familiar way for Java developers to write bots. The SDK v4 is in preview, for more information visit Java [GitHub repo](https://github.com/Microsoft/botbuilder-java).

> [!NOTE]
> Our code samples and docs are currently targeting Java version 1.8.

## Getting Started

The v4 SDK consists of a series of [libraries](https://github.com/Microsoft/botbuilder-java/tree/master/libraries). To build them locally, see [Building the SDK](https://github.com/Microsoft/botbuilder-java/wiki/building-the-sdk).

- Install [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Create EchoBot

In the App.java file add the following:

```Java
package com.microsoft.bot.connector.sample;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.microsoft.aad.adal4j.AuthenticationException;
import com.microsoft.bot.connector.customizations.CredentialProvider;
import com.microsoft.bot.connector.customizations.CredentialProviderImpl;
import com.microsoft.bot.connector.customizations.JwtTokenValidation;
import com.microsoft.bot.connector.customizations.MicrosoftAppCredentials;
import com.microsoft.bot.connector.implementation.ConnectorClientImpl;
import com.microsoft.bot.schema.models.Activity;
import com.microsoft.bot.schema.models.ActivityTypes;
import com.microsoft.bot.schema.models.ResourceResponse;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import com.sun.net.httpserver.HttpServer;

import java.io.IOException;
import java.io.InputStream;
import java.net.InetSocketAddress;
import java.net.URLDecoder;
import java.util.logging.Level;
import java.util.logging.Logger;

public class App {
    private static final Logger LOGGER = Logger.getLogger( App.class.getName() );
    private static String appId = "";       // <-- app id -->
    private static String appPassword = ""; // <-- app password -->

    public static void main( String[] args ) throws IOException {
        CredentialProvider credentialProvider = new CredentialProviderImpl(appId, appPassword);
        HttpServer server = HttpServer.create(new InetSocketAddress(3978), 0);
        server.createContext("/api/messages", new MessageHandle(credentialProvider));
        server.setExecutor(null);
        server.start();
    }

    static class MessageHandle implements HttpHandler {
        private ObjectMapper objectMapper;
        private CredentialProvider credentialProvider;
        private MicrosoftAppCredentials credentials;

        MessageHandle(CredentialProvider credentialProvider) {
            this.objectMapper = new ObjectMapper()
                    .configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false)
                    .findAndRegisterModules();
            this.credentialProvider = credentialProvider;
            this.credentials = new MicrosoftAppCredentials(appId, appPassword);
        }

        public void handle(HttpExchange httpExchange) throws IOException {
            if (httpExchange.getRequestMethod().equalsIgnoreCase("POST")) {
                Activity activity = getActivity(httpExchange);
                String authHeader = httpExchange.getRequestHeaders().getFirst("Authorization");
                try {
                    JwtTokenValidation.assertValidActivity(activity, authHeader, credentialProvider);

                    // send ack to user activity
                    httpExchange.sendResponseHeaders(202, 0);
                    httpExchange.getResponseBody().close();

                    if (activity.type().equals(ActivityTypes.MESSAGE)) {
                        // reply activity with the same text
                        ConnectorClientImpl connector = new ConnectorClientImpl(activity.serviceUrl(), this.credentials);
                        ResourceResponse response = connector.conversations().sendToConversation(activity.conversation().id(),
                                new Activity()
                                        .withType(ActivityTypes.MESSAGE)
                                        .withText("Echo: " + activity.text())
                                        .withRecipient(activity.from())
                                        .withFrom(activity.recipient())
                        );
                    }
                } catch (AuthenticationException ex) {
                    httpExchange.sendResponseHeaders(401, 0);
                    httpExchange.getResponseBody().close();
                    LOGGER.log(Level.WARNING, "Auth failed!", ex);
                } catch (Exception ex) {
                    LOGGER.log(Level.WARNING, "Execution failed", ex);
                }
            }
        }

        private String getRequestBody(HttpExchange httpExchange) throws IOException {
            StringBuilder buffer = new StringBuilder();
            InputStream stream = httpExchange.getRequestBody();
            int rByte;
            while ((rByte = stream.read()) != -1) {
                buffer.append((char)rByte);
            }
            stream.close();
            if (buffer.length() > 0) {
                return URLDecoder.decode(buffer.toString(), "UTF-8");
            }
            return "";
        }

        private Activity getActivity(HttpExchange httpExchange) {
            try {
                String body = getRequestBody(httpExchange);
                LOGGER.log(Level.INFO, body);
                return objectMapper.readValue(body, Activity.class);
            } catch (Exception ex) {
                LOGGER.log(Level.WARNING, "Failed to get activity", ex);
                return null;
            }

        }
    }
}
```

If you are using Maven you can copy the pom.xml file from the samples folder in this repo. Once you have started running your executable, start the Bot Framework Emulator.

### Start the emulator and connect your bot

At this point, your bot is running locally.
Next, start the emulator and then connect to your bot in the emulator:

1. Click **create a new bot configuration** link in the emulator "Welcome" tab. 

2. Enter a **Bot name** and enter the directory path to your bot code. The bot configuration file will be saved to this path.

3. Type `http://localhost:port-number/api/messages` into the **Endpoint URL** field, where *port-number* matches the port number shown in the browser where your application is running.

4. Click **Connect** to connect to your bot. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you register your bot.

### Interact with your bot
Send "Hi" to your bot, and the bot will echo the message back.

## Next steps

Next, [deploy your bot to azure](../bot-builder-howto-deploy-azure.md) or jump into the concepts that explain a bot and how it works.

> [!div class="nextstepaction"]
> [Basic Bot concepts](../v4sdk/bot-builder-basics.md)
