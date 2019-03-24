'use strict';

$(document).ready(function () {

    /*
        Nav-Bar link active class toggle
    */
    $(".navbar-nav").find(".active").removeClass("active");
    $('.navbar-nav a[href="' + location.pathname + '"]').closest('li').addClass('active');

    // Disable scroll when focused on a number input.
    $('form').on('focus', 'input[type=number]', function (e) {
        $(this).on('wheel', function (e) {
            e.preventDefault();
        });
    });

    // Restore scroll on number inputs.
    $('form').on('blur', 'input[type=number]', function (e) {
        $(this).off('wheel');
    });

    // Disable up and down keys.
    $('form').on('keydown', 'input[type=number]', function (e) {
        if (e.which === 38 || e.which === 40)
            e.preventDefault();
    });

});



