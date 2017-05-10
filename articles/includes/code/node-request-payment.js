// <requestPayment>
var bot = new builder.UniversalBot(connector, (session) => {

  catalog.getPromotedItem().then(product => {

    // Store userId for later, when reading relatedTo to resume dialog with the receipt.
    var cartId = product.id;
    session.conversationData[CartIdKey] = cartId;
    session.conversationData[cartId] = session.message.address.user.id;

    // Create PaymentRequest obj based on product information.
    var paymentRequest = createPaymentRequest(cartId, product);

    var buyCard = new builder.HeroCard(session)
      .title(product.name)
      .subtitle(util.format('%s %s', product.currency, product.price))
      .text(product.description)
      .images([
        new builder.CardImage(session).url(product.imageUrl)
      ])
      .buttons([
        new builder.CardAction(session)
          .title('Buy')
          .type(payments.PaymentActionType)
          .value(paymentRequest)
      ]);

    session.send(new builder.Message(session)
      .addAttachment(buyCard));
  });
});
// </requestPayment>    


// <processCallback>
connector.onInvoke((invoke, callback) => {
  console.log('onInvoke', invoke);

  // This is a temporary workaround for the issue that the channelId for "webchat" is mapped to "directline" in the incoming RelatesTo object
  invoke.relatesTo.channelId = invoke.relatesTo.channelId === 'directline' ? 'webchat' : invoke.relatesTo.channelId;

  var storageCtx = {
    address: invoke.relatesTo,
    persistConversationData: true,
    conversationId: invoke.relatesTo.conversation.id
  };

  connector.getData(storageCtx, (err, data) => {
    var cartId = data.conversationData[CartIdKey];
    if (!invoke.relatesTo.user && cartId) {
      // Bot keeps the userId in context.ConversationData[cartId]
      var userId = data.conversationData[cartId];
      invoke.relatesTo.useAuth = true;
      invoke.relatesTo.user = { id: userId };
    }

    // Continue based on PaymentRequest event.
    var paymentRequest = null;
    switch (invoke.name) {
      case payments.Operations.UpdateShippingAddressOperation:
      case payments.Operations.UpdateShippingOptionOperation:
        paymentRequest = invoke.value;

        // Validate address AND shipping method (if selected).
        checkout
          .validateAndCalculateDetails(paymentRequest, paymentRequest.shippingAddress, paymentRequest.shippingOption)
          .then(updatedPaymentRequest => {
            // Return new paymentRequest with updated details.
            callback(null, updatedPaymentRequest, 200);
          }).catch(err => {
            // Return error to onInvoke handler.
            callback(err);
            // Send error message back to user.
            bot.beginDialog(invoke.relatesTo, 'checkout_failed', {
              errorMessage: err.message
            });
          });

        break;

      case payments.Operations.PaymentCompleteOperation:
        var paymentRequestComplete = invoke.value;
        paymentRequest = paymentRequestComplete.paymentRequest;
        var paymentResponse = paymentRequestComplete.paymentResponse;

        // Validate address AND shipping method.
        checkout
          .validateAndCalculateDetails(paymentRequest, paymentResponse.shippingAddress, paymentResponse.shippingOption)
          .then(updatedPaymentRequest =>
            // Process payment.
            checkout
              .processPayment(updatedPaymentRequest, paymentResponse)
              .then(chargeResult => {
                // Return success.
                callback(null, { result: "success" }, 200);
                // Send receipt to user.
                bot.beginDialog(invoke.relatesTo, 'checkout_receipt', {
                  paymentRequest: updatedPaymentRequest,
                  chargeResult: chargeResult
                });
              })
          ).catch(err => {
            // Return error to onInvoke handler.
            callback(err);
            // Send error message back to user.
            bot.beginDialog(invoke.relatesTo, 'checkout_failed', {
              errorMessage: err.message
            });
          });

        break;
    }

  });
});
// </processCallback>
