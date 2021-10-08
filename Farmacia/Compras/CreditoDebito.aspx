<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreditoDebito.aspx.cs" Inherits="Farmacia.Compras.CreditoDebito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Registro de Crédito y Débito</h3>
		</div>
		<div class="panel-body">
			<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
				<li class="nav-item">
					<a class="nav-link" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
						<i class="icon-stack3 position-left"></i>
						Lista</a>
				</li>
				<li class="nav-item">
					<a class="nav-link active" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
						<i class="icon-pencil7 position-left"></i>
						Registro</a>
				</li>
			</ul>
			<div class="tab-content mb-4" id="border-tabsContent">
				<div class="tab-pane fade show" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
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
									<div class="col-md-3">
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
									<div class="col-md-2">
										<div class="form-group">
											<label>Estado Compra:</label>
											<asp:DropDownList ID="ddlBIDEstadoCompra" runat="server"></asp:DropDownList>
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
									<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDCompras" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand">
										<Columns>
											<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<ItemTemplate>
													<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Número Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("NumeroCompra") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title='<%# Eval("TipoDocumento") %>'><%# Eval("Sigla") %></span>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<span class="label bg-vigencia" style="font-size: 11px; display: block;">
														<span style="display: block; padding-bottom: 2px;"><%# Eval("SerieNumero")%></span>

													</span>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="FormaPago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FormaPago") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaCompra", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="24%">
												<ItemTemplate>
													<%# Eval("RucProveedor") %> - <%# Eval("Proveedor") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("Moneda") %> <%# Eval("TotalCompra") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("EstadoNombre") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
												<ItemTemplate>
													<div style="min-width: 75px;">
														<ul class="icons-list">
															<li style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 3 || Int32.Parse(Eval("IDEstado").ToString()) == 2 )  ? "display:none;": "width: 35px;" %>' title="Editar Documento">
																<asp:LinkButton ID="lbEditar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDCompras").ToString() %>'><i class="icon-pencil7"></i></asp:LinkButton>
															</li>
															<li style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 3 || Int32.Parse(Eval("IDEstado").ToString()) == 2) ? "display:none;": "width: 35px;" %>' title="Aprobar Documento">
																<asp:LinkButton ID="lnkAprobar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Aprobar" CommandArgument='<%# Eval("IDCompras").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas aprobar este documento?\",\"{0}\"); return false;", Eval("NumeroCompra"))%>'><i class="icon-shield-check"></i></asp:LinkButton>
															</li>
															<li class="text-primary" style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 3 || Int32.Parse(Eval("IDEstado").ToString()) == 2) ? "display:none;": "width: 35px;" %>' title="Anular Documento">
																<asp:LinkButton ID="lnkAnular" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Anular" CommandArgument='<%# Eval("IDCompras").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas anular este documento?\",\"{0}\"); return false;", "123")%>'><i class="icon-bin"></i></asp:LinkButton>
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
				<div class="tab-pane fade show active" id="tab2" role="tabpanel" aria-labelledby="border-profile-tab">
					<asp:UpdatePanel ID="upRegistroCompra" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnRegistroCompra" runat="server">
								<asp:HiddenField ID="hdfIDCompraDetalle" runat="server" Value="-1" />
								<asp:HiddenField ID="hdfIDCompra" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDCompraDocumentoModifica" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDVentaDetalleTemp" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDVentaAfectado" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDMoneda" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDAlmacen" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDFormaPago" runat="server" Value="0" />

								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Serie-Número Afectado:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfTipoDocumentoReferencia" runat="server" Value="0" />
												<asp:TextBox ID="txtSerieNumeroAfectado" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
												<span class="input-group-btn">
													<asp:LinkButton ID="lnkBuscarComprobante" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkBuscarComprobante_Click"><i class="icon-search4"></i></asp:LinkButton>
												</span>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Comprobante Afectado:</label>
											<asp:HiddenField ID="hdfRegIDComprobanteAfectado" runat="server" Value="0" />
											<asp:DropDownList ID="ddlIDTipoComprobanteAfectado" runat="server" Enabled="false"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Nro.Documento:</label>

											<asp:TextBox ID="txtRegNumeroDocumentoProveedorAfectado" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Proveedor:</label>
											<asp:HiddenField ID="hdfIDProveedor" runat="server" Value="0" />
											<asp:TextBox ID="txtRegProveedorAfectado" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Total Compra:</label>
											<asp:TextBox ID="txtTotalCompraAfectado" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Tipo Comprobante:</label>
											<asp:DropDownList ID="ddlIDTipoComprobante" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Serie:</label>
											<asp:TextBox ID="txtSerieNumero" runat="server" OnTextChanged="txtSerieNumero_TextChanged" AutoPostBack="true" SkinID="ui-textbox-requerido" Enabled="true"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Número:</label>
											<asp:TextBox ID="txtNumeroDocumento" runat="server" OnTextChanged="txtNumeroDocumento_TextChanged" AutoPostBack="true" SkinID="ui-textbox-requerido" Enabled="true"></asp:TextBox>
										</div>
									</div>
									<%--<div class="col-md-4">
												<div class="form-group">
													<label>Tipo Motivo:</label>
													<asp:DropDownList ID="ddlTipoMotivo" runat="server"></asp:DropDownList>
												</div>
											</div>--%>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Emisión:</label>
											<asp:TextBox ID="txtFechaEmision" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Motivo:</label>
											<asp:TextBox ID="txtMotivo" runat="server" SkinID="ui-textbox-requerido" TextMode="MultiLine" Rows="3"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-12">
										<div class="table-responsive" style="margin-bottom: 0;">
											<asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvDetalleLista_RowDeleting" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged">
												<Columns>
													<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
														<ItemTemplate>
															<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
														<ItemTemplate>
															<asp:HiddenField ID="hfIDUnidadMedida" runat="server" Value='<%# Eval("IDUnidadMedida") %>' />
															<%# Eval("CodigoProducto") %> - <%# Eval("Producto") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Precio de Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
														<ItemTemplate>
															<asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("PrecioUnitario") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
														<ItemTemplate>
															<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal") %>' />
															<%# Eval("SubTotal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<div style="width: 75px;">
																<ul class="icons-list">
																	<li style="width: 35px;">
																		<asp:LinkButton ID="lnkEditar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Select"><span class="icon-pencil7"></span></asp:LinkButton>
																	</li>
																	<li style="width: 35px;">
																		<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="delete" CommandArgument='<%# Eval("IDProducto") %>'><span class="icon-trash"></span></asp:LinkButton>
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
									<div class="col-sm-8">
									</div>
									<div class="col-sm-4">
										<div class="content-group no-margin-bottom">
											<div class="table-responsive no-margin-bottom no-border">
												<table class="ui-table" style="float: right;">
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
							</asp:Panel>
							<div class="espacio"></div>
							<div class="separador"></div>
							<div class="espacio"></div>
							<div class="row text-right">
								<div class="col-md-12">
									<div class="form-group">
										<asp:LinkButton ID="lnkNuevaNotaCredito" runat="server" CssClass="btn btn-default" OnClick="lnkNuevaNotaCredito_Click"><i class="icon-file-empty"></i> Nueva Compra</asp:LinkButton>
										<asp:LinkButton ID="lnkGuardarNota" runat="server" CssClass="btn btn-primary" Visible="true" OnClick="lnkGuardarNota_Click"><i class="icon-cash3"></i> Guardar Compra</asp:LinkButton>
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
								<asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12">
										<div class="form-group has-feedback">
											<label>Producto:</label>
											<asp:DropDownList ID="ddlIDReProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDReProducto_SelectedIndexChanged"></asp:DropDownList>

										</div>
									</div>
								</div>
								<div class="espacio"></div>

								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Stock:</label>
											<asp:TextBox ID="txtRegStock" SkinID="ui-textbox-requerido" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Unidad Medida:</label>
											<asp:TextBox ID="txtRegUnidMedida" SkinID="ui-textbox-requerido" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Cantidad:</label>
											<asp:TextBox ID="txtRegCantidad" SkinID="ui-textbox-number-requerido" runat="server" Text="1"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Precio Unitario:</label>
											<asp:TextBox ID="txtRegPrecioCompra" SkinID="ui-textbox-price-requerido" runat="server" MaxLength="11" Text="0.00"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarItem" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarItem_Click" />
								<asp:Button ID="btnAgregarItem" runat="server" Text="Agregar" SkinID="ui-boton-success" Visible="true" OnClick="btnAgregarItem_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>
	<div id="DatosDocumentoReferencia" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Lista de Documentos </h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upDocumentoElectronicoListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="row">
								<div class="col-md-12">
									<div class="row">
										<div class="col-md-5">
											<div class="form-group">
												<label>Filtro:</label>
												<asp:TextBox ID="txtBFiltro" runat="server"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Inicio:</label>
												<asp:TextBox ID="txtBFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Fin:</label>
												<asp:TextBox ID="txtBFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:Button ID="btnBuscarDocumento" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscarDocumento_Click" />
											</div>
										</div>
									</div>
									<div class="table-responsive">
										<asp:GridView ID="gvDocumentoElectronicoListar" runat="server" DataKeyNames="IDCompras" AutoGenerateColumns="False" Width="99%" GridLines="None" OnPageIndexChanging="gvDocumentoElectronicoListar_PageIndexChanging" OnSelectedIndexChanged="gvDocumentoElectronicoListar_SelectedIndexChanged" AllowPaging="true">
											<Columns>
												<asp:TemplateField HeaderText="Item" ItemStyle-Width="3%">
													<ItemTemplate>
														<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Número Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<%# Eval("NumeroCompra") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:Label ID="lblIDTipoComprobanteCS" runat="server" Text='<%# Bind("IDTipoComprobanteCS") %>'></asp:Label>
														<asp:HiddenField ID="hdfIDComprobante" runat="server" Value='<%# Bind("IDTipoComprobanteCS") %>' />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblSerieNumero" runat="server" Text='<%# Bind("SerieNumero") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
													<ItemTemplate>
														<asp:Label ID="lblFechaCompra" runat="server" Text='<%# Bind("FechaCompra", "{0:dd/MM/yyyy}") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
													<ItemTemplate>
														<asp:HiddenField ID="hdfIDProveedor" runat="server" Value='<%# Bind("IDProveedor") %>' />
														<asp:Label ID="lblRucProveedor" runat="server" Text='<%# Bind("RucProveedor") %>'></asp:Label>
														-
                                                        <asp:Label ID="lblProveedor" runat="server" Text='<%# Bind("Proveedor") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Total Compras" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
													<ItemTemplate>
														<%# Eval("Moneda") %> <%# Eval("TotalCompra") %>
														<asp:Label ID="lblCompra" runat="server" Visible="false" Text='<%# Bind("TotalCompra") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Acción" ItemStyle-Width="5%">
													<ItemTemplate>
														<div style="width: 40px;">
															<ul class="icons-list">
																<li style="width: 35px">
																	<asp:LinkButton ID="LinkButton1" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Seleccionar"><span class="icon-multitouch"></span></asp:LinkButton>
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
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnBuscarDocumento" EventName="Click" />
							<asp:AsyncPostBackTrigger ControlID="gvDocumentoElectronicoListar" EventName="PageIndexChanging" />
							<asp:AsyncPostBackTrigger ControlID="gvDocumentoElectronicoListar" EventName="SelectedIndexChanged" />
						</Triggers>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />

	<script type="text/javascript">

		function ConfigJS() {
            <%-- $("#<%= ddlRegIDTipoComprobante.ClientID %>").select2({
                width: "100%"
            });
            BuscarProducto();--%>
			$("#<%= ddlIDReProducto.ClientID %>").select2({
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

       <%-- function BuscarProducto() {
            try {
                $("#<%= txtRegProducto.ClientID %>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "<%= ResolveClientUrl("~/Controles/WebService/WSConsultas.asmx/ProductoSucursalListar") %>",
                            dataType: "json",
                            data: "{'pIDSucursal':" + $("#<%= hdfIDSucursal.ClientID %>").val() + " , 'pBuscar':'" + request.term + "'}",
                            success: function (data) {
                                $("#<%= hdfIDProducto.ClientID %>").val("");
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item.Nombre,
                                        _Stock: item.StockActual,
                                        _IDProducto: item.IDProducto,
                                        _Nombre: item.Nombre,
                                        _IDUnidadMedida: item.IDUnidadMedida,
                                        _UnidadMedida: item.UnidadMedida,
                                        _IDTipoImpuesto: item.IDTipoImpuesto


                                    }
                                }))
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event, ui) {
                        $("#<%= hdfIDProducto.ClientID %>").val(ui.item._IDProducto);
                        $("#<%= txtRegProducto.ClientID %>").val(ui.item._Nombre);
                        $("#<%= hdfIDUnidadMedida.ClientID %>").val(ui.item._IDUnidadMedida);
                        $("#<%= txtRegStock.ClientID %>").val(ui.item._Stock);
                        console.log("IDProducto = " + ui.item._IDProducto);
                        console.log("hdfIDUnidadMedida = " + ui.item._IDUnidadMedida);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log("Error en WebService: " + textStatus);
                    }
                });
            }
            catch (err) {
                console.log("Error en WebService: " + err.toString());
            }
        }--%>

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

		function funModalDocumentoReferenciaAbrir() {
			$('#DatosDocumentoReferencia').modal('show');
		}

		function funModalDocumentoReferenciaCerrar() {
			$('#DatosDocumentoReferencia').modal('hide');
		}


		function funModalLoteAbrir() {
			$('#DatosLote').modal('show');
		}

		function funModalLoteCerrar() {
			$('#DatosLote').modal('hide');
		}

	</script>

</asp:Content>
