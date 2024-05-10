"use strict";
class effects
{
	applyEffect(effect, durationInSeconds) {
		// actually apply effect

		var timeoutDuration = durationInSeconds * 1000;
		setTimeout(function () {removeEffect(effect), timeoutDuration})
	}

	removeEffect(effect) {
		//actually remove effect
	}
}
