function BotSendCodeSample(session){
	var codesample = "```javascript\n"
            		+"function fancyAlert(arg) {\n"
            		+"  if(arg) {\n"
            		+"    $.facebox({div:'#foo'})\n"
            		+"  }\n"
            		+"}\n"
            		+"```";
	session.send(codesample);
}
