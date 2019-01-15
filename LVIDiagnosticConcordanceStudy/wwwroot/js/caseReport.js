'use strict';

//import { populateProbabilityChart } from "./probabilityChart.js";



$(document).ready(function () {

    /*
        Alternative submit button handler for intervention group
    */

    $("#CaseReportViewModel_TumourGrade").change(function (e) {
        var caseReportData = getViewModelValues();

        if (!caseReportData.TumourGrade) {
            // If we don't have a value, remove the existing pre-test probability and return
            $("#preTestProbabilityPlaceholder").empty();
            return;
        }

        if (caseReportData.NumberofLVI) {
            // Run the full set of probability calculations again as we have complete set of report data
        } else {
            $.ajax({
                type: "GET",
                url: window.location.pathname + "?handler=PreTestProbabilityData&" + jQuery.param(caseReportData),
                contentType: "application/json",
                dataType: "json"
            }).done(function (res) {
                getPreTestProbabilityViewComponent(res);
            });
        }
    });

    $("#probabilityInfo").click(function (e) {

        // First, clear out the modal to reset with new data
        $("#interventionGroupModalPlaceholder").empty();
        var $caseReportForm = $("#caseReport");

        checkNumberOfLVI();

        if ($caseReportForm.length === 1 && $caseReportForm.valid()) {
            // If we have no jQuery validation errors on the form, then display additional probability data
            // to intervention group.
            var caseUrl = window.location.pathname;
            var caseReportData = getViewModelValues();

            $.ajax({
                type: "GET",
                url: caseUrl + "?handler=InterventionData&" + jQuery.param(caseReportData),
                contentType: "application/json",
                dataType: "json"
                //data: JSON.stringify(caseReportData),

                //// Need to add this for .NET Core Pages for posting otherwise will return 400
                //headers: {
                //    RequestVerificationToken:
                //        $('input:hidden[name="__RequestVerificationToken"]').val()
                //}
            }).done(function (res) {
                getInterventionViewComponent(res);
            });
        } else {
            // Errors with the form - send to OnPostAsync method, which will reload page with errors
            //$caseReportForm.submit();
            $caseReportForm.validate().showErrors();
        }
    });

    function getViewModelValues() {
        return {
            PatientAge: $("#CaseReportViewModel_PatientAge")[0].value,
            TumourSize: $("#CaseReportViewModel_TumourSize")[0].value,
            TumourGrade: $("#CaseReportViewModel_TumourGrade")[0].value,
            NumberofLVI: $("#CaseReportViewModel_NumberofLVI")[0].value
        };
    }

    function checkNumberOfLVI() {
        // Check here if the number of LVI is actually an int
        if (!Number.isInteger(Number($("#CaseReportViewModel_NumberofLVI")[0].value))) {
            forceError($("#CaseReportViewModel_NumberofLVI"), "The Number Of LVI must be an integer of 0 or greater");
            //setError("CaseReportViewModel.NumberofLVI", "The Number Of LVI must be an integer of 0 or greater");
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

    function getPreTestProbabilityViewComponent(data) {

        $.ajax({
            type: "GET",
            url: window.location.pathname + "?handler=PreTestProbabilityViewComponent&" + jQuery.param({ preTestProb: data })
        }).done(function (res) {
            $("#preTestProbabilityPlaceholder").fadeToggle("300", function () {
                $("#preTestProbabilityPlaceholder").html(res);
                $("#preTestProbabilityPlaceholder").fadeToggle("300");
            });
            
        });
    }

    function getInterventionViewComponent(data) {
        var observedValue;

        for (var i = 0; i < data.observedYValues.length; i++) {
            // There will only be one observed value not equal to 0 in our oberved series
            // so break once we have it
            if (data.observedYValues[i] !== 0) {
                observedValue = data.observedYValues[i];
                break;
            }
        }

        var caseUrl = window.location.pathname;
        var probabilityData = {
            preTestProb: data.preTestProbability,
            postTestProb: data.postTestProbability,
            observedValue: observedValue
        };

        $.ajax({
            type: "GET",
            url: caseUrl + "?handler=InterventionViewComponent&" + jQuery.param(probabilityData)
        }).done(function (res) {
            $("#interventionGroupModalPlaceholder").append(res);
            $("#interventionGroupModal").modal("show");
            populateProbabilityChart(data);

            // Add the final submission click handler to submit the original form
            $("#interventionSubmit").click(confirmSubmission);
        });
    }

    function confirmSubmission() {
        if ($("#confirmationCode").length) {
            if ($("#codeInput").val() === $("#confirmationCode").text()) {
                submitCaseReport();
            } else {
                $("#codeInputError").text("The code does not match. Please try again.");
            }
        } else {
            submitCaseReport();
        }
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
});