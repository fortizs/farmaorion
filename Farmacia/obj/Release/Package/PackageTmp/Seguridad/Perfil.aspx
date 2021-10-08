<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Farmacia.Seguridad.Perfil" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Perfiles</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label>Filtro:[<b>Nombre</b>]</label>
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
							<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDPerfil" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundField DataField="IDPerfil" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle HorizontalAlign="Center" Width="2%" />
									</asp:BoundField>
									<asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="40%" />
									</asp:BoundField>
									<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle HorizontalAlign="Center" Width="5%" />
									</asp:CheckBoxField>
									<asp:TemplateField HeaderText="Auditoría" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="13%">
										<ItemTemplate>
											<%# Eval("UsuarioModificacion") %><br />
											<b>(<%# Eval("FechaModificacion") %>)</b>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
										<ItemTemplate>
											<div style="width: 75px;">
												<ul class="icons-list">
													<li style="width: 35px">
														<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
													</li>
													<li style="width: 35px" title="Eliminar">
														<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDPerfil").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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

	<div id="DatosPerfil" class="modal animated fadeInLeft" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Perfil</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upFrm" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-12 text-right">
									<div class="form-group">
										<asp:CheckBox ID="chkEstado" runat="server" Text="Activo" />
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-2">
									<div class="form-group">
										<label>ID:</label>
										<asp:TextBox ID="txtIDPerfil" runat="server" Enabled="False"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-10">
									<div class="form-group">
										<label>Nombre:</label>
										<asp:TextBox ID="txtNombre" runat="server" MaxLength="50" SkinID="ui-textbox-requerido"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosPerfil')" />
							<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
						</div>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
					</Triggers>
				</asp:UpdatePanel>
			</div>
		</div>
	</div> 
</asp:Content>
