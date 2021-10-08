<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kit.aspx.cs" Inherits="Farmacia.Configuracion.Kit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
    <style>
        #cphPrincipal_txtSerieDocumento {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Mantenimiento de Kits</h2>
                    </div>
                    <ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
                                <i class="icon-stack3 position-left"></i>
                                Lista</a>
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
                                    <asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Sucursal:</label>
                                                    <asp:DropDownList ID="ddlSucursal" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Filtro:</label>
                                                    <asp:TextBox ID="txtCOMFiltro" runat="server"></asp:TextBox>
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
                                            <asp:GridView ID="gvLista" runat="server" DataKeyNames="IDKit" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("IDProducto") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("NombreProducto") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("UnidadMedida") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <div style="min-width: 75px;">
                                                                <ul class="icons-list">
                                                                    <li title="Editar Documento">
                                                                        <asp:LinkButton ID="lbEditar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDKit").ToString() %>'><i class="fa fa-pencil mr-2"></i></asp:LinkButton>
                                                                    </li>
                                                                    <li class="text-primary" style='<%# (Convert.ToDecimal(Eval("Cantidad").ToString()) == 0) ? "width: 35px;": "display:none;"  %>' title="Anular Kit">
                                                                        <asp:LinkButton ID="lnkAnular" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDKit").ToString() %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este Kit?\",\"{0}\"); return false;", "")%>'><i class="icon-bin"></i></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
                            <asp:UpdatePanel ID="upRegistroCompra" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdfIDKitDetalle" runat="server" Value="-1" />
                                    <asp:HiddenField ID="hdfIDKit" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />

                                    <div class="row text-right">
                                        <div class="col-md-12">
                                            <span style="color: black; font-size: 18px; font-weight: bold;">Nº de Kit:</span>
                                            <span style="color: red; font-size: 24px; font-weight: bold;">
                                                <asp:Label ID="lblNumeroKit" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="separador"></div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Sucursal:</label>
                                                <asp:DropDownList ID="ddlIDSucursal" runat="server" SkinID="ui-dropdownlist-requerido"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Producto:</label>
                                                <asp:DropDownList ID="ddlIDProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDProducto_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Unidad Medida:</label>
                                                <asp:TextBox ID="txtUnidadMedida" runat="server" Text="" Enabled="false" />
                                                <asp:HiddenField ID="hdfIDUnidadMedidaVenta" runat="server" Value="0" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Glosa:</label>
                                                <asp:TextBox ID="txtGlosa" runat="server" MaxLength="100" />
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel runat="server" ID="pnKitRegistro">
                                        <div class="row">
                                            <div class="col-md-12 text-right">
                                                <div class="form-group">
                                                    <label class="etiqueta"></label>
                                                    <asp:LinkButton ID="lnkNuevoItem" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkNuevoItem_Click"><i class="glyphicon glyphicon-plus-sign"></i> Agregar Producto</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive" style="margin-bottom: 0;">
                                                    <asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvDetalleLista_RowDeleting" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hfIDUnidadMedida" runat="server" Value='<%# Eval("IDUnidadMedida") %>' />
                                                                    <%# Eval("IDProducto") %> - <%# Eval("NombreProducto") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("CantidadReg") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <div style="width: 75px;">
                                                                        <ul class="icons-list">
                                                                            <li style="width: 35px;">
                                                                                <asp:LinkButton ID="lnkEditar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Select"><span class="icon-pencil7"></span></asp:LinkButton>
                                                                            </li>
                                                                            <li style="width: 35px;">
                                                                                <asp:LinkButton ID="lnkEliminar" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="delete" CommandArgument='<%# Eval("IDProducto") %>'><span class="icon-trash"></span></asp:LinkButton>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="espacio"></div>
                                    <div class="separador"></div>
                                    <div class="espacio"></div>
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lnkNuevoKit" runat="server" SkinID="ui-link-boton-default" OnClick="lnkNuevoKit_Click"><i class="fa fa-hand-paper-o"></i> Nuevo Kit</asp:LinkButton>
                                                <asp:LinkButton ID="lnkGuardarKit" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarKit_Click"><i class="glyphicon glyphicon-floppy-saved"></i> Grabar Kit</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DatosProducto" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Nuevo Producto</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="pnRegistroProducto" runat="server" DefaultButton="btnAgregarItem">
                    <asp:UpdatePanel ID="upRegistroProducto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group has-feedback">
                                            <label>Producto:</label>
                                            <asp:DropDownList ID="ddlIDReProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDReProducto_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="espacio"></div>
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Stock:</label>
                                            <asp:TextBox ID="txtRegStock" SkinID="ui-textbox-requerido" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Unidad Medida:</label>
                                            <asp:TextBox ID="txtRegUnidMedida" SkinID="ui-textbox-requerido" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtRegCantidad" SkinID="ui-textbox-number-requerido" runat="server" Text="1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelarItem" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarItem_Click" />
                                <asp:Button ID="btnAgregarItem" runat="server" Text="Agregar" SkinID="ui-boton-success" OnClick="btnAgregarItem_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

    <asp:HiddenField ID="TabActivo" runat="server" ClientIDMode="Static" Value="tab1" />

    <script type="text/javascript">

        function ConfigJS() {
            $("#<%= ddlIDReProducto.ClientID %>").select2({
                width: "100%"
            });

            $("#<%= ddlIDProducto.ClientID %>").select2({
                width: "100%"
            });
        }

        function ActivarTabxId(id) {
            var x = $("#TabActivo").val(id);
            ActivarTabxBoton();
        }

        function ActivarTabxBoton() {
            $('.nav-tabs a[href="#' + $("#TabActivo").val() + '"]').tab('show');
        }

        function funModalProductoAbrir() {
            $('#DatosProducto').modal('show');
        }

        function funModalProductoCerrar() {
            $('#DatosProducto').modal('hide');
        }

        function funModalLoteAbrir() {
            $('#DatosLote').modal('show');
        }

        function funModalLoteCerrar() {
            $('#DatosLote').modal('hide');
        }

    </script>
</asp:Content>

