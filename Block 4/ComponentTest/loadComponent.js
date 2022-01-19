import * as myElement from "./myElement/myElement.js";

customElements.define(myElement.tagName, await myElement.getClass('myElement'));