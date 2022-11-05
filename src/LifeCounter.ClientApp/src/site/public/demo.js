let Widget = null;
window.DemoCallback = (e) => {
  Widget = e;
}

window.addEventListener('DOMContentLoaded', () => {
  const attribute = 'data-monitor-src';
  const iframe = document.querySelector(`[${attribute}]`);
  iframe.src = iframe.getAttribute(attribute);
});

let timeout = null;
const input = document.getElementById('number-input');
input.addEventListener('input', _ => {
  clearTimeout(timeout);
  timeout = setTimeout(() => {
    const number = input.value;
    if (Widget) {
      Widget.updateProps({number});
    }
  }, 200);
});