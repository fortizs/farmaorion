<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferenciaDirecta.aspx.cs" Inherits="Farmacia.Almacen.TransferenciaDirecta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Transferencia Directa</h2>
                    </div>
                    <asp:UpdatePanel ID="upRegistro" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfIDSucursal" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfIDMovimientoDetalle" runat="server" Value="-1" />
                            <asp:HiddenField ID="hdfIDMovimiento" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfToken" runat="server" Value="" />

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Almacen Origen:</label>
                                        <asp:DropDownList ID="ddlIDAlmacenOrigen" runat="server" Enabled="false" SkinID="ui-dropdownlist-requerido">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Almacen Destino:</label>
                                        <asp:DropDownList ID="ddlIDAlmacenDestino" runat="server" SkinID="ui-dropdownlist-requerido">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group has-feedback">
                                        <label>Fecha Movimiento:</label>
                                        <asp:TextBox ID="txtFechaMovimiento" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                        <div class="form-control-feedback">
                                            <i class="icon-calendar"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Numero Doc. Documento:</label>
                                        <asp:TextBox ID="txtNumeroDocumento" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Transacción:</label>
                                        <asp:DropDownList ID="ddlIDTransaccion" runat="server" SkinID="ui-dropdownlist-requerido" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Numero Doc. Referencia:</label>
                                        <asp:TextBox ID="txtNumeroReferencia" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Motivo</label>
                                        <asp:TextBox ID="txtMotivo" runat="server" TextMode="multiline" Rows="2" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8 text-right">
                                    <div class="form-group">
                                        <label class="etiqueta"></label>
                                        <asp:LinkButton ID="lnkNuevoItem" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkNuevoItem_Click"><i class="glyphicon glyphicon-plus-sign"></i> Agregar Producto </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="espacio"></div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="table-responsive" style="margin-bottom: 0;">
                                        <asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvDetalleLista_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <div style="width: 73px"><%# Eval("Codigo") %></div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="55%">
                                                    <ItemTemplate>
                                                        <%# Eval("NombreProducto") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidad Medida" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# Eval("UnidadMedida") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Eval("Cantidad") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <div style="width: 40px;">
                                                            <ul class="icons-list">
                                                                <li style="width: 35px">
                                                                    <asp:LinkButton ID="lnkEliminar" CssClass="btn btn-danger" runat="server" CausesValidation="False" CommandName="delete" CommandArgument='<%# Eval("IDProducto") %>'><span class="icon-trash"></span></asp:LinkButton>
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
                            <div class="espacio"></div>
                            <div class="row text-right">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkNuevaTransferencia" runat="server" SkinID="ui-link-boton-success" OnClick="lnkNuevaTransferencia_Click"><i class="icon-file-plus"></i> Nuevo</asp:LinkButton>
                                        <asp:LinkButton ID="lnkGuardarTransferencia" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarTransferencia_Click"><i class="icon-floppy-disk"></i> Guardar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="DatosProducto" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Nuevo Item</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="paPrincipal" runat="server" DefaultButton="lnkAgregarProducto">
                    <asp:UpdatePanel ID="upRegistroProducto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdfIDProducto" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfIDUnidadMedida" runat="server" Value="0" />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Producto:</label>
                                            <asp:DropDownList ID="ddlIDProducto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDProducto_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Stock Actual:</label>
                                            <asp:TextBox ID="txtStockActual" SkinID="ui-textbox-requerido" runat="server" Text="0" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Unidad Medida:</label>
                                            <asp:TextBox ID="txtUnidadMedida" SkinID="ui-textbox-requerido" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="DivCantidad" runat="server">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtCantidad" SkinID="ui-textbox-price-requerido" runat="server" Text="0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server" id="DivCantidadLote" visible="false">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <div class="input-group">
                                                <asp:HiddenField ID="hdfCantidadLote" runat="server" Value="0" />
                                                <asp:TextBox ID="txtRegCantidadLote" runat="server" SkinID="ui-textbox-number-requerido" Text="0" Enabled="false"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="lnkAbrirLote" runat="server" OnClick="lnkAbrirLote_Click" SkinID="ui-link-boton-primario"><i class="fa fa-cube"></i></asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="lnkCancelarProducto" runat="server" SkinID="ui-link-boton-default" OnClientClick="CerrarModal('DatosProducto')"> <i class="icon-undo2"></i> Cerrar </asp:LinkButton>
                                <asp:LinkButton ID="lnkNuevoProducto" runat="server" SkinID="ui-link-boton-success" OnClick="lnkNuevoProducto_Click"> <i class="icon-file-empty"></i> Nuevo </asp:LinkButton>
                                <asp:LinkButton ID="lnkAgregarProducto" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkAgregarProducto_Click"> <i class="icon-floppy-disk"></i> Agregar </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

            </div>
        </div>
    </div>

    <div id="DatosAgregarLoteProducto" class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Lotes</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkNuevoLote">
                    <asp:UpdatePanel ID="upLoteListar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdIDProductoLote" runat="server" Value="0" />
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <asp:LinkButton ID="lnkNuevoLote" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkNuevoLote_Click"><span class="glyphicon glyphicon-plus"></span> Nuevo Lote</asp:LinkButton>
                                    </div>
                                    <div class="espacio"></div>
                                    <div class="espacio"></div>
                                    <div class="col-md-12" style="overflow-y: scroll; height: 300px">
                                        <div class="table-responsive" style="margin-bottom: 0; font-size: 10px;">
                                            <asp:GridView ID="gvLoteProducto" runat="server" DataKeyNames="IDLote,Lote" AllowPaging="True" Width="100%" AllowSorting="True" OnRowCommand="gvLoteProducto_RowCommand" GridLines="None" AutoGenerateColumns="False">
                                                <Columns>                                                    
                                                    <asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("Lote") %>
                                                            <asp:Label ID="lblNombreLote" runat="server" Text=' <%# Eval("Lote") %>' Visible="false" />
                                                            <asp:Label ID="lblIDLote" runat="server" Text=' <%# Eval("IDLote") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Stock" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("Stock") %>
                                                            <asp:Label ID="lblStockLote" runat="server" Text='<%# Eval("Stock") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidadLote" runat="server" Text='<%# Eval("CantidadLote") %>' SkinID="ui-textbox-number-requerido" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fab-Venc" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaFabricacion", "{0:dd/MM/yyyy}") %> - <%# Eval("FechaVencimiento", "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <div style="width: 90px;">
                                                                <ul class="icons-list">
                                                                    <li style="width: 35px;">
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Editar" CommandArgument='<%# Eval("IDLote") %>' ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
                                                                    </li>
                                                                    <li style="width: 35px;">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" SkinID="ui-link-boton-primario" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("IDLote") %>' OnClientClick='<%# String.Format("return Confirmacion(this,event,\"¿Está seguro que deseas eliminar este Lote?\",\"{0}\"); return false;", "")%>' ToolTip="Eliminar"><span class="icon-trash"></span></asp:LinkButton>
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
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-4 text-right">
                                        <div class="espacio"></div>
                                        <asp:LinkButton ID="lnkAplicarLote" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkAplicarLote_Click"><i class="fa fa-check-square"></i> Aplicar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div id="DatosLote" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Nuevo Lote</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="PnLote" runat="server" DefaultButton="btnGuardarLote">
                    <asp:UpdatePanel ID="UpRegistroLote" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfIDLote" runat="server" Value="0" />
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Nombre Lote:</label>
                                            <asp:TextBox ID="txtLote" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Fecha Fabricación:</label>
                                            <asp:TextBox ID="txtLFechaFabricacion" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Fecha Vencimiento:</label>
                                            <asp:TextBox ID="txtLFechaVencimiento" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Stock:</label>
                                            <asp:TextBox ID="txtLStock" Enabled="false" SkinID="ui-textbox-requerido" runat="server" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Cantidad:</label>
                                            <asp:TextBox ID="txtLCantidad" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelarLote" runat="server" Text="Cancelar" SkinID="ui-boton-default" OnClick="btnCancelarLote_Click" />
                                <asp:Button ID="btnGuardarLote" runat="server" Text="Guardar" SkinID="ui-boton-success" OnClick="btnGuardarLote_Click" />
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

    <script type="text/javascript">

        function ConfigJS() {
            $("#<%= ddlIDProducto.ClientID %>").select2({
                width: "100%"
            });
        }


        function funModalListaLoteAbrir() {
            $('#DatosAgregarLoteProducto').modal('show');
        }

        function funModalListaLoteCerrar() {
            $('#DatosAgregarLoteProducto').modal('hide');
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

