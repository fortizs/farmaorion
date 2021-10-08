<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gastos.aspx.cs" Inherits="Farmacia.Compras.Gastos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtSerieDocumento {
			text-transform: uppercase;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Otras Compras</h2>
					</div>
					<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
						<li class="nav-item">
							<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
								<i class="icon-stack3 position-left"></i>
								Lista</a>
						</li>
						<li class="nav-item">
							<a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
								<i class="icon-pencil7 position-left"></i>
								Registro</a>
						</li>
					</ul>
					<div class="tab-content mb-4" id="border-tabsContent">
						<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
							<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
										<div class="row">
											<div class="col-md-2">
												<div class="form-group">
													<label>Sucursal:</label>
													<asp:DropDownList ID="ddlSucursal" runat="server"></asp:DropDownList>
												</div>
											</div>
											<div class="col-md-5">
												<div class="form-group">
													<label>Filtro:</label>
													<asp:TextBox ID="txtCOMFiltro" runat="server"></asp:TextBox>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group has-feedback">
													<label>Fecha Inicio:</label>
													<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
													<div class="form-control-feedback form-control-feedback-sm">
														<i class="icon-calendar"></i>
													</div>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group has-feedback">
													<label>Fecha Fin:</label>
													<asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
													<div class="form-control-feedback form-control-feedback-sm">
														<i class="icon-calendar"></i>
													</div>
												</div>
											</div>
											<div class="col-md-1">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDGasto" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Número Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("NumeroCompra") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title='<%# Eval("TipoDocumento") %>'><%# Eval("IDTipoComprobanteCS") %></span> 
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("SerieNumero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
														<ItemTemplate>
															<%# Eval("RucProveedor") %> - <%# Eval("Proveedor") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Moneda") %> <%# Eval("TotalCompra") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("EstadoNombre") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField ShowHeader="False" HeaderText="Editar" ItemStyle-Width="5%">
														<ItemTemplate>
															<div style="width: 75px;">
																<ul class="icons-list">
																	<li class="text-primary-600" style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 8 || Int32.Parse(Eval("IDEstado").ToString()) == 9) ? "display:none;": "width: 35px;" %>' title="Editar Documento">
																		<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDGasto").ToString() %>'><span class="fa fa-pencil"></span></asp:LinkButton>
																	</li>
																	<li class="text-success-600" style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 8 || Int32.Parse(Eval("IDEstado").ToString()) == 9) ? "display:none;": "width: 35px;" %>' title="Aprobar Documento">
																		<asp:LinkButton ID="lnkAprobar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Aprobar" CommandArgument='<%# Eval("IDGasto").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas aprobar este documento?\",\"{0}\"); return false;", Eval("NumeroCompra"))%>'><i class="fa fa-check-square-o mr-2"></i></asp:LinkButton>
																	</li>
																</ul>
															</div>
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center" />
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
										<br />
									</asp:Panel>
								</ContentTemplate>
							</asp:UpdatePanel>


						</div>
						<div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
							<asp:UpdatePanel ID="upRegistroCompra" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<asp:HiddenField ID="hdfIDCompraDetalle" runat="server" Value="-1" />
									<asp:HiddenField ID="hdfIDCompra" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />

									<div class="row text-right">
										<div class="col-md-12">
											<span style="color: black; font-size: 18px; font-weight: bold;">Nº de Compra:</span>
											<span style="color: red; font-size: 24px; font-weight: bold;">
												<asp:Label ID="lblNumeroCompra" runat="server"></asp:Label></span>
										</div>
									</div>
									<div class="separador"></div>
									<div class="row">
										<div class="col-md-8">
											<div class="form-group">
												<label>Tipo Documento:</label>
												<asp:DropDownList ID="ddlRegTipoDocumento" runat="server"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-1">
											<div class="form-group">
												<label>Serie</label>
												<asp:TextBox ID="txtSerieDocumento" runat="server" MaxLength="4" SkinID="ui-textbox-requerido" AutoPostBack="true" OnTextChanged="txtSerieDocumento_TextChanged"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-1">
											<div class="form-group">
												<label>Numero</label>
												<asp:TextBox ID="txtNumeroDocumento" runat="server" MaxLength="8" SkinID="ui-textbox-requerido" AutoPostBack="true" OnTextChanged="txtNumeroDocumento_TextChanged"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Emisión:</label>
												<asp:TextBox ID="txtFechaEmision" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Vencimiento:</label>
												<asp:TextBox ID="txtFechaVencimiento" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Registro:</label>
												<asp:TextBox ID="txtFechaRegistro" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Moneda:</label>
												<asp:DropDownList ID="ddlRegMoneda" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Forma de Pago:</label>
												<asp:DropDownList ID="ddlRegFormaPago" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-2">
											<div class="form-group">
												<label>Tipo Documento:</label>
												<asp:DropDownList ID="ddlRegIDTipoDocumento" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Número Documento:</label>
												<div class="input-group">
													<asp:HiddenField ID="hdfRegIDProveedor" runat="server" Value="0" />
													<asp:TextBox ID="txtRegNumeroDocumento" runat="server" MaxLength="11" SkinID="ui-textbox-requerido"></asp:TextBox>
													<span class="input-group-btn">
														<asp:LinkButton ID="lnkBusProveedor" runat="server" OnClick="lnkBusProveedor_Click" SkinID="ui-link-boton-primario"><i class="icon-search4"></i></asp:LinkButton>
													</span>
												</div>
											</div>
										</div>
										<div class="col-md-6">
											<div class="form-group">
												<label>Proveedor / Razon Social:</label>
												<asp:TextBox ID="txtRegProveedor" runat="server" MaxLength="50" Enabled="false" SkinID="ui-textbox-requerido"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Nro. Documento:</label>
												<asp:TextBox ID="txtRegProveedorNumeroDocumento" runat="server" Enabled="false" SkinID="ui-textbox-requerido"></asp:TextBox>
											</div>
										</div>

									</div>
									<div class="row">
										<div class="col-md-1">
											<div class="form-group">
												<label>Cuenta:</label>
												<asp:TextBox ID="txtCuenta" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-1">
											<div class="form-group">
												<label>Cuenta Caja:</label>
												<asp:TextBox ID="txtCuentaCaja" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-8">
											<div class="form-group">
												<label>Glosa:</label>
												<asp:TextBox ID="txtGlosa" runat="server" MaxLength="200" />
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-12 text-right">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:LinkButton ID="lnkNuevoItem" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkNuevoItem_Click"><i class="glyphicon glyphicon-plus-sign"></i> Agregar Producto</asp:LinkButton>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-12">
											<div class="table-responsive" style="margin-bottom: 0;">
												<asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="Item" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvDetalleLista_RowDeleting" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged">
													<Columns>
														<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
															<ItemTemplate>
																<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Descripción" ItemStyle-Width="35%">
															<ItemTemplate>
																<asp:HiddenField ID="hfIDUnidadMedida" runat="server" Value='<%# Eval("IDUnidadMedida") %>' />
																<asp:Label ID="lblProductoDetalle" runat="server" Text='<%# Eval("ProductoDetalle") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="AplicaIgv" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:Label ID="lblAplicaIgv" runat="server" Text='<%# Eval("AplicaIgv") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Precio de Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("PrecioUnitario") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Igv" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:Label ID="lblIgv" runat="server" Text='<%# Eval("Igv") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal") %>' />
																<asp:HiddenField ID="hfTotal" runat="server" Value='<%# Eval("Total") %>' />
																<%# Eval("SubTotal") %>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
															<ItemTemplate>
																<div style="width: 75px;">
																	<ul class="icons-list">
																		<li style="width: 35px;">
																			<asp:LinkButton ID="lnkEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select"><span class="icon-pencil7"></span></asp:LinkButton>
																		</li>
																		<li style="width: 35px;">
																			<asp:LinkButton ID="lnkEliminar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="delete" CommandArgument='<%# Eval("Item") %>'><span class="icon-trash"></span></asp:LinkButton>
																		</li>
																	</ul>
																</div>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" Width="5%" />
														</asp:TemplateField>
													</Columns>
												</asp:GridView>
											</div>
										</div>
									</div>
									<div class="espacio"></div>
									<div class="espacio"></div>
									<div class="row">
										<div class="col-sm-10">
										</div>
										<div class="col-sm-2">
											<div class="content-group no-margin-bottom">
												<div class="table-responsive no-margin-bottom no-border">
													<table class="ui-table">
														<tbody>
															<tr>
																<th style="width: 60%; text-align: right!important; background-color: #fbfbfb;">
																	<div style="min-width: 140px;"><b>SUBTOTAL:</b></div>
																</th>
																<td style="width: 40%; text-align: right!important; font-weight: bold;">
																	<span class="text-bold" style="font-size: 16px; color: black; text-align: right;">
																		<asp:Label ID="lblSubTotal" runat="server" Text="0.00"></asp:Label></span>
																	<asp:HiddenField ID="hdfSubTotal" runat="server" Value="0" />
																</td>
															</tr>
															<tr>
																<th style="width: 60%; text-align: right!important; background-color: #fbfbfb;">
																	<div style="min-width: 140px;"><b>I.G.V.:</b></div>
																</th>
																<td style="width: 40%; text-align: right!important; font-weight: bold;">
																	<span class="text-bold" style="font-size: 16px; color: black; text-align: right;">
																		<asp:Label ID="lblTotalIgv" runat="server" Text="0.00"></asp:Label></span>
																	<asp:HiddenField ID="hdfTotalIGV" runat="server" Value="0" />
																</td>
															</tr>
															<tr>
																<th style="width: 60%; text-align: right!important; background-color: #fbfbfb;">
																	<div style="min-width: 140px;"><b>TOTAL A PAGAR:</b></div>
																</th>
																<td style="width: 40%; text-align: right!important; font-weight: bold;">
																	<span class="text-bold" style="font-size: 16px; color: red; text-align: right;">
																		<asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label></span>
																	<asp:HiddenField ID="hdfTotalCompra" runat="server" Value="0" />
																</td>
															</tr>
														</tbody>
													</table>
												</div>
											</div>
										</div>
									</div>
									<div class="espacio"></div>
									<div class="separador"></div>
									<div class="espacio"></div>
									<div class="row">
										<div class="col-md-12 text-right">
											<div class="form-group">
												<asp:LinkButton ID="lnkNuevaCompra" runat="server" SkinID="ui-link-boton-default" OnClick="lnkNuevaCompra_Click"><i class="fa fa-hand-paper-o"></i> Nueva Compra</asp:LinkButton>
												<asp:LinkButton ID="lnkGuardarCompra" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarCompra_Click"><i class="glyphicon glyphicon-floppy-saved"></i> Grabar Compras</asp:LinkButton>
											</div>
										</div>
									</div>
								</ContentTemplate>
							</asp:UpdatePanel>


						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<div id="DatosProveedor" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Lista de Proveedores</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upProveedor" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="row">
								<div class="col-md-12">
									<div class="row">
										<div class="col-md-8">
											<div class="form-group">
												<label class="control-label col-lg-2">Filtro:</label>
												<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:LinkButton ID="lnkBuscarProveedor" runat="server" SkinID="ui-link-boton-default" OnClick="lnkBuscarProveedor_Click"><i class="icon-search4"></i> Buscar</asp:LinkButton>
											</div>
										</div>
									</div>
									<div class="table-responsive">
										<asp:GridView ID="gvListadoProveedor" runat="server" DataKeyNames="IDProveedor" AutoGenerateColumns="False" Width="99%" GridLines="None" OnSelectedIndexChanged="gvListadoProveedor_SelectedIndexChanged" AllowPaging="False">
											<Columns>
												<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-Width="15%">
													<ItemTemplate>
														<asp:HiddenField ID="hdfIDProveedor" runat="server" Value='<%# Bind("IDProveedor") %>' />
														<asp:HiddenField ID="hdfIDTipoDocumento" runat="server" Value='<%# Bind("IDTipoDocumento") %>' />
														<asp:Label ID="lblTipoDocumento" runat="server" Text='<%# Bind("TipoDocumento") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Numero Documento" ItemStyle-Width="15%">
													<ItemTemplate>
														<asp:Label ID="lblNumeroDocumento" runat="server" Text='<%# Bind("NumeroDocumento") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Proveedor" ItemStyle-Width="65%">
													<ItemTemplate>
														<asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Sel." ShowHeader="False">
													<ItemTemplate>
														<div style="width: 40px;">
															<ul class="icons-list">
																<li style="width: 35px">
																	<asp:LinkButton ID="lblSeleccionar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-multitouch"></span></asp:LinkButton>
																</li>
															</ul>
														</div>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="15%" />
												</asp:TemplateField>
											</Columns>
										</asp:GridView>
									</div>

								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<div id="DatosProducto" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Nuevo Producto</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="pnRegistroProducto" runat="server" DefaultButton="btnAgregarItem">
					<asp:UpdatePanel ID="upRegistroProducto" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<label>Aplica Igv:</label>
											<asp:CheckBox ID="chkAplicaIgv" runat="server" Checked="true"></asp:CheckBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Detalle:</label>
											<asp:TextBox ID="txtRegProductoDetalle" runat="server" TextMode="MultiLine" Rows="3" SkinID="ui-textbox-requerido"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Cantidad:</label>
											<asp:TextBox ID="txtRegCantidad" SkinID="ui-textbox-number-requerido" runat="server" MaxLength="3" Text="1"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Precio Compra:</label>
											<asp:TextBox ID="txtRegPrecioCompra" SkinID="ui-textbox-price-requerido" runat="server" MaxLength="11" Text="0.00"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarItem" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarItem_Click" />
								<asp:Button ID="btnAgregarItem" runat="server" Text="Agregar" OnClick="btnAgregarItem_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>

	<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />

	<script type="text/javascript">

		function ConfigJS() {
			$("#<%= ddlRegTipoDocumento.ClientID %>").select2({
				width: "100%"
			});
		}

		function ActivarTabxId(id) {
			var x = $("#TabActivo").val(id);
			ActivarTabxBoton();
		}

		function ActivarTabxBoton() {
			$('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
		}

		function funModalProveedorAbrir() {
			$('#DatosProveedor').modal('show');
		}

		function funModalProveedorCerrar() {
			$('#DatosProveedor').modal('hide');
		}

		function funModalProductoAbrir() {
			$('#DatosProducto').modal('show');
		}

		function funModalProductoCerrar() {
			$('#DatosProducto').modal('hide');
		}

	</script>
</asp:Content>



