import {WidgetApi} from "./WidgetApi";
import {config} from "./config";


export function initializeHandlers(widgetId, page, lifeId) {
  addAliveHandler(widgetId, page, lifeId);
  addPageCloseHandler(widgetId, page, lifeId);
}

let isHidden = false;

function addAliveHandler(widgetId, page, lifeId) {
  setInterval(
    () => {
      if(!isHidden) {
        WidgetApi.alive(widgetId, page, lifeId);
      }
    },
    config.alivePeriod
  );
}

function addPageCloseHandler(widgetId, page, lifeId) {
  document.addEventListener('visibilitychange', (e) => {
    if (document.visibilityState === 'hidden') {
      isHidden = true;
      WidgetApi.stop(widgetId, page, lifeId);
    } else {
      isHidden = false;
      WidgetApi.alive(widgetId, page, lifeId);
    }
  });
}