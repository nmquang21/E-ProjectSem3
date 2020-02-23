function getParameterFromUrl(parameter) {
    var url_string = window.location.href;
    var url = new URL(url_string);
    return url.searchParams.get(parameter);
}

function drawLineChart(contronllerName) {

    var startDate = new Date();
    startDate.setFullYear(startDate.getFullYear() - 1);
    var endDate = new Date();

    var startPara = getParameterFromUrl('start');
    if (startPara != null) {
        startDate = new Date(startPara);
    }
    var endPara = getParameterFromUrl('end');
    if (endPara != null) {
        endDate = new Date(endPara);
    }
    $('input[name="datefilter"]').daterangepicker({
        autoUpdateInput: true,
        startDate: startDate,
        endDate: endDate,
        locale: {
            cancelLabel: 'Clear'
        },
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [
                moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')
            ]
        }
    });

    $('input[name="datefilter"]').on('apply.daterangepicker',
        function (ev, picker) {
            var start = picker.startDate.format('YYYY-MM-DD');
            var end = picker.endDate.format('YYYY-MM-DD');
            window.location.href = window.location.href.split('?')[0] + '?start=' + start + '&end=' + end;
        });

    $.ajax({
        url: `/${contronllerName}/GetChartDataRevenue?start=${startPara}&end=${endPara}`,
        type: 'GET',
        success: function (responseData) {
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = new google.visualization.DataTable();
                data.addColumn('date', 'Month');
                data.addColumn('number', 'Total');
                for (var i = 0; i < responseData.length; i++) {
                    data.addRow([new Date(responseData[i].Date), responseData[i].Total]);
                }
                var options = {
                    title: 'Company Performance',
                    curveType: 'function',
                    legend: {position: 'bottom' }
                };
                var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
                chart.draw(data, options);
            }
        }
    });
}



