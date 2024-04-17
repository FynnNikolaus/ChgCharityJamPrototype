"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/communicationHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
}); 

connection.on("ReceiveCard", function (card, team) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${team} played ${card}`;
});

connection.start().then(function () {
    console.log("SignalR UP");
}).catch(function (err) {
    return console.error(err.toString());
});