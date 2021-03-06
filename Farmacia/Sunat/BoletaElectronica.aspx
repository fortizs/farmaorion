<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BoletaElectronica.aspx.cs" Inherits="Farmacia.Sunat.BoletaElectronica" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Boletas Electrónica</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDFacturaBoleta" runat="server" Value="0" />
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Sucursal:</label>
									<asp:DropDownList ID="ddlIDSucursal" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label>Filtro <b>(Serie-Numero/Afectado/Cliente)</b>:</label>
									<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Estado Sunat:</label>
									<asp:DropDownList ID="ddlIDEstadoSunat" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-1">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
								</div>
							</div>
						</div>
						<div class="table-responsive">
							<asp:GridView ID="gvListaBoleta" runat="server" DataKeyNames="IDFacturaBoleta" AllowPaging="True" Width="100%" OnPageIndexChanging="gvListaBoleta_PageIndexChanging" OnRowCommand="gvListaBoleta_RowCommand" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateField HeaderText="Tipo Doc." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
										<ItemTemplate>
											<%# Eval("IDTipoComprobante") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<span style="color: black; font-weight: bold;"><%# Eval("SerieNumero") %></span>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("FechaEmision") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="22%">
										<ItemTemplate>
											<b>Nro.Doc:</b><%# Eval("NumeroDocumentoAdquiriente") %><br />
											<b>Cliente:</b><%# Eval("RazonSocialAdquiriente") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
										<ItemTemplate>
											<%# Eval("MonedaSimbolo") %> <%# Eval("TotalVenta") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Estado Sunat" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
										<ItemTemplate>
											<i class="icon-clipboard"></i><%# Eval("EstadoSunat") %><br />
											<i class="icon-calendar52"></i><%# (Eval("FechaEnvioSunat", "{0:dd/MM/yyyy}") == "01/01/1900") ? "": Eval("FechaEnvioSunat") %><br />
											<i class="icon-bubble-dots4"></i><%# Eval("MensajeSunat") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
										<ItemTemplate>
											<div style="min-width: 115px;">
												<ul class="icons-list">
													<li style='<%# (Eval("IDEstadoSunat").ToString() == "2") ? "display:none;": "width: 35px;" %>' data-popup="tooltip" data-original-title="Enviar a Sunat">
														<asp:LinkButton ID="lnkEnviarSunat" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="FirmarEnviar" CommandArgument='<%# Eval("IDFacturaBoleta").ToString() %>'><i class="icon-database-insert"></i></asp:LinkButton>
													</li>
													<li style='<%# (Eval("IDEstadoSunat").ToString() == "2") ? "width: 35px;": "width: 35px;" %>' data-popup="tooltip" title="Descargar XML-CDR" data-original-title="Descargar CDR">
														<asp:LinkButton ID="lnkDescargarCDR" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="DescargaXML" CommandArgument='<%# Eval("IDFacturaBoleta").ToString() %>'><i class="icon-cloud-download"></i></asp:LinkButton>
													</li>
													<li class="text-warning" style="width: 35px;" data-popup="tooltip" data-original-title="Imprimir Comprobante">
														<a href="javascript:;" title="Imprimir pdf" class="btn btn-primary btn-xs" onclick="<%# String.Format("return ModalImpresion(" + (char)39 + "#ModalImprimir" + (char)39 + ", " + (char)39 + "{0}?ID={1}" + (char)39 + ");", ResolveClientUrl("~/Ventas/Imprimir.aspx"), Eval("IDVenta").ToString().Trim()) %>" target="_blank" title="Imprimir Comprobante"><i class="icon-printer"></i></a>
													</li>
													<li style='<%# (Int32.Parse(Eval("IDEstadoSunat").ToString()) == 1) ? "width: 35px;": "display:none;" %>' data-popup="tooltip" title="Anular Comprobante">
														<asp:LinkButton ID="lnkAnular" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Anular" CommandArgument='<%# Eval("IDFacturaBoleta").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas anular este documento?\",\"{0}\"); return false;", Eval("SerieNumero"))%>'><i class="icon-trash"></i></asp:LinkButton>
													</li>
												</ul>
											</div>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
					</asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="ModalImprimir" class="modal fade mprint">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-body">
					<div class="loading-iframe">
						<iframe src="" style="width: 100%; height: 450px; border: none;"></iframe>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-link" data-dismiss="modal">Cerrar</button>
				</div>
			</div>
		</div>
	</div>

	<div id="EnviarMail" class="modal fade" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Envío de Mail</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body" style="padding: 0;">
					<div class="loading-iframe">
						<iframe id="ifrmEnviarMail" src="" style="width: 100%; height: 620px; border: none;"></iframe>
					</div>
				</div>
			</div>
		</div>
	</div>


	<div style="display: none;">
		<iframe id="ifrmDescargar" src="" style="border: 0px; padding: 0; width: 0; height: 0;"></iframe>
		<iframe id="ifrmGeneraExcel" src="" style="width: 100%; height: 350px; border: none;"></iframe>
	</div>


	<script type="text/javascript">
		function Descargar(pCodigo) {
			$('#ifrmDescargar').attr('src', 'Descargar.aspx?pCodigo=' + pCodigo + "&pTipo=FB");
		}

		function DescargarCerrar() {
			$('#ifrmDescargar').attr('src', '');
		}

		function EnviarMailAbrir() {
			$("#ifrmEnviarMail").attr("src", "<%= ResolveClientUrl("~/Controles/ciEnviarMail.aspx") %>");
			$('#EnviarMail').modal('show');
			return false;
		}

		//function EnviarMailCerrar() {
		//	$("#ifrmEnviarMail").attr("src", "");
		//	console.log("entrando a cerrar email");
		//	$('#EnviarMail').modal('hide');
		//	Mensaje("confirmation", "El correo se envió correctamente");
		//}

		function EnviarMailCerrar(paccion) {
			$("#ifrmEnviarMail").attr("src", "");
			$('#EnviarMail').modal('hide');
			if (paccion == "E") {
				Mensaje("confirmation", "El correo se envió correctamente");
			}
		}

	</script>
</asp:Content>
