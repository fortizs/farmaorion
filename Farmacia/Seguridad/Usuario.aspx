<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Farmacia.Seguridad.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Usuarios</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Sucursal:</label>
									<asp:DropDownList ID="ddlBIDSucursal" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-5">
								<div class="form-group">
									<label>Criterio:<b>[Usuario|Número Documento|Nombres]</b></label>
									<asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese criterio" MaxLength="50"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" data-loading-text="<i class='icon-spinner4 spinner'></i>" />
									<asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
								</div>
							</div>
						</div>
					</asp:Panel>
					<div class="espacio"></div>
					<div class="table-responsive">
						<asp:GridView ID="gvUsuarios" runat="server" OnPageIndexChanging="gvUsuarios_PageIndexChanging" OnRowCommand="gvUsuarios_RowCommand">
							<Columns>
								<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<div style="width: 40px;">
											<asp:Label ID="lblIDUsuario" runat="server" Text='<%# Eval("IDUsuario") %>'></asp:Label>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="IDColaborador" Visible="false">
									<ItemTemplate>
										<asp:Label ID="lblIDColaborador" runat="server" Text='<%# Eval("IDColaborador") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="15%">
									<ItemTemplate>
										<asp:Label ID="lblSucursal" runat="server" Text='<%# Eval("Sucursal") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 115px;">
											<asp:Label ID="lblUsuario" runat="server" Text='<%# Eval("Usuario") %>'></asp:Label>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nro. Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 85px;">
											<asp:Label ID="lblNumeroDocumento" runat="server" Text='<%# Eval("NumeroDocumento") %>'></asp:Label>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nombre Completo" ItemStyle-Width="35%">
									<ItemTemplate>
										<asp:Label ID="lblNombreCompleto" runat="server" Text='<%# Eval("NombreCompleto") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Auditoría" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%">
									<ItemTemplate>
										<%# Eval("UsuarioModificacion") %><br />
										<b>(<%# Eval("FechaModificacion") %>)</b>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 95px;">
											<ul class="icons-list">
												<li style="width: 30px">
													<asp:LinkButton ID="lnkEditar" SkinID="ui-link-boton-primario" runat="server" CommandName="Seleccionar" CommandArgument='<%# Eval("IDUsuario").ToString() + "," + Eval("IDColaborador").ToString() %>'><i class="icon-pencil7"></i></asp:LinkButton>
												</li>
												<li style="width: 30px">
													<asp:LinkButton ID="lnkReiniciar" SkinID="ui-link-boton-primario" runat="server" CommandName="ReiniciarClave" CommandArgument='<%# Eval("IDUsuario") %>'><i class="icon-key"></i></asp:LinkButton>
												</li>
												<li style="width: 30px">
													<asp:LinkButton ID="lnkEliminar" SkinID="ui-link-boton-primario" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IDUsuario").ToString() + "," + Eval("IDColaborador").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
												</li>
											</ul>
										</div>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="RegUsuCol" class="modal fade" tabindex="-1" role="dialog">
		<asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Panel ID="pnRegUsuCol" runat="server" DefaultButton="btnGuardar">
					<asp:HiddenField ID="hfIDUsuario" runat="server" Value="0" />
					<asp:HiddenField ID="hfIDColaborador" runat="server" Value="0" />
					<div class="modal-dialog modal-lg">
						<div class="modal-content">
							<div class="modal-header bg-hmodal">
								<h6 class="modal-title">Datos del Usuario</h6>
								<button type="button" class="close" data-dismiss="modal">×</button>
							</div>
							<div class="modal-body">
								<div class="tab-pane has-padding active" id="tabUsuario">
									<asp:UpdatePanel ID="upTabUsuario" runat="server" UpdateMode="Conditional">
										<ContentTemplate>
											<div class="row">
												<div class="col-md-12 text-right">
													<div class="form-group">
														<label>Estado:<input runat="server" id="chkUsuEstado" type="checkbox" data-on-color="success" data-off-color="danger" data-on-text="Activo" data-off-text="Inactivo" checked="checked"></label>
														<label>Bloqueado:<input runat="server" id="chkUsuBloqueado" type="checkbox" data-on-color="danger" data-off-color="success" data-on-text="Si" data-off-text="No"></label>
														<label>Reiniciar Clave:<input runat="server" id="chkUsuReiniciarClave" type="checkbox" data-on-color="success" data-off-color="danger" data-on-text="Si" data-off-text="No"></label>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-4">
													<div class="form-group">
														<label>Empresa:</label>
														<asp:DropDownList ID="ddlColEmpresa" runat="server" SkinID="ui-dropdownlist-requerido" AutoPostBack="true" OnSelectedIndexChanged="ddlColEmpresa_SelectedIndexChanged"></asp:DropDownList>
													</div>
												</div>
												<div class="col-md-4">
													<div class="form-group">
														<label>Sucursal:</label>
														<asp:DropDownList ID="ddlColSucursal" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
													</div>
												</div>
												<div class="col-md-4">
													<div class="form-group">
														<label>Cargo:</label>
														<asp:DropDownList ID="ddlColCargo" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
													</div>
												</div>
												<div class="col-md-3">
													<div class="form-group">
														<label>Nro. Documento:</label>
														<asp:TextBox ID="txtUsuNroDocumento" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-3">
													<div class="form-group">
														<label>Nombres:</label>
														<asp:TextBox ID="txtUsuNombres" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-3">
													<div class="form-group">
														<label>Apellido Paterno:</label>
														<asp:TextBox ID="txtUsuApellidoPaterno" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-3">
													<div class="form-group">
														<label>Apellido Materno:</label>
														<asp:TextBox ID="txtUsuApellidoMaterno" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-3">
													<div class="form-group">
														<label>Usuario:</label>
														<asp:TextBox ID="txtUsuCodigo" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
													</div>
												</div>
												<div class="col-md-6">
													<div class="form-group">
														<label>E-mail:</label>
														<asp:TextBox ID="txtUsuEmail" runat="server"></asp:TextBox>
													</div>
												</div>
											</div>
											<div class="espacio"></div>
											<div class="row">
												<div class="col-md-6">
													<div style="padding: 2px;">
														<div style="overflow-y: auto; height: 169px; width: 100%; border: 1px solid #e2e2e2;">
															<div class="table-responsive" style="margin: 0;">
																<asp:GridView ID="gvRolesDisponibles" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" AllowPaging="false" Style="font-size: 11px;">
																	<Columns>
																		<asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																			<HeaderTemplate>
																				<asp:CheckBox ID="chkRDTodos" runat="server" ClientIDMode="Static" ToolTip="Seleccionar/Deseleccionar Todos" />
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox ID="chkRDSel" runat="server" ClientIDMode="Static" />
																				<asp:Label ID="lblIndex" runat="server" Text='<%# ((GridViewRow) Container).RowIndex %>' Style="display: none;"></asp:Label>
																				<asp:Label ID="lblIDRol" runat="server" Visible="false" Text='<%# Eval("IDRol") %>' Style="display: none;"></asp:Label>
																			</ItemTemplate>
																		</asp:TemplateField>
																		<asp:TemplateField HeaderText="Roles Disponibles" ItemStyle-Width="60%">
																			<ItemTemplate>
																				<b>
																					<asp:Label ID="lblRol" runat="server" Text='<%# Eval("Rol") %>' ToolTip='<%# String.Format("ID: {0}", Eval("IDRol")) %>'></asp:Label></b><br />
																				Perfil:<asp:Label ID="lblPerfil" runat="server" Text='<%# Eval("Perfil") %>'></asp:Label>
																			</ItemTemplate>
																		</asp:TemplateField>
																	</Columns>
																	<EmptyDataTemplate>
																		No se han encontrado roles disponibles. Verifique si ya están asignados.
																	</EmptyDataTemplate>
																</asp:GridView>
															</div>
														</div>
														<div class="espacio"></div>
														<br />
														<asp:Button ID="btnRolAgregar" runat="server" SkinID="ui-boton-default" Enabled="false" Text="Agregar" OnClick="btnRolAgregar_Click" />
													</div>
												</div>
												<div class="col-md-6">
													<div style="padding: 2px;">
														<div style="overflow-y: auto; height: 169px; width: 100%; border: 1px solid #e2e2e2;">
															<div class="table-responsive" style="margin: 0;">
																<asp:GridView ID="gvRolesAsignados" runat="server" ShowHeader="true" ShowHeaderWhenEmpty="true" AllowPaging="false" Style="font-size: 11px;">
																	<Columns>
																		<asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
																			<HeaderTemplate>
																				<asp:CheckBox ID="chkRATodos" runat="server" ClientIDMode="Static" ToolTip="Seleccionar/Deseleccionar Todos" />
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox ID="chkRASel" runat="server" ClientIDMode="Static" />
																				<asp:Label ID="lblIndex" runat="server" Text='<%# ((GridViewRow) Container).RowIndex %>' Style="display: none;"></asp:Label>
																				<asp:Label ID="lblIDRol" runat="server" Visible="false" Text='<%# Eval("IDRol") %>' Style="display: none;"></asp:Label>
																			</ItemTemplate>
																		</asp:TemplateField>
																		<asp:TemplateField HeaderText="Roles Asignados" ItemStyle-Width="60%">
																			<ItemTemplate>
																				<b>
																					<asp:Label ID="lblRol" runat="server" Text='<%# Eval("Rol") %>' ToolTip='<%# String.Format("ID: {0}", Eval("IDRol")) %>'></asp:Label>
																				</b>
																				<br />
																				Perfil:<asp:Label ID="lblPerfil" runat="server" Text='<%# Eval("Perfil") %>'></asp:Label>

																			</ItemTemplate>
																		</asp:TemplateField>
																	</Columns>
																	<EmptyDataTemplate>
																		No se han encontrado roles asignados.
																	</EmptyDataTemplate>
																</asp:GridView>
															</div>
														</div>
														<div class="espacio"></div>
														<br />
														<asp:Button ID="btnRolEliminar" runat="server" SkinID="ui-boton-default" Enabled="false" Text="Quitar" OnClick="btnRolEliminar_Click" />
													</div>
												</div>
											</div>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="return UsuarioCerrar();" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
							</div>
						</div>
					</div>
				</asp:Panel>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>

	<div id="CambiarClave" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Cambiar Clave</h6>
					<button type="button" class="close" data-dismiss="modal">×</button> 
				</div>
				<asp:UpdatePanel ID="upCambiarClave" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="paCambiarClave" runat="server" DefaultButton="btnCambiarClave">
							<asp:HiddenField ID="hfIDUsuarioClave" runat="server" Value="0" />
							<div class="modal-body">

								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>N° Documento:</label>
											<asp:TextBox ID="txtNumeroDocumentoClave" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-8">
										<div class="form-group">
											<label>Apellidos y Nombre:</label>
											<asp:TextBox ID="txtNombreCompletoClave" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Usuario:</label>
											<asp:TextBox ID="txtUsuarioClave" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Nueva Contraseña :</label>
											<asp:TextBox ID="txtNuevaClave" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarCambiarC" runat="server" Text="Cancelar" SkinID="ui-boton-danger" CausesValidation="False" TabIndex="106" OnClientClick="return CambiarClaveCerrar();" />
								<asp:Button ID="btnCambiarClave" runat="server" Text="Guardar" ValidationGroup="frmCambiarClave" TabIndex="105" OnClick="btnCambiarClave_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="gvUsuarios" EventName="RowCommand" />
					</Triggers>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>



	<script type="text/javascript">
		function ConfigJS() {
			// Estados Checkbox

			// Selección de ítems grilla Disponibles
			console.log("1");
			$("#chkRDTodos").on("change", function () {
				console.log("2");
				$("#<%= gvRolesDisponibles.ClientID %> #chkRDSel").each(function () {
					$(this).prop("checked", $("#chkRDTodos").is(":checked"));
				});
				$('#<%= btnRolAgregar.ClientID %>').prop("disabled", true).addClass("aspNetDisabled");
				if ($('#<%= gvRolesDisponibles.ClientID %> #chkRDSel:checked').length > 0) {
					console.log("3");
					$('#<%= btnRolAgregar.ClientID %>').prop("disabled", false).removeClass("aspNetDisabled");
				}
			});

			$("#<%= gvRolesDisponibles.ClientID %> #chkRDSel").on("change", function () {
				var SeleccionarTodos = true;
				if ($("#<%= gvRolesDisponibles.ClientID %> #chkRDSel:not(:checked)").length > 0) {
					SeleccionarTodos = false;
				}
				$("#chkRDTodos").prop("checked", SeleccionarTodos);
				$('#<%= btnRolAgregar.ClientID %>').prop("disabled", true).addClass("aspNetDisabled");
				if ($('#<%= gvRolesDisponibles.ClientID %> #chkRDSel:checked').length > 0) $('#<%= btnRolAgregar.ClientID %>').prop("disabled", false).removeClass("aspNetDisabled");
			});

			// Selección de ítems grilla Asignados

			$("#chkRATodos").on("change", function () {
				$("#<%= gvRolesAsignados.ClientID %> #chkRASel").each(function () {
					$(this).prop("checked", $("#chkRATodos").is(":checked"));
				});
				$('#<%= btnRolEliminar.ClientID %>').prop("disabled", true).addClass("aspNetDisabled");
				if ($('#<%= gvRolesAsignados.ClientID %> #chkRASel:checked').length > 0) $('#<%= btnRolEliminar.ClientID %>').prop("disabled", false).removeClass("aspNetDisabled");
			});

			$("#<%= gvRolesAsignados.ClientID %> #chkRASel").on("change", function () {
				var SeleccionarTodos = true;
				if ($("#<%= gvRolesAsignados.ClientID %> #chkRASel:not(:checked)").length > 0) {
					SeleccionarTodos = false;
				}
				$("#chkRATodos").prop("checked", SeleccionarTodos);
				$('#<%= btnRolEliminar.ClientID %>').prop("disabled", true).addClass("aspNetDisabled");
				if ($('#<%= gvRolesAsignados.ClientID %> #chkRASel:checked').length > 0) $('#<%= btnRolEliminar.ClientID %>').prop("disabled", false).removeClass("aspNetDisabled");
			});
		}

		function UsuarioAbrir() {
			$("#RegUsuCol").modal("show");
		}

		function UsuarioCerrar() {
			$("#RegUsuCol").modal("hide");
			return false;
		}

		function CambiarClaveAbrir() {
			$('#CambiarClave').modal("show");
			return false;
		}

		function CambiarClaveCerrar() {
			$("#CambiarClave").modal("hide");
			return false;
		}

	</script>
</asp:Content>
