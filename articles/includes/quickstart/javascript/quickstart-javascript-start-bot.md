<!-- Include under ## Start you bot H2 header -->

In a terminal or command prompt change directories to the one created for your bot, and start it with `npm start`.

```bash
cd my-chat-bot
npm start
```

At this point, your bot is running locally on port 3978.

## Start the Emulator and connect your bot

1. Start the Bot Framework Emulator.

2. Click **Open Bot** on the Emulator's **Welcome** tab.

3. Enter your bot's URL, which is the URL of the local port, with /api/messages added to the path, typically `http://localhost:3978/api/messages`.

   <!--This is the same process in the Emulator for all three languages.-->
   ![open a bot screen](../media/python/quickstart/open-bot.png)

4. Then click **Connect**.

   Send a message to your bot, and the bot will respond back with a message.

   ![Emulator running](../media/emulator-v4/js-quickstart.png)
