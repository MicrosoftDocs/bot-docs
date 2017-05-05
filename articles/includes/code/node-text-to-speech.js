// <IMessageSpeak>
var msg = new builder.Message(session)
    .speak('This is the text that will be spoken.')
    .inputHint(builder.InputHint.acceptingInput);
session.send(msg).endDialog();
// </IMessageSpeak>



// <SessionSay>
session.say('Please hold while I calculate a response.', 
    'Please hold while I calculate a response.', 
    { inputHint: builder.InputHint.ignoringInput }
);
// </SessionSay>



// <Prompt>
builder.Prompts.text(session, 'Are you sure that you want to cancel this transaction?', {            
    speak: 'Are you <emphasis level=\"moderate\">sure</emphasis> that you want to cancel this transaction?',
    retrySpeak: 'Are you <emphasis level=\"moderate\">sure</emphasis> that you want to cancel this transaction?',
    inputHint: builder.InputHint.expectingInput
});
// </Prompt>
