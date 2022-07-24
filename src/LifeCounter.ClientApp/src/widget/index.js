import {Widget} from "./Widget";

export default async function (config) {
  const {onLoaded, props, initialize = true} = window.LifeStoreConfig || {};
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