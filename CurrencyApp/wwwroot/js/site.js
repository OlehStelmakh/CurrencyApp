﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var ctx = document.getElementById('myChart').getContext('2d');

var chart = new Chart(ctx, {

    type: 'line',

    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "Dollar exchange rate",
            borderColor: 'rgb(255, 99, 132)',
            data: [25, 24.4, 24.6, 24, 25, 24,24.5],
        }]
    },

    options: {
        /*scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }*/
    }
});


var ctx = document.getElementById('myChart1').getContext('2d');

var chart = new Chart(ctx, {

    type: 'line',

    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "Euro exchange rate",
            borderColor: 'rgb(99, 255, 132)',
            data: [27, 26.4, 26, 25.7, 26.3, 26.8, 26],
        }]
    },

    options: {
        
    }
});


