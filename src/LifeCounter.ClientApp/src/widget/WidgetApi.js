import {config} from './config'
const PREFIX = '/widget/api/v1/'

export class WidgetApi {
  static start(widgetId, page) {
    return fetch(`${config.apiUrl}${PREFIX}start`, {
      method: 'POST',
      body: JSON.stringify({widgetId, page}),
      headers: {
        'Content-type': 'application/json'
      }
    })
      .then(e => e.json())
      .catch(() => ({}));
  }

  static alive(widgetId, page, lifeId) {
    return WidgetApi.#sendBeacon('alive', {widgetId, page, lifeId});
  }

  static stop(widgetId, page, lifeId) {
    return WidgetApi.#sendBeacon('stop', {widgetId, page, lifeId});
  }
  
  static #sendBeacon(action, data) {
    const body = new Blob([JSON.stringify(data)], {type: 'application/json'} );
    return navigator.sendBeacon(`${config.apiUrl}${PREFIX}${action}`, body);
  }
  
}