<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TipoComprobante.aspx.cs" Inherits="Farmacia.Configuracion.TipoComprobante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Tipo Comprobantes</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDTipoComprobante" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label class="etiqueta">Filtro: <b>[Nombre]</b></label>
								<asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese criterio" MaxLength="50"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
								<asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
							</div>
						</div>
					</div>
					<div class="table-responsive">
						<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDTipoComprobante" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("IDTipoComprobante") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("Sigla") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
									<ItemTemplate>
										<%# Eval("Nombre") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="CodigoSunat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("CodigoSunat") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<HeaderStyle HorizontalAlign="Center" />
									<ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:CheckBoxField>
								<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 80px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px" title="Eliminar">
													<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDTipoComprobante").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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

	<div id="DatosTipoComprobante" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos Tipo Comprobante</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardarComprobante">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sigla:</label>
											<asp:TextBox ID="txtSigla" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-10">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row"> 
									<div class="col-md-4">
										<div class="form-group">
											<label>Cod. Sunat:</label>
											<asp:TextBox ID="txtCodigoSunat" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>IndDoc:</label>
											<asp:TextBox ID="txtIndDoc" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:CheckBox ID="chkEstado" runat="server" Text="Estado" Checked="true" />
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosTipoComprobante')" />
								<asp:Button ID="btnGuardarComprobante" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardarComprobante_Click" />

							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>
	 
</asp:Content>
