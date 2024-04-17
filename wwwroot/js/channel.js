"use strict";

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/communicationHub").build();

    connection.start().then(function () {
        console.log('Signal Run');
    }).catch(function (err) {
        return console.error(err.toString());
    });

    function sendCard() {
        var card = $("#cards").val();
        var team = "TeamX";

        connection.invoke("PlayCard", card, team).catch(function (err) {
            return console.error(err.toString());
        });
    }

    $(".sendCard").click(function () {
        sendCard();
    });
});
