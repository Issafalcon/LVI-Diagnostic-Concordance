/*
    Nav-Bar link active class toggle
*/

$(document).ready(function () {
    $(".navbar-nav").find(".active").removeClass("active");
    $('.navbar-nav a[href="' + location.pathname + '"]').closest('li').addClass('active');

    // For the treatment group case report submission, handle the submission action via boostrap Modal
    // which displays the probability curve and pre / post test probabilities:
    $("#probabilityInfo").click(function (e) {
        var $caseReportForm = $("#caseReport");
        var errors;

        //if ($caseReportForm[0]['__MVC_FormValidation']) {
        //    errors = $caseReportForm[0]['__MVC_FormValidation'].validate("submit");
        //}

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

            //$("#probabilityChartPlaceholder").load(
            //    caseUrl + "?handler=chartVC",
            //    JSON.stringify(caseReportData)
            //);

            $.ajax({
                type: "POST",
                url: caseUrl + "?handler=ChartVC",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(caseReportData),
                
                // Need to get this for .NET Core Pages for posting otherwise will return 400
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                }
            }).done(function (chart) {
                $("#probabilityChartPlaceholder").append(chart);
            });

            $("#interventionGroupModal").modal("show");

        } else {
            // Errors with the form - send to OnPostAsync method, which will reload page with errors
            $caseReportForm.submit();
        }
    });
});

/*
    On-Click handler for intervention group report 'Submit' button    
*/




