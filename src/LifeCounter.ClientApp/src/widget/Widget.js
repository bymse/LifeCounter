import {WidgetApi} from "./WidgetApi";

export class Widget {

  constructor(widgetId, alivePeriod, baseProps) {
    this.widgetId = widgetId;
    this.props = baseProps;
    this.alivePeriod = alivePeriod;
    this.aliveInterval = null;
    this.isActive = false;
    this.lifeId = null;
    this.currentPage = null;
    this.#handleVisibilityChange = this.#handleVisibilityChange.bind(this);
  }

  initialize() {
    this.currentPage = Widget.#getPage();
    return WidgetApi
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

  setProps(props) {
    this.props = props;
  }

  #initializeHandlers() {
    this.aliveInterval = setInterval(() => this.isActive && this.#alive(), this.alivePeriod);
    document.addEventListener('visibilitychange', this.#handleVisibilityChange);
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

  #stopLife(){
    WidgetApi.stop(this.widgetId, this.currentPage, this.lifeId);
  }
  
  #alive() {
    WidgetApi.alive(this.widgetId, this.currentPage, this.lifeId, this.props);
  }

  static #getPage() {
    const search = document.location.search;
    return document.location.pathname + (!!search ? `${search}` : '');
  }
}