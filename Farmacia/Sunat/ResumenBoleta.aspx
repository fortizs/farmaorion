<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ResumenBoleta.aspx.cs" Inherits="Muebleria.Sunat.ResumenBoleta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-bd">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h3>RESUMEN DE COMPROBANTES</h3>
                    </div>
                </div>
                <div class="panel-body">

                    <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnBuscar" runat="server" DefaultButton="btnBuscar">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sucursal:</label>
                                            <asp:DropDownList ID="ddlSucursal" runat="server">
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label>Tipo Documento:</label>
                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Fecha Inicio:</label>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>
                                                <asp:TextBox ID="txtFechaInicio" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Fecha Fin:</label>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>
                                                <asp:TextBox ID="txtFechaFin" SkinID="ui-textbox-fecha-simple" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="etiqueta">&nbsp;</label>
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="ui-boton-default" OnClick="btnBuscar_Click" />
											 <asp:Button ID="btnCrear" runat="server" Text="Nuevo" OnClick="btnCrear_Click" />
                                        </div>
                                    </div> 
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowCommand="gvLista_RowCommand" DataKeyNames="Codigo" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID Resumen" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("IDResumen") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comprobante" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("TipoComprobante") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Emision" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <%# Eval("FechaEmisionComprobante") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Generacion" ItemStyle-Width="13%">
                                                <ItemTemplate>
                                                    <%# Eval("FechaGeneracionResumen") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TicketSunat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <%# Eval("TicketSunat") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estado Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("EstadoDocumento") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estado y Fecha Envío a Sunat" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("EstadoSunat") %><br />
                                                    <span class="bg-fecha" style="font-size: 11px; display: block;"><%# (Eval("FechaEnvioSunat", "{0:dd/MM/yyyy}") == "01/01/1900") ? "": Eval("FechaEnvioSunat") %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MensajeSunat" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <%# Eval("MensajeSunat") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div style="min-width: 100px;">
                                                        <ul class="icons-list">
                                                            <li data-popup="tooltip" title="Ver Documentos" data-original-title="Ver Detalle">
                                                                <asp:LinkButton ID="lnkDetalle" SkinID="ui-link-boton-danger" runat="server" CausesValidation="False" CommandName="VerDocumentos" CommandArgument='<%# Eval("Codigo") + "," + Eval("IDTipoComprobante").ToString() %>'><i class="fa fa-search"></i></asp:LinkButton>
                                                            </li>
                                                            <li style='<%# Eval("IDEstadoDocumento").ToString() == "5" ? "display:none;": "" %>' class="text-teal-600" data-popup="tooltip" data-original-title="Firmar Documento">
                                                                <asp:LinkButton ID="lnkSeleccionar" SkinID="ui-link-boton-info" runat="server" CausesValidation="False" CommandName="Firmar" CommandArgument='<%# Eval("Codigo") + "," + Eval("IDTipoComprobante").ToString() %>'><i class="fa fa-key"></i></asp:LinkButton>
                                                            </li>
                                                            <li style='<%# ((Eval("IDEstadoDocumento").ToString() == "5") && (Eval("IDEstadoSunat").ToString() != "2"))  ? "": "display:none;" %>' class="text-teal-600" data-popup="tooltip" data-original-title="Enviar a Sunat">
                                                                <asp:LinkButton ID="LinkButton1" SkinID="ui-link-boton-success" runat="server" CausesValidation="False" CommandName="Enviar" CommandArgument='<%# Eval("Codigo") + "," + Eval("IDTipoComprobante").ToString() %>'><i class="fa fa-send"></i></asp:LinkButton>
                                                            </li>
<%--                                                            <li style='<%# Eval("IDEstadoSunat").ToString() == "2" ? "": "display:block;" %>' class="text-teal-600" data-popup="tooltip" data-original-title="Descargar XML">
                                                                <asp:LinkButton ID="LinkButton2" SkinID="ui-link-boton-pink" runat="server" CausesValidation="False" CommandName="GenerarXML" CommandArgument='<%# Eval("Codigo") %>'><i class="fa fa-cloud-download"></i></asp:LinkButton>
                                                            </li>--%>
                                                            <li class="text-pink" style='<%# ((Eval("IDEstadoSunat").ToString() == "2") && (Eval("IDEstadoDocumento").ToString() != "7")) ? "": "display:none;" %>' data-popup="tooltip" title="Genera XML" data-original-title="Genera XML">
                                                                <asp:LinkButton ID="lnkGeneraXML" runat="server" CausesValidation="False" CommandName="GeneraXML" CommandArgument='<%# Eval("Codigo").ToString() + "," + Eval("IDTipoComprobante").ToString() %>'><i class="fa fa-file-code-o"></i></asp:LinkButton>
                                                            </li> 
                                                            <li class="text-violet" style='<%# ((Eval("IDEstadoSunat").ToString() == "2") && (Eval("IDEstadoDocumento").ToString() != "7")) ? "": "display:none;" %>' data-popup="tooltip" title="Descargar XML" data-original-title="Descargar XML">
                                                                <asp:LinkButton ID="lnkDescargarCDR" runat="server" CausesValidation="False" CommandName="DescargaXML" CommandArgument='<%# Eval("Codigo").ToString() + "," + Eval("IDTipoComprobante").ToString() %>'><i class="fa fa-cloud-download"></i></asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <br />
                                <div class="leyenda">
                                    <ul class="icons-list" style="font-size: 15px; text-align: right; font-weight: bold;">
                                        <li>Leyenda de Estados: </li>
                                        <li style="color: #4ebcff!important;" class="text-info-600"><i class="fa fa-key"></i>Firmar Documento</li>
                                        <li style="color: #26a000!important;" class="text-info-600"><i class="fa fa-send"></i>Enviar a Sunat</li>
                                        <li style="color: #8500d6!important;" class="text-info-600"><i class="fa fa-cloud-download"></i>Descargar CDR</li>
                                        <li style="color: #f30d0d!important;" class="text-info-600"><i class="fa fa-trash-o"></i>Anular Documento</li>
                                    </ul>
                                </div>


                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="DatosDetalleDocumentos" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-usuario">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h6 class="modal-title">Detalle de Documentos</h6>
                </div>
                <asp:UpdatePanel ID="upDetalleDocumentos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDetalleLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvDetalleLista_PageIndexChanging" DataKeyNames="IDResumenComprobanteDetalle" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="IDResumen" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <%# Eval("IDResumen") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Numero Item" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Eval("NumeroItem") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serie" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <%# Eval("SerieNumero") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Eval("TipoMoneda") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Igv" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Eval("TotalIgv") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Vta" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Eval("TotalVenta") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDetalleCancelar" runat="server" Text="Cerrar" SkinID="ui-boton-danger" CausesValidation="False" OnClick="btnDetalleCancelar_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="DatosResumen" class="modal fade" tabindex="-1" role="dialog">
        <asp:UpdatePanel ID="upResumen" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h6 class="modal-title">Crear Resumen Diario</h6>
                </div>
                <asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
                    <asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Documentos:</label>
                                            <asp:DropDownList ID="ddlDocumentos" runat="server">
                                                <asp:ListItem Value="03">Boletas</asp:ListItem>
                                                <asp:ListItem Value="07">Notas de Credito</asp:ListItem>
                                                <asp:ListItem Value="08">Notas de Debito</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>
                                                Fecha Emision:
                                                <asp:RequiredFieldValidator ID="rfvtxtFechaResumen" runat="server" SetFocusOnError="true" ControlToValidate="txtFechaResumenM" ErrorMessage="Ingrese una fecha." ValidationGroup="frmPrincipal" CssClass="estRequerido">*</asp:RequiredFieldValidator>
                                            </label>
                                            <asp:TextBox ID="txtFechaResumenM" SkinID="ui-textbox-fecha-simple" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-5">
                                        <div class="form-group">
                                            <label>Tipo Comprobante:</label>
                                            <asp:DropDownList ID="ddlTipoComprobanteM" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <%--<div class="col-md-5">
                                        <div class="form-group">
                                            <label>Tipo Resumen:</label>
                                            <asp:DropDownList ID="ddlTipoResumenM" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:Button ID="btnResumenBuscar" runat="server" Text="Buscar" SkinID="ui-boton-info" OnClick="btnResumenBuscar_Click" />
                                        </div>
                                    </div>
                                </div>

                                    <div class="table-responsive">
                                    <asp:GridView ID="gvFacturaLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvFacturaLista_PageIndexChanging" DataKeyNames="Codigo" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                                        <Columns>
<%--                                            <asp:TemplateField HeaderText="Sel." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRDSel" runat="server" ClientIDMode="Static" />
                                                    <asp:Label ID="lblIndex" runat="server" Text='<%# ((GridViewRow) Container).RowIndex %>' Style="display: none;"></asp:Label>
                                                    <asp:Label ID="lblIDFacturaBoleta" runat="server" Text='<%# Eval("Codigo") %>' Style="display: none;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Serie Numero" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("SerieNumero") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Emisión" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <%# Eval("FechaEmision") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nro.Doc./Cliente" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <b>Nro.Doc:</b><%# Eval("NumeroDocumentoCliente") %><br /><b>Cliente:</b><%# Eval("Cliente") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("TipoMoneda") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Venta" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Eval("TotalVenta") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

<%--                                            <asp:TemplateField HeaderText="Motivo Baja" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMotivoBaja" runat="server" SkinID="ui-textbox-requerido"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>



                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-danger" CausesValidation="False" OnClick="btnCancelar_Click" />
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCrear" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <div id="vsmsgPrincipal" style="display: none">
                    <asp:ValidationSummary ID="vsfrmPrincipal" runat="server" ValidationGroup="frmPrincipal" CssClass="ValidationSummary" DisplayMode="BulletList" />
                </div>

            </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div style="display: none;">
        <iframe id="ifrmDescargar" src="" style="border: 0px; padding: 0; width: 0; height: 0;"></iframe>
    </div>

    <script type="text/javascript">
        function Descargar(Archivo, TipoArchivo) {
            $('#ifrmDescargar').attr('src', 'Descargar.aspx?Archivo=' + Archivo + '&TipoArchivo=' + TipoArchivo);
        }


        function DescargarCerrar() {
            $('#ifrmDescargar').attr('src', '');
        }

        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
                Mensaje('warning', $(".ValidationSummary").html());
                return false;
            }
            return true;
        }

        function ModalRegistroDetalleDocumento() {
            $('#DatosDetalleDocumentos').modal('show');
        }

        function ModalCerrarDetalleDocumento() {
            $('#DatosDetalleDocumentos').modal('hide');
        }

        function funModalAbrir() {
            $('#DatosResumen').modal('show');
        }

        function funModalCerrar() {
            $('#DatosResumen').modal('hide');
        }

    </script>

    <script type="text/javascript">
        function ConfigJS() {
             
        }
    </script>
</asp:Content>
