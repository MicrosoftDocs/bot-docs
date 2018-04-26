---
title: Create a bot using Bot Builder SDK for Python | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for Python.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/21/2018
monikerRange: 'azure-bot-service-4.0'
---
# Create a bot with the Bot Builder SDK for Python
The Bot Builder SDK for Python is an easy-to-use framework for developing bots. This quickstart walks you through building a bot, and then testing it with the Bot Framework Emulator. The Python SDK consists of a series of [libraries](https://github.com/Microsoft/botbuilder-python/tree/master/libraries). To build them locally, see [Building the SDK](https://github.com/Microsoft/botbuilder-python/wiki/building-the-sdk).

## Pre-requisite
[Python 3.6.4](https://www.python.org/downloads/) 

You will need to install the following 3 packages:
- [botframework-connector](https://pypi.org/project/botframework-connector/)
- [botbuilder-core](https://pypi.org/project/botbuilder-core/)
- [botbuilder-schema](https://pypi.org/project/botbuilder-schema/)

[Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator)

# Create a bot
In the main.py file, import the following standard modules:

```python
import http.server
import json
import asyncio
from botbuilder.schema import (Activity, ActivityTypes, ChannelAccount)
from botbuilder.core import BotFrameworkAdapter
```

And the following SDK modules:

```python
from botbuilder.schema import (Activity, ActivityTypes, ChannelAccount)
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

    def __handle_conversation_update_activity(self, activity: Activity):
        self.send_response(202)
        self.end_headers()
        if activity.members_added[0].id != activity.recipient.id:
            self._adapter.send([BotRequestHandler.__create_reply_activity(activity, 'Hello and welcome to the echo bot!')])

    def __handle_message_activity(self, activity: Activity):
        self.send_response(200)
        self.end_headers()
        self._adapter.send([BotRequestHandler.__create_reply_activity(activity, 'You said: %s' % activity.text)])

    def __unhandled_activity(self):
        self.send_response(404)
        self.end_headers()

    def on_receive(self, activity: Activity):
        if activity.type == ActivityTypes.conversation_update.value:
            self.__handle_conversation_update_activity(activity)
        elif activity.type == ActivityTypes.message.value:
            self.__handle_message_activity(activity)
        else:
            self.__unhandled_activity()

    def do_POST(self):
        body = self.rfile.read(int(self.headers['Content-Length']))
        data = json.loads(str(body, 'utf-8'))
        activity = Activity.deserialize(data)
        self._adapter = BotFrameworkAdapter(APP_ID, APP_PASSWORD)
        self._adapter.on_receive = self.on_receive
        self._adapter.receive(self.headers.get("Authorization"), activity)


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

- Start the Bot Framework Emulator, connect to your bot by using `http://localhost:9000/api/messages`. 
- The bot says 'Hello and welcome to the echo bot!' to welcome you.
- Type **Hello** in the emulator, and the bot will echo back **You said "Hello"**.
