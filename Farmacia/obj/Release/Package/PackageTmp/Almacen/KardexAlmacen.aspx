<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KardexAlmacen.aspx.cs" Inherits="Farmacia.Almacen.KardexAlmacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div class="layout-px-spacing">
        <div class="row">
            <div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
                <div class="widget-content widget-content-area br-6">
                    <div class="panel-heading">
                        <h2 class="panel-title">Kardex Resumido</h2>
                    </div>
                    <asp:UpdatePanel ID="upConsulta" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        
                                        <label>Sucursal:</label>
                                        <asp:DropDownList ID="ddlSucursalOrigen" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group has-feedback">
                                        <label>Fecha Movimiento Inicio:</label>
                                        <asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                        <div class="form-control-feedback form-control-feedback-sm">
                                            <i class="icon-calendar"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group has-feedback">
                                        <label>Fecha Movimiento Fin:</label>
                                        <asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                        <div class="form-control-feedback form-control-feedback-sm">
                                            <i class="icon-calendar"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="etiqueta"></label>
                                        <asp:LinkButton ID="lnkBuscarKardex" runat="server" CssClass="btn btn-info" OnClick="lnkBuscarKardex_Click">Consultar </asp:LinkButton>
										 <a class='btn btn-success btn-xs' id="A1" href="javascript:imprimir_kardex();">Excel</a>
                                    </div>
                                </div> 
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive" style="margin-bottom: 0;">
                                        <asp:GridView ID="gvDetalleLista" runat="server" AllowPaging="True" Width="100%" AllowSorting="True" GridLines="None" AutoGenerateColumns="False" AllowCustomPaging="True" Style="margin-top: 0px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Código Producto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <%# Eval("CodigoProducto") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Producto" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <%# Eval("NombreProducto") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entrada" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <%# Eval("Entrada", "{0:n2}") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salida" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <%# Eval("Salida", "{0:n2}") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <%# Eval("Saldo", "{0:n2}") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <iframe id="ifrmGeneraExcel" src="" style="width: 100%; height: 350px; border: none;"></iframe>
    <script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>

    <script type="text/javascript">

        function ConfigJS() {
            $("#<%= ddlSucursalOrigen.ClientID %>").select2();
        }

        function imprimir_kardex() {
            alert("imprimire");
            //if (validar1() == false) return false;
            var pIDSucursal = $("#<%= ddlSucursalOrigen.ClientID %>").get(0).value;            
            var pFechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
            var pFechaFin = $("#<%= txtFechaFin.ClientID %>").val();

            console.log("pFechaInicio =" + pFechaInicio);
            console.log("pFechaFin =" + pFechaFin);

            $("#ifrmGeneraExcel").attr("src", "");
            $("#ifrmGeneraExcel").attr("src", "GeneradorExcel.aspx?pTipoReporte=KARDEXALM&pIDSucursal=" + pIDSucursal + 
                "&pFechaInicio=" + pFechaInicio + "&pFechaFin=" + pFechaFin);
            return false;
        }

        function validar1() {
            $(".error").remove();
        	<%--if (document.getElementById("<%=ddlRegProducto.ClientID%>").selectedIndex == 0) {
        		alert("Seleccione Producto...");
        		$("#<%= ddlRegProducto.ClientID %>").focus();
        		return false;
			}--%>
            return true;
        }

    </script>
</asp:Content>

