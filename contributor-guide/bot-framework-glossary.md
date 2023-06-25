# Bot Framework glossary

Contains a consolidated list of collected terms for the Bot Framework SDK docs, and by extension, the Composer docs. This is not meant to be the definitive source of truth, but notes taken as we hunt down the official terms to use. Meant to be a living document, as definitions and official terms will change over time.

To try to keep this from exploding, the intent to to capture terms that either are not easy to otherwise look up or we are constantly looking up but are not likely to change frequently.

### Resources

These should be considered the sources of truth, and you _should not_ "use Microsoft.com, any other web page, or product UI as a source for product and feature names, capitalization, spelling, and usage".

#### On Corpnet

- [Microsoft Cloud Style Guide][] / **A&ndash;Z names + terms dictionary** &mdash; Cloud-specific style guide.
- [Microsoft Writing Style Guide][] / **A&ndash;Z word list and term collections** &mdash; Microsoft-wide style guide.
- [Microsoft 365 Style Guide][] / **A&ndash;Z word list** &mdash; for terms specific to Microsoft 365, in addition to Windows, Office 365, and Microsoft Teams.
- [Term Studio](https://termstudio.azurewebsites.net/default.aspx) &mdash; This is used by localization and is not meant for our consumption. However, in the absence of other material, it is sometimes useful for identifying the correct version of a product name.

> [!TIP]
> Use [Search for a term](https://styleguides.azurewebsites.net/StyleGuide/Search) to search across all style guides.

[Microsoft Cloud Style Guide]: https://styleguides.azurewebsites.net/StyleGuide/Read?id=2696
[Microsoft Writing Style Guide]: https://styleguides.azurewebsites.net/StyleGuide/Read?id=2700
[Microsoft 365 Style Guide]: https://styleguides.azurewebsites.net/StyleGuide/Read?id=2869

#### On the web

- [American Heritage Dictionary][] &mdash; Source of truth for [many] common (non-Microsoft-specific) terms.
- Other resources &mdash; For targeted use, when the standard resources come up short: [GitHub glossary][].

[American Heritage Dictionary]: https://www.ahdictionary.com/
[GitHub glossary]: https://docs.github.com/github/getting-started-with-github/github-glossary

#### Other

When in doubt and we don't have guidance yet, check with an appropriate subject matter expert, and get a second opinion, if the first answer seems odd or otherwise dubious.

- Who on the Bot Framework team can answer these questions is more difficult than it should be. Marieke may be able to field a response.

### General entry format

- **Heading**: The complete term to use on first mention, including proper casing and official acronym (if applicable).
  - **Status**: Indicates whether the term is approved, internal jargon, or other.
  - **Short form**: If applicable, shorter forms of the term that can be used on subsequent mention in an article, as long as context makes it clear.
  - **Forms**: For some product names, lists the various forms the name can take, with the "the" included, if applicable.
  - **Definition**: More or less official definition of the term.
  - **Notes**: Additional notes, if any.
  - **Examples**: Included for terms that have multiple uses or meanings.
  - **See**: Related pages outside the glossary.
  - **See also**: Links to related terms.

For some terms that include a group of products, services, tools, and so on, such as [Microsoft Bot Framework](#microsoft-bot-framework), the various items might be grouped into a table.

### Context

For many terms that start with the word Microsoft or Azure, like Microsoft Azure App Service Environment, the _Microsoft_ or _Azure_ bit can be left off if the context is obvious within the article itself. Remember that readers may land on any given page from a search.

<hr/>

[#](#symbol) &MediumSpace;
[A](#a) &MediumSpace; [B](#b) &MediumSpace; [C](#c) &MediumSpace; [D](#d) &MediumSpace; [E](#e) &MediumSpace;
[F](#f) &MediumSpace; [G](#g) &MediumSpace; [H](#h) &MediumSpace; [I](#i) &MediumSpace; [J](#j) &MediumSpace;
[K](#k) &MediumSpace; [L](#l) &MediumSpace; [M](#m) &MediumSpace; [N](#n) &MediumSpace; [O](#o) &MediumSpace;
[P](#p) &MediumSpace; [Q](#q) &MediumSpace; [R](#r) &MediumSpace; [S](#s) &MediumSpace; [T](#t) &MediumSpace;
[U](#u) &MediumSpace; [V](#v) &MediumSpace; [W](#w) &MediumSpace; [X](#x) &MediumSpace; [Y](#y) &MediumSpace;
[Z](#z)

<hr/>

<a id="symbol"></a>

## .dialog file

- **Status**: Informal, part of the Bot Framework, consumed by Composer, operated on by the CLI.
- **Definition**: A file that contains declarative assets for adaptive dialogs (and uses the .dialog extension).
- **See**: OBI's (Microsoft internal) [fileformat/dialog folder](https://github.com/microsoft/botframework-obi/tree/main/fileformats/dialog) for archeological information.
- **See also**: [declarative dialog](#declarative-dialog).

## .lg file

- **Status**: Informal, part of the Bot Framework, consumed by Composer, operated on by the CLI.
- **Definition**: A file that describes response generation templates for adaptive dialogs (and uses the .lg extension).
- **See**: OBI's (Microsoft internal) [fileformat/lg folder](https://github.com/microsoft/botframework-obi/tree/main/fileformats/lg) for archeological information.
- **See also**: [response generation template](#response-generation-template).

## .lu file

- **Status**: Informal, part of the Bot Framework, consumed by Composer, operated on by the CLI.
- **Definition**: A file that contains language understanding assets (and uses the .lu extension).
- **See**: OBI's (Microsoft internal) [fileformat/lu folder](https://github.com/microsoft/botframework-obi/tree/main/fileformats/lu) for archeological information.
- **See also**: [language understanding](#language-understanding).

## .qna file

- **Status**: Informal, part of the Bot Framework, consumed by Composer, operated on by the CLI.
- **Definition**: A file that contains data for a QnA Maker knowledge base (and uses the .qna extension).
- **See**: OBI's (Microsoft internal) [fileformat/qna folder](https://github.com/microsoft/botframework-obi/tree/main/fileformats/qna) for archeological information.

<a id="a"></a>

## ~ABS~

Don't use. See [Azure AI Bot Service](#azure-bot-service).

## action

- **Status**: Complicated.
- **Definition**: Depends on the context. In the abstract, an action is anything that could be recognized as an event by another part of the system.
- **Notes**: Many things that could create an event are called an action, so always clarify 1) what category of action you're talking about, and 2) as applicable, what type of action within that category. For this same reason, don't casually call something an action that hasn't been defined specifically as one.
- **See also**: [card action](#card-action), [event](#event), [skill](#skill).

## Actionable Messages

See [Outlook Actionable Messages](#outlook-actionable-messages)

## actionable

- **Status**: In general, don't use; however.
- **See also**: [Outlook Actionable Messages](#outlook-actionable-messages)

## actionable message

- **Status**: Approved, in context.
- **See also**: [Outlook Actionable Messages](#outlook-actionable-messages)

## activity

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: "Each activity object includes a type field and represents a single action: most commonly, sending text content, but also including multimedia attachments and non-content behaviors like a 'like' button or a typing indicator."
- **See**: The [Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).
- **See also**: [message activity](#message), [event activity](#event).

## activity handler

- **Status**: Informal, part of the Bot Framework SDK.
- **Definition**: An event-driven way to organize the conversational logic for your bot.
- **See**: [Event-driven conversations](https://learn.microsoft.com/azure/bot-service/bot-activity-handler-concept) in the SDK docs.
- **See also**: [activity](#activity), [event](#event), [Teams activity handler](#teams-activity-handler).

<a id="adaptive-card"></a>
<a id="adaptive-cards"></a>

## Adaptive Card(s)

- **Status**: Product/technology name.
- **Forms**:
  - Adaptive Cards
  - the Adaptive Cards format
  - an Adaptive Card
  - a card
- **Definition**:
  - "Adaptive Cards are an open card exchange format enabling developers to exchange UI content in a common and consistent way."
  - "An Adaptive Card is a JSON-serialized card object model."
- **Notes**: _Adaptive Cards_ (plural) is the official name of the format. _Adaptive Card_ (singular) refers to a JSON object in that format or a rendering of such a card.
- **See**: [Adaptive Cards](https://learn.microsoft.com/adaptive-cards) docs.
- **See also**: [card](#card).

## adaptive dialog

- **Status**: Informal, part of the Bot Framework SDK.
- **Definition**:
  - "A type of container dialog that allows for flexible conversation flow. It includes built-in support for language recognition, language generation, and memory scoping features. To run an adaptive dialog (or another dialog that contains an adaptive dialog), you must start the dialog from a dialog manager."
  - "Adaptive dialogs offer a new event-based addition to the dialogs library that enables you to easily layer in sophisticated conversation management techniques like handling interruptions, dispatching, and more."
- **See**: List of [dialog types](https://learn.microsoft.com/azure/bot-service/bot-builder-concept-dialog#dialog-types) in the SDK docs.
- **See also**: [dialog](#dialog), [dialogs library](#dialogs-library).

## Adaptive Tools

See [[Microsoft] Bot Framework Adaptive Tools](#bot-framework-adaptive-tools).

## API

- **Status**: Approved.
  See [APIs](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=40722) in the Cloud style guide.
- **Definition**: APIs create rules for how two applications communicate and interact with each other. APIs disclose parts of a service application's code, allowing a second application to accomplish tasks using the service.
- **Notes**:
  - Don't spell out _application programming interface_.
  - For Bot Framework, we use the artificial distinction of API for the REST services (Connector, Direct Line) and SDK for the Bot Framework SDK (built on top of the Connector REST API).
  - When ambiguous, qualify which API you're talking about. This might also include qualifying the release version or programming language.
- **See also**: [SDK](#sdk), [REST API](#rest-api).

## App Service Environment

- **Status**: Approved and being updated in the online style guides.
- **Forms**:
  - the App Service Environment &mdash; the Azure App Service feature
  - an App Service environment &mdash; an instance of such an environment
  - App Service environments &mdash; multiple instances of such environments (avoid unless the article specifically needs to talk about more than one)
  - the/an environment &mdash; On subsequent use and if the context is clear, can use as shorthand for the feature or an instance
- **Definition**: "The App Service Environment is an Azure App Service feature that provides a fully isolated and dedicated environment for securely running App Service apps at high scale."
- **Notes**:
  - Don't use ASE as an acronym.
- **See**:
  - [Introduction to the App Service Environments](https://learn.microsoft.com/azure/app-service/environment/intro)
  - [[Azure] App Service](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=25375) in the Cloud style guide
- **See also**: [Direct Line App Service extension](#direct-line-app-service-extension).

## ARM

Don't use. See [Azure Resource Manager](#azure-resource-manager).

## ARM template

See notes for [Azure Resource Manager](#azure-resource-manager).

## attachment

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: An object to be displayed or otherwise included as part of an activity.
- **See**: The [Attachments](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#attachments) section of the Activity schema.
- **See also**: [activity](#activity), [carousel](#carousel).

<a id="microsoft-azure-active-directory"></a>
<a id="azure-active-directory"></a>

## [Microsoft] Azure Active Directory

- **Status**: Approved. See [Azure Active Directory](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=25369) in the Cloud style guide.
- **Definition**: "The identity service in Microsoft Azure that provides identity management and access control capabilities through a REST-based API. (The on-premises product is Windows Server Active Directory.)"
- **Forms**:
  - Microsoft Azure Active Directory
  - Azure Active Directory
  - Azure AD
- **Notes**: Don't use _AAD_ as an acronym.

<a id="microsoft-azure-bot-service"></a>
<a id="azure-bot-service"></a>

## [[Microsoft] Azure] Bot Service

- **Status**: Approved.
  See [Bot Service](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=42286) in the Cloud style guide.
- **Forms**:
  - the Microsoft Azure AI Bot Service
  - the Azure AI Bot Service
  - the Bot Service
  - the service
- **Definition**: Provides an integrated environment for bot development.
- **Notes**: Don't use ABS as an acronym.
- **See**: The [Azure AI Bot Service](https://azure.microsoft.com/services/bot-services/) marketing site.
- **See also**: [Microsoft Bot Framework](#microsoft-bot-framework), [bot service](#bot-service).

<a id="resource-manager"></a>

## Azure Resource Manager

- **Status**: Approved. See [Resource Manager](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=39369) in the Cloud style guide.
- **Forms**:
  - Azure Resource Manager
  - Resource Manager
- **Notes**:
  - When the name appears by itself (as a noun), don't precede it with "the". But if the name functions as an adjective, the name can be preceded by "the".
  - Don't use ARM or RM as an acronym.
  - You can use _ARM template_ only after spelling out at first mention, as in, "You can use this Azure Resource Manager template (ARM template) to do..."

<a id="b"></a>

## bot

- **Status**: Informal.
- **Definition**: "Bots provide an experience that feels less like using a computer and more like dealing with a person - or at least an intelligent robot."
- **Notes**: As a generic term, a _bot_ could also refer to a bot developed for another platform using a differ framework. If needed, qualify when you're talking about a bot developed using the Bot Framework SDK (or Composer).
- **See**: [What is a bot?](https://learn.microsoft.com/azure/bot-service/bot-service-overview-introduction#what-is-a-bot) in the SDK **What is the Bot Framework SDK?** article.
- **See also**: [bot service](#bot-service), [Azure AI Bot Service](#azure-bot-service).

## bot adapter

- **Status**: Informal, approved.
- **Short form**: adapter
- **Definition**: "Represents a bot adapter that can connect a bot to a service endpoint."
- **Notes**:
  - There are two categories of adapters, Azure AI Bot Service adapters and channel adapters. For Bot Service adapters, the Azure AI Bot Service handles translation between the channel's schema and the Bot Framework activity schema. Channel adapters bypass the Bot Service and have to do this translation themselves.
  - A bot adapter is the generic term for any object that serves the purpose of an adapter for a bot.
  - A specific bot's adapter is the particular adapter that bot uses.
  - For a bot that uses a channel adapter, it could use multiple ones to support different channels.
- **See**: [The bot adapter](https://learn.microsoft.com/azure/bot-service/bot-builder-basics#the-bot-adapter) in the SDK **How bots work** article.
- **See also**: [channel](#channel), [channel adapter](#channel-adapter).

## Bot Framework

See [Microsoft Bot Framework](#microsoft-bot-framework).

## Bot Framework Connector

See [Microsoft Bot Framework Connector](#microsoft-bot-framework-connector).

## bot service

- **Status**: Informal.
- **Definition**: An instance created by the user using the Azure AI Bot Service.
- **See also**: [Azure AI Bot Service](#azure-bot-service).

<a id="c"></a>

## card

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: An interactive card for use within chat and other applications.
- **See**: The [Cards schema](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md).
- **See also**: [Adaptive Card](#adaptive-card), [attachment](#attachment).

## card action

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: A clickable or interactive button for use within cards or as suggested actions; used to solicit input from users.
- **See**: The [Card action](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#card-action) section in the Activity schema.
- **See also**: [card](#card).

## carousel

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: A side-by-side, potentially scrollable, arrangement for attachments.
- **See**: The [Attachment layout](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#attachment-layout) section in the Activity schema.
- **See also**: [attachment](#attachment).

## channel

## channel adapter

## chatbot

See [bot](#bot).

<a id="ci"></a>
<a id="cd"></a>

## CI/CD

- **Status**: Approved. See [CI/CD](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=47522) in the Cloud style guide.
- **Definition**: Continuous integration and continuous delivery.
- **Notes**: Spell out on first mention.
- **Example**: From [Build a CI/CD pipeline for chatbots with ARM templates](https://learn.microsoft.com/azure/architecture/example-scenario/apps/devops-cicd-chatbot), "This article presents a DevOps approach to setting up a continuous integration and continuous deployment (CI/CD) pipeline that deploys a chatbot app and its infrastructure as code."

## CLI

- **Status**: Approved.
- **Notes**:
  - Don't spell out _command-line interface_.
  - Qualify which CLI you're referring to on first mention. You can shorten to _the CLI_ if the context makes it clear which CLI you mean.
- **See also**: [Bot Framework CLI](#bot-framework-cli).

## Composer

See [Microsoft Bot Framework](#microsoft-bot-framework).

## continuous development

See [CI/CD](#cicd).

## continuous integration

See [CI/CD](#cicd).

<a id="d"></a>

## declarative dialog

- **Status**: Informal, part of the Bot Framework SDK and Composer, mostly under the covers for now. Try to avoid using this term.
- **Definition**: A declarative dialog is part or all of an adaptive dialog, represented as _declarative assets_ in a file.
- **Notes**: Composer primarily saves and loads bots using declarative assets, and uses a common bot, _the runtime_ or _common core bot_, to consume and _run_ these assets. As such, they define the logic the bot will use and the common bot contains the code for stepping through that logic.
- **See also**: [.dialog file](#dialog-file).

## dialog

- **Status**: Informal, part of the Bot Framework SDK.
- **Definition**: "Dialogs are a central concept in the SDK, providing ways to manage a long-running conversation with the user. A dialog performs a task that can represent part of or a complete conversational thread. It can span just one turn or many, and can span a short or long period of time."
- **Notes**: There are many types of dialogs. If you need to call out a specific type of dialog, use the same conventions as the **Dialogs library** article.
- **See**: [Dialogs library](https://learn.microsoft.com/azure/bot-service/bot-builder-concept-dialog) in the SDK docs.
- **See also**: [dialogs library](#dialogs-library).

## dialogs library

- **Status**: Informal, part of the Bot Framework SDK.
- **Definition**: The code libraries that support dialogs. For example, these NuGet packages for the Bot Framework SDK for .NET: `Microsoft.Bot.Builder.Dialogs`, `Microsoft.Bot.Builder.Dialogs.Declarative`, `Microsoft.Bot.Builder.Dialogs.Adaptive`, and `Microsoft.Bot.Builder.Dialogs.Adaptive.Teams`.
- **See**: [Dialogs library](https://learn.microsoft.com/azure/bot-service/bot-builder-concept-dialog) in the SDK docs.
- **See also**: [adaptive dialog](#adaptive-dialog), [dialog](#dialog).

<a id="direct-line"></a>

## Direct Line API

- **Status**: Approved.
- **Forms**:
  - the Direct Line API
  - Direct Line
- **Definition**: A REST API that allows you to enable communication between your bot and your own client application.
- **Notes**: Don't use DL as an acronym.
- **See also**: [Web Chat](#web-chat).

## Direct Line App Service extension

- **Status**: Internal, but approved.
- **Forms**:
  - the Direct Line App Service extension
  - the App Service extension
  - the extension
- **Definition**: An extension to the Bot Framework SDK that allows clients to connect directly with the host, where the bot is located. It runs inside the same subscription, App Service, and Azure network as your bot and provides network isolation and, in some cases, improved performance.
- **Notes**: Don't use ASE as an acronym.
- **See also**: [App Service Environment](#app-service-environment).

## Direct Line channel

## Direct Line Speech

- **Status**: Internal, but approved.
- **Forms**:
  - Direct Line Speech
- **Definition**:
  - "A robust, end-to-end solution for creating a flexible, extensible voice assistant. It is powered by the Bot Framework and its Direct Line Speech channel, that is optimized for voice-in, voice-out interaction with bots."
  - Direct Line Speech is a collection of services and protocols for supporting speech-enabled bots.
- **Notes**: Provides integration with the [Speech SDK](https://learn.microsoft.com/azure/cognitive-services/speech-service/speech-sdk).
- **See**: [What is Direct Line Speech?](https://learn.microsoft.com/azure/cognitive-services/speech-service/direct-line-speech).
- **See also**: [Direct Line](#direct-line), [WebSockets](#websocket).

<a id="e"></a>

<a id="event"></a>
<a id="event-activity"></a>

## European Union data boundary

- **Notes**: A GDPR related effort to make sure customer data stays in the European Union region.

## EUDB

Don't use. See [European Union data boundary](#european-union-data-boundary).

## event

- **Status**: Complicated, see notes.
- **Definition**: The occurrence of an action in a system. Whenever you have a system that can react to some (internal or external) stimulus, the system can be described as recognizing certain events (stimuli) for which the developer can register or define handlers (that define the way in which the system will react).
- **Notes**: Any object that might represent some kind of event is often called an event, so always clarify 1) what category of event you're talking about, and 2) as applicable, what type of event within that category. For this same reason, don't casually call something an event that hasn't been defined specifically as one.
- **Examples**: None of the category names are proper nouns, so none of them would be capitalized unless they appear at the start of a sentence. For sample events, most of these are descriptive. Sample events that use the verbatim name of the event are in italic.
  | Category | Description | Sample event types in this category |
  |:-|:-|:-|
  | activity | An `event` activity is a specific type of [activity](#activity). | The meaning of the event activity is defined by the channel and the activity's `name` property. |
  | activity handler | Each type (and sometimes each subtype) of activity received by the bot is thought of as the event. | Message activity, conversation update activity, members added, event activity, token-response event activity, and so on. |
  | activity handler, Teams | The Teams activity handler has a finer-grained approach to `event` and `invoke` activities, but otherwise is similar to the base activity handler. | Channel created, _fileConsent/invoke_, and so on. |
  | dialogs library |  |  |
  | dialogs, adaptive | Anything that can satisfy a condition that will cause a trigger to fire. These are broken down further into subcategories. The types are implicit on the SDK side and explicit, if a bit fanciful, on the Composer side. Similar in some ways to activity handler events, but much more elaborate. | Intent recognized, dialog started, event activity received, message received, custom event, and so on. |
  | skills |  |  |
  | telemetry | Some aspect of the telemetry feature classifies certain activities within a bot into events that it can log. | Activity, bot errors, waterfall start, bot message received, bot message send, and so on. |
  | turn context | Your bot and middleware can register event handlers on the turn context to allow them to react when an activity is sent from the bot. | Send activity, update activity, and delete activity. |
- **See also**: [activity](#activity), [adaptive dialog](#adaptive-dialog), [dialog](#dialog).

<a id="f"></a>
<a id="g"></a>
<a id="h"></a>

## handoff to human

- **Status**: Internal, but approved.
- **Description**: Describes both an instance of a bot handing off a conversation to a human agent and the design pattern that allows the bot to do so.
- **Notes**: The `handoff` activity is deprecated and is different than the `event` activity with a `name` of `"handoff.initiate"` or `"handoff.status"`.
- **See**:
  - Activity schema: [Handoff activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#handoff-activity)
  - SDK docs: [Handoff to human pattern](https://learn.microsoft.com/azure/bot-service/bot-service-design-pattern-handoff-human)
- **See also**: [event](#event).

<a id="i"></a>

## internet

See [web](#web).

<a id="j"></a>
<a id="k"></a>
<a id="l"></a>

## language understanding

- **Status**: In general use.
- **Definition**: An aspect of natural language processing (NLP).
  - via Wikipedia,
    > Converts chunks of text into more formal representations such as first-order logic structures that are easier for computer programs to manipulate. Natural language understanding involves the identification of the intended semantic from the multiple possible semantics which can be derived from a natural language expression which usually takes the form of organized notations of natural language concepts."
- **Notes**: The casing is different for the product versus the general concept.
- **See also**: [.lu file](#lu-file), [Language Understanding (LUIS)](#language-understanding-luis).

## Language Understanding (LUIS)

- **Status**: Approved. See [Language Understanding (LUIS)](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=48260) in the Cloud style guide.
- **Definition**:
  - LUIS portal: "An AI service that allows users to interact with your applications, bots, and IoT devices by using natural language."
  - Term Studio: "The Azure AI services Language API service that applies custom machine-learning intelligence to a user's conversational, natural language text to predict overall meaning, and pull out relevant, detailed information."
- **Forms**:
  - Language Understanding (LUIS)&mdash;on fist use.
  - LUIS&mdash;on subsequent mentions.
- **Notes**:
  - The casing is different for the product versus the general concept.
  - "Don't use Azure LUIS or Microsoft LUIS. LUIS is part of Azure AI services but it isn't Microsoft or Azure branded."
- **See also**: [language understanding](#language-understanding).

## library

- **Status**: via American Heritage Dictionary
- **Definition**: A collection of standard routines used in computer programs, usually stored as an executable file.
- **Notes**: Usually, a package will include one or more libraries. (You install a package so you can access one of the contained libraries.)
- **See also**: [module](#module), [package](#package), [SDK](#sdk).

<a id="m"></a>

## Markdown

- **Status**: Approved. See [Markdown](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=45050) in the Cloud style guide.
- **Definition**: Both a lightweight markup language and a tool for converting Markdown text to HTML and other formats.
- **Notes**: The Bot Framework protocol supports Markdown, plain-text, and XML formats.
- **See also**: [markup language](#markup-language).

## markup language

- **Status**: In general use.
- **Short form**: markup.
- **Definition**: The collection of tags that describe the specifications of an electronic document, as for formatting, such as in HTML, XML, and Markdown.
- **See also**: [Markdown](#markdown).

<a id="message-activity"></a>

## message

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: Shorthand for a `message` activity, an activity with a _type_ property value of `message`, either incoming or outgoing.
- **See also**: [activity](#activity), [event](#event).

## Microsoft account

- **Status**: See [Microsoft account](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=27994) in the Cloud style guide.
- **Forms**:
  - a Microsoft account
  - your Microsoft account
  - your account
- **Definition**: "Your account for all things Microsoft. It enables every customer to get the most out of their Microsoft experience."
- **Notes**:
  - Do not shorten to MSA.
  - In contrast to a _work or school account_ (via Azure Active Directory).
- **See also**: [Azure Active Directory](#azure-active-directory).

<a id="microsoft-bot-framework"></a>

## [Microsoft] Bot Framework

- **Status**: Approved.
- **Forms**:
  - Microsoft Bot Framework
  - Bot Framework
  - the framework
- **Definition**: "The comprehensive offering to build and deploy high quality bots for users to enjoy wherever they are talking. Users can start conversations with your bot from any channel that you've configured your bot to work on, such as SMS, Skype, Slack, Facebook, and other popular services."
- **Notes**:
  - Do not shorten to BF.
  - In general, precede this term with "the" or "a" when using it as an adjective, as in _the Bot Framework protocol_.
- **See also**: [the](#the).

<a id="microsoft-bot-framework-adaptive-tools"></a>
<a id="bot-framework-adaptive-tools"></a>
<a id="adaptive-tools"></a>
<a id="microsoft-bot-framework-cli-tool"></a>
<a id="bot-framework-cli-tool"></a>
<a id="microsoft-bot-framework-cli"></a>
<a id="bot-framework-cli"></a>
<a id="microsoft-bot-framework-composer"></a>
<a id="bot-framework-composer"></a>
<a id="bot-framework-composer"></a>
<a id="microsoft-bot-framework-emulator"></a>
<a id="bot-framework-emulator"></a>
<a id="microsoft-bot-framework-protocol"></a>
<a id="bot-framework-protocol"></a>
<a id="microsoft-bot-framework-sdk"></a>
<a id="bot-framework-sdk"></a>
<a id="microsoft-bot-framework-connector-service"></a>
<a id="microsoft-bot-framework-connector"></a>
<a id="bot-framework-connector-service"></a>
<a id="bot-framework-connector"></a>
<a id="connector-service"></a>
<a id="connector"></a>

Tools, products, services, and so on that are all part of the Microsoft Bot Framework:

|Name|Short forms|Description|
|:-|:-|:-|
|Bot Framework Adaptive Tools| Adaptive Tools| A VS Code extension for editing and debugging assets (such as .lg, .lu, .qna, and .dialog files) for bots that use adaptive dialogs. Can also call this _the Bot Framework Adaptive Tools VS Code extension_. |
|the Bot Framework CLI tool| the Bot Framework CLI, the CLI| A CLI (command-line interface) for managing Bot Framework bots and related services.|
|the Bot Framework Composer| Composer| The open-source visual authoring tool that is used to build bots.|
|the Bot Framework Emulator| the Emulator| The application that allows bot developers to test and debug their bots locally or run them remotely through a tunnel.<br/> Do not use _Bot Framework Channel Emulator_ or _the emulator_.|
|the Bot Framework protocol| the protocol| A set of data-transfer protocols and schemas used by the Azure AI Bot Service for exchanging information between a bot and channel.|
|the Bot Framework SDK| the SDK|&bullet; "...a modular and extensible SDK for building bots..."<br/>&bullet; "The Bot Framework SDK allows you to build bots that can be hosted on the Azure AI Bot Service. The service defines a REST API and an activity protocol for how your bot and channels or users can interact. The SDK builds upon this REST API and provides an abstraction of the service so that you can focus on the conversational logic."<br/>**Note**: The SDK builds upon the Bot Framework Connector service. If relevant, qualify which language version of the SDK you're talking about.|
|the Bot Framework Connector service|the Connector service, the service| The communication service that helps you connect your bot with many different communication channels, such as SMS, email, and Skype.<br/>**Note**: The Connector service defines a REST API.|

For individual flavors of the SDK, append _for \<environment>_, to match the [Azure SDK](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=27903).

- Bot Framework SDK for .NET
- Bot Framework SDK for Java
- Bot Framework SDK for Node.js
- Bot Framework SDK for Python

## module

- **Status**: Specific to JavaScript/TypeScript.
- **Definition**: "Modules as a concept in JavaScript have a long and complicated history that makes any single definition or description difficult." From [TypeScript docs / A brief history of modules](https://www.typescriptlang.org/docs/handbook/2/modules.html#a-brief-history-of-modules).
- **Notes**: The Bot Framework JavaScript SDK uses the Node.js (CommonJS) version of modules.
- **See also**: [library](#library), [package](#package).

<a id="n"></a>
<a id="o"></a>

<a id="outlook-actionable-message"></a>
<a id="outlook-actionable-messages"></a>

## [Outlook] Actionable Messages

- **Status**: Approved, but not in the style guide yet.
- **Definition**: The feature that allows users to create and interact with actionable message cards.
- **Notes**: Always qualify on first use, even when talking about an instance of such a message.
- **Forms**:  
  |Form|Notes|
  |:--|:--|
  | Actionable Messages | The Outlook feature. |
  | actionable messages | Instances of the feature, as in a collection of such messages. |
  | actionable message | An instance of the feature. |
  | the Outlook Actionable Message channel | Azure connector support for such messages as a channel. Proper form still TBD. |
  | Outlook Actionable Message cards | An Adaptive Card format that contains Outlook-specific support for Actionable Messages. |
  | Actionable Message cards | ditto. |
  | actionable message card | An instance of an Adaptive Card that includes Outlook-specific Adaptive Card properties or features. |
- **See**:
  - [Actionable messages in Outlook and Office 365 Groups](https://learn.microsoft.com/outlook/actionable-messages/)
  - [Designing Outlook Actionable Message cards with the Adaptive Card format](https://learn.microsoft.com/outlook/actionable-messages/adaptive-card)
  - [Adaptive Cards for Outlook Actionable Message Developers](https://learn.microsoft.com/adaptive-cards/getting-started/outlook)
  - Adaptive Cards: [Universal Action Model](https://learn.microsoft.com/adaptive-cards/authoring-cards/universal-action-model)
  - [Actionable Message Designer](https://amdesigner.azurewebsites.net/)
  - Adaptive Cards Schema Explorer: [Action.Execute](https://adaptivecards.io/explorer/Action.Execute.html)

<a id="p"></a>

## package

- **Status**: In general use.
- **Definition**: A group of classes or interfaces that is distributed and consumed as a unit.
- **Notes**: Always qualify what type of package you're talking about whenever it might be ambiguous, such as on first use.
  Usually, a package will include one or more libraries. You install a package so you can access one of the contained libraries.
  A package is often published to and installed from a package repository.
- **Examples**:
  | Package type/repository | Language | Example |
  |:-|:-|:-|
  | Maven | Java | _TBD_ |
  | npm | JavaScript or TypeScript | To use dialogs, install the **botbuilder-dialogs** npm package. |
  | NuGet | C# | To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package. |
  | PyPI | Python | To use dialogs, install the **botbuilder-dialogs** and **botbuilder-ai** PyPI packages. |
- **See also**: [library](#library), [module](#module), [repository](#repository), [SDK](#sdk).

## page

- **Status**: Approved. See [page](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=35546) in the Microsoft style guide.
- **Notes**: Use page or webpage to describe one of a collection of web documents that make up a website. Use page to refer to the page the reader is on or to a specific page, such as the home page or start page.
- **See also**: [site](#site), [web](#web).

<a id="microsoft-power-virtual-agents"></a>
<a id="power-virtual-agents"></a>

## [Microsoft] Power Virtual Agents

- **Status**: Approved. See [Power Virtual Agents](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=45592) in the Cloud style guide.
- **Forms**:
  - Microsoft Power Virtual Agents
  - Power Virtual Agents
- **Definition**: An app that "lets you create powerful chatbots that can answer questions posed by your customers, other employees, or visitors to your website or service."
- **Notes**:
  - Do not shorten to PVA.
  - Power Virtual Agents is singular. Don't treat it as plural in a sentence.
- **See**: [Power Virtual Agents overview](https://learn.microsoft.com/power-virtual-agents/fundamentals-what-is-power-virtual-agents).

<a id="q"></a>

<a id="r"></a>

## README

- **Status**: In general use. See the [README](https://docs.github.com/en/github/getting-started-with-github/github-glossary#readme) entry in the GitHub glossary.
- **Definition**: A text file containing information about the files in a repository that is typically the first file a visitor to your repository will see. A `README` file, along with a repository license, contribution guidelines, and a code of conduct, helps you share expectations and manage contributions to your project.
- **Notes**:
  - To refer to content of a `README`, you can say, "See the `README` for the X repository..." or, "See the Bot Framework SDK `README`."
  - To refer to the file as a file, you can say something like "Add a `README.md` file to your project", and then format `README.md` as you would any other file name or path.

## repository

- **Status**: In general use, though its meaning varies.
- **Definition**: A central location (to varying extents) that contains code or packages. This can be at anywhere from the project level or platform level.
- **Notes**: Don't shorten to _repo_.
- **See also**: [package](#package).

## response generation template

- **Status**: Informal, part of the Bot Framework SDK and Composer, mostly visible via the Composer UI.
- **See also**: [.lg file](#lg-file).

## REST

- **Status**: In general use.
- **Definition**: The architectural style that defines standards to be used for creating web services, providing interoperability between computer systems on the Internet. Used to describe the general architecture or as an adjective.
- **Notes**:
  - Don't spell out _representational state transfer_.
  - The Connector service and the Direct Line service both define a REST API.

## REST API

- **Status**: In general use.
- **Definition**: A set of instructions that uses the methods of the HTTP protocol (GET, POST, PUT, DELETE) for distributed hypertext systems to perform operations using URIs as parameters. Used to describe the interface for a specific REST service.
- **Notes**: The Connector service and the Direct Line service both define a REST API.
- **See also**: [Direct Line API](#direct-line-api), [Microsoft Bot Framework Connector](#microsoft-bot-framework-connector).

<a id="s"></a>

## SDK

- **Status**: Approved.
  See [SDKs](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=43846) in the Cloud style guide.
- **Definition**: A set of routines (usually in one or more libraries) designed to allow developers to more easily write programs for a given computer, operating system, or user interface.
- **Notes**:
  - Don't spell out _software development kit_.
  - For the Bot Framework, we use the artificial distinction of API for the REST services (Connector, Direct Line) and SDK for the Bot Framework SDK (built on top of the Connector REST API).
  - When ambiguous, qualify which SDK you're talking about. This might also include qualifying the release version or programming language.
- **Examples**:
- **See also**: [API](#api), [library](#library), [Microsoft Bot Framework SDK](#bot-framework-sdk).

## site

- **Status**: Approved. See [site](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=36058) in the Microsoft writing style guide.
- **Notes**: Use to describe a collection of webpages that's part of a larger whole, such as the Microsoft website or the MSDN website. Use website instead of site if necessary for clarity.
- **See also**: [page](#page), [web](#web).

## skill

TBD

## skill consumer

TBD

## skill manifest

TBD

<a id="t"></a>

<a id="microsoft-teams"></a>
<a id="teams"></a>

## [Microsoft] Teams

- **Status**: Approved. See [Microsoft Teams](https://styleguides.azurewebsites.net/Styleguide/Read?id=2869&topicid=48152) in the Microsoft 365 style guide.
- **Forms**:
  - Microsoft Teams
  - Teams
- **Definition**: "An app that creates a shared workspace that includes chat, meetings, calling, video conferencing, file sharing and storage, and collaboration."
- **Notes**: In documentation, use the full name in the first reference within the page or section, and then it's OK to shorten to just _Teams_ in subsequent mentions within that same page or section.

## Teams activity handler

- **Status**: Informal, part of the Bot Framework SDK.
- **Definition**: An event-driven way to organize the conversational logic for a bot designed to work with Microsoft Teams.
- **See**: [How Microsoft Teams bots work](https://learn.microsoft.com/azure/bot-service/bot-builder-basics-teams) in the SDK docs.
- **See also**: [activity](#activity), [activity handler](#activity-handler), [event](#event), [Microsoft Teams](#teams).

## the

- Whether to use "the" or not with a product name is tricky, and there are as many exceptions as rules of thumb. See the glossary entry for each product name.
- Often, when a name is used as an adjective to modify another word, precede the phrase with "the", unless the whole phrase is itself a proper name.
- **Examples**: [the Emulator](#emulator), Composer (the Bot Framework Composer), Teams (Microsoft Teams).

<a id="u"></a>
<a id="v"></a>

## virtual agent

In general, do not use this term. Instead, use [bot](#bot). See [AI and bot terms](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=42480) in the Writing style guide.

## Virtual Assistant

- **Status**: Unsure, part of Bot Framework Solutions.
- **Definition**: "A project template with the best practices for developing a bot on the Microsoft Azure platform."
- **Notes**:
  - Do not shorten to VA.
  - Whenever possible, do not document Virtual Assistant features, but instead send readers to a page on the [Bot Framework Solutions](https://microsoft.github.io/botframework-solutions/index) site.
  - Bot Framework Solutions has its own concept of a skill that is related to the Bot Framework SDK's concept of a skill.
- **See**: [What is a Virtual Assistant?](https://microsoft.github.io/botframework-solutions/overview/virtual-assistant-solution/) in the Bot Framework Solution documentation.
- **See also**: [skill](#skill).

<a id="w"></a>

## web

- **Status**: Approved. See [web, World Wide Web, WWW](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=36431) in the Microsoft writing style guide.
- **Definition**: When referencing the web or the internet, think of _web_ as information-sharing model and _internet_ as network infrastructure.
- **Notes**: Lowercase _web_ as a modifier except to match UI, in feature names that include _web_, and to comply with your group's editorial style guide.
- **See**: [URLs and web addresses](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=34905) in the writing style guide for instructions on how to refer to URLs and addresses.
- **See also**: [internet](#internet), [page](#page), [site](#site).

## Web Chat

- **Status**: Approved, part of the Bot Framework ecosystem.
- **Forms**:
  - Web Chat
  - the Web Chat channel
  - the Web Chat control
- **Definition**: An embeddable web chat control for the Microsoft Bot Framework using the Direct Line API.
- **Notes**: You may need to qualify this term, as there is a Web Chat control (to embed in a web page) and a Web Chat channel (in Azure).
- **See also**: [Direct Line API](#direct-line-api), [Direct Line Speech](#direct-line-speech).

<a id="websocket"></a>
<a id="websockets"></a>

## WebSocket(s)

- **Status**: Not in the style guide, but an industry standard. The following is my best guess at the moment.
- **Forms**:
  - the WebSocket protocol &mdash; the protocol, as defined in an IEFT standard.
  - the WebSocket API &mdash; a W3C "living standard" for the _WebSocket interface_.
  - the WebSocket interface &mdash; use _the WebSocket API_, instead.
  - a WebSocket connection &mdash; the negotiated connection between the client and host/server over which data is sent.
  - a WebSocket client &mdash; a browser or web app that requests the connection from the server.
  - a WebSocket server &mdash; a web service with an endpoint for WebSocket connections.
  - a WebSocket endpoint &mdash; an endpoint for a web service for WebSocket connections.
  - a WebSocket &mdash; Generally refers to a WebSocket connection or endpoint, depending on context; qualify as necessary.
  - WebSockets &mdash; the supporting technology, protocol or API, and so on.
- **Definition**:
  - From the IEFT standard, "The WebSocket protocol enables two-way communication between a client running untrusted code in a controlled environment to a remote host that has opted-in to communications from that code."
  - From MDN, "The WebSocket API is an advanced technology that makes it possible to open a two-way interactive communication session between the user's browser and a server."
- **See**:
  - IETF [RFC 6455: The WebSocket protocol](https://tools.ietf.org/html/rfc6455)
  - MDN [The WebSocket API (WebSockets)](https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API)
- **See also**: [Direct Line Speech](#direct-line-speech).

<a id="x"></a>
<a id="y"></a>
<a id="z"></a>

<hr/>

[#](#symbol) &MediumSpace;
[A](#a) &MediumSpace; [B](#b) &MediumSpace; [C](#c) &MediumSpace; [D](#d) &MediumSpace; [E](#e) &MediumSpace;
[F](#f) &MediumSpace; [G](#g) &MediumSpace; [H](#h) &MediumSpace; [I](#i) &MediumSpace; [J](#j) &MediumSpace;
[K](#k) &MediumSpace; [L](#l) &MediumSpace; [M](#m) &MediumSpace; [N](#n) &MediumSpace; [O](#o) &MediumSpace;
[P](#p) &MediumSpace; [Q](#q) &MediumSpace; [R](#r) &MediumSpace; [S](#s) &MediumSpace; [T](#t) &MediumSpace;
[U](#u) &MediumSpace; [V](#v) &MediumSpace; [W](#w) &MediumSpace; [X](#x) &MediumSpace; [Y](#y) &MediumSpace;
[Z](#z)

<hr/>
