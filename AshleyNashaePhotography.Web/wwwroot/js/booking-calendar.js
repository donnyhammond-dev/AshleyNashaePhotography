flatpickr("#session-date",
    {
        inline: true,
        minDate: new Date().fp_incr(7),
        disable: bookedDates

    });