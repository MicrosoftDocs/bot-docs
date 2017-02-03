function setFocused(text){
	localStorage.setItem("focused", text);
}

//URL Methods
function changeURL(channel, feature, example){
        window.location = "/en-us/channel-inspector/channels/" + channel + "#f=" + feature + "&e=" + example;
}

function channelChanged(select){
    localStorage.setItem("focused", "channels");
    changeURL(select.value, select.parentElement.elements["features"].value, "example1");
}

function featureChanged(select){
    localStorage.setItem("focused", "features");        
    changeURL(select.parentElement.elements["channels"].value, select.value, "example1");
}

function exampleChanged(example_name){
    localStorage.setItem("focused", "examples");
    changeURL(document.getElementById('channels').value, document.getElementById('features').value, example_name);
}

//URL Parameter Methods
function queryStringParams(querystring) {
    if (querystring === void 0) { querystring = document.location.search.substring(1); }
    if (querystring) {
        var pairs = querystring.split('&');
        for (var i = 0; i < pairs.length; i++) {
            var pair = pairs[i].split('=');
            this[pair[0]] = decodeURIComponent(pair[1]);
        }
    }        
}

//Defining page behavior using jQuery
function matrix(){
    var qp = new queryStringParams(window.location.hash.substring(1));        
    
	//Global variables
	var channel = $('#channel_name').text();
	/*
	alert("channel = " + channel)
	if(channel == "Web Chat"){
		channel = "Webchat";
	}
	*/
	
    var feature =  qp["f"];
    var example = qp["e"];

    //Validating Global Variables
    if(feature === undefined || feature === null || feature == ""){
        //Getting last_feature from localStorage 
		if(localStorage.getItem("last_feature") === null || localStorage.getItem("last_feature") === ""){
            feature = "Keyboards";
		}
        else{
            feature = localStorage.getItem("last_feature");
        }
        example = "example1";
    }

	//Channel-Feature variables
	var channel_feature = "#channel_" + channel + "_feature_" + feature;
	var examples = channel_feature + "_examples";
    var facts = channel_feature + "_facts";
	var current_example = channel_feature + "_current_example";
	var examples_num = channel_feature + "_examples_num";
	
	//Channel-Feature-Example variables
	var channel_feature_example = channel_feature + "_example_" + example;
	var channel_feature_no_example = channel_feature + "_example_example";
	var matrix_image_url = channel_feature_example + "_matrix-image-url"; 
	var matrix_description = channel_feature_example + "_matrix-description";	
	var samples = channel_feature_example + "_samples";
	var buttons = channel_feature_example + "_buttons";

	//Animation variables
	var max = parseInt($(examples_num).text());
	var index = parseInt($(current_example).text().substring(7));
	var play = channel_feature_example + "_play";
	var stop = channel_feature_example + "_stop";
	var forward = channel_feature_example + "_forward";
    var backward = channel_feature_example + "_backward";
	var timer;

	//New Design
	//Selecting (Coloring) current Channel and Feature from unorder lists
	$('#channels_ul').children("li#" + channel).children("a").attr("aria-checked", "true");
	$('#features_ul').children("li#" + feature).children("a").attr("aria-checked", "true");
	//Setting name of current Channel and Feature from unorder lists
	$('#select_channel').text(channel);
	$('#select_feature').text(feature);

    //Getting value from current example
    $(current_example).text(example);

    //Hide control divs 
    $('#channel_name').hide();
	$('.matrix-info').hide();
	$('.matrix-image-url').hide();
	$('.img-button').hide();
	$('.matrix-description').hide();
    $('.matrix-samples').hide();
	$('.matrix-info').hide();
    $('.buttons').hide();
    $('.button').css("display", "inline-block");

    //Showing div with feature
    $(channel_feature).show();

    //Showing div with example and facts    
    $(channel_feature_example).show();
	$(matrix_image_url).show();
	$(matrix_description).show();
    $(facts).show();
    $(samples).show();

    //Showing buttons if more than one image
    if(max > 1){
        $(buttons).show();

		$(matrix_image_url).mouseenter(function(){
			index = parseInt($(current_example).text().substring(7));
			$(channel_feature_no_example + index + "_backward").show(); 
			$(channel_feature_no_example + index + "_forward").show();
		}).mouseleave(function(){
			index = parseInt($(current_example).text().substring(7));
			$(channel_feature_no_example + index + "_backward").hide(); 
			$(channel_feature_no_example + index + "_forward").hide();
		});			
		
		//Setting paging dots 		
		for(ex = 1; ex <= max; ex++){
			var div_dots = "";
			
			for(i = 1; i <= max; i++){			
				if(ex == i){
					div_dots += "<div class='matrix-oval matrix-fill' id='example" + i + "'> </div>";
				}
				else{
					div_dots += "<div class='matrix-oval' id='example" + i + "'> </div>";
				}
			}
			$(channel_feature_no_example + ex +'_dots').html(div_dots);
		}

		$('.matrix-oval').click(function() {
			var last_ix = parseInt($(current_example).text().substring(7));
       		hideLastDivs(last_ix);
			var my_index = parseInt($(this).attr("id").substring(7));
			showNextDivs(my_index);
			setDivsWithIndex(my_index);
			setMouseOver(my_index);
 		});
    }
    else{
        $(buttons).hide();
    }

    //Setting localStorage for Menu - Channels Navigation
    localStorage.setItem("last_feature", feature);

	//Activating Control Buttons
	$('.img-button').click(function(){
		var img = $(this);
		var button_type = img.attr('class');
		var index = parseInt($(current_example).text().substring(7));		
		switch(button_type){
			case "img-button button-forward":							
				//Animating divs
				hideLastDivs(index);
				(index>=max)? index=1 : index++;
				showNextDivs(index);
				
				//Setting new variables
				setDivsWithIndex(index);
				setMouseOver(index);
				break;
			case "img-button button-backward":							
				//Animating divs
				hideLastDivs(index);
				(index <= 1)? index=max : index--;
				showNextDivs(index);
				
				//Setting new variables
				setDivsWithIndex(index);
				setMouseOver(index);
				break;                
		}        
	});
	
    function hideLastDivs(index){
		$(channel_feature_no_example + index + "_matrix-image-url").hide();
        $(channel_feature_no_example + index).hide();				
		$(channel_feature_no_example + index + "_matrix-description").hide();
        $(channel_feature_no_example + index + "_samples").hide();
        $(channel_feature_no_example + index + "_buttons").hide();            
    }

    function showNextDivs(index){
        $(channel_feature_no_example + index).show();
		$(channel_feature_no_example + index + "_matrix-image-url").fadeIn(1000, function(){}); 
		$(channel_feature_no_example + index + "_matrix-description").show();
        $(channel_feature_no_example + index + "_samples").show();
        $(channel_feature_no_example + index + "_buttons").show();		            
    }

	function setDivsWithIndex(index){
		$(current_example).text("example" + index);            
		play = channel_feature_no_example + index + "_play";
		stop = channel_feature_no_example + index + "_stop";
		forward = channel_feature_no_example + index + "_forward";
		backward = channel_feature_no_example + index + "_backward";		
	}

	function setMouseOver(index){
		$(channel_feature_no_example + index + "_matrix-image-url").mouseenter(function(){
			$(channel_feature_no_example + index + "_backward").show(); 
			$(channel_feature_no_example + index + "_forward").show();
		}).mouseleave(function(){
			$(channel_feature_no_example + index + "_backward").hide(); 
			$(channel_feature_no_example + index + "_forward").hide();
		});			
	}
}