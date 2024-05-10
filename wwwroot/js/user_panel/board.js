var connection = new signalR.HubConnectionBuilder()
	.withUrl("/communicationHub")
	.build();

connection.on("UpdateGameStatus", function (gameStatus) {
	console.log("Spielstatus aktualisiert:", gameStatus);
	updateUI(gameStatus);
});

connection.start().then(function () {
	console.log("Verbunden mit SignalR.");
}).catch(function (err) {
	console.error(err.toString());
});

function updateUI(gameStatus) {
	$("#game-status").empty();
	gameStatus.Teams.forEach(function (team) {
		$("#game-status").append("<div>Team " + team.Id + ": " + team.Name + "</div>");
	});
	$("#uptime").text("Betriebszeit: " + gameStatus.Uptime);
}
