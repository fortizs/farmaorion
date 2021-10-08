<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlmacenPendienteVtas.aspx.cs" Inherits="Farmacia.Almacen.AlmacenPendienteVtas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Salidas / Despacho de Venta</h2>
                    </div>
                    <ul class="nav nav-tabs mt-3" id="border-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="border-home-tab" data-toggle="tab" href="#tab1" role="tab" aria-controls="border-home" aria-selected="true">
                                <i class="icon-stack3 position-left"></i>
                                Salidas Ventas</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="border-profile-tab" data-toggle="tab" href="#tab2" role="tab" aria-controls="border-profile" aria-selected="false">
                                <i class="icon-pencil7 position-left"></i>
                                Salidas Notas Credito</a>
                        </li>
                    </ul>
                    <div class="tab-content mb-4" id="border-tabsContent">
                        <div class="tab-pane fade show active" id="tab1" role="tabpanel" aria-labelledby="border-home-tab">
                            <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group has-feedback">
                                                    <label>Fecha Venta Inicio:</label>
                                                    <asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                                    <div class="form-control-feedback form-control-feedback-sm">
                                                        <i class="icon-calendar"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group has-feedback">
                                                    <label>Fecha Venta Fin:</label>
                                                    <asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                                    <div class="form-control-feedback form-control-feedback-sm">
                                                        <i class="icon-calendar"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label class="etiqueta"></label>
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDVenta" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <%# Eval("IDVenta") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# Eval("Sucursal") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nro.Documento" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("NumeroDocumento") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <%# Eval("Cliente") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SerieNumero" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("SerieNumero") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Venta" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaVenta") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                    <asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# "S/."+Eval("TotalVenta", "{0:N}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usuario" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Usuario") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Despacho" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <div style="width: 40px;">
                                                                <ul class="icons-list">
                                                                    <li style='<%# (Int32.Parse(Eval("IDEstadoAlmacen").ToString()) == 2 )  ? "display:none;": "width: 30px;" %>'>
                                                                        <asp:LinkButton ID="lbPagar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Entrega de Productos"><span class="glyphicon glyphicon-pushpin"></span></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane fade show" id="tab2" role="tabpanel" aria-labelledby="border-home-tab">
                            <asp:UpdatePanel ID="upListaNC" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnBuscarNC" runat="server" DefaultButton="btnBuscarNC">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group has-feedback">
                                                    <label>Fecha Emisión Inicio:</label>
                                                    <asp:TextBox ID="txtFechaInicioNC" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                                    <div class="form-control-feedback form-control-feedback-sm">
                                                        <i class="icon-calendar"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group has-feedback">
                                                    <label>Fecha Emisión Fin:</label>
                                                    <asp:TextBox ID="txtFechaFinNC" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                                    <div class="form-control-feedback form-control-feedback-sm">
                                                        <i class="icon-calendar"></i>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label class="etiqueta"></label>
                                                    <asp:Button ID="btnBuscarNC" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscarNC_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvListaNC" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvListaNC_PageIndexChanging" OnSelectedIndexChanged="gvListaNC_SelectedIndexChanged" DataKeyNames="IDVenta" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sucursal" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%# Eval("Sucursal") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NumeroDocumento" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("NumeroDocumento") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <%# Eval("Cliente") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TipoDocumento" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("TipoComprobante") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SerieNumero" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("SerieNumero") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Emision" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaVenta") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Moneda" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("IDMoneda") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Venta" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("TotalVenta") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usuario" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Usuario") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Despacho" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <div style="width: 40px;">
                                                                <ul class="icons-list">
                                                                    <li style="width: 30px">
                                                                        <asp:LinkButton ID="lbPagarNC" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Entrega de Productos"><span class="glyphicon glyphicon-pushpin"></span></asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvListaNC" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalSalidaxVenta" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <h6 class="modal-title">Pedido por Despachar</h6>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
                    <asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdfIDVenta" runat="server" Value="0" />
                                <asp:HiddenField ID="hdTipoDocumento" runat="server" Value="0" />
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Serie-Número:</label>
                                            <asp:TextBox ID="txtSerieNumero" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label>Cliente:</label>
                                            <asp:TextBox ID="txtCliente" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
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
                                        <div class="form-group has-feedback">
                                            <label>Fecha Salida:</label>
                                            <asp:TextBox ID="txtFechaSalidaAlmacen" SkinID="ui-textbox-fecha-simple" Enabled="false" runat="server"></asp:TextBox>
                                            <div class="form-control-feedback form-control-feedback-sm">
                                                <i class="icon-calendar"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Transacción:</label>
                                            <asp:DropDownList ID="ddlIDTransaccion" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Almacen Salida:</label>
                                            <asp:DropDownList ID="ddlIDAlmacenSalida" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Glosa:</label>
                                            <asp:TextBox ID="txtGlosa" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive" style="margin-bottom: 0;">
                                            <asp:GridView ID="gvDetalleLista" runat="server" DataKeyNames="CodigoProducto" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <%# Eval("Producto") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Eval("Cantidad", "{0:n}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Precio" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# "S/."+Eval("PrecioVenta", "{0:n}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# "S/."+ Eval("ImporteTotal", "{0:n}") %>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="espacio"></div>
                                <div class="row">
                                    <div class="col-md-7"></div>
                                    <div class="col-md-5">
                                        <div class="table-responsive no-margin-bottom no-border">
                                            <table class="ui-table">
                                                <tr>
                                                    <th style="width: 40%; text-align: right!important;">
                                                        <div style="min-width: 140px;"><b>TOTAL VENTA:</b></div>
                                                    </th>
                                                    <td style="width: 60%; text-align: right!important; color: darkred; font-size: 18px; font-weight: bold;">
                                                        S/. <asp:Label ID="lblImporteTotalVenta" runat="server" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelar" runat="server" Text="Salir" SkinID="ui-boton-default" CausesValidation="False" OnClientClick="CerrarModal('ModalSalidaxVenta')" />
                                <asp:Button ID="btnGuardar" runat="server" Text="Entregar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

            </div>
        </div>
    </div>
</asp:Content>
