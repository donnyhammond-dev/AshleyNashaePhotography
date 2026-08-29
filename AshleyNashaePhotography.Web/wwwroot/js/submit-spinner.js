const forms = document.querySelectorAll(".submit-spinner");

forms.forEach(function (form) {

    form.addEventListener("submit", function () {

        const submitButton = form.querySelector('button[type="submit"]');

        submitButton.disabled = true;

        submitButton.innerHTML = `<span class="spinner-border spinner-border-sm"></span>`;
    });

});