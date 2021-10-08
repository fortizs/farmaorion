<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcaModeloVersion.aspx.cs" Inherits="Farmacia.Configuracion.MarcaModeloVersion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Lista de Marcas, Modelo y Sub Modelo</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label>Origen:</label>
									<asp:DropDownList ID="ddlOrigen" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrigen_SelectedIndexChanged"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Marca:</label>
									<asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Modelo:</label>
									<asp:DropDownList ID="ddlModelo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelo_SelectedIndexChanged"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-2">
								<div class="form-group">
									<label>Sub Modelo:</label>
									<asp:DropDownList ID="ddlSubModelo" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-1">
								<div class="form-group">
									<label class="etiqueta"></label>
									<asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100%" OnClick="btnBuscar_Click" />
								</div>
							</div>
						</div>
					</asp:Panel>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="ddlOrigen" EventName="SelectedIndexChanged" />
					<asp:AsyncPostBackTrigger ControlID="ddlMarca" EventName="SelectedIndexChanged" />
					<asp:AsyncPostBackTrigger ControlID="ddlModelo" EventName="SelectedIndexChanged" />
					<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
				</Triggers>
			</asp:UpdatePanel>
			<div class="espacio"></div>

			<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
						<i class="icon-car position-left"></i>
						Marca</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
						<i class="icon-car position-left"></i>
						Modelo</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="border-profile-tab2" data-toggle="tab" href="#tab3" role="tab" aria-controls="border-profile" aria-selected="false">
						<i class="icon-car position-left"></i>
						Sub Modelo</a>
				</li>
			</ul>
			<div class="tab-content mb-4" id="border-tabsContent">
				<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upDatosMarca" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnDatosMarca" runat="server">
								<asp:GridView ID="gvListaMarca" runat="server" DataKeyNames="IDMarca" OnRowEditing="gvListaMarca_RowEditing" OnRowCancelingEdit="gvListaMarca_RowCancelingEdit" OnRowUpdating="gvListaMarca_RowUpdating" OnRowDeleting="gvListaMarca_RowDeleting" OnPageIndexChanging="gvListaMarca_PageIndexChanging">
									<RowStyle Height="36" VerticalAlign="Middle" />
									<Columns>
										<asp:TemplateField HeaderText="ID" ItemStyle-Width="45" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<asp:Label ID="lblIDMarca" runat="server" Text='<%# Bind("IDMarca") %>'></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Label1" runat="server" Text='<%# Bind("IDMarca") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Origen" ItemStyle-Width="150">
											<EditItemTemplate>
												<asp:DropDownList ID="ddlOrigen" runat="server"></asp:DropDownList>
												<asp:Label ID="lblOrigen" runat="server" Text='<%# Bind("Origen") %>' Visible="false"></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Label2" runat="server" Text='<%# Bind("Origen") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Marca">
											<EditItemTemplate>
												<asp:TextBox ID="txtMarca" runat="server" MaxLength="50" Text='<%# Bind("Marca") %>'></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblMarca" runat="server" Text='<%# Bind("Marca") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="51">
											<EditItemTemplate>
												<asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' />
											</EditItemTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Estado") %>' Enabled="false" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-Width="85" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="lbCancelar" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancelar"><span class="icon-reset"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="lbGuardar" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" ToolTip="Guardar" CommandName="Update"><span class=" icon-floppy-disk"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</EditItemTemplate>
											<ItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="lbNuevo" CssClass="btn btn-active btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Nuevo" Visible='<%# Eval("IDMarca").ToString() == "0" ? true : false %>'><span class="icon-plus-circle2"></span></asp:LinkButton>
															<asp:LinkButton ID="lbEditar" CssClass="btn btn-default btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Editar" Visible='<%# Eval("IDMarca").ToString() == "0" ? false : true %>'><span class="icon-pencil7"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="lbEliminar" CssClass="btn btn-default btn-lg" runat="server" OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que desea eliminar la Marca?\",\"Marca: {0}\"); return false;", Eval("Marca")) %>' CausesValidation="False" ToolTip="Eliminar" CommandName="Delete" CommandArgument='<%# Bind("IDMarca") %>' Visible='<%# Eval("IDMarca").ToString() == "0" ? false : true %>'><span class="icon-trash"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</asp:Panel>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>
				</div>
				<div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upDatosModelo" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnDatosModelo" runat="server">
								<asp:GridView ID="gvListaModelo" runat="server" DataKeyNames="IDModelo" OnRowEditing="gvListaModelo_RowEditing" OnRowCancelingEdit="gvListaModelo_RowCancelingEdit" OnRowUpdating="gvListaModelo_RowUpdating" OnRowDeleting="gvListaModelo_RowDeleting" OnPageIndexChanging="gvListaModelo_PageIndexChanging">
									<RowStyle Height="36" VerticalAlign="Middle" />
									<Columns>
										<asp:TemplateField HeaderText="ID" ItemStyle-Width="45" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<asp:Label ID="lblIDModelo" runat="server" Text='<%# Bind("IDModelo") %>'></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblIDModelo" runat="server" Text='<%# Bind("IDModelo") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Marca" ItemStyle-Width="150">
											<EditItemTemplate>
												<asp:DropDownList ID="ddlMarca" runat="server"></asp:DropDownList>
												<asp:Label ID="lblIDMarca" runat="server" Text='<%# Bind("IDMarca") %>' Visible="false"></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblMarca" runat="server" Text='<%# Bind("Marca") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Modelo">
											<EditItemTemplate>
												<asp:TextBox ID="txtModelo" runat="server" MaxLength="50" Text='<%# Bind("Modelo") %>'></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblModelo" runat="server" Text='<%# Bind("Modelo") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Tipo" ItemStyle-Width="150">
											<EditItemTemplate>
												<asp:DropDownList ID="ddlTipoVehiculo" runat="server"></asp:DropDownList>
												<asp:Label ID="lblIDTipoVehiculo" runat="server" Text='<%# Bind("IDTipoVehiculo") %>' Visible="false"></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblTipoVehiculo" runat="server" Text='<%# Bind("TipoVehiculo") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Asie." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
											<EditItemTemplate>
												<asp:TextBox ID="txtNumeroAsientos" runat="server" MaxLength="50" Text='<%# Bind("NumeroAsientos") %>'></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblNumeroAsientos" runat="server" Text='<%# Bind("NumeroAsientos") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="51">
											<EditItemTemplate>
												<asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' />
											</EditItemTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' Enabled="false" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-Width="85" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton1" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancelar"><span class="icon-reset"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton2" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" ToolTip="Guardar" CommandName="Update"><span class=" icon-floppy-disk"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</EditItemTemplate>
											<ItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton3" CssClass="btn btn-active btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Nuevo" Visible='<%# Eval("IDModelo").ToString() == "0" ? true : false %>'><span class="icon-plus-circle2"></span></asp:LinkButton>
															<asp:LinkButton ID="LinkButton4" CssClass="btn btn-default btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Editar" Visible='<%# Eval("IDModelo").ToString() == "0" ? false : true %>'><span class="icon-pencil7"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton5" CssClass="btn btn-default btn-lg" runat="server" OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que desea eliminar la Modelo?\",\"Modelo: {0}\"); return false;", Eval("Modelo")) %>' CausesValidation="False" ToolTip="Eliminar" CommandName="Delete" CommandArgument='<%# Bind("IDModelo") %>' Visible='<%# Eval("IDModelo").ToString() == "0" ? false : true %>'><span class="icon-trash"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</asp:Panel>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>
				</div>
				<div class="tab-pane fade" id="tab3" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upDatosModeloVersion" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:Panel ID="pnDatosModeloVersion" runat="server">
								<asp:GridView ID="gvListaModeloVersion" runat="server" DataKeyNames="IDModeloVersion" OnRowEditing="gvListaModeloVersion_RowEditing" OnRowCancelingEdit="gvListaModeloVersion_RowCancelingEdit" OnRowUpdating="gvListaModeloVersion_RowUpdating" OnRowDeleting="gvListaModeloVersion_RowDeleting" OnPageIndexChanging="gvListaModeloVersion_PageIndexChanging">
									<RowStyle Height="36" VerticalAlign="Middle" />
									<Columns>
										<asp:TemplateField HeaderText="ID" ItemStyle-Width="45" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<asp:Label ID="lblIDModeloVersion" runat="server" Text='<%# Bind("IDModeloVersion") %>'></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblIDModeloVersion" runat="server" Text='<%# Bind("IDModeloVersion") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Marca" ItemStyle-Width="150">
											<EditItemTemplate>
												<asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlListaModeloVersionMarca_SelectedIndexChanged"></asp:DropDownList>
												<asp:Label ID="lblIDMarca" runat="server" Text='<%# Bind("IDMarca") %>' Visible="false"></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblMarca" runat="server" Text='<%# Bind("Marca") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Modelo" ItemStyle-Width="150">
											<EditItemTemplate>
												<asp:DropDownList ID="ddlModelo" runat="server"></asp:DropDownList>
												<asp:Label ID="lblIDModelo" runat="server" Text='<%# Bind("IDModelo") %>' Visible="false"></asp:Label>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblModelo" runat="server" Text='<%# Bind("Modelo") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Sub Modelo">
											<EditItemTemplate>
												<asp:TextBox ID="txtModeloVersion" runat="server" MaxLength="50" Text='<%# Bind("ModeloVersion") %>'></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="lblModeloVersion" runat="server" Text='<%# Bind("ModeloVersion") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="51">
											<EditItemTemplate>
												<asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' />
											</EditItemTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' Enabled="false" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-Width="85" ItemStyle-HorizontalAlign="Center">
											<EditItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton6" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancelar"><span class="icon-reset"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton7" CssClass="btn btn-active btn-lg" runat="server" CausesValidation="False" ToolTip="Guardar" CommandName="Update"><span class=" icon-floppy-disk"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</EditItemTemplate>
											<ItemTemplate>
												<div style="width: 75px;">
													<ul class="icons-list">
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton8" CssClass="btn btn-active btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Nuevo" Visible='<%# Eval("IDModeloVersion").ToString() == "0" ? true : false %>'><span class="icon-plus-circle2"></span></asp:LinkButton>
															<asp:LinkButton ID="LinkButton9" CssClass="btn btn-default btn-lg" runat="server" CommandName="Edit" CausesValidation="False" ToolTip="Editar" Visible='<%# Eval("IDModeloVersion").ToString() == "0" ? false : true %>'><span class="icon-pencil7"></span></asp:LinkButton>
														</li>
														<li style="width: 30px">
															<asp:LinkButton ID="LinkButton10" CssClass="btn btn-default btn-lg" runat="server" OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que desea eliminar la ModeloVersion?\",\"ModeloVersion: {0}\"); return false;", Eval("ModeloVersion")) %>' CausesValidation="False" ToolTip="Eliminar" CommandName="Delete" CommandArgument='<%# Bind("IDModeloVersion") %>' Visible='<%# Eval("IDModeloVersion").ToString() == "0" ? false : true %>'><span class="icon-trash"></span></asp:LinkButton>
														</li>
													</ul>
												</div>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</asp:Panel>
						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
						</Triggers>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<style type="text/css">
		.table .form-control {
			height: 25px;
			padding: 3px;
			border-radius: 3px;
		}

		.table input[type=text].form-control {
			padding-left: 5px;
		}
	</style>
	<script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>
	<script type="text/javascript">
		function ConfigJS() {
			$("#<%= ddlOrigen.ClientID %>").select2();
			$("#<%= ddlMarca.ClientID %>").select2();
			$("#<%= ddlModelo.ClientID %>").select2();
			$("#<%= ddlSubModelo.ClientID %>").select2();
		}
		 
	</script>
</asp:Content>
