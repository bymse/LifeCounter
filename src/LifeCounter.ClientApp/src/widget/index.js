import {WidgetApi} from "./WidgetApi";
import {config} from "./config";

initialize(config.widgetId);

function initialize(widgetId) {
  const page = getPage();
  WidgetApi.start(widgetId, page)
    .then(({lifeId}) => {
      if (lifeId) {
        addAliveHandler(widgetId, page, lifeId);
        addPageCloseHandler(widgetId, page, lifeId);
      }
    })
}

function addAliveHandler(widgetId, page, lifeId) {
  setTimeout(
    () => WidgetApi.alive(widgetId, page, lifeId),
    config.alivePeriod
  );
}

function addPageCloseHandler(widgetId, page, lifeId) {
  document.addEventListener('visibilitychange', () => {
    WidgetApi.stop(widgetId, page, lifeId);
  });
}

const getPage = () => `${document.location.pathname}?${document.location.search}`;