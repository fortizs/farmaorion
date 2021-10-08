<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RegistroPagoProveedores.aspx.cs" Inherits="Farmacia.CuentasPorPagar.RegistroPagoProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtSerieDocumento {
			text-transform: uppercase;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Pago de Proveedores</h3>
		</div>
		<div class="panel-body">
			<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
						<i class="icon-stack3 position-left"></i>
						Lista</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
						<i class="icon-pencil7 position-left"></i>
						Registro</a>
				</li>
			</ul>
			<div class="tab-content mb-4" id="border-tabsContent">
				<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upPagoListar" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnPagoListar" runat="server" DefaultButton="lnkBuscarDocumentoPago">
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Proveedor:</label>
											<asp:DropDownList ID="ddlBIDProveedor" runat="server"></asp:DropDownList>
										</div>
									</div>
									<%--  <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Concepto Pago:</label>
                                                    <asp:DropDownList ID="ddlBIDConceptoPago" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>--%>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Inicio:</label>
											<asp:TextBox ID="txtBFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Fin:</label>
											<asp:TextBox ID="txtBFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>

									<div class="col-md-1">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:LinkButton ID="lnkBuscarDocumentoPago" runat="server" Text="Buscar" CssClass="btn btn-wide btn-default" OnClick="lnkBuscarDocumentoPago_Click"></asp:LinkButton>
										</div>
									</div>
								</div>

								<div class="table-responsive">
									<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDPago" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
										<Columns>
											<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<ItemTemplate>
													<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Número Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("NumeroPagoFormato") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Pago" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaPago", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie-Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("SerieNumero") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Nro.Documento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("ProveedorNumeroDocumento") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Proveedor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
												<ItemTemplate>
													<%# Eval("Proveedor") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Concepto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
												<ItemTemplate>
													<%# Eval("Concepto") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Importe Pagado" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("Simbolo") %> <%# Eval("ImportePagado", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
								<br />
							</asp:Panel>
						</ContentTemplate>
					</asp:UpdatePanel>

				</div>

				<div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upPagoRegistro" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnCompraProveedor" runat="server" DefaultButton="lnkBuscarDocumento">
								<div class="row">
									<div class="col-md-12">
										<div class="panel-heading">
											<h3 class="panel-title">Pago de Documentos</h3>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Proveedor:</label>
											<asp:DropDownList ID="ddlIDProveedor" runat="server" OnSelectedIndexChanged="ddlIDProveedor_SelectedIndexChanged"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Email:</label>
											<asp:TextBox ID="txtEmail" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtCelular" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo:</label>
											<asp:DropDownList ID="ddlTipoFiltro" runat="server">
												<asp:ListItem Value="FR">Fecha Registro</asp:ListItem>
												<asp:ListItem Value="FE">Fecha Emisión</asp:ListItem>
												<asp:ListItem Value="FV">Fecha Vencimiento</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Inicio:</label>
											<asp:TextBox ID="txtFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido" MaxLength="10"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Fin:</label>
											<asp:TextBox ID="txtFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido" MaxLength="10"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Filtro:</label>
											<asp:TextBox ID="txtFiltroDocumento" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:LinkButton ID="lnkBuscarDocumento" runat="server" CssClass="btn btn-success" OnClick="lnkBuscarDocumento_Click"><i class="icon-search4"></i> Buscar </asp:LinkButton>
										</div>
									</div>
								</div>
								<div class="table-responsive">
									<asp:GridView ID="gvCompraPendienteProveedor" runat="server" DataKeyNames="IDCompras" AllowPaging="True" Width="100%" OnPageIndexChanging="gvCompraPendienteProveedor_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
										<Columns>
											<asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<HeaderTemplate>
													<asp:CheckBox ID="chkSeleccionarTodos" runat="server" ClientIDMode="Static" />
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox ID="chkSeleccionar" runat="server" ClientIDMode="Static" data-id='<%# Eval("IDCompras").ToString() %>' />
													<asp:HiddenField ID="hfIDCompra" runat="server" Value='<%# Eval("IDCompras") %>' />
													<asp:HiddenField ID="hfSaldo" runat="server" Value='<%# Eval("Saldo", "{0:N}") %>' />
													<asp:HiddenField ID="hfSerie" runat="server" Value='<%# Eval("Serie") %>' />
													<asp:HiddenField ID="hfNumeroComprobante" runat="server" Value='<%# Eval("NumeroComprobante") %>' />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<ItemTemplate>
													<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Número Compra" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("NumeroCompraFormato") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie-Número" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("Serie") %> - <%# Eval("NumeroComprobante") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaEmision", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Registro" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaRegistro", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Vencimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Total Compra" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("MonedaSimbolo") %> <%# Eval("TotalCompra", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("MonedaSimbolo") %> <%# Eval("Saldo", "{0:N}") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Importe a Pagar" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
												<ItemTemplate>
													<asp:TextBox ID="txtImportePagar" runat="server" SkinID="ui-textbox-price-requerido" Text='<%# Eval("Saldo", "{0:N}") %>'></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>

							</asp:Panel>


							<asp:Panel ID="pnPagoRegistro" runat="server">
								<div class="row">
									<div class="col-md-5">
										<div class="form-group">
											<label>Medio Pago:</label>
											<asp:DropDownList ID="ddlIDMedioPago" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Banco:</label>
											<asp:DropDownList ID="ddlIDBanco" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Cuenta Corriente:</label>
											<asp:TextBox ID="txtCuentaCorriente" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Número Operación:</label>
											<asp:TextBox ID="txtNumeroOperacion" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Moneda de Pago:</label>
											<asp:DropDownList ID="ddlIDMonedaPago" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group has-feedback">
											<label>Fecha Pago:</label>
											<asp:TextBox ID="txtFechaPago" SkinID="ui-textbox-fecha-simple-requerido" MaxLength="10" runat="server" Enabled="false"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Glosa:</label>
											<asp:TextBox ID="txtGlosa" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
										</div>
									</div>
								</div>
							</asp:Panel>
							<div class="row">
								<div class="col-md-12 text-right">
									<div class="form-group">
										<label class="etiqueta"></label>
										<asp:LinkButton ID="lnkNuevoPago" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkNuevoPago_Click"><i class="icon-file-plus"></i> Nuevo Pago</asp:LinkButton>
										<asp:LinkButton ID="lnkGuardarPago" runat="server" CssClass="btn btn-success" OnClick="lnkGuardarPago_Click"><i class="icon-floppy-disk"></i> Guardar Pago</asp:LinkButton>
									</div>
								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />
	<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>


	<script type="text/javascript">
		function ConfigJS() {


			$("#<%= ddlIDProveedor.ClientID %>").select2({
				width: "100%"
			});

          <%--  $("#<%= ddlIDConceptoPago.ClientID %>").select2({
                width: "100%"
            });--%>


			$("#<%= ddlBIDProveedor.ClientID %>").select2({
				width: "100%"
			});

			$("#chkSeleccionarTodos").on("change", function () {
				$("#<%= gvCompraPendienteProveedor.ClientID %> #chkSeleccionar").each(function () {
					$(this).prop("checked", $("#chkSeleccionarTodos").is(":checked"));
				});
			});

				$("#<%= gvCompraPendienteProveedor.ClientID %> #chkSeleccionar").on("change", function () {
				var SeleccionarTodos = true;
				if ($("#<%= gvCompraPendienteProveedor.ClientID %> #chkSeleccionar:not(:checked)").size() > 0) {
					SeleccionarTodos = false;
				}
				$("#chkSeleccionarTodos").prop("checked", SeleccionarTodos);
				});
		}

		function ActivarTabxId(id) {
			var x = $("#TabActivo").val(id);
			ActivarTabxBoton();
		}

		function ActivarTabxBoton() {
			$('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
		}

	</script>

</asp:Content>
