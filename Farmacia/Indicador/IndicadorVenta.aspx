<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndicadorVenta.aspx.cs" Inherits="Farmacia.Indicador.IndicadorVenta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<script src="https://code.highcharts.com/highcharts.js"></script>
	<script src="https://code.highcharts.com/modules/exporting.js"></script>
	<script src="https://code.highcharts.com/modules/accessibility.js"></script>
	<script src='<%= ResolveClientUrl("~/Indicador/IndicadorVenta.js") %>' type="text/javascript"></script>



	<style>
		[id^='chartdiv'] {
			width: 100%;
			height: 400px;
		}

		#chartdiv8, #chartdiv9, #chartdiv10 {
			height: 300px !important;
		}


		.text-semibold {
			font-weight: bold;
			font-size: 1.2em;
		}

		.text-2x {
			font-weight: bolder;
			font-size: 1.8em;
		}

		.panel-colorful {
			background-color: #cacaca;
			color: black;
		}

		.pad-all {
			background-color: whitesmoke;
			color: #22a6b3;
		}

		.panel-flat {
			border: 1px solid #e5e5e5;
		}

		.titulo-grafico {
			background-color: #cacaca;
			color: black;
			padding: 3px;
			text-align: center;
			font-weight: bold;
		}

		Style Attribute {
			color: black;
		}
	</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Indicadores de Ventas</h2>
					</div>
					<asp:HiddenField ID="hdfIDUsuario" runat="server" Value="0" />
					<asp:HiddenField ID="hdfIDEmpresa" runat="server" Value="0" />

					<div class="row">
						<div class="col-md-3">
							<div class="form-group">
								<label>Sucursal</label>
								<asp:DropDownList ID="ddlSucursal" runat="server" />
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label>Cliente</label>
								<asp:DropDownList ID="ddlCliente" runat="server" />
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Inicio:</label>
								<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Fin:</label>
								<asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta"></label> 
								<button type="button" class="btn btn-primary" onclick="generarDashboard()">Consultar</button>
							</div>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-sm-6 col-lg-3">
							<div class="panel panel-dark panel-colorful" style="border-color: #22a6b3; color: white; background: #00b8ff;">
								<div class="panel-body-etiqueta text-center" style="padding: 10px;">
									<i class="icon-coins icon-5x" style="font-size: xx-large"></i>
								</div>
								<div class="pad-all text-center">
									<p class="text-semibold text-lg mar-no">Total Ventas</p>
									<p class="text-2x text-bold mar-no" id="TotalVentas">0.00</p>
								</div>
							</div>
						</div>
						<div class="col-sm-6 col-lg-3">
							<div class="panel panel-dark panel-colorful" style="border-color: #22a6b3; color: white; background: red;">
								<div class="panel-body-etiqueta text-center" style="padding: 10px;">
									<i class="icon-basket" style="font-size: xx-large"></i>
								</div>
								<div class="pad-all text-center">
									<p class="text-semibold mar-no">Total Gastos</p>
									<p class="text-2x text-bold mar-no" id="TotalCompras">0.00</p>
								</div>
							</div>
						</div>
						<div class="col-sm-6 col-lg-3">
							<div class="panel panel-dark panel-colorful" style="border-color: #22a6b3; color: white; background: lime;">
								<div class="panel-body-etiqueta text-center" style="padding: 10px;">
									<i class="icon-basket" style="font-size: xx-large"></i>
								</div>
								<div class="pad-all text-center">
									<p class="text-semibold mar-no">Utilidad</p>
									<p class="text-2x text-bold mar-no" id="TotalUtilidad">0.00</p>
								</div>
							</div>
						</div>
						<div class="col-sm-6 col-lg-3">
							<div class="table-responsive">
								<asp:GridView ID="gvFormaPagoListar" runat="server" DataKeyNames="IDFormaPago">
									<Columns>
										<asp:TemplateField HeaderText="FormaPago" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("FormaPago") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
											<ItemTemplate>
												S/ <%# Eval("Total","{0:N}") %>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>


					<div class="row">
						<div class="col-md-6">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">Porcentaje de Ventas por Categoría</p>
									<div id="chartdiv1"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">Porcentaje de Ventas por Sucursal</p>
									<div id="chartdiv2"></div>
								</div>
							</div>
						</div>


					</div>
					<br />
					<div class="row">
						<div class="col-md-4">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Meta Anual por Sucursal - <span id="lblGrafico8Sucursal"></span>
									</p>
									<div id="chartdiv8"></div>
									<div class="row text-center">
										<div class="col-md-12">
											Año: <strong>
												<label id="lblGrafico8Anio"></label>
											</strong>
										</div>
										<div class="col-md-12">
											Monto Meta: <strong>
												<label id="lblGrafico8TotalMeta"></label>
											</strong>|  
                                            Total Venta: <strong>
												<label id="lblGrafico8TotalVenta"></label>
											</strong>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="col-md-4">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Meta Anual por Sucursal - <span id="lblGrafico9Sucursal"></span>
									</p>
									<div id="chartdiv9"></div>
									<div class="row text-center">
										<div class="col-md-12">
											Año: <strong>
												<label id="lblGrafico9Anio"></label>
											</strong>
										</div>
										<div class="col-md-12">
											Monto Meta: <strong>
												<label id="lblGrafico9TotalMeta"></label>
											</strong>|  
                                            Total Venta: <strong>
												<label id="lblGrafico9TotalVenta"></label>
											</strong>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="col-md-4">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Meta Anual por Sucursal - <span id="lblGrafico10Sucursal"></span>
									</p>
									<div id="chartdiv10"></div>
									<div class="row text-center">
										<div class="col-md-12">
											Año: <strong>
												<label id="lblGrafico10Anio"></label>
											</strong>
										</div>
										<div class="col-md-12">
											Monto Meta: <strong>
												<label id="lblGrafico10TotalMeta"></label>
											</strong>|  
                                            Total Venta: <strong>
												<label id="lblGrafico10TotalVenta"></label>
											</strong>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-12">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Meta Anual de Ventas en la Empresa
									</p>
									<div id="chartdiv11"></div>
									<div class="row text-center">
										<div class="col-md-12">
											Año: <strong>
												<label id="lblGrafico11Anio"></label>
											</strong>
										</div>
										<div class="col-md-12">
											Monto Meta: <strong>
												<label id="lblGrafico11TotalMeta"></label>
											</strong>|  
                                            Total Venta: <strong>
												<label id="lblGrafico11TotalVenta"></label>
											</strong>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-6 ">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">Ranking Productos Más Vendidos</p>
									<div id="chartdiv4"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6 ">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">Ranking de Vendedores</p>
									<div id="chartdiv5"></div>
								</div>
							</div>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-12">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Ventas Mensuales por Año
									</p>
									<div class="row text-center">
										<div class="col-md-9"></div>
										<div class="col-md-1">
											Año
                                            <input id="Grafico6_Anio" class="form-control" type="number" value="" placeholder="2020" />
										</div>
										<div class="col-md-2">
											<br />
											<button type="button" class="btn btn-success" onclick="btnReporteVentaMensualPorAnio()">Actualizar</button>
										</div>
									</div>
									<div id="chartdiv6"></div>
								</div>
							</div>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-12">
							<div class="panel panel-flat">
								<div class="panel-body">
									<p class="titulo-grafico">
										Ventas Diaria por Mes y Año
									</p>
									<div class="row text-center">
										<div class="col-md-7"></div>
										<div class="col-md-2">
											Mes
                                            <select id="Grafico7_Mes" class="form-control"></select>
										</div>
										<div class="col-md-1">
											Año
                                            <input id="Grafico7_Anio" class="form-control" type="number" value="" placeholder="2020" />
										</div>
										<div class="col-md-2">
											<br />
											<button type="button" class="btn btn-success" onclick="btnReporteVentaDiariaPorMes()">Actualizar</button>
										</div>
									</div>
									<div id="chartdiv7"></div>
								</div>
							</div>
						</div>
					</div>

				</div>
			</div>
		</div>

		<asp:HiddenField ID="hfIDUsuario" runat="server" ClientIDMode="Static" Value="0" />
		<asp:HiddenField ID="hfMesActual" runat="server" ClientIDMode="Static" Value="1" />

		<!-- Resources -->
		<%--<script src="IndicadorVenta.js"></script>--%>
		<script src="https://www.amcharts.com/lib/4/core.js"></script>
		<script src="https://www.amcharts.com/lib/4/charts.js"></script>
		<script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>

		<!-- Chart code -->
		<script type="text/javascript">
			$(document).ready(function () {
				generarDashboard();
			});

			function obtenerFiltros() {
				var IDEmpresa = $("#<%= hdfIDEmpresa.ClientID %>").val();
				var IDSucursal = $("#<%= ddlSucursal.ClientID %>").val();
				var FechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
				var FechaFin = $("#<%= txtFechaFin.ClientID %>").val();
				var IDCliente = $("#<%= ddlCliente.ClientID %>").val();
				var IDUsuario = $("#<%= hdfIDUsuario.ClientID %>").val();
				var Grafico6_Anio = $("#Grafico6_Anio").val();
				var Grafico7_Anio = $("#Grafico7_Anio").val();
				var Grafico7_Mes = $("#Grafico7_Mes").val();
				var IDTipoMeta = 1;
				var filtros_array = [IDEmpresa, IDSucursal, FechaInicio, FechaFin, IDCliente, IDUsuario, Grafico6_Anio, Grafico7_Anio, Grafico7_Mes, IDTipoMeta];
				return filtros_array;
			}

			function obtenerMeses() {

				var objMeses = [];

				for (i = 1; i <= 12; i++) {
					var nombreMes = ObtenerMes(i);
					var meses = {
						IDMes: i,
						Mes: nombreMes
					}
					objMeses.push(meses);
				}
				return objMeses;
			}

			function poblarMesGraficoVentasDiarias() {

				var meses = obtenerMeses();
				$.each(meses, function (ind, mes) {
					$("#Grafico7_Mes").append('<option value="' + mes['IDMes'] + '">' + mes['Mes'] + '</option>');
				});
			}



			function generarDashboard() { 
				poblarMesGraficoVentasDiarias();

				var d = new Date();
				var anioActual = d.getFullYear();
				var mesActual = d.getMonth() + 1;
				console.log("Anio actual: " + anioActual);
				$("#Grafico7_Mes").val(mesActual);
				$("#Grafico7_Anio").val(anioActual);
				$("#Grafico6_Anio").val(anioActual);

				var Filtros_Array = obtenerFiltros();

				var Filtros = {
					Filtros: Filtros_Array,
				};


				var Filtros_OBJ = JSON.stringify(Filtros);
				ReporteVentaProductos(Filtros_OBJ);
				VentaxTipoServicioListar(Filtros_OBJ);
				ReporteVentaXSucursal(Filtros_OBJ);
				ReporteTotalCompraVenta(Filtros_OBJ);
				ReporteVendedoresTop(Filtros_OBJ);
				ReporteVentaMensualPorAnio(Filtros_OBJ);
				ReporteVentaDiariaPorMes(Filtros_OBJ);
				ReporteMetaAnualPorSucursal(Filtros_OBJ);
				ReporteMetaAnualPorEmpresa(Filtros_OBJ);
			}

			function btnReporteVentaMensualPorAnio() {

				var Filtros_Array = obtenerFiltros();

				var Filtros = {
					Filtros: Filtros_Array,
				};
				var Filtros_OBJ = JSON.stringify(Filtros);
				ReporteVentaMensualPorAnio(Filtros_OBJ);
			}

			function btnReporteVentaDiariaPorMes() {

				var Filtros_Array = obtenerFiltros();

				var Filtros = {
					Filtros: Filtros_Array,
				};
				var Filtros_OBJ = JSON.stringify(Filtros);
				ReporteVentaDiariaPorMes(Filtros_OBJ);
			}

		</script>
</asp:Content>

