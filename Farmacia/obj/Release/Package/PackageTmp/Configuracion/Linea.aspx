<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Linea.aspx.cs" Inherits="Farmacia.Configuracion.Linea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Línea</h2>
					</div>
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
								<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDLinea" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("IDLinea") %>
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
		</div>
	</div>



	<div id="DatosLinea" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos de Linea</h6>
					<button type="button" class="close" data-dismiss="modal">×</button> 
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<asp:HiddenField ID="hdfIDLinea" runat="server" Value="0" />
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Codigo:</label>
											<asp:TextBox ID="txtCodigo" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-10">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnCancelar_Click" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>

	<script type="text/javascript">

		function funModalAbrir() {
			$('#DatosLinea').modal('show');
		}

		function funModalCerrar() {
			$('#DatosLinea').modal('hide');
		}

	</script>

</asp:Content>
