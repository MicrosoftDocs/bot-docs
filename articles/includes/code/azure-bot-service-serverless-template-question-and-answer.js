//<processMessage>
bot.dialog('/', BasicQnAMakerDialog);
//</processMessage>



// <BasicQnAMakerDialog>
var recognizer = new cognitiveservices.QnAMakerRecognizer({
	knowledgeBaseId: 'set your kbid here', 
	subscriptionKey: 'set your subscription key here'});

var BasicQnAMakerDialog = new cognitiveservices.QnAMakerDialog({ 
	recognizers: [recognizer],
	defaultMessage: 'No good match in FAQ.',
	qnaThreshold: 0.5});
//</BasicQnAMakerDialog>

