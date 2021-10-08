<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotaSalida.aspx.cs" Inherits="Farmacia.Almacen.NotaSalida" %>

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
						<h2 class="panel-title">Notas de Salida</h2>
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
													<asp:DropDownList ID="ddlBIDSucursal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBIDSucursal_SelectedIndexChanged"></asp:DropDownList>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group">
													<label>Almacen:</label>
													<asp:DropDownList ID="ddlBIDAlmacen" runat="server"></asp:DropDownList>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group has-feedback">
													<label>Fecha Emisión Inicio:</label>
													<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
													<div class="form-control-feedback form-control-feedback-sm">
														<i class="icon-calendar"></i>
													</div>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group has-feedback">
													<label>Fecha Emisión Fin:</label>
													<asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
													<div class="form-control-feedback form-control-feedback-sm">
														<i class="icon-calendar"></i>
													</div>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDMovimiento" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand">
												<Columns>
													<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("Codigo") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
													<asp:BoundField HeaderText="Almacen" DataField="Almacen.Nombre" ReadOnly="true" />
													<asp:BoundField HeaderText="Transacción" DataField="Transaccion.Nombre" ReadOnly="true" />

													<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
														<ItemTemplate>
															<div style="min-width: 75px;">
																<ul class="icons-list">
																	<li style="width: 35px" title="Editar">
																		<asp:LinkButton ID="lbEditar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDMovimiento").ToString() %>'><i class="icon-pencil7"></i></asp:LinkButton>
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
							<asp:UpdatePanel ID="upRegistroAjuste" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<asp:HiddenField ID="hdfIDMovimientoDetalle" runat="server" Value="-1" />
									<asp:HiddenField ID="hdfIDMovimiento" runat="server" Value="0" />
									<asp:HiddenField ID="hdfToken" runat="server" Value="" />
									<asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />

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
											<div class="form-group">
												<label>Sucursal:</label>
												<asp:DropDownList ID="ddlIDSucursal" runat="server" SkinID="ui-dropdownlist-requerido" AutoPostBack="true" OnSelectedIndexChanged="ddlIDSucursal_SelectedIndexChanged"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Almacen:</label>
												<asp:DropDownList ID="ddlIDAlmacen" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-5">
											<div class="form-group">
												<label>Tipo Transacción:</label>
												<asp:DropDownList ID="ddlIDTransaccion" runat="server"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2" style="display: none">
											<div class="form-group">
												<label>Moneda:</label>
												<asp:DropDownList ID="ddlRegMoneda" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-12">
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
												<asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvDetalleLista_RowDeleting" OnPageIndexChanging="gvDetalleLista_PageIndexChanging" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged">
													<Columns>
														<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
															<ItemTemplate>
																<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
															<ItemTemplate>
																<asp:HiddenField ID="hfIDUnidadMedida" runat="server" Value='<%# Eval("IDUnidadMedida") %>' />
																<%# Eval("ProductoCodigo") %> - <%# Eval("NombreProducto") %>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
															<ItemTemplate>
																<asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
																<asp:Label ID="lblPrecioUnitario" Visible="false" runat="server" Text='<%# Eval("PrecioUnitario") %>'></asp:Label>
																<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal") %>' />
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
															<ItemTemplate>
																<div style="width: 75px;">
																	<ul class="icons-list" runat="server" id="ulOpciones" visible="true">
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
									<div class="row" style="display: none">
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
												<asp:LinkButton ID="lnkNuevoAjuste" runat="server" SkinID="ui-link-boton-default" OnClick="lnkNuevoAjuste_Click"><i class="fa fa-hand-paper-o"></i> Nueva Nota</asp:LinkButton>
												<asp:LinkButton ID="lnkGuardarAjuste" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarAjuste_Click"><i class="glyphicon glyphicon-floppy-saved"></i> Guardar Nota Salida</asp:LinkButton>
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
								<asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12">
										<div class="form-group has-feedback">
											<label>Producto:</label>
											<asp:DropDownList ID="ddlIDProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDProducto_SelectedIndexChanged"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Unidad Medida:</label>
											<asp:TextBox ID="txtRegUnidadMedida" SkinID="ui-textbox-requerido" runat="server" Text="" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Stock:</label>
											<asp:TextBox ID="txtRegStock" SkinID="ui-textbox-requerido" runat="server" Text="0" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Precio Unitario:</label>
											<asp:TextBox ID="txtRegPrecioUnitario" SkinID="ui-textbox-price-requerido" runat="server" MaxLength="11" Text="0.00"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3" runat="server" id="DivCantidad">
										<div class="form-group">
											<label>Cantidad:</label>
											<asp:TextBox ID="txtRegCantidad" SkinID="ui-textbox-number-requerido" runat="server" Text="1"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3" runat="server" id="DivCantidadLote" visible="false">
										<div class="form-group">
											<label>Cantidad:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfCantidadLote" runat="server" Value="0" />
												<asp:TextBox ID="txtRegCantidadLote" runat="server" SkinID="ui-textbox-number-requerido" Text="0" Enabled="false"></asp:TextBox>
												<span class="input-group-btn">
													<asp:LinkButton ID="lnkAbrirLote" runat="server" OnClick="lnkAbrirLote_Click" SkinID="ui-link-boton-primario"><i class="fa fa-cube"></i></asp:LinkButton>
												</span>
											</div>
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

	<div id="DatosAgregarLoteProducto" class="modal fade" role="dialog">
		<div class="modal-dialog modal-md">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Lotes</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="Panel1" runat="server" DefaultButton="lnkNuevoLote">
					<asp:UpdatePanel ID="upLoteListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdIDProductoLote" runat="server" Value="0" />
								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-6">
									</div>
									<div class="col-md-6 text-right">
										<asp:LinkButton ID="lnkNuevoLote" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkNuevoLote_Click"><span class="glyphicon glyphicon-plus"></span> Nuevo Lote</asp:LinkButton>
									</div>
									<div class="espacio"></div>
									<div class="espacio"></div>
									<div class="col-md-12" style="overflow-y: scroll; height: 300px">
										<div class="table-responsive" style="margin-bottom: 0; font-size: 10px;">
											<asp:GridView ID="gvLoteProducto" runat="server" DataKeyNames="IDLote,Lote" AllowPaging="True" Width="100%" AllowSorting="True" OnRowCommand="gvLoteProducto_RowCommand" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("Lote") %>
															<asp:Label ID="lblNombreLote" runat="server" Text=' <%# Eval("Lote") %>' Visible="false" />
															<asp:Label ID="lblIDLote" runat="server" Text=' <%# Eval("IDLote") %>' Visible="false" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("Stock") %>
															<asp:Label ID="lblStockLote" runat="server" Text='<%# Eval("Stock") %>' Visible="false" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<asp:TextBox ID="txtCantidadLote" runat="server" Text='<%# Eval("CantidadLote") %>' SkinID="ui-textbox-number-requerido" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fab-Venc" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %> - <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<div style="width: 90px;">
																<ul class="icons-list">
																	<li style="width: 35px;">
																		<asp:LinkButton ID="LinkButton1" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDLote") %>' ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
																	</li>
																	<li style="width: 35px;">
																		<asp:LinkButton ID="LinkButton2" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDLote") %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este Lote?\",\"{0}\"); return false;", "")%>' ToolTip="Eliminar"><span class="icon-trash"></span></asp:LinkButton>
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
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
										</div>
									</div>
									<div class="col-md-4 text-right">
										<div class="espacio"></div>
										<asp:LinkButton ID="lnkAplicarLote" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkAplicarLote_Click"><i class="fa fa-check-square"></i> Aplicar</asp:LinkButton>
									</div>
								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>
			</div>
		</div>
	</div>

	<div id="DatosLote" class="modal fade" role="dialog">
		<div class="modal-dialog modal-sm">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Nuevo Lote</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="PnLote" runat="server" DefaultButton="btnGuardarLote">
					<asp:UpdatePanel ID="UpRegistroLote" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:HiddenField ID="hdfIDLote" runat="server" Value="0" />
							<div class="modal-body">
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Nombre Lote:</label>
											<asp:TextBox ID="txtLote" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha Fabricación:</label>
											<asp:TextBox ID="txtLFechaFabricacion" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha Vencimiento:</label>
											<asp:TextBox ID="txtLFechaVencimiento" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<%-- <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Stock:</label>
                                            <asp:TextBox ID="txtLStock" Enabled="false" SkinID="ui-textbox-requerido" runat="server" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtLCantidad" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarLote" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarLote_Click" />
								<asp:Button ID="btnGuardarLote" runat="server" Text="Guardar" SkinID="ui-boton-success" OnClick="btnGuardarLote_Click" />
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

			$("#<%= ddlIDProducto.ClientID %>").select2({
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


		function funModalListaLoteAbrir() {
			$('#DatosAgregarLoteProducto').modal('show');
		}

		function funModalListaLoteCerrar() {
			$('#DatosAgregarLoteProducto').modal('hide');
		}

		function funModalProductoAbrir() {
			$('#DatosProducto').modal('show');
		}

		function funModalProductoCerrar() {
			$('#DatosProducto').modal('hide');
		}

		function funModalLoteAbrir() {
			$('#DatosLote').modal('show');
		}

		function funModalLoteCerrar() {
			$('#DatosLote').modal('hide');
		}



	</script>
</asp:Content>

