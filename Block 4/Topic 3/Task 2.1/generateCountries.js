const COUNTRIES = new Map([
	['Россия', ['Москва', 'Санкт-Петербург', 'Казань', 'Самара', 'Новосибирск']],
	['Германия', ['Берлин', 'Гамбург', 'Франкфурт-на-Майне']],
	['Италия', ['Рим', 'Флоренция', 'Венеция']],
	['Франция', ['Париж', 'Марсель', 'Лион']],
	['Испания', ['Мадрид', 'Барселона', 'Валенсия', 'Севилья']]
]);

window.addEventListener('load', () => {
	const countrySelect = document.getElementById('country');
	const citiesSelect = document.getElementById('city');
	updateSelect(countrySelect, COUNTRIES.keys());

	countrySelect.addEventListener('change', (e) => {
		updateSelect(citiesSelect, COUNTRIES.get(countrySelect.value).values());

	});
});

function updateSelect(select, options) {
	while (select.children.length > 1) {
		select.lastElementChild.remove();
	}
	[...options].forEach(element => {
		let option = document.createElement('option');
		option.textContent = element;
		select.append(option);
	});
	select.value = select.firstElementChild.value;
}