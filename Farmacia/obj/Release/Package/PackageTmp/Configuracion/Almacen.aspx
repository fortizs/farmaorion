<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Almacen.aspx.cs" Inherits="Farmacia.Configuracion.Almacen" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Almacenes</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="row">
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
						<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" DataKeyNames="IDAlmacen" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("IDAlmacen") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("Sucursal") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("Codigo") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nombre" ItemStyle-Width="30%">
									<ItemTemplate>
										<%# Eval("Nombre") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nro.Movimientos" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
									<ItemTemplate>
										<span class="badge outline-badge-info"><b>Nro.Nota Ingreso: <%# Eval("NumeroNotaIngreso") %></b></span>
										<span class="badge outline-badge-success"><b>Nro.Nota Salida: <%# Eval("NumeroNotaSalida") %></b></span>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="12%">
									<ItemTemplate>
										<div style="width: 75px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px" title="Eliminar">
													<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDAlmacen").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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

	<div id="ModalAlmacen" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Almacen</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDAlmacen" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Codigo:</label>
											<asp:TextBox ID="txtCodigo" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlIDSucursal" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-7">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label for="tags">Ubigeo:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfIDUbigeo" runat="server" Value="0" />
												<asp:TextBox ID="txtUbigeo" Enabled="false" runat="server"></asp:TextBox>
												<span class="input-group-btn">
													<button class="btn btn-primary" type="button" onclick="BuscarUbigeo();"><i class="icon-search4"></i></button>
												</span>
											</div>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Dirección:</label>
											<asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Teléfono:</label>
											<asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Nro.Nota Ingreso:</label>
											<asp:TextBox ID="txtNumeroNotaIngreso" Enabled="false" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Nro.Nota Salida:</label>
											<asp:TextBox ID="txtNumeroNotaSalida" Enabled="false" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalAlmacen')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
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

	<script type="text/javascript">

		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_hdfIDUbigeo', 'cphPrincipal_txtUbigeo');
		}

	</script>
</asp:Content>
