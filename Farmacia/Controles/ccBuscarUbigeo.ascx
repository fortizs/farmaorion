<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ccBuscarUbigeo.ascx.cs" Inherits="Farmacia.Controles.ccBuscarUbigeo" %>
<asp:Panel ID="pBuscarUbigeo" runat="server" DefaultButton="btnBuscarUbigeo">
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <asp:TextBox ID="txtBuscarUbigeo" runat="server" placeholder="Buscar"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <asp:Button ID="btnBuscarUbigeo" runat="server" SkinID="ui-boton-default" Text="Buscar" ValidationGroup="BuscarUbigeo" OnClick="btnBuscarUbigeo_Click" />
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gvBuscarUbigeo" runat="server"  AutoGenerateColumns="False" EnableModelValidation="True" AllowPaging="True" DataKeyNames="IDUbigeo" EnableSortingAndPagingCallbacks="True" PageSize="6" OnPageIndexChanging="gvBuscarUbigeo_PageIndexChanging" OnRowDataBound="gvBuscarUbigeo_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IDUbigeo" HeaderText="Código">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="NombreCompleto" HeaderText="Ubigeo" />
                <asp:TemplateField>
                    <ItemTemplate>                        
                        <asp:LinkButton ID="lbSeleccionar" CssClass="btn btn-default btn-lg" runat="server" CausesValidation="False" ToolTip="Seleccionar" ><span class="icon-multitouch"></span></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Literal ID="Literal5" runat="server" Text="No se encontraron registros"></asp:Literal>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Panel>


<script type="text/javascript">
    var reBCIDUbigeo;
    var reBCNombre;

    function gBuscarUbigeo(preBCIDUbigeo, preBCNombre) {
        reBCIDUbigeo = preBCIDUbigeo;
        reBCNombre = preBCNombre;
        $('#BuscarUbigeo').modal('show');
    }

    function SeleccionarUbigeo(pIDUbigeo, pNombre) {
        $("#" + reBCIDUbigeo).val(pIDUbigeo);
        $("#" + reBCNombre).val(pNombre);
        $('#BuscarUbigeo').modal('hide');
    }
</script>
