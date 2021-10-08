<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Moneda.aspx.cs" Inherits="Farmacia.Configuracion.Moneda" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">

    <div class="panel panel-flat">
        <div class="panel-heading">
            <h3 class="panel-title">Moneda</h3>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="upLista" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="etiqueta"></label>
                                <asp:TextBox ID="txtBuscar" runat="server" placeholder="Ingrese criterio" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="etiqueta"></label>
                                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Width="100%" />
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="etiqueta"></label>
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" Width="100%" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" DataKeyNames="IDMoneda" AllowSorting="True" EnableSortingAndPagingCallbacks="True" GridLines="None" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Eval("IDMoneda") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="50%">
                                    <ItemTemplate>
                                        <%# Eval("Nombre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Simbolo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("NombreCorto") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CheckBoxField DataField="Estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:CheckBoxField>
                                <asp:TemplateField ShowHeader="False" HeaderText="Editar" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div style="width: 40px;">
                                            <ul class="icons-list">
                                                <li style="width: 35px">
                                                    <asp:LinkButton ID="lbEditar" SkinID="ui-link-boton-primario" runat="server" CausesValidation="False" CommandName="Select" ToolTip="Editar"><span class="icon-pencil7"></span></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

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


    <div id="DatosMoneda" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-hmodal">
                    <button type="button" class="close" data-dismiss="modal">×</button>
                    <h6 class="modal-title">Datos de Moneda</h6>
                </div>
                <asp:Panel ID="paPrincipal" runat="server" DefaultButton="btnGuardar">
                    <asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hdfIDMoneda" runat="server" Value="0" />
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <div class="form-group">
                                            <label class="etiqueta"></label>
                                            <asp:CheckBox ID="chkEstado" runat="server" Text="Estado" Checked="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Codigo:</label>
                                            <asp:TextBox ID="txtCodigo" SkinID="ui-textbox-requerido" MaxLength="3" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label>Nombre:</label>
                                            <asp:TextBox ID="txtNombre" SkinID="ui-textbox-requerido" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Simbolo:</label>
                                            <asp:TextBox ID="txtSimbolo" SkinID="ui-textbox-requerido" MaxLength="5" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="ui-boton-danger" CausesValidation="False" OnClick="btnCancelar_Click" />
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="frmPrincipal" OnClick="btnGuardar_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

            </div>
        </div>
    </div>

    <script type="text/javascript">

        function funModalAbrir() {
            $('#DatosMoneda').modal('show');
        }

        function funModalCerrar() {
            $('#DatosMoneda').modal('hide');
        }

    </script>

</asp:Content>
