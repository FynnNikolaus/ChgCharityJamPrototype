/**
 * AjaxCall object to make a ajax request
 * @param {any} action the action method
 * @param {any} controller the controller
 * @param {any} updateObject the html object which should be updated
 * @param {any} model the data which should transport to action method 
 */
class AjaxCall {

	/**
	 * Initializes a new instance of Ajaxcall
	 * @param {any} action target action
	 * @param {any} controller target controller
	 */
	constructor(action, controller) {
		this._action = encodeURI(action);
		this._controller = controller;
		this._model = {};
		this._contentType = "application/x-www-form-urlencoded; charset=utf-8";
	}
	get action() { return this._action; }

	/**
	 * Controller
	 */
	get controller() { return this._controller; }
	get contentType() { return this._contentType; }

	/**
	 * What should happen on ajax succeed
	 */
	set successFunction(successFunction) { this._successFunction = successFunction; }
	get successFunction() { return this._successFunction; }

	/**
	 * What should happen on ajax error
	 */
	set errorFunction(errorFunction) { this._errorFunction = errorFunction; }
	get errorFunction() { return this._errorFunction; }

	/**
	 * sets the data for the given parametername in the model
	 * @param {any} parameterName name of the parameter
	 * @param {any} data data to send
	 */
	setModel(parameterName, data) {
		if (typeof data === "object") {
			data = AjaxCall.setIsoStringsToDatetimeObjects(data);
		}

		this._model[parameterName] = data;
	}

	/**
	 * Iterates thru the given obj and checks each property if it is an property of type Date.
	 * If it is a date property it gets translated to a ISO date string, otherwise the ASP.NET controller
	 * can not handle the date property.
	 * @param {any} obj
	 */
	static setIsoStringsToDatetimeObjects(obj) {
		Object.keys(obj).forEach(function (key) {
			if (Object.prototype.toString.call(obj[key]) === '[object Array]') {
				var i, n = obj[key].length;
				for (i = 0; i < n; ++i) {
					obj[key][i] = AjaxCall.setIsoStringsToDatetimeObjects(obj[key][i]);
				}
			}
			if (Object.prototype.toString.call(obj[key]) === '[object Object]') {
				obj[key] = AjaxCall.setIsoStringsToDatetimeObjects(obj[key]);
			}
			if (Object.prototype.toString.call(obj[key]) === '[object Date]') {
				obj[key] = obj[key].toISOString();
			}
		});

		return obj;
	}

	/**
	* the ajax request
	* @returns {} http response
	*/
	call(httpVerb) {
		$.ajax({
			url: window.ROOT_URL + this.controller + "/" + this.action,
			cache: false,
			type: httpVerb,
			dataType: this.dataType,
			contentType: this.contentType,
			data: this.model,
			error: this.errorFunction,
			async: true,
			success: this.successFunction
		});
	}

	/**
	 * Makes an ajax call with json as body content 
	 * @param {any} httpVerb The target http verb (PUT, POST, DELETE, PATCH)
	 * @param {any} model The model to send
	 */
	sendJson(httpVerb, model) {
		this.contentType = "application/json; charset=utf-8";

		if (typeof model !== 'string') {
			this._model = JSON.stringify(model);
		} else {
			this._model = model;
		}

		this.call(httpVerb);
	}
}