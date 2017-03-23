
//<routeMessage>
bot.dialog('/', BasicQnAMakerDialog);
//</routeMessage>




// <setMessageReceived>
// QnA Maker Dialogs

var recognizer = new cognitiveservices.QnAMakerRecognizer({
	knowledgeBaseId: 'set your kbid here', 
	subscriptionKey: 'set your subscription key here'});

var BasicQnAMakerDialog = new cognitiveservices.QnAMakerDialog({ 
	recognizers: [recognizer],
	defaultMessage: 'No good match in FAQ.',
	qnaThreshold: 0.5});
    //</setMessageReceived>

