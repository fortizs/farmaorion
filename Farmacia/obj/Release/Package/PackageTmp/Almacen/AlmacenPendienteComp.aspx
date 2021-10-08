<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlmacenPendienteComp.aspx.cs" Inherits="Farmacia.Almacen.AlmacenPendienteComp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Entradas / Ingreso por Compras</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Almacen:</label>
									<asp:DropDownList ID="ddlIDAlmacen" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3">
								<div class="form-group">
									<label>Filtro <strong>(RUC/Proveedor/Nro Compra/Nro Factura)</strong>:</label>
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
									<label>Estado Almacen:</label>
									<asp:DropDownList ID="ddlIDEstadoAlmacen" runat="server"></asp:DropDownList>
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
							<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDCompras" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateField HeaderText="Fecha Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
										<ItemTemplate>
											<%# Eval("FechaCompra") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="TipoDoc" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("TipoDocumento") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="SerieNumero" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("SerieNumero") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
										<ItemTemplate>
											<%# Eval("Sucursal") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Nro.Documento" ItemStyle-Width="5%">
										<ItemTemplate>
											<%# Eval("RucProveedor") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Proveedor" ItemStyle-Width="20%">
										<ItemTemplate>
											<%# Eval("Proveedor") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
										<ItemTemplate>
											<%# Eval("Moneda").ToString() +" "+ Eval("TotalCompra", "{0:N}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("EstadoAlmacen") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField ShowHeader="False" HeaderText="Despacho" ItemStyle-Width="5%">
										<ItemTemplate>
											<div style="width: 40px;">
												<ul class="icons-list">
													<li style='<%# (Int32.Parse(Eval("IDEstadoAlmacen").ToString()) == 2 )  ? "display:none;": "width: 35px;" %>'>
														<asp:LinkButton ID="lbIngreso" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Ingreso al Almacen"><span class="glyphicon glyphicon-pushpin"></span></asp:LinkButton>
													</li>
												</ul>
											</div>
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
					</asp:Panel>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="gvLista" EventName="PageIndexChanging" />
				</Triggers>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="ModalIngresoCompra" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Ingreso a Almacen por Compras</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>

				<asp:UpdatePanel ID="upIngresoAlmacenCompra" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnIngresoAlmacenCompra" runat="server" DefaultButton="btnGuardar">


							<div class="modal-body">
								<asp:HiddenField ID="hdfIDCompra" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Nro.Orden Compra:</label>
											<asp:TextBox ID="txtNumeroOrdenCompra" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-8">
										<div class="form-group">
											<label>Proveedor:</label>
											<asp:TextBox ID="txtProveedor" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group has-feedback">
											<label>Fecha Emisión:</label>
											<asp:TextBox ID="txtFechaEmision" SkinID="ui-textbox-fecha-simple" Enabled="false" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group has-feedback">
											<label>Fecha Ingreso:</label>
											<asp:TextBox ID="txtFechaIngresoAlmacen" SkinID="ui-textbox-fecha-simple" Enabled="false" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Transacción:</label>
											<asp:DropDownList ID="ddlIDTransaccion" runat="server" Enabled="false"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Almacen Ingreso:</label>
											<asp:DropDownList ID="ddlIDAlmacenIngreso" runat="server"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Glosa:</label>
											<asp:TextBox ID="txtGlosa" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<asp:HiddenField ID="hdfIDMovimiento" runat="server" Value="0" />
										<asp:HiddenField ID="hdIDProductoLote" runat="server" Value="0" />
										<asp:HiddenField ID="hdfIDProductoSeleccionado" runat="server" Value="0" />
										<asp:HiddenField ID="hdfTotalLote" runat="server" Value="0" />
										<div class="table-responsive" style="margin-bottom: 0;">
											<asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto,CantidadVenta" AllowPaging="True" Width="100%" AllowSorting="True" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
														<ItemTemplate>
															<%# Eval("Producto") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cantidad Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("Cantidad") + " "+  Eval("UnidadMedida")%>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Factor" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("Factor") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cantidad a Ingresar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("CantidadVenta") +" "+Eval("UnidadMedidaVenta") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Existe Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# (Boolean.Parse(Eval("ControlaLote").ToString())) ? "SI" : "NO" %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="N° LOTE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<asp:Label ID="lblLote" runat="server" Text=""></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Saldo Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<asp:Label ID="lblSaldoCantidad" runat="server" Text=' <%# (Boolean.Parse(Eval("ControlaLote").ToString())) ? Eval("CantidadVenta") : "0" %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
														<ItemTemplate>
															<div style="<%# (Boolean.Parse(Eval("ControlaLote").ToString())) ? "width:40px": "display:none" %>">
																<ul class="icons-list">
																	<li style="width: 30px">
																		<asp:LinkButton ID="lnkLote" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Ingreso Lote"><span class="fa fa-cube"></span></asp:LinkButton>
																	</li>
																</ul>
															</div>
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center" />
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
									</div>
								</div>
								<div class="espacio"></div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCerrar" runat="server" Text="Salir" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalIngresoCompra')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Ingresar" OnClick="btnGuardar_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
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
								<asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
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
										<%--<div class="form-group">
                                             <label>Saldo Cantidad:</label>
                                            <asp:TextBox ID="txtLTotal" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>--%>
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


	<%--<div id="DatosAgregarLoteProducto" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Lotes</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="pnRegistroProducto" runat="server" DefaultButton="lnkNuevoLote">
                    <asp:UpdatePanel ID="upRegistroProducto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <p style="text-transform: uppercase; font-weight: bold;">
                                            Elegir Lote
                                        </p>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        
                                    </div>
                                    <div class="espacio"></div>
                                    <div class="espacio"></div>
                                    <div class="col-md-12">
                                        <div class="table-responsive" style="margin-bottom: 0;">
                                            <asp:GridView ID="gvLoteProducto" runat="server" DataKeyNames="IDLote,Lote" AllowPaging="True" Width="100%" AllowSorting="True" OnRowCommand="gvLoteProducto_RowCommand" GridLines="None" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("Lote") %>
                                                            <asp:Label ID="lblNombreLote" runat="server" Text=' <%# Eval("Lote") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("CantidadLote") %>
                                                            <asp:Label ID="lblCantidadLote" runat="server" Text='<%# Eval("CantidadLote") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("CantidadLote") %>
                                                            <asp:Label ID="lblCantidadLote" runat="server" Text='<%# Eval("CantidadLote") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fabricacion" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
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
                                            <label>Saldo Cantidad:</label>
                                            <asp:TextBox ID="txtLTotal" runat="server" ReadOnly="true"></asp:TextBox>
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
    </div>--%>

	<%-- Modal Lote--%>
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
											<label>Lote:</label>
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
								<%--<div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtLCantidad" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
                                            <asp:TextBox ID="txtLCantidadAnterior" runat="server" SkinID="ui-textbox-requerido" Visible="false" Text="0"></asp:TextBox>
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
	<script type="text/javascript">

		function ConfigJS() {

		}

		function ActivarTabxId(id) {
			var x = $("#TabActivo").val(id);
			ActivarTabxBoton();
		}

		function ActivarTabxBoton() {
			$('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
		}

		function funModalLoteAbrir() {
			$('#DatosLote').modal('show');
		}

		function funModalLoteCerrar() {
			$('#DatosLote').modal('hide');
		}

		function funModalListaLoteAbrir() {
			$('#DatosAgregarLoteProducto').modal('show');
		}

		function funModalListaLoteCerrar() {
			$('#DatosAgregarLoteProducto').modal('hide');
		}

	</script>
</asp:Content>
