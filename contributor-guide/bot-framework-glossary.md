# Bot Framework glossary

Contains a consolidated list of collected terms for the Bot Framework SDK docs, and by extension, the Composer docs. This is not meant to be the definitive source of truth, but notes taken as we hunt down the official terms to use. Meant to be a living document, as definitions and official terms will change over time.

To try to keep this from exploding, the intent to to capture terms that either are not easy to otherwise look up or we are constantly looking up but are not likely to change frequently.

### Resources

These should be considered the sources of truth, and you _should not_ "use Microsoft.com, any other web page, or product UI as a source for product and feature names, capitalization, spelling, and usage".

#### On Corpnet

- [Term Studio][] &mdash; In theory, the source of truth, at least for product names.
- [Microsoft Cloud Style Guide][] / A–Z names + terms dictionary &mdash; Cloud-specific style guide. Searches here should also pick up terms in the Microsoft Writing Style Guide.
- [Microsoft Writing Style Guide][] / A–Z word list and term collections &mdash; Microsoft-wide style guide.

[Term Studio]: https://termstudio.azurewebsites.net/default.aspx
[Microsoft Cloud Style Guide]: https://styleguides.azurewebsites.net/StyleGuide/Read?id=2696
[Microsoft Writing Style Guide]: https://styleguides.azurewebsites.net/StyleGuide/Read?id=2700

#### On the web

- [American Heritage Dictionary][] &mdash; Source of truth for [most] common (non-Microsoft-specific) terms.

[American Heritage Dictionary]: https://www.ahdictionary.com/

#### Other

When in doubt and we don't have guidance yet, check with an appropriate subject matter expert, and get a second opinion, if the first answer seems odd or otherwise dubious.

- The Connector API team for the Connector REST API, Direct Line, and the Azure Bot Service channel connectors.
- The Bot Framework SDK dev team for the SDK and the channel adaptors.
- The Bot Framework Composer team.
- The Bot Framework Emulator team.

### General entry format

- **Heading**: The complete term to use on first mention, including proper casing and official acronym (if applicable).
  - **Status**: Indicates whether the term is approved, in Term Studio, or just internal jargon.
  - **Short form**: If applicable, shorter forms of the term that can be used on subsequent mention in an article, as long as context is clear.
  - **Definition**: More or less official definition of the term.
  - **Notes**: Additional notes, if any.
  - **See**: Related pages outside the glossary.
  - **See also**: Links to related terms.

### Context

For many terms that start with the word Microsoft or Azure, like Microsoft Azure App Service Environment, the _Microsoft_ or _Azure_ bit can be left off if the context is obvious within the article itself. Remember that readers may land on any given page from a search.

<hr/>

[A](#a) &MediumSpace; [B](#b) &MediumSpace; [C](#c) &MediumSpace; [D](#d) &MediumSpace; [E](#e) &MediumSpace;
[F](#f) &MediumSpace; [G](#g) &MediumSpace; [H](#h) &MediumSpace; [I](#i) &MediumSpace; [J](#j) &MediumSpace;
[K](#k) &MediumSpace; [L](#l) &MediumSpace; [M](#m) &MediumSpace; [N](#n) &MediumSpace; [O](#o) &MediumSpace;
[P](#p) &MediumSpace; [Q](#q) &MediumSpace; [R](#r) &MediumSpace; [S](#s) &MediumSpace; [T](#t) &MediumSpace;
[U](#u) &MediumSpace; [V](#v) &MediumSpace; [W](#w) &MediumSpace; [X](#x) &MediumSpace; [Y](#y) &MediumSpace; [Z](#z)

<hr/>

<a id="a"></a>

## action

- **Status**: Complicated, see notes.
- **Notes**: Always clarify what type of action you are talking about.
- **See also**: [card action](#card-action), [skill](#skill).

## activity

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **See**: The [Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).

<a id="adaptive-card"></a>
<a id="adaptive-cards"></a>

## Adaptive Card(s)

- **Status**: Product/technology name, not in Term Studio.
- **Definition**:
  - "Adaptive Cards are an open card exchange format enabling developers to exchange UI content in a common and consistent way." 
  - "An Adaptive Card is a JSON-serialized card object model."
- **Notes**: _Adaptive Cards_ (plural) is the official name of the format. _Adative Card_ (singular) refers to a JSON object in that format or a rendering of such a card.
- **See**: [Adaptive Cards](https://docs.microsoft.com/en-us/adaptive-cards) docs.

## adaptive dialog

TBD

## API

- **Status**: Approved, in Term Studio.
- **Definition**: A set of routines that an application uses to request and carry out lower-level services performed by a computer's or device's operating system. These routines usually carry out maintenance tasks such as managing files and displaying information.
- **Notes**: For the Bot Framework, we use the artificial distinction of API for the REST services (Connector, Direct Line) and SDK for the Bot Framework SDK (built on top of the Connector REST API).
- **See also**: [SDK](#sdk).

## attachment

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: An object to be displayed or otherwise included as part of an activity.
- **See**: The [Attachements](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#attachments) section of the Activity schema.
- **See also**: [activity](#activity), [carousel](#carousel).

## Azure App Service Environment

- **Status**: Approved, in Term Studio.
- **Short form**: App Service Environment, environment.
- **Definition**: The Azure App Service feature that provides a fully isolated and dedicated environment for securely running App Service apps at high scale.
- **Notes**: Don't use ASE as an acronym.
- **See also**: [Direct Line App Service extension](#direct-line-app-service-extension).

## Azure Bot Service

- **Status**: Approved, in Term Studio.
- **Short form**: Bot Service.
- **Definition**: The service that accelerates the process of developing a bot. It provisions a web host with one of five bot templates that can be modified in an integrated environment.
- **See also**: [Microsoft Bot Framework](#microsoft-bot-framework), [bot service](#bot-service).

<a id="b"></a>

## Bot Framework

See [Microsoft Bot Framework](#microsoft-bot-framework).

## Bot Framework Connector

See [Microsoft Bot Framework Connector](#microsoft-bot-framework-connector).

## bot service

- **Status**: Approved, in Term Studio.
- **Definition**: An instance created by the user using Azure Bot Service.
- **See also**: [Azure Bot Service](#azure-bot-service).

<a id="c"></a>

## card

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: An interactive card for use within chat and other applications.
- **See**: The [Cards schema](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md).
- **See also**: [attachment](#attachment).

## card action

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: A clickable or interactive button for use within cards or as suggested actions; used to solicit input from users.
- **See**: The [Card action](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#card-action) section in the Activity schema.
- **See also**: [card](#card).

## carousel

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: A side-by-side, potentially scrollable, arrangement for attachments.
- **See**: The [Attachment layout](https://github.com/microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#attachment-layout) section in the Activity schema.

<a id="d"></a>

## dialog

TBD

## Direct Line API

- **Status**: Approved, in Term Studio.
- **Short form**: Direct Line.
- **Definition**: The API that allows you to enable communication between your bot and your own client application.

## Direct Line App Service extension

- **Status**: Approved, not in Term Studio.
- **Short form**: App Service extension, extension.
- **Definition**: Allows clients to connect directly with the host, where the bot is located. It runs inside the same subscription, App Service, and Azure network as your bot and provides network isolation and, in some cases, improved performance.
- **Notes**: Don't use ASE as an acronym.
- **See also**: [Azure App Service Environment](#azure-app-service-environment).

## Direct Line Speech

- **Status**: Approved, in Term Studio.
- **Definition**: The solution used to create a voice assistant with richer and more sophisticated capabilities.

<a id="e"></a>

<a id="event"></a>
<a id="event-specific"></a>

## event (specific type)

- **Status**: Complicated, see notes.
- **Definition**: The occurrence of an action in a system.
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
- **See also**: [activity](#activity).

<a id="event-abstract"></a>

## event (in the abstract)

Whenever you have a system that can react to some (internal or external) stimulus, the system can be described as recognizing certain events (stimuli) for which the developer can register or define handlers (that define the way in which the system will react).

<a id="f"></a>
<a id="g"></a>
<a id="h"></a>

## handoff to human

- **Status**: Unofficial.
- **Description**: Describes both an instance of a bot handing off a conversation to a human agent and the design pattern that allows the bot to do so.
- **Notes**: The `handoff` activity is deprecated and is different than the `event` activity with a `name` of `"handoff.initiate"` or `"handoff.status"`.
- **See**:
  - Activity schema: [Handoff activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#handoff-activity)
  - SDK docs: [Handoff to human pattern](https://docs.microsoft.com/azure/bot-service/bot-service-design-pattern-handoff-human)
- **See also**: [event](#event).

<a id="i"></a>
<a id="j"></a>
<a id="k"></a>
<a id="l"></a>

## library

- **Status**: via American Heritage Dictionary
- **Definition**: A collection of standard routines used in computer programs, usually stored as an executable file.
- **Notes**: Usually, a package will include one or more libraries.
  Usually, a package will include one or more libraries. (You install a package so you can access one of the contained libraries.)
- **See also**: [module](#module), [package](#package), [SDK](#sdk).

<a id="m"></a>

## Markdown

- **Status**: Approved, in Term Studio.
- **Definition**: A lightweight markup language with plain text formatting syntax designed to be converted to HTML and other formats.
- **Notes**: The Bot Framework protocol supports Markdown, plain-text, and XML formats.

## markup language

- **Status**: Approved, in Term Studio.
- **Short form**: markup.
- **Definition**: A set of codes in a text file that instruct a computer how to format it on a printer or video display or how to index and link its contents, such as HTML, XML, and Markdown.

## message

- **Status**: Informal, part of the Bot Framework protocol and SDK.
- **Definition**: Shorthand for a `message` activity, an activity with a _type_ property value of `message`, either incoming or outgoing.
- **See also**: [activity](#activity), [event](#event).

## Microsoft Bot Framework

- **Status**: Approved, in Term Studio.
- **Short form**: Bot Framework, framework.
- **Definition**: The comprehensive offering to build and deploy high quality bots for users to enjoy wherever they are talking. Users can start conversations with your bot from any channel that you've configured your bot to work on, such as SMS, Skype, Slack, Facebook, and other popular services.
- **Notes**: Do not shorten to BF.

<a id="microsoft-bot-framework-cli-tool"></a>
<a id="bot-framework-cli-tool"></a>

## [Microsoft] Bot Framework CLI tool

- **Status**: tbd
- **Short form**: CLI.
- **Definition**: A CLI (command-line interface) for managing Bot Framework bots and related services.

<a id="microsoft-bot-framework-composer"></a>
<a id="bot-framework-composer"></a>

## [Microsoft] Bot Framework Composer

- **Status**: Approved, in Term Studio.
- **Short form**: Composer.
- **Definition**: The open-source visual authoring tool that is used to build bots.

<a id="microsoft-bot-framework-emulator"></a>
<a id="bot-framework-emulator"></a>

## [Microsoft] Bot Framework Emulator

- **Status**: Approved, in Term Studio.
- **Short form**: Emulator.
- **Definition**: The application that allows bot developers to test and debug their bots locally or run them remotely through a tunnel.
- **Notes**: Do not use "Bot Framework Channel Emulator".

<a id="microsoft-bot-framework-protocol"></a>
<a id="bot-framework-protocol"></a>

## [Microsoft] Bot Framework protocol

- **Status**: Approved, not in Term Studio.
- **Short form**: protocol.
- **Definition**: A set of data-transfer protocols and schemas used by the Azure Bot Service for exchanging information between a bot and channel.
- **See also**: [Microsoft Bot Framework Connector](#microsoft-bot-framework-connector).

<a id="microsoft-bot-framework-sdk"></a>
<a id="bot-framework-sdk"></a>

## [Microsoft] Bot Framework SDK

- **Status**: Approved, not in Term Studio.
- **Short form**: SDK.
- **Definition**:
  - "...a modular and extensible SDK for building bots..."
  - "The Bot Framework SDK allows you to build bots that can be hosted on the Azure Bot Service. The service defines a REST API and an activity protocol for how your bot and channels or users can interact. The SDK builds upon this REST API and provides an abstraction of the service so that you can focus on the conversational logic."
- **Notes**: The SDK builds upon the Bot Framework Connector service. If relevant, qualify which language version of the SDK you're talking about.
- **See also**: [Microsoft Bot Framework Connector](#connector).

<a id="microsoft-bot-framework-connector-service"></a>
<a id="microsoft-bot-framework-connector"></a>
<a id="bot-framework-connector-service"></a>
<a id="bot-framework-connector"></a>
<a id="connector-service"></a>
<a id="connector"></a>

## [Microsoft] Bot Framework Connector service

- **Status**: Approved, in Term Studio, potentially stale.
- **Short form**: Connector service, Connector.
- **Definition**: The communication service that helps you connect your bot with many different communication channels, such as SMS, email, and Skype.
- **Notes**: The Connector service defines a REST API.
- **See also**: [Bot Framework protocol](#bot-framework-protocol), [Bot Framework SDK](#bot-framework-sdk).

## module

- **Status**: Specific to JavaScript/TypeScript.
- **Definition**: "Modules as a concept in JavaScript have a long and complicated history that makes any single definition or description difficult." From [TypeScript docs / A brief history of modules](https://www.typescriptlang.org/docs/handbook/2/modules.html#a-brief-history-of-modules).
- **Notes**: The Bot Framework JavaScript SDK uses the Node.js (CommonJS) version of modules.
- **See also**: [library](#library), [package](#package).

<a id="n"></a>
<a id="o"></a>
<a id="p"></a>

## package

- **Status**: Obviously stale in Term Studio.
- **Definition**: A group of classes or interfaces that is distributed and consumed as a unit.
- **Notes**: Always qualify what type of package you're talking about whenever it might be ambiguous, such as on first use.
  Usually, a package will include one or more libraries. You install a package so you can access one of the contained libraries.
  A package is published to and installed from a package repository.
- **See also**: [library](#library), [module](#module), [SDK](#sdk).
- **Examples**:
  | Package type/repository | Language | Example |
  |:-|:-|:-|
  | Maven | Java | _TBD_ |
  | npm | JavaScript or TypeScript | To use dialogs, install the **botbuilder-dialogs** npm package. |
  | NuGet | C# | To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package. |
  | PyPI | Python | To use dialogs, install the **botbuilder-dialogs** and **botbuilder-ai** PyPI packages. |

<a id="q"></a>
<a id="r"></a>

## README

- **Status**: Obviously stale in Term Studio.
- **Definition**: A text file containing information about the files in a repository that is typically the first file a visitor to your repository will see. A README file, along with a repository license, contribution guidelines, and a code of conduct, helps you share expectations and manage contributions to your project.
- **Notes**: Running with the convention from GitHub.

## REST

- **Status**: Approved, in Term Studio.
- **Definition**: The architectural style that defines standards to be used for creating web services, providing interoperability between computer systems on the Internet.
- **Notes**: The Connector and Direct Line services define a REST API.

## REST API

- **Status**: Approved, in Term Studio.
- **Definition**: A set of instructions that uses the methods of the HTTP protocol (GET, POST, PUT, DELETE) for distributed hypertext systems to perform operations using URIs as parameters.
- **See also**: [Direct Line API](#direct-line-api), [Microsoft Bot Framework Connector](#microsoft-bot-framework-connector).

<a id="s"></a>

## SDK

- **Status**: Approved, in Term Studio, probably stale.
- **Definition**: A set of routines (usually in one or more libraries) designed to allow developers to more easily write programs for a given computer, operating system, or user interface.
- **Notes**: When ambiguous, qualify which SDK you're talking about. This might also include qualifying the release version or programming language.
- **See also**: [API](#api), [library](#library), [Microsoft Bot Framework SDK](#bot-framework-sdk).

## skill

TBD

## skill consumer

TBD

## skill manifest

TBD

<a id="t"></a>
<a id="u"></a>
<a id="v"></a>
<a id="w"></a>

## Web Chat

- **Status**: Approved, in Term Studio.
- **Definition**: The embeddable web chat control for the Microsoft Bot Framework using the Direct Line API.
- **Notes**: You may need to qualify this term, as there is a Web Chat control (to embed in a web page) and a Web Chat channel (in Azure).
- **See also**: [Direct Line API](#direct-line-api).

<a id="x"></a>
<a id="y"></a>
<a id="z"></a>
