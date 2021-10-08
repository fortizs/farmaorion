<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteResumenCaja.aspx.cs" Inherits="Farmacia.Reportes.ReporteResumenCaja" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte Resumen Caja</h2>
					</div>
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnPagoListar" runat="server" DefaultButton="btnBuscar">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlBIDSucursal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBIDSucursal_SelectedIndexChanged"></asp:DropDownList>
											<asp:Literal ID="FechaPago" runat="server" Visible="false"></asp:Literal>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Caja:</label>
											<asp:DropDownList ID="ddlBIDCaja" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Cajero:</label>
											<asp:DropDownList ID="ddlBIDCajero" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Apertura:</label>
											<asp:TextBox ID="txtBFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Cierre:</label>
											<asp:TextBox ID="txtBFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></asp:Button>
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('RES_CAJA');"><i class='icon-cloud-download'></i>Excel</a>
											<asp:LinkButton ID="lnkImprimirPDF" Visible="false" runat="server" OnClick="lnkImprimirPDF_Click" CssClass="btn btn-danger btn-danger" Width="80%"><i class="icon-printer"></i> Imprimir PDF</a></asp:LinkButton>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<asp:Panel ID="pnListarGrid" runat="server" Visible="false">
									<div class="col-md-12">
										<div class="table-responsive" style="background-color: white">
											<asp:GridView ID="gvListaResumenCaja" runat="server" DataKeyNames="IDCaja" OnPageIndexChanging="gvListaResumenCaja_PageIndexChanging" AllowPaging="True" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Caja Mecanica" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("CajaMecanica") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Apertura" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
														<ItemTemplate>
															<%# Eval("FechaApertura", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Cierre" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
														<ItemTemplate>
															<%# Eval("FechaCierre", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Us. Open" ItemStyle-HorizontalAlign="left" ItemStyle-Width="12%">
														<ItemTemplate>
															<%# Eval("UsuarioApertura") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Us. Close" ItemStyle-HorizontalAlign="left" ItemStyle-Width="12%">
														<ItemTemplate>
															<%# Eval("UsuarioCierre") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Contado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Contado", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Calculado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Calculado", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Diferencia", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Retiro" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Retiro", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("NombreEstado") %>
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
			var pIDCaja = $("#<%= ddlBIDCaja.ClientID %>").val();
			var pIDCajero = $("#<%= ddlBIDCajero.ClientID %>").val();
			var pFechaInicio = $("#<%= txtBFechaInicio.ClientID %>").val();
			var pFechaFin = $("#<%= txtBFechaFin.ClientID %>").val();
			$("#ifrmGeneraExcel").attr("src", "");
			$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pIDCaja=" + pIDCaja + "&pIDCajero=" + pIDCajero + "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
			return false;
		}
	</script>
</asp:Content>
