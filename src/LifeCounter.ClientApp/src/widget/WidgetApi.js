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
    const data = JSON.stringify({widgetId, page, lifeId});
    return navigator.sendBeacon(`${config.apiUrl}${PREFIX}alive`, data);
  }

  static stop(widgetId, page, lifeId) {
    const data = JSON.stringify({widgetId, page, lifeId});
    return navigator.sendBeacon(`${config.apiUrl}${PREFIX}stop`, data);
  }
}