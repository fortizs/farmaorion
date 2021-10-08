<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductoxVencer.aspx.cs" Inherits="Farmacia.Almacen.ProductoxVencer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Alerta de Productos por Vencidoy y por Vencer</h2>
					</div>
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
													<asp:TemplateField HeaderText="Codigo Barra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<ItemTemplate>
															<%# Eval("CodigoBarra") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
														<ItemTemplate>															
                                                             <asp:Label runat="server" ID="lblProducto" ForeColor='Red'  Text='<%# Eval("Producto") %>' />
														</ItemTemplate>
													</asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
														<ItemTemplate>
															<%# Eval("Lote.Lote") %>
														</ItemTemplate>
													</asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Lote.CantidadLote", "{0:N}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Unidad Medida" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("UnidadMedida") %>
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField HeaderText="Fabricación" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("Lote.FechaFabricacion", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>															
                                                            <%# Eval("Lote.FechaVencimiento", "{0:dd/MM/yyyy}") %>
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
</asp:Content>


