import * as signalR from "@microsoft/signalr";

export class WidgetSignalrApi {
  #connection;
  
  constructor(baseUrl) {
    this.#connection = new signalR.HubConnectionBuilder()
      .withUrl(`${baseUrl}/widget/hub/life-counter`)
      .build();
  }
  
  start(widgetId, page, properties) {
    return this.#connection.start()
      .then(() => this.#connection.invoke('Start', {widgetId, page, properties}))
      .catch(() => {});
  }

  alive(widgetId, page, lifeId, properties) {
    return this.#connection.invoke('Alive', {widgetId, page, lifeId, properties});
  }

  stop(widgetId, page, lifeId) {
    return this.#connection.invoke('Stop', {widgetId, page, lifeId});
  }
}