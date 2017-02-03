function BotSendSignIn(session, builder){
	var signin_card = new builder.SigninCard(session)
	.text("Login to signin sample")
	.button("Signin", "https://login.live.com/login.srf?wa=wsignin1.0&ct=1464753247&rver=6.6.6556.0&wp=MBI_SSL&wreply=https:%2F%2Foutlook.live.com%2Fowa%2F&id=292841&CBCXT=out");

	var msg = new builder.Message(session)
	.attachments([signin_card]);

	session.send(msg);
}
