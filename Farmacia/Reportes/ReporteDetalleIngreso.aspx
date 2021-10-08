<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteDetalleIngreso.aspx.cs" Inherits="Farmacia.Reportes.ReporteDetalleIngreso" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte Detalle de Ingresos</h2>
					</div>

					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label for="tags">Sucursal:</label>
											<asp:DropDownList ID="ddlIDSucursal" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label for="tags">Fecha Inicio:</label>
											<asp:TextBox ID="txtFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label for="tags">Fecha Fin:</label>
											<asp:TextBox ID="txtFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
											<a id="A1" class="btn btn-primary btn-xs" href="javascript:generar_excel('DI');"><i class='icon-cloud-download'></i>Exportar</a>
										</div>
									</div> 
								</div>

								<div class="row" style="background-color: #f1f3f6">
									<div class="espacio"></div>
									<div class="espacio"></div>
									<div class="col-md-12" style="text-align: center; font-size: 15px"><b>REPORTE DETALLE DE INGRESOS</b></div>
									<div class="espacio"></div>
									<div class="espacio"></div>
									<div class="col-md-3"></div>
									<div class="col-md-2">
										<b>SUCURSAL: </b>
										<asp:Literal ID="ltSucursal" runat="server"></asp:Literal>
									</div>
									<div class="col-md-2">
										<b>FECHA INICIO: </b>
										<asp:Literal ID="ltFechaInicio" runat="server"></asp:Literal>
									</div>
									<div class="col-md-2">
										<b>FECHA FIN: </b>
										<asp:Literal ID="ltFechaFin" runat="server"></asp:Literal>
									</div>
									<br />
									<div class="separador"></div>
									<div class="col-md-12">

										<div class="table-responsive" style="background-color: white">
											<asp:GridView ID="gvLista" runat="server" DataKeyNames="Codigo" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Ítem" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
														<ItemTemplate>
															<%# (Container.DataItemIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="TipoComprobante" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("TipoComprobante") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="SerieNumero" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("SerieNumero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="FechaEmision" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("FechaEmision") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="NumeroDocumento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("NumeroDocumento") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="RazonSocial" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
														<ItemTemplate>
															<%# Eval("RazonSocial") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="TotalVenta" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("TotalVenta") %>
														</ItemTemplate>
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
									</div>
								</div>
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
			var pIDSucursal = $("#<%= ddlIDSucursal.ClientID %>").val();
			var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
			var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();
			$("#ifrmGeneraExcel").attr("src", "");
			$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
			return false;
		}
	</script>
</asp:Content>


