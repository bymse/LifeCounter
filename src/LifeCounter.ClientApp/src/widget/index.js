import {config} from "./config";
import {Widget} from "./Widget";

const {onLoaded, props} = window.LifeStoreConfig || {};
const widget = window.LifeStoreWidget = new Widget(config.widgetId, config.alivePeriod, props);

if (onLoaded) {
  onLoaded(widget);
}