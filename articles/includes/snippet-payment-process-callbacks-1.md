When receiving a Shipping Address Update or a Shipping Option Update callback, 
your bot will be provided with the current state of the payment details from the client in the `Activity.Value` property.
As a merchant, you should treat these callbacks as static, given input payment details you will calculate some output payment details and 
fail if the input state provided by the client is invalid for any reason. 
If the bot determines the given information is valid as-is, simply send HTTP status code `200 OK` along with the unmodified payment 
details. Alternatively, the bot may send HTTP status code `200 OK` along with an updated payment details that should be applied before the order can be processed. 
In some cases, your bot may determine that the updated information is invalid and the 
order cannot be processed as-is. For example, the user's shipping address may specify a country to which the 
product supplier does not ship. In that case, the bot may send HTTP status code `200 OK` and a message populating the error property of the payment details object. 
Sending any HTTP status code in the `400` or `500` range to will result in a generic error  for the customer.
