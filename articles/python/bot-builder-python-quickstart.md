---
title: Create a bot using Bot Builder SDK for Python | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for Python.
keywords: Bot Builder SDK, create a bot, quickstart, python, getting started
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/21/2018
monikerRange: 'azure-bot-service-4.0'
---
# Create a bot with the Bot Builder SDK for Python
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

The Bot Builder SDK for Python is an easy-to-use framework for developing bots. This quickstart walks you through building a bot, and then testing it with the Bot Framework Emulator. The SDK v4 is in preview, visit Python [GitHub repo](https://github.com/Microsoft/botbuilder-python) for more information. 

## Pre-requisite
- [Python 3.6.4](https://www.python.org/downloads/) 
- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases)

# Create a bot
In the main.py file, import the following standard modules:

```python
import http.server
import json
import asyncio
```

And the following SDK modules:
```python
from botbuilder.schema import (Activity, ActivityTypes)
from botframework.connector import ConnectorClient
from botframework.connector.auth import (MicrosoftAppCredentials,
                                         JwtTokenValidation, SimpleCredentialProvider)
```
Next, add the following code to create the bot using the ConnectorClient:
```python
APP_ID = ''
APP_PASSWORD = ''


class BotRequestHandler(http.server.BaseHTTPRequestHandler):

    @staticmethod
    def __create_reply_activity(request_activity, text):
        return Activity(
            type=ActivityTypes.message,
            channel_id=request_activity.channel_id,
            conversation=request_activity.conversation,
            recipient=request_activity.from_property,
            from_property=request_activity.recipient,
            text=text,
            service_url=request_activity.service_url)

    def __handle_conversation_update_activity(self, activity):
        self.send_response(202)
        self.end_headers()
        if activity.members_added[0].id != activity.recipient.id:
            credentials = MicrosoftAppCredentials(APP_ID, APP_PASSWORD)
            reply = BotRequestHandler.__create_reply_activity(activity, 'Hello and welcome to the echo bot!')
            connector = ConnectorClient(credentials, base_url=reply.service_url)
            connector.conversations.send_to_conversation(reply.conversation.id, reply)

    def __handle_message_activity(self, activity):
        self.send_response(200)
        self.end_headers()
        credentials = MicrosoftAppCredentials(APP_ID, APP_PASSWORD)
        connector = ConnectorClient(credentials, base_url=activity.service_url)
        reply = BotRequestHandler.__create_reply_activity(activity, 'You said: %s' % activity.text)
        connector.conversations.send_to_conversation(reply.conversation.id, reply)

    def __handle_authentication(self, activity):
        credential_provider = SimpleCredentialProvider(APP_ID, APP_PASSWORD)
        loop = asyncio.new_event_loop()
        try:
            loop.run_until_complete(JwtTokenValidation.assert_valid_activity(
                activity, self.headers.get("Authorization"), credential_provider))
            return True
        except Exception as ex:
            self.send_response(401, ex)
            self.end_headers()
            return False
        finally:
            loop.close()

    def __unhandled_activity(self):
        self.send_response(404)
        self.end_headers()

    def do_POST(self):
        body = self.rfile.read(int(self.headers['Content-Length']))
        data = json.loads(str(body, 'utf-8'))
        activity = Activity.deserialize(data)

        if not self.__handle_authentication(activity):
            return

        if activity.type == ActivityTypes.conversation_update.value:
            self.__handle_conversation_update_activity(activity)
        elif activity.type == ActivityTypes.message.value:
            self.__handle_message_activity(activity)
        else:
            self.__unhandled_activity()


try:
    SERVER = http.server.HTTPServer(('localhost', 9000), BotRequestHandler)
    print('Started http server on localhost:9000')
    SERVER.serve_forever()
except KeyboardInterrupt:
    print('^C received, shutting down server')
    SERVER.socket.close()
```


Save main.py. To run the sample on Windows, enter the following into your command line window:
```
python main.py
```
In your local terminal you should see the message 'Started http server on localhost:9000'

### Start the emulator and connect your bot

Next, start the emulator and then connect to your bot in the emulator:


1. Click **create a new bot configuration** link in the emulator "Welcome" tab. 

2. Enter a **Bot name** and enter the directory path to your bot code. The bot configuration file will be saved to this path.

3. Type `http://localhost:port-number/api/messages` into the **Endpoint URL** field, where *port-number* matches the port number shown in the browser where your application is running.

4. Click **Connect** to connect to your bot. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you register your bot.

Type **Hello** in the emulator, and the bot will echo back **You said "Hello"**.

## Next steps

Next, jump into the concepts that explain a bot and how it works.

> [!div class="nextstepaction"]
> [Basic Bot concepts](../v4sdk/bot-builder-basics.md)
