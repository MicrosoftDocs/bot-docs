// <receiveMessage>
bot.dialog('/', function (session) {
    var queuedMessage = { address: session.message.address, text: session.message.text };
    session.sendTyping();
    var queueSvc = azure.createQueueService(process.env.AzureWebJobsStorage);
    queueSvc.createQueueIfNotExists('bot-queue', function(err, result, response){
        if(!err){
            var queueMessageBuffer = new Buffer(JSON.stringify(queuedMessage)).toString('base64');
            queueSvc.createMessage('bot-queue', queueMessageBuffer, function(err, result, response){
                if(!err){
                    session.send('Your message (\'' + session.message.text + '\') has been added to a queue, and it will be sent back to you via a Function');
                } else {
                    session.send('There was an error inserting your message into queue');
                }
            });
        } else {
            session.send('There was an error creating your queue');
        }
    });
});
// </receiveMessage>



// <queueTrigger>
module.exports = function (context, myQueueItem) {
    context.log('Sending Bot message', myQueueItem);

    var message = {
        'text': myQueueItem.text,
        'address': myQueueItem.address
    };

    context.done(null, message);
}
// </queueTrigger>



// <handleMessageFromFunction>
bot.on('trigger', function (message) {
    // handle message from trigger function
    var queuedMessage = message.value;
    var reply = new builder.Message()
        .address(queuedMessage.address)
        .text('This is coming from the trigger: ' + queuedMessage.text);
    bot.send(reply);
});
// </handleMessageFromFunction>
