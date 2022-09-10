const attribute = 'data-confirmation';
const items = [...document.querySelectorAll(`[${attribute}]`)];
for (let item of items) {
  const message = item.getAttribute(attribute);
  item.addEventListener('click', (e) => {
    if (!confirm(message)) {
      e.preventDefault();
    }
  })
}