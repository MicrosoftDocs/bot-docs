---
title: Connect a bot to Alexa
description: Learn how to configure your bot in Azure to allow communication with Alexa.
keywords: connect a bot, bot channel, Alexa bot, credentials, configure, phone
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 03/22/2022
ms.custom:
  - evergreen
---

# Connect a bot to Alexa

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people through an Alexa custom skill. This article describes how to create an Alexa skill using the Alexa Developer Console, connect your bot to your Alexa skill in Azure, and test your bot in Alexa.

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to Alexa.
- An Amazon account.

> [!IMPORTANT]
> Your bot must use the [Bot Framework SDK](https://github.com/microsoft/botframework-sdk) version 4.8 or later. If you have an existing bot you may need to update your SDK version and republish your bot.

## Create an Alexa skill

1. Sign in to the [Alexa Developer Console](https://developer.amazon.com/alexa/console/ask) and select **Create Skill**.

1. On the next page:
    1. Enter a name for your new skill.
    1. Make sure that **Choose a model to add to your skill** is set to **Custom**.
    1. Make sure that **Choose a method to host your skill's backend resources** is set to **Provision your own**.
    1. Select **Create Skill**.

    :::image type="content" source="media/channels/alexa-create-skill-options.png" alt-text="Choose model and hosting":::

1. On the next page:
    1. Make sure that **Choose a template** is set to **Start from scratch**
    1. Select **Choose**.

    :::image type="content" source="media/channels/alexa-create-skill-options2.png" alt-text="Choose a template":::

1. On your skill dashboard under **Interaction Model**, select **JSON Editor**.

1. In the JSON editor:
    1. Replace the existing contents with the following JSON.

        ```json
        {
            "interactionModel": {
                "languageModel": {
                    "invocationName": "<your-skill-invocation-name>",
                    "intents": [
                        {
                            "name": "GetUserIntent",
                            "slots": [
                                {
                                    "name": "phrase",
                                    "type": "phrase"
                                }
                            ],
                            "samples": [
                                "{phrase}"
                            ]
                        },
                        {
                            "name": "AMAZON.StopIntent",
                            "samples": []
                        }
                    ],
                    "types": [
                        {
                            "name": "phrase",
                            "values": [
                                {
                                    "name": {
                                        "value": "<example-phrase>"
                                    }
                                },
                                {
                                    "name": {
                                        "value": "<example-phrase>"
                                    }
                                },
                                {
                                    "name": {
                                        "value": "<example-phrase>"
                                    }
                                }
                            ]
                        }
                    ]
                }
            }
        }
        ```

    1. For `invocationName`, change \<your-skill-invocation-name> to the name that users will use to invoke your skill on Alexa. For example, if your skill invocation name was "adapter helper", then a user could say "Alexa, launch adapter helper" to launch the skill.

    1. In the `values` array under `types`, replace the three instances of `<example-phrase>` with phrases that users can say to trigger your skill. For example, if a user says "Alexa, ask adapter helper to give me details of the alexa adapter", one example phrase could be "give me details of the alexa adapter".

1. Select **Save Model**, then select **Build Model**. This updates your skill's configuration on Alexa.

## Configure your bot in Azure

To complete this step, you'll need your Alexa Skill ID. Get the ID either from the URL in the Alexa portal or by going to the [Alexa Developer Console](https://developer.amazon.com/alexa/console/ask) and selecting **Copy Skill ID**. Your Alexa Skill ID should be a value like "amzn1.ask.skill.\<some-guid>".

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Alexa**.
1. In **Alexa Channel Configuration**, enter the information you copied in the previous step.
    1. In **Enter skill Id**, enter the Alexa skill ID that you copied from the Alexa Developer Console.
    1. Select **Apply**.
    1. Copy the Azure-generated Alexa service endpoint URI.

## Update your Alexa bot registration

1. Sign in to the [Alexa Developer Console](https://developer.amazon.com/alexa/console/ask).
1. Find and open your skill's configuration page.
1. Select **Endpoint**.
1. For **Service Endpoint Type**, select **HTTPS**.
1. For **Default Region**:
    1. Enter the Alexa service endpoint URI you copied from the Azure portal.
    1. In the drop-down, select **My development endpoint is a sub-domain of a domain that has a wildcard certificate from a certificate authority**.

    :::image type="content" source="media/channels/alexa-endpoint.png" alt-text="Set service endpoint and endpoint type":::

1. Select **Save Endpoints**.

## Test and publish your skill

If you own an Alexa device, you can test your skill before you publish it.

See the [Alexa Skills Kit developer documentation for custom voice model skills](https://developer.amazon.com/docs/alexa/custom-skills/understanding-custom-skills.html) for information on how to test and publish your skill on their platform.

## Additional information

For more information about Alexa skills, see the Amazon developer documentation:

- [What is the Alexa Skills Kit?](https://developer.amazon.com/docs/alexa/ask-overviews/what-is-the-alexa-skills-kit.html)
- [Custom Voice Model Skills](https://developer.amazon.com/docs/alexa/custom-skills/understanding-custom-skills.html)
- [Alexa Skills Kit Object Schemas](https://developer.amazon.com/docs/alexa/smapi/object-schemas.html)

### User authentication in Alexa

User authentication in Alexa is done by setting up and using **Account Linking on the Alexa skill**.
For more information, see [Understand Account Linking for Alexa Skills](https://developer.amazon.com/docs/alexa/account-linking/understand-account-linking.html).
You can require account linking when the user enables the skill, or you can require it as part of a conversation flow.

If you add user authentication as part of the conversation:

1. Attach a [sign-in card](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md#signin-card) on the outgoing activity. This will be converted to an Alexa **LinkAccount card** that prompts the user to sign in using the Alexa app.

1. If the user successfully links their account into the app, a token is then available on subsequent requests in the channel data.

## Next steps

- For information about building bots, see [How bots work](v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Tutorial: Provision a bot in Azure](tutorial-provision-a-bot.md) and [Tutorial: Publish a basic bot](tutorial-publish-a-bot.md).
- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](bot-service-manage-channels.md).
- For more information about the Bot Framework schemas, see the [Bot Framework activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) and the [Bot Framework cards schema](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md).
