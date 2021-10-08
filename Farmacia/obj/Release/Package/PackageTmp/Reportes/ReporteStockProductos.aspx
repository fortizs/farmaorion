<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteStockProductos.aspx.cs" Inherits="Farmacia.Reportes.ReporteStockProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Reporte de Productos</h2>
					</div>
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">

						<ContentTemplate>
							<asp:Panel ID="pnStockListar" runat="server" DefaultButton="btnBuscarStock">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Categoria:</label>
											<asp:DropDownList ID="ddlBIDCategoria" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Laboratorio:</label>
											<asp:DropDownList ID="ddlBIDMarca" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label><b>Filtro[Producto]:</b></label>
											<asp:TextBox ID="txtFiltroProducto" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo Reporte:</label>
											<asp:DropDownList ID="ddlTipoReporte" runat="server">
												<asp:ListItem Value="0">Productos con Stock</asp:ListItem>
												<asp:ListItem Value="1">Todos los Productos</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscarStock" runat="server" Text="Buscar" OnClick="btnBuscarStock_Click"></asp:Button>
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('PRODUCTOS');"><i class='icon-cloud-download'></i>Excel</a>
											<asp:LinkButton ID="lnkImprimirPDF" Visible="false" runat="server" OnClick="lnkImprimirPDF_Click" CssClass="btn btn-danger btn-danger" Width="76%"><i class="icon-printer"></i> PDF</a></asp:LinkButton>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<div class="separador"></div>
								<asp:Panel ID="pnListarGrid" runat="server" Visible="false">
									<div class="row">
										<div class="col-md-12">
											<div class="widget-content widget-content-area" style="background: #f0f0f0">
												<div class="widget-content widget-content-area">
													<div class="row">
														<div class="col-md-12 text-center">
															<div class="form-group">
																<h3><b>Reporte de Productos</b></h3> 
															</div>
														</div>
														<div class="col-md-2"></div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6>Sucursal:<span id="spSucursal" runat="server"></span></h6> 
															</div>
														</div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6>Categoria:<span id="spCategoria" runat="server"></span></h6> 
															</div>
														</div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6>Laboratorio/Marca:<span id="spLaboratorio" runat="server"></span></h6> 
															</div>
														</div>
														<div class="col-md-1"></div>
														<div class="col-md-2"></div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6>Tipo Reporte:<span id="spTipoReporte" runat="server"></span></h6> 
															</div>
														</div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6>Nro.Productos:<span id="spNroProductos" runat="server"></span></h6> 
															</div>
														</div>
														<div class="col-md-3 text-center">
															<div class="form-group">
																<h6><b>Valorizado:S/<span id="spValorizado" runat="server"></span></b></h6> 
															</div>
														</div>
														<div class="col-md-2"></div>
													</div>
													<hr />
													<div class="table-responsive" style="background-color: white">
														<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
															<Columns>
																<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("Sucursal") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("Categoria") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Laboratorio" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("Laboratorio") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																	<ItemTemplate>
																		<%# Eval("CodigoAlterna") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
																	<ItemTemplate>
																		<%# Eval("Nombre") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="UM.Venta" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
																	<ItemTemplate>
																		<%# Eval("UnidadMedidaVenta") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="StockMinimo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
																	<ItemTemplate>
																		<%# Eval("StockMinimo", "{0:N}") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="StockActual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
																	<ItemTemplate>
																		<%# Eval("StockActual", "{0:N}") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="PC.UNI" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
																	<ItemTemplate>
																		<%# Eval("PrecioCostoUnidadConIgv", "{0:N}") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Valorizado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("Valorizado", "{0:N}") %>
																	</ItemTemplate>
																</asp:TemplateField>
															</Columns>
														</asp:GridView>
													</div>
												</div>
											</div>
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
			var pIDCategoria = $("#<%= ddlBIDCategoria.ClientID %>").val();
			var pIDMarca = $("#<%= ddlBIDMarca.ClientID %>").val();
			var pIDFiltroProducto = $("#<%= txtFiltroProducto.ClientID %>").val();
			var pTipoConsulta = $("#<%= ddlTipoReporte.ClientID %>").val();

			$("#ifrmGeneraExcel").attr("src", "");
			$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pIDCategoria=" + pIDCategoria + "&pIDMarca=" + pIDMarca + "&pIDFiltroProducto=" + pIDFiltroProducto +
                "&pTipoReporte=" + pTipoReporte + "&pTipoConsulta=" + pTipoConsulta);
			return false;
		}
	</script>
</asp:Content>
