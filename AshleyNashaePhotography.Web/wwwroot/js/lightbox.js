const galleryPhotos = document.querySelectorAll(".gallery-photo");
const lightbox = document.getElementById("lightbox");
const lightboxImage = document.getElementById("lightbox-image");

galleryPhotos.forEach(photo => {
    photo.addEventListener("click", () => {
        lightboxImage.src = photo.src;
        lightbox.classList.add("show");
    });
});

lightbox.addEventListener("click", () => {
    lightbox.classList.remove("show");
});