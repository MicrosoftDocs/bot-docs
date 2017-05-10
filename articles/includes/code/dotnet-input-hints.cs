// <InputHintAcceptingInput>
Activity reply = activity.CreateReply("This is the text that will be displayed."); 
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.AcceptingInput;
await connector.Conversations.ReplyToActivityAsync(reply);
// </InputHintAcceptingInput>



// <InputHintExpectingInput>
Activity reply = activity.CreateReply("This is the text that will be displayed."); 
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.ExpectingInput;
// </InputHintExpectingInput>



// <InputHintIgnoringInput>
Activity reply = activity.CreateReply("This is the text that will be displayed."); 
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.IgnoringInput;
// </InputHintIgnoringInput>