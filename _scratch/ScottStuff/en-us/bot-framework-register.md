---
layout: page
title: Registering a Bot
permalink: /en-us/registration/
weight: 4030
parent1: none
---

Before users can use your bot, you must register it with the framework. You may register only bots that were developed using Bot Framework. Registration is a simple process that asks you a few questions about your bot and then returns you an app ID and password. You can run your bot in the [emulator](/en-us/bot-framework-emulator/) without the app ID and password, but in order for users to use your bot, it must be registered and you must have an app ID and password.

To register your bot:

1. Go to the [developer portal](https://dev.botframework.com) and click **Register a bot**. 
2. Upload a PNG image that represents your bot in the conversation. The icon is limited to 30 KB.
3. Provide your bot's name. If you publish the bot, this is the name that's used in Bot Directory. The name is limited to 35 characters.
4. Provide your bot's handle. This is the name that's used in the conversation. Pick a good handle because you cannot change it after you register the bot. The handle may contain only alphanumeric and underscore characters.
5. Provide a description of your bot. The description is displayed in the directory so it needs to describe what your bot does. The description is limited to 512 characters. The first 46 characters are displayed on your bot's card in the directory, and the full description is displayed in your bot's details page in the directory.
6. Provide your bot's HTTPS endpoint. This is the endpoint where your bot will receive HTTP POST messages from Bot Connector. 
7. Create an app ID and password by clicking **Create Microsoft App ID and password**.  
  
    - When you click Create, you’re taken to another page where you click **Generate an app password to continue** to get your password. Copy and securely store the password.  
    - Click **Finish and go back to Bot Framework**.  
    - When you come back, the app ID field is populated for you. Paste the password that you copied into the password field.    
    - Note that you can manage passwords anytime in the Bot Framework portal. Click **My bots**, select the bot, and in the Details section, click **Edit**. Under Configuration, click **Manage Microsoft App ID and password**.  

8. Provide a comma-delimited list of email address of the owners of the bot. You must provide monitored emails because the framework will send all communications to these emails.
9. If you host your bot in Azure and use Azure App Insights, provide your insights key.


By registering your bot, you agree to the [Privacy Statement](https://aka.ms/bf-privacy), [Terms of Use](https://aka.ms/bf-terms), and [Code of Conduct](https://aka.ms/bf-conduct).


<span style="color:red"><< Feedback on webpage:

- Regarding the icon, "30K" s/b "30 KB", and "png" s/b "PNG"
- I don't think any of the text that you prefill for name, handle, or description provides any benefit. If you keep them, "Type in your Bot handle" s/b "Type in your bot's handle."
- Under Configuration, I think it's odd that it says, "Register your bot with Microsoft to generate..." because they're on the registration page.

<< Where's the bot's handle used? Isn't the handle used in the conversation? The field's help says, "Used in the URL for your bot." Which URL? I thought the bot's endpoint was specified under Configuration and had nothing to do with the handle. 

What's the maximum length of the handle? >>

<< Why do both the registration page and publishing page include icon upload, name, and description?

Is correspondance sent to the owner's email specified on the registration page or the publisher email specified on the publisher page?

What's the difference between the owner email and publisher email?

They can only register bots developed using BF, correct?
>> </span>
