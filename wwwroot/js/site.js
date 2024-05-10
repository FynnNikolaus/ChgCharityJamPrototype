// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/// Backend JavaScript
function submitAddTeamForm()
{
	event.preventDefault();
	$.ajax({
		url: "/addTeam",
		type: "post",
		data: $("#addTeamForm").serialize(),
		success: function () {
			$('#addTeamForm').trigger("reset");
			loadTeams();
		}
	});
}

function loadTeams()
{
	$('#teamSelection option').remove();

	$.ajax({
		url: "BackendBoard/teams",
		type: "get",
		success: function (teams) {

			teams.forEach(function (x) {
				var option = $("<option>", { text: x.teamData.Name });
				$('#teamSelection').append(option);
			});
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
				addRow(table, data);
			});
		}
	});
}

function addRow(table, data) {
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
}

function addColumn(row, columnValue) {
	var column = $("<td>", { text: columnValue });
	row.append(column);
}

function deleteTeam() {
	$.ajax({
		url: "BackendBoard/DeleteTeam?team=" + getSelectedTeam(), type: "DELETE", success: function ()
		{
			loadTeams();
	    }
	});
}

// initialize teams and stuff idk
$(document).ready(function() {
	loadTeams();
	loadCards();

	$('#cardTable').on('click', 'tbody tr', function (event) {
		$(this).addClass('highlight').siblings().removeClass('highlight');
	});

	$("#cardSearchBox").on("keyup", function () {
		var value = $(this).val().toLowerCase();
		$("#cardTable tr").filter(function () {
			$(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
		});
	});
});