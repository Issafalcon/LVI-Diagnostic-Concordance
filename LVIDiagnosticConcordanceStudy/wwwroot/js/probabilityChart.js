'use strict';

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
            responsive: true,
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