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
				var option = $("<option>", { text: x.name });
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

			cards.forEach(function (x) {
				var tableRow = $("<tr>", {});
				var idColumn = $("<td>", { text: x.id });
				tableRow.append(idColumn);
				var nameColumn = $("<td>", { text: x.name });
				tableRow.append(nameColumn);
				$('#cardTable').append(tableRow);
			});
		}
	});
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

	$(document).ready(function () {
		$("#cardSearchBox").on("keyup", function () {
			var value = $(this).val().toLowerCase();
			$("#cardTable tr").filter(function () {
				$(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
			});
		});
	});
});