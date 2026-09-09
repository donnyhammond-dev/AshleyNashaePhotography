const hero = document.getElementById("hero");
const heroLinks = document.querySelectorAll(".hero-navigation a");

let currentImagePath = hero.dataset.image;

heroLinks.forEach(link => {

	link.addEventListener("mouseenter", () => {

		const imagePath = link.dataset.image;

		if (imagePath != currentImagePath) {
			console.log("1. Mouse entered/Condition met", link.textContent);

			hero.style.opacity = "0";
			console.log("2. Fade out", hero.style.backgroundImage);

			setTimeout(() => {

				hero.style.backgroundImage = `url('${imagePath}')`;
				currentImagePath = imagePath;
				hero.style.opacity = "1";
				console.log("3. Fade in", imagePath);
			}, 750);
		}
		


	});
});