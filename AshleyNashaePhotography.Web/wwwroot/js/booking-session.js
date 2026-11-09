const sessionType = document.getElementById("session-type");
const sessionDescription = document.getElementById("session-description");

function updateSessionDescription() {
    const selectedOption = sessionType.selectedOptions[0];

    sessionDescription.textContent = selectedOption.dataset.description;
}

sessionType.addEventListener("change", updateSessionDescription);

updateSessionDescription();