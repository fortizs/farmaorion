<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ComunicacionBajas.aspx.cs" Inherits="Farmacia.Sunat.ComunicacionBajas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Comunicación de Baja</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
						<asp:HiddenField ID="hdfIDComunicacionBaja" runat="server" Value="0" />
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label>Filtro <b>(IDResumen/TicketSunat)</b>:</label>
									<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Estado Sunat:</label>
									<asp:DropDownList ID="ddlIDEstadoSunat" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
									<asp:Button ID="btnCrearResumen" runat="server" Text="Nuevo" OnClick="btnCrearResumen_Click" />
								</div>
							</div>
						</div>

						<div class="table-responsive">
							<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand" DataKeyNames="IDComunicacionBaja" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
								<Columns>
									<asp:TemplateField HeaderText="IDResumen" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("IDResumen") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Comprobante" ItemStyle-Width="15%">
										<ItemTemplate>
											<%# Eval("TipoComprobante") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fecha Emision" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("FechaEmisionComprobante") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Fecha Generacion Resumen" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("FechaGeneracionResumen") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="TicketSunat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
										<ItemTemplate>
											<%# Eval("TicketSunat") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Estado Sunat" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
										<ItemTemplate>
											<i class="icon-clipboard"></i><%# Eval("EstadoSunat") %><br />
											<i class="icon-calendar52"></i><%# (Eval("FechaEnvioSunat", "{0:dd/MM/yyyy}") == "01/01/1900") ? "": Eval("FechaEnvioSunat") %><br />
											<i class="icon-bubble-dots4"></i><%# Eval("MensajeSunat") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
										<ItemTemplate>
											<div style="min-width: 95px;">
												<ul class="icons-list">
													<li class="text-danger" style="width: 35px;">
														<asp:LinkButton ID="lnkVer" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="VerDocumentos" CommandArgument='<%# Eval("IDComunicacionBaja").ToString() %>'><i class="icon-search4"></i></asp:LinkButton>
													</li>
													<li class="text-success" style='<%# (Eval("IDEstadoSunat").ToString() == "2") ? "display:none;": "width: 35px;" %>'>
														<asp:LinkButton ID="lnkEnviarSunat" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Enviar" CommandArgument='<%# Eval("IDComunicacionBaja").ToString() %>'><i class="icon-database-insert"></i></asp:LinkButton>
													</li>
													<li class="text-violet" style="width: 35px;">
														<asp:LinkButton ID="lnkDescargarCDR" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="DescargaXML" CommandArgument='<%# Eval("IDComunicacionBaja").ToString() %>'><i class="icon-cloud-download"></i></asp:LinkButton>
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

	<div id="DatosDetalleDocumentos" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog modal-lg">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Detalle de Documentos</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:UpdatePanel ID="upDetalleDocumentos" runat="server" UpdateMode="Conditional">
					<ContentTemplate>
						<div class="modal-body">
							<div class="table-responsive">
								<asp:GridView ID="gvDetalleLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvDetalleLista_PageIndexChanging" DataKeyNames="IDComunicacionBajaDetalle" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
									<Columns>
										<asp:TemplateField HeaderText="IDResumen" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
											<ItemTemplate>
												<%# Eval("IDResumen") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Numero Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
											<ItemTemplate>
												<%# Eval("NumeroItem") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Serie" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
											<ItemTemplate>
												<%# Eval("SerieNumero") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Motivo Baja" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%">
											<ItemTemplate>
												<%# Eval("MotivoBaja") %>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnDetalleCancelar" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnDetalleCancelar_Click" />
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</div>
	</div>

	<div id="DatosResumen" class="modal fade" tabindex="-1" role="dialog">
		<asp:UpdatePanel ID="upResumen" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<div class="modal-dialog modal-lg">
					<div class="modal-content">
						<div class="modal-header bg-hmodal">
							<h6 class="modal-title">Crear Resumen de Documentas Dados de Baja</h6>
							<button type="button" class="close" data-dismiss="modal">×</button>
						</div>
						<div class="modal-body">
							<asp:Panel ID="pnResumen" runat="server" DefaultButton="btnFacturaBuscar">
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Documentos:</label>
											<asp:DropDownList ID="ddlDocumentos" runat="server">
												<asp:ListItem Value="01">Factura</asp:ListItem>
												<asp:ListItem Value="07">Notas de Credito</asp:ListItem>
												<asp:ListItem Value="08">Notas de Debito</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group has-feedback">
											<label>Fecha Emision:</label>
											<asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
											<div class="form-control-feedback form-control-feedback-sm">
												<i class="icon-calendar"></i>
											</div>
										</div>
									</div>
									<div class="col-md-1">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:Button ID="btnFacturaBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnFacturaBuscar_Click" />
										</div>
									</div>
								</div>
								<div class="table-responsive">
									<asp:GridView ID="gvFacturaLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvFacturaLista_PageIndexChanging" DataKeyNames="Codigo" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
										<Columns>
											<asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<ItemTemplate>
													<asp:CheckBox ID="chkRDSel" runat="server" ClientIDMode="Static" />
													<asp:Label ID="lblIndex" runat="server" Text='<%# ((GridViewRow) Container).RowIndex %>' Style="display: none;"></asp:Label>
													<asp:Label ID="lblIDFacturaBoleta" runat="server" Text='<%# Eval("Codigo") %>' Style="display: none;"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("SerieNumero") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
												<ItemTemplate>
													<%# Eval("FechaEmision") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
												<ItemTemplate>
													<b>Nro.Doc:</b><%# Eval("NumeroDocumentoCliente") %><br />
													<b>Cliente:</b><%# Eval("Cliente") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("TipoMoneda") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("TotalVenta") %>
												</ItemTemplate>
											</asp:TemplateField>

											<asp:TemplateField HeaderText="Motivo Baja" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
												<ItemTemplate>
													<asp:TextBox ID="txtMotivoBaja" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
							</asp:Panel>
						</div>
						<div class="modal-footer">
							<asp:Button ID="btnCerrarResumen" runat="server" Text="Cerrar" SkinID="ui-boton-default" CausesValidation="False" OnClick="btnCerrarResumen_Click" />
							<asp:Button ID="btnGenerarResumen" runat="server" Text="Generar" CausesValidation="False" OnClick="btnGenerarResumen_Click" />
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>


	<div style="display: none;">
		<iframe id="ifrmDescargar" src="" style="border: 0px; padding: 0; width: 0; height: 0;"></iframe>
	</div>

	<script type="text/javascript">
		function Descargar(Codigo, RutaArchivo, NombreArchivo) {
			$('#ifrmDescargar').attr('src', "Descargar.aspx?pCodigo=" + Codigo + "&pTipo=CB&pRutaArchivo=" + RutaArchivo + "&pNombreArchivo=" + NombreArchivo);
		}

		function DescargarCerrar() {
			$('#ifrmDescargar').attr('src', '');
		}

		function ModalRegistroDetalleDocumento() {
			$('#DatosDetalleDocumentos').modal('show');
		}

		function ModalCerrarDetalleDocumento() {
			$('#DatosDetalleDocumentos').modal('hide');
		}

		function ModalRegistroResumen() {
			$('#DatosResumen').modal('show');
		}

		function ModalCerrarResumen() {
			$('#DatosResumen').modal('hide');
		}


	</script>

	<script type="text/javascript">
		function ConfigJS() {

		}
	</script>
</asp:Content>

