"use strict";

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/communicationHub").build();

    connection.on("ReceiveCard", function (card, team) {
        var li = $("<li></li>").text(`${team} played ${card}`);
        $("#messagesList").append(li);
    });

    connection.start().then(function () {
        console.log("SignalR UP");
    }).catch(function (err) {
        return console.error(err.toString());
    });
});
