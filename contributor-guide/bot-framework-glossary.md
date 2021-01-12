# Bot Framework glossary

Contains a consolidated list of collected terms for the Bot Framework SDK docs, and by extension, the Composer docs. This is not meant to be the definitive source of truth, but notes taken as we hunt down the official terms to use. Meant to be a living document, as definitions and official terms will change over time.

To try to keep this from exploding, the intent to to capture terms that either are not easy to otherwise look up or we are constantly looking up but are not likely to change frequently.

<hr/>

[A](#a) | [B](#b) | [C](#c) | [D](#d) | [E](#e) | [F](#f) | [G](#g) | [H](#h) | [I](#i) | [J](#j) | [K](#k) | [L](#l) | [M](#m) | [N](#n) | [O](#o) | [P](#p) | [Q](#q) | [R](#r) | [S](#s) | [T](#t) | [U](#u) | [V](#v) | [W](#w) | [X](#x) | [Y](#y) | [Z](#z)

<hr/>

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
  - **See also**: Links to related terms.

### Context

For many terms that start with the word Microsoft or Azure, like Microsoft Azure App Service Environment

<a id="a"></a>

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

## bot service

- **Status**: Approved, in Term Studio.
- **Definition**: An instance created by the user using Azure Bot Service.
- **See also**: [Azure Bot Service](#azure-bot-service).

<a id="c"></a>
<a id="d"></a>

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
<a id="f"></a>
<a id="g"></a>
<a id="h"></a>
<a id="i"></a>
<a id="j"></a>
<a id="k"></a>
<a id="l"></a>
<a id="m"></a>

## Microsoft Bot Framework

- **Status**: Approved, in Term Studio.
- **Short form**: Bot Framework, framework.
- **Definition**: The comprehensive offering to build and deploy high quality bots for users to enjoy wherever they are talking. Users can start conversations with your bot from any channel that you've configured your bot to work on, such as SMS, Skype, Slack, Facebook, and other popular services.
- **Notes**: Do not shorten to BF.

### Bot Framework CLI tool

- **Status**: tbd
- **Short form**: CLI.
- **Definition**: A CLI (command-line interface) for managing Bot Framework bots and related services.

### Bot Framework Composer

- **Status**: Approved, in Term Studio.
- **Short form**: Composer.
- **Definition**: The open-source visual authoring tool that is used to build bots.

### Bot Framework Emulator

- **Status**: Approved, in Term Studio.
- **Short form**: Emulator.
- **Definition**: The application that allows bot developers to test and debug their bots locally or run them remotely through a tunnel.
- **Notes**: Do not use "Bot Framework Channel Emulator".

### Bot Framework SDK

## Microsoft Bot Framework Connector

- **Status**: Approved, in Term Studio, potentially stale.
- **Definition**: The communication service that helps you connect your bot with many different communication channels, such as SMS, email, and Skype.

<a id="n"></a>
<a id="o"></a>
<a id="p"></a>
<a id="q"></a>
<a id="r"></a>
<a id="s"></a>
<a id="t"></a>
<a id="u"></a>
<a id="v"></a>
<a id="w"></a>

## Web Chat

- **Status**: Approved, in Term Studio.
- **Definition**: The embeddable web chat control for the Microsoft Bot Framework using the Direct Line API.
- **Notes**: You may need to qualify this term, as there is a Web Chat control (to embed in a web page) and a Web Chat channel (in Azure).

<a id="x"></a>
<a id="y"></a>
<a id="z"></a>
