<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ccBuscarCliente.ascx.cs" Inherits="Farmacia.Controles.ccBuscarCliente" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>

<asp:Panel ID="pnDatosClienteListado" runat="server" Visible="true">
	<div class="row">
		<div class="col-md-6">
			<div class="form-group">
				<asp:TextBox ID="txtFiltro" runat="server" placeholder="Buscar"></asp:TextBox>
			</div>
		</div>
		<div class="col-md-2">
			<div class="form-group">
				<asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" OnClick="btnBuscarCliente_Click" />
			</div>
		</div>
	</div>

	<div class="espacio"></div>
	<div class="table-responsive">
		<asp:GridView ID="gvClienteLista" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataKeyNames="IDCliente" GridLines="None" OnPageIndexChanging="gvClienteLista_PageIndexChanging" OnSelectedIndexChanged="gvClienteLista_SelectedIndexChanged" EnableSortingAndPagingCallbacks="True" OnRowDataBound="gvClienteLista_RowDataBound">
			<Columns>
				<asp:TemplateField HeaderText="IDCliente" Visible="False">
					<ItemTemplate>
						<asp:Label ID="lblIDCliente" runat="server" Text='<%# Bind("IDCliente") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Tipo Documento" ItemStyle-Width="15%">
					<ItemTemplate>
						<asp:HiddenField ID="hdfIDTipoCliente" runat="server" Value='<%# Bind("IDTipoDocumento") %>' />
						<asp:Label ID="lblTipoDocumento" runat="server" Text='<%# Bind("TipoDocumento") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Numero Documento" ItemStyle-Width="15%">
					<ItemTemplate>
						<asp:Label ID="lblNumeroDocumento" runat="server" Text='<%# Bind("NumeroDocumento") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Cliente" ItemStyle-Width="70%">
					<ItemTemplate>
						<asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RazonSocial") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Direccion" ItemStyle-Width="70%" Visible="False">
					<ItemTemplate>
						<asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("Direccion") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
					<ItemTemplate>
						<div style="width: 70px;">
							<ul class="icons-list">
								<li style="width: 25px;" class="text-warning-600" data-popup="tooltip" data-original-title="Editar">
									<asp:LinkButton ID="lnkEditar" runat="server" CssClass="btn btn-default btn-lg" CommandName="Select"><i class="icon-pencil"></i></asp:LinkButton>
								</li>
								<li style="width: 25px;" class="text-primary-600" data-popup="tooltip" data-original-title="Seleccionar">
									<asp:LinkButton ID="lbSeleccionar" CssClass="btn btn-default btn-lg" runat="server" CausesValidation="False" ToolTip="Seleccionar"><span class="icon-multitouch"></span></asp:LinkButton>

								</li>
							</ul>
						</div>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
	<div class="espacio"></div>
	<div class="espacio"></div>
	<div class="separador"></div>
	<div class="row">
		<div class="col-md-12 text-right">
			<asp:Button ID="btnClienteListadoCancelar" runat="server" Text="Cerrar" SkinID="ui-boton-danger" CausesValidation="False" OnClientClick="CerrarModal('DatosCliente'); return false;" />
			<asp:Button ID="btnClienteListadoNuevo" runat="server" Text="Nuevo Cliente" CausesValidation="False" OnClick="btnClienteListadoNuevo_Click" />
		</div>
	</div>
</asp:Panel>
<asp:Panel ID="pnDatosClienteRegistro" runat="server" Visible="false">
	<asp:HiddenField ID="hdfIDCliente" runat="server" Value="0" />
	<div class="row">
		<div class="col-md-2">
			<div class="form-group">
				<label>Tipo de Documento:</label>
				<asp:DropDownList ID="ddlIDTipoDocumentoX" runat="server" SkinID="ui-dropdownlist-requerido" AutoPostBack="true" OnSelectedIndexChanged="ddlIDTipoDocumentoX_SelectedIndexChanged">
					<asp:ListItem Value="1">DNI</asp:ListItem>
					<asp:ListItem Value="2">RUC</asp:ListItem>
				</asp:DropDownList>

			</div>
		</div>
		<div class="col-md-3">
			<div class="form-group">
				<label>Numero de documento:</label>
				<asp:TextBox ID="txtNumeroDocumento" SkinID="ui-textbox-requerido" MaxLength="11" runat="server"></asp:TextBox>
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
				<asp:TextBox ID="txtFechaNacimiento" runat="server" SkinID="ui-textbox-fecha-simple" MaxLength="10" onchange="CalcularEdad('cphPrincipal_ccBuscarCliente_txtFechaNacimiento','cphPrincipal_ccBuscarCliente_txtEdad')"></asp:TextBox>
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
	<div class="espacio"></div>
	<div class="espacio"></div>
	<div class="espacio"></div>
	<div class="row">
		<div class="col-md-12 text-right">
			<asp:Button ID="btnClienteRetornar" runat="server" Text="Cancelar y Retornar" SkinID="ui-boton-danger" CausesValidation="False" OnClick="btnClienteRetornar_Click" />
			<asp:Button ID="btnClienteGuardar" runat="server" Text="Guardar" CausesValidation="False" OnClick="btnClienteGuardar_Click" />
		</div>
	</div>
</asp:Panel>


<div id="BuscarUbigeo" class="modal fade" tabindex="-1" role="dialog">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header bg-hmodal">
				<h6 class="modal-title">Buscar Ubigeo</h6>
				<button type="button" class="close" data-dismiss="modal">×</button>
			</div>
			<div class="modal-body">
				<asp:UpdatePanel ID="upBuscarUbigeo" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<uc1:ccbuscarubigeo runat="server" id="ccBuscarUbigeo" />
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
			<div class="modal-footer">
			</div>
		</div>
	</div>
</div>


<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>
<script type="text/javascript">

	function ConfigJS() {
		$("#cphPrincipal_ccBuscarCliente_txtFechaNacimiento").mask('00/00/0000');
		$("#cphPrincipal_ccBuscarCliente_txtFechaNacimiento").attr("placeholder", "__/__/____");
		$("#cphPrincipal_ccBuscarCliente_txtFechaNacimiento").daterangepicker({
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


		var reBCIDCliente;
		var reBCNumeroDocumento;
		var reBCRazonSocial;
		var reBCDireccion;

		function gBuscarCliente(preBCIDCliente, preBCNumeroDocumento, preBCRazonSocial, preBCDireccion) {
			reBCIDCliente = preBCIDCliente;
			reBCNumeroDocumento = preBCNumeroDocumento;
			reBCRazonSocial = preBCRazonSocial;
			reBCDireccion = preBCDireccion;
			$('#DatosCliente').modal('show');
		}

		function SeleccionarCliente(pIDCliente, pNumeroDocumento, pNombre, pDireccion) {
			console.log("pIDCliente =" + pIDCliente);
			console.log("reBCIDCliente =" + reBCIDCliente);
			console.log("pNumeroDocumento =" + pNumeroDocumento);
			console.log("reBCNumeroDocumento =" + reBCNumeroDocumento);
			console.log("pNombre =" + pNombre);
			console.log("reBCRazonSocial =" + reBCRazonSocial);
			console.log("pDireccion =" + pDireccion);
			console.log("reBCDireccion =" + reBCDireccion);

			$("#" + reBCIDCliente).val(pIDCliente);
			$("#" + reBCNumeroDocumento).val(pNumeroDocumento);
			$("#" + reBCRazonSocial).val(pNombre);
			$("#" + reBCDireccion).val(pDireccion);
			$('#DatosCliente').modal('hide');
		}


		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_ccBuscarCliente_hdfRegIDUbigeo', 'cphPrincipal_ccBuscarCliente_txtRegUbigeo');
		}
</script>
