window.addEventListener('load', () => {
	const form = document.getElementById('form');

	form.addEventListener('submit', (e) => {
		e.preventDefault();
		e.stopPropagation();

		let initialSum = form['initial-sum'].value;
		let interestRate = form['interest-rate'].value / 100;
		let depositTerm = form['deposit-term'].value;
		let numberFormat = new Intl.NumberFormat('ru-RU', options = { useGrouping: false, maximumFractionDigits: 2 });
		let finalSum = numberFormat.format(calcDeposit(initialSum, interestRate, depositTerm));

		form['final-sum'].value = finalSum;

		console.log(`${initialSum} * (1 + ${interestRate} / 12) ** ${depositTerm} = ${finalSum}`);
	});
});

function calcDeposit(initialSum, interestRate, depositTerm) {
	return initialSum * (1 + interestRate / 12) ** depositTerm;
}