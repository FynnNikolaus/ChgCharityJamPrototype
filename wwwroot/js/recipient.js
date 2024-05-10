"use strict";

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/communicationHub").build();

    connection.on("ReceiveCard", function (card, team) {
		new Recievers().ReceivedCard(card, team);
    });

    connection.start().then(function () {
        console.log("SignalR UP");
    }).catch(function (err) {
        return console.error(err.toString());
    });
});
