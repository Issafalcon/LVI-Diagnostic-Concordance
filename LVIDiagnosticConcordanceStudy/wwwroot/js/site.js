'use strict';

$(document).ready(function () {

    /*
        Nav-Bar link active class toggle
    */
    $(".navbar-nav").find(".active").removeClass("active");
    $('.navbar-nav a[href="' + location.pathname + '"]').closest('li').addClass('active');

    
});



