<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteMovimientoAlmacen.aspx.cs" Inherits="Farmacia.Reportes.ReporteMovimientoAlmacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Reporte de Movimientos de Almacen</h2>
                    </div>
                    <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnMovimientoAlmacen" runat="server" DefaultButton="btnBuscarMovimiento">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Sucursal:</label>
                                            <asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
                                            <asp:Literal ID="FechaPago" runat="server" Visible="false"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Tipo Movimiento:</label>
                                            <asp:DropDownList ID="ddlBTipoMovimiento" runat="server">
                                                <asp:ListItem Value="0">--Todos--</asp:ListItem>
                                                <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                                <asp:ListItem Value="S">Salidad</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group has-feedback">
                                            <label>Fecha Inicio:</label>
                                            <asp:TextBox ID="txtBFechaInicio" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
                                            <div class="form-control-feedback form-control-feedback-sm">
                                                <i class="icon-calendar"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group has-feedback">
                                            <label>Fecha Fin:</label>
                                            <asp:TextBox ID="txtBFechaFin" runat="server" SkinID="ui-textbox-fecha-simple-requerido"></asp:TextBox>
                                            <div class="form-control-feedback form-control-feedback-sm">
                                                <i class="icon-calendar"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:Button ID="btnBuscarMovimiento" runat="server" Text="Buscar" OnClick="btnBuscarMovimiento_Click"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <a class='btn btn-success btn-xs' id="A1" style="width: 130%; height: 43px;" href="javascript:generar_excel('MOVIMIENTOALM');"><i class='icon-cloud-download'></i>
                                                <p style="font-size: 10px; color: white;">Excel</p>
                                            </a>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:LinkButton ID="lnkImprimirPDF" runat="server" OnClick="lnkImprimirPDF_Click" CssClass="btn btn-danger btn-danger" Width="72%" Height="43px"><i class="icon-printer"></i> Imprimir PDF</a></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="espacio"></div>
                                <div class="separador"></div>
                                <asp:Panel ID="pnListarGrid" runat="server" Visible="false">
                                    <div class="col-md-12">
                                        <div class="table-responsive" style="background-color: white">
                                            <asp:GridView ID="gvListaProducto" runat="server" DataKeyNames="IDMovimiento" AllowPaging="True" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" OnPageIndexChanging="gvListaProducto_PageIndexChanging" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <%# (((GridViewRow) Container).RowIndex + 1).ToString() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Numero Movimiento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("IDMovimiento") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Entidad") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transaccion" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Transaccion") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TipoMovimiento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("TipoMovimiento") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AlmacenOrigen" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("AlmacenOrigen") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AlmacenDestino" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("AlmacenDestino") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FechaMovimiento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("FechaMovimiento" , "{0:dd/MM/yyyy}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Producto") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("Cantidad", "{0:N}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UnidadMedida" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Eval("UnidadMedida") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ValorUnidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            S/. <%# Eval("ValorUnidad", "{0:N}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ValorTotal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            S/. <%# Eval("ValorTotal", "{0:N}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnImprimirPDF" runat="server" Visible="false">
                                    <div id="div_iframe" runat="server">
                                        <iframe src="" id="iframe" runat="server" style="width: 100%; height: 450px; border: none;"></iframe>
                                    </div>
                                </asp:Panel>
                                <br />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <iframe id="ifrmGeneraExcel" src="" style="width: 100%; height: 350px; border: none;"></iframe>
    <script type="text/javascript">
        function ConfigJS() {
        }

        function generar_excel(pTipoReporte) {
            var pIDSucursal = $("#<%= ddlBIDSucursal.ClientID %>").val();
            var pTipoMovimiento = $("#<%= ddlBTipoMovimiento.ClientID %>").val();
            var pFechaInicio = $("#<%= txtBFechaInicio.ClientID %>").val();
            var pFechaFin = $("#<%= txtBFechaFin.ClientID %>").val();

            $("#ifrmGeneraExcel").attr("src", "");
            $("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pTipoMovimiento=" + pTipoMovimiento + "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
            return false;
        }
    </script>


</asp:Content>
