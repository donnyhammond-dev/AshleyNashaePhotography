const heroLinks = document.querySelectorAll(".hero-navigation a");

const mobileQuery = window.matchMedia("(max-width: 767.98px)");
let slideshowTimer = null;
let currentPortfolioIndex = 2;

const layer1 = document.querySelector(".hero-image-1");
const layer2 = document.querySelector(".hero-image-2");

let activeLayer = 1;
let currentImagePath = heroLinks[2].dataset.image;

heroLinks.forEach(link => {

	link.addEventListener("mouseenter", () => {

		changeHeroImage(link.dataset.image);

	});

});

if (mobileQuery.matches) {
	heroLinks[currentPortfolioIndex].classList.add("active");
	startMobileSlideshow();
}



function changeHeroImage(imagePath) {

	if (imagePath === currentImagePath) {
		return;
	}

	const incomingLayer = activeLayer === 1 ? layer2 : layer1;
	const outgoingLayer = activeLayer === 1 ? layer1 : layer2;

	incomingLayer.style.zIndex = "1";
	outgoingLayer.style.zIndex = "0";

	incomingLayer.style.backgroundImage = `url('${imagePath}')`;

	incomingLayer.style.opacity = "1";
	outgoingLayer.style.opacity = "0";

	activeLayer = activeLayer === 1 ? 2 : 1;
	currentImagePath = imagePath;
}

function startMobileSlideshow() {

	function nextImage() {
		heroLinks[currentPortfolioIndex].classList.remove("active");

		currentPortfolioIndex = (currentPortfolioIndex + 1) % heroLinks.length;

		changeHeroImage(heroLinks[currentPortfolioIndex].dataset.image);

		heroLinks[currentPortfolioIndex].classList.add("active");
	}

	setTimeout(() => {
		nextImage();

		slideshowTimer = setInterval(nextImage, 6000);

	}, 1000);
}