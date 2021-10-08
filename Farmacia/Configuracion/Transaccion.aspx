<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaccion.aspx.cs" Inherits="Farmacia.Configuracion.Transaccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Transacciones</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDTransaccion" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label>Tipo Movimiento</label>
								<asp:DropDownList ID="ddlBTipoMovimiento" runat="server">
									<asp:ListItem Value="0">-Todos-</asp:ListItem>
									<asp:ListItem Value="I">Ingresos</asp:ListItem>
									<asp:ListItem Value="S">Salidas</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese criterio" MaxLength="50"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
								<asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
							</div>
						</div>
					</div>
					<div class="table-responsive">
						<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" DataKeyNames="IDTransaccion" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("IDTransaccion") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("Codigo") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Tipo Movimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("TipoMovimientoNombre") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nombre" ItemStyle-Width="40%">
									<ItemTemplate>
										<%# Eval("Nombre") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 75px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px" title="Eliminar">
													<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDTransaccion").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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
	</div>

	<div id="ModalTransaccion" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos de la Transacción</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
							<div class="modal-body">

								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Codigo:</label>
											<asp:TextBox ID="txtCodigo" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Tipo Movimiento</label>
											<asp:DropDownList ID="ddlTipoMovimiento" runat="server" SkinID="ui-dropdownlist-requerido">
												<asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
												<asp:ListItem Value="I">Ingresos</asp:ListItem>
												<asp:ListItem Value="S">Salidas</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-12">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalTransaccion')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>
</asp:Content>
