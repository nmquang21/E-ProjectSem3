﻿jQuery(document).ready(function () {
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
    $('input[name="datefilter"]').on('apply.daterangepicker',function (ev, picker) {
        var start = picker.startDate.format('YYYY-MM-DD');
        var end = picker.endDate.format('YYYY-MM-DD');
        $.ajax({
            url: "Test",
            method: "POST",
            data: {
                start: start,
                end: end,
            },
            success: function (datas) {
                $('.statistics-vip-content').empty();
                if (datas.data.length > 0) {
                    $.each(datas.data, function (index, item) {
                        $('.statistics-vip-content').append(BodyContent(u = index+1, item))
                    })
                    $('.data-table').DataTable();
                }
                else {
                    $('.statistics-vip-content').html("<div style='color: red; '>There are no VIP members</div>")
                }

                google.charts.load('current', { 'packages': ['corechart'] });
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                    var data = new google.visualization.DataTable();
                    data.addColumn('date', 'Month');
                    data.addColumn('number', 'Silver');
                    data.addColumn('number', 'Gold');
                    for (var i = 0; i < data.length; i++) {
                        data.addRow([new Date(data[i].Date), datas.data[i].silver, datas.data[i].gold]);
                    }
                    var options = {
                        title: 'Company Performance',
                        curveType: 'function',
                        legend: { position: 'bottom' }
                    };
                    var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
                    chart.draw(data, options);
                }
            }
        })
    });
    function BodyContent(index,data) {
        var html = `<tr><td>${index}</td><td>${data.username}</td><td>${data.amount}</td><td>${data.type}</td><td>${data.created_at}</td></tr>`;
        return html;
    }
})