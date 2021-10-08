<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GeneradorClasesSQL.aspx.cs" Inherits="Farmacia.GeneradorClasesSQLTunki" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">
				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title">Generador de Clases</h2>
					</div>

					<asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="hide" style="display: none;">
								<asp:Literal ID="litInput" runat="server"></asp:Literal>
								<asp:Literal ID="litInputOutput" runat="server"></asp:Literal>
								<asp:Literal ID="litOutput" runat="server"></asp:Literal>
								<asp:Literal ID="litReturnValue" runat="server"></asp:Literal>
							</div>
							<p>
								<b>Procedimientos Almacenados:</b>
								<asp:DropDownList ID="ddlProcedimientosAlmacedos" AutoPostBack="True" runat="server" DataSourceID="sqlProcedimientosAlmacenados" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="ddlProcedimientosAlmacedos_SelectedIndexChanged"></asp:DropDownList>
								<asp:SqlDataSource ID="sqlProcedimientosAlmacenados" runat="server" ConnectionString="<%$ ConnectionStrings:BD %>" SelectCommand="SELECT sys.schemas.name + '.' + sys.objects.name AS Name FROM sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE sys.objects.type = @type ORDER BY sys.schemas.name, sys.objects.name">
									<SelectParameters>
										<asp:Parameter DefaultValue="P" Name="type" />
									</SelectParameters>
								</asp:SqlDataSource>
							</p>
							<table cellpadding="8">
								<tr>
									<td style="width: 50%; vertical-align: top;">
										<b>Parámetros Input:</b>
										<asp:BulletedList ID="blInputParametros" runat="server"></asp:BulletedList>
									</td>
									<td style="width: 50%; vertical-align: top;">
										<b>Parámetros Output:</b>
										<asp:BulletedList ID="blOutputParametros" runat="server"></asp:BulletedList>
									</td>
								</tr>
							</table>
							<hr />
							<p>
								<asp:GridView ID="gvParametros" runat="server" AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="ParameterName">
									<Columns>
										<asp:BoundField DataField="ParameterName" HeaderText="Parámetro" />
										<asp:TemplateField HeaderText="Valor">
											<ItemTemplate>
												<asp:TextBox ID="ParameterValue" runat="server" Columns="40"></asp:TextBox>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</p>
							<p>
								<asp:Button ID="btnEjecutarSql" runat="server" Text="Ejecutar" OnClick="btnEjecutarSql_Click" />
							</p>
							<asp:Panel runat="server" ID="pnResultadoSql" Visible="false">
								<div class="row">
									<div class="col-md-12">
										<div class="tabbable tab-content-bordered">
											<ul id="tabGrupo" class="nav nav-tabs nav-tabs-highlight">
												<li class="active"><a href="#itemClaseGE" data-toggle="tab" aria-expanded="false"><i class="icon-ticket position-left"></i>Clase Generico</a></li>
												<li><a href="#itemClaseBE" data-toggle="tab" aria-expanded="false"><i class="icon-ticket position-left"></i>Clase BE</a></li>
												<li><a href="#itemClaseDL" data-toggle="tab" aria-expanded="false"><i class="icon-ticket position-left"></i>Clase DL</a></li>
											</ul>
											<div class="tab-content">
												<div class="tab-pane has-padding active" id="itemClaseGE">
													<p>Clase Generico:</p>
													<asp:TextBox ID="txtResultadoGE" runat="server" TextMode="MultiLine" Rows="25"></asp:TextBox>
												</div>
												<div class="tab-pane has-padding" id="itemClaseBE">
													<p>Clase BE:</p>
													<asp:TextBox ID="txtResultadoBE" runat="server" TextMode="MultiLine" Rows="25"></asp:TextBox>
												</div>
												<div class="tab-pane has-padding" id="itemClaseDL">
													<p>Clase DL:</p>
													<asp:TextBox ID="txtResultadoDL" runat="server" TextMode="MultiLine" Rows="25"></asp:TextBox>
												</div>
											</div>
										</div>
									</div>
								</div>
							</asp:Panel>

						</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="btnEjecutarSql" EventName="Click" />
							<asp:AsyncPostBackTrigger ControlID="ddlProcedimientosAlmacedos" EventName="SelectedIndexChanged" />
						</Triggers>
					</asp:UpdatePanel>



				</div>
			</div>
		</div>
	</div>



	<script src='<%= ResolveClientUrl("~/Recursos/plugins/select2/select2.js") %>' type="text/javascript"></script>
	<script type="text/javascript">
		function ConfigJS() {
			$("#<%= ddlProcedimientosAlmacedos.ClientID %>").select2();
		}
	</script>
</asp:Content>
