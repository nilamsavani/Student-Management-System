//Project:	Alpino - Responsive Bootstrap 4 Template
//Primary use:	Alpino - Responsive Bootstrap 4 Template
$(function() {
    "use strict";	
    MorrisArea();
    initDonutChart();
});

$(function () {   
    $('.sparkline-pie').sparkline('html', {
        type: 'pie',
        offset: 90,
        width: '138px',
        height: '138px',
        sliceColors: ['#454c56', '#61ccb7', '#5589cd']
    })

    $("#sparkline14").sparkline([8,2,3,7,6,5,2,1,4,8], {
        type: 'line',
        width: '100%',
        height: '28',
        lineColor: '#3f7dc5',
        fillColor: 'transparent',
        spotColor: '#000',
        lineWidth: 1,
        spotRadius: 2,        

    });
    $("#sparkline15").sparkline([2,3,9,1,2,5,4,7,8,2], {
        type: 'line',
        width: '100%',
        height: '28',
        lineColor: '#e66990',
        fillColor: 'transparent',
        spotColor: '#000',
        lineWidth: 1,
        spotRadius: 2,
    });
   
    $('.sparkbar').sparkline('html', {
        type: 'bar',
        height: '100',
        width: '100%',        
        barSpacing: '20',
        barColor: '#e56590',
        negBarColor: '#4ac2ae',
        responsive: true,
    });
});

// Morris-chart
function MorrisArea() {
    Morris.Area({
        element: 'area_chart',
            data: [{
                period: '2011',
                America: 2,
                India: 0,
                Australia: 0
            }, {
                period: '2012',
                America: 31,
                India: 10,
                Australia: 5
            }, {
                period: '2013',
                America: 15,
                India: 28,
                Australia: 23
            }, {
                period: '2014',
                America: 45,
                India: 12,
                Australia: 7
            }, {
                period: '2015',
                America: 20,
                India: 32,
                Australia: 55
            }, {
                period: '2016',
                America: 39,
                India: 67,
                Australia: 20
            }, {
                period: '2017',
                America: 20,
                India: 9,
                Australia: 5
            }

        ],
        lineColors: ['#a890d3', '#FFC107', '#666666'],
        xkey: 'period',
        ykeys: ['America', 'India', 'Australia'],
        labels: ['America', 'India', 'Australia'],
        pointSize: 0,
        lineWidth: 0,
        resize: true,
        fillOpacity: 0.8,
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        hideHover: 'auto'
    });
    Morris.Area({
        element: 'm_area_chart',
        data: [{
                period: '2011',
                Chadengle: 45,
                Damien: 75,
                Monica: 18
            }, {
                period: '2012',
                Chadengle: 130,
                Damien: 110,
                Monica: 82
            }, {
                period: '2013',
                Chadengle: 80,
                Damien: 60,
                Monica: 85
            }, {
                period: '2014',
                Chadengle: 78,
                Damien: 205,
                Monica: 135
            }, {
                period: '2015',
                Chadengle: 180,
                Damien: 124,
                Monica: 140
            }, {
                period: '2016',
                Chadengle: 105,
                Damien: 100,
                Monica: 85
            },
            {
                period: '2017',
                Chadengle: 210,
                Damien: 180,
                Monica: 120
            }
        ],
        xkey: 'period',
        ykeys: ['Chadengle', 'Damien', 'Monica'],
        labels: ['Chadengle', 'Damien', 'Monica'],
        pointSize: 3,
        fillOpacity: 0,
        pointStrokeColors: ['#007bff', '#28a745', '#ffc107'],
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        lineWidth: 2,
        hideHover: 'auto',
        lineColors: ['#007bff', '#28a745', '#ffc107'],
        resize: true

    });
}
function initDonutChart() {
    Morris.Donut({
        element: 'donut_chart',
        data: [{
                label: 'Chrome',
                value: 37
            }, {
                label: 'Firefox',
                value: 30
            }, {
                label: 'Safari',
                value: 18
            }, {
                label: 'Opera',
                value: 12
            },
            {
                label: 'Other',
                value: 3
            }
        ],
        colors: ['#93e3ff', '#b0dd91', '#ffe699', '#f8cbad', '#a4a4a4'],
        formatter: function(y) {
            return y + '%'
        }
    });
}
//======
$(window).on('scroll',function() {
    $('.card .sparkline').each(function() {
        var imagePos = $(this).offset().top;

        var topOfWindow = $(window).scrollTop();
        if (imagePos < topOfWindow + 400) {
            $(this).addClass("pullUp");
        }
    });
});

/*VectorMap Init*/
$(function () {
    $('#world-map-markers2').vectorMap({
        map : 'world_mill_en',
        scaleColors : ['#ea6c9c', '#ea6c9c'],
        normalizeFunction : 'polynomial',
        hoverOpacity : 0.7,
        hoverColor : false,
        regionStyle : {
            initial : {
                fill : '#e0e0e0'
            }
        },
         markerStyle: {
            initial: {
                r: 15,
                'fill': '#313740',
                'fill-opacity': 0.9,
                'stroke': '#fff',
                'stroke-width' : 5,
                'stroke-opacity': 0.5
            },

            hover: {
                'stroke': '#fff',
                'fill-opacity': 1,
                'stroke-width': 5
            }
        },
        backgroundColor : 'transparent',
        markers: [
            { latLng: [37.09,-95.71], name: 'America' },
            { latLng: [51.16,10.45], name: 'Germany' },
            { latLng: [-25.27, 133.77], name: 'Australia' },
            { latLng: [56.13,-106.34], name: 'Canada' },
            { latLng: [20.59,78.96], name: 'India' },
            { latLng: [55.37,-3.43], name: 'United Kingdom' },
        ]
    });
});

