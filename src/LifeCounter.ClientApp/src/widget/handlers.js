import {WidgetApi} from "./WidgetApi";
import {config} from "./config";


export function initializeHandlers(widgetId, page, lifeId, props) {
  addAliveHandler(widgetId, page, lifeId, props);
  addPageCloseHandler(widgetId, page, lifeId, props);
}

let isHidden = false;

function addAliveHandler(widgetId, page, lifeId, props) {
  setInterval(
    () => {
      if(!isHidden) {
        WidgetApi.alive(widgetId, page, lifeId, props);
      }
    },
    config.alivePeriod
  );
}

function addPageCloseHandler(widgetId, page, lifeId, props) {
  document.addEventListener('visibilitychange', (e) => {
    if (document.visibilityState === 'hidden') {
      isHidden = true;
      WidgetApi.stop(widgetId, page, lifeId);
    } else {
      isHidden = false;
      WidgetApi.alive(widgetId, page, lifeId, props);
    }
  });
}