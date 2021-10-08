<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TipoCambio.aspx.cs" Inherits="Farmacia.Configuracion.TipoCambio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Tipo Cambio</h3>
		</div>
		<div class="panel-body">

			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label>Periodo Año:</label>
								<asp:DropDownList ID="ddlPeriodoAnio" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label>Periodo Mes:</label>
								<asp:DropDownList ID="ddlPeriodoMes" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
								<asp:Button ID="btnNuevo" runat="server" SkinID="ui-boton-default" OnClick="btnNuevo_Click" Text="Nuevo" />
								<asp:Button ID="btnSincronizar" runat="server" Visible="false" OnClick="btnSincronizar_Click" Text="Sincronizar" />
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-8">
							<div class="table-responsive">
								<asp:GridView ID="gvLista" DataKeyNames="IDTipoCambio" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("IDTipoCambio") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("IDMoneda") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Fecha Publicación" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaPublicacion", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="PrecioCompra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("PrecioCompra") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="PrecioVenta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("PrecioVenta") %>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="gvLista" EventName="PageIndexChanging" />
				</Triggers>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="ModalTipoCambio" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos de Tipo de Cambio</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:HiddenField ID="hdfIDTipoCambio" runat="server" Value="0" />
							<div class="modal-body">
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Fecha Publicación:</label>
											<asp:TextBox ID="txtFechaPublicacion" SkinID="ui-textbox-fecha-simple-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Moneda:</label>
											<asp:DropDownList ID="ddlIDMoneda" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Precio Compra:</label>
											<asp:TextBox ID="txtPrecioCompra" runat="server" SkinID="ui-textbox-price4-requerido"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Precio Venta:</label>
											<asp:TextBox ID="txtPrecioVenta" runat="server" SkinID="ui-textbox-price4-requerido"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalTipoCambio')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>

</asp:Content>
