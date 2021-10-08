<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteLibroCompra.aspx.cs" Inherits="Farmacia.Reportes.ReporteLibroCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	< 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">


	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte de Libro de Compras</h2>
					</div>
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">

								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sucursal</label>
											<asp:DropDownList ID="ddlIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Periodo Año:</label>
											<asp:DropDownList ID="ddlPeriodoAnio" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Periodo Mes:</label>
											<asp:DropDownList ID="ddlPeriodoMes" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-info" OnClick="btnBuscar_Click" />
											<a class="btn btn-success btn-xs" id="A1" href="javascript:generar_excel('LIBRO_COMPRAS');"><i class='icon-cloud-download'></i>Excel </a>
										</div>
									</div>
								</div>
								<div class="table-responsive">
									<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDCompras" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
										<Columns>
											<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaRegistro", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Vencimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
												<ItemTemplate>
													<%# Eval("PROVNumeroDocumento") %><br />
													<%# Eval("PROVRazonSocial") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Tipo Comprobante" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title="Tipo Comprobante"><%# Eval("Sigla") %></span>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie-Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("CPSerieDocumento") %> - <%# Eval("CPNumeroDocumento") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Forma Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FormaPago") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="BaseImponible" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("BaseImponible", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Igv" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("Igv", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("ImporteTotal", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
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
		function generar_excel(pTipoReporte) {
			var pIDSucursal = $("#<%= ddlIDSucursal.ClientID %>").val();
        	var pPeriodo = $("#<%= ddlPeriodoAnio.ClientID %>").val() + $("#<%= ddlPeriodoMes.ClientID %>").val();
        	$("#ifrmGeneraExcel").attr("src", "");
        	$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pTipoReporte=" + pTipoReporte + "&pPeriodo=" + pPeriodo + "&pIDSucursal=" + pIDSucursal);
        	return false;
        }
	</script>


</asp:Content>
