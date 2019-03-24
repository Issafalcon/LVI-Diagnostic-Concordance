'use strict';

$(document).ready(function () {

    // For use with simple phrase localization in JS
    // Would need to add more extensive solution for additional languages /  larger app, but this will do for a few phrases
    var culture = /c=(.+-.+)\|/.exec(getCookie(".AspNetCore.Culture"))[1];

    /***************************************************************/
    /*              Pre-Test Probability Calls                     */
    /***************************************************************/

    // Data Retrieval
    $("#CaseReportViewModel_TumourGrade").change(function (e) {
        var caseReportData = getViewModelValues();

        $("[data-toggle=popover]").popover('dispose');

        if (!caseReportData.TumourGrade) {
            // If we don't have a value clear out all additional details
            clearAdditionalProbabilityData();
            clearPreTestProbabilityData();
            return;
        }

        if (caseReportData.NumberofLVI) {
            // Run the full set of probability calculations again as we have complete set of report data
            getPostTestProbabilityData();
        } else {
            $.ajax({
                type: "GET",
                url: window.location.pathname + "?handler=PreTestProbabilityData&" + jQuery.param(caseReportData),
                contentType: "application/json",
                dataType: "json"
            }).done(function (res) {
                $("#preTestProbabilityPlaceholder").hide();
                getPreTestProbabilityViewComponent(res);
            });
        }
    });

    // View Component retrieval
    function getPreTestProbabilityViewComponent(data) {

        $.ajax({
            type: "GET",
            url: window.location.pathname + "?handler=PreTestProbabilityViewComponent&" + jQuery.param({ preTestProb: data })
        }).done(function (res) {
            $("#preTestProbabilityPlaceholder").html(res);
            $("[data-toggle=popover]").popover();
            $("#preTestProbabilityPlaceholder").fadeIn("300");
        });
    }

    /***************************************************************/
    /*              Additional Probability Data Calls              */
    /***************************************************************/    

    var timeout = null;

    $("#CaseReportViewModel_NumberofLVI").keyup(function (e) {

        clearTimeout(timeout);

        timeout = setTimeout(getPostTestProbabilityData, 500);
    });

    // Data Retrieval
    function getPostTestProbabilityData() {

        $("[data-toggle=popover]").popover('dispose');
        var caseReportData = getViewModelValues();

        if (caseReportData.TumourGrade && caseReportData.NumberofLVI) {
            var $caseReportForm = $("#caseReport");

            checkNumberOfLVI();

            if ($caseReportForm.length === 1 && $caseReportForm.valid()) {
                // If we have no jQuery validation errors on the form, then display additional probability data
                // to intervention group.
                var caseUrl = window.location.pathname;

                $.ajax({
                    type: "GET",
                    url: caseUrl + "?handler=AdditionalProbabilityData&" + jQuery.param(caseReportData),
                    contentType: "application/json",
                    dataType: "json"

                }).done(function (res) {
                    // Refresh the pre-test probability data as changing the Grade may have triggered probability data refresh
                    getPreTestProbabilityViewComponent(res.preTestProbability);
                    getAdditionalProbabilityDataViewComponent(res);
                });
            } else {
                // Errors with the form - send to OnPostAsync method, which will reload page with errors
                $caseReportForm.validate().showErrors();
                clearAdditionalProbabilityData();
            }
        } else {
            // Clear out the data as appropriate
            if (!caseReportData.NumberofLVI) {
                clearAdditionalProbabilityData();
            }

            if (!caseReportData.TumourGrade) {
                clearAdditionalProbabilityData();
                clearPreTestProbabilityData();
            }            
        }
    }

    // View Component Retrieval
    function getAdditionalProbabilityDataViewComponent(data) {

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
            observedValue: observedValue,
            lviReported: data.lviReported
        };

        $.ajax({
            type: "GET",
            url: caseUrl + "?handler=AdditionalProbabilityViewComponent&" + jQuery.param(probabilityData)
        }).done(function (res) {
            $("#additionalProbabilityDataPlaceholder").fadeIn("300", function () {

                // Take a reference to the relocated submit button before we blow it away with the new view component
                const $interventionSubmitBtn = $("#interventionSubmitDiv");
                $("#additionalProbabilityDataPlaceholder").html(res);
                $("[data-toggle=popover]").popover();
                $("#additionalProbabilityDataPlaceholder").addClass("card");
                $("#probabilityChartPlaceholder").after($interventionSubmitBtn);
                populateProbabilityChart(data);

                const codeInput = document.getElementById('codeInput');

                if (codeInput !== null) {
                    codeInput.onpaste = function (e) {
                        e.preventDefault();
                    };
                }


                $("#interventionSubmit").click(confirmSubmission);
                // Need to re-add the initial submit button (before the modal) as we have moved it to the new chart card
                $("#interventionReportSubmit").click(initialSubmit);
            });
        });
    }

    /***************************************************************/
    /*              Submission Handlers                            */
    /***************************************************************/

    // Attach the click event to the button on page load
    $("#interventionReportSubmit").click(initialSubmit);

    function initialSubmit() {
        var $caseReportForm = $("#caseReport");

        checkNumberOfLVI();

        if ($caseReportForm.length === 1 && $caseReportForm.valid()) {
            $("#interventionGroupModal").modal("show");
        } else {
            // Errors with the form - send to OnPostAsync method, which will reload page with errors
            $caseReportForm.validate().showErrors();
        }
    };

    function confirmSubmission() {
        if ($("#confirmationCode").length) {
            if ($("#codeInput").val() === $("#confirmationCode").text()) {
                submitCaseReport();
            } else {
                $("#codeInputError").text(culture === "en-GB"
                    ? "The code does not match. Please try again."
                    : "Spanish: The code does not match. Please try again");
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

    /***************************************************************/
    /*              Helper Functions                               */
    /***************************************************************/

    function clearPreTestProbabilityData() {
        $("#preTestProbabilityPlaceholder").fadeOut("300");
        $("#preTestProbabilityPlaceholder").empty();
    }

    function clearAdditionalProbabilityData() {
        $("#additionalProbabilityDataPlaceholder").fadeOut("300");
        $("#additionalProbabilityDataPlaceholder").removeClass("card");
        $("#caseReport").append($("#interventionSubmitDiv"));
        $("#additionalProbabilityDataPlaceholder").empty();
    }

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
    
    // Get specified cookies
    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
    /***************************************************************/
    /*              Chart Functions                                */
    /***************************************************************/

    function populateProbabilityChart(data) {
        var ctx = document.getElementById("probabilityChartPlaceholder").getContext("2d");

        var probabilityChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.chartXAxis,
                datasets: [{
                    label: culture === "en-GB"
                        ? 'Theoretical probability to every possible combination'
                        : 'Probabilidad teórica para todas las combinaciones combinaciones posibles',
                    data: data.theoreticalYValues,
                    backgroundColor: 'rgba(34, 167, 240, 0.5)',
                    borderColor: 'rgba(34, 167, 240, 0.5)',
                    borderWidth: 1
                },
                {
                    label: culture === "en-GB"
                        ? 'Probability based on LVI positive cases reported by the user'
                        : 'Probabilidad basada en Casos PV positivos ingresados por el usuario',
                    data: data.observedYValues,
                    backgroundColor: 'rgba(165, 55, 253, 0.5)',
                    borderColor: 'rgba(165, 55, 253, 0.5)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: culture === "en-GB"
                        ? "Probability Based On Cumulative LVI"
                        : "Probability Based On Cumulative LVI"
                },
                tooltips: {
                    enabled: true
                },
                legend: {
                    display: true
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        },
                        scaleLabel: {
                            display: true,
                            labelString: culture === "en-GB"
                                ? "Probability of Having LVI"
                                : "Probability of Having LVI"
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true
                        },
                        scaleLabel: {
                            display: true,
                            labelString: culture === "en-GB"
                                ? "Cumulative Cases Reported As LVI Positive"
                                : "Cumulative Cases Reported As LVI Positive"
                        }
                    }]
                }
            }
        });
    }

    // Note - For POST requests, the request verification token is required - Include in ajax calls as so:
    //data: JSON.stringify(caseReportData),

    //// Need to add this for .NET Core Pages for posting otherwise will return 400
    //headers: {
    //    RequestVerificationToken:
    //        $('input:hidden[name="__RequestVerificationToken"]').val()
    //}
});