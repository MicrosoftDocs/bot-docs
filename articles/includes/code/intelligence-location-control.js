// <specifyingRequiredFields>
var options = {
    prompt: "Where should I ship your order? Type or say an address.",
    requiredFields:
        locationDialog.LocationRequiredFields.streetAddress |
        locationDialog.LocationRequiredFields.postalCode
}
locationDialog.getLocation(session, options);
// </specifyingRequiredFields>

// <handlingReturnedLocation>
locationDialog.create(bot);

bot.dialog("/", [
    function (session) {
        locationDialog.getLocation(session, {
            prompt: "Where should I ship your order? Type or say an address.",
            requiredFields:
                locationDialog.LocationRequiredFields.streetAddress |
                locationDialog.LocationRequiredFields.locality |
                locationDialog.LocationRequiredFields.region |
                locationDialog.LocationRequiredFields.postalCode |
                locationDialog.LocationRequiredFields.country
        });
    },
    function (session, results) {
        if (results.response) {
            var place = results.response;
            session.send(place.streetAddress + ", " + place.locality + ", " + place.region + ", " + place.country + " (" + place.postalCode + ")");
        }
        else {
            session.send("OK, I won't be shipping it");
        }
    }
]);
// </handlingReturnedLocation>
