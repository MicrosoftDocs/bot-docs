function BotSendTyping(session, builder){
	function delaySend(index) {
		setTimeout(function () {
			if(index < 3){
				if(index == 0){
					var msg = new builder.Message(session)
					.text("");
					session.sendTyping();
					session.send(msg);
				}
				else if(index == 1){
					var msg = new builder.Message(session)
					.text("");
					session.sendTyping();
					session.send(msg);
				}
				else if(index == 2){
					var msg = new builder.Message(session)
					.text("My robot must rate as my favorite toy,  A wonderful, whirring, mechanical joy.  My robot can talk, but he'd much rather sing,  Or go to the park and play on the swings!");
					session.send(msg);					
				}
				delaySend(index + 1);
			}
			else{
				//Do something
			}
		}, 2000);		
	}
	delaySend(0);	
}
