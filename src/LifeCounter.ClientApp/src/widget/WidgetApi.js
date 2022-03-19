const PREFIX = '/widget/api/v1/'

export class WidgetApi {
  static start(widgetId, page) {
    return fetch(`${PREFIX}start`, {
      method: 'POST',
      body: {widgetId, page}
    })
      .then(e => e.json())
      .catch(() => ({}));
  }

  static alive(widgetId, page, lifeId) {
    const data = JSON.stringify({widgetId, page, lifeId});
    return navigator.sendBeacon(`${PREFIX}alive`, data);
  }

  static stop(widgetId, page, lifeId) {
    const data = JSON.stringify({widgetId, page, lifeId});
    return navigator.sendBeacon(`${PREFIX}stop`, data);
  }
}