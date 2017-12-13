// Code snippets for channel-connect-webchat-speech.md

// <BrowserSpeech>
const speechOptions = {
     speechRecognizer: new BotChat.Speech.BrowserSpeechRecognizer(),
     speechSynthesizer: new BotChat.Speech.BrowserSpeechSynthesizer()
};
// </BrowserSpeech>

// <BingSpeech>
const speechOptions = {
    speechRecognizer: new CognitiveServices.SpeechRecognizer({ subscriptionKey: 'YOUR_COGNITIVE_SPEECH_API_KEY' }),
    speechSynthesizer: new CognitiveServices.SpeechSynthesizer({
      gender: CognitiveServices.SynthesisGender.Female,
      subscriptionKey: 'YOUR_COGNITIVE_SPEECH_API_KEY',
      voiceName: 'Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'
    })
  };
// </BingSpeech>

// <FetchToken>
function getToken() {
// This call would be to your backend, or to retrieve a token that was served as part of the original page.
return fetch(
  'https://api.cognitive.microsoft.com/sts/v1.0/issueToken',
  {
    headers: {
      'Ocp-Apim-Subscription-Key': 'YOUR_COGNITIVE_SPEECH_API_KEY'
    },
    method: 'POST'
  }
).then(res => res.text());
}

const speechOptions = {
  speechRecognizer: new CognitiveServices.SpeechRecognizer({
    fetchCallback: (authFetchEventId) => getToken(),
    fetchOnExpiryCallback: (authFetchEventId) => getToken()
  }),
  speechSynthesizer: new BotChat.Speech.BrowserSpeechSynthesizer()
};
// </FetchToken>

// <CustomSpeechService>
const speechOptions = {
    speechRecognizer: new YourOwnSpeechRecognizer(),
    speechSynthesizer: new YourOwnSpeechSynthesizer()
  };
// </CustomSpeechService>

// <PassSpeechOptionsToWebChat>
BotChat.App({
    bot: bot,
    locale: params['locale'],
    resize: 'detect',
    // sendTyping: true,    // defaults to false. set to true to send 'typing' activities to bot (and other users) when user is typing
    speechOptions: speechOptions,
    user: user,
    directLine: {
      domain: params['domain'],
      secret: params['s'],
      token: params['t'],
      webSocket: params['webSocket'] && params['webSocket'] === 'true' // defaults to true
    }
  }, document.getElementById('BotChatGoesHere'));
// </PassSpeechOptionsToWebChat>