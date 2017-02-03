$( document ).ready(function() {
	// If on mobile, scroll page to first header
	if( /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ) {
		var linkfromnav = '#navtitle';
		var urlhash = document.location.hash;
		if (urlhash == linkfromnav) {
			$('html, body').animate({
				scrollTop: $("h1").offset().top
			}, 1500);
		}
	}
    // open left nav container if a page is currently selected
    var currentNav = getClosestNavcontainer($(".page-link.navselected"));
    var isNodeRefDocVar = isNodeRefDoc();
    if (currentNav.length == 0 && isNodeRefDocVar == "") {
        // show all nodes
        $( ".level1.parent" ).show();
    } else {
        // hide top level links, show back to top
        //$(".level0").hide();
        $(".backToHome").show();
        if (isNodeRefDocVar == "chat") {
            var currentListHref = $('a[href*="/node/builder/chat-ref/"]').first();
            currentListHref.addClass("navselected");
            currentNav = getClosestNavcontainer(currentListHref);
        } 
        if (isNodeRefDocVar == "calling") {
            var currentListHref = $('a[href*="/node/builder/calling-ref/"]').first();
            currentListHref.addClass("navselected");
            currentNav = getClosestNavcontainer(currentListHref);
        } 
        //toggleNav(currentNav, 0);
    }
    
    // left nav toggle on top level container
    $( ".level1.parent" ).click(function() {
        //toggleNav($(this), 400);
    });

    var allTabsInPage = $('[id^="thetabs"]');
    activateAllTabs(allTabsInPage);

    $(".brand-primary").after('<div class="upgrade-message"><span>There\'s a new version of the Microsoft Bot Framework. Update your bot now to use cards, carousels and action buttons. </span><a href="https://aka.ms/bf-migrate"><span>Learn how</span></a></div>');
    
});

function getClosestNavcontainer(currentElement) {
    return currentElement.closest(".navContainer").prev();
}

function activateAllTabs(allTabsInPage) {
    $.each(allTabsInPage, function(i, val){
        $( "#"+ val.id ).tabs({
            active: localStorage.botFrameworkDocsActiveTab ? localStorage.botFrameworkDocsActiveTab : 0,
            activate: function(event, ui) {
                localStorage.setItem("botFrameworkDocsActiveTab", ui.newTab.parent().children().index(ui.newTab));
                activateAllTabs(allTabsInPage);
            }
        });
    });
}

function toggleNav(parent, dur) {    
    //$content = parent.children().first();
    $content = parent.next();
    $content.slideToggle(dur);
    parent.show();
    parent.toggleClass("rotate");
}

// 
function isNodeRefDoc() {
    var currentUrl = window.location.href;
    if (currentUrl.indexOf("/node/builder/chat-reference/") != -1) {
        return "chat";
    } 
    if (currentUrl.indexOf("/node/builder/calling-reference/") != -1) {
        return "calling";
    } 
    return "";
}

/*! Bing Search Helper v1.0.0 - requires jQuery v1.7.2 */
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

// this function is used in the C# docs too
function setProgrammingLanguage() {
    var storedLang = localStorage.botFrameworkDocsSearchLang ? localStorage.botFrameworkDocsSearchLang : '';
    $('#lang-select option[value="'+ storedLang +'"]').prop('selected', true);
}

$(function () {
    // $('#lang-select').remove();
    // $('#q').css('padding','3px 25px 3px 10px');
    
    var q = getParameterByName('q');
    var mkt = getParameterByName('mkt');
    var lang = getParameterByName('lang');
    var v = getParameterByName('v');
	var data = { q: q, mkt: mkt, v: v, lang: lang };

    setProgrammingLanguage();
    
    if (q) {
        $('#q').val(q);
        search(data);
    }

    // Attaches a click handler to the button.
    $('#bt_search').click(function (e) {
        q = $('#q').val();
        if (q) {
            lang = $('#lang-select').find('option:selected').val() ? $('#lang-select').find('option:selected').val() : '';
            localStorage.setItem("botFrameworkDocsSearchLang", lang);
            var formaction = $('#docs-search-form').attr('action');

            if (!formaction) {
                e.preventDefault();
                // Clear the results div.
                $('#search-results').empty();
                mkt = $('#mkt').val() ? $('#mkt').val() : '';
                v = $('#v').val() ? $('#v').val() : '';
                data = { q: q, mkt: mkt, v: v, lang: lang };
                updateAddressBar(data);
                search(data);
            } 
        } else {
            e.preventDefault();
        }
    });

    // Performs the search.
    function search(data) {
        // Set the page title
        $('.post-title').text('Search results for \'' + data["q"] + '\'');
        $('#search-progress').addClass("loading");
        // Establish the data to pass to the proxy.
        var host = 'https://dev.botframework.com/api/docssearch';
        // Calls the proxy, passing the query, service operation and market.
        $.ajax({
            url: host,
            type: 'GET',
            dataType: 'json',
            data: data,
            success: function(obj) {
                if (obj.webPages !== undefined) {
                    var items = obj.webPages.value;
                    if (items.length > 0) {
                        for (var k = 0, len = items.length; k < len; k++) {
                            var item = items[k];
                            showWebResult(item);
                        }
                    } 
                } else {
                    $('#search-results').html('no results');
                }
            },
            error: function() {
               $('#search-results').html('no results');
            },
            complete: function() {
                $('#search-progress').removeClass("loading");
            }
        });
    }

    // Shows one item of Web result.
    function showWebResult(item) {
        var container = document.createElement('div');
        $(container).addClass('search-result-item');
        var p = document.createElement('p');
        var a = document.createElement('a');
        var pp = document.createElement('p');
        a.href = item.url;
        $(a).append(item.name);
        $(p).append(item.snippet);
        $(pp).append(item.displayUrl);
        $(pp).addClass('bingSearchUrl');
        $(container).append(a, pp, p);
        $('#search-results').append(container);
    }

    function updateAddressBar(data) {
        window.history.pushState("", "", "?q=" + data["q"] + "&mkt=" + data["mkt"] + "&v=" + data["v"] + "&lang=" + data["lang"]);
    }
});