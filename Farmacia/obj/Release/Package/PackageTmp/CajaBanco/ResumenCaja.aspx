<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ResumenCaja.aspx.cs" Inherits="Farmacia.CajaBanco.ResumenCaja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.resumen {
			display: block;
			text-align: center;
		}

			.resumen .resumen-item {
				display: inline-block;
				padding: 3px 1px;
				font-size: 11px;
				font-weight: bold;
			}

			.resumen span.resumen-ico {
				display: inline-block;
				width: 20px;
				text-align: center;
			}

		.separador {
			height: 1px;
			clear: both;
			border-top: 1px dotted #bbb;
		}

		.resumen-user {
			display: block;
			text-align: left;
		}

		.gfot {
			border: 1px solid #f9dd34;
			background: #ffef8f url(plugins/jquery_ui/cupertino/images/ui-bg_highlight-soft_25_ffef8f_1x100.png) 50% top repeat-x;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Resumen de Caja</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<asp:HiddenField ID="hdfCJIDCaja" runat="server" Value="0" />
						<div class="row">
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
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
									<asp:Button ID="btnNuevoAperturarCaja" runat="server" Text="Aperturar Caja" OnClick="btnNuevoAperturarCaja_Click" />
								</div>
							</div>
						</div>
						<div class="table-responsive">
							<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDCaja" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
										<ItemTemplate>
											<%# Eval("Fecha", "{0:dd/MM/yyyy}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="8%">
										<ItemTemplate>
											<%# Eval("Sucursal") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Caja" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("CajaMecanica") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Apertura/Cierre" ItemStyle-HorizontalAlign="left" ItemStyle-Width="20%">
										<ItemTemplate>
											<div class="resumen-user">
												<div class="resumen-item" data-popup="tooltip" data-placement="left" data-original-title="Usuario y Fecha de Apertura"><span class="resumen-ico"><i class="text-primary"></i></span><span><%# Eval("UsuarioApertura") %>-<%# Eval("FechaApertura") %></span></div>
											</div>
											<div class="resumen-user">
												<div class="resumen-item" data-popup="tooltip" data-placement="left" data-original-title="Usuario y Fecha de Cierre"><span class="resumen-ico"><i class="text-danger"></i></span><span><%# Eval("UsuarioCierre") %>-<%# Eval("FechaCierre", "{0:dd/MM/yyyy}") == "01/01/1900" ? "": Eval("FechaCierre") %></span></div>
											</div>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Contado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("Contado", "{0:N}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Calculado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("Calculado", "{0:N}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("Diferencia", "{0:N}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Retiro" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("Retiro", "{0:N}") %>
										</ItemTemplate>
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
											<%# Eval("NombreEstado") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<div style="min-width: 75px;">
												<ul class="icons-list">
													<li title="Eliminar Caja">
														<asp:LinkButton ID="lnkEliminarCaja" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="EliminarCaja" CommandArgument='<%# Eval("IDCaja").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar esta caja?\",\"{0}\"); return false;", Eval("CajaMecanica"))%>'><i class="icon-trash"></i></asp:LinkButton>
													</li>
													<li title="Cerrar Caja">
														<asp:LinkButton ID="lnkCerrarCaja" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="CerrarCaja" CommandArgument='<%# Eval("IDCaja").ToString() + ";" + Eval("CajaMecanica").ToString() + ";" + Eval("UsuarioApertura").ToString() %>'><i class="icon-lock"></i></asp:LinkButton>
													</li>
													<li title="Ver Movimientos" style="width: 35px;">
														<asp:LinkButton ID="lnkVerMovimientos" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="VerMovimiento" CommandArgument='<%# Eval("IDCaja").ToString() %>'><span class="icon-file-text2"></span></asp:LinkButton>
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

	<div id="ModalAperturarCaja" class="modal fade" role="dialog">
		<div class="modal-dialog modal-sm">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Aperturar Caja</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upAperturarCaja" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnAperturarCaja" runat="server" DefaultButton="btnGuardarAperturarCaja">
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDCaja" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Caja:</label>
											<asp:DropDownList ID="ddlIDCajaMecanica" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha Apertura:</label>
											<asp:TextBox ID="txtFechaApertura" SkinID="ui-textbox-fecha-simple-requerido" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Monto Apertura:</label>
											<asp:TextBox ID="txtMontoApertura" SkinID="ui-textbox-price-requerido" runat="server" MaxLength="11" Text="0.00"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarAperturarCaja" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="CerrarModal('ModalAperturarCaja')" />
								<asp:Button ID="btnGuardarAperturarCaja" runat="server" Text="Guardar" OnClick="btnGuardarAperturarCaja_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="ModalCorteCaja" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Corte de Caja</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upCorteCajaRegistrar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnCorteCajaRegistrar" runat="server" DefaultButton="btnGuardarCorteCaja">
							<div class="modal-body">
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Caja:</label>
											<asp:TextBox ID="txtCaja" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-8">
										<div class="form-group">
											<label>Cajero:</label>
											<asp:TextBox ID="txtCajero" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<hr />
								<div class="row">
									<div class="col-md-12">
										<div class="table-responsive" style="margin-bottom: 0;">
											<asp:GridView ID="gvCorteCajaPreListar" runat="server" ShowFooter="true" DataKeyNames="IDCorteCaja" ShowHeader="true" ShowHeaderWhenEmpty="true" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Ítem" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="MedioPago" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("MedioPago") %>
															<asp:HiddenField ID="hdfIDMedioPago" runat="server" Value='<%# Eval("IDMedioPago") %>' />
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>Totales:</b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Contado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:TextBox ID="txtContado" runat="server" AutoPostBack="true" SkinID="ui-textbox-price-requerido" OnTextChanged="txtContado_TextChanged" Text='<%# Eval("Contado", "{0:N}") %>'></asp:TextBox>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTPCJContado" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Calculado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:TextBox ID="txtCalculado" runat="server" Enabled="false" AutoPostBack="true" SkinID="ui-textbox-price-requerido" OnTextChanged="txtCalculado_TextChanged" Text='<%# Eval("Calculado", "{0:N}") %>'></asp:TextBox>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTPCJCalculado" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:TextBox ID="txtDiferencia" runat="server" Enabled="false" AutoPostBack="true" SkinID="ui-textbox-price-requerido" OnTextChanged="txtDiferencia_TextChanged" Text='<%# Eval("Diferencia", "{0:N}") %>'></asp:TextBox>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTPCJDiferencia" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Retiro" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:TextBox ID="txtRetiro" runat="server" SkinID="ui-textbox-price-requerido" Text='<%# Eval("Retiro", "{0:N}") %>'></asp:TextBox>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTPCJRetiro" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
											<asp:GridView ID="gvCorteCajaListar" runat="server" Visible="false" ShowFooter="true" DataKeyNames="IDCorteCaja" AllowPaging="false" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Ítem" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
														<ItemTemplate>
															<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="MedioPago" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("MedioPago") %>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>Totales:</b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Contado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblCJContado" runat="server" Text='<%# Eval("Contado", "{0:N}") %>'></asp:Label>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTTCJContado" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Calculado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblCJCalculado" runat="server" Text='<%# Eval("Calculado", "{0:N}") %>'></asp:Label>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTTCJCalculado" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblCJDiferencia" runat="server" Text='<%# Eval("Diferencia", "{0:N}") %>'></asp:Label>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTTCJDiferencia" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Retiro" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblCJRetiro" runat="server" Text='<%# Eval("Retiro", "{0:N}") %>'></asp:Label>
														</ItemTemplate>
														<FooterTemplate>
															<div style="min-width: 100%; text-align: right; font-weight: 700; font-size: 16px;">
																<b>
																	<asp:Label ID="lblTTCJRetiro" runat="server" Text=''></asp:Label></b>
															</div>
														</FooterTemplate>
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCerrarCorteCaja" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="CerrarModal('ModalCorteCaja')" />
								<asp:Button ID="btnGuardarCorteCaja" runat="server" Text="Guardar" OnClick="btnGuardarCorteCaja_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>


	<div id="ModalMovimientoCaja" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Movimiento de Caja</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upMovimientoCajaListar" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnMovimientoCajaListar" runat="server">
							<div class="modal-body"> 
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo Movimiento:</label>
											<asp:DropDownList ID="ddlBTipoMovimiento" runat="server">
												<asp:ListItem Value="0">-TODOS-</asp:ListItem>
												<asp:ListItem Value="I">Ingreso</asp:ListItem>
												<asp:ListItem Value="S">Salida</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Forma Pago:</label>
											<asp:DropDownList ID="ddlBIDFormaPago" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Filtro:<b>Serie-Numero</b></label>
											<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Inicio:</label>
											<asp:TextBox ID="txtBMFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Fin:</label>
											<asp:TextBox ID="txtBMFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnBuscarMovimiento" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscarMovimiento_Click" /> 
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="table-responsive" style="margin-bottom: 0;">
											<asp:GridView ID="gvMovimientoCajaListar" runat="server" DataKeyNames="IDMovimientoCaja" OnPageIndexChanging="gvMovimientoCajaListar_PageIndexChanging" AllowPaging="false" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Caja" ItemStyle-Width="8%">
														<ItemTemplate>
															<%# Eval("CajaMecanica") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Mov." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
														<ItemTemplate>
															<%# Eval("FechaMovimiento", "{0:dd/MM/yyyy hh:mm:ss}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Tipo Mov." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<ItemTemplate>
															<%# Eval("NombreTipoMovimiento") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Operación" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
														<ItemTemplate>
															<%# Eval("Operacion") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="DOC." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
														<ItemTemplate>
															<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title='<%# Eval("TipoComprobante") %>'><%# Eval("SiglaTipoComprobante") %></span>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Serie-Numero" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("SerieNumero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="FP" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
														<ItemTemplate>
															<%# Eval("MedioPago") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<div class="resumen">
																<div class="resumen-item" data-popup="tooltip" data-placement="left" data-original-title='<%# Eval("NombreTipoMovimiento") %>'><span class="resumen-ico"><i class='<%# (Eval("TipoMovimiento").ToString() == "I") ? "icon-arrow-up7 text-info":"icon-arrow-down7 text-danger" %>'></i></span><span><%# Eval("Monto") %></span></div>
															</div>
														</ItemTemplate>
													</asp:TemplateField>
												</Columns>
											</asp:GridView>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-6"></div>
									<div class="col-md-2">
										<div class="form-group has-feedback" style="color: #2700ff;">
											<label><b>Total Ingresos:</b></label>
											<asp:TextBox ID="txtTotalIngresos" runat="server" Text="0.00" SkinID="ui-textbox-price" Enabled="false"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-stairs-up"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback" style="color: red;">
											<label><b>Total Egresos:</b></label>
											<asp:TextBox ID="txtTotalEgresos" runat="server" Text="0.00" SkinID="ui-textbox-price" Enabled="false"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-stairs-down"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback" style="color: #25b800;">
											<label><b>Saldo:</b></label>
											<asp:TextBox ID="txtSaldo" runat="server" Text="0.00" SkinID="ui-textbox-price" Enabled="false"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-coins"></i>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCerrarMovimientoCaja" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="CerrarModal('ModalMovimientoCaja')" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

</asp:Content>
