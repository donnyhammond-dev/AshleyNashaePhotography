const hero = document.getElementById("hero")
const heroLinks = document.querySelectorAll(".hero-navigation a")

heroLinks.forEach(link => {

	link.addEventListener("mouseenter", () => {

		const imagePath = link.dataset.image;

		hero.style.opacity = "0";

		setTimeout(() => {
			hero.style.backgroundImage = `url('${imagePath}')`;
			hero.style.opacity = "1";
		},500);

	});
});