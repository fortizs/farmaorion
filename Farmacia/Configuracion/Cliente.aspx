<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Cliente.aspx.cs" Inherits="Farmacia.Configuracion.Cliente" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Clientes</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDCliente" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<label>Filtro: <b>[Nro.Documento|Razón Social]</b></label>
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
						<asp:GridView ID="gvLista" runat="server" DataKeyNames="IDCliente" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvLista_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
							<Columns>
								<asp:TemplateField HeaderText="IDCliente" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
									<ItemTemplate>
										<%# Eval("IDCliente") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("TipoDocumento") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Número Documento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
									<ItemTemplate>
										<%# Eval("NumeroDocumento") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Razón Social" ItemStyle-Width="30%">
									<ItemTemplate>
										<%# Eval("RazonSocial") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Límite de Crédito" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("LimiteCredito","{0:N}") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Dias de Crédito" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
									<ItemTemplate>
										<%# Eval("DiasCredito") %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
									<ItemTemplate>
										<div style="width: 75px;">
											<ul class="icons-list">
												<li style="width: 35px">
													<asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
												</li>
												<li style="width: 35px" title="Eliminar">
													<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDCliente").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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

	<div id="DatosCliente" class="modal fade" role="dialog">
		<div class="modal-dialog modal-xl">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos del Cliente</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div> 
				<asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
							<div class="modal-body">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Tipo de Documento:</label>
											<asp:DropDownList ID="ddlIDTipoDocumento" runat="server" SkinID="ui-dropdownlist-requerido" AutoPostBack="true" OnSelectedIndexChanged="ddlIDTipoDocumento_SelectedIndexChanged"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Numero de documento:</label>
											<asp:TextBox ID="txtNumeroDocumento" MaxLength="11" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<asp:Label ID="lblRazonSocial" runat="server">Razón Social:</asp:Label>
											<asp:TextBox ID="txtRazonSocial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3" id="div_NombreComercial" runat="server">
										<div class="form-group">
											<asp:Label ID="lblNombreComercial" runat="server">Nombre Comercial:</asp:Label>
											<asp:TextBox ID="txtNombreComercial" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Sexo:</label>
											<asp:DropDownList ID="ddlSexo" runat="server">
												<asp:ListItem Value="0">-Ninguno-</asp:ListItem>
												<asp:ListItem Value="M">Masculino</asp:ListItem>
												<asp:ListItem Value="F">Femenino</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Fecha Nacimiento:</label>
											<asp:TextBox ID="txtFechaNacimiento" runat="server" SkinID="ui-textbox-fecha-simple" MaxLength="10" onchange="CalcularEdad('cphPrincipal_txtFechaNacimiento','cphPrincipal_txtEdad')"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Edad:</label>
											<asp:TextBox ID="txtEdad" runat="server" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Teléfono:</label>
											<asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label for="tags">Ubigeo:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfRegIDUbigeo" runat="server" Value="0" />
												<asp:TextBox ID="txtRegUbigeo" Enabled="false" runat="server"></asp:TextBox>
												<span class="input-group-btn">
													<button class="btn btn-primary" type="button" onclick="BuscarUbigeo();"><i class="icon-search4"></i></button>
												</span>
											</div>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Urbanización:</label>
											<asp:TextBox ID="txtUrbanizacion" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Direccion:</label>
											<asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Correo:</label>
											<asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Límite Crédito:</label>
											<asp:TextBox ID="txtLimiteCredito" runat="server" SkinID="ui-textbox-price"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Días Crédito:</label>
											<asp:TextBox ID="txtDiasCredito" runat="server" SkinID="ui-textbox-number"></asp:TextBox>
										</div>
									</div>
								</div>

							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClientClick="CerrarModal('DatosCliente')" />
								<asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="BuscarUbigeo" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Buscar Ubigeo</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upBuscarUbigeo" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<uc1:ccBuscarUbigeo runat="server" ID="ccBuscarUbigeo" />
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
				<div class="modal-footer">
				</div>
			</div>
		</div>
	</div>

	<script type="text/javascript">

		function ConfigJS() {
			$("#cphPrincipal_txtFechaNacimiento").mask('00/00/0000');
			$("#cphPrincipal_txtFechaNacimiento").attr("placeholder", "__/__/____");
			$("#cphPrincipal_txtFechaNacimiento").daterangepicker({
				autoApply: true,
				singleDatePicker: true,
				showDropdowns: true,
				initialValue: false,
				outsideClickUpdateDate: false
			});
		}


		function parseDate(input) {
			var parts = input.split('/');
			return new Date(parts[2], parts[1] - 1, parts[0]); // Note: months are 0-based
		}

		/*----------Funcion para obtener la edad------------*/
		function CalcularEdad(txtfecha, txtRetorno) {
			var fecha = $("#" + txtfecha).val();
			var DateServer = '<%= (DateTime.Now.ToString("dd/MM/yyyy")) %>';
			console.log("fecha =" + fecha);
			var fechaActual = parseDate(DateServer);
			if (fecha.length > 0) {
				var diaActual = fechaActual.getDate();
				var mmActual = fechaActual.getMonth() + 1;
				var yyyyActual = fechaActual.getFullYear();
				FechaNac = fecha.split("/");
				var diaCumple = FechaNac[0];
				var mmCumple = FechaNac[1];
				var yyyyCumple = FechaNac[2];
				//retiramos el primer cero de la izquierda
				if (mmCumple.substr(0, 1) == 0) {
					mmCumple = mmCumple.substring(1, 2);
				}
				//retiramos el primer cero de la izquierda
				if (diaCumple.substr(0, 1) == 0) {
					diaCumple = diaCumple.substring(1, 2);
				}
				var edad = yyyyActual - yyyyCumple;

				//validamos si el mes de cumpleaños es menor al actual
				//o si el mes de cumpleaños es igual al actual
				//y el dia actual es menor al del nacimiento
				//De ser asi, se resta un año
				if ((mmActual < mmCumple) || (mmActual == mmCumple && diaActual < diaCumple)) {
					edad--;
				}
				if (edad < 0) {
					edad = 0;
				}
				$("#" + txtRetorno).val(edad);

			}
		};

		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_hdfRegIDUbigeo', 'cphPrincipal_txtRegUbigeo');
		}

	</script>
</asp:Content>
