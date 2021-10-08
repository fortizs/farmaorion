//Consultas Ajax

function PoblarClientes(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/ClientesTop10",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteVentaProductos(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/VentaProductos",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                //graficoVentas(data.d);
                console.log("Productos mas vendidos");
                console.log(data.d);
                graficoProductoMasVendido(data.d);

                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteVentaXSucursal(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/VentaxSucursal",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                graficoVentasXSucursal(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function VentaxTipoServicioListar(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/VentaxTipoServicioListar",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                graficoVentaXTipoServicio(data.d);
                //graficoPrueba(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteTotalCompraVenta(Filtros_OBJ) {
    $.ajax(
        {
            url: "WSDashBoard.asmx/IBTotalVentayCompraListar",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                etiquetaTotalCompraVenta(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteVendedoresTop(Filtros_OBJ) {
    $.ajax(
        {
            url: "WSDashBoard.asmx/graficoVendedoresTop",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                console.log(data.d);
                graficoVendedoresTop(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteVentaMensualPorAnio(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/IBReporteVentaMensualPorAnio",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                console.log("venta por meses");
                console.log(data.d);

                IBReporteVentaMensualPorAnio(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteVentaDiariaPorMes(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/IBReporteVentaDiariaPorMes",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                console.log("venta por dia");
                console.log(data.d);

                IBReporteVentaDiariaPorMes(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteMetaAnualPorSucursal(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/IBMetaAnualPorSucursal",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                console.log("meta anual por sucursal");
                console.log(data.d);

                IBReporteMetaAnualPorSucursal(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

function ReporteMetaAnualPorEmpresa(Filtros_OBJ) {

    $.ajax(
        {
            url: "WSDashBoard.asmx/IBMetaAnualPorEmpresa",
            data: Filtros_OBJ,
            dataType: "json",
            type: "post",
            contentType: "application/json; charset-utf-8",
            success: function (data) {
                IBReporteMetaAnualPorEmpresa(data.d);
                $("#upCargando").hide();
            },
            error: function (result) {
                $("#upCargando").hide();
                Mensaje("warning", "Error " + result.status + " " + result.statusText);
            }
        });
}

//Graficos

function graficoPrueba(Data) {

    var cantidad = Data.length;


    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    var chart = am4core.create("chartdiv1", am4charts.PieChart3D);
    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in


    var DataTipoServicio = new Array(cantidad);

    for (var i = 0; i < cantidad; i++) {

        DataTipoServicio = {
            country: Data[i]['TipoServicio'],
            litres: parseFloat(Data[i]['MontoServicio'])
        };
        chart.data.push(DataTipoServicio);
    }


    chart.innerRadius = am4core.percent(40);
    chart.depth = 120;

    chart.legend = new am4charts.Legend();

    var series = chart.series.push(new am4charts.PieSeries3D());
    series.dataFields.value = "litres";
    series.dataFields.depthValue = "litres";
    series.dataFields.category = "country";
    series.slices.template.cornerRadius = 5;
    series.colors.step = 3;

}

function graficoVentas(Data) {

    var cantidad = Data.length;

    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("chartdiv3", am4charts.XYChart3D);
        var DataServicio = new Array(cantidad);

        for (var i = 0; i < cantidad; i++) {

            DataServicio = {
                "servicio": Data[i]['NombreProducto'],
                "ventas": parseFloat(Data[i]['Cantidad']),
                "color": chart.colors.next()
            };

            //DataServicio.servicio = Data[i]['NombreProducto'];
            //DataServicio.ventas = parseFloat(Data[i]['Cantidad']);
            //DataServicio.color = chart.colors.next();
            chart.data.push(DataServicio);
        }

        // Create axes
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "servicio";
        categoryAxis.renderer.labels.template.rotation = 270;
        categoryAxis.renderer.labels.template.hideOversized = false;
        categoryAxis.renderer.minGridDistance = 20;
        categoryAxis.renderer.labels.template.horizontalCenter = "right";
        categoryAxis.renderer.labels.template.verticalCenter = "middle";
        categoryAxis.tooltip.label.rotation = 270;
        categoryAxis.tooltip.label.horizontalCenter = "right";
        categoryAxis.tooltip.label.verticalCenter = "middle";

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.title.text = "Productos Mas Vendidos";
        valueAxis.title.fontWeight = "bold";

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries3D());
        series.dataFields.valueY = "ventas";
        series.dataFields.categoryX = "servicio";
        series.name = "Ventas";
        series.tooltipText = "{categoryX}: [bold]{valueY}[/]";
        series.columns.template.fillOpacity = .8;
        series.columns.template.propertyFields.fill = "color";

        var columnTemplate = series.columns.template;
        columnTemplate.strokeWidth = 2;
        columnTemplate.strokeOpacity = 1;
        columnTemplate.stroke = am4core.color("#FFFFFF");

        chart.cursor = new am4charts.XYCursor();
        chart.cursor.lineX.strokeOpacity = 0;
        chart.cursor.lineY.strokeOpacity = 0;

        // Enable export
        chart.exporting.menu = new am4core.ExportMenu();

    }); // end am4core.ready()

}

function graficoProductoMasVendido(Data) {

    var cantidad = Data.length;

    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    var chart = am4core.create("chartdiv4", am4charts.XYChart);
    chart.padding(40, 40, 40, 40);

    var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.dataFields.category = "network";
    categoryAxis.renderer.minGridDistance = 1;
    categoryAxis.renderer.inversed = true;
    categoryAxis.renderer.grid.template.disabled = true;
    categoryAxis.title.text = "Productos Más Vendidos";


    var valueAxis = chart.xAxes.push(new am4charts.ValueAxis());
    valueAxis.min = 0;
    valueAxis.title.text = "Cantidades Vendidas";
    valueAxis.title.fontWeight = "bold";

    var DataServicio = new Array(cantidad);

    for (var i = 0; i < cantidad; i++) {

        DataServicio = {
            "network": Data[i]['NombreProducto'].substr(0, 25),
            "MAU": parseFloat(Data[i]['Cantidad'])
        };
        chart.data.push(DataServicio);
    }


    var series = chart.series.push(new am4charts.ColumnSeries());
    series.dataFields.categoryY = "network";
    series.dataFields.valueX = "MAU";
    series.tooltipText = "{valueX.value}";
    series.columns.template.strokeOpacity = 0;
    series.columns.template.column.cornerRadiusBottomRight = 5;
    series.columns.template.column.cornerRadiusTopRight = 5;
    //series.columns.template.column.tooltipText = "{name}: {valueX.value}";

    var labelBullet = series.bullets.push(new am4charts.LabelBullet());
    labelBullet.label.horizontalCenter = "left";
    labelBullet.label.dx = 10;
    labelBullet.label.text = "{values.valueX.workingValue.formatNumber('#.0as')}";
    labelBullet.locationX = 1;

    // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set


    series.columns.template.adapter.add("fill", function (fill, target) {
        return chart.colors.getIndex(target.dataItem.index);
    });

    categoryAxis.sortBySeries = series;


}

function graficoVentaXTipoServicio(Data) {

    var cantidad = Data.length;

    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("chartdiv1", am4charts.PieChart);

        var DataTipoServicio = new Array(cantidad);

        for (var i = 0; i < cantidad; i++) {

            DataTipoServicio = {
                "country": Data[i]['TipoServicio'],
                "litres": parseFloat(Data[i]['MontoServicio']),
            };
            chart.data.push(DataTipoServicio);
        }


        // Add and configure Series
        var pieSeries = chart.series.push(new am4charts.PieSeries());
        pieSeries.dataFields.value = "litres";
        pieSeries.dataFields.category = "country";
        pieSeries.slices.template.stroke = am4core.color("#fff");
        pieSeries.slices.template.strokeWidth = 2;
        pieSeries.slices.template.strokeOpacity = 1;

        // This creates initial animation
        pieSeries.hiddenState.properties.opacity = 1;
        pieSeries.hiddenState.properties.endAngle = -90;
        pieSeries.hiddenState.properties.startAngle = -90;

    }); // end am4core.ready()

}

function graficoVentasXSucursal(Data) {
    console.log(Data);
    var cantidad = Data.length;

    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        var chart = am4core.create("chartdiv2", am4charts.PieChart);
        chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

        var DataTipoServicio = new Array(cantidad);

        for (var i = 0; i < cantidad; i++) {

            DataTipoServicio = {
                country: Data[i]['Sucursal'],
                value: parseFloat(Data[i]['MontoServicio'])
            };
            chart.data.push(DataTipoServicio);
        }


        //chart.data = [
        //    {
        //        country: "España",
        //        value: 401
        //    },
        //    {
        //        country: "Ayachucho",
        //        value: 300
        //    },
        //    {
        //        country: "San Isidro",
        //        value: 200
        //    }
        //];
        chart.radius = am4core.percent(70);
        chart.innerRadius = am4core.percent(40);
        chart.startAngle = 180;
        chart.endAngle = 360;

        var series = chart.series.push(new am4charts.PieSeries());
        series.dataFields.value = "value";
        series.dataFields.category = "country";

        series.slices.template.cornerRadius = 10;
        series.slices.template.innerCornerRadius = 7;
        series.slices.template.draggable = true;
        series.slices.template.inert = true;
        series.alignLabels = false;

        series.hiddenState.properties.startAngle = 90;
        series.hiddenState.properties.endAngle = 90;

        chart.legend = new am4charts.Legend();

    }); // end am4core.ready()

}

function graficoVendedoresTop(Data) {

    var cantidad = Data.length;
    // Themes begin
    am4core.useTheme(am4themes_animated);
    // Themes end

    /**
     * Chart design taken from Samsung health app
     */

    var chart = am4core.create("chartdiv5", am4charts.XYChart);
    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

    chart.paddingBottom = 30;

    var DataServicio = new Array(cantidad);

    for (var i = 0; i < cantidad; i++) {

        DataServicio = {
            "name": Data[i]['Colaborador'].substr(0, 25),
            "steps": parseFloat(Data[i]['TotalVenta']),
            //"href": Data[i]['Foto'],
            "href": "https://www.amcharts.com/wp-content/uploads/2019/04/monica.jpg"
        };
        chart.data.push(DataServicio);
    }

    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "name";
    categoryAxis.renderer.grid.template.strokeOpacity = 0;
    categoryAxis.renderer.minGridDistance = 10;
    categoryAxis.renderer.labels.template.dy = 35;
    categoryAxis.renderer.tooltip.dy = 35;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.renderer.inside = true;
    valueAxis.renderer.labels.template.fillOpacity = 0.3;
    valueAxis.renderer.grid.template.strokeOpacity = 0;
    valueAxis.min = 0;
    valueAxis.cursorTooltipEnabled = false;
    valueAxis.renderer.baseGrid.strokeOpacity = 0;

    var series = chart.series.push(new am4charts.ColumnSeries);
    series.dataFields.valueY = "steps";
    series.dataFields.categoryX = "name";
    series.tooltipText = "{valueY.value}";
    series.tooltip.pointerOrientation = "vertical";
    series.tooltip.dy = - 6;
    series.columnsContainer.zIndex = 100;

    var columnTemplate = series.columns.template;
    columnTemplate.width = am4core.percent(50);
    columnTemplate.maxWidth = 66;
    columnTemplate.column.cornerRadius(60, 60, 10, 10);
    columnTemplate.strokeOpacity = 0;

    series.heatRules.push({ target: columnTemplate, property: "fill", dataField: "valueY", min: am4core.color("#e5dc36"), max: am4core.color("#5faa46") });
    series.mainContainer.mask = undefined;

    var cursor = new am4charts.XYCursor();
    chart.cursor = cursor;
    cursor.lineX.disabled = true;
    cursor.lineY.disabled = true;
    cursor.behavior = "none";

    var bullet = columnTemplate.createChild(am4charts.CircleBullet);
    bullet.circle.radius = 30;
    bullet.valign = "bottom";
    bullet.align = "center";
    bullet.isMeasured = true;
    bullet.mouseEnabled = false;
    bullet.verticalCenter = "bottom";
    bullet.interactionsEnabled = false;

    var hoverState = bullet.states.create("hover");
    var outlineCircle = bullet.createChild(am4core.Circle);
    outlineCircle.adapter.add("radius", function (radius, target) {
        var circleBullet = target.parent;
        return circleBullet.circle.pixelRadius + 10;
    });

    var image = bullet.createChild(am4core.Image);
    image.width = 60;
    image.height = 60;
    image.horizontalCenter = "middle";
    image.verticalCenter = "middle";
    image.propertyFields.href = "href";

    image.adapter.add("mask", function (mask, target) {
        var circleBullet = target.parent;
        return circleBullet.circle;
    });

    var previousBullet;
    chart.cursor.events.on("cursorpositionchanged", function (event) {
        var dataItem = series.tooltipDataItem;

        if (dataItem.column) {
            var bullet = dataItem.column.children.getIndex(1);

            if (previousBullet && previousBullet !== bullet) {
                previousBullet.isHover = false;
            }

            if (previousBullet !== bullet) {

                var hs = bullet.states.getKey("hover");
                hs.properties.dy = -bullet.parent.pixelHeight + 30;
                bullet.isHover = true;

                previousBullet = bullet;
            }
        }
    });
}

function IBReporteVentaMensualPorAnio(Data) {


    var cantidad = Data.length;

    var DataServicio = new Array(12);




    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        // Create chart instance
        var chart = am4core.create("chartdiv6", am4charts.XYChart);
        chart.scrollbarX = new am4core.Scrollbar();

        for (var i = 1; i <= 12; i++) {

            var Mes = ObtenerMes(i);
            var TotalVenta = 0;

            for (var j = 0; j < cantidad; j++) {

                if (i === parseInt(Data[j]['IDMes'])) {
                    TotalVenta = parseFloat(Data[j]['TotalVenta']);
                }
            }

            DataServicio = {
                "country": Mes,
                "visits": parseFloat(TotalVenta)
            };
            chart.data.push(DataServicio);
        }

        // Create axes
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "country";
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.renderer.minGridDistance = 30;
        categoryAxis.renderer.labels.template.horizontalCenter = "right";
        categoryAxis.renderer.labels.template.verticalCenter = "middle";
        categoryAxis.renderer.labels.template.rotation = 270;
        categoryAxis.tooltip.disabled = true;
        categoryAxis.renderer.minHeight = 110;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.renderer.minWidth = 50;

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries());
        series.sequencedInterpolation = true;
        series.dataFields.valueY = "visits";
        series.dataFields.categoryX = "country";
        series.tooltipText = "[{categoryX}: bold]{valueY}[/]";
        series.columns.template.strokeWidth = 0;

        series.tooltip.pointerOrientation = "vertical";

        series.columns.template.column.cornerRadiusTopLeft = 10;
        series.columns.template.column.cornerRadiusTopRight = 10;
        series.columns.template.column.fillOpacity = 0.8;

        // on hover, make corner radiuses bigger
        var hoverState = series.columns.template.column.states.create("hover");
        hoverState.properties.cornerRadiusTopLeft = 0;
        hoverState.properties.cornerRadiusTopRight = 0;
        hoverState.properties.fillOpacity = 1;

        series.columns.template.adapter.add("fill", function (fill, target) {
            return chart.colors.getIndex(target.dataItem.index);
        });

        // Cursor
        chart.cursor = new am4charts.XYCursor();

    }); // end am4core.ready()


}

function IBReporteVentaDiariaPorMes(Data) {

    var cantidad = Data.length;
    var dias = diasEnUnMes("DICIEMBRE", 2020);
    console.log("Nro Dias: " + dias);
    var DataServicio = new Array(dias);

    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end
        var chart = am4core.create("chartdiv7", am4charts.XYChart);
        chart.hiddenState.properties.opacity = 0; // this makes initial fade in effect

        for (var i = 1; i <= dias; i++) {


            var TotalVenta = 0;

            for (var j = 0; j < cantidad; j++) {

                if (i === parseInt(Data[j]['IDDia'])) {
                    TotalVenta = parseFloat(Data[j]['TotalVenta']);
                }
            }

            DataServicio = {
                "country": i,
                "value": parseFloat(TotalVenta)
            };
            chart.data.push(DataServicio);
        }


        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.dataFields.category = "country";
        categoryAxis.renderer.minGridDistance = 40;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

        var series = chart.series.push(new am4charts.CurvedColumnSeries());
        series.dataFields.categoryX = "country";
        series.dataFields.valueY = "value";
        series.tooltipText = "{valueY.value}"
        series.columns.template.strokeOpacity = 0;
        series.columns.template.tension = 1;

        series.columns.template.fillOpacity = 0.75;

        var hoverState = series.columns.template.states.create("hover");
        hoverState.properties.fillOpacity = 1;
        hoverState.properties.tension = 0.8;

        chart.cursor = new am4charts.XYCursor();

        // Add distinctive colors for each column using adapter
        series.columns.template.adapter.add("fill", function (fill, target) {
            return chart.colors.getIndex(target.dataItem.index);
        });

        chart.scrollbarX = new am4core.Scrollbar();
        chart.scrollbarY = new am4core.Scrollbar();

    }); // end am4core.ready()




}

function IBReporteMetaAnualPorSucursal(Data) {

    var nDiv = 8;

    if (Data.length > 0) {

        for (var i = 0; i < Data.length; i++) {

            var MontoMeta = parseFloat(Data[i]['MontoMeta']);
            var TotalVenta = parseFloat(Data[i]['TotalVenta']);
            var PorcentajeIndicador = 100 * (parseFloat(TotalVenta) / parseFloat(MontoMeta));
            var PorcentajeGauge = PorcentajeIndicador;
            if (PorcentajeIndicador > 100) {
                PorcentajeGauge = 100;
            }

            $("#lblGrafico"+ nDiv +"TotalVenta").text(TotalVenta.toFixed(2) + " (" + PorcentajeIndicador.toFixed() + "%) ");
            $("#lblGrafico" + nDiv + "TotalMeta").text(MontoMeta.toFixed(2));
            $("#lblGrafico" + nDiv + "Anio").text(Data[i]['Anio']);
            $("#lblGrafico" + nDiv + "Sucursal").text(Data[i]['Sucursal']);

            am4core.ready(function () {

                am4core.useTheme(am4themes_animated);
                // create chart
                var chart = am4core.create("chartdiv"+nDiv, am4charts.GaugeChart);
                chart.innerRadius = -15;

                var axis = chart.xAxes.push(new am4charts.ValueAxis());
                axis.min = 0;
                axis.max = 100;
                axis.strictMinMax = true;

                var colorSet = new am4core.ColorSet();

                var gradient = new am4core.LinearGradient();
                gradient.stops.push({ color: am4core.color("red") })
                gradient.stops.push({ color: am4core.color("yellow") })
                gradient.stops.push({ color: am4core.color("green") })

                axis.renderer.line.stroke = gradient;
                axis.renderer.line.strokeWidth = 35;
                axis.renderer.line.strokeOpacity = 1;

                axis.renderer.grid.template.disabled = true;

                var hand = chart.hands.push(new am4charts.ClockHand());
                hand.radius = am4core.percent(97);
                hand.showValue(PorcentajeGauge);

            }); // end am4core.ready()


            nDiv++;
        }

        
    }

    

    //am4core.ready(function () {

    //    am4core.useTheme(am4themes_animated);
    //    // create chart
    //    var chart = am4core.create("chartdiv9", am4charts.GaugeChart);
    //    chart.innerRadius = -15;

    //    var axis = chart.xAxes.push(new am4charts.ValueAxis());
    //    axis.min = 0;
    //    axis.max = 100;
    //    axis.strictMinMax = true;

    //    var colorSet = new am4core.ColorSet();

    //    var gradient = new am4core.LinearGradient();
    //    gradient.stops.push({ color: am4core.color("red") })
    //    gradient.stops.push({ color: am4core.color("yellow") })
    //    gradient.stops.push({ color: am4core.color("green") })

    //    axis.renderer.line.stroke = gradient;
    //    axis.renderer.line.strokeWidth = 35;
    //    axis.renderer.line.strokeOpacity = 1;

    //    axis.renderer.grid.template.disabled = true;

    //    var hand = chart.hands.push(new am4charts.ClockHand());
    //    hand.radius = am4core.percent(97);
    //    hand.showValue(PorcentajeGauge);

    //}); // end am4core.ready()

    //am4core.ready(function () {

    //    am4core.useTheme(am4themes_animated);
    //    // create chart
    //    var chart = am4core.create("chartdiv10", am4charts.GaugeChart);
    //    chart.innerRadius = -15;

    //    var axis = chart.xAxes.push(new am4charts.ValueAxis());
    //    axis.min = 0;
    //    axis.max = 100;
    //    axis.strictMinMax = true;

    //    var colorSet = new am4core.ColorSet();

    //    var gradient = new am4core.LinearGradient();
    //    gradient.stops.push({ color: am4core.color("red") })
    //    gradient.stops.push({ color: am4core.color("yellow") })
    //    gradient.stops.push({ color: am4core.color("green") })

    //    axis.renderer.line.stroke = gradient;
    //    axis.renderer.line.strokeWidth = 35;
    //    axis.renderer.line.strokeOpacity = 1;

    //    axis.renderer.grid.template.disabled = true;

    //    var hand = chart.hands.push(new am4charts.ClockHand());
    //    hand.radius = am4core.percent(97);
    //    hand.showValue(PorcentajeGauge);

    //}); // end am4core.ready()
}

function IBReporteMetaAnualPorEmpresa(Data) {

    var nDiv = 11;
    var MontoMeta = parseFloat(Data[0]['MontoMeta']);
    var TotalVenta = parseFloat(Data[0]['TotalVenta']);
    var PorcentajeIndicador = 100 * (parseFloat(TotalVenta) / parseFloat(MontoMeta));
    var PorcentajeGauge = PorcentajeIndicador;
    if (PorcentajeIndicador > 100) {
        PorcentajeGauge = 100;
    }

    $("#lblGrafico" + nDiv + "TotalVenta").text(TotalVenta.toFixed(2) + " (" + PorcentajeIndicador.toFixed() + "%) ");
    $("#lblGrafico" + nDiv + "TotalMeta").text(MontoMeta.toFixed(2));
    $("#lblGrafico" + nDiv + "Anio").text(Data[0]['Anio']);    

    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        var chartMin = -50;
        var chartMax = 100;



        var data = {
            score: 52.7,
            gradingData: [
                {
                    title: "INSOSTENIBLE",
                    color: "#ee1f25",
                    lowScore: -100,
                    highScore: -20
                },
                {
                    title: "VOLÁTIL",
                    color: "#f04922",
                    lowScore: -20,
                    highScore: 0
                },
                {
                    title: "INICIANDO",
                    color: "#fdae19",
                    lowScore: 0,
                    highScore: 20
                },
                {
                    title: "DESARROLLANDO",
                    color: "#f3eb0c",
                    lowScore: 20,
                    highScore: 40
                },
                {
                    title: "MADURANDO",
                    color: "#b0d136",
                    lowScore: 40,
                    highScore: 60
                },
                {
                    title: "SOSTENIBLE",
                    color: "#54b947",
                    lowScore: 60,
                    highScore: 80
                },
                {
                    title: "ALTO RENDIMIENTO",
                    color: "#0f9747",
                    lowScore: 80,
                    highScore: 100
                }
            ]
        };        

        // create chart
        var chart = am4core.create("chartdiv11", am4charts.GaugeChart);
        chart.hiddenState.properties.opacity = 0;
        chart.fontSize = 11;
        chart.innerRadius = am4core.percent(80);
        chart.resizable = true;

        /**
         * Normal axis
         */

        var axis = chart.xAxes.push(new am4charts.ValueAxis());
        axis.min = chartMin;
        axis.max = chartMax;
        axis.strictMinMax = true;
        axis.renderer.radius = am4core.percent(80);
        axis.renderer.inside = true;
        axis.renderer.line.strokeOpacity = 0.1;
        axis.renderer.ticks.template.disabled = false;
        axis.renderer.ticks.template.strokeOpacity = 1;
        axis.renderer.ticks.template.strokeWidth = 0.5;
        axis.renderer.ticks.template.length = 5;
        axis.renderer.grid.template.disabled = true;
        axis.renderer.labels.template.radius = am4core.percent(15);
        axis.renderer.labels.template.fontSize = "0.9em";

        /**
         * Axis for ranges
         */

        var axis2 = chart.xAxes.push(new am4charts.ValueAxis());
        axis2.min = chartMin;
        axis2.max = chartMax;
        axis2.strictMinMax = true;
        axis2.renderer.labels.template.disabled = true;
        axis2.renderer.ticks.template.disabled = true;
        axis2.renderer.grid.template.disabled = false;
        axis2.renderer.grid.template.opacity = 0.5;
        axis2.renderer.labels.template.bent = true;
        axis2.renderer.labels.template.fill = am4core.color("#000");
        axis2.renderer.labels.template.fontWeight = "bold";
        axis2.renderer.labels.template.fillOpacity = 0.3;



        /**
        Ranges
        */

        for (let grading of data.gradingData) {
            var range = axis2.axisRanges.create();
            range.axisFill.fill = am4core.color(grading.color);
            range.axisFill.fillOpacity = 0.8;
            range.axisFill.zIndex = -1;
            range.value = grading.lowScore > chartMin ? grading.lowScore : chartMin;
            range.endValue = grading.highScore < chartMax ? grading.highScore : chartMax;
            range.grid.strokeOpacity = 0;
            range.stroke = am4core.color(grading.color).lighten(-0.1);
            range.label.inside = true;
            range.label.text = grading.title.toUpperCase();
            range.label.inside = true;
            range.label.location = 0.5;
            range.label.inside = true;
            range.label.radius = am4core.percent(10);
            range.label.paddingBottom = -5; // ~half font size
            range.label.fontSize = "0.9em";
        }

        var matchingGrade = lookUpGrade(data.score, data.gradingData);

        /**
         * Label 1
         */

        var label = chart.radarContainer.createChild(am4core.Label);
        label.isMeasured = false;
        label.fontSize = "6em";
        label.x = am4core.percent(50);
        label.paddingBottom = 15;
        label.horizontalCenter = "middle";
        label.verticalCenter = "bottom";
        //label.dataItem = data;
        label.text = data.score.toFixed(1);
        //label.text = "{score}";
        label.fill = am4core.color(matchingGrade.color);

        /**
         * Label 2
         */

        var label2 = chart.radarContainer.createChild(am4core.Label);
        label2.isMeasured = false;
        label2.fontSize = "2em";
        label2.horizontalCenter = "middle";
        label2.verticalCenter = "bottom";
        label2.text = matchingGrade.title.toUpperCase();
        label2.fill = am4core.color(matchingGrade.color);


        /**
         * Hand
         */

        var hand = chart.hands.push(new am4charts.ClockHand());
        hand.axis = axis2;
        hand.innerRadius = am4core.percent(55);
        hand.startWidth = 8;
        hand.pin.disabled = true;
        hand.value = data.score;
        hand.fill = am4core.color("#444");
        hand.stroke = am4core.color("#000");

        hand.events.on("positionchanged", function () {
            label.text = axis2.positionToValue(hand.currentPosition).toFixed(1);
            var value2 = axis.positionToValue(hand.currentPosition);
            var matchingGrade = lookUpGrade(axis.positionToValue(hand.currentPosition), data.gradingData);
            label2.text = matchingGrade.title.toUpperCase();
            label2.fill = am4core.color(matchingGrade.color);
            label2.stroke = am4core.color(matchingGrade.color);
            label.fill = am4core.color(matchingGrade.color);
        });

        
        hand.showValue(PorcentajeGauge, 1000, am4core.ease.cubicOut);
        //setInterval(function () {
        //    var value = chartMin + Math.random() * (chartMax - chartMin);
        //    hand.showValue(value, 1000, am4core.ease.cubicOut);
        //}, 2000);

    }); // end am4core.ready()
}



//FUNCIONES 
 
function lookUpGrade(lookupScore, grades) {
    // Only change code below this line
    for (var i = 0; i < grades.length; i++) {
        if (
            grades[i].lowScore < lookupScore &&
            grades[i].highScore >= lookupScore
        ) {
            return grades[i];
        }
    }
    return null;
}

function etiquetaTotalCompraVenta(Data) {

    var cantidad = Data.length;

    for (var i = 0; i < cantidad; i++) {
        var TotalVenta = Number.parseFloat(Data[i]['TotalVenta']);
        var TotalCompra = Number.parseFloat(Data[i]['TotalCompra']);
        var TotalUtilidad = Number.parseFloat(Data[i]['Utilidad']);
        $("#TotalVentas").html(ReplaceNumberWithCommas(TotalVenta.toFixed(2)));
        $("#TotalCompras").html(ReplaceNumberWithCommas(TotalCompra.toFixed(2)));
        $("#TotalUtilidad").html(ReplaceNumberWithCommas(TotalUtilidad.toFixed(2)));
    }
}

function ReplaceNumberWithCommas(yourNumber) {
    //Seperates the components of the number
    var n = yourNumber.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}

function ObtenerMes(IDMes) {

    var Mes = "";

    switch (IDMes) {

        case 1:
            Mes = "Enero";
            break;
        case 2:
            Mes = "Febrero";
            break;
        case 3:
            Mes = "Marzo";
            break;
        case 4:
            Mes = "Abril";
            break;
        case 5:
            Mes = "Mayo";
            break;
        case 6:
            Mes = "Junio";
            break;
        case 7:
            Mes = "Julio";
            break;
        case 8:
            Mes = "Agosto";
            break;
        case 9:
            Mes = "Setiembre";
            break;
        case 10:
            Mes = "Octubre";
            break;
        case 11:
            Mes = "Noviembre";
            break;
        case 12:
            Mes = "Diciembre";
            break;
    }
    return Mes;
}

function diasEnUnMes(mes, anio) {
    mes = mes.toUpperCase();
    var meses = ["ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"];
    return new Date(anio, meses.indexOf(mes) + 1, 0).getDate();
}

/****/

