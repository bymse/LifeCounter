import {Widget} from "./Widget";


window.__LifeCounterWidgetRunner = async (config) => {
  const {onLoaded, props, initialize = true} = window.LifeCounterConfig || {};
  const widget = window.LifeStoreWidget = new Widget(config, props);

  if (onLoaded) {
    try {
      onLoaded(widget);
    } catch {

    }
  }

  if (initialize) {
    await widget.initialize();
  }
}