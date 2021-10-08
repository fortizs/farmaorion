<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KitDesarmar.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Farmacia.Configuracion.KitDesarmar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Desarmar Kits</h2>
                    </div>

                    <asp:updatepanel id="upIngreso" runat="server" updatemode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnIngreso" runat="server" DefaultButton="btnGuardar">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group has-feedback">
                                            <label>Fecha Emisión:</label>
                                            <asp:TextBox ID="txtFechaEmision" SkinID="ui-textbox-fecha-simple" Enabled="false" runat="server"></asp:TextBox>
                                            <div class="form-control-feedback form-control-feedback-sm">
                                                <i class="icon-calendar"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Transacción:</label>
                                            <asp:DropDownList ID="ddlIDTransaccion" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Almacen Salida:</label>
                                            <asp:DropDownList ID="ddlIDAlmacenIngreso" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>                                
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Kit:</label>
                                            <asp:HiddenField ID="hdfIDProductoKit" runat="server" Value="0" />
                                            <asp:DropDownList ID="ddlIDKit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIDKit_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Precio Costo:</label>
                                            <asp:TextBox runat="server" ID="txtPrecioCosto" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Stock:</label>
                                            <asp:TextBox runat="server" ID="txtKitStock" Text="0" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>U. Medida:</label>
                                            <asp:HiddenField runat="server" ID="hdfIDUnidadMedida" Value="0" />
                                            <asp:TextBox runat="server" ID="txtUnidadMedida" Text="" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cantidad Desarmar:</label>
                                            <asp:TextBox runat="server" ID="txtCantidad" Text="0"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnCalcular" Text="Calcular" OnClick="btnCalcular_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdfIDMovimiento" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdIDProductoLote" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfIDProductoSeleccionado" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfTotalLote" runat="server" Value="0" />
                                        <div class="table-responsive" style="margin-bottom: 0;">

                                            <asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="IDProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="gvDetalleLista_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            
                                                            <%# Eval("IDProducto") %> - <%# Eval("NombreProducto") %>
                                                            <asp:Label ID="lblIDProducto" runat="server" Text='<%# Eval("IDProducto") %>' Visible="false" ></asp:Label>
                                                            <asp:Label ID="lblIDUnidadMedida" runat="server" Text='<%# Eval("IDUnidadMedida") %>' Visible="false" />
                                                            <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Eval("NombreProducto") %>' Visible="false" />
                                                            <asp:Label ID="lblControlaLote" runat="server" Text='<%# Eval("Producto.ControlaLote") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cant. Reg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCantidadReg" runat="server" Text='<%# Eval("CantidadReg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Es Lote" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <div style="<%# (Boolean.Parse(Eval("Producto.ControlaLote").ToString())) ? "width:40px": "display:none" %>">
                                                                <ul class="icons-list">
                                                                    <li style="width: 30px">
                                                                        <asp:LinkButton ID="lnkLote" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Ingreso Lote"><span class="fa fa-cube"></span></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cant. Desarmar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCantidadArmado" runat="server" Text='<%# Eval("CantidadArmado") %>'></asp:Label>
                                                            <asp:Label ID="lblCantidadArmadoSaldo" runat="server" Text='0' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Cant. Dispon" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%# Eval("CantidadDisponible") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Ingresar" OnClick="btnGuardar_Click" />

                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:updatepanel>
                </div>
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
                <asp:panel id="Panel1" runat="server">
                    <asp:UpdatePanel ID="upLoteListar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        Cantidad a Retirar 
                                        <asp:TextBox runat="server" ID="txtSaldoCantidadLote" Text="0" Enabled="false" />
                                    </div>
                                    <div class="col-md-6 text-right">
                                        
                                    </div>
                                    <div class="espacio"></div>
                                    <div class="espacio"></div>
                                    <div class="col-md-12" style="overflow-y: scroll; height: 300px">
                                        <div class="table-responsive" style="margin-bottom: 0; font-size: 10px;">
                                            <asp:GridView ID="gvLoteProducto" runat="server" DataKeyNames="IDLote,Lote" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
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
                </asp:panel>
            </div>
        </div>
    </div>


    <script src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Recursos/plugins/jqueryui/jquery-ui.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

    <script type="text/javascript">

        function ConfigJS() {
            $("#<%= ddlIDKit.ClientID %>").select2({
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
