<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlertaProductos.aspx.cs" Inherits="Farmacia.Almacen.AlertaProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Alerta de Productos con Stock Bajo</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upConsulta" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnConsultaStock" runat="server" DefaultButton="lnkBuscarStockProducto">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Sucursal:</label>
									<asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-6">
								<div class="form-group">
									<label>Filtro <strong>(Cod.Barra/Producto)</strong>:</label>
									<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:LinkButton ID="lnkBuscarStockProducto" runat="server" SkinID="ui-link-boton-default" OnClick="lnkBuscarStockProducto_Click"><i class="icon-search4"></i> Buscar </asp:LinkButton>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="table-responsive" style="margin-bottom: 0;">
									<asp:GridView ID="gvLista" runat="server" DataKeyNames="IdStock" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging">
										<Columns>
											<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("Sucursal") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
												<ItemTemplate>
													<%# Eval("Codigo") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
												<ItemTemplate>
													<%# Eval("Producto") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Unidad Medida" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("UnidadMedida") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Stock Mínimo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("StockMinimo") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Stock Actual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("StockActual") %>
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

</asp:Content>


