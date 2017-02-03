---
layout: page
title: Releases
permalink: /en-us/csharp/builder/libraries/latest/
weight: 3560
parent1: Building your Bot Using Bot Builder for .NET
---

<span style="color:red">We need a holistic release notes/what's new story.</span>


Microsoft Bot Builder is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. It is easy to use and leverages C# to provide a natural way to write bots.

High Level Features:

* Powerful dialog system with dialogs that are isolated and composable.
* Built-in dialogs for simple things like Yes/No, strings, numbers, enumerations.
* Built-in dialogs that utilize powerful AI frameworks like LUIS.
* Bots are stateless which helps them scale.
* FormFlow for automatically generating a bot from a C# class for filling in the class and that supports help, navigation, clarification and confirmation.
* SDK source code is found on [http://github.com/Microsoft/botbuilder](http://github.com/Microsoft/botbuilder).

## Install
To install Microsoft.Bot.Builder, run the following command in the [Package Manager Console](http://docs.nuget.org/consume/package-manager-console)

    PM> Install-Package Microsoft.Bot.Builder

## Release Notes
The framework is still in preview mode so developers should expect breaking changes in future versions of the framework. A list of current issues can be found on our [GitHub Repository](https://github.com/Microsoft/BotBuilder/issues).

### [v3.3.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.3.1)

#### Changes

- General bug fixes
- Add intent-based dispatch dialog
- Add dialog task manager to enable multiple stacks per conversation
- Add credential provider to enable multi-bot authentication
- Add bot authenticator facility to enable non-attribute based bot authentication scenarios

***

### [v3.3](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.3)

#### Breaking changes
- Update [IScorable](https://github.com/Microsoft/BotBuilder/commit/36aea6c0fdfca8bebc4767c4ba1bf38b6aa14aa5#diff-8f76e9b3bfbb6254ab5173ddf2fe7252R50) interface. __Note__: This will only impact bots that leverage [ScoringDialogTask](https://github.com/Microsoft/BotBuilder/blob/10893730134135dd4af4250277de8e1b980f81c9/CSharp/Library/Dialogs/ScorableDialogs.cs#L49) and IScorable implementation to interrupt the conversation based on the score assigned to the incoming activity.

#### Changes

- General bug fixes
- Remove unnecessary dependencies from bot.builder nuspec
- Add support for keyboard card and Facebook quick replies
- Add scorable dispatch support

***

### [v3.2.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.2.1)

#### Changes

- General bug fixes
- Factor out `Address` from `ResumptionCookie`
- Serialize dialog execution pessimistically by conversation
- Add `Then dialog` to enhance chaining of dialogs

***

### [v3.2](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.2.0)

#### Breaking Change 

- IField<T>.FieldDescription is now a DescribeAttribute.
__Note:__ This is a breaking change if your bot is implementing its own Field<T> class.

#### Changes

- Improved card support in Form dialog
- Making Incoming activity available in LuisDialog intent handlers
- Add a mechanism to serialize incoming requests for a conversation 
- Add Luis resolution parser
- Update connector to depend on Microsoft.Rest.ClientRuntime 2.3.2
- General bug fixes

***

### [v3.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.1.0)

#### Breaking Change 

- BotAuthentication attribute is now inheriting from ActionFilterAttribute and not AuthorizationFilterAttribute

#### Changes

- Make [ETag](https://en.wikipedia.org/wiki/HTTP_ETag) consistency policy the default data consistency policy for IBotDataStore
- Add better exception translation/propagation to bot builder internals
- Make Bot authentication more reliable
- Add trusted service urls to [MicrosoftAppCredentials](https://github.com/Microsoft/BotBuilder/blob/master/CSharp/Library/Microsoft.Bot.Connector/MicrosoftAppCredentials.cs#L20)
- General bug fixes

***

### [v3.0.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.0.1)

#### Changes

- Fix issue with MicrosoftAppCredentials not setting AuthToken

***

### [v3.0.0](https://www.nuget.org/packages/Microsoft.Bot.Builder/3.0.0)

You can read more about v3 API and what have changed in more depth [here](http://docs.botframework.com/en-us/support/upgrade-code-to-v3/#navtitle). Here is a high-level list of changes in this release: 
#### Breaking Changes

- v3 schema: Message is now [Activity](http://docs.botframework.com/en-us/csharp/builder/sdkreference/activities.html) and there is a [new addressing scheme](http://docs.botframework.com/en-us/support/upgrade-code-to-v3/#addressing)
- [Change in reply model](http://docs.botframework.com/en-us/support/upgrade-code-to-v3/#sending-replies): replies to the user will be sent asynchronously over a separately initiated HTTP request rather than inline with the HTTP POST for the incoming message to bot
- [Authentication model](http://docs.botframework.com/en-us/restapi/authentication/#navtitle)
- Decoupled bot data storage ([Bot State](http://docs.botframework.com/en-us/csharp/builder/sdkreference/stateapi.html)) from messaging API
- [New card](http://docs.botframework.com/en-us/csharp/builder/sdkreference/attachments.html) format for attachments

#### Changes

- Bot Builder and Connector are now one [nuget](https://www.nuget.org/packages/Microsoft.Bot.Builder/)
- Bot Connector is now open source
- [Bot.Builder.Calling](https://www.nuget.org/packages/Microsoft.Bot.Builder.calling) nuget package to build [Skype Calling bots](http://docs.botframework.com/en-us/skype/calling/#navtitle)

***

### [v1.2.5](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.5)

#### Changes
* Moving Microsoft.Bot.Builder.FormFlow.Json into a separate nuget package
* Add PromptDialog Attachment
* General bug fixes
* Added nuget symbols to nuget.org

***

### [v1.2.4](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.4)

#### Breaking Changes

* Renamed IFormBuilder.OnCompletionAsync to OnCompletion since the method itself is not async.
* JSON FormFlow has changed substantially. (See New Features)
* Missing resources cuases an error to be thrown.

#### New Features


* JSON FormFlow now allows completely specifying everything through the JSON Schema file:

    1. There is a completely seperate builder, FormBuilderJson which takes the schema.
    2. JSON Schema now supports message, confirmation, validation with regex and dynamically compiled code.
    3. You no longer need to specify the schema on every field.
    4. It is packaged as a seperate dll Microsoft.Bot.Builder.FormFlow.Json which depends on Microsoft.CodeAnalysis.CSharp.Scripting which brings in lots of dependencies.

* LuisDialog supports multiple LuisModel and ILuisService instances.
* New attribute PatternAttribute for field validation via regex.
* IBotDataStore now allows supplying your own storage implementation.
* We continue to update the documentation. :-)

#### Bug Fixes

* [#414](https://github.com/Microsoft/BotBuilder/issues/414) if an entity did not match a value, a blank string would be returned.
* [#434](https://github.com/Microsoft/BotBuilder/issues/434) add calculator scorable/dialog as an example of IDialogStack.Forward from IScorable.PostAsync
* [#465](https://github.com/Microsoft/BotBuilder/issues/465) preserve exception stack trace when re-throwing
* [#466](https://github.com/Microsoft/BotBuilder/issues/466) add a typed TooManyAttemptsException for prompts
* [#472](https://github.com/Microsoft/BotBuilder/issues/472) test showing how to resolve dynamic form from container
* [#416](https://github.com/Microsoft/BotBuilder/issues/416) Fix bug in enumeration where if a term included numbers it would result in no match.
* [#440](https://github.com/Microsoft/BotBuilder/issues/440) Fix syntax error in Japanese localization.
* [#446](https://github.com/Microsoft/BotBuilder/issues/446) by exposing a Confirm method mistakenly marked internal.
* [#449](https://github.com/Microsoft/BotBuilder/issues/449) InitialUpper for Normalize did not return value.
* Make it so validation feedback is surfaced if FeedbackOptions.Always.
* Buttons did not include no preference.
* JSON Forms were not handling optional correctly.

***


### [v1.2.3](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.3)

#### Changes

* FormFlow now handles clarification and verification of LUIS entities.  Initial messages are processed, then LUIS entities and then remaining steps.   Bug [#227](https://github.com/Microsoft/BotBuilder/issues/227)
* Made it so n-gram generation would not include empty strings if there were multiple spaces. Bug [#355](https://github.com/Microsoft/BotBuilder/issues/335)
* Update FormFlow recognizers to take consistent set of args
* Add CancelScorable as example of IScorable
* Add sample using Azure Active Directory Authentication to access Microsoft graph
* Fix a bug causing issue [#400](https://github.com/Microsoft/BotBuilder/issues/400)

***

### [v1.2.2](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.2)

#### Breaking Changes
* There is no longer a dependency on Newtonsoft.Json.Schema so you use JObject.Parse to parse your JSON Schema and define forms.

#### Changes
* Fix bug from github issue #227.  The problem was that when processing LUIS entities if the validation failed the phase was left as responding which caused a crash.
* Allow ConnectorClientCredentials to be injected from container fixes GitHub issue #230
* Make it possible to resolve root dialog from the container
* Decouple IPostToBot "middleware" from IDialogTask
* Push lazy dialog instantiation into DialogTask
* Factor out ReactiveDialogTask from DialogTask
* Implement Forwarding of an item to child dialog
* Add persistent dialog task
* Add example of AlwaysSendDirect_BotToUser
* Add 1 & 2 as possible responses to boolean recognizer because buttons might send them.

* * *

### [v1.2.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.1)

#### Changes
* Ensure that LUIS service queries are encoded with UTF8
* Fixed a Choice prompt bug to rank complete matches higher than partial matches

* * *

### [v1.2.0.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.0.1)

#### Changes
* Fixed missing dependencies for Microsoft.Bot.Builder 1.2.0.0 nuget

* * *

### [v1.2.0](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.0)

#### Breaking Changes
* Target framework is now .Net 4.6.  This change was necessary to reliably support using the thread culture for localization.
* FormFlow ValidateAsyncDelegate now needs to return the value to set in the field.  This was in order to support programmatic value transformations.
* The signature of Conversation.Resume has changed in order to support a resumption cookie to maintain conversation state across dialog resumption.
* Moved to the latest Nuget packages including for the Bot Framework Connector.

#### New Features
* System dialogs and FormFlow will now generate buttons for channels that support them. 
* FormFlow can now be driven by an extended JSON Schema that allows doing attributes in a similar way to C#.  This allows forms to be generated at run-time from data rather than C# reflection.
* System dialogs and FormFlow are localized to nine languages.  (Contributions for more languages would be welcome.) We also provide tools to help generate the resource files required to localize your FormFlow state classes.
* DateTime parsing in English now uses Chronic which supports more natural Date/Time expressions like "tomorrow at 4".
* The LuisDialog now supports the full LUIS schema including actions.

#### Bugs
* Fixed lots of bugs as reported by developers--thanks!

* * *

### [v1.1.0](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.1.0)

* __Breaking Change:__ Rename some delegates and methods to be more consistent and to support dynamic field definition. Unless you were using Field or FieldReflector directly this should be transparent.
* Provide a way to dynamically define fields, confirmations and messages.
* Add FormCanceledException which provides information on what steps were completed and where the user quit.
* Add more flexibility on how parenthesis are used when generating prompts.
* Fix a number of bugs around initial state and LUIS entities.
* Extend chain model to support branching (Chain.Switch)
* Add support for resumption of a conversation
* Add Facebook OAuth Example

* * *

### [v1.0.2](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.0.2)

* Move to IDialog<T> typed for result type
* Add support for linq query syntax (e.g. Select, SelectMany)
* Multiple IBotToUser.Post(Message) calls
* Move to Autofac dependency injection container
* IConnectorClient instantiated to point to emulator when emulating bot
* Fix CommandDialog<T>
* Update LUIS Models
* Add ChoiceCase, ChoiceParens to Form template attributes

* * *

### [v1.0.1](https://www.nuget.org/packages/Microsoft.Bot.Builder/1.0.1)
* Fixed LuisDialog to handle null score returned by Luis. This is because of a behavior change in Cortana pre-built apps by [Luis](http://luis.ai)
* Updated nuspec with better description 
* Added error resilient context store
