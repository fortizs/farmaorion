<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Farmacia.Proceso.Import" %>

<%@ Register Src="~/Controles/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.hide {
			display: none;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Import Masivo</h3>
		</div>
		<div class="panel-body">

			<asp:UpdatePanel ID="upFiltro" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label>Filtro:</label>
								<asp:TextBox ID="txtFiltro" runat="server" />
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label class="etiqueta"></label>
								<asp:LinkButton ID="lbBuscar" runat="server" SkinID="ui-link-boton-default" OnClick="lbBuscar_Click">
                                            <i class="icon-search4 position-left"></i>Buscar
								</asp:LinkButton>
							</div>
						</div>
					</div>
					<div class="espacio"></div>
					<div class="row">
						<div class="col-md-10">
							<div class="form-group">
								<div class="table-responsive">
									<asp:GridView ID="gvEstructura" runat="server" AllowPaging="True" DataKeyNames="IDEstructuraProceso, NombreHoja" EnableSortingAndPagingCallbacks="True" OnSelectedIndexChanged="gvEstructura_SelectedIndexChanged" OnPageIndexChanging="gvEstructura_PageIndexChanging">
										<Columns>
											<asp:BoundField DataField="IDEstructuraProceso" HeaderText="ID">
												<ItemStyle Width="1%" />
											</asp:BoundField>
											<asp:BoundField DataField="Nombre" HeaderText="Nombre Estructura" />
											<asp:BoundField DataField="Extension" HeaderText="Extensión" />
											<asp:TemplateField ShowHeader="False">
												<ItemTemplate>
													<asp:LinkButton ID="lbSeleccionar" Class="text-info-600" runat="server" CausesValidation="False" ToolTip="Seleccionar" CommandName="Select"><span class="icon-select2"></span></asp:LinkButton>
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="50px" />
											</asp:TemplateField>
										</Columns>
										<RowStyle Height="30px" />
									</asp:GridView>
									<asp:HiddenField ID="hfIDEstructuraProcesos" runat="server" Value="0" />
									<asp:HiddenField ID="hfNombreEstructura" runat="server" Value="0" />
									<asp:HiddenField ID="hfExtensionArchivo" runat="server" Value="" />
									<asp:HiddenField ID="hfidTramaLog" runat="server" Value="0" />
									<asp:HiddenField ID="hfNombreHoja" runat="server" Value="0" />
								</div>
							</div>
						</div>
					</div>
					<asp:Panel ID="pnFiltroExport" Visible="true" runat="server">
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label>Estructura:</label>
									<asp:TextBox ID="txtEstructuraSel" runat="server" ReadOnly="true"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<label>Archivo</label>
									<div class="uploader">
										<asp:FileUpload ID="fuCarga" runat="server" Alt="Seleccionar" ClientIDMode="Static" CssClass="file-styled" Title="Seleccionar" onchange="fUploadValue('fusCargaNombreArchivo',this); " />
										<span id="fusCargaNombreArchivo" class="filename" style="-webkit-user-select: none;">Seleccionar...</span><span class="btn btn-default btn-xs" style="-webkit-user-select: none;"><i class="icon-folder-search position-left"></i>Seleccionar</span>
									</div>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnProcesar" runat="server" Text="Procesar" OnClick="btnProcesar_Click" CssClass="hide" />
									<asp:LinkButton ID="blProcesar" runat="server" CssClass="btn btn-default btn-xs" OnClientClick="return Cargando(true);">
                                                <i class="icon-power2 position-left"></i>Procesar
									</asp:LinkButton>
								</div>
							</div>
						</div>
					</asp:Panel>

				</ContentTemplate>
				<Triggers>
					<asp:PostBackTrigger ControlID="btnProcesar" />
					<asp:AsyncPostBackTrigger ControlID="gvEstructura" EventName="SelectedIndexChanged" />
				</Triggers>
			</asp:UpdatePanel>

			<asp:UpdatePanel ID="upTramaLog" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div runat="server" id="cListaArchivosGenerados" visible="false">
						<div class="col-md-12">
							<div class="form-group">
								<h6 class="panel-title text-uppercase"><i class="icon-stack2 position-left"></i>Archivos Cargados</h6>
							</div>
						</div>

						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Archivo:</label>
									<asp:TextBox ID="txtArchivo" runat="server"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Desde:</label>
									<asp:TextBox ID="txtFInicio" ClientIDMode="Static" runat="server" SkinID="ui-textbox-fecha" placeholder="__/__/____" autocomplete="off" MaxLength="10"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Hasta:</label>
									<asp:TextBox ID="txtFFin" ClientIDMode="Static" runat="server" SkinID="ui-textbox-fecha" placeholder="__/__/____" autocomplete="off" MaxLength="10"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Estado:</label>
									<asp:DropDownList ID="ddlEstadoTramaLog" runat="server">
										<asp:ListItem Value="1">Generado</asp:ListItem>
										<asp:ListItem Value="0">Eliminado</asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:LinkButton ID="lnkBuscar" runat="server" SkinID="ui-link-boton-default" OnClick="lnkBuscar_Click">
                                            <i class="icon-search4 position-left"></i>Buscar
									</asp:LinkButton>
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-md-10">
								<div class="form-group">
									<div class="table-responsive">
										<asp:GridView ID="gvTramaLog" runat="server" AllowPaging="True" DataKeyNames="IDTramaLog" EnableSortingAndPagingCallbacks="True" PageSize="6" OnPageIndexChanging="gvTramaLog_PageIndexChanging" OnSelectedIndexChanged="gvTramaLog_SelectedIndexChanged" OnRowCommand="gvTramaLog_RowCommand">
											<Columns>
												<asp:TemplateField HeaderText="ID">
													<ItemTemplate>
														<asp:Label ID="lblIDTramaLog" runat="server" Text='<%# Bind("IDTramaLog") %>'></asp:Label>
													</ItemTemplate>
													<ItemStyle Width="1%" />
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Archivo">
													<ItemTemplate>
														<asp:Label ID="lblNombreArchivo" runat="server" Text='<%# Bind("NombreArchivo") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Generación"></asp:BoundField>

												<asp:BoundField DataField="CantidadI" HeaderText="Cant. Registrados">
													<ItemStyle HorizontalAlign="Center" />
												</asp:BoundField>
												<asp:BoundField DataField="CantidadR" HeaderText="Cant. Rechazados">
													<ItemStyle HorizontalAlign="Center" />
												</asp:BoundField>
												<asp:BoundField DataField="EstadoEvento" HeaderText="Estado Archivo">
													<ItemStyle HorizontalAlign="Center" />
												</asp:BoundField>
												<asp:TemplateField ShowHeader="False">
													<ItemTemplate>
														<div style="width: 100px;">
															<ul class="icons-list">
																<li style='<%# Boolean.Parse(Eval("ArchivoLog").ToString()) ? "": "display:none;" %>' class="text-danger-600" data-popup="tooltip" data-original-title="Ver Resumen Log">
																	<asp:LinkButton ID="lbGenVerResumen" runat="server" CausesValidation="False" CommandName="ResumenLog" CommandArgument='<%# Eval("IDTramaLog") %>'><i class="icon-info22"></i></asp:LinkButton></li>
																<li class="text-info-600" data-popup="tooltip" data-original-title="Ver Archivo Cargado">
																	<asp:LinkButton ID="lbDesCarga" runat="server" CausesValidation="False" CommandName="ArchivoCarga" CommandArgument='<%# Eval("IDTramaLog") %>'><i class="icon-file-download"></i></asp:LinkButton>
																</li>
																<li style='<%# Boolean.Parse(Eval("EliminarCarga").ToString()) ? "": "display:none;" %>' class="text-danger-600" data-popup="tooltip" data-original-title="Anular Carga">
																	<asp:LinkButton ID="lbGenAnular" runat="server" CausesValidation="False" CommandName="Anular" CommandArgument='<%# Eval("IDTramaLog") %>'><i class="icon-blocked"></i></asp:LinkButton></li>
															</ul>
														</div>
													</ItemTemplate>
													<ItemStyle HorizontalAlign="Center" Width="50px" />
												</asp:TemplateField>
											</Columns>
											<RowStyle Height="30px" />
										</asp:GridView>
									</div>
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="gvEstructura" EventName="SelectedIndexChanged" />
					<asp:PostBackTrigger ControlID="gvTramaLog" />
				</Triggers>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="tramacarga" style="display: none;">
		<div class="ui-cargando">
			<div style="width: 40px; height: 40px; position: fixed; bottom: 50%; left: 50%; margin-left: -20px; margin-top: -20px;">
				<svg version="1.1" id="L7" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 100 100" enable-background="new 0 0 100 100" xml:space="preserve">
					<path fill="#f68020" d="M31.6,3.5C5.9,13.6-6.6,42.7,3.5,68.4c10.1,25.7,39.2,38.3,64.9,28.1l-3.1-7.9c-21.3,8.4-45.4-2-53.8-23.3c-8.4-21.3,2-45.4,23.3-53.8L31.6,3.5z" transform="rotate(41.6674 50 50)">
						<animateTransform attributeName="transform" attributeType="XML" type="rotate" dur="2s" from="0 50 50" to="360 50 50" repeatCount="indefinite"></animateTransform>
					</path>
					<path fill="#6e6f73" d="M42.3,39.6c5.7-4.3,13.9-3.1,18.1,2.7c4.3,5.7,3.1,13.9-2.7,18.1l4.1,5.5c8.8-6.5,10.6-19,4.1-27.7c-6.5-8.8-19-10.6-27.7-4.1L42.3,39.6z" transform="rotate(-83.3347 50 50)">
						<animateTransform attributeName="transform" attributeType="XML" type="rotate" dur="1s" from="0 50 50" to="-360 50 50" repeatCount="indefinite"></animateTransform>
					</path>
					<path fill="#d1d2d4" d="M82,35.7C74.1,18,53.4,10.1,35.7,18S10.1,46.6,18,64.3l7.6-3.4c-6-13.5,0-29.3,13.5-35.3s29.3,0,35.3,13.5L82,35.7z" transform="rotate(41.6674 50 50)">
						<animateTransform attributeName="transform" attributeType="XML" type="rotate" dur="2s" from="0 50 50" to="360 50 50" repeatCount="indefinite"></animateTransform>
					</path>
				</svg>
			</div>
		</div>
	</div>

	<script type="text/javascript">

		function ConfigJS() {

		}

		function Cargando(estado) {
			if (estado) {
				$("#tramacarga").show();
				$("#<%= btnProcesar.ClientID %>").click();
			}
			else {
				$("#tramacarga").hide();
			}
		}

		function fUploadValue(txtControlValue, pFUpload) {
			var nArchivo = pFUpload.value;
			//Validar Tipo Archivo----------------  
			var val = false;
			var ext = nArchivo.split('.').pop().toLowerCase();
			if ($.inArray(ext, ['xls', 'xlsx', 'txt', 'csv']) != -1) {
				val = true;
			}

			if (val == true) {
				if (nArchivo === "") {
					nArchivo = "...";
				} else {
					nArchivo = nArchivo.split(/[\/\\]+/);
					nArchivo = nArchivo[(nArchivo.length - 1)];
				}
				$("#" + txtControlValue).text(nArchivo);
			} else {
				pFUpload.value = '';
				$("#" + txtControlValue).text('Seleccionar...');
				Mensaje('warning', 'Archivo no válido <br>(Archivos válidos: .xls, .xlsx, .txt, .csv)');
			}
		}
	</script>
</asp:Content>
