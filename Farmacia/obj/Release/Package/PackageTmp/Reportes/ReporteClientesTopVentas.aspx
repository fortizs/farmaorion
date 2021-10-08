<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReporteClientesTopVentas.aspx.cs" Inherits="Farmacia.Reportes.ReporteClientesTopVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Reporte Clientes Top</h2>
                    </div>
                    <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnPagoListar" runat="server" DefaultButton="btnBuscar">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Sucursal:</label>
                                            <asp:DropDownList ID="ddlBIDSucursal" runat="server"></asp:DropDownList>
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click"></asp:Button>
											<a class='btn btn-success btn-xs' id="A1" href="javascript:generar_excel('TOP_CLIENTE');"><i class='icon-cloud-download'></i> Excel</a>
											<asp:LinkButton ID="lnkImprimirPDF" Visible="false" runat="server" OnClick="lnkImprimirPDF_Click" CssClass="btn btn-danger btn-danger" Width="72%"><i class="icon-printer"></i> PDF</a></asp:LinkButton>
                                        </div>
                                    </div>  
                                </div>
                                <div class="espacio"></div>
                                <div class="separador"></div>
                                <asp:Panel ID="pnListarGrid" runat="server" Visible="false">
                                  <div class="col-md-8">
                                    <div class="table-responsive" style="background-color: white">
                                        <asp:GridView ID="gvListaClientesTop" runat="server" DataKeyNames="IDCliente" OnPageIndexChanging="gvListaClientesTop_PageIndexChanging" AllowPaging="True" Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sucursal" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                       <%# Eval("Nombre") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
												<asp:TemplateField HeaderText="Numero Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# Eval("NumeroDocumento") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <%# Eval("Cliente") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Total Ventas" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# Eval("VentasTotal", "{0:N}") %>
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
    <iframe id="ifrmGeneraPDF" src="" style="width: 100%; height: 350px; border: none;"></iframe>
    <script type="text/javascript">
        function ConfigJS() {
        }

        function generar_excel(pTipoReporte) {

            var pIDSucursal = $("#<%= ddlBIDSucursal.ClientID %>").val(); 
            var pFechaInicio = $("#<%= txtBFechaInicio.ClientID %>").val();
            var pFechaFin = $("#<%= txtBFechaFin.ClientID %>").val();
             
        	$("#ifrmGeneraExcel").attr("src", "");
        	$("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pIDSucursal=" + pIDSucursal + "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin + "&pTipoReporte=" + pTipoReporte);
            return false;
        }
         
    </script>

</asp:Content>
