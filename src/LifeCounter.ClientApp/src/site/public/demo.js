let Widget = null;
window.DemoCallback = (e) => {
  Widget = e;
}

window.addEventListener('DOMContentLoaded', () => {
  const attribute = 'data-monitor-src';
  const iframe = document.querySelector(`[${attribute}]`);
  iframe.src = iframe.getAttribute(attribute);
});

const form = document.getElementById('number-form');
form.addEventListener('submit', e => {
  e.preventDefault();
  const number = document.getElementById('user-number').value;
  if (Widget) {
    Widget.setProps({number});
  }
});