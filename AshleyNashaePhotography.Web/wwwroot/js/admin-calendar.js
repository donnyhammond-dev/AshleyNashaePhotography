flatpickr("#availability-calendar", {
    inline: true,
    minDate: "today",

    onDayCreate: function (dObj, dStr, fp, dayElem) {

        const date = fp.formatDate(dayElem.dateObj, "Y-m-d");

        if (bookedDates.includes(date)) {
            dayElem.classList.add("booked-date");
        }

        if (blockedDates.includes(date)) {
            dayElem.classList.add("blocked-date");
        }
    }
});