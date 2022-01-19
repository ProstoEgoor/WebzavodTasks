import * as myElement from "./component/myElement.js";

customElements.define(myElement.tagName, await myElement.getClass('component'));