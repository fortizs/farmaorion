<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Farmacia.Compras.Compras" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtSerieDocumento {
			text-transform: uppercase;
		}

		.filtered-list-search form button {
			border-radius: 50%;
			padding: 7px 7px;
			position: absolute;
			right: 4px;
			top: 4px;
		}

		.alert-icon-left {
			border-left: 64px solid;
		}

		.alert-light-primary {
			color: #1b55e2;
			background-color: #c2d5ff;
			border-color: #1b55e2;
		}

		.alert-icon-left svg:not(.close) {
			color: #FFF;
			width: 4rem;
			left: -4rem;
			text-align: center;
			position: absolute;
			top: 50%;
			margin-top: -10px;
			font-size: 1.25rem;
			font-weight: 400;
			line-height: 1;
			-webkit-font-smoothing: antialiased;
			-moz-osx-font-smoothing: grayscale;
		}

		svg {
			overflow: hidden;
			vertical-align: middle;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Registro de Compras</h3>
		</div>
		<div class="panel-body">
			<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
						<i class="icon-stack3 position-left"></i>
						Consulta</a>
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
											<asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("Moneda") %> <%# Eval("TotalCompra","{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("EstadoNombre") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Estado Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
												<ItemTemplate>
													<%# Eval("EstadoPago") %>
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
															<li style='<%# (Int32.Parse(Eval("IDEstado").ToString()) == 1)  ? "display:none;": "width: 35px;" %>' title="Ver Documento">
																<asp:LinkButton ID="lnkVerMotivo" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="VerMotivo" CommandArgument='<%# Eval("IDCompras").ToString() %>'><i class="icon-file-eye"></i></asp:LinkButton>
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
							<asp:HiddenField ID="hdfIDCompraDocumentoModifica" runat="server" Value="0" />
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
								<div class="col-md-2">
									<div class="form-group">
										<label>Almacen:</label>
										<asp:DropDownList ID="ddlIDAlmacen" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-6">
									<div class="form-group">
										<label>Tipo Comprobante:</label>
										<asp:DropDownList ID="ddlRegIDTipoComprobante" runat="server"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-2">
									<div class="form-group">
										<label>Serie</label>
										<asp:TextBox ID="txtSerieDocumento" runat="server" OnTextChanged="txtSerieDocumento_TextChanged" AutoPostBack="true" MaxLength="4" SkinID="ui-textbox-requerido"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-2">
									<div class="form-group">
										<label>Numero</label>
										<asp:TextBox ID="txtNumeroDocumento" runat="server" OnTextChanged="txtNumeroDocumento_TextChanged" AutoPostBack="true" MaxLength="8" SkinID="ui-textbox-requerido"></asp:TextBox>
									</div>
								</div>
								<%--<div class="col-md-2">
                                            <div class="form-group">
                                                <label>Doc.Referencia:</label>
                                                <div class="input-group">
                                                    <asp:HiddenField ID="hdfTipoDocumentoReferencia" runat="server" Value="0" />
                                                    <asp:TextBox ID="txtDocumentoReferencia" runat="server" Enabled="false" SkinID="ui-textbox-requerido"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:LinkButton ID="lnkBuscarDocumentoReferencia" Enabled="false" runat="server" OnClick="lnkBuscarDocumentoReferencia_Click" SkinID="ui-link-boton-primario"><i class="icon-search4"></i></asp:LinkButton>
                                                    </span>
                                                </div>

                                            </div>
                                        </div>--%>
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
								<div class="col-md-4">
									<div class="form-group">
										<label>Forma de Pago:</label>
										<asp:DropDownList ID="ddlRegFormaPago" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
									</div>
								</div>
								<%--<div class="col-md-2">
                                            <div class="form-group has-feedback">
                                                <label>Fecha Doc.Referencia:</label>
                                                <asp:TextBox ID="txtFechaDocumentoReferencia" Enabled="false" runat="server"></asp:TextBox>
                                                <div class="form-control-feedback form-control-feedback-sm">
                                                    <i class="icon-calendar"></i>
                                                </div>
                                            </div>
                                        </div>--%>
							</div>
							<div class="row">
								<div id="divTipoDoc" runat="server" visible="false">
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo Documento:</label>
											<asp:DropDownList ID="ddlRegIDTipoDocumento" SkinID="ui-dropdownlist-requerido" runat="server" Visible="false"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="col-md-4">
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
								<%-- <div class="col-md-1">
                                            <div class="form-group">
                                                <label>Cuenta:</label>
                                                <asp:TextBox ID="txtCuenta" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label>Cuenta Caja:</label>
                                                <asp:TextBox ID="txtCuentaCaja" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Glosa:</label>
                                                <asp:TextBox ID="txtGlosa" runat="server" MaxLength="200" />
                                            </div>
                                        </div>--%>
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
														<asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("PrecioUnitario" , "{0:N}") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal") %>' />
														<%# Eval("SubTotal" , "{0:N}") %>
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
										<div class="col-md-4">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:LinkButton ID="lnkBuscarProveedor" runat="server" SkinID="ui-link-boton-default" OnClick="lnkBuscarProveedor_Click"><i class="icon-search4"></i> Buscar</asp:LinkButton>
												<asp:LinkButton ID="lnkAgregarProveedor" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkAgregarProveedor_Click"><i class="fa fa-plus"></i> Agregar</asp:LinkButton>
											</div>
										</div>

									</div>
									<div class="table-responsive">
										<asp:GridView ID="gvListadoProveedor" runat="server" DataKeyNames="IDProveedor" OnPageIndexChanging="gvListadoProveedor_PageIndexChanging" AutoGenerateColumns="False" GridLines="None" OnSelectedIndexChanged="gvListadoProveedor_SelectedIndexChanged">
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
																<li style="width: 35px" data-popup="tooltip" title="Seleccionar">
																	<asp:LinkButton ID="lblSeleccionar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select"><span class="icon-multitouch"></span></asp:LinkButton>
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
								<asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12">
										<div class="form-group has-feedback">
											<label>Producto:</label>
											<asp:DropDownList ID="ddlIDReProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDReProducto_SelectedIndexChanged"></asp:DropDownList>
											<%--                                            <asp:TextBox ID="txtRegProducto" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>--%>
											<%-- <div class="form-control-feedback">
                                                <i class="icon-keyboard"></i>
                                            </div>--%>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<%--      <div id="divLote" class="row" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <p style="text-transform: uppercase; font-weight: bold;">Elegir Lote
                                        </p>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <asp:LinkButton ID="lnkNuevoLote" runat="server" CssClass="btn btn-success btn-sm"><span class="glyphicon glyphicon-plus"></span> Nuevo Lote</asp:LinkButton>
                                    </div>
                                    <div class="espacio"></div>
                                    <div class="espacio"></div>
                                    <div class="col-md-12">
                                            <div class="table-responsive" style="margin-bottom: 0;">
                                                <asp:GridView ID="gvLote" runat="server" DataKeyNames="IDLote" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%# Eval("Lote") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Eval("CantidadLote") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fecha Fabricacion" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fecha Vencimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                 </div>--%>
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
											<label>Unidad Medida Compra:</label>
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
											<label>Precio Unitario Compra:</label>
											<asp:TextBox ID="txtRegPrecioCompra" SkinID="ui-textbox-price-requerido" runat="server" MaxLength="11" Text="0.00"></asp:TextBox>
										</div>
									</div>

								</div>

							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarItem" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarItem_Click" />
								<asp:Button ID="btnAgregarItem" runat="server" Text="Agregar" SkinID="ui-boton-success" OnClick="btnAgregarItem_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>
	<%--  <div id="DatosDocumentoReferencia" class="modal fade" tabindex="-1" role="dialog">
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
                                                <asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# Eval("Moneda") %> <%# Eval("TotalCompra") %>
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
    </div>--%>
	<%--   <div id="DatosLote" class="modal fade" role="dialog">
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
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Lote:</label>
                                            <asp:TextBox ID="txtLote" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtLCantidad" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
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
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelarLote" runat="server" Text="Cancelar" SkinID="ui-boton-default" />
                                <asp:Button ID="btnGuardarLote" runat="server" Text="Guardar" SkinID="ui-boton-success" OnClick="btnGuardarLote_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>--%>


	<div id="DatosRegistroProveedor" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Nuevo Proveedor</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDProveedor" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Tipo de Documento:</label>
											<asp:DropDownList ID="ddlIDPTipoDocumento" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Numero de documento:</label>
											<asp:TextBox ID="txtPNroDocumento" MaxLength="11" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Razón Social:</label>
											<asp:TextBox ID="txtPRazonSocial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Nombre Comercial:</label>
											<asp:TextBox ID="txtPNombreComercial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label for="tags">Ubigeo:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfRegIDUbigeo" runat="server" Value="0" />
												<asp:TextBox ID="txtRegUbigeo" Enabled="false" runat="server"></asp:TextBox>
												<span class="input-group-btn">
													<button class="btn btn-primary" type="button" onclick="BuscarUbigeo();"><i class="icon-search4"></i></button>
												</span>
											</div>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Urbanización:</label>
											<asp:TextBox ID="txtPUrbanizacion" runat="server"></asp:TextBox>
										</div>
									</div>

								</div>

								<div class="row">
									<div class="col-md-5">
										<div class="form-group">
											<label>Direccion:</label>
											<asp:TextBox ID="txtPDireccion" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-5">
										<div class="form-group">
											<label>Correo:</label>
											<asp:TextBox ID="txtPCorreo" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtPCelular" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>

							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnCancelar_Click" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>

	<div id="BuscarUbigeo" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Buscar Ubigeo</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upBuscarUbigeo" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<uc1:ccBuscarUbigeo runat="server" ID="ccBuscarUbigeo" />
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
				<div class="modal-footer">
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

		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_hdfRegIDUbigeo', 'cphPrincipal_txtRegUbigeo');
		}

		function funModalAbrirProve() {
			$('#DatosRegistroProveedor').modal('show');
		}

		function funModalCerrarProve() {
			$('#DatosRegistroProveedor').modal('hide');
		}

	</script>
</asp:Content>

