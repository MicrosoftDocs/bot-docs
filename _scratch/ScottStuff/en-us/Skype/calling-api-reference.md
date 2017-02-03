---
layout: page
title: Skype Calling API Reference
permalink: /en-us/skype/calling/
weight: 5130
parent1: Channels
parent2: Skype bots
---

* TOC
{:toc}


## Overview

Skype Bot Platform for Calling API provides a mechanism for receiving and
handling Skype voice calls by bots.

Every time a Skype user places a call to a bot, Skype Bot Platform for Calling
will look up an HTTPs address of the bot and notify the bot about the call. The
bot can provide a list basic actions, called workflow, in response to initial
call notification.

In the first action of the workflow the bot should decide if it’s interested in
answering the call or rejecting the call. Should the bot decide to answer the
call, the subsequent actions instruct the Skype Bot Platform for Calling to
either play prompt, record audio, recognize speech, or collect digits from a
dial pad. The last action of the workflow could be a hang up the voice call.
Skype Bot Platform for Calling then takes the workflow and attempts to execute
actions in order given by bot.

If the workflow is executed successfully, the Skype Bot Platform for Calling
will post a result of last action on given webhook callback HTTPs address. For
example, if the last action was to record audio, the result will be a media
content with audio data. If the workflow could not be completed, for example
because a Skype user hang up the call, then the result will correspond to last
executed action.

During a voice call, the bot can decide after each callback on how to continue
interaction with Skype user. This allows the bots to drive complex interactions
comprising of basic action steps.

## Call Flow

Sample timeline of interactions between Skype caller, Skype Bot Platform and
bot.

<a href="/en-us/images/skype/calling/flow.png" target="_blank">
![](/en-us/images/skype/calling/flow.png)
</a>


## JSON contract overview

This is the high level overview of protocol request and responses provided by
Skype Bot Platform for Calling:

-   [Conversation](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#conversation)
    – this is a JSON body of first voice call notification request send by Skype
    Bot Platform for Calling to a bot. This message contains information about
    caller and target of the call.

-   [ConversationResult](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#ConversationResult)
    – this is a JSON body of any callback that is posted by Skype Bot Platform
    for Calling. ConversationResult contains result of the last successfully
    executed workflow action.

-   [Workflow](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#Workflow)
    – this is a JSON body that is created by bot in response to either
    Conversation notification or ConversationResult callback. The workflow
    contains one or more of atomic actions that a bot wishes for Skype Bot
    Platform for Calling to execute on a Skype voice call.

## Programming notes


### HTTP headers
{:.no_toc}

Skype Bot Platform for Calling will provide following HTTP headers for each
HTTPs callback.

-   Content-Type – always set to application/json.

-   Accept – always set to application/json.

-   X-Microsoft-Skype-Chain-ID – a unique ID that remains the same for all
    callbacks related to same Skype voice call. This ID should be used to track
    all actions related to a particular call.

-   X-Microsoft-Skype-Message-ID – a unique ID that is specific to this
    callback. If bot fails to responds to callback or responds with HTTP status
    indicating error – the same message-ID will be used on subsequent retries.

### HTTP errors and retries
{:.no_toc}

Since the Skype Bot Platform for Calling interact with the bot during ongoing
interactive voice call it is important for the bot to respond to callbacks
quickly and with HTTP status indicating success.

In case a bot responds with non-success HTTP status code (5xx) or a callback
times out, the Skype Bot Platform for Calling will retry the request providing
the same X-Microsoft-Skype-Message-ID header. In case all retries fail, Skype
Bot Platform for Calling will hang up the call.

Note, that callback mechanism does guarantee reliable delivery. In case a bot
maintains a persistent state about a Skype call, the bot should implement
appropriate logic to clean up the state in case the callback is not delivered.
Examples of such logic could be a timeout in case the Skype Bot Platform for
Calling callback was not received in expected time.

### Authentication
{:.no_toc}

Skype Bot Platform for Calling requires all callbacks to be sent of secure HTTPs
channel. The certificate presented by the bot must chain to one of well-known
public certificate authorities. On every HTTPs call the Skype Bot Platform for
Calling will present a certificate using HTTPs client authentication, the bot
should validate that certificate common name is issued for \*.skype.com domain,
the certificate is not-expired, not revoked and chains to Microsoft Baltimore
Root certificate authority.

### Note about Skype Platform Calling API versioning
{:.no_toc}

Skype Bot Platform for Calling will evolve over time. We will notify developers
about any breaking changes or future versions of APIs and give a time to migrate
to newer version of API. We could however change the JSON contracts in some ways
and bots should handle such changes without needing advanced API change notice:

-   We could add new primitive or complex JSON elements inside any JSON element.
    We recommend that implementers of the protocol are prepared to ignore
    elements that may not be understood.

-   We could extend any enumerations with new values. We recommend that
    implementers of the protocol are prepared to handle new values by defaulting
    to NotUnderstood value.

## Calling conversation object model.

### Conversation
{:.no_toc}

Conversation is a JSON body of a first request for new Skype voice call made by
Skype Bot Platform for Calling to a bot. Conversation JSON body is posted on
initial HTTPs endpoint registered by a bot developer in Skype Developer Portal.
Conversation request contains information about caller and target of the call
and some additional information about initial state of a call.

| **Field**                  | **Meaning**                                                                            | **Type**                                                                                              |
|----------------------------|----------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------|
| **Id**                     | **Required.** A unique identifier for Skype call.                                      | String                                                                                                |
| **Participants**           | **Required.** List of participants in the conversation.                                | Array of [Participant](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#participant) |
| **PresentedModalityTypes** | **Required.** Flag indicates which modalities were presented by Skype user for a call. | [ModalityType](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#modalitytype)        |
| **IsMultiparty**           | **Required.** Indicates whether a call is a group call or 1:1.                         | Boolean                                                                                               |
| **CallState**              | **Required.** Indicates the current state of the call.                                 | [CallState](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#callstate)              |
| **Links**                  | **Optional.** Dictionary containing list of HTTPs links.                               | Dictionary of string to uri                                                                           |
| **Subject**                | **Optional.** The subject of a group call.                                             | String                                                                                                |
| **ThreadId**               | **Optional.** The identifier of a chat thread for group call.                          | String                                                                                                |


{% highlight plaintext %}
POST https://example.com/bot/oncallreceived/ HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
Content-Type:  application/json; charset=utf-8
{
  "id": "11111111-2222-3333-4444-555555555555",",
  "participants": [
    {
      "identity": "8:skype.user",
      "languageId": "en",
      "originator": true
    },
    {
      "identity": "28:b0100000-1d00-aaaa-bbbb-cccccccccccc",
      "originator": false
    }
  ],
  "isMultiparty": false,
  "presentedModalityTypes": [
    "audio"
  ],
  "callState": "incoming"
}
{% endhighlight %}


### ConversationResult
{:.no_toc}

ConversationResult is a JSON body of any subsequent request following the
initial Conversation notification that is send to a bot from Skype Bot Platform
for Calling. ConversationResult is posted to a callback link provided by
previous Workflow response. ConversationResult represents the result of a last
successful action from previous Workflow response.

| **Field**            | **Meaning**                                                                                                                                                                                                                                   | **Type**                                                                                 |
|----------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| **Id**               | **Required.** A unique identifier for Skype call.                                                                                                                                                                                             | String                                                                                   |
| **CallState**        | **Required.** Indicates the current state of the call.                                                                                                                                                                                        | [CallState](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#callstate) |
| **OperationOutcome** | **Required.** Outcome of last executed workflow action. For more information about objects that could be returned see [actions and outcomes section.](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#actions-and-outcomes) | Outcome specific for action.                                                             |
| **Links**            | **Optional.** Dictionary containing list of HTTPs links.                                                                                                                                                                                      | Dictionary of string to uri                                                              |
| **AppState**         | **Optional.** Echoed application context state from previous workflow.                                                                                                                                                                        | String                                                                                   |

{% highlight plaintext %}

POST https://example.com/bot/oncallanswered HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
Content-Type:  application/json; charset=utf-8

{
  "id": "11111111-2222-3333-4444-555555555555",
  "appId": "b0100000-1d00-aaaa-bbbb-cccccccccccc",
  "operationOutcome": {
    "type": "answerOutcome",
    "id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
    "outcome": "success",
    "failureReason": ""
  },
  "callState": "established",
  "appState": "applicationSpecificState",
  "links": {
    "call": "https://calling.skype.com/platform/v1/calls/11111111-2222-3333-4444-555555555555"
  }
}
{% endhighlight %}


### Workflow
{:.no_toc}

Workflow is a JSON body send by the bot in response to Conversation or
ConversationResult request from Skype Bot Platform for Calling. Workflow
contains list of one or more actions that bots instructs Skype Bot Platform for
Calling on execute on its behalf as well as callback HTTPs address if bot wants
to be notified about result of last executed action outcome.

| **Field**     | **Meaning**                                                                                                                                                                                                                                                                   | **Type**                                                                                 |
|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| **Actions**   | **Required.** A list of one or more actions that a bot wants to execute on call. For more information about supported actions and action outcomes see [actions and outcomes section.](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#actions-and-outcomes) | Array of ActionBase                                                                      |
| **Links**     | **Optional.** A callback link that will be used once the workflow is executed to reply with outcome of workflow.                                                                                                                                                              | CallbackLink                                                                             |
| **CallState** | **Required.** Indicates the current state of the call.                                                                                                                                                                                                                        | [CallState](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#callstate) |
| **AppState**  | **Optional.** Application state string that bot can use to use for maintaining call context. AppState will be echoed back to bot on ConversationResult corresponding to the workflow. This field is limited in length to 1024 characters.                                     | String                                                                                   |

{% highlight plaintext %}

HTTP/1.1 200

{
  "links": {
    "callback": "https://example.com/bot/oncallanswered"
  },
  "actions": [
    {
      "operationId": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
      "action": "answer",
      "acceptModalityTypes": [
        "audio"
      ]
    }
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}


## Actions and outcomes.

### Answer
{:.no_toc}

Answer action allows a bot to accept a Skype call. Answer action should be a
first action in response to Conversation notification.

| **Field**               | **Meaning**                                                                                                | **Type** |
|-------------------------|------------------------------------------------------------------------------------------------------------|----------|
| **OperationId**         | **Required.** Used to correlate outcomes to actions in ConversationResult.                                 | String   |
| **AcceptModalityTypes** | **Optional.** The modality types the application will accept. If none are specified it assumes audio only. | String   |

{% highlight plaintext %}
POST https://example.com/bots/oncallreceived/
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "links": {
    "callback": "https://example.com/bots/onanswerdone"
  },
  "actions": [
    {
      "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
      "action": "answer",
      "acceptModalityTypes": [
        "audio"
      ]
    },
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}


### AnswerOutcome
{:.no_toc}

Result of Answer action.

| **Field**         | **Meaning**                                                                                                                                      | **Type** |
|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Outcome**       | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action. | String   |
| **FailureReason** | **Optional.** The reason for failure.                                                                                                            | String   |
| **Type**          | **Optional.** [ValidOutcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#validoutcomes) enum indicates the outcome type.  | String   |

{% highlight plaintext %}
POST https://example.com/bots/onanswerdone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
Content-Type:  application/json; charset=utf-8

{
  "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
  "appId": "534C328F-9885-47A6-ADCF-692B53BDA4FE",
  "operationOutcome": {
    "type": "answerOutcome",
    "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
    "outcome": "success",
    "failureReason": ""
  },
  "callState": "established",
  "appState": "applicationSpecificState",
  "links": {
    "call": "https://calling.skype.com:6700/platform/v1/calls/d33fd126-f48d-49f6-bd3e-e3afe7e9d644"
  }
}
{% endhighlight %}


### Reject
{:.no_toc}

Reject allows to decline to answer the call. Reject action could be used as
first action of first workflow instead of Answer.

| **Field**       | **Meaning**                                                                | **Type** |
|-----------------|----------------------------------------------------------------------------|----------|
| **OperationId** | **Required.** Used to correlate outcomes to actions in ConversationResult. | String   |

{% highlight plaintext %}
POST https://example.com/bots/
HTTP/1.1 200
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "links": {
    "callback": "https://example.com/bots/onrejectdone"
  },
  "actions": [
    {
      "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
      "action": "reject"
    },
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}

### RejectOutcome
{:.no_toc}

Result of reject action. Reject can be used instead of Answer action if bot
decides that the bot does not want to answer the call.

| **Field**         | **Meaning**                                                                                                                                      | **Type** |
|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Id**            | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                       | String   |
| **Outcome**       | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action. | String   |
| **FailureReason** | **Optional.** The reason for failure.                                                                                                            | String   |

{% highlight plaintext %}
POST https://example.com/bots/onanswerdone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
Content-Type:  application/json; charset=utf-8
{
  "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
  "appId": "534C328F-9885-47A6-ADCF-692B53BDA4FE",
  "operationOutcome": {
    "type": "rejectOutcome",
    "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
    "outcome": "success",
    "failureReason": ""
  },
  "callState": "terminated",
  "appState": "applicationSpecificState",
  "links": {
    "call": "https://calling.skype.com:6700/platform/v1/calls/d33fd126-f48d-49f6-bd3e-e3afe7e9d644"
  }
}
{% endhighlight %}


### Record
{:.no_toc}

Record action is interactive action where Skype user audio is recorded.

| **Field**                          | **Meaning**                                                                                                                                                                                                                    | **Type**                                                                                             |
|------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|
| **OperationId**                    | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                                                                                                     | String                                                                                               |
| **PlayPrompt**                     | **Optional.** A prompt to be played before the recording.                                                                                                                                                                      | String                                                                                               |
| **MaxDurationInSeconds**           | **Optional.** Maximum duration of recording. The default value is 180 seconds.                                                                                                                                                 | Int                                                                                                  |
| **InitialSilenceTimeoutInSeconds** | **Optional.** Maximum initial silence allowed before the recording is stopped. The default value is 5 seconds.                                                                                                                 | Int                                                                                                  |
| **MaxSilenceTimeoutInSeconds**     | **Optional.** Maximum silence allowed after the speech is detected. The default value is 5 seconds.                                                                                                                            | Int                                                                                                  |
| **RecordingFormat**                | **Optional.** The format expected for the recording. The [RecordingFormat](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#recordingformat) enum describes the supported values. The default value is “wma”. | [RecordingFormat](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#recordingformat) |
| **PlayBeep**                       | **Optional.** Indicates whether to play beep sound before starting a recording action.                                                                                                                                         | Boolean                                                                                              |
| **StopTones**                      | **Optional.** Stop digits that user can press on dial pad to stop the recording.                                                                                                                                               | Boolean                                                                                              |

{% highlight plaintext %}
POST https://example.com/bots/oncallanswered/
HTTP/1.1 200
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "links": {
    "callback": "https://example.com/bots/onplaypromptdone"
  },
  "actions": [
    {
      "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
      "action": "record",
      "playPrompt": {
        "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
        "action": "playPrompt",
        "prompts": [
          {
            "value": "Welcome to Skype Platform Sample Application! How are you doing today?"
          },
          {
            "fileUri":
                  "https://example.com/beepsound.wma"
          },
        ]
      },
      "maxDurationInSeconds": 15,
      "initialSilenceTimeoutInSeconds": 5,
      "maxSilenceTimeoutInSeconds": 2,
      "playBeep": false
    }
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}

### RecordOutcome
{:.no_toc}

Record outcome returns the result of record audio action. RecordOutcome could be
returned as multipart content where first part of multipart contain contains the
result of action while second part contains binary stream representing recorded
audio.

| **Field**                   | **Meaning**                                                                                                                                                                        | **Type** |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Id**                      | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                                                         | String   |
| **Outcome**                 | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action.                                   | String   |
| **FailureReason**           | **Optional.** The reason for failure.                                                                                                                                              | String   |
| **Type**                    | **Required.** [ValidOutcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#validoutcomes) enum indicates the outcome.                                         | String   |
| **CompletionReason**        | **Required.** The [RecordingCompletionReason](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#recordingcompletionreason) enum value for the completion's reason. | String   |
| **LengthOfRecordingInSecs** | **Required.** Length of recorded audio in seconds.                                                                                                                                 | Decimal  |

{% highlight plaintext %}
POST https://example.com/bots/OnRecordingDone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  ce0a8776-cd0f-4134-9dbd-7a7d0120ccbe
Content-Type:  multipart/form-data; boundary="105dfb0d-6ae5-4774-87ef-916017ea6971", application/json; charset=utf-8
-- beginning of multipart content --
Content-Disposition:  form-data; name=conversationResult
{
  "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
  "operationOutcome": {
    "type": "recordOutcome",
    "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
    "format": "wma",
    "completionReason": "maxRecordingTimeout",
    "lengthOfRecordingInSecs": 13,
    "outcome": "success"
  },
  "callState": "established",
  "appState": "applicationSpecificState",
  "links": {
    "call": "https://calling.skype.com:6700/platform/v1/calls/d33fd126-f48d-49f6-bd3e-e3afe7e9d644"
  }
}
-- end of multipart content --
-- beginning of multipart content --
Content-Type: audio/x-ms-wma
Content-Disposition: form-data; name=recordedAudio
(byte stream)
-- end of multipart content –
{% endhighlight %}


### PlayPrompt
{:.no_toc}

PlayPrompt allows to play either Text-To-Speech audio or a media file.

| **Field**       | **Meaning**                                                                | **Type** |
|-----------------|----------------------------------------------------------------------------|----------|
| **OperationId** | **Required.** Used to correlate outcomes to actions in ConversationResult. | String   |
| **Prompts**     | **Required.** List of prompts to play out with each single prompt object.  | String   |

Prompt structure.

| **Field**                       | **Meaning**                                                                                                                                                                                                                                                            | **Type** |
|---------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Value**                       | **Optional.** Text-To-Speech text to be played to Skype user. Either Value or FileUri must be specified.                                                                                                                                                               | String   |
| **FileUri**                     | **Optional.** HTTP of played media file. Supported formats are WMA or WAV. The file is limited to 512kb in size and cached by Skype Bot Platform for Calling. Either Value or FileUri must be specified.                                                               | String   |
| **Voice**                       | **Optional.** [VoiceGender](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#voicegender) enum value. The default value is “Male”.                                                                                                                    | String   |
| **Culture**                     | **Optional.** The [Language](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#language) enum value to use for Text-To-Speech. Only applicable if "Value" is text. The default value is “en-US”. Note, currently en-US is the only supported language. | String   |
| **SilenceLengthInMilliseconds** | **Optional.** Any silence played out before "Value” is played. If “Value" is null, this field must be a valid \> 0 value.                                                                                                                                              | Int      |
| **Emphasize**                   | **Optional.** Indicates whether to emphasize when tts'ing out. It's applicable only if "Value" is text. The default value is false.                                                                                                                                    | Boolean  |
| **SayAs**                       | **Optional.** The [SayAs](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#sayas) enum value indicates whether to customize pronunciation during tts. It's applicable only if "Value" is text. The default value is none.                             | Boolean  |

{% highlight plaintext %}
POST https://example.com/bots/
HTTP/1.1 200
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "links": {
    "callback": "https://example.com/bots/onplaypromptdone"
  },
  "actions": [
    {
      "playPrompt": {
        "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
        "action": "playPrompt",
        "prompts": [
          {
            "value": "Here is your recording"
          },
          {
            "fileUri":
                  "https://example.blob.core.windows.net/samplerecording.wma"
          }
        ]
      },
      "maxDurationInSeconds": 15,
      "initialSilenceTimeoutInSeconds": 5,
      "maxSilenceTimeoutInSeconds": 2,
      "playBeep": false
    }
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}

### PlayPromptOutcome
{:.no_toc}

Play prompt outcome returns the result of playing a prompt.

| **Field**            | **Meaning**                                                                                                                                                                  | **Type** |
|----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Id**               | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                                                   | String   |
| **Outcome**          | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action.                             | String   |
| **FailureReason**    | **Optional.** The reason for failure.                                                                                                                                        | String   |
| **Type**             | **Required.** [ValidOutcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#validoutcomes) enum indicates the outcome.                                   | String   |
| **CompletionReason** | **Required.** The [PromptCompletionReason](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#promptcompletionreason) enum value for the completion's reason. | String   |

{% highlight plaintext %}
POST https://example.com/bots/onplaypromptdone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  ef43cb5f-d40e-4e8b-9ee7-f1adc8726e2f
Content-Type:  application/json; charset=utf-8
{
  "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
  "appId": "534C328F-9885-47A6-ADCF-692B53BDA4FE",
  "operationOutcome": {
    "type": "playPromptOutcome",
    "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
    "outcome": "success",
    "failureReason": ""
  },
  "callState": "established",
  "appState": "applicationSpecificState",
  "links": {
    "call":"https://calling.skype.com:6700/platform/v1/calls/d33fd126-f48d-49f6-bd3e-e3afe7e9d644"
  }
} 
{% endhighlight %}

### Recognize
{:.no_toc}

Recognize action allows to either capture the speech recognition output or
collect digits from Skype user dial pad.

| **Field**                          | **Meaning**                                                                                                                                                                                                                                                     | **Type** |
|------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **OperationId**                    | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                                                                                                                                      | String   |
| **Action**                         | **Required.** The type of action.                                                                                                                                                                                                                               | String   |
| **PlayPrompt**                     | **Optional.** A prompt to be played before the recognition starts.                                                                                                                                                                                              | String   |
| **BargeInAllowed**                 | **Optional.** Indicates if Skype user is allowed to enter choice before the prompt finishes. The default value is true.                                                                                                                                         | String   |
| **Culture**                        | **Optional.** Culture is an enum indicating what culture the speech recognizer should use. The default value is “en-US”. Currently the only culture supported is en-US.                                                                                         | String   |
| **InitialSilenceTimeoutInSeconds** | **Optional.** Maximum initial silence allowed before failing the operation from the time we start the recording. The default value is 5 seconds.                                                                                                                | Decimal  |
| **InterdigitTimeoutInSeconds**     | **Optional.** Maximum allowed time between dial pad digits. The default value is 1 second.                                                                                                                                                                      | Decimal  |
| **Choices**                        | **Optional.** List of [RecognitionOption](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#recognitionoption) objects dictating the recognizable choices. Choices can be speech or dial pad digit based.                                       | Decimal  |
| **CollectDigits**                  | **Optional.** [CollectDigits](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#collectdigits) will result in collecting digits from Skype user dial pad as part of recognize. Either CollectDigits or Choices must be specified, but not both. | Decimal  |

{% highlight plaintext %}
POST https://example.com/bots/
HTTP/1.1 200
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "links": {
    "callback": "https://example.com/bots/OnRecognizeDone"
  },
  "actions": [
    {
      "operationId": "6aac19af-ef89-4fa3-85e3-9e4236ac8dc4",
      "action": "answer",
      "acceptModalityTypes": [
        "audio"
      ]
    },
    {
      "operationId": "94dbb0c6-5bbf-489f-b531-523a55a29312",
      "action": "recognize",
      "playPrompt": {
        "operationId": "94dbb0c6-5bbf-489f-b531-523a55a29312",
        "action": "playPrompt",
        "prompts": [
          {
            "value": "Type 1 to hangup, type 2 to play a sound, type 3 to record and playback, type 4 to transfer call ",
            "voice": "male"
          }
        ]
      },
      "bargeInAllowed": true,
      "choices": [
        {
          "name": "hangup",
          "dtmfVariation": "1"
        },
        {
          "name": "play",
          "dtmfVariation": "2"
        },
        {
          "name": "record",
          "dtmfVariation": "3"
        },
        {
          "name": "transfer",
          "dtmfVariation": "4"
        }
      ]
    }
  ],
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}


### RecognizeOutcome
{:.no_toc}

Recognize outcome is a result of recognize action. It contains either recognized
digits or recognized speech.

| **Field**                | **Meaning**                                                                                                                                      | **Type** |
|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Id**                   | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                       | String   |
| **Outcome**              | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action. | String   |
| **FailureReason**        | **Optional.** The reason for failure.                                                                                                            | String   |
| **Type**                 | **Required.** The type indicating RecognizeOutcome.                                                                                              | String   |
| **ChoiceOutcome**        | **Optional.** The value indicating captured speech recognition choice.                                                                           | String   |
| **CollectDigitsOutcome** | **Optional.** The value indicating capturing collected dial pad digit.                                                                           | String   |


{% highlight plaintext %}
POST https://example.com/bots/OnRecognizeDone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  e97317c8-af90-4dbe-a3f6-7ba37f2e367f
Accept:  application/json
X-Microsoft-Skype-Message-ID:  41cebb20-8150-45ed-8dac-25075a940417
Content-Type:  application/json; charset=utf-8
{
  "id": "e97317c8af904dbea3f67ba37f2e367f",
  "operationOutcome": {
    "type": "recognizeOutcome",
    "id": "94dbb0c6-5bbf-489f-b531-523a55a29312",
    "choiceOutcome": {
      "completionReason": "dtmfOptionMatched",
      "choiceName": "record"
    },
    "outcome": "success"
  },
  "callState": "established",
  "links": {
    "call": "https://calling.skype.com:6700/platform/v1/calls/dba2c931d22e4f22830bf24a23cf89f8"
  }
}
{% endhighlight %}

### Hangup
{:.no_toc}

Hang up allows for bot to end ongoing call. Hang up is the last action in
workflow. Note, the different between Hangup and Reject. Reject action allows
the bot to end the call instead of answering the call while Hangup terminates
ongoing call.

| **Field**       | **Meaning**                                                                | **Type** |
|-----------------|----------------------------------------------------------------------------|----------|
| **OperationId** | **Required.** Used to correlate outcomes to actions in ConversationResult. | String   |
| **Action**      | **Required.** The type of action.                                          | String   |

{% highlight plaintext %}
POST https://example.com/bots/
HTTP/1.1 200
X-Microsoft-Skype-Message-ID:  ef43cb5f-d40e-4e8b-9ee7-f1adc8726e2f
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Content-Type:  application/json; charset=utf-8
{
  "actions": [
    {
      "operationId": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
      "action": "hangup"
    }
  ],
  "appState": "applicationSpecificState",
  "notificationSubscriptions": [
    "callStateChange"
  ]
}
{% endhighlight %}


### HangupOutcome
{:.no_toc}

Returns the result of hangup.

| **Field**         | **Meaning**                                                                                                                                      | **Type** |
|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| **Id**            | **Required.** Used to correlate outcomes to actions in ConversationResult.                                                                       | String   |
| **Outcome**       | **Required.** The [outcomes](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#outcomes) enum value is the result of the action. | String   |
| **FailureReason** | **Optional.** The reason for failure.                                                                                                            | String   |
| **Type**          | **Required.** The type indicating RecognizeOutcome.                                                                                              | String   |

{% highlight plaintext %}
POST https://example.com/bots/onanswerdone HTTP/1.1
X-Microsoft-Skype-Chain-ID:  09021b10-38c7-4f7c-91ba-3b7a60aa9d54
Accept:  application/json
X-Microsoft-Skype-Message-ID:  a287213d-217d-4ed2-b994-ba6bdd734d2f
Content-Type:  application/json; charset=utf-8
{
  "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
  "appId": "534C328F-9885-47A6-ADCF-692B53BDA4FE",
  "operationOutcome": {
    "type": "hangupOutcome",
    "id": "09021b10-38c7-4f7c-91ba-3b7a60aa9d54",
    "outcome": "success",
    "failureReason": ""
  },
  "callState": "established",
  "appState": "applicationSpecificState",
  "links": {
    "call": "https://calling.skype.com:6700/platform/v1/calls/d33fd126-f48d-49f6-bd3e-e3afe7e9d644"
  }
}
{% endhighlight %}


## Complex data types.

### ValidOutcomes
{:.no_toc}

ValidOutcomes is enumeration that specifies types for all supported action
outcomes.

| **Name**                  | **Meaning**                                                                                         |
|---------------------------|-----------------------------------------------------------------------------------------------------|
| AnswerOutcome             | The outcome of answer action.                                                                       |
| HangupOutcome             | The outcome of hangup action.                                                                       |
| RejectOutcome             | The outcome of reject action.                                                                       |
| PlayPromptOutcome         | The outcome of playprompt action.                                                                   |
| RecordOutcome             | The outcome of record action.                                                                       |
| RecognizeOutcome          | The outcome of recognize action.                                                                    |
| WorkflowValidationOutcome | The outcome of workflow validation. Sent when the bot sends invalid workflow to Skype Bot Platform. |

### Participant
{:.no_toc}

Participant describes member of either 1:1 or group conversations.

| **Name**    | **Descripton**                                                              | **Type** |
|-------------|-----------------------------------------------------------------------------|----------|
| Identity    | **Required.** Skype identifier of participant. For example 8:skype.user     | String   |
| DisplayName | **Optional.** Display name of the participant (if any).                     | String   |
| LanguageId  | **Optional.** Language configured on Skype client.                          | String   |
| Originator  | **Required.** Flag indicating whether the participant started a Skype call. | Boolean  |

### RecordingCompletionReason
{:.no_toc}

The RecordingCompletionReason enum describes the reasons for a recording
operation's completion.

| **Name**                  | **Description**                                                                                                     |
|---------------------------|---------------------------------------------------------------------------------------------------------------------|
| InitialSilenceTimeout     | If the maximum initial silence tolerated had been reached. It results in a failed recording attempt.                |
| MaxRecordingTimeout       | If the maximum recording duration for recording was reached. It results in a failed recognition attempt.            |
| CompletedSilenceDetected  | Silence after a burst of talking was detected, ending the call. It results in a successful recording attempt.       |
| CompletedStopToneDetected | The customer completed recording by punching in a stop tone. It results in a successful recording attempt.          |
| CallTerminated            | The underlying call was terminated. If there were any bytes recorded, it results in a successful recording attempt. |
| TemporarySystemFailure    | System failure.                                                                                                     |

### Language
{:.no_toc}

The Language enum used in PlayPrompt or Recognize action.

| **Name** | **Description**   |
|----------|-------------------|
| EnUs     | American English. |

### RecognitionOption
{:.no_toc}

Description is part of the "recognize" action. It needs to be specified if the
developer wants speech/DTMF choice-based recognition. For example, "Say 'Sales'
or press 1 for the sales department

| **Name**        | **Description**                                                                                                                           | **Type**          |
|-----------------|-------------------------------------------------------------------------------------------------------------------------------------------|-------------------|
| Name            | **Required.** The choice's name. Once a choice matches, this name is conveyed back to the bot in the outcome.                             | String            |
| SpeechVariation | **Optional.** Speech variations that form the choice's grammar. For example, Name : "Yes", SpeechVariation : {"Yes", "yeah", "ya", "yo" } | Array of strings  |
| DtmfVariation   | **Optional.** DTMF variations for the choice. For e.g. Name : "Yes" , DtmfVariation : {'1'}                                               | Array if decimals |

### CollectDigits
{:.no_toc}

Description is part of the "recognize" action. It's specified if the developer
wants to collect digits. For example, "Enter your 5-digit zip code followed by
the pound sign."

| **Name**         | **Description**                                                       | **Type**          |
|------------------|-----------------------------------------------------------------------|-------------------|
| MaxNumberOfDtmfs | **Optional.** Maximum number of digits expected.                      | Decimal           |
| StopTones        | **Optional.** Stop tones that will end the dial pad digit collection. | Array of Decimal. |

### ChoiceOutcome
{:.no_toc}

ChoiceOutcome is part of the "recognize" action outcome. It's specified if the
developer indicated any recognition options.

| **Name**         | **Description**                                                                                                                                                                                                                   | **Type** |
|------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|
| CompletionReason | **Required.** [RecognizeCompletionReason](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#recognizecompletionreason) enum indicates the recognition operation's completion reason of the recognition operation. | String   |
| ChoiceName       | **Optional.** Choice that was recognized if any.                                                                                                                                                                                  | String   |

### RecognitionCompletionReason
{:.no_toc}

The RecognitionCompletionReason enum describes the reasons for completing the
speech or digit recognition.

| **Name**               | **Description**                                                                                                                                                                                                             |
|------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| InitialSilenceTimeout  | Indicates the maximum initial silence tolerated was reached. It results in a failed recognition attempt.                                                                                                                    |
| IncorrectDtmf          | The recognition completed because the customer pressed in a digit not among the possible choices. For speech recognition based menus, this completion reason is never possible. It results in a failed recognition attempt. |
| InterdigitTimeout      | The maximum time between a customer punching in successive digits has elapsed. For speech menus, the completion reason is never possible. It results in a failed recognition attempt.                                       |
| SpeechOptionMatched    | The recognition successfully matched a grammar option.                                                                                                                                                                      |
| DtmfOptionMatched      | The recognition successfully matched a digit option.                                                                                                                                                                        |
| CallTerminated         | The underlying call was terminated. It results in a failed recognition attempt                                                                                                                                              |
| TemporarySystemFailure | System failure.                                                                                                                                                                                                             |

### CollectDigitsOutcome
{:.no_toc}

CollectDigitsOutcome is part of the "recognize" action outcome. Specified if the
developer chose any collectdigits operation.

| **Name**         | **Description**                                                                                                                                                                                            | **Type**          |
|------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------|
| CompletionReason | [DigitCollectionCompletionReason](https://developer.microsoft.com/en-us/skype/bots/docs/api/calling#digitcollectioncompletionreason) enum. Indicates the completion reason of the collectdigits operation. | String            |
| Digits           | **Optional.** Recognized digits.                                                                                                                                                                           | Array of decimal. |

### DigitCollectionCompletionReason
{:.no_toc}

The DigitCollectionCompletionReason enum describes the completion reasons of the
digit collection operation.

| **Name**                  | **Description**                                                                                                                                                                                |
|---------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| InitialSilenceTimeout     | Indicates the maximum initial silence tolerated was reached. It results in a failed recognition attempt.                                                                                       |
| InterdigitTimeout         | The maximum time between a customer pressing in successive digits has elapsed. It results in a successful digit collection together with the digits collected until timeout.                   |
| CompletedStopToneDetected | The customer completed recording by pressing a stop tone. It results in a successful recording attempt. The stoptones detected are excluded and not returned in collection of captured digits. |
| CallTerminated            | The underlying call was terminated. It results in a failed recognition attempt.                                                                                                                |
| TemporarySystemFailure    | System failure.                                                                                                                                                                                |

### VoiceGender
{:.no_toc}

The VoiceGender enum describes the list of voice genders for text to speech.

| **Name** | **Description**         |
|----------|-------------------------|
| Male     | Indicates male voice.   |
| Female   | Indicates female voice. |

### RecordingFormat
{:.no_toc}

The RecordingFormat enum describes the list of encoding formats used for
recording.

| **Name** | **Description**                       |
|----------|---------------------------------------|
| Wma      | Indicates Windows media audio format. |
| Wav      | Indicates waveform audio file format. |
| Mp3      | Indicates mp3 audio file format.      |

### CallState
{:.no_toc}

The CallState enum describes call's various possible states.

| **Name**     | **Description**                                                                         |
|--------------|-----------------------------------------------------------------------------------------|
| Idle         | Indicates the call's initial state.                                                     |
| Incoming     | Indicates the call has just been received.                                              |
| Establishing | Indicates the call establishment is in progress after initiating or accepting the call. |
| Established  | Indicates the call is established.                                                      |
| Hold         | Indicates the call is on hold.                                                          |
| Unhold       | Indicates the call is no longer on hold.                                                |
| Transferring | Indicates the call initiated a transfer.                                                |
| Redirecting  | Indicates the call initiated a redirection.                                             |
| Terminating  | Indicates the call is terminating.                                                      |
| Terminated   | Indicates the call is terminated.                                                       |

### ModalityType
{:.no_toc}

The ModalityType enum describes the various supported call modality types.

| **Name**                | **Description**                                             |
|-------------------------|-------------------------------------------------------------|
| Audio                   | Indicates the call has audio modality.                      |
| Video                   | Indicates the call has video modality.                      |
| VideoBasedScreenSharing | Indicates the call has video-based screen sharing modality. |

### Outcomes
{:.no_toc}

The Outcomes enum describes possible result value.

| **Name** | **Description**    |
|----------|--------------------|
| Success  | Indicates success. |
| Failure  | Indicates failure. |

### SayAs
{:.no_toc}

The SayAs enum describes the list of supported pronunciation attributes when
using text to speech.

| **Name**     | **Description**              |
|--------------|------------------------------|
| YearMonthDay | Say as year, month and day.  |
| MonthDayYear | Say as month, day and year.  |
| DayMonthYear | Say say day, month and year. |
| YearMonth    | Say as year and month.       |
| MonthYear    | Say as month and year.       |
| MonthDay     | Say as month and day.        |
| DayMonth     | Say as day and month.        |
| Day          | Say as day.                  |
| Month        | Say as month.                |
| Year         | Say as year.                 |
| Cardinal     | Say as cardinal.             |
| Ordinal      | Say as ordinal.              |
| Letters      | Say as letters.              |
| Time12       | Say as 12 hour time.         |
| Time24       | Say as 24 hour time.         |
| Telephone    | Say as telephone.            |
| Name         | Say as name.                 |
| PhoneticName | Say as phonetic name.        |
