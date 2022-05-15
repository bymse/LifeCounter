import {WidgetApi} from "./WidgetApi";
import {config} from "./config";
import {initializeHandlers} from "./handlers";

const props = window.LifeStoreProperties || {};
initialize(config.widgetId, props);

function getPage() {
  const search = document.location.search;
  return document.location.pathname + (!!search ? `${search}` : '');
}

function initialize(widgetId, props) {
  const page = getPage();
  WidgetApi.start(widgetId, page, props)
    .then(({lifeId}) => {
      if (lifeId) {
        initializeHandlers(widgetId, page, lifeId, props);
      }
    })
}