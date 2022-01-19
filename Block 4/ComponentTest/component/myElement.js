const tagName = 'my-element';

async function getClass(path) {
	if (!/\/$/.test(path)) {
		path += '/';
	}

	var root = document.createElement('div');
	root.innerHTML = await (await fetch(path + '/myElement.html')).text();

	let template = root.querySelector('template');
	let cssLinks = root.querySelectorAll('link[rel="stylesheet"]');

	return class extends HTMLElement {
		constructor() {
			super();
			let templateContent = template.content;

			const shadowRoot = this.attachShadow({ mode: 'open' });

			// let link = document.createElement('link');
			// link.setAttribute('rel', 'stylesheet');
			// link.setAttribute('href', path + '/myElement.css');
			// shadowRoot.appendChild(link);

			cssLinks.forEach(cssLink => {
				cssLink.setAttribute('href', path + cssLink.getAttribute('href'));
				shadowRoot.appendChild(cssLink);
			});

			shadowRoot.appendChild(templateContent.cloneNode(true));
		}
	}
};


export { tagName, getClass };