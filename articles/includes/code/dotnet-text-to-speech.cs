// <Speak1>
Activity reply = activity.CreateReply("This is the text that will be displayed."); 
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.AcceptingInput;
await connector.Conversations.ReplyToActivityAsync(reply);
// </Speak1>



// <Speak2>
await context.SayAsync(text: "Thank you for your order!", speak: "Thank you for your order!");
// </Speak2>



// <Speak3>
PromptDialog.Confirm(context, AfterResetAsync, 
    new PromptOptions<string>(prompt: "Are you sure that you want to cancel this transaction?", 
        speak: "Are you <emphasis level=\"moderate\">sure</emphasis> that you want to cancel this transaction?",
        retrySpeak: "Are you <emphasis level=\"moderate\">sure</emphasis> that you want to cancel this transaction?")); 
// </Speak3>
