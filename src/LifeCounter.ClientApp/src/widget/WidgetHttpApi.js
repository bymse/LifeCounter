const PREFIX = '/widget/api/v1/'

export class WidgetHttpApi {
  #baseUrl;
  
  constructor(baseUrl) {
    this.#baseUrl = baseUrl;
  }
  
  start(widgetId, page, properties) {
    return fetch(`${this.#baseUrl}${PREFIX}start`, {
      method: 'POST',
      body: JSON.stringify({widgetId, page, properties}),
      headers: {
        'Content-type': 'application/json'
      }
    })
      .then(e => e.json())
      .catch(() => ({}));
  }

  alive(widgetId, page, lifeId, properties) {
    return this.#sendBeacon('alive', {widgetId, page, lifeId, properties});
  }

  stop(widgetId, page, lifeId) {
    return this.#sendBeacon('stop', {widgetId, page, lifeId});
  }
  
  #sendBeacon(action, data) {
    const body = new Blob([JSON.stringify(data)], {type: 'application/json'} );
    return navigator.sendBeacon(`${this.#baseUrl}${PREFIX}${action}`, body);
  }
  
}