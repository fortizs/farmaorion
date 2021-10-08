<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Proveedor.aspx.cs" Inherits="Farmacia.Configuracion.Proveedor" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Proveedores</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDProveedor" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label>Filtro: <b>[Nombres]</b></label>
								<asp:TextBox ID="txtBuscar" runat="server" MaxLength="50"></asp:TextBox>
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
						<asp:GridView ID="gvLista" DataKeyNames="IDProveedor" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="IDProveedor" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
									<ItemTemplate>
										<%# Eval("IDProveedor") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("TipoDocumento") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Número Documento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("NumeroDocumento") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Razón Social" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
									<ItemTemplate>
										<%# Eval("RazonSocial") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Celular" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("Celular") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="NroCategoria" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("NroCategoria") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="15%">
									<ItemTemplate>
										<div style="min-width: 100px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px">
													<asp:LinkButton ID="lnkCategoria" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Categoria" CommandArgument='<%# Eval("IDProveedor").ToString() %>' ToolTip="Asignar Categorias"><span class="icon-stack-check"></span></asp:LinkButton>
												</li>
												<li style="width: 35px" title="Eliminar">
													<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDProveedor").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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

	<div id="DatosProveedor" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Proveedor</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">

								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo de Documento:</label>
											<asp:DropDownList ID="ddlIDTipoDocumento" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Numero de documento:</label>
											<asp:TextBox ID="txtNumeroDocumento" MaxLength="11" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Razón Social:</label>
											<asp:TextBox ID="txtRazonSocial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Nombre Comercial:</label>
											<asp:TextBox ID="txtNombreComercial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
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
											<asp:TextBox ID="txtUrbanizacion" runat="server"></asp:TextBox>
										</div>
									</div>

								</div>

								<div class="row">
									<div class="col-md-5">
										<div class="form-group">
											<label>Direccion:</label>
											<asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-5">
										<div class="form-group">
											<label>Correo:</label>
											<asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>

							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosProveedor')" />
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

	<div id="DatosProveedorCategoria" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos Proveedor - Categoria</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="pnProveedorCategoria" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upProveedorCategoria" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDProveedorCategoria" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Categoria:</label>
											<asp:DropDownList ID="ddlIDCategoria" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnAgregarProveedorCategoria" runat="server" Text="Agregar" OnClick="btnAgregarProveedorCategoria_Click" />
										</div>
									</div>
								</div>
								<div class="table-responsive">
									<asp:GridView ID="gvProveedorCategoriaListar" DataKeyNames="IDProveedorCategoria" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProveedorCategoriaListar_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
										<Columns>
											<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%">
												<ItemTemplate>
													<%# Eval("Categoria") %>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCerrarProveedorCategoria" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosProveedorCategoria')" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>


	<script type="text/javascript">

		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_hdfRegIDUbigeo', 'cphPrincipal_txtRegUbigeo');
		}
		 
	</script>

</asp:Content>
