import {WidgetApi} from "./WidgetApi";
import {config} from "./config";
import {initializeHandlers} from "./handlers";

initialize(config.widgetId);

function getPage() {
  const search = document.location.search;
  return document.location.pathname + (!!search ? `?${search}` : '');
}

function initialize(widgetId) {
  const page = getPage();
  WidgetApi.start(widgetId, page)
    .then(({lifeId}) => {
      if (lifeId) {
        initializeHandlers(widgetId, page, lifeId);
      }
    })
}