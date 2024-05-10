class Senders
{
	constructor(connection) {
		this._connection = connection;
	}

	sendCard() {
		var card = $("#cards").val();
		var team = "TeamX";

		this._connection.invoke("PlayCard", card, team).catch(function (err) {
			return console.error(err.toString());
	});
}
}
