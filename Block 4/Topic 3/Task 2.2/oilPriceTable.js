const UP_ARROW_ICON = 'fas fa-long-arrow-alt-up text-success';
const DOWN_ARROW_ICON = 'fas fa-long-arrow-alt-down text-danger';

const mockData = {
	"success": true,
	"timeseries": true,
	"start_date": "2021-12-15",
	"end_date": "2022-01-15",
	"base": "USD",
	"rates": {
		"2022-01-01": {
			"USD": 1,
			"BRENTOIL": 0.012858061198251
		},
		"2022-01-02": {
			"USD": 1,
			"BRENTOIL": 0.012867446644382
		},
		"2022-01-03": {
			"USD": 1,
			"BRENTOIL": 0.012767738642164
		},
		"2022-01-04": {
			"USD": 1,
			"BRENTOIL": 0.0126643454039
		},
		"2022-01-05": {
			"USD": 1,
			"BRENTOIL": 0.012510875
		},
		"2022-01-06": {
			"USD": 1,
			"BRENTOIL": 0.012465977068794
		},
		"2022-01-07": {
			"USD": 1,
			"BRENTOIL": 0.012202000487924
		},
		"2022-01-09": {
			"USD": 1,
			"BRENTOIL": 0.012236819571865
		},
		"2022-01-10": {
			"USD": 1,
			"BRENTOIL": 0.012278944137508
		},
		"2022-01-11": {
			"USD": 1,
			"BRENTOIL": 0.01236737974527
		},
		"2022-01-12": {
			"USD": 1,
			"BRENTOIL": 0.011955566172957
		},
		"2022-01-13": {
			"USD": 1,
			"BRENTOIL": 0.01181492854612
		},
		"2022-01-14": {
			"USD": 1,
			"BRENTOIL": 0.011896326239448
		},
		"2022-01-15": {
			"USD": 1,
			"BRENTOIL": 0.011624215663491
		}
	},
	"unit": "per barrel"
}

window.addEventListener('load', async () => {
	const table = document.getElementById('table');
	const button = document.getElementById('update-button');
	const streak = document.getElementById('streak-place');

	let data = await loadData();
	let formatedData = getFormatedData(data);
	updateTable(table, formatedData);
	updateStreak(streak, formatedData);

	button.addEventListener('click', async (e) => {
		e.stopPropagation();
		dropTable(table);
		data = await loadData();
		formatedData = getFormatedData(data);
		updateTable(table, formatedData);
		updateStreak(streak, formatedData);
	});
});

async function loadData() {
	let endDate = new Date();
	let startDate = new Date(endDate);

	endDate.setDate(endDate.getDate() - 1);
	startDate.setMonth(startDate.getMonth() - 1);

	endDate = formatDateUTC(endDate);
	startDate = formatDateUTC(startDate);

	let queryParams = new URLSearchParams({
		access_key: '1804x4u66cv46vgmpgi4h64keegw76pu9qg7t49tvi3ebwc8cn628lk7h32a',
		start_date: startDate,
		end_date: endDate,
		base: 'USD',
		symbols: 'BRENTOIL'
	});

	let newData = mockData;

	try {
		const response = await fetch('https://commodities-api.com/api/timeseries?' + queryParams.toString());
		if (!response.ok) {
			throw Error(response.statusText);
		}
		const result = await response.json();
		if (result.data.error) {
			throw Error(JSON.stringify(result.data.error));
		}

		newData = result.data;
	} catch (err) {
		console.log('Не получилось загрузить данные. Используются mock данные.', err.message);
	}

	console.log('Загруженные данные:', newData);
	return newData;
}

function getFormatedData(data) {
	let rows = [];

	let increaseCount = 0;
	let decreaseCount = 0;

	for (const i in data.rates) {
		let row = { date: i, price: formatNumber(1 / data.rates[i]['BRENTOIL']) };
		row.priceIncrease = '-';
		row.chainIndex = '-';
		row.priceIncreaseRate = '-';
		row.priceIncreaseIcon = { value: '-' };

		if (rows.length) {
			let prevRow = rows[rows.length - 1];
			row.priceIncrease = formatNumber(row.price - prevRow.price);
			if (row.priceIncrease > 0) {
				increaseCount++;
				row.priceIncreaseIcon = { icon: UP_ARROW_ICON };
			} else if (row.priceIncrease < 0) {
				decreaseCount++;
				row.priceIncreaseIcon = { icon: DOWN_ARROW_ICON };
			}
			row.chainIndex = formatNumber(row.price / prevRow.price * 100);
			row.priceIncreaseRate = formatNumber(row.chainIndex - 100);
		}

		rows.push(row);
	}

	let total = { date: 'Итог', priceIncreaseIcon: '' };
	let minPrice = rows[0]?.price || 0;
	let maxPrice = rows[0]?.price || 0;
	rows.forEach(row => {
		minPrice = Math.min(minPrice, row.price);
		maxPrice = Math.max(maxPrice, row.price);
	});

	total.price = [`Макс = ${maxPrice}`, `Мин = ${minPrice}`];

	let priceIncreaseSum = rows.reduce((sum, row) => sum + (+row.priceIncrease || 0), 0);
	total.priceIncrease = `Среднее = ${formatNumber(priceIncreaseSum / (rows.length - 1) || 0)}`;

	let chainIndexSum = rows.reduce((sum, row) => sum + (+row.chainIndex || 0), 0);
	total.chainIndex = `Среднее = ${formatNumber(chainIndexSum / (rows.length - 1) || 0)}`;

	total.priceIncreaseRate = [{ value: `${increaseCount} `, icon: UP_ARROW_ICON },
	{ value: `${decreaseCount} `, icon: DOWN_ARROW_ICON }];

	return { rows: rows, total: total };
}

function dropTable(table) {
	let body = table.querySelector('tbody');
	body.textContent = '';
}

function updateTable(table, data) {
	let body = table.querySelector('tbody');
	body.textContent = '';

	let bodyRows = data.rows.map((row) => getRow(row, false));
	body.append(...bodyRows);

	let footer = table.querySelector('tfoot');
	footer.textContent = '';

	let footerRow = getRow(data.total, true);
	footer.append(footerRow);
}

function updateStreak(streak, data) {
	let streakLength = getStreakLength(data);
	streak.textContent = streakLength;
}

function getStreakLength(data) {
	let sign = 1;
	let streakLength = 0;
	let maxStreakLength = 1;

	data.rows.forEach(row => {
		if (sign > 0 && row.priceIncrease > 0
			|| sign == 0 && (+row.priceIncrease || 0) == 0
			|| sign < 0 && row.priceIncrease < 0) {
			streakLength++;
			maxStreakLength = Math.max(maxStreakLength, streakLength);
		} else {
			streakLength = 1;
			sign = 0;
			if (row.priceIncrease > 0) {
				sign = 1;
			} else if (row.priceIncrease < 0) {
				sign = -1;
			}
		}
	});

	return maxStreakLength;
}

function getRow(rowData, footer = false) {
	let row = document.createElement('tr');

	row.append(getCell(rowData.date, true));
	row.append(getCell(rowData.priceIncreaseIcon, footer));
	row.append(getCell(rowData.price, footer, 'text-right'));
	row.append(getCell(rowData.priceIncrease, footer, 'text-right'));
	row.append(getCell(rowData.chainIndex, footer, 'text-right'));
	row.append(getCell(rowData.priceIncreaseRate, footer, 'text-right'));

	return row;
};

function getCell(content, header, align = 'text-left') {
	let cell = !!header ? document.createElement('th') : document.createElement('td');
	cell.classList.add(align);

	if (!Array.isArray(content)) {
		content = [content];
	}

	content.forEach(element => {
		if (cell.childNodes.length) {
			cell.append(document.createElement('br'));
		}

		if (element.value !== undefined) {
			cell.append(document.createTextNode(element.value));
		} else if (!element.icon && element !== '') {
			cell.append(document.createTextNode(element));
		}

		if (element.icon) {
			let icon = document.createElement('i');
			element.icon.split(' ').forEach((className) => {
				icon.classList.add(className);
			})
			cell.append(icon);
		}
	});

	return cell;
};

function formatNumber(x, maximumFractionDigits = 3) {
	let factor = 10 ** maximumFractionDigits;
	return Math.round(x * factor) / factor;
}


function formatDateUTC(date) {
	date = date || new Date();
	date = new Date(Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate()));
	return date.toISOString().substring(0, 10);
};