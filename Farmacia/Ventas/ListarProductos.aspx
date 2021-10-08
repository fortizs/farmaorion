<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ListarProductos.aspx.cs" Inherits="Farmacia.Ventas.ListarProductos" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		fieldset {
			padding: 10px 15px 15px 15px;
			border-radius: 5px;
			border: 1px solid #ddd;
		}

		fieldset {
			margin: 0;
			min-width: 0;
		}

			fieldset:first-child legend:first-child {
				padding-top: 0;
			}

		legend {
			padding: 10px;
			border-color: #eee;
		}

		legend {
			font-size: 12px;
			padding-top: 10px;
			padding-bottom: 10px;
			text-transform: uppercase;
		}

		legend {
			font-weight: bold;
			font-size: 14px;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Productos</h2>
					</div>
					<ul class="nav nav-tabs mt-3" role="tablist">
						<li class="nav-item">
							<a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
								<i class="icon-stack3 position-left"></i>
								Lista</a>
						</li>
                    </ul>
                    <div class="tab-content mb-4">
						<div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
							<asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<div class="row">
										<div class="col-md-6">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:TextBox ID="txtBuscar" runat="server" MaxLength="50"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:Button ID="btnBuscar" runat="server" SkinID="ui-boton-default" OnClick="btnBuscar_Click" Text="Buscar" />
											</div>
										</div>
									</div>
									<div class="table-responsive">
										<asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="IDProducto" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
											<Columns>
												<asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<%# Eval("CodigoBarra") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Categoria" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
													<ItemTemplate>
														<%# Eval("Categoria") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Nombre Comercial" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
													<ItemTemplate>
														<%# Eval("Nombre") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Localización" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
													<ItemTemplate>
														<%# Eval("Localizacion") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<%# Eval("Stock") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="Precio Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
													<ItemTemplate>
														<%# Eval("PrecioVenta") %>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle HorizontalAlign="Center" Width="7%" />
												</asp:CheckBoxField> 
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


                    </div>
                </div>
            </div>
        </div>

    <asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />

	<script type="text/javascript">

		function ActivarTabxId(id) {
			var x = $("#TabActivo").val(id);
			ActivarTabxBoton();
		}

		function ActivarTabxBoton() {
			$('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
		}

	</script>

    </asp:Content>