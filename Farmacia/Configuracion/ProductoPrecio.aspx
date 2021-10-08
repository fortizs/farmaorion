<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProductoPrecio.aspx.cs" Inherits="Farmacia.Configuracion.ProductoPrecio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		fieldset {
			padding: 10px 15px 15px 15px;
			border-radius: 5px;
			border: 1px solid #ddd;
		}

		fieldset {
			margin: 0;
			min-width: 0;
		}

			fieldset:first-child legend:first-child {
				padding-top: 0;
			}

		legend {
			padding: 10px;
			border-color: #eee;
		}

		legend {
			font-size: 12px;
			padding-top: 10px;
			padding-bottom: 10px;
			text-transform: uppercase;
		}

		legend {
			font-weight: bold;
			font-size: 14px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Precios</h3>
		</div>
		<div class="panel-body">
			<ul class="nav nav-tabs mt-3" role="tablist">
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
			<div class="tab-content mb-4">
				<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="row">
								<div class="col-md-6">
									<div class="form-group">
										<label class="etiqueta">Filtro: <b>[Código | Nombre]</b></label>
										<asp:TextBox ID="txtBuscar" runat="server" MaxLength="50"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label class="etiqueta"></label>
										<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
										<asp:Button ID="btnExtraerCatalogo" runat="server" SkinID="ui-boton-default" OnClick="btnExtraerCatalogo_Click" Text="Extraer de Catálogo" />
									</div>
								</div>
							</div>
							<div class="table-responsive">
								<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" DataKeyNames="IDProducto" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("CodigoBarra") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
											<ItemTemplate>
												<%# Eval("Categoria") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
											<ItemTemplate>
												<%# Eval("Nombre") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Localización" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
											<ItemTemplate>
												<%# Eval("Localizacion") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="U.M.Venta" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("UnidadMedidaVenta") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("Stock", "{0:N}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("PrecioVenta", "{0:N}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:CheckBoxField DataField="VentaConReceta" HeaderText="R" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
											<HeaderStyle HorizontalAlign="Center" />
											<ItemStyle HorizontalAlign="Center" Width="3%" />
										</asp:CheckBoxField>
										<asp:CheckBoxField DataField="ControlaLote" HeaderText="L" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
											<HeaderStyle HorizontalAlign="Center" />
											<ItemStyle HorizontalAlign="Center" Width="3%" />
										</asp:CheckBoxField>
										<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="15%">
											<ItemTemplate>
												<div style="min-width: 90px;">
													<ul class="icons-list">
														<li style="width: 35px" title="Editar">
															<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select"><span class="icon-pencil7"></span></asp:LinkButton>
														</li>
														<li style="width: 35px" title="Eliminar">
															<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDProducto").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este producto?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
														</li>
														<li style="width: 35px" title="Ver Precios">
															<asp:LinkButton ID="lnkVerPrecios" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="VerPrecio" CommandArgument='<%# Eval("IDProducto").ToString() %>'><span class="icon-coins"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" />
										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</div>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="gvLista" EventName="PageIndexChanging" />
						</Triggers>
					</asp:UpdatePanel>
				</div>
				<div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
					<div class="modal-body">
						<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Código Barra:</label>
											<asp:TextBox ID="txtCodigoBarra" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Código Alterna:</label>
											<asp:TextBox ID="txtCodigoAlterna" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Controla Stock:</label>
											<asp:DropDownList ID="ddlControlaStock" SkinID="ui-dropdownlist-requerido" runat="server">
												<asp:ListItem Value="SK">Controla Stock</asp:ListItem>
												<asp:ListItem Value="SE">Servicio</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-8">
										<div class="form-group">
											<label>Nombre Comercial:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Categoria:</label>
											<asp:DropDownList ID="ddlIDCategoria" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Laboratorio/Marca:</label>
											<asp:DropDownList ID="ddlIDMarca" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Unidad Compra:</label>
											<asp:DropDownList ID="ddlIDUnidadMedidaCompra" SkinID="ui-dropdownlist-requerido" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDUnidadMedidaCompra_SelectedIndexChanged"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Unidad Medida Venta:</label>
											<asp:DropDownList ID="ddlIDUnidadMedidaVenta" SkinID="ui-dropdownlist-requerido" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDUnidadMedidaVenta_SelectedIndexChanged"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-1">
										<div class="form-group">
											<label>Factor:</label>
											<asp:TextBox ID="txtFactor" SkinID="ui-textbox-number-requerido" Text="1" runat="server" AutoPostBack="true" OnTextChanged="txtFactor_TextChanged"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Stock Mínimo:</label>
											<asp:TextBox ID="txtStockMinimo" SkinID="ui-textbox-number-requerido" Text="0" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Stock:</label>
											<asp:TextBox ID="txtStock" SkinID="ui-textbox-number-requerido" runat="server" Text="0" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Localización:</label>
											<asp:TextBox ID="txtLocalizacion" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<fieldset>
												<legend>Precio de Compra</legend>
												<div class="row">
													<div class="col-md-3">
														<div class="form-group">
															<label>Prec.Compra Total:</label>
															<asp:TextBox ID="txtPrecioCosto" SkinID="ui-textbox-price-requerido" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtPrecioCosto_TextChanged"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-3">
														<div class="form-group">
															<label>Prec.Compra Total S/IGV:</label>
															<asp:TextBox ID="txtPrecioCostoTotalSinIgv" SkinID="ui-textbox-price4-requerido" Text="0.00" runat="server" Enabled="false"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-3">
														<div class="form-group">
															<label>Prec.Compra Unit.S/IGV:</label>
															<asp:TextBox ID="txtPrecioCostoUnidadSinIgv" SkinID="ui-textbox-price4-requerido" Text="0.00" runat="server" Enabled="false"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-3">
														<div class="form-group">
															<label>Pre.Compra Unit.C/IGV:</label>
															<asp:TextBox ID="txtPrecioCostoUnidadConIgv" SkinID="ui-textbox-price4-requerido" Text="0.00" runat="server" Enabled="false"></asp:TextBox>
														</div>
													</div>
												</div>
											</fieldset>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<fieldset>
												<legend>Precio de Venta</legend>
												<div class="row">
													<div class="col-md-4">
														<div class="form-group">
															<label>Márgenes de Utilidad<b> (%)</b>:</label>
															<asp:TextBox ID="txtMargenUtilidad" Enabled="false" SkinID="ui-textbox-price-requerido" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtMargenUtilidad_TextChanged"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-4">
														<div class="form-group">
															<label>Precio Venta Neto:</label>
															<asp:TextBox ID="txtPrecioVenta" SkinID="ui-textbox-price-requerido" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtPrecioVenta_TextChanged"></asp:TextBox>
														</div>
													</div>
													<div class="col-md-4">
														<div class="form-group">
															<label>Mayoreo (Unidades):</label>
															<asp:TextBox ID="txtMayoreoUnidad" SkinID="ui-textbox-number-requerido" runat="server" Enabled="false" Text="0"></asp:TextBox>
														</div>
													</div>
												</div>
											</fieldset>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnNuevo" runat="server" Text="Nuevo" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnNuevo_Click" />
											<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
										</div>
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>

						<asp:UpdatePanel ID="upProductoDetalle" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Panel ID="pnProductoDetalle" runat="server" Visible="false">
									<div class="separador"></div>
									<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
										<li class="nav-item">
											<a class="nav-link active" data-toggle="tab" href="#tab22" role="tab" aria-controls="border-profile" aria-selected="true">
												<i class="icon-newspaper position-left"></i>
												Adicional</a>
										</li>
										<li class="nav-item">
											<a class="nav-link" data-toggle="tab" href="#tab33" role="tab" aria-controls="border-profile" aria-selected="false">
												<i class="icon-coin-dollar position-left"></i>
												Precios Proveedor</a>
										</li>
										<li class="nav-item">
											<a class="nav-link" data-toggle="tab" href="#tab44" role="tab" aria-controls="border-profile" aria-selected="false">
												<i class="icon-flip-vertical4 position-left"></i>
												Compatibles</a>
										</li>
									</ul>
									<div class="tab-content mb-4" id="border-tabsContent">
										<div class="tab-pane fade show active" id="tab22" role="tabpanel" aria-labelledby="border-home-tab">
											<asp:UpdatePanel ID="upProdutoAdicional" runat="server" UpdateMode="Conditional">
												<ContentTemplate>
													<fieldset>
														<legend>Datos Adicionales</legend>
														<div class="row">
															<div class="col-md-6">
																<div class="form-group">
																	<label>Caracteristicas:</label>
																	<asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
																</div>
															</div>
															<div class="col-md-6">
																<div class="form-group">
																	<label>Principio Activo:</label>
																	<asp:TextBox ID="txtPrincipioActivo" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
																</div>
															</div>
														</div>
														<div class="row">
															<div class="col-md-12">
																<div class="form-group">
																	<label class="etiqueta"></label>
																	<asp:CheckBox ID="chkControlaLote" runat="server" Text="Lote" />
																	(Indica si manejará un control de lotes y caducidades para el producto)
																</div>
															</div>
															<div class="col-md-12">
																<div class="form-group">
																	<label class="etiqueta"></label>
																	<asp:CheckBox ID="chkVentaConReceta" runat="server" Text="Receta" />
																	(Indica si el producto se vende solo con receta médica)
																</div>
															</div>
														</div>
													</fieldset>
													<fieldset>
														<legend>Sunat</legend>
														<div class="row">
															<div class="col-md-6">
																<div class="form-group">
																	<label>Tipo de Impuesto:</label>
																	<asp:DropDownList ID="ddlIDTipoImpuesto" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
																</div>
															</div>
															<div class="col-md-4">
																<div class="form-group">
																	<label>Tipo Precio:</label>
																	<asp:DropDownList ID="ddlIDTipoPrecio" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
																</div>
															</div>
														</div>
													</fieldset>
													<div class="row">
														<div class="col-md-12 text-right">
															<div class="form-group">
																<label class="etiqueta"></label>
																<asp:Button ID="btnGuardarDatosAdicional" runat="server" Text="Guardar" OnClick="btnGuardarDatosAdicional_Click" />
															</div>
														</div>
													</div>
												</ContentTemplate>
											</asp:UpdatePanel>
										</div>
										<div class="tab-pane fade" id="tab33" role="tabpanel" aria-labelledby="border-home-tab">

											<asp:UpdatePanel ID="upPrecioProveedorListar" runat="server" UpdateMode="Conditional">
												<ContentTemplate>
													<asp:HiddenField ID="hdfIDPrecioProveedor" runat="server" Value="0" />
													<div class="row">
														<div class="col-md-4">
															<div class="form-group">
																<label class="etiqueta">Proveedor</label>
																<asp:DropDownList ID="ddlPPIDProveedor" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
															</div>
														</div>
														<div class="col-md-4">
															<div class="form-group">
																<label class="etiqueta">Último Pre.Compra</label>
																<asp:TextBox ID="txtPPUltimoPrecioCompra" runat="server" SkinID="ui-textbox-price-requerido" Text="0.00"></asp:TextBox>
															</div>
														</div>
														<div class="col-md-2">
															<div class="form-group">
																<label class="etiqueta"></label>
																<asp:Button ID="btnGuardarPrecioProveedor" runat="server" SkinID="ui-boton-default" OnClick="btnGuardarPrecioProveedor_Click" Text="Agregar" />
															</div>
														</div>
													</div>
													<div class="table-responsive">
														<asp:GridView ID="gvPrecioProveedorListar" runat="server" DataKeyNames="IDPrecioProveedor" AllowPaging="True" Width="100%" OnPageIndexChanging="gvPrecioProveedorListar_PageIndexChanging" OnRowCommand="gvPrecioProveedorListar_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
															<Columns>
																<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																	<ItemTemplate>
																		<%# Eval("IDPrecioProveedor") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Fecha último Precio" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("FechaUltimoPrecio") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
																	<ItemTemplate>
																		<%# Eval("Proveedor") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Último Precio Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("UltimoPrecioCompra") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<div style="width: 40px;">
																			<ul class="icons-list">
																				<li title="Eliminar">
																					<asp:LinkButton ID="LinkButton1" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDPrecioProveedor").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "123")%>'><i class="icon-trash"></i></asp:LinkButton>
																				</li>
																			</ul>
																		</div>
																	</ItemTemplate>
																	<ItemStyle HorizontalAlign="Center" />
																</asp:TemplateField>

															</Columns>
														</asp:GridView>
													</div>
												</ContentTemplate>
											</asp:UpdatePanel>

										</div>
										<div class="tab-pane fade" id="tab44" role="tabpanel" aria-labelledby="border-home-tab">

											<asp:UpdatePanel ID="upProductoCompatibleListar" runat="server" UpdateMode="Conditional">
												<ContentTemplate>
													<asp:HiddenField ID="hdfIDProductoCompatible" runat="server" Value="0" />
													<div class="row">
														<div class="col-md-4">
															<div class="form-group">
																<label class="etiqueta">Producto Compatible</label>
																<asp:DropDownList ID="ddlPCIDProductoComp" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
															</div>
														</div>
														<div class="col-md-2">
															<div class="form-group">
																<label class="etiqueta"></label>
																<asp:Button ID="btnGuardarProductoCompatible" runat="server" SkinID="ui-boton-default" OnClick="btnGuardarProductoCompatible_Click" Text="Agregar" />
															</div>
														</div>
													</div>
													<div class="table-responsive">
														<asp:GridView ID="gvProductoCompatibleListar" runat="server" DataKeyNames="IDProductoCompatible" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProductoCompatibleListar_PageIndexChanging" OnRowCommand="gvProductoCompatibleListar_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
															<Columns>
																<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																	<ItemTemplate>
																		<%# Eval("CodigoBarra") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<%# Eval("Categoria") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Nombre Comercial" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
																	<ItemTemplate>
																		<%# Eval("Nombre") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Localización" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
																	<ItemTemplate>
																		<%# Eval("Localizacion") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																	<ItemTemplate>
																		<%# Eval("Stock") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																	<ItemTemplate>
																		<%# Eval("PrecioVenta") %>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
																	<HeaderStyle HorizontalAlign="Center" />
																	<ItemStyle HorizontalAlign="Center" Width="7%" />
																</asp:CheckBoxField>
																<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
																	<ItemTemplate>
																		<div style="width: 40px;">
																			<ul class="icons-list">
																				<li title="Eliminar">
																					<asp:LinkButton ID="LinkButton2" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDProductoCompatible").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "123")%>'><i class="icon-trash"></i></asp:LinkButton>
																				</li>
																			</ul>
																		</div>
																	</ItemTemplate>
																	<ItemStyle HorizontalAlign="Center" />
																</asp:TemplateField>

															</Columns>
														</asp:GridView>
													</div>
												</ContentTemplate>
											</asp:UpdatePanel>
										</div>
									</div>
								</asp:Panel>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
	</div>

	<div id="ModalProductoPrecio" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Lista de Precios</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>

				<asp:UpdatePanel ID="upProductoPrecioListar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-12">
									<div class="table-responsive">
										<asp:GridView ID="gvProductoPrecioListar" runat="server" DataKeyNames="IdProductoPrecio" AutoGenerateColumns="False" GridLines="None">
											<Columns>
												<asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:HiddenField ID="hdfIdProductoPrecio" runat="server" Value='<%# Eval("IdProductoPrecio") %>' />
														<%# Eval("Sucursal") %> 
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="PrecioVenta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="TextBox1" runat="server" SkinID="ui-textbox-price-requerido" Text='<%# Eval("PrecioVenta" , "{0:N}") %>'></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateField>
											</Columns>
										</asp:GridView>
									</div>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCerrarProductoPrecio" runat="server" Text="Cerrar" SkinID="ui-boton-default" OnClientClick="CerrarModal('ModalProductoPrecio')" />
							<asp:Button ID="btnActualizarProductoPrecio" runat="server" Text="Actualizar" OnClick="btnActualizarProductoPrecio_Click" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>


	<div id="ModalCatalogoProducto" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-xl">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Lista de Productos del Catálogo</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upCatalogoProductoListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnCatalogoProductoListar" runat="server" DefaultButton="btnGuardarProductoCatalogo">
								<div class="row">
									<div class="col-md-12">
										<h5 class="panel-title">Lista de Productos del Catálogo</h5>
									</div>
								</div>
								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Filtro: <b>[Nombre|Laboratorio]</b></label>
											<asp:TextBox ID="txtFiltroCatalogoProducto" runat="server" MaxLength="100" CausesValidation="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-12">
										<div class="table-responsive" style="margin-bottom: 0;">
											<div style="overflow-y: auto; max-height: 300px; width: 100%;">
												<asp:GridView ID="gvCatalogoProductoListar" runat="server" DataKeyNames="IDProductoTemp" AllowPaging="false" Width="100%" ShowHeader="true" ShowHeaderWhenEmpty="true" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
													<Columns>
														<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
															<ItemTemplate>
																<div style="min-width: 35px;"><%# Eval("Cod_Prod") %></div>
																<asp:HiddenField ID="hfIDProductoTemp" runat="server" Value='<%# Eval("IDProductoTemp") %>' />
																<asp:HiddenField ID="hfCodigo" runat="server" Value='<%# Eval("Cod_Prod") %>' />
																<asp:HiddenField ID="hfNombreCompleto" runat="server" Value='<%# Eval("NombreCompleto") %>' />
																<asp:HiddenField ID="hfLaboratorio" runat="server" Value='<%# Eval("Nom_Titular") %>' />
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
															<ItemTemplate>
																<div><span><%# Eval("NombreCompleto") %></span></div>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Laboratorio" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
															<ItemTemplate>
																<div><span><%# Eval("Nom_Titular") %></span></div>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="*" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
															<HeaderTemplate>
																<asp:CheckBox ID="chkCATSeleccionarTodos" runat="server" ClientIDMode="Static" />
															</HeaderTemplate>
															<ItemTemplate>
																<asp:CheckBox ID="chkCATSeleccionar" runat="server" ClientIDMode="Static" data-id='<%# Eval("IDProductoTemp").ToString() %>' />
															</ItemTemplate>
														</asp:TemplateField>
													</Columns>
												</asp:GridView>
											</div>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
								<div class="espacio"></div>
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<asp:Button ID="btnCerrarProductoCatalogo" runat="server" ClientIDMode="Static" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalCatalogoProducto'); return false;" />
											<asp:Button ID="btnGuardarProductoCatalogo" runat="server" Text="Guardar" OnClick="btnGuardarProductoCatalogo_Click" />
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

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />
	<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

	<script type="text/javascript">


		function ConfigJS() {

			$("#cphPrincipal_txtFiltroCatalogoProducto").on("keyup", function () {
				BuscarCatalogoProducto();
			});

			$("#<%= ddlPCIDProductoComp.ClientID %>").select2({
				width: "100%"
			});



			$("#chkCATSeleccionarTodos").on("change", function () {
				$("#cphPrincipal_gvCatalogoProductoListar #chkCATSeleccionar").each(function () {
					$(this).prop("checked", $("#chkCATSeleccionarTodos").is(":checked"));
				});
				//HabilitarBotonGuardarProductoCatalogo();
			});
			$("#cphPrincipal_gvCatalogoProductoListar #chkCATSeleccionar").on("change", function () {
				var SeleccionarTodos = true;
				if ($("#cphPrincipal_gvCatalogoProductoListar #chkCATSeleccionar:not(:checked)").length > 0) {
					SeleccionarTodos = false;
				}
				$("#chkCATSeleccionarTodos").prop("checked", SeleccionarTodos);
				//HabilitarBotonGuardarProductoCatalogo();
			});

		}

		function BuscarCatalogoProducto() {
			var value = $("#cphPrincipal_txtFiltroCatalogoProducto").val().toLowerCase().trim();
			$("table#cphPrincipal_gvCatalogoProductoListar tr").each(function (index) {
				if (!index) return;
				$(this).find("span").each(function () {
					var id = $(this).text().toLowerCase().trim();
					var not_found = (id.indexOf(value) == -1);
					$(this).closest('tr').toggle(!not_found);
					return not_found;
				});
			});
		}

		function HabilitarBotonGuardarProductoCatalogo() {
			$('#btnGuardarProductoCatalogo').prop("disabled", true);
			$('#btnGuardarProductoCatalogo').addClass("aspNetDisabled");
			var Seleccionados = $("#cphPrincipal_gvCatalogoProductoListar #chkCATSeleccionar:checked").length;
			if (Seleccionados > 0) {
				$('#btnGuardarProductoCatalogo').prop("disabled", false);
				$('#btnGuardarProductoCatalogo').removeClass("aspNetDisabled");
			}
		}



		function ActivarTabxId(id) {
			var x = $("#TabActivo").val(id);
			ActivarTabxBoton();
		}

		function ActivarTabxBoton() {
			$('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
		}

	</script>

</asp:Content>
