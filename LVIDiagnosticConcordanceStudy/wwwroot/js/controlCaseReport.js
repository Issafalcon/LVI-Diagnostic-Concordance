'use strict';

$(document).ready(function () {

    /***************************************************************/
    /*              Submission Handlers                            */
    /***************************************************************/

    // Attach the click event to the button on page load
    $("#controlReportSubmit").click(initialSubmit);
    $("#controlSubmit").click(confirmSubmission);

    function initialSubmit() {
        var $caseReportForm = $("#caseReport");

        checkNumberOfLVI();

        if ($caseReportForm.length === 1 && $caseReportForm.valid()) {
            $("#controlGroupModal").modal("show");
        } else {
            // Errors with the form - send to OnPostAsync method, which will reload page with errors
            $caseReportForm.validate().showErrors();
        }
    };

    function confirmSubmission() {
        submitCaseReport();
    }

    function submitCaseReport() {
        var caseReportFormData = $("#caseReport").serialize();
        var caseUrl = window.location.pathname;
        $.ajax({
            type: "POST",
            url: caseUrl + "?handler=Submitted&" + jQuery.param({ isFromClient: true }),
            data: caseReportFormData,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            }
        }).done(function (res) {
            window.location.href = res.redirectUrl;
        });
    }

    /***************************************************************/
    /*              Helper Functions                               */
    /***************************************************************/

    function checkNumberOfLVI() {
        // Check here if the number of LVI is actually an int
        if (!Number.isInteger(Number($("#CaseReportViewModel_NumberofLVI")[0].value))) {
            forceError($("#CaseReportViewModel_NumberofLVI"), culture === "en-GB"
                ? "The Number Of LVI must be an integer of 0 or greater"
                : "Spanish: The Number Of LVI must be an integer of 0 or greater");
        } else {
            forceError($("#CaseReportViewModel_NumberofLVI"), "");
        }
    }

    function forceError(element, errorMessage) {
        $(element).rules("add", {
            forcibleerror: true,
            messages: {
                forcibleerror: function () { return errorMessage; }
            }
        });
        var isForced = false;
        if (errorMessage) {
            isForced = true;
        }
        $(element)[0].dataset.isForced = isForced;
        $(element).valid();
    }
    $.validator.addMethod("forcibleerror", function (value, element) {
        return $(element)[0].dataset.isForced !== "true";
    });
});