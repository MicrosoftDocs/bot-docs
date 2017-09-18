// <setBasicProperties>
IMessageActivity message =  Activity.CreateMessageActivity();
message.Text = "Hello!";
message.TextFormat = "plain";
message.Locale = "en-Us";
// </setBasicProperties>

// <setMention>
var entity = new Entity();
entity.SetAs(new Mention()
{
    Text = "@johndoe",
    Mentioned = new ChannelAccount()
    {
        Name = "John Doe",
        Id = "UV341235"
    }
});
entities.Add(entity);
// </setMention>

// <setGeoCoord>
var entity = new Entity();
entity.SetAs(new Place()
{
    Geo = new GeoCoordinates()
    {
        Latitude = 32.4141,
        Longitude = 43.1123123,
    }
});
entities.Add(entity);
// </setGeoCoord>

// <examineEntity1>
if (entity.Type == "Place")
{
    dynamic place = entity.Properties;
    if (place.geo.latitude > 34)
        // do something
}
// </examineEntity1>

// <examineEntity2>
if (entity.Type == "Place")
{
    Place place = entity.GetAs<Place>();
    GeoCoordinates geo = place.Geo.ToObject<GeoCoordinates>();
    if (geo.Latitude > 34)
        // do something
}
// </examineEntity2>