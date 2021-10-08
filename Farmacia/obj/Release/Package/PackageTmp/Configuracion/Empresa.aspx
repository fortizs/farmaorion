<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" ValidateRequest="false" CodeBehind="Empresa.aspx.cs" Inherits="Farmacia.Configuracion.Empresa" %>

<%@ Register Src="~/Controles/ccBuscarUbigeo.ascx" TagPrefix="uc1" TagName="ccBuscarUbigeo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.Ocultar {
			display: none;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Información de la Empresa</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upRegistroEmpresa" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hdfIDEmpresa" runat="server" Value="0" />
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Número Documento:</label>
								<asp:TextBox ID="txtRegNumeroDocumento" SkinID="ui-textbox-requerido" runat="server" MaxLength="11"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label for="tags">Razon Social:</label>
								<asp:TextBox ID="txtRegRazonSocial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Nombre Comercial:</label>
								<asp:TextBox ID="txtRegNombreComercial" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label for="tags">Correo:</label>
								<asp:TextBox ID="txtRegCorreo" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Clave Correo Emisor:</label>
								<asp:TextBox ID="txtRegClaveCorreoEmisor" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-4">
							<div class="form-group">
								<label for="tags">Ubigeo:</label>
								<div class="input-group">
									<asp:HiddenField ID="hdfRegIDUbigeo" runat="server" Value="0" />
									<asp:TextBox ID="txtRegUbigeo" SkinID="ui-textbox-requerido" Enabled="false" runat="server"></asp:TextBox>
									<span class="input-group-btn">
										<button class="btn btn-primary" type="button" onclick="BuscarUbigeo();"><i class="icon-search4"></i></button>
									</span>
								</div>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Urbanización:</label>
								<asp:TextBox ID="txtRegUrbanizacion" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<label for="tags">Dirección:</label>
								<asp:TextBox ID="txtRegDireccion" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2" style="display: none;">
							<div class="form-group">
								<label class="etiqueta">Color de Interfaz</label>
								<asp:DropDownList ID="ddlIDTema" runat="server" SkinID="ui-dropdownlist-requerido">
									<asp:ListItem Value="1">Amarrillo</asp:ListItem>
									<asp:ListItem Value="2">Anaranjado</asp:ListItem>
									<asp:ListItem Value="3">Celeste</asp:ListItem>
									<asp:ListItem Value="4">Rojo</asp:ListItem>
									<asp:ListItem Value="5">Verde</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">ClaveCertificado:</label>
								<asp:TextBox ID="txtRegClaveCertificado" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Usuario Sol:</label>
								<asp:TextBox ID="txtRegUsuarioSol" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label for="tags">Clave Sol:</label>
								<asp:TextBox ID="txtRegClaveSol" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta">SMTP</label>
								<asp:DropDownList ID="ddlIDEmailSMTP" runat="server" SkinID="ui-dropdownlist-requerido">
									<asp:ListItem Value="1">GMAIL</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta">Sunat</label>
								<asp:DropDownList ID="ddlIDSunat" runat="server" SkinID="ui-dropdownlist-requerido">
									<asp:ListItem Value="1">DESARROLLO</asp:ListItem>
									<asp:ListItem Value="2">PRODUCCIÓN</asp:ListItem>
								</asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label class="etiqueta">Código Establecimiento:</label>
								<asp:TextBox ID="txtCodigoEstablecimiento" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<br />
					<div class="row text-right">
						<div class="col-md-12">
							<asp:Button ID="btnResetearEmpresa" runat="server" Text="Resetear Empresa" SkinID="ui-boton-default" TabIndex="0" OnClick="btnResetearEmpresa_Click" />
							<asp:Button ID="btnGuardar" runat="server" Text="Grabar Empresa" TabIndex="0" OnClick="btnGuardar_Click" />
						</div>
					</div>
					<!-- /.content -->
				</ContentTemplate>
			</asp:UpdatePanel>
			<hr />
			<!-- Nav tabs -->
			<asp:UpdatePanel ID="upDetalle" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnDetalle" runat="server">
						<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
							<li class="nav-item">
								<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
									<i class="icon-office position-left"></i>
									Sucursales</a>
							</li>
							<li class="nav-item">
								<a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
									<i class="icon-newspaper position-left"></i>
									Serie-Documentos</a>
							</li>
							<li class="nav-item">
								<a class="nav-link" id="border-profile-tab2" data-toggle="tab" href="#tab3" role="tab" aria-controls="border-profile" aria-selected="false">
									<i class="icon-cog52 position-left"></i>
									Configuraciones</a>
							</li>
							<li class="nav-item">
								<a class="nav-link" id="border-profile-tab4" data-toggle="tab" href="#tab4" role="tab" aria-controls="border-profile" aria-selected="false">
									<i class="icon-cloud-upload position-left"></i>
									Archivos
								</a>
							</li>
							<li class="nav-item">
								<a class="nav-link" id="border-profile-tab5" data-toggle="tab" href="#tab5" role="tab" aria-controls="border-profile" aria-selected="false">
									<i class="icon-design position-left"></i>
									Cotización
								</a>
							</li>
						</ul>
						<div class="tab-content mb-4" id="border-tabsContent">
							<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
								<asp:UpdatePanel ID="upSucursal" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />
										<div class="row text-right">
											<div class="col-md-12">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnNuevoSucursal" runat="server" Text="Nuevo" OnClick="btnNuevoSucursal_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvListarSucursal" runat="server" AllowPaging="True" Width="100%" OnSelectedIndexChanged="gvListarSucursal_SelectedIndexChanged" OnRowCommand="gvListarSucursal_RowCommand" DataKeyNames="IDSucursal" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
														<ItemTemplate>
															<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="20%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Celular" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Celular") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Email" ItemStyle-Width="15%">
														<ItemTemplate>
															<%# Eval("Email") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Dirección" ItemStyle-Width="25%">
														<ItemTemplate>
															<%# Eval("Direccion") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
														<ItemTemplate>
															<div style="width: 75px;">
																<ul class="icons-list">
																	<li style="width: 35px">
																		<asp:LinkButton ID="LinkButton1" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
																	</li>
																	<li style="width: 35px" title="Eliminar">
																		<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDSucursal").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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
								</asp:UpdatePanel>

							</div>
							<div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
								<asp:UpdatePanel ID="upDocumentoSerie" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:HiddenField ID="hfIDDocumentoSerie" runat="server" Value="0" />
										<div class="row">
											<div class="col-md-2">
												<div class="form-group">
													<label>Sucursal:</label>
													<asp:DropDownList ID="ddlDSIDSucursal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDSIDSucursal_SelectedIndexChanged"></asp:DropDownList>
												</div>
											</div>
											<div class="col-md-2">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnNuevoDocumentoSerie" runat="server" Text="Nuevo" OnClick="btnNuevoDocumentoSerie_Click" />
												</div>
											</div>
										</div>
										<div class="table-responsive">
											<asp:GridView ID="gvDocumentoSerieLista" runat="server" AllowPaging="True" Width="100%" OnSelectedIndexChanged="gvDocumentoSerieLista_SelectedIndexChanged" OnPageIndexChanging="gvDocumentoSerieLista_PageIndexChanging" OnRowCommand="gvDocumentoSerieLista_RowCommand" DataKeyNames="IDDocumentoSerie" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
												<Columns>
													<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
														<ItemTemplate>
															<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="20%">
														<ItemTemplate>
															<%# Eval("Sucursal") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Tipo Comprobante" ItemStyle-Width="20%">
														<ItemTemplate>
															<%# Eval("TipoComprobante") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Serie" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Serie") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Numero" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("Numero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Serie Actual" ItemStyle-Width="10%">
														<ItemTemplate>
															<%# Eval("SerieNumero") %>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
														<ItemTemplate>
															<div style="width: 75px;">
																<ul class="icons-list">
																	<li style="width: 35px">
																		<asp:LinkButton ID="lnkEditarDocumentoSerie" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
																	</li>
																	<li style="width: 35px" title="Eliminar">
																		<asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDDocumentoSerie").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
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
								</asp:UpdatePanel>

							</div>

							<div class="tab-pane fade show" id="tab3" role="tabpanel" aria-labelledby="border-home-tab">
								<asp:UpdatePanel ID="upEmpresaConfig" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<div class="row">
											<div class="col-md-4">
												<div class="form-group">
													<label>Salida de Almacen:</label>
													<asp:DropDownList ID="ddlSalidaAlmacen" runat="server">
														<asp:ListItem Value="AU">Automático</asp:ListItem>
														<asp:ListItem Value="MA">Manual</asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-md-4">
												<div class="form-group">
													<label>Ingreso de Almacen:</label>
													<asp:DropDownList ID="ddlIngresoAlmacen" runat="server">
														<asp:ListItem Value="AU">Automático</asp:ListItem>
														<asp:ListItem Value="MA">Manual</asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-md-4">
												<div class="form-group">
													<label>Impresión de Ventas:</label>
													<asp:DropDownList ID="ddlImpresionVenta" runat="server">
														<asp:ListItem Value="TK">Ticket</asp:ListItem>
														<asp:ListItem Value="A4">Documento A4</asp:ListItem>
														<asp:ListItem Value="A4Moto">A4 Moto</asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>
										</div>
										<div class="row text-right">
											<div class="col-md-4">
												<asp:Button ID="btnGuardarConfig" runat="server" Text="Grabar Configuración" TabIndex="0" OnClick="btnGuardarConfig_Click" />
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>

							<div class="tab-pane fade show" id="tab4" role="tabpanel" aria-labelledby="border-home-tab">
								<asp:UpdatePanel ID="upEmpresaArchivos" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<asp:HiddenField ID="hdfIDEmpresaArchivo" runat="server" Value="0" />
										<div class="row text-right">
											<div class="col-md-8">
												<div class="form-group">
													<label class="etiqueta"></label>
													<asp:Button ID="btnBuscarEmpresaArchivo" runat="server" CssClass="Ocultar" Text="Buscar" OnClick="btnBuscarEmpresaArchivo_Click" />
													<asp:Button ID="btnNuevoArchivo" runat="server" Text="Nuevo" OnClick="btnNuevoArchivo_Click" />
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-md-8">
												<div class="table-responsive">
													<asp:GridView ID="gvEmpresaArchivos" runat="server" AllowPaging="True" Width="100%" OnRowCommand="gvEmpresaArchivos_RowCommand" DataKeyNames="IDArchivoEmpresa" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
														<Columns>
															<asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
																<ItemTemplate>
																	<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Tipo Archivo" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("TipoArchivo") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Ruta Archivo" ItemStyle-Width="10%">
																<ItemTemplate>
																	<%# Eval("RutaArchivo") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Nombre Archivo" ItemStyle-Width="15%">
																<ItemTemplate>
																	<%# Eval("NombreArchivo") %>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField ShowHeader="False" HeaderText="Acciones" ItemStyle-Width="10%">
																<ItemTemplate>
																	<div style="width: 75px;">
																		<ul class="icons-list">
																			<li style="width: 35px" title="Eliminar">
																				<asp:LinkButton ID="lnkEliminarArchivo" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" Visible="true" CommandName="Eliminar" CommandArgument='<%# Eval("IDArchivoEmpresa").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este registro?\",\"{0}\"); return false;", "")%>'><i class="icon-trash"></i></asp:LinkButton>
																			</li>
																			<li style="width: 35px" title="Descargar">
																				<a href='<%# String.Format("{0}/{1}", ConfigurationManager.AppSettings["RutaArchivosWeb"].ToString(), Eval("RutaArchivo").ToString() + Eval("NombreArchivo").ToString()) %>' target="_blank" class="btn btn-primary btn-xs" style='<%# Eval("RutaArchivo").ToString().Length > 0 ? "display: block;": "display: none;" %>'>
																					<i class="icon-download"></i>
																				</a>
																			</li>
																		</ul>
																	</div>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Center" />
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
												</div>
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>

							<div class="tab-pane fade show" id="tab5" role="tabpanel" aria-labelledby="border-home-tab">
								<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
									<ContentTemplate>
										<div class="row">
											<div class="col-md-12">
												<div class="form-group">
													<label>Terminos y Condiciones:</label>
													<asp:TextBox ID="txtTerminoCondicion" runat="server" CssClass="summernote" TextMode="MultiLine" Rows="3"></asp:TextBox>
												</div>
											</div>
										</div>
										<div class="row text-right">
											<div class="col-md-12">
												<asp:Button ID="btnGuardarTerminoCondicion" runat="server" Text="Guardar" TabIndex="0" OnClick="btnGuardarTerminoCondicion_Click" />
											</div>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div> 
						</div>
					</asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel> 
		</div>
	</div>

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
							<uc1:ccBuscarUbigeo runat="server" ID="ccBuscarUbigeo" />
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
				<div class="modal-footer">
				</div>
			</div>
		</div>
	</div>

	<div id="DatosSucursal" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos de Sucursal</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upRegistroSucursal" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">

								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label>Nombre:</label>
											<asp:TextBox ID="txtSuNombre" SkinID="ui-textbox-requerido" runat="server" MaxLength="100"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Teléfono:</label>
											<asp:TextBox ID="txtSuTelefono" runat="server" MaxLength="100"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Celular:</label>
											<asp:TextBox ID="txtSuCelular" runat="server" MaxLength="100"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Email:</label>
											<asp:TextBox ID="txtSuEmail" runat="server" MaxLength="100"></asp:TextBox>
										</div>
									</div>
								</div>

								<div class="row">
									<div class="col-md-4" style="display: none;">
										<div class="form-group">
											<label for="tags">Ubigeo:</label>
											<div class="input-group">
												<asp:HiddenField ID="hdfSuIDUbigeo" runat="server" Value="0" />
												<asp:TextBox ID="txtSuUbigeo" Enabled="false" runat="server"></asp:TextBox>
												<span class="input-group-btn">
													<button class="btn btn-primary" type="button" onclick="BuscarUbigeo();"><i class="icon-search4"></i></button>
												</span>
											</div>
										</div>
									</div>
									<div class="col-md-12">
										<div class="form-group">
											<label for="tags">Dirección:</label>
											<asp:TextBox ID="txtSuDireccion" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarSucursal" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosSucursal')" />
								<asp:Button ID="btnGuardarSucursal" runat="server" Text="Guardar" OnClick="btnGuardarSucursal_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>

	<div id="DatosDocumentoSerie" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header bg-hmodal">
					<h6 class="modal-title">Datos de Documento Serie</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<asp:Panel ID="Panel1" runat="server" DefaultButton="btnGuardar">
					<asp:UpdatePanel ID="upRegistroDocumentoSerie" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-body">

								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label>Sucursal:</label>
											<asp:DropDownList ID="ddlIDSucursal" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label>Tipo Comprobante:</label>
											<asp:DropDownList ID="ddlIDTipoComprobante" runat="server"></asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-3">
										<div class="form-group">
											<label>Serie:</label>
											<asp:TextBox ID="txtSerie" runat="server" MaxLength="4"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-3">
										<div class="form-group">
											<label>Número:</label>
											<asp:TextBox ID="txtNumero" runat="server" MaxLength="6"></asp:TextBox>
										</div>
									</div>
								</div>
							</div>
							<div class="modal-footer">
								<asp:Button ID="btnCancelarDocumentoSerie" runat="server" Text="Cancelar" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('DatosDocumentoSerie')" />
								<asp:Button ID="btnGuardarDocumentoSerie" runat="server" Text="Guardar" OnClick="btnGuardarDocumentoSerie_Click" />
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</asp:Panel>

			</div>
		</div>
	</div>


	<div id="ModalArchivos" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<h6 class="modal-title">Cargar Archivos</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<iframe id="ifrmImagenCargar" src="" style="width: 100%; height: 300px; border: none;"></iframe>
				</div>
			</div>
		</div>
	</div>
	 
	<!-- include summernote css/js -->
	<link href="https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/summernote.min.css" rel="stylesheet">
	<script src="https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/summernote.min.js"></script>
	 
	<script type="text/javascript">

		function ConfigJS() {
			ConfigAuxiliarEditor();

		}

		function ConfigAuxiliarEditor() {
			$('#cphPrincipal_txtTerminoCondicion').summernote({
				toolbar: [
                    ['style', ['bold', 'italic', 'underline', 'clear']],
				    ['fontname', ['fontname']],
                    ['fontsize', ['fontsize']],
                    ['para', ['ul', 'ol', 'paragraph']],
					['height', ['height']],
					['color', ['color']],
					['insert', ['link', 'picture', 'video']],
				    ['table', ['table']]
				],
				fontSizes: ['8', '9', '10', '12', '14', '16', '24', '36', '48', '64', '82', '150'],
				height: '350px'
			});
			$('.note-editable').css("font-size", "10px");

		}

		function BuscarUbigeo() {
			gBuscarUbigeo('cphPrincipal_hdfRegIDUbigeo', 'cphPrincipal_txtRegUbigeo');
		}

		function ModalImagen() {
			var IDEmpresa = $("#<%= hdfIDEmpresa.ClientID %>").val();
			console.log("IDEmpresa =" + IDEmpresa);
			$("#ifrmImagenCargar").attr("src", "pEmpresaArchivo.aspx?pIDElemento=" + IDEmpresa);
			$('#ModalArchivos').modal('show');
			return false;
		}

		function CerrarModalImagen() {
			$('#ModalArchivos').modal('hide');
			$("#cphPrincipal_btnBuscarEmpresaArchivo").click();
			Mensaje("confirmation", "La operación se realizó con éxito!");
			return false;
		}
		 
	</script>

</asp:Content>
