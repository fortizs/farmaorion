<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Operacion.aspx.cs" Inherits="Farmacia.CajaBanco.Operacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Operaciones</h3>
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
						<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDOperacion" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="TipoOperacion" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("TipoOperacion") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("Codigo") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nombre" ItemStyle-Width="50%">
									<ItemTemplate>
										<%# Eval("Nombre") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<HeaderStyle HorizontalAlign="Center" />
									<ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:CheckBoxField>
								<asp:TemplateField ShowHeader="False" HeaderText="Editar" ItemStyle-Width="5%">
									<ItemTemplate>
										<div style="width: 40px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
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

	<div id="DatosOperacion" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Operacion</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDOperacion" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Codigo:</label>
											<asp:TextBox ID="txtCodigo" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Tipo Operación:</label>
											<asp:DropDownList ID="ddlTipoOperacion" runat="server" SkinID="ui-dropdownlist-requerido">
												<asp:ListItem Value="0">-Seleccionar-</asp:ListItem>
												<asp:ListItem Value="I">I-Ingreso</asp:ListItem>
												<asp:ListItem Value="S">S-Salida</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-10">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:CheckBox ID="chkEstado" runat="server" Text="Estado" />
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnCancelar_Click" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>
			</div>
		</div>
	</div>

	<script type="text/javascript">

		function funModalAbrir() {
			$('#DatosOperacion').modal('show');
		}

		function funModalCerrar() {
			$('#DatosOperacion').modal('hide');
		}

	</script>

</asp:Content>

