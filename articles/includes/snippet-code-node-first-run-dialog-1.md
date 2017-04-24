```javascript 
// Add first run dialog
bot.dialog('firstRun', function (session) {    
    session.userData.firstRun = true;
    session.send("Hello...").endDialog();
}).triggerAction({
    onFindAction: function (context, callback) {
        // Only trigger if we've never seen user before
        if (!context.userData.firstRun) {
            // Return a score of 1.1 to ensure the first run dialog wins
            callback(null, 1.1);
        } else {
            callback(null, 0.0);
        }
    }
});

```