<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kardex.aspx.cs" Inherits="Farmacia.Almacen.Kardex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Kardex</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upConsulta" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label id="lblEmpresa" runat="server" forecolor="Red" font-bold="true" visible="false" />
								<label>Sucursal:</label>
								<asp:DropDownList ID="ddlSucursalOrigen" runat="server">
								</asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Movimiento Inicio:</label>
								<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback form-control-feedback-sm">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group has-feedback">
								<label>Fecha Movimiento Fin:</label>
								<asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
								<div class="form-control-feedback form-control-feedback-sm">
									<i class="icon-calendar"></i>
								</div>
							</div>
						</div>
						<div class="col-md-4">
							<div class="form-group">
								<label>Producto:</label>
								<asp:DropDownList ID="ddlRegProducto" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:LinkButton ID="lnkBuscarKardex" runat="server" CssClass="btn btn-info" OnClick="lnkBuscarKardex_Click">Consultar </asp:LinkButton>
								<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('KARDEX');">Excel</a>
							</div>
						</div> 
					</div>
					<div class="row">
						<div class="col-md-12">
							<div class="table-responsive" style="margin-bottom: 0;">
								<asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDMovimientoDetalle" AllowPaging="True" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="gvDetalleLista_PageIndexChanging">
									<Columns>
										<asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("FechaMovimiento") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Serie-Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("DocumentoReferencia") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Transaccion" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
											<ItemTemplate>
												<%# Eval("Transaccion") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Entrada Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("EntradaCantidad").ToString() == "0.00" ? "":Eval("EntradaCantidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Entrada Precio Unidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("EntradaCantidad").ToString() == "0.00" ? "": "S/." +Eval("EntradaValorUnidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Entrada Precio Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("EntradaCantidad").ToString() == "0.00" ? "": "S/." +Eval("EntradaValorTotal", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Salida Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("SalidaCantidad").ToString() == "0.00" ? "":Eval("SalidaCantidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Salida Precio Unidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("SalidaCantidad").ToString() == "0.00" ? "": "S/." +Eval("SalidaValorUnidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Salida Precio Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("SalidaCantidad").ToString() == "0.00" ? "": "S/."+Eval("SalidaValorTotal", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField> 
										<asp:TemplateField HeaderText="Saldo Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# Eval("SaldoCantidad").ToString() == "0.00" ? "":Eval("SaldoCantidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Saldo Precio Unidad" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# "S/." + Eval("SaldoValorUnidad", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Saldo Precio Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
											<ItemTemplate>
												<%# "S/." + Eval("SaldoValorTotal", "{0:n2}") %>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>

	<iframe id="ifrmGeneraExcel" src="" style="width: 100%; height: 350px; border: none;"></iframe>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

	<script type="text/javascript">

		function ConfigJS() {
			$("#<%= ddlRegProducto.ClientID %>").select2();
		}

		function imprimir_kardex() {
			if (validar1() == false) return false;
			var pIDProducto = $("#<%= ddlRegProducto.ClientID %>").get(0).value;
        	var pIDSucursal = $("#<%= ddlSucursalOrigen.ClientID %>").get(0).value;
        	var pIDEmpresa = $("#<%= lblEmpresa.ClientID %>").html();
        	var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
        	var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();

        	console.log("pFechaInicio =" + pFechaInicio);
        	console.log("pFechaFin =" + pFechaFin);

        	$("#ifrmGeneraExcel").attr("src", "");
        	$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pTipoReporte=KARDEX&pIDSucursal=" + pIDSucursal + "&pIDProducto=" + pIDProducto + "&pIDEmpresa=" + pIDEmpresa +
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin);
        	return false;
        }


        function imprimir_kardex_total() {
        	//if (validar1() == false) return false;
        	var pIDProducto = "%";
        	var pIDSucursal = $("#<%= ddlSucursalOrigen.ClientID %>").get(0).value;
        	var pIDEmpresa = $("#<%= lblEmpresa.ClientID %>").html();
        	var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
        	var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();

        	console.log("pFechaInicio =" + pFechaInicio);
        	console.log("pFechaFin =" + pFechaFin);

        	$("#ifrmGeneraExcel").attr("src", "");
        	$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pTipoReporte=KARDEX&pIDSucursal=" + pIDSucursal + "&pIDProducto=" + pIDProducto + "&pIDEmpresa=" + pIDEmpresa +
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin);
        	return false;
        }

        function validar1() {
        	$(".error").remove();
        	if (document.getElementById("<%=ddlRegProducto.ClientID%>").selectedIndex == 0) {
        		alert("Seleccione Producto...");
        		$("#<%= ddlRegProducto.ClientID %>").focus();
            	return false;
			}
			return true;
		}

		function generar_excel(pTipoReporte) {
			var pIDSucursal = $("#<%= ddlSucursalOrigen.ClientID %>").val();
			var pIDProducto = $("#<%= ddlRegProducto.ClientID %>").val();
			var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
			var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();

			$("#ifrmGeneraExcel").attr("src", "");
			$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pIDProducto=" + pIDProducto +
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
			return false;
		}
	</script>
</asp:Content>

