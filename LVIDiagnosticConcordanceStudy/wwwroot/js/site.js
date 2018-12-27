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
                type: "GET",
                url: caseUrl + "?handler=InterventionData&" +  jQuery.param(caseReportData),
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
            $("#interventionGroupModalPlaceholder").replaceWith(res);
            $("#interventionGroupModal").modal("show");
            populateProbabilityChart(data);
        });
    }

    function populateProbabilityChart(data) {
        var ctx = document.getElementById("probabilityChartPlaceholder").getContext("2d");
        var probabilityChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.chartXAxis,
                datasets: [{
                    label: 'Theoretical Probability of LVI',
                    data: data.theoreticalYValues,
                    backgroundColor: 'rgba(34, 167, 240, 0.5)',
                    borderColor: 'rgba(34, 167, 240, 0.5)',
                    borderWidth: 1
                },
                {
                    label: 'Observed Probability of LVI',
                    data: data.observedYValues,
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



