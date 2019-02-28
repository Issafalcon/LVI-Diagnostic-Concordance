'use strict';

$(document).ready(function () {
    /***************************************************************/
    /*              Submission Handlers                            */
    /***************************************************************/

    // Attach the click event to the button on page load
    $("#resetCasesSubmit").click(initialSubmit);

    function initialSubmit() {
        $("#resetWarningModal").modal("show");
    }
});