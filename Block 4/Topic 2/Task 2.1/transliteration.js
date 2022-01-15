const LOWER_LETTERS = ['a', 'b', 'v', 'g', 'd', 'e', 'ž', 'z', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'f', 'h', 'c', 'č', 'š', 'ŝ', 'ʺ', 'y', 'ʹ', 'è', 'û', 'â', 'ë'];
const UPPER_LETTERS = ['A', 'B', 'V', 'G', 'D', 'E', 'Ž', 'Z', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'F', 'H', 'C', 'Č', 'Š', 'Ŝ', 'ʺ', 'Y', 'ʹ', 'È', 'Û', 'Â', 'Ë'];

window.addEventListener('load', () => {
	const form = document.getElementById('main-form');
	const wordCountSpan = document.getElementById('word-count');
	const letterCountSpan = document.getElementById('letter-count');

	form.addEventListener('submit', (e) => {
		e.preventDefault();
		e.stopPropagation();
		let ruText = e.target['ru-text'].value;
		e.target['eng-text'].value = translit(ruText);

		wordCountSpan.textContent = wordCount(ruText);
		letterCountSpan.textContent = ruText.length;
	});
});

function translit(text) {
	let translitText = '';
	for (let i = 0; i < text.length; i++) {
		translitText += getTranslitLetter(text[i]);
	}

	return translitText;
}

function wordCount(text) {
	return text.match(/([\wА-Яа-яЁё]+)/g).length;
}

function getTranslitLetter(letter) {
	if ('а'.charCodeAt(0) <= letter.charCodeAt(0) && letter.charCodeAt(0) <= 'я'.charCodeAt(0)) {
		return LOWER_LETTERS[letter.charCodeAt(0) - 'а'.charCodeAt(0)];
	}
	if ('ё'.charCodeAt(0) == letter.charCodeAt(0)) {
		return LOWER_LETTERS[LOWER_LETTERS.length - 1];
	}

	if ('А'.charCodeAt(0) <= letter.charCodeAt(0) && letter.charCodeAt(0) <= 'Я'.charCodeAt(0)) {
		return UPPER_LETTERS[letter.charCodeAt(0) - 'А'.charCodeAt(0)];
	}
	if ('Ё'.charCodeAt(0) == letter.charCodeAt(0)) {
		return UPPER_LETTERS[UPPER_LETTERS.length - 1];
	}

	return letter;
}