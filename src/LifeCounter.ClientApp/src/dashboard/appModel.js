let appModel;

try {
  const element = document.getElementById('app-model');
  appModel = JSON.parse(element.textContent);
}
catch (e) {
  console.error(e);
  appModel = null;
}

export default appModel;