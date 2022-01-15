document.addEventListener('DOMContentLoaded', () => {
	const columnCount = 5;
	const rowCount = 5;
	const highlightedClassName = 'highlighted';

	let table = generateTable(columnCount, rowCount, highlightedClassName);
	document.body.prepend(table);
});

function generateTable(columnCount, rowCount, highlightedClassName) {
	let table = document.createElement('table');
	for (let i of Array(+rowCount).keys()) {
		let tr = document.createElement('tr');

		for (let j of Array(+columnCount).keys()) {
			let td = document.createElement('td');
			td.textContent = `${j + 1}:${i + 1}`;
			if (i == j) {
				td.classList.add(highlightedClassName.toString());
			}
			tr.append(td);
		}
		table.append(tr);
	}

	return table;
}