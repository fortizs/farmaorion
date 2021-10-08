<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="Farmacia.Cobranza.Cobranza" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtNumeroPlanillaCobranza, #cphPrincipal_txtFechaCobranza {
			font-weight: bold;
			font-size: 14px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Cobranza de Clientes</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Cliente:</label>
									<asp:DropDownList ID="ddlBIDCliente" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3">
								<div class="form-group">
									<label>Filtro:<b>[Serie-Numero|NumeroDocumento]</b></label>
									<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Estado:</label>
									<asp:DropDownList ID="ddlBIDEstadoCobranza" runat="server">
										<asp:ListItem Value="0">-Todos-</asp:ListItem>
									</asp:DropDownList>
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
									<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
								</div>
							</div>
						</div>
						<div class="table-responsive">
							<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDVenta" Width="100%" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand">
								<Columns>
									<asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
											<%# Eval("TipoComprobanteCodigoSunat") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Serie-Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("SerieNumero") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("FechaVenta", "{0:dd/MM/yyyy}") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
										<ItemTemplate>
											<b>Nro.Doc:</b><%# Eval("NumeroDocumentoCliente") %><br />
											<b>Cliente:</b><%# Eval("Cliente") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemTemplate>
											<%# Eval("IDMoneda") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
										<ItemTemplate>
											<%# Eval("TotalVenta") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("EstadoCobranza") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Acciones" ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<div style="min-width: 75px;">
												<ul class="icons-list">
													<li class="text-primary" title="Aplicar Cobro">
														<asp:LinkButton ID="lnkAplicarCobro" runat="server" SkinID="ui-link-boton-primario" CommandName="Cobranza" CausesValidation="False" CommandArgument='<%# Eval("IDVenta").ToString() %>'><span class="icon-coin-dollar"></span></asp:LinkButton>
													</li>
												</ul>
											</div>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
					</asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>


	<div id="ModalCobranzaListar" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Lista de Cobranza </h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upCobranzaRegistro" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnCobranza" runat="server">
								<asp:HiddenField ID="hdfIDPlanillaCobranza" runat="server" Value="0" />
								<asp:HiddenField ID="hdfIDVenta" runat="server" Value="0" />

								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Cliente:</label>
											<asp:TextBox ID="txtCliente" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Serie-Numero:</label>
											<asp:TextBox ID="txtSerieNumero" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha Venta:</label>
											<asp:TextBox ID="txtFechaVenta" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnNuevaCobranza" runat="server" SkinID="ui-boton-default" OnClick="btnNuevaCobranza_Click" Text="Nuevo" />
										</div>
									</div>
								</div>
								<div class="separador"></div>
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="table-responsive">
											<asp:GridView ID="gvCobranzaListar" runat="server" DataKeyNames="IDCobranza" AllowSorting="True" EnableSortingAndPagingCallbacks="True" OnRowCommand="gvCobranzaListar_RowCommand" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Nro.Cobranza" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("NumeroCobranzaFormato") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Medio Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("MedioPago") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Fecha Cobro" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
														<ItemTemplate>
															<%# Eval("FechaCobro", "{0:dd/MM/yyyy}") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Monto Cobrado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("MontoCobrado") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
														<ItemTemplate>
															<%# Eval("Observacion") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%">
														<ItemTemplate>
															<div style="min-width: 50px;">
																<ul class="icons-list">
																	<li class="text-primary" data-popup="tooltip" title="Eliminar Cobranza">
																		<asp:LinkButton ID="lnkAnular" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Anular" CommandArgument='<%# Eval("IDCobranza").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas anular esta cobranza?\",\"{0}\"); return false;", "")%>'><i class="icon-bin"></i></asp:LinkButton>
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
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<div id="ModalCobranza" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-slate-300">
					<h6 class="modal-title">Registro de Cobranza </h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnFormulario" runat="server">
							<asp:HiddenField ID="hdfIDCobranza" runat="server" Value="0" />
							<div class="modal-body">
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Medio de Pago:</label>
											<asp:DropDownList ID="ddlIDMedioPago" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha de Cobranza:</label>
											<asp:TextBox ID="txtFechaCobranza" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Total Pago:</label>
											<asp:TextBox ID="txtTotalPago" SkinID="ui-textbox-price-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-12">
										<div class="form-group">
											<label>Observación:</label>
											<asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:LinkButton ID="lnkCancelarCobranza" runat="server" SkinID="ui-link-boton-default" OnClientClick="CerrarModal('ModalCobranza')"><i class="icon-undo"></i> Cancelar </asp:LinkButton>
								<asp:LinkButton ID="lnkGuardarCobranza" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarCobranza_Click"><i class="icon-floppy-disk"></i> Guardar </asp:LinkButton>
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>

			</div>
		</div>
	</div>

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

	<script type="text/javascript">
		function ConfigJS() {
			$("#<%= ddlBIDCliente.ClientID %>").select2({
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

		function AbrirModalPlanillaCobranza() {
			$('#DatosPlanillaCobranza').modal('show');
		}

		function CerrarModalPlanillaCobranza() {
			$('#DatosPlanillaCobranza').modal('hide');
		}

		function AbrirModalVentaPendienteCobro() {
			$('#ModalVentaPendienteCobro').modal('show');
		}

		function CerrarModalVentaPendienteCobro() {
			$('#ModalVentaPendienteCobro').modal('hide');
		}


	</script>
</asp:Content>
