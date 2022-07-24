import {WidgetHttpApi} from "./WidgetHttpApi";
import {WidgetSignalrApi} from "./WidgetSignlarApi";

export class Widget {

  constructor({widgetId, alivePeriod, apiUrl, transportType}, baseProps) {
    this.widgetId = widgetId;
    this.props = baseProps;
    this.alivePeriod = alivePeriod;
    this.aliveInterval = null;
    this.isActive = false;
    this.lifeId = null;
    this.currentPage = null;
    this.api = Widget.#getApi(transportType, apiUrl);
    this.#handleVisibilityChange = this.#handleVisibilityChange.bind(this);
  }

  static #getApi(transportType, apiUrl) {
    switch (transportType) {
      case 'http':
        return new WidgetHttpApi(apiUrl);
      case 'signalr' :
        return new WidgetSignalrApi(apiUrl);
      default:
        throw new Error();
    }
  }

  initialize() {
    this.currentPage = Widget.#getPage();
    return this.api
      .start(this.widgetId, this.currentPage, this.props)
      .then(({lifeId}) => {
        if (lifeId) {
          this.lifeId = lifeId;
          this.isActive = true;
          this.#initializeHandlers();
        }
        return this.isActive;
      })
  }

  updateProps(props) {
    clearInterval(this.aliveInterval);
    this.props = props;
    this.#alive();
    this.#setAliveInterval();
  }

  #initializeHandlers() {
    this.#setAliveInterval();
    document.addEventListener('visibilitychange', this.#handleVisibilityChange);
  }

  #setAliveInterval() {
    this.aliveInterval = setInterval(() => this.isActive && this.#alive(), this.alivePeriod);
  }

  #handleVisibilityChange = () => {
    if (document.visibilityState === 'hidden') {
      this.isActive = false;
      this.#stopLife();
    } else {
      this.isActive = true;
      this.#alive();
    }
  }

  #stopLife() {
    this.api.stop(this.widgetId, this.currentPage, this.lifeId);
  }

  #alive() {
    this.api.alive(this.widgetId, this.currentPage, this.lifeId, this.props);
  }

  static #getPage() {
    const search = document.location.search;
    return document.location.pathname + (!!search ? `${search}` : '');
  }
}