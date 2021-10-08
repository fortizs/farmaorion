<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteDetalleVenta.aspx.cs" Inherits="Farmacia.Reportes.ReporteDetalleVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte de Ventas Detallada</h2>
					</div>
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnPagoListar" runat="server" DefaultButton="btnBuscarVentas">

								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Colaborador:</label>
											<asp:DropDownList ID="ddlIDColaborador" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Estado:</label>
											<asp:DropDownList ID="ddlBIDEstado" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Estado Cobranza:</label>
											<asp:DropDownList ID="ddlBIDEstadoCobranza" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Filtro:<b>[Serie|Nro.Documento|Cliente]</b></label>
											<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Inicio:</label>
											<asp:TextBox ID="txtBFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Fin:</label>
											<asp:TextBox ID="txtBFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div> 
									<div class="col-md-3">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscarVentas" OnClick="btnBuscarVentas_Click" runat="server" Text="Buscar"></asp:Button>
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('VENTA_DETALLADA');"><i class='icon-cloud-download'></i>Excel</a>
									    </div>
									</div>
								</div>
								<div class="separador"></div>
								<div class="espacio"></div>
								<asp:Panel ID="pnListarGrid" runat="server" Visible="false">
									<div class="col-md-12">
										<div class="table-responsive" style="background-color: white">
											<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDVenta" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="TipoComprobante" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="13%">
														<ItemTemplate>
															<%# Eval("TipoComprobante") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="SerieNumero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("SerieNumero")%>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="NumeroDocumento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("NumeroDocumento") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Cliente") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("FechaVenta", "{0:dd/MM/yyyy HH:mm:ss}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															S/. <%# Eval("TotalVenta", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cajero" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Cajero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Venta" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("EstadoVenta") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cobranza" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("EstadoCobranza") %>
														</ItemTemplate>
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
									</div>
								</asp:Panel>
								<asp:Panel ID="pnImprimirPDF" runat="server" Visible="false">
									<div id="div_iframe" runat="server">
										<iframe src="" id="iframe" runat="server" style="width: 100%; height: 450px; border: none;"></iframe>
									</div>
								</asp:Panel>
								<br />
							</asp:Panel>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<iframe id="ifrmGeneraExcel" src="" style="width: 100%; height: 350px; border: none;"></iframe>
	<script type="text/javascript">
		function ConfigJS() {
		}

		function generar_excel(pTipoReporte) {
			var pIDSucursal = $("#<%= ddlBIDSucursal.ClientID %>").val();
			var pIDColaborador = $("#<%= ddlIDColaborador.ClientID %>").val();
			var pFechaInicio = $("#<%= txtBFechaInicio.ClientID %>").val();
			var pFechaFin = $("#<%= txtBFechaFin.ClientID %>").val();
			var pFiltro = $("#<%= txtFiltro.ClientID %>").val();
			var pIDEstadoVenta = $("#<%= ddlBIDEstado.ClientID %>").val();
			var pIDEstadoCobranza = $("#<%= ddlBIDEstadoCobranza.ClientID %>").val();
			$("#ifrmGeneraExcel").attr("src", "");
			$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pIDColaborador=" + pIDColaborador +
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pIDEstadoVenta=" + pIDEstadoVenta + "&pIDEstadoCobranza=" + pIDEstadoCobranza + "&pFiltro=" + pFiltro + "&pTipoReporte=" + pTipoReporte);
			return false;
		}
	</script>


</asp:Content>
