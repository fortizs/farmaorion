<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteCompras.aspx.cs" Inherits="Farmacia.Reportes.ReporteCompras" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte de Compras</h2>
					</div>
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:HiddenField ID="hdIDSucursal" runat="server" Value="0" />
							<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label for="tags">Sucursal:</label>
											<asp:DropDownList ID="ddlIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label for="tags">Proveedor:</label>
											<asp:DropDownList ID="ddlIDProveedor" runat="server"></asp:DropDownList>
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
											<asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('COMPRAS');"><i class='icon-cloud-download'></i>Excel</a>
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('COMPRAS_DET');"><i class='icon-cloud-download'></i>Detallado</a>
											<asp:LinkButton ID="lnkImprimirPDF" Visible="false" runat="server" OnClick="lnkImprimirPDF_Click" CssClass="btn btn-danger btn-danger"><i class="icon-printer"></i> PDF</a></asp:LinkButton>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<asp:Panel ID="pnListar" runat="server" Visible="false">
									<div class="col-md-12">
										<div class="table-responsive" style="background-color: white">
											<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDCompras" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="NumeroDocumento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("NumeroDocumento") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
														<ItemTemplate>
															<%# Eval("RazonSocial") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha de Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<ItemTemplate>
															<%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Tipo Comprobante" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("TipoComprobante") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Serie-Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("SerieNumero") %>
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															S/. <%# Eval("TotalCompra", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="FormaPago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("FormaPago") %>
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField HeaderText="EstadoCompra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("EstadoCompra") %>
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField HeaderText="EstadoPago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("EstadoPago") %>
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
			var pIDSucursal = $("#<%= hdIDSucursal.ClientID %>").val();
        	var pIDProveedor = $("#<%= ddlIDProveedor.ClientID %>").val();
        	var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
        	var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();

        	$("#ifrmGeneraExcel").attr("src", "");
        	$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pIDProveedor=" + pIDProveedor +
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
        	return false;
        }
	</script>

</asp:Content>
