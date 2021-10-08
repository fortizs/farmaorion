<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="Farmacia.Seguridad.Rol" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.ocultar {
			display: none;
		}

		.list-item {
			position: relative;
			padding-top: 3px;
			padding-bottom: 3px;
			padding-left: 30px;
			margin-left: 30px;
		}

		label {
			display: inline-block;
			margin-bottom: -0.5rem;
			color: #898989;
			font-size: 13px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Roles</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hfIDRol" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label>Empresa:</label>
								<asp:DropDownList ID="ddlBEmpresa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBEmpresa_SelectedIndexChanged"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label>Perfil:</label>
								<asp:DropDownList ID="ddlBPerfil" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBPerfil_SelectedIndexChanged"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label class="etiqueta">Filtro:<b>[Nombre]</b></label>
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
						<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDRol" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvLista_RowCommand">
							<Columns>
								<asp:BoundField DataField="IDRol" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<HeaderStyle HorizontalAlign="Center" />
									<ItemStyle HorizontalAlign="Center" Width="2%" />
								</asp:BoundField>
								<asp:BoundField DataField="Perfil" HeaderText="Perfil" />
								<asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
									<HeaderStyle HorizontalAlign="Center" />
								</asp:BoundField>
								<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<HeaderStyle HorizontalAlign="Center" />
									<ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:CheckBoxField>
								<asp:TemplateField HeaderText="Auditoría" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%">
									<ItemTemplate>
										<%# Eval("UsuarioModificacion") %><br />
										<b>(<%# Eval("FechaModificacion") %>)</b>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
									<ItemTemplate>
										<div style="width: 110px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-default" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px">
													<asp:LinkButton ID="lbEliminar" SkinID="ui-link-boton-danger" runat="server" CausesValidation="False" CommandName="Eliminar" ToolTip="Eliminar Rol" CommandArgument='<%# Eval("IDRol") %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><span class="icon-trash"></span></asp:LinkButton>
												</li>
												<li style="width: 35px">
													<asp:LinkButton ID="lbConfigPermiso" SkinID="ui-link-boton-success" runat="server" CausesValidation="False" CommandName="ConfigPermiso" ToolTip="Configurar Permisos" CommandArgument='<%# Eval("IDRol") %>'><span class="icon-cog52"></span></asp:LinkButton>
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
	</div>

	<div id="DatosRol" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Rol</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>ID:</label>
											<asp:TextBox ID="txtIDRol" runat="server" Enabled="False"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-5">
										<div class="form-group">
											<label>Empresa:</label>
											<asp:DropDownList ID="ddlEmpresa" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-5">
										<div class="form-group">
											<label>Perfil:</label>
											<asp:DropDownList ID="ddlPerfil" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-10">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" runat="server" SkinID="ui-textbox-requerido" MaxLength="50"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:CheckBox ID="chkEstado" runat="server" Text="Activo" />
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cerrar" SkinID="ui-boton-default" OnClientClick="CerrarModal('DatosRol')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
							</div>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>
				</asp:Panel>
			</div>
		</div>
	</div>

	<div id="configPermiso" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Configuración de Permisos </h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upPermisoModulo" runat="server" UpdateMode="Conditional">
						<ContentTemplate>

							<div class="row">
								<div class="col-md-12">
									<strong>ROL:
                                        <asp:Label ID="lblRol" runat="server" Text=""></asp:Label>
									</strong>
								</div>
							</div>
							<div class="espacio"></div>
							<div class="separador"></div>
							<div class="espacio"></div>
							<div class="row">
								<div class="col-md-12">
									<div class="form-group">
										<label class="control-label col-lg-2">Módulo:</label>
										<div class="col-lg-10">
											<asp:DropDownList ID="ddlModulo" runat="server" OnSelectedIndexChanged="ddlModulo_SelectedIndexChanged" AutoPostBack="True">
											</asp:DropDownList>
										</div>
									</div>
								</div>
							</div>
							<div class="espacio"></div>
							<div class="table-responsive">
								<asp:GridView ID="gvRolMenu" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None" ClientIDMode="Static" OnSelectedIndexChanged="gvRolMenu_SelectedIndexChanged" OnRowDataBound="gvRolMenu_RowDataBound" AllowPaging="False">
									<Columns>
										<asp:TemplateField HeaderText="IDRol" Visible="False">
											<ItemTemplate>
												<asp:Label ID="lblIDRol" runat="server" Text='<%# Bind("IDRol") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="IDMenu" Visible="False">
											<ItemTemplate>
												<asp:Label ID="lblIDMenu" runat="server" Text='<%# Bind("IDMenu") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Menú">
											<ItemTemplate>
												<asp:Label ID="Label3" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
												<asp:Panel ID="Panel1" runat="server" CssClass='<%# String.Format("alert alpha-grey-300 alert-security border-grey alert-styled-left ocultar operacion ope_{0}", Eval("IDMenu")) %>'>
													<asp:CheckBoxList ID="cblOperacion" runat="server" RepeatLayout="Flow">
														<asp:ListItem>Imprimir</asp:ListItem>
														<asp:ListItem>Exportar</asp:ListItem>
														<asp:ListItem>Ver Imágenes</asp:ListItem>
														<asp:ListItem>Anular</asp:ListItem>
														<asp:ListItem>Anular2</asp:ListItem>
														<asp:ListItem>Anular2</asp:ListItem>
													</asp:CheckBoxList>
												</asp:Panel>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Ope." ShowHeader="False">
											<ItemTemplate>
												<asp:LinkButton ID="lbConfigOpe" Visible='<%# Bind("ConfigOperacion") %>' OnClientClick='<%# String.Format("verOperacion({0});return false;", Eval("IDMenu")) %>' CssClass="btn btn-default btn-lg" runat="server" CausesValidation="False" ToolTip="Operaciones"><span class="icon-cog52"></span></asp:LinkButton>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="2%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Permiso">
											<HeaderTemplate>
												Asignar
                                                        <input id="chkRolMenuSel" name="chkRolMenuSel" type="checkbox" onclick="SeleccionarCheckBox(this, 'chkRolMenu', 'gvRolMenu');" />
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="chkRolMenu" runat="server" Checked='<%# Bind("Estado") %>' />
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="100px" />
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
							<div class="modal-footer" style="padding: 0px; padding-top: 5px;">
								<asp:Button ID="btnCerrarRolMenu" runat="server" Text="Cerrar" SkinID="ui-boton-default" OnClientClick="CerrarModal('configPermiso')" />
								<asp:Button ID="btnGrabarRolMenu" runat="server" Text="Guardar" TabIndex="0" OnClick="btnGrabarRolMenu_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<script type="text/javascript">

		function WebForm_OnSubmit() {
			if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
				Mensaje('warning', $(".ValidationSummary").html());
				return false;
			}
			return true;
		}

		function verOperacion(id) {
			$(".ope_" + id).toggle("slow");;
		}

		function SeleccionarCheckBox(CheckBoxAll, pCheckBoxRow, gvID) {
			var TargetBaseControl = document.getElementById(gvID);
			var TargetChildControl = pCheckBoxRow;
			var Inputs = TargetBaseControl.getElementsByTagName("input");
			for (var iCount = 0; iCount < Inputs.length; ++iCount) {
				if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
					Inputs[iCount].checked = CheckBoxAll.checked;
			}
		}
	</script>

</asp:Content>
