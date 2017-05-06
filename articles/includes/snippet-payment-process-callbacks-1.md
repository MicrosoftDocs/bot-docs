In response to an Update Shipping Address callback or an Update Shipping Options callback, 
your bot may simply send HTTP status code `200 OK`, if it determines that the updated information is valid as-is. Alternatively, it may send HTTP status code `200 OK` along with a response body that specifies changes that should be 
applied before the order can be processed. 
In some cases, your bot may determine that the updated information is invalid and the 
order cannot be processed as-is. For example, the user's shipping address may specify a country to which the 
product supplier does not ship. In that case, the bot may 
respond with any HTTP status code in the `400` or `500` range to cancel the payment, and in doing so, 
may also specify a message (string) that will be displayed to the user in the browswer where they provided payment, shipping, and contact information. 