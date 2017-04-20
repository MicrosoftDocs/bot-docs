---
title: Release notes | Microsoft Docs
description: View the release notes and changelog for the Bot Builder SDK for .NET 
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/27/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Release notes

A record of changes introduced by each version of the Bot Builder SDK for .NET. 

> [!IMPORTANT]
> As long as the Microsoft Bot Framework is in Preview mode, 
> you should expect breaking changes in new versions of the Bot Builder SDK for .NET. 
> See the 
> <a href="https://github.com/Microsoft/BotBuilder/issues" target="_blank">Bot Builder SDK GitHub repository</a> 
> for a list of known issues.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.5.5" target="_blank">v3.5.5</a>

### Changes

- Added `ConversationReference` (as a replacement to deprecated `ResumptionCookie`).
- Changed default LUIS host in LUIS service and deprecated LUIS v1 endpoint.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.5.3" target="_blank">v3.5.3</a>

### Breaking changes

- Changed the FormFlow prompter so that the state and current field are passed in. This only impacts bots that use a custom prompter instead of the default prompter in FormFlow.

### Changes

- Improved exception propagation and messaging for bot authentication failures.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.5.2" target="_blank">v3.5.2</a>

### Changes

- Fixed some FormFlow issues and added markdown support for FormFlow multi-line prompts.
- Updated `ActivityResolver` to support `IInvokeActivity`.
- Added `AlteredQuery` to `LuisResult`.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.5.1" target="_blank">v3.5.1</a>

### Changes

- Deprecated `ITriggerActivity`. `IEventActivity` replaces `ITriggerActivity`.
- Added new activity types (`event` and `invoke`) to the Connector. 
- Prepared the release of .NET core support for Microsoft Bot Connector.
- Implemented general bug fixes and code refactoring.


## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.5.0" target="_blank">v3.5.0</a>

### Changes

- Updated to Bot Framework v3.1 `JwtToken`. For more information about v3.1 token changes, 
see [Authentication][authentication].

- Implemented general bug fixes and code refactoring.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.4.0" target="_blank">v3.4.0</a>

### Breaking changes

- Renames `ScorableOrder` attribute to `ScorableGroup` in `DispatchDialog`. This only impacts bots that are using `ScorableOrder` in their `DispatchDialog`.

### Changes

- Implemented improvements to `DispatchDialog`.
- Added support for <a href="http://luis.ai" target="_blank">LUIS</a> API v2 and LUIS action dialog.
- Automatically remember last wait for each frame of stack.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.3.3" target="_blank">v3.3.3</a>

### Breaking changes

- `ICredentialProvider` should now implement `IsAuthenticatedAsync`. This only impacts bots that are using `ICredentialProvider` for their `BotAuthentication`.

### Changes

- Added support for new ~/media card types (e.g. `AnimationCard`, `VideoCard`, and `AudioCard`).
- Added new `BotAuthenticator` utility class that can be used for bot authentication instead of using the `BotAuthentication` attribute.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.3.1" target="_blank">v3.3.1</a>

### Changes

- Added intent-based dispatch dialog.
- Added dialog task manager to enable multiple stacks per conversation.
- Added credential provider to enable multi-bot authentication.
- Added bot authenticator facility to enable non-attribute-based bot authentication scenarios.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.3.0" target="_blank">v3.3.0</a>

### Breaking changes
- Updated <a href="https://github.com/Microsoft/BotBuilder/commit/36aea6c0fdfca8bebc4767c4ba1bf38b6aa14aa5#diff-8f76e9b3bfbb6254ab5173ddf2fe7252R50" target="_blank">IScorable</a> interface. This only impacts bots that use <a href="https://github.com/Microsoft/BotBuilder/blob/10893730134135dd4af4250277de8e1b980f81c9/CSharp/Library/Dialogs/ScorableDialogs.cs#L49" target="_blank">ScoringDialogTask</a> and `IScorable` implementation to interrupt the conversation based on the score assigned to the incoming activity.

### Changes

- Removed unnecessary dependencies from `bot.builder` **nuspec**.
- Added support for keyboard card and Facebook quick replies.
- Added scorable dispatch support.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.2.1" target="_blank">v3.2.1</a>

### Changes

- Factored out `Address` from `ResumptionCookie`.
- Serialized dialog execution pessimistically by conversation.
- Added `Then dialog` to enhance chaining of dialogs.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.2.0" target="_blank">v3.2.0</a>

### Breaking changes 

- `IField<T>.FieldDescription` is now a `DescribeAttribute`. This only impacts bots that implement their own `Field<T>` class.

### Changes

- Improved card support in Form dialog.
- Made Incoming activity available in `LuisDialog` intent handlers.
- Added a mechanism to serialize incoming requests for a conversation. 
- Added LUIS resolution parser.
- Updated the Connector to depend on `Microsoft.Rest.ClientRuntime` v2.3.2.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.1.0" target="_blank">v3.1.0</a>

### Breaking changes 

- `BotAuthentication` attribute now inherits from `ActionFilterAttribute` and not `AuthorizationFilterAttribute`.

### Changes

- Made <a href="https://en.wikipedia.org/wiki/HTTP_ETag" target="_blank">ETag</a> consistency policy the default data consistency policy for `IBotDataStore`.
- Added better exception translation/propagation to bot builder internals.
- Made Bot authentication more reliable.
- Added trusted service urls to <a href="https://github.com/Microsoft/BotBuilder/blob/master/CSharp/Library/Microsoft.Bot.Connector.Shared/MicrosoftAppCredentials.cs" target="_blank">MicrosoftAppCredentials</a>.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.0.1" target="_blank">v3.0.1</a>

### Changes

- Fixed an issue with `MicrosoftAppCredentials` not setting `AuthToken`.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/3.0.0" target="_blank">v3.0.0</a>

> [!NOTE]
> This version of the SDK reflects changes that were implemented in [version 3 of the Bot Connector API][connectorAPIv3]. 

### Breaking changes

- Updated schema to v3. `Message` is now [Activity](~/dotnet/activities.md) and there is a [new addressing scheme][addressing].
- [Changed reply model][sendingReplies] such that replies to the user will be sent asynchronously over a separately initiated HTTP request rather than inline with the HTTP POST for the incoming message to bot.
- Updated [Authentication model][authenticationModel].
- Decoupled bot data storage [Bot State][stateAPI] from messaging API.
- Added [new card format](~/dotnet/add-rich-card-attachments.md) for attachments.

### Changes

- Combined Bot Builder and Bot Connector into one <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/" target="_blank">NuGet package</a>.
- Made Bot Connector open source.
- Published <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder.calling" target="_blank">Bot.Builder.Calling</a> NuGet package that can be used to build [Skype Calling bots][skypeCallingBot].

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.5" target="_blank">v1.2.5</a>

### Changes

- Moved `Microsoft.Bot.Builder.FormFlow.Json` into a separate NuGet package.
- Added `PromptDialog` Attachment.
- Added NuGet symbols to <a href="https://www.nuget.org" target="_blank">NuGet.org</a>.
- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.4" target="_blank">v1.2.4</a>

### Breaking changes

- Renamed `IFormBuilder.OnCompletionAsync` to `OnCompletion` (since the method itself is not async).
- Implemented significant changes to JSON FormFlow.
- Implemented change such that missing resources will cause an error to be thrown.

### New features

- JSON FormFlow now supports specifying everything through the JSON schema file:

    1. There is a completely seperate builder, `FormBuilderJson` which takes the schema.
    2. JSON Schema now supports message, confirmation, validation with regex and dynamically compiled code.
    3. You no longer need to specify the schema on every field.
    4. It is packaged as a seperate dll (`Microsoft.Bot.Builder.FormFlow.Json`) that depends on `Microsoft.CodeAnalysis.CSharp.Scripting`, which brings in several dependencies.

- `LuisDialog` supports multiple `LuisModel` and `ILuisService` instances.
- New attribute `PatternAttribute` for field validation via regex.
- `IBotDataStore` now allows supplying your own storage implementation.

### Bug fixes

- <a href="https://github.com/Microsoft/BotBuilder/issues/414" target="_blank">#414</a> If an entity did not match a value, a blank string would be returned.
- <a href="https://github.com/Microsoft/BotBuilder/issues/434" target="_blank">#434</a> Add calculator scorable/dialog as an example of `IDialogStack.Forward` from `IScorable.PostAsync`.
- <a href="https://github.com/Microsoft/BotBuilder/issues/465" target="_blank">#465</a> Preserve exception stack trace when re-throwing.
- <a href="https://github.com/Microsoft/BotBuilder/issues/466" target="_blank">#466</a> Add a typed `TooManyAttemptsException` for prompts.
- <a href="https://github.com/Microsoft/BotBuilder/issues/472" target="_blank">#472</a> Test showing how to resolve dynamic form from container.
- <a href="https://github.com/Microsoft/BotBuilder/issues/416" target="_blank">#416</a> Fix bug in enumeration where if a term included numbers it would result in no match.
- <a href="https://github.com/Microsoft/BotBuilder/issues/440" target="_blank">#440</a> Fix syntax error in Japanese localization.
- <a href="https://github.com/Microsoft/BotBuilder/issues/446" target="_blank">#446</a> Expose a `Confirm` method that was mistakenly marked internal.
- <a href="https://github.com/Microsoft/BotBuilder/issues/449" target="_blank">#449</a> `InitialUpper` for `Normalize` did not return value.
- Make it so validation feedback is surfaced if `FeedbackOptions.Always`.
- Buttons did not include no preference.
- JSON Forms were not handling optional correctly.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.3" target="_blank">v1.2.3</a>

### Changes

- Updated FormFlow recognizers to take consistent set of args.
- Added `CancelScorable` as example of `IScorable`.
- Added sample using Azure Active Directory Authentication to access Microsoft graph.

### Bug fixes

- <a href="https://github.com/Microsoft/BotBuilder/issues/227" target="_blank">#227</a> FormFlow now handles clarification and verification of <a href="http://luis.ai" target="_blank">LUIS</a> entities. Initial messages are processed, then LUIS entities, and then remaining steps. 
- <a href="https://github.com/Microsoft/BotBuilder/issues/335" target="_blank">#335</a> Made it so that n-gram generation would not include empty strings if there were multiple spaces. 
- <a href="https://github.com/Microsoft/BotBuilder/issues/400" target="_blank">#400</a> Resolved issue with the way that ETag is set. 

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.2" target="_blank">v1.2.2</a>

### Breaking changes
- There is no longer a dependency on `Newtonsoft.Json.Schema`, so you can use `JObject.Parse` to parse your JSON Schema and define forms.

### Changes

- Made it possible to resolve root dialog from the container.
- Decoupled `IPostToBot` "middleware" from `IDialogTask`.
- Pushed lazy dialog instantiation into `DialogTask`.
- Factored out `ReactiveDialogTask` from `DialogTask`.
- Implemented forwarding of an item to child dialog.
- Added persistent dialog task.
- Added example of `AlwaysSendDirect_BotToUser`.
- Added **1** and **2** as possible responses to boolean recognizer (because buttons might send these values).

### Bug fixes

- <a href="https://github.com/Microsoft/BotBuilder/issues/227" target="_blank">#227</a> Fix issue related to processing of <a href="http://luis.ai" target="_blank">LUIS</a> entities.
- <a href="https://github.com/Microsoft/BotBuilder/issues/230" target="_blank">#230</a> Allow `ConnectorClientCredentials` to be injected from container.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.1" target="_blank">v1.2.1</a>

### Changes

- Ensured that <a href="http://luis.ai" target="_blank">LUIS</a> service queries are encoded with UTF8.

- Fixed a `Choice` prompt bug to rank complete matches higher than partial matches.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.0.1" target="_blank">v1.2.0.1</a>

### Changes

- Fixed missing dependencies for `Microsoft.Bot.Builder` 1.2.0 NuGet package.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.2.0" target="_blank">v1.2.0</a>

### Breaking changes

- Changed target framework to .NET 4.6. This change was necessary to reliably support using the thread culture for localization.
- Made it so that FormFlow `ValidateAsyncDelegate` must return the value to set in the field. This change was necessary to support programmatic value transformations.
- Changed the signature of `Conversation.Resume` to support a resumption cookie to maintain conversation state across dialog resumption.
- Moved to the latest NuGet packages, including for the Bot Framework Connector.

### New features
- System dialogs and FormFlow will now generate buttons for channels that support them. 
- FormFlow can now be driven by an extended JSON Schema that allows doing attributes in a similar way to C#.  This allows forms to be generated at run-time from data rather than C# reflection.
- System dialogs and FormFlow are localized to nine languages. (Contributions for more languages would be welcome.) We also provide tools to help generate the resource files required to localize your FormFlow state classes.
- `DateTime` parsing in English now uses **Chronic**, which supports more natural Date/Time expressions such as "tomorrow at 4".
- `LuisDialog` now supports the full <a href="http://luis.ai" target="_blank">LUIS</a> schema, including actions.

### Bug fixes

- Implemented general bug fixes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.1.0" target="_blank">v1.1.0</a>

### Breaking changes

- Renamed some delegates and methods for consistency and to support dynamic field definition. This only impacts bots that were directly using `Field` or `FieldReflector`.

### Changes
- Provided a way to dynamically define fields, confirmations and messages.
- Added `FormCanceledException`, which provides information on what steps were completed and where the user quit.
- Added more flexibility on how parenthesis are used when generating prompts.
- Fixed several bugs related to initial state and <a href="http://luis.ai" target="_blank">LUIS</a> entities.
- Extended chain model to support branching (`Chain.Switch`).
- Added support for resumption of a conversation.
- Added example of Facebook OAuth.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.0.2" target="_blank">v1.0.2</a>

### Changes

- Moved to `IDialog<T>` typed for result type.
- Added support for LINQ query syntax (e.g. `Select`, `SelectMany`).
- Multiple `IBotToUser.Post(Message)` calls.
- Moved to `Autofac` dependency injection container.
- Made it so that `IConnectorClient` is instantiated to point to emulator when emulating bot.
- Fixed an issue with `CommandDialog<T>`.
- Updated <a href="http://luis.ai" target="_blank">LUIS</a> models.
- Added `ChoiceCase`, `ChoiceParens` to Form template attributes.

## <a href="https://www.nuget.org/packages/Microsoft.Bot.Builder/1.0.1" target="_blank">v1.0.1</a>

### Changes

- Fixed `LuisDialog` to handle **null** score returned by LUIS (due to a behavior change in Cortana pre-built apps by <a href="http://luis.ai" target="_blank">LUIS</a>).
- Updated **nuspec** with better description.
- Added error-resilient context store.

[authentication]: https://docs.botframework.com/en-us/restapi/authentication/#changes

[connectorAPIv3]: https://docs.botframework.com/en-us/support/upgrade-to-v3/#navtitle

[addressing]: http://docs.botframework.com/en-us/support/upgrade-code-to-v3/#addressing

[sendingReplies]: http://docs.botframework.com/en-us/support/upgrade-code-to-v3/#sending-replies

[authenticationModel]: http://docs.botframework.com/en-us/restapi/authentication/#navtitle

[stateAPI]: http://docs.botframework.com/en-us/csharp/builder/sdkreference/stateapi.html

[skypeCallingBot]: http://docs.botframework.com/en-us/skype/calling/#navtitle