// <InputHintAcceptingInput>
var msg = new builder.Message(session)
    .speak('This is the text that will be spoken.')
    .inputHint(builder.InputHint.acceptingInput);
session.send(msg).endDialog();
// </InputHintAcceptingInput>



// <InputHintIgnoringInput>
session.say('Please hold while I calculate a response. Thanks!', 
    'Please hold while I calculate a response. Thanks!', 
    { inputHint: builder.InputHint.ignoringInput }
);
// </InputHintIgnoringInput>



// <InputHintExpectingInput>
builder.Prompts.text(session, 'This is the text that will be displayed.', {                                    
    speak: 'This is the text that will be spoken initially.',
    retrySpeak: 'This is the text that is spoken after waiting a while for user input.',  
    inputHint: builder.InputHint.expectingInput
});
// </InputHintExpectingInput>
