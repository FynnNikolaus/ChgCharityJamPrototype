"use strict";
"use strict";

class Recievers{
	ReceivedCard(card, team) {
		modal.fadeIn(300);
		modal.delay(6000).fadeOut(300)

		var li = $("<li></li>").text(`${team} played ${card}`);
		$("#messagesList").append(li);
	};
}