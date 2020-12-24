To function correctly, assign an action type to each clickable item on a hero card. This table lists and describes the available action types and what should be in the associated value property.
The `messageBack` card action has a more generalized meaning than the other card actions. See the [Card action](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#card-action) section of the [Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) for more information about the `messageBack` and other card action types.

| Type         | Description                                                                  | Value                                                              |
|:-------------|:-----------------------------------------------------------------------------|:-------------------------------------------------------------------|
| call         | Initiates a phone call.                                                      | Destination for the phone call in this format: `tel:123123123123`. |
| downloadFile | Downloads a file.                                                            | The URL of the file to download.                                   |
| imBack       | Sends a message to the bot, and posts a visible response in the chat.        | Text of the message to send.                                       |
| messageBack  | Represents a text response to be sent via the chat system.                   | An optional programmatic value to include in generated messages.   |
| openUrl      | Opens a URL in the built-in browser.                                         | The URL to open.                                                   |
| playAudio    | Plays audio.                                                                 | The URL of the audio to play.                                      |
| playVideo    | Plays a video.                                                               | The URL of video to play.                                          |
| postBack     | Sends a message to the bot, and may not post a visible response in the chat. | Text of the message to send.                                       |
| showImage    | Displays an image.                                                           | The URL of the image to display.                                   |
| signin       | Initiates an OAuth sign-in process.                                           | The URL of the OAuth flow to initiate.                             |
