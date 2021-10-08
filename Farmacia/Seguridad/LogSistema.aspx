<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogSistema.aspx.cs" Inherits="Farmacia.Seguridad.LogSistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.DetalleError {
			overflow: auto;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Log del Sistema</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta">Módulo:</label>
									<asp:DropDownList ID="ddlModulo" runat="server">
									</asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta">Desde:</label>
									<asp:TextBox ID="txtFechaDesde" runat="server" MaxLength="10" placeholder="__/__/____" SkinID="ui-textbox-fecha-simple"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta">Hasta:</label>
									<asp:TextBox ID="txtFechaHasta" runat="server" MaxLength="10" placeholder="__/__/____" SkinID="ui-textbox-fecha-simple"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta">Filtro:</label>
									<asp:DropDownList ID="ddlFiltro" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
										<asp:ListItem>Ninguno</asp:ListItem>
										<asp:ListItem Value="IDLogSistema">Nro. Log</asp:ListItem>
										<asp:ListItem>Usuario</asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2" runat="server" id="cFiltro" visible="false">
								<div class="form-group">
									<label class="etiqueta">
										<asp:Label ID="lblFiltro" runat="server"></asp:Label></label>
									<asp:TextBox ID="txtBuscar" runat="server" MaxLength="50" placeholder="Ingrese criterio" Visible="False"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar"/>
								</div>
							</div>
						</div> 
						<div class="table-responsive">
							<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDLogSistema" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundField DataField="IDLogSistema" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle HorizontalAlign="Center" Width="5%" />
									</asp:BoundField>
									<asp:BoundField DataField="Modulo" HeaderText="Módulo" HeaderStyle-HorizontalAlign="Center">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="10%" />
									</asp:BoundField> 
									<asp:BoundField DataField="Compilado" HeaderText="Compilado" HeaderStyle-HorizontalAlign="Center">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="5%" HorizontalAlign="Center" />
									</asp:BoundField> 
									<asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="7%" HorizontalAlign="Center" />
									</asp:BoundField> 
									<asp:BoundField DataField="Fecha" HeaderText="Hora" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0: hh:mm:ss tt}">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="7%" HorizontalAlign="Center" />
									</asp:BoundField> 
									<asp:BoundField DataField="Evento" HeaderText="Evento" HeaderStyle-HorizontalAlign="Center">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="10%" />
									</asp:BoundField> 
									<asp:BoundField DataField="Usuario" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Center">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="7%" />
									</asp:BoundField> 
									<asp:BoundField DataField="MensajeError" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
										<HeaderStyle HorizontalAlign="Center" />
										<ItemStyle Width="40%" />
									</asp:BoundField> 
									<asp:TemplateField ShowHeader="False">
										<ItemTemplate>
											<asp:LinkButton ID="lbEditar" CssClass="btn btn-default btn-lg" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Ver"><span class="icon-search4"></span></asp:LinkButton>
										</ItemTemplate>
										<ItemStyle HorizontalAlign="Center" Width="3%" />
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
					</asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="DatosLog" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Información del Log</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server">
					<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>ID:</label>
											<asp:Label ID="lblIDLogSistema" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Compilado:</label>
											<asp:Label ID="lblCompilado" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Módulo:</label>
											<asp:Label ID="lblModulo" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Fecha Hora:</label>
											<asp:Label ID="lblFecha" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>

								</div>
								<div class="row">
									<div class="col-md-4">
										<div class="form-group">
											<label>Evento:</label>
											<asp:Label ID="lblEvento" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>

									<div class="col-md-4">
										<div class="form-group">
											<label>Usuario:</label>
											<asp:Label ID="lblUsuario" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>IP:</label>
											<asp:Label ID="lblHost" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Url:</label>
											<asp:Label ID="lblOpcion" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Detalle:</label>
											<asp:Label ID="lblDetalle" runat="server" CssClass="form-control"></asp:Label>
										</div>
									</div>
								</div>
								<pre class="language-" style="height: 290px;">
                                    <asp:Label ID="lblMensajeError" runat="server" Width="1500px"></asp:Label>                           
                               </pre>
							</div>


						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>
			</div>
		</div>
	</div>
	<script type="text/javascript">

		function verLog() {
			$('#DatosLog').modal('show');
		}
	</script>

</asp:Content>
