<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovimientoCaja.aspx.cs" Inherits="Farmacia.CajaBanco.MovimientoCaja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtTotalIngresos {
			font-size: 17px;
			font-weight: 700;
			color: blue;
		}

		#cphPrincipal_txtTotalEgresos {
			font-size: 17px;
			font-weight: 700;
			color: red;
		}

		#cphPrincipal_txtSaldo {
			font-size: 17px;
			font-weight: 700;
			color: green;
		}
	</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Movimiento de Caja</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<asp:HiddenField ID="hdfIDMovimientoCaja" runat="server" Value="0" />
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
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
									<asp:Button ID="btnNuevoMovimientoCaja" runat="server" Text="Nuevo" OnClick="btnNuevoMovimientoCaja_Click" />
								</div>
							</div>
						</div>
						<div class="table-responsive">
							<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDMovimientoCaja" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
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
									<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
										<ItemTemplate>
											<div style="min-width: 75px;">
												<ul class="icons-list">
													<li title="Editar Mov.Caja">
														<asp:LinkButton ID="lbEditarMovCaja" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="EditarMovCaja" CommandArgument='<%# Eval("IDMovimientoCaja").ToString() %>'><i class="icon-pencil7"></i></asp:LinkButton>
													</li>
													<li title="Eliminar Mov.Caja" style='<%# (Int32.Parse(Eval("IDOperacion").ToString()) == 1) ? "display:none;": "width: 35px;" %>'>
														<asp:LinkButton ID="lnkEliminarMovCaja" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="EliminarMovCaja" CommandArgument='<%# Eval("IDMovimientoCaja").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este Movimiento?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
													</li>
												</ul>
											</div>
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
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
					</asp:Panel>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="gvLista" EventName="PageIndexChanging" />
				</Triggers>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="ModalMovimientoCaja" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Movimiento Caja</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upMovimientoCaja" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="pnMovimientoCaja" runat="server" DefaultButton="btnGuardarMovimientoCaja">
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
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Tipo Movimiento:</label>
											<asp:DropDownList ID="ddlTipoMovimiento" runat="server" SkinID="ui-dropdownlist-requerido" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoMovimiento_SelectedIndexChanged">
												<asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
												<asp:ListItem Value="I">Ingreso</asp:ListItem>
												<asp:ListItem Value="S">Salida</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-8">
										<div class="form-group">
											<label>Operación:</label>
											<asp:DropDownList ID="ddlIDOperacion" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Fecha Mov.:</label>
											<asp:TextBox ID="txtFechaMovimiento" SkinID="ui-textbox-fecha-simple-requerido" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Tipo Comprobante:</label>
											<asp:DropDownList ID="ddlIDTipoComprobante" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Serie:</label>
											<asp:TextBox ID="txtSerie" runat="server" MaxLength="4"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Número:</label>
											<asp:TextBox ID="txtNumero" runat="server" MaxLength="8"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Moneda:</label>
											<asp:DropDownList ID="ddlIDMoneda" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Forma Pago:</label>
											<asp:DropDownList ID="ddlIDFormaPago" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Monto:</label>
											<asp:TextBox ID="txtMonto" runat="server" SkinID="ui-textbox-price-requerido" MaxLength="12"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Observación:</label>
											<asp:TextBox ID="txtObservacion" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarMovimientoCaja" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="CerrarModal('ModalMovimientoCaja')" />
								<asp:Button ID="btnGuardarMovimientoCaja" runat="server" Text="Guardar" OnClick="btnGuardarMovimientoCaja_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>
</asp:Content>
