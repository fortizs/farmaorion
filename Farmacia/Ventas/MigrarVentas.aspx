<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MigrarVentas.aspx.cs" Inherits="Farmacia.Ventas.MigrarVentas" %>


<%@ Register Src="~/Controles/ccBuscarCliente.ascx" TagPrefix="uc1" TagName="ccBuscarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.filtered-list-search form button {
			border-radius: 50%;
			padding: 7px 7px;
			position: absolute;
			right: 4px;
			top: 4px;
		}

		.alert-icon-left {
			border-left: 64px solid;
		}

		.alert-light-primary {
			color: #1b55e2;
			background-color: #c2d5ff;
			border-color: #1b55e2;
		}

		.alert-icon-left svg:not(.close) {
			color: #FFF;
			width: 4rem;
			left: -4rem;
			text-align: center;
			position: absolute;
			top: 50%;
			margin-top: -10px;
			font-size: 1.25rem;
			font-weight: 400;
			line-height: 1;
			-webkit-font-smoothing: antialiased;
			-moz-osx-font-smoothing: grayscale;
		}

		svg {
			overflow: hidden;
			vertical-align: middle;
		}

		.btn {
			border-radius: 0.0rem;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Migrar Ventas</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label>Sucursal:</label>
								<asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label>Tipo Comprobante:</label>
								<asp:DropDownList ID="ddlBIDTipoComprobante" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label>Estado:</label>
								<asp:DropDownList ID="ddlBIDEstado" runat="server"></asp:DropDownList>
							</div>
						</div> 
						<div class="col-md-5">
							<div class="form-group">
								<label>Filtro:</label>
								<asp:TextBox ID="txtBFiltro" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Inicio:</label>
								<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback form-control-feedback-sm">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Fin:</label>
								<asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback form-control-feedback-sm">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
								<asp:Button ID="btnMigrar" runat="server" Text="Migrar" OnClick="btnMigrar_Click" />
							</div>
						</div>
					</div>
					<div class="table-responsive">
						<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDVenta" Width="100%" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging">
							<Columns>
								<asp:TemplateField HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<span class="badge badge-pills outline-badge-primary" data-popup="tooltip" title='<%# Eval("TipoComprobante") %>'><%# Eval("TipoComprobanteCodigoSunat") %></span>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Serie-Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("SerieNumero") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
									<ItemTemplate>
										<%# Eval("FechaVenta", "{0:dd/MM/yyyy HH:mm:ss}") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%">
									<ItemTemplate>
										<b>Nro.Doc:</b><%# Eval("NumeroDocumentoCliente") %><br />
										<b>Cliente:</b><%# Eval("Cliente") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("Simbolo") + " " + Eval("TotalVenta") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("EstadoSunat") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Cobranza" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("EstadoCobranza") %>
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
	  
</asp:Content>

