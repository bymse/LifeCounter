import appModel from "./appModel";

if (!appModel) {
  throw new Error();
}

const connection = new signalR.HubConnectionBuilder()
  .withUrl('/monitor/life-updates')
  .build();

startConnection()
  .then(() => handleEnableUpdatesChange());

connection.on('Update', handleMessage);

function startConnection() {
  return connection.start()
    .then(() => {
      initializeDashboard();
    });
}

function initializeDashboard() {
  const {widgetId, page} = appModel;
  connection.invoke('Start', widgetId, page)
    .catch(e => {
      console.error(e);
    })
  ;
}

function handleMessage(html) {
  const table = document.getElementById('lives-table');
  const newTable = document.createElement('div');
  newTable.innerHTML = html;
  table.replaceWith(newTable.children[0]);
}

function handleEnableUpdatesChange() {
  const checkbox = document.getElementById('enable-updates');
  checkbox.addEventListener('change', (e) => {
    if (e.target.checked) {
      startConnection();
    } else {
      connection.stop();
    }
  })
}