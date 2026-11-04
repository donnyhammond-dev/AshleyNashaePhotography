flatpickr("#session-date",
    {
        inline: false,
        minDate: new Date().fp_incr(7),
        disable: bookedDates

    });