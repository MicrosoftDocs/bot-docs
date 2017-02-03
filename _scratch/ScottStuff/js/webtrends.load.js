// WebTrends SmartSource Data Collector Tag v10.4.23
// Copyright (c) 2016 Webtrends Inc.  All rights reserved.
// Tag Builder Version: 4.1.3.5
// Created: 2016.01.13
window.webtrendsAsyncInit=function(){
    var dcs=new Webtrends.dcs().init({
        dcsid:"dcs222o33tntg5tzotpzlm6qn_4r8h",
        domain:"statse.webtrendslive.com",
        timezone:-8,
        i18n:true,
        offsite:true,
        download:true,
        downloadtypes:"xls,doc,pdf,txt,csv,zip,docx,xlsx,rar,gzip",
        anchor:true,
        onsitedoms:"docs.botframework.com",
        fpcdom:".docs.botframework.com",
        plugins:{
            hm:{src:"//s.webtrends.com/js/webtrends.hm.js"}
        }
        }).track();
};
(function(){
    var s=document.createElement("script"); s.async=true; s.src="/js/webtrends.min.js";    
    var s2=document.getElementsByTagName("script")[0]; s2.parentNode.insertBefore(s,s2);
}());
