<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteLibroVenta.aspx.cs" Inherits="Farmacia.Reportes.ReporteLibroVenta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Reporte de Libro de Ventas</h2>
                    </div>
                    <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Sucursal</label>
                                            <asp:DropDownList ID="ddlIDSucursal" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Periodo Año:</label>
                                            <asp:DropDownList ID="ddlPeriodoAnio" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Periodo Mes:</label>
                                            <asp:DropDownList ID="ddlPeriodoMes" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
                                            <a class="btn btn-primary btn-xs" id="A1" href="javascript:generar_excel('LIBRO_VENTAS');"><i class='icon-cloud-download'></i>Excel</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvLista" runat="server" DataKeyNames="IDVenta" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("Venta") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Periodo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("Periodo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("Sucursal") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre Cliente" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <%# Eval("NombreCliente") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("Producto") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SerieNumero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("SerieNumero") %>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="T.Ope.Gravada" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    S/. <%# Eval("TotalOperacionGravada", "{0:N}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Igv" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    S/. <%# Eval("TotalIgv", "{0:N}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    S/. <%# Eval("TotalVenta", "{0:N}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("FechaVenta", "{0:dd/MM/yyyy}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
            var pIDSucursal = $("#<%= ddlIDSucursal.ClientID %>").val();
		    var pPeriodo = $("#<%= ddlPeriodoAnio.ClientID %>").val() + $("#<%= ddlPeriodoMes.ClientID %>").val();
		    console.log("PERIODO =" + pPeriodo);

		    $("#ifrmGeneraExcel").attr("src", "");
		    $("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pTipoReporte=" + pTipoReporte + "&pPeriodo=" + pPeriodo + "&pIDSucursal=" + pIDSucursal);
		    return false;
		}
    </script>
</asp:Content>
