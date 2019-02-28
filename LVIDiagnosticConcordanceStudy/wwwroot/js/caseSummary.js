'use strict';

$(document).ready(function () {
    /***************************************************************/
    /*              Submission Handlers                            */
    /***************************************************************/

    // Attach the click event to the button on page load
    $("#resetReportsSubmit").click(initialSubmit);
    $("#completeStudySubmit").click(completeStudySubmit);

    function initialSubmit() {
        $("#resetWarningModal").modal("show");
    }

    function completeStudySubmit() {
        $("#completeStudyWarningModal").modal("show");
    }
});