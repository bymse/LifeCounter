import appModel from "./appModel";

if (!appModel) {
  throw new Error();
}

const connection = new signalR.HubConnectionBuilder()
  .withUrl('/monitor/lives')
  .build();

connection.start()
  .then(() => {
    initialize()
  });

connection.on('Update', handleMessage);

function initialize() {
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