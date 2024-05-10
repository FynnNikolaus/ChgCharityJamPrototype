function submitAddTeamForm()
{
	event.preventDefault();
	$.ajax({
		url: "/addTeam",
		type: "post",
		data: $("#addTeamForm").serialize(),
		success: function () {
			$('#addTeamForm').trigger("reset");
		}
	});
}

function displayTeams(teams) {
	var table = $("#teamTable");

	teams.forEach(function (data) {
		var tableRow = $("<tr>", {});
		addColumn(tableRow, data.id);
		addColumn(tableRow, data.name);
		addColumn(tableRow, data.balance);
		addColumn(tableRow, data.workspace);
		table.append(tableRow);
	});
}


function loadTeams()
{
	$.ajax({
		url: "BackendBoard/teams",
		type: "get",
		success: function (teams) {
			displayTeams(teams);
		}
	});
}

function getSelectedTeam() {
	return $('#teamSelection :selected').text();
}


function loadCards() {
	$('#cardTable tr').remove();

	$.ajax({
		url: "BackendBoard/cards",
		type: "get",
		success: function (cards) {

			var table = $("#cardTable");

			cards.forEach(function (data) {
				var tableRow = $("<tr>", {});
				addColumn(tableRow, data.id);
				addColumn(tableRow, data.name);
				addColumn(tableRow, data.type);
				addColumn(tableRow, data.activation);
				addColumn(tableRow, data.duration);
				addColumn(tableRow, data.price);
				addColumn(tableRow, data.ttsPrimaryDesc);
				addColumn(tableRow, data.ttsSecondaryDesc);
				table.append(tableRow);
			});
		}
	});
}

function addColumn(row, columnValue) {
	var column = $("<td>", { text: columnValue });
	row.append(column);
}

function deleteTeam() {
	$.ajax({
		url: "BackendBoard/DeleteTeam?team=" + getSelectedTeam(), type: "DELETE" });
}


function setupSignalR() {
	var connection = new signalR.HubConnectionBuilder().withUrl("/communicationHub").build();

	connection.on("UpdateGameStatus", function (gameStatus) {
		displayTeams(gameStatus.teams);
	});

	connection.start().then(function () {
		console.log("SignalR UP");
	}).catch(function (err) {
		return console.error(err.toString());
	});
}


// initialize teams and stuff idk
$(document).ready(function() {
	setupSignalR();

	loadTeams();
	loadCards();

	$('#teamTable').on('click', 'tr', function (event) {
		if ($(this).attr("id") == "selected") {
			$(this).removeClass('bg-info');
			$(this).removeAttr('id');
			return;
		}
		$(this).addClass('bg-info').siblings().removeClass('bg-info');
		$(this).attr("id", "selected").siblings().removeAttr('id');
	});

	$('#cardTable').on('click', 'tr', function (event) {
		$(this).addClass('bg-info border-0').siblings().removeClass('bg-info border-0');
		$(this).attr("id", "selected").siblings().removeAttr('id');
	});

	$("#cardSearchBox").on("keyup", function () {
		var value = $(this).val().toLowerCase();
		$("#cardTable tr").filter(function () {
			$(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
		});
	});
});