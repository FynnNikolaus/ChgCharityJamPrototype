"use strict";
"use strict";

class Recievers{
	ReceivedCard(card, team) {
		var li = $("<li></li>").text(`${team} played ${card}`);
		$("#messagesList").append(li);
	};
}