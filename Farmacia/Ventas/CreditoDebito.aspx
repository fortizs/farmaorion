<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreditoDebito.aspx.cs" Inherits="Farmacia.Ventas.CreditoDebito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
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
			<h3 class="panel-title">Crédito y Débito</h3>
		</div>
		<div class="panel-body">
			<asp:Panel ID="pnMensaje" runat="server" Visible="true">
				<div class="widget-content widget-content-area">
					<div class="alert alert-arrow-left alert-icon-left alert-light-primary mb-4" role="alert">
						<button type="button" class="close" data-dismiss="alert" aria-label="Close">
							<i class="icon-cross3 mr-3 icon-2x"></i>
						</button>
						<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-bell">
							<path d="M18 8A6 6 0 0 0 6 8c0 7-3 9-3 9h18s-3-2-3-9"></path><path d="M13.73 21a2 2 0 0 1-3.46 0"></path></svg>
						<strong>Warning!</strong>
						<asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
					</div>
				</div>
			</asp:Panel>
			<asp:Panel ID="pnRegistro" runat="server" Visible="false">

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
					<div class="tab-pane fade" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
						<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<div class="row">
									<div class="col-md-5">
										<div class="form-group">
											<label>Cliente:</label>
											<asp:DropDownList ID="ddlBIDCliente" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Tipo Comprobante:</label>
											<asp:DropDownList ID="ddlBIDTipoComprobante" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Estado:</label>
											<asp:DropDownList ID="ddlBIDEstado" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Estado Cobranza:</label>
											<asp:DropDownList ID="ddlBIDEstadoCobranza" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Filtro:</label>
											<asp:TextBox ID="txtBFiltro" runat="server"></asp:TextBox>
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
									<div class="col-md-3">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
											<asp:Button ID="btnMigrar" runat="server" Text="Migrar" OnClick="btnMigrar_Click" />
										</div>
									</div>
								</div>
								<div class="table-responsive">
									<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDVenta" Width="100%" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand">
										<Columns>
											<asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title='<%# Eval("TipoComprobante") %>'><%# Eval("TipoComprobanteCodigoSunat") %></span>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie-Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" style="margin: 2px;" title='Comprobante'><%# Eval("SerieNumero") %></span><br />
													<span class="badge badge-pills outline-badge-danger" data-popup="tooltip" title='Afectado'><%# Eval("SerieNumeroAfectado") %></span>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaVenta", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
												<ItemTemplate>
													<b>Nro.Doc:</b><%# Eval("NumeroDocumentoCliente") %><br />
													<b>Cliente:</b><%# Eval("Cliente") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("Simbolo") + " " + Eval("TotalVenta") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("EstadoSunat") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Acciones" ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<div style="min-width: 75px;">
														<ul class="icons-list">
															<li class="text-primary" style='<%# (Eval("Migrado").ToString() == "S" || Boolean.Parse(Eval("Anulado").ToString())) ? "display:none;": "width: 35px;" %>' title="Ver Comprobante">
																<asp:LinkButton ID="lnkEditar" runat="server" SkinID="ui-link-boton-primario" CommandName="Editar" CausesValidation="False" CommandArgument='<%# Eval("IDVenta").ToString() %>'><span class="icon-pencil7"></span></asp:LinkButton>
															</li>
															<li class="text-warning" style="width: 35px;">
																<a href="javascript:;" title="Imprimir pdf" class="btn btn-primary btn-xs" onclick="<%# String.Format("return ModalImpresion(" + (char)39 + "#ModalImprimir" + (char)39 + ", " + (char)39 + "{0}?ID={1}" + (char)39 + ");", ResolveClientUrl("~/Ventas/Imprimir.aspx"), Eval("IDVenta").ToString().Trim()) %>" target="_blank" title="Imprimir Comprobante"><i class="icon-printer"></i></a>
															</li>
														</ul>
													</div>
												</ItemTemplate>
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
					<div class="tab-pane fade show active" id="tab2" role="tabpanel" aria-labelledby="border-profile-tab">
						<asp:UpdatePanel ID="upRegistroVenta" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Panel ID="pnRegistroVenta" runat="server">
									<asp:HiddenField ID="hdfIDVenta" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDVentaDetalle" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDCliente" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDVentaDetalleTemp" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDVentaAfectado" runat="server" Value="0" />
									<asp:HiddenField ID="hdfIDMoneda" runat="server" Value="0" />
									<div class="row">
										<div class="col-md-2">
											<div class="form-group">
												<label>Tipo Comprobante:</label>
												<asp:DropDownList ID="ddlIDTipoComprobante" SkinID="ui-dropdownlist-requerido" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDTipoComprobante_SelectedIndexChanged"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Serie-Número:</label>
												<asp:TextBox ID="txtSerieNumero" runat="server" SkinID="ui-textbox-requerido" Enabled="false"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-3">
											<div class="form-group">
												<label>Tipo Motivo:</label>
												<asp:DropDownList ID="ddlTipoMotivo" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Fecha Emisión:</label>
												<asp:TextBox ID="txtFechaEmision" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-calendar"></i>
												</div>
											</div>
										</div>
										<div class="col-md-3">
											<div class="form-group">
												<label>Motivo:</label>
												<asp:TextBox ID="txtMotivo" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-2">
											<div class="form-group">
												<label>Serie-Número Afectado:</label>
												<div class="input-group">
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
												<asp:DropDownList ID="ddlIDTipoComprobanteAfectado" runat="server" Enabled="false"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-3">
											<div class="form-group">
												<label>Cliente:</label>
												<asp:TextBox ID="txtRegClienteAfectado" runat="server" Enabled="false"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-6">
											<div class="form-group">
												<label>Producto:<b>[Código|Nombre|Principio activo]</b></label>
												<div class="input-group">
													<asp:TextBox ID="txtFiltroProducto" SkinID="ui-textbox-requerido" runat="server" AutoPostBack="true" OnTextChanged="txtFiltroProducto_TextChanged"></asp:TextBox>
													<span class="input-group-btn">
														<asp:LinkButton ID="lnkBuscarProducto" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkBuscarProducto_Click"><i class="icon-search4"></i></asp:LinkButton>
													</span>
												</div>
											</div>
										</div>
									</div>
									<div class="espacio"></div>

									<asp:Panel ID="pnVentaDetalleTempListar" runat="server" Visible="false">
										<div class="row">
											<div class="col-md-12">
												<div class="table-responsive" style="margin-bottom: 0;">
													<asp:GridView ID="gvVentaDetalleTempListar" runat="server" DataKeyNames="IDVentaDetalleTemp" AllowPaging="false" OnPageIndexChanging="gvVentaDetalleTempListar_PageIndexChanging" OnRowCommand="gvVentaDetalleTempListar_RowCommand" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
														<Columns>
															<asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
																<ItemTemplate>
																	<asp:HiddenField ID="hfImporteTotal" runat="server" Value='<%# Eval("ImporteTotal", "{0:N}") %>' />
																	<asp:HiddenField ID="hfDescuento" runat="server" Value='<%# Eval("Descuento", "{0:N}") %>' />
																	<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal", "{0:N}") %>' />
																	<%# Eval("Item") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="left" ItemStyle-Width="35%">
																<ItemTemplate>
																	<%# Eval("Codigo") %> - <%# Eval("Producto") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("Cantidad") +  " " + Eval("UnidadMedidaVenta") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Descuento" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<b>(<%# Eval("PorcentajeDescuento", "{0:N}") %>%)</b>  <%# Eval("Descuento", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("PrecioVenta", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Importe" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("ImporteTotal", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:CheckBoxField DataField="ControlaLote" HeaderText="Lote?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
																<HeaderStyle HorizontalAlign="Center" />
																<ItemStyle HorizontalAlign="Center" Width="7%" />
															</asp:CheckBoxField>
															<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
																<ItemTemplate>
																	<div style="width: 85px;">
																		<ul class="icons-list">
																			<li class="text-primary" style="width: 35px;">
																				<asp:LinkButton ID="LinkButton1" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDVentaDetalleTemp").ToString() + ";" + Eval("Codigo").ToString() + ";" + Eval("Producto").ToString() + ";" + Eval("ControlaLote").ToString() + ";" + Eval("Cantidad").ToString() %>'><span class="icon-pencil7"></span></asp:LinkButton>
																			</li>
																			<li class="text-danger" style="width: 35px;">
																				<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDVentaDetalleTemp").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este producto?\",\"{0}\"); return false;", "123")%>'><i class="icon-trash"></i></asp:LinkButton>
																			</li>
																		</ul>
																	</div>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
												</div>
											</div>
										</div>
									</asp:Panel>
									<asp:Panel ID="pnVentaDetalleListar" runat="server" Visible="false">
										<div class="row">
											<div class="col-md-12">
												<div class="table-responsive" style="margin-bottom: 0;">
													<asp:GridView ID="gvVentaDetalleListar" runat="server" DataKeyNames="IDVentaDetalle" AllowPaging="false" OnPageIndexChanging="gvVentaDetalleListar_PageIndexChanging" OnRowCommand="gvVentaDetalleListar_RowCommand" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
														<Columns>
															<asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
																<ItemTemplate>
																	<asp:HiddenField ID="hfImporteTotal" runat="server" Value='<%# Eval("ImporteTotal", "{0:N}") %>' />
																	<asp:HiddenField ID="hfDescuento" runat="server" Value='<%# Eval("Descuento", "{0:N}") %>' />
																	<asp:HiddenField ID="hfSubTotal" runat="server" Value='<%# Eval("SubTotal", "{0:N}") %>' />
																	<%# Eval("Item") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="left" ItemStyle-Width="35%">
																<ItemTemplate>
																	<%# Eval("Codigo") %> - <%# Eval("Producto") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("Cantidad") +  " " + Eval("UnidadMedidaVenta") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Descuento" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<b>(<%# Eval("PorcentajeDescuento", "{0:N}") %>%)</b>  <%# Eval("Descuento", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Descuento" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<b>(<%# Eval("PorcentajeDescuento", "{0:N}") %>%)</b>  <%# Eval("Descuento", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("PrecioVenta", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Importe" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("ImporteTotal", "{0:N}") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:CheckBoxField DataField="ControlaLote" HeaderText="Lote?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
																<HeaderStyle HorizontalAlign="Center" />
																<ItemStyle HorizontalAlign="Center" Width="7%" />
															</asp:CheckBoxField>
															<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
																<ItemTemplate>
																	<div style="width: 85px;">
																		<ul class="icons-list">
																			<li class="text-primary" style="width: 35px;">
																				<asp:LinkButton ID="LinkButton1" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="VerLote" CommandArgument='<%# Eval("IDVentaDetalle").ToString() + ";" + Eval("Codigo").ToString() + ";" + Eval("Producto").ToString() + ";" + Eval("ControlaLote").ToString() + ";" + Eval("Cantidad").ToString() %>'><span class="icon-pencil7"></span></asp:LinkButton>
																			</li>
																		</ul>
																	</div>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
												</div>
											</div>
										</div>
									</asp:Panel>

									<div class="espacio"></div>
									<div class="row">
										<div class="col-sm-7">
										</div>
										<div class="col-sm-5 text-right">
											<table class="ui-table">
												<tbody>
													<tr>
														<th width="25%" style="text-align: right!important;">
															<div style="min-width: 140px;">OP. EXONERADA:</div>
														</th>
														<td width="25%" class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda1" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteOperacionExonerada" runat="server" Text="0.00"></asp:Label>
														</td>
														<th width="25%" style="text-align: right!important;">
															<div style="min-width: 140px;">SUB TOTAL:</div>
														</th>
														<td width="25%" class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda2" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteSubTotal" runat="server" Text="0.00"></asp:Label>
														</td>
													</tr>
													<tr>
														<th style="text-align: right!important;">
															<div style="min-width: 140px;">OP. INAFECTA:</div>
														</th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda3" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteOperacionInafecta" runat="server" Text="0.00"></asp:Label>
														</td>
														<th style="text-align: right!important;">
															<div style="min-width: 140px;">IGV(18%):</div>
														</th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda4" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteTotalIgv" runat="server" Text="0.00"></asp:Label>
														</td>
													</tr>
													<tr>
														<th style="text-align: right!important;">
															<div style="min-width: 140px;">OP. GRATUITA:</div>
														</th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda5" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteOperacionGratuita" runat="server" Text="0.00"></asp:Label>
														</td>
														<th style="text-align: right!important;">
															<div style="min-width: 140px;">DESCUENTO:</div>
														</th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="Label3" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblDescuento" runat="server" Text="0.00"></asp:Label>
														</td>
													</tr>
													<tr>
														<th style="text-align: right!important;"></th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;"></td>
														<th style="text-align: right!important;">
															<div style="min-width: 140px;">TOTAL VENTA:</div>
														</th>
														<td class="text-right" style="font-size: 16px; font-weight: 700;">
															<asp:Label ID="lblMoneda6" runat="server" Text="S/"></asp:Label>
															<asp:Label ID="lblImporteTotalVenta" runat="server" Text="0.00"></asp:Label>
														</td>
													</tr>
												</tbody>
											</table>
										</div>
									</div>
								</asp:Panel>
								<div class="espacio"></div>
								<div class="separador"></div>
								<div class="espacio"></div>
								<div class="row text-right">
									<div class="col-md-12">
										<div class="form-group">
											<asp:LinkButton ID="lnkNuevaVenta" runat="server" CssClass="btn btn-default" OnClick="lnkNuevaVenta_Click"><i class="fa fa-hand-paper-o"></i> Nueva Nota</asp:LinkButton>
											<asp:LinkButton ID="lnkPagoRapido" runat="server" CssClass="btn btn-primary" OnClick="lnkPagoRapido_Click"><i class="icon-cash3"></i> Forma Pago</asp:LinkButton>
										</div>
									</div>
								</div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</asp:Panel>
		</div>
	</div>

	<div id="ModalImprimir" class="modal fade mprint">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-body">
					<div class="loading-iframe">
						<iframe src="" style="width: 100%; height: 450px; border: none;"></iframe>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-link" data-dismiss="modal">Cerrar</button>
				</div>
			</div>
		</div>
	</div>

	<div id="ModalProducto" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Busqueda de Productos</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upProductoListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnProductoListar" runat="server" DefaultButton="btnBuscarProducto">
								<asp:HiddenField ID="hfIDProducto" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12">
										<div class="row">
											<div class="col-md-8">
												<div class="form-group">
													<label>Filtro:<b>[Código|Nombre]</b></label>
													<asp:TextBox ID="txtFiltroPro" runat="server"></asp:TextBox>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnBuscarProducto" runat="server" SkinID="ui-boton-default" Text="Buscar" OnClick="btnBuscarProducto_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvProductoListar" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProductoListar_PageIndexChanging" OnSelectedIndexChanged="gvProductoListar_SelectedIndexChanged" DataKeyNames="IDProducto" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
														<ItemTemplate>
															<%# Eval("CodigoBarra") %><br />
															<%# Eval("Nombre") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Localización" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Localizacion") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("Stock") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# Eval("PrecioVenta") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:CheckBoxField DataField="ControlaLote" HeaderText="Lote?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Center" Width="7%" />
													</asp:CheckBoxField>
													<asp:CheckBoxField DataField="VentaConReceta" HeaderText="Receta?" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<HeaderStyle HorizontalAlign="Center" />
														<ItemStyle HorizontalAlign="Center" Width="7%" />
													</asp:CheckBoxField>
													<asp:TemplateField ShowHeader="False" HeaderText="Sel." ItemStyle-Width="7%">
														<ItemTemplate>
															<div style="width: 40px;">
																<ul class="icons-list">
																	<li style="width: 35px">
																		<asp:LinkButton ID="lnkSeleccionarProducto" SkinID="ui-link-boton-primario" runat="server" ToolTip="Seleccionar Producto" CausesValidation="False" CommandName="Select"><span class="icon-multitouch"></span></asp:LinkButton>
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
							</asp:Panel>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<div id="ModalSalidaManualLote" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Salida Manual de Lotes</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upSalidaManualLote" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<asp:HiddenField ID="hfSLIDProducto" runat="server" Value="0" />
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label>Código Barra:</label>
										<asp:TextBox ID="txtSLCodigoBarra" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-8">
									<div class="form-group">
										<label>Producto:</label>
										<asp:TextBox ID="txtSLProducto" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
							</div>
							<div class="table-responsive" style="margin-bottom: 0;">
								<asp:GridView ID="gvLotexProducto" runat="server" DataKeyNames="IDLote" AllowPaging="false" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="Nro.Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<asp:HiddenField ID="hfIDLote" runat="server" Value='<%# Eval("IDLote") %>' />
												<%# Eval("Lote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Caducidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Fabricación" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Stock Actual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("CantidadLote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Salida Manual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<asp:TextBox ID="txtSLSalidaManual" runat="server" SkinID="ui-textbox-number-requerido" Text="0"></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCerrarLote" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalSalidaManualLote')" />
							<asp:Button ID="btnAgregarLote" runat="server" Text="Agregar" OnClick="btnAgregarLote_Click" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalFormaPago" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<asp:UpdatePanel ID="upVentaFormaPago" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnVentaFormaPago" runat="server" DefaultButton="btnConfirmarVenta">
							<div class="modal-body">
								<div class="row">
									<div class="col-md-12 text-center">
										<div class="form-group">
											<label style="color: #003ac8;"><b>Total de la Nota de Crédito o Débito</b></label>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12 text-center">
										<div class="form-group">
											<label style="font-size: 42px; font-weight: 700; color: #003ac8;">
												S/
												<asp:Literal ID="ltTotalPagar" runat="server"></asp:Literal></label>
										</div>
									</div>
								</div>
								<div class="row" style="margin-top: -20px;">
									<div class="col-md-12 text-center">
										<div class="form-group">
											<label style="font-weight: 700; color: #003ac8;">(<asp:Literal ID="ltImporteLetras" runat="server"></asp:Literal>)</label>
										</div>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-md-4 text-center">
										<div class="form-group">
											<img src="../Recursos/assets/img/efectivo.svg" style="width: 30%;" /><br />
											<label><b>Efectivo</b></label>
											<asp:TextBox ID="txtEfectivo" runat="server" SkinID="ui-textbox-price" Text="0.00" AutoPostBack="true" OnTextChanged="txtEfectivo_TextChanged"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4 text-center">
										<div class="form-group">
											<img src="../Recursos/assets/img/tarjeta.svg" style="width: 30%;" /><br />
											<label><b>Tarjeta:</b></label>
											<asp:TextBox ID="txtTarjeta" runat="server" SkinID="ui-textbox-price" Text="0.00" AutoPostBack="true" OnTextChanged="txtTarjeta_TextChanged"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4 text-center">
										<div class="form-group">
											<img src="../Recursos/assets/img/transferencia.svg" style="width: 30%;" /><br />
											<label><b>Transferencia:</b></label>
											<asp:TextBox ID="txtTransferencia" runat="server" SkinID="ui-textbox-price" Text="0.00" AutoPostBack="true" OnTextChanged="txtTransferencia_TextChanged"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label><b>Forma Pago:</b></label>
											<asp:DropDownList ID="ddlVIDFormaPago" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-9">
										<div class="form-group">
											<label><b>Referencia:</b></label>
											<asp:TextBox ID="txtReferencia" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<label style="font-size: 20px; font-weight: 700; color: #003ac8;"><b>Resta:</b></label><br />
											<label style="font-size: 33px; font-weight: 800; color: #247a00;">
												S/
												<asp:Literal ID="ltVuelto" runat="server"></asp:Literal></label>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="Button2" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalFormaPago')" />
								<asp:Button ID="btnConfirmarVenta" runat="server" Text="Confirmar Venta" CausesValidation="False" OnClick="btnConfirmarVenta_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalVentaDetalleLoteTemp" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Salida Manual de Lotes</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upVentaDetalleLoteTempListar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label>Código Barra:</label>
										<asp:TextBox ID="txtVLCodigoBarra" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-8">
									<div class="form-group">
										<label>Producto:</label>
										<asp:TextBox ID="txtVLProducto" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
							</div>
							<div class="table-responsive" style="margin-bottom: 0;">
								<asp:GridView ID="gvVentaDetalleLoteTempListar" runat="server" DataKeyNames="IDVentaDetalleLoteTemp" AllowPaging="false" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="Nro.Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<asp:HiddenField ID="hfVDTIDVentaDetalleLoteTemp" runat="server" Value='<%# Eval("IDVentaDetalleLoteTemp") %>' />
												<asp:HiddenField ID="hfVDTIDLote" runat="server" Value='<%# Eval("IDLote") %>' />
												<asp:HiddenField ID="hfVDTCantidad" runat="server" Value='<%# Eval("Cantidad") %>' />
												<%# Eval("Lote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Caducidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Fabricación" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Stock Actual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("StockActualLote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("Cantidad") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Devolver" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<asp:TextBox ID="txtVDTSalidaManual" runat="server" SkinID="ui-textbox-number-requerido" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCerrarVentaDetalleLote" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalVentaDetalleLoteTemp')" />
							<asp:Button ID="btnActualizarVentaDetalleLote" runat="server" Text="Agregar" OnClick="btnActualizarVentaDetalleLote_Click" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalVentaDetalleLote" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Salida Manual de Lotes</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upVentaDetalleLoteListar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label>Código Barra:</label>
										<asp:TextBox ID="txtVDLCodigoBarra" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-8">
									<div class="form-group">
										<label>Producto:</label>
										<asp:TextBox ID="txtVDLProducto" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
							</div>
							<div class="table-responsive" style="margin-bottom: 0;">
								<asp:GridView ID="gvVentaDetalleLoteListar" runat="server" DataKeyNames="IDVentaDetalleLote" AllowPaging="false" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="Nro.Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("Lote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Caducidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Fabricación" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Stock Actual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("StockActualLote") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Salida Manual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("Cantidad") %>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="Button1" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalVentaDetalleLote')" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalVentaDetalle" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Actualizar Cantidad</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upVentaDetalleActualizar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label>Código Barra:</label>
										<asp:TextBox ID="txtVDCodigoBarra" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-8">
									<div class="form-group">
										<label>Producto:</label>
										<asp:TextBox ID="txtVDProducto" runat="server" Enabled="false"></asp:TextBox>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">
									<div class="form-group">
										<label>Cantidad:</label>
										<asp:TextBox ID="txtVDCantidad" runat="server" Enabled="false" SkinID="ui-textbox-number-requerido"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-4">
									<div class="form-group">
										<label>Devolver:</label>
										<asp:TextBox ID="txtVDDevolver" runat="server" SkinID="ui-textbox-number-requerido"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCerrarVentaDetalle" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalVentaDetalle')" />
							<asp:Button ID="btnActualizarVentaDetalle" runat="server" Text="Guardar" OnClick="btnActualizarVentaDetalle_Click" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalVentaAfectado" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Lista de Facturas y Boletas </h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upVentaAfectadaListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnVentaAfectadaListar" runat="server" DefaultButton="btnBuscarComprobanteAfectado">
								<div class="row">
									<div class="col-md-12">
										<div class="row">
											<div class="col-md-6">
												<div class="form-group">
													<label class="control-label">Filtro:<b>[SerieNumero|NumeroDocumento|Nombres]</b></label>
													<asp:TextBox ID="txtVEFiltro" runat="server"></asp:TextBox>
												</div>
											</div>
											<div class="col-md-4">
												<div class="form-group">
													<label class="control-label">Tipo Comprobante:</label>
													<asp:DropDownList ID="ddlVEIDTipoComprobante" runat="server"></asp:DropDownList>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnBuscarComprobanteAfectado" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscarComprobanteAfectado_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvDocumentoElectronicoListar" runat="server" DataKeyNames="IDVenta" AutoGenerateColumns="False" Width="99%" GridLines="None" OnPageIndexChanging="gvDocumentoElectronicoListar_PageIndexChanging" OnSelectedIndexChanged="gvDocumentoElectronicoListar_SelectedIndexChanged" AllowPaging="true">
												<Columns>
													<asp:TemplateField HeaderText="Serie - Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblTipoComprobante" runat="server" Text='<%# Bind("TipoComprobanteCodigoSunat") %>'></asp:Label><br />
															<asp:Label ID="lblSerieNumero" runat="server" Text='<%# Bind("SerieNumero") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Cliente" ItemStyle-Width="25%">
														<ItemTemplate>
															<asp:Label ID="lblNumeroDocumentoCliente" runat="server" Text='<%# Bind("NumeroDocumentoCliente") %>'></asp:Label><br />
															<asp:Label ID="lblCliente" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblFechaEmision" runat="server" Text='<%# Bind("FechaVenta") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Simbolo") + " " + Eval("TotalVenta") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Acción" ItemStyle-Width="5%">
														<ItemTemplate>
															<div style="width: 40px;">
																<ul class="icons-list">
																	<li style="width: 35px">
																		<asp:LinkButton ID="lblSeleccionar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Seleccionar"><span class="icon-multitouch"></span></asp:LinkButton>
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
							</asp:Panel>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnBuscarComprobanteAfectado" EventName="Click" />
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

		}

		function Imprimir(ID, Tipo) {
			$("#ModalImprimir iframe").attr("src", "");
			$("#ModalImprimir iframe").attr("src", 'ImprimirDocumento.ashx?ID=' + ID + '&Tipo=' + Tipo);
			$("#ModalImprimir").modal();
			return false;

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

