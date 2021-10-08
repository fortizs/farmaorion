<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="Farmacia.Almacen.Inventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Inventario Físico</h3>
		</div>
		<div class="panel-body">
			<ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
						<i class="icon-stack3 position-left"></i>
						Consulta</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
						<i class="icon-pencil7 position-left"></i>
						Registro</a>
				</li>
			</ul>
			<div class="tab-content mb-4" id="border-tabsContent">
				<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="row">
								<div class="col-md-2">
									<div class="form-group">
										<label>Almacen:</label>
										<asp:DropDownList ID="ddlBIDAlmacen" runat="server"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-6">
									<div class="form-group">
										<label>Filtro <strong>(Nro Inventario)</strong>:</label>
										<asp:TextBox ID="txtBFiltro" runat="server"></asp:TextBox>
									</div>
								</div>
								<div class="col-md-1">
									<div class="form-group">
										<label class="etiqueta"></label>
										<asp:Button ID="btnBuscar" Width="100%" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
									</div>
								</div>
							</div>
							<div class="table-responsive">
								<asp:GridView ID="gvInventarioFisicoListar" runat="server" DataKeyNames="IDInventarioFisico" Width="100%" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False" OnPageIndexChanging="gvInventarioFisicoListar_PageIndexChanging" OnRowCommand="gvInventarioFisicoListar_RowCommand">
									<Columns>
										<asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
											<ItemTemplate>
												<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Almacen" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
											<ItemTemplate>
												<%# Eval("Almacen") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Número Inventario" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("NumeroInventarioFormato") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Fecha Inventario" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<%# Eval("FechaInventario", "{0:dd/MM/yyyy}") %>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:CheckBoxField DataField="Procesado" HeaderText="Procesado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
											<HeaderStyle HorizontalAlign="Center" />
											<ItemStyle HorizontalAlign="Center" Width="5%" />
										</asp:CheckBoxField>
										<asp:TemplateField HeaderText="Acciones" ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
											<ItemTemplate>
												<div style="min-width: 75px;">
													<ul class="icons-list">
														<li class="text-primary" title="Editar">
															<asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CausesValidation="False" CommandArgument='<%# Eval("IDInventarioFisico").ToString() %>' SkinID="ui-link-boton-primario"><span class="icon-pencil"></span></asp:LinkButton>
														</li>
														<li class="text-danger" title="Procesar Ajuste" style='<%# (Boolean.Parse(Eval("Procesado").ToString())) ? "display:none;": "" %>'>
															<asp:LinkButton ID="lnkProcesar" runat="server" CommandName="Procesar" CausesValidation="False" CommandArgument='<%# Eval("IDInventarioFisico").ToString() %>' SkinID="ui-link-boton-success" OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas realizar el proceso de Ajuste del Inventario?\",\"{0}\"); return false;", "")%>'><i class="icon-spinner4"></i></asp:LinkButton>
														</li>
													</ul>
												</div>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
				<div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
					<asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<asp:HiddenField ID="hdfIDInventarioFisico" runat="server" Value="0" />
							<asp:HiddenField ID="hdfIDInventarioFisicoDetalle" runat="server" Value="0" />
							<asp:Panel ID="pnRegistro" runat="server">
								<div class="row">
									<div class="col-md-2">
										<div class="form-group">
											<label>Número Inventario:</label>
											<asp:TextBox ID="txtRegNumeroInventario" runat="server" SkinID="ui-textbox-requerido" Enabled="false"></asp:TextBox>
										</div>
									</div>
									<div class="col-md-4">
										<div class="form-group">
											<label>Almacen:</label>
											<asp:DropDownList ID="ddlRegIDAlmacen" SkinID="ui-dropdownlist-requerido" runat="server"></asp:DropDownList>
										</div>
									</div>
									<div class="col-md-2">
										<div class="form-group">
											<label>Fecha Inventario:</label>
											<asp:TextBox ID="txtRegFechaInventario" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-8">
										<div class="form-group">
											<label>Observación:</label>
											<asp:TextBox ID="txtRegObservacion" SkinID="ui-textbox-requerido" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
										</div>
									</div>
								</div>
							</asp:Panel>

							<asp:Panel Enabled="true" runat="server" ID="pnDetalleListar"> 
								<div class="separador"></div>
								<div class="table-responsive" style="overflow-y: scroll; max-height: 800px;">
									<asp:LinkButton ID="lnkImprimirConteo" runat="server" CssClass="btn btn-default" OnClick="lnkImprimirConteo_Click" Visible="false"><i class="glyphicon glyphicon-floppy-saved"></i> Imprimir Conteo </asp:LinkButton>
									<asp:LinkButton ID="lnkImprimirDiferencia" runat="server" CssClass="btn btn-default" OnClick="lnkImprimirDiferencia_Click" Visible="false"><i class="glyphicon glyphicon-floppy-saved"></i> Imprimir Diferencias </asp:LinkButton>
									<asp:HiddenField ID="hdfProcesado" runat="server" Value="false" />
									<asp:GridView ID="gvInventarioFisicoDetalleListar" AllowPaging="false" runat="server" DataKeyNames="IDInventarioFisicoDetalle" AutoGenerateColumns="False" Width="100%" GridLines="None" OnSelectedIndexChanged="gvInventarioFisicoDetalleListar_SelectedIndexChanged">
										<Columns>
											<asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
												<ItemTemplate>
													<%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
													<asp:Label ID="lblIDInventarioFisicoDetalle" runat="server" Text='<%# Eval("IDInventarioFisicoDetalle") %>' Visible="false"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
												<ItemTemplate>
													<%# Eval("Categoria") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("Codigo") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Producto/Lote" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%">
												<ItemTemplate>
													<%# Eval("Nombre") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Unidad Medida" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
												<ItemTemplate>
													<%# Eval("UnidadMedida") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Stock Actual" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
												<ItemTemplate>
													<asp:HiddenField ID="hfStock" runat="server" Value='<%# Eval("StockActual") %>' />
													<%# Eval("StockActual") %>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Ingreso Conteo" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
												<ItemTemplate>
													<asp:TextBox ID="txtIngresoConteo" runat="server" SkinID="ui-textbox-number-requerido" Text='<%# Eval("IngresoConteo") %>'></asp:TextBox>
													<div style="width: 40px; display: none">
														<ul class="icons-list">
															<li style="width: 35px">
																<asp:LinkButton ID="lbGrabar" SkinID="ui-link-boton-info" runat="server" CausesValidation="False" CommandName="Select"><span class="fa fa-save"></span></asp:LinkButton>
															</li>
														</ul>
													</div>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
							</asp:Panel>
							<br />
							<div class="row text-right">
								<div class="col-md-12">
									<div class="form-group">
										<asp:LinkButton ID="lnkNuevoInventarioFisico" runat="server" CssClass="btn btn-success" OnClick="lnkNuevoInventarioFisico_Click"><i class="fa fa-hand-paper-o"></i> Nuevo </asp:LinkButton>
										<asp:LinkButton ID="lnkGuardarInventarioFisico" runat="server" CssClass="btn btn-primary" OnClick="lnkGuardarInventarioFisico_Click"><i class="glyphicon glyphicon-floppy-saved"></i> Guardar </asp:LinkButton>
									</div>
								</div>
							</div>
							<asp:LinkButton ID="lnkGuardarTodo" Visible="false" runat="server" SkinID="ui-link-boton-success" OnClick="lnkGuardarTodo_Click">Guardar Todo</asp:LinkButton>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</div>

	<asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />

	<script type="text/javascript">

		function ConfigJS() {

		}

		function Imprimir(ID, Tipo) {
			$("#ModalImprimir iframe").attr("src", "");
			$("#ModalImprimir iframe").attr("src", 'ImprimirDocumento.ashx?ID=' + ID + '&Tipo=' + Tipo);
			$("#ModalImprimir").modal();
			return false;

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

