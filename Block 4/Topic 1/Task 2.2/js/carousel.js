window.addEventListener('load', e => {
	const track = document.querySelector('.carousel__track');
	const slides = Array.from(track.children);
	const nextButton = document.querySelector('.carousel__button--next');
	const prevButton = document.querySelector('.carousel__button--prev');
	const dotsNav = document.querySelector('.carousel__nav');
	const dots = Array.from(dotsNav.children);

	window.addEventListener('resize', e => {
		let currentSlide = track.querySelector('.selected');
		track.classList.add('notransition');
		moveToSlide(track, currentSlide, currentSlide);
		track.classList.remove('notransition');
	}, true);

	nextButton.addEventListener('click', e => {
		let currentSlide = track.querySelector('.selected');
		let nextSlide = currentSlide.nextElementSibling;

		if (nextSlide) {
			moveToSlide(track, currentSlide, nextSlide);
			updateButtons(nextSlide, prevButton, nextButton);

			let currentDot = dotsNav.querySelector('.selected');
			let nextDot = currentDot.nextElementSibling;
			updateDots(currentDot, nextDot);
		}
	});

	prevButton.addEventListener('click', e => {
		let currentSlide = track.querySelector('.selected');
		let prevSlide = currentSlide.previousElementSibling;

		if (prevSlide) {
			moveToSlide(track, currentSlide, prevSlide);
			updateButtons(prevSlide, prevButton, nextButton);

			let currentDot = dotsNav.querySelector('.selected');
			let prevDot = currentDot.previousElementSibling;
			updateDots(currentDot, prevDot);
		}
	});

	dotsNav.addEventListener('click', e => {
		let targetDot = e.target.closest('button');

		if (!targetDot) return;

		let currentSlide = track.querySelector('.selected');
		let currentDot = dotsNav.querySelector('.selected');
		let targetIndex = dots.findIndex(dot => dot === targetDot);
		let targetSlide = slides[targetIndex];

		moveToSlide(track, currentSlide, targetSlide);
		updateDots(currentDot, targetDot);
		updateButtons(targetSlide, prevButton, nextButton);
	});
});

const moveToSlide = (track, currentSlide, targetSlide) => {
	const amountToMove = targetSlide.getBoundingClientRect().x - track.getBoundingClientRect().x;
	// track.parentNode.scrollLeft = amountToMove;
	// console.log("translateX(-" + amountToMove + "px)");
	track.style.transform = "translateX(-" + amountToMove + "px)";
	if (currentSlide !== targetSlide) {
		currentSlide.classList.remove('selected');
		targetSlide.classList.add('selected');
	}
};

const updateButtons = (currentSlide, prevButton, nextButton) => {
	if (currentSlide.previousElementSibling && !prevButton.classList.contains('clickable')) {
		prevButton.classList.add('clickable');
	} else if (!currentSlide.previousElementSibling) {
		prevButton.classList.remove('clickable');
	}

	if (currentSlide.nextElementSibling && !nextButton.classList.contains('clickable')) {
		nextButton.classList.add('clickable');
	} else if (!currentSlide.nextElementSibling) {
		nextButton.classList.remove('clickable');
	}
};

const updateDots = (currentDot, targetDot) => {
	currentDot.classList.remove('selected');
	targetDot.classList.add('selected');
};