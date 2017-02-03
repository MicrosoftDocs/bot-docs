public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	GeoCoordinates location = new GeoCoordinates();
	location.Latitude = 47.64451613;
	location.Longitude = -122.13681221;
	location.Name = "Microsoft - The Commons";
	location.Elevation = 113.531;
	location.Type = "GeoCoordinates";

	Place place = new Place();
	place.Geo = location;
	place.Name = "Microsoft - The Commons";
	place.Type = "Place";

	Entity entity = new Entity();
	entity.SetAs(place);

	var reply = activity.CreateReply($"Here it is a location: ");
	reply.Entities.Add(entity);
	await SendResponse(activityContext, reply).DropContext();
}
