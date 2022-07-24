import {config} from "./config";
import {TransportType} from "./TransportType";

const {transportType, signalrUrl, widgetUrl} = config;
let promise;
if (transportType === TransportType.SignalR) {
  promise = loadScript(signalrUrl);
}

promise = promise ?? new Promise(resolve => resolve());

promise
  .then(() => loadScript(widgetUrl))
  .then(() => window.__LifeCounterWidgetRunner(config));

function loadScript(url) {
  return new Promise((resolve, reject) => {
    const script = document.createElement('script');
    script.src = url;
    script.onload = resolve;
    script.onerror = reject;
    document.body.appendChild(script);
  });
}