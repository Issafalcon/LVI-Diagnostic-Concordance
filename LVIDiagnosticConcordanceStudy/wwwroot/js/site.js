$(document).ready(function () {

    /*
        Nav-Bar link active class toggle
    */
    $(".navbar-nav").find(".active").removeClass("active");
    $('.navbar-nav a[href="' + location.pathname + '"]').closest('li').addClass('active');

    /*
        Alternative submit button handler for intervention group
    */

    $("#probabilityInfo").click(function (e) {
        var $caseReportForm = $("#caseReport");
        
        checkNumberOfLVI();

        if ($caseReportForm.length === 1 && $caseReportForm.valid()) {
            // If we have no jQuery validation errors on the form, then display additional probability data
            // to intervention group.
            var caseUrl = window.location.pathname;
            var caseReportData = {
                PatientAge: $("#CaseReportViewModel_PatientAge")[0].value,
                TumourSize: $("#CaseReportViewModel_TumourSize")[0].value,
                TumourGrade: $("#CaseReportViewModel_TumourGrade")[0].value,
                NumberofLVI: $("#CaseReportViewModel_NumberofLVI")[0].value
            };

            $.ajax({
                type: "POST",
                url: caseUrl + "?handler=ChartVC",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(caseReportData),
                
                // Need to add this for .NET Core Pages for posting otherwise will return 400
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).done(function (res) {
                populateProbabilityChart(res);
            });

            $("#interventionGroupModal").modal("show");

        } else {
            // Errors with the form - send to OnPostAsync method, which will reload page with errors
            //$caseReportForm.submit();
            $caseReportForm.validate().showErrors();
        }
    });

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

    function setError(name, message) {
        const span = $(`span[data-valmsg-for="${name}"]`);
        const input = $(`input[name="${name}"]`);
        if (span && span.length > 0) {
            $(span).html(message);
            if (message) {
                $(input).addClass("input-validation-error");
                $(input).attr("aria-invalid", true);
                $(span).removeClass("field-validation-valid");
                $(span).addClass("field-validation-error");
            } else {
                $(input).removeClass("input-validation-error");
                $(span).removeClass("field-validation-error");
                $(span).addClass("field-validation-valid");
            }
        }
    }

    function displayInterventionInfo() {
        // If we have no jQuery validation errors on the form, then display additional probability data
        // to intervention group.
        var caseUrl = window.location.pathname;
        var caseReportData = {
            PatientAge: $("#CaseReportViewModel_PatientAge")[0].value,
            TumourSize: $("#CaseReportViewModel_TumourSize")[0].value,
            TumourGrade: $("#CaseReportViewModel_TumourGrade")[0].value,
            NumberofLVI: $("#CaseReportViewModel_NumberofLVI")[0].value
        };

        $.ajax({
            type: "POST",
            url: caseUrl + "?handler=ChartVC",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(caseReportData),

            // Need to add this for .NET Core Pages for posting otherwise will return 400
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            }
        }).done(function (res) {
            populateProbabilityChart(res);
        });

        $("#interventionGroupModal").modal("show");
    }

    function populateProbabilityChart(chartData) {
        var ctx = document.getElementById("probabilityChartPlaceholder").getContext("2d");
        var probabilityChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: chartData.ChartXAxis,
                datasets: [{
                    label: 'Theoretical Probability of LVI',
                    data: chartData.TheoreticalYValues,
                    backgroundColor: 'rgba(34, 167, 240, 0.5)',
                    borderColor: 'rgba(34, 167, 240, 0.5)',
                    borderWidth: 1
                },
                {
                    label: 'Observed Probability of LVI',
                    data: chartData.ObservedYValues,
                    backgroundColor: 'rgba(165, 55, 253, 0.5)',
                    borderColor: 'rgba(165, 55, 253, 0.5)',
                    borderWidth: 1
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Probability Based On Cumulative LVI"
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Probability of Having LVI"
                        }                        
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true
                        },
                        scaleLabel: {
                            display: true,
                            labelString: "Cumulative Cases Reported As LVI Positive"
                        }                        
                    }]
                }
            }
        });
    }
});



