<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarClaveUsuario.aspx.cs" Inherits="Farmacia.Seguridad.CambiarClaveUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		.sweet-alert ul li {
			text-align: left;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
		<div class="row">
			<div class="col-xl-12 col-lg-12 col-sm-12  layout-spacing">

				<div class="widget-content widget-content-area br-6">
					<div class="panel-heading">
						<h2 class="panel-title"></h2>
					</div>
					<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
						<ContentTemplate>
							<div style="max-width: 450px; margin: auto">
								<asp:HiddenField ID="hfIDUsuario" runat="server" Value="0" />
								<asp:HiddenField ID="hfCantClaveSinRepetir" runat="server" Value="0" />
								<div class="panel panel-default">
									<div class="panel-heading">
										<h6 class="panel-title text-uppercase"><i class="icon-key position-left"></i>Cambiar contraseña</h6>
									</div>
									<div class="panel-body">
										<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
											<asp:View ID="View1" runat="server">
												<asp:Panel ID="pPaso1" runat="server" DefaultButton="lbGuardar">
													<div class="form-group">
														<div runat="server" id="dEstadoClave" visible="false" class="alert alert-primary no-border">
															<asp:Label ID="lblEstadoClave" runat="server"></asp:Label>
														</div>
													</div>
													<div class="form-group">
														<label>
															Contraseña Actual
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClaveActual" ErrorMessage="La contraseña actual es obligatoria." CssClass="text-warning-600">*</asp:RequiredFieldValidator>
														</label>
														<asp:TextBox ID="txtClaveActual" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
													</div>
													<div class="form-group">
														<label>
															Nueva Contraseña
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
												ControlToValidate="txtNuevaClave" Display="Dynamic"
												ErrorMessage="La nueva contraseña es obligatoria." CssClass="text-warning-600">*</asp:RequiredFieldValidator>
															<asp:CompareValidator ID="CompareValidator1" runat="server"
																ControlToCompare="txtNuevaClave" ControlToValidate="txtNuevaClaveConfir"
																Display="Dynamic"
																ErrorMessage="La nueva contraseña y la confirmación no son iguales" CssClass="text-warning-600">*</asp:CompareValidator>
															<asp:RegularExpressionValidator ID="revSeguridadClave" runat="server" Display="Dynamic"
																ControlToValidate="txtNuevaClave" Enabled="False"
																ErrorMessage="La Nueva Contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número o carácter especial (Ej. Seguridad$)"
																ValidationExpression="((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" CssClass="text-warning-600">*</asp:RegularExpressionValidator>
															<asp:RegularExpressionValidator ID="revLongitudNuevaC" runat="server"
																ControlToValidate="txtNuevaClave" Enabled="False" Display="Dynamic"
																ErrorMessage="La nueva contraseña debe tener una longitud mínima de ... caracteres"
																ValidationExpression="[\S\s]{6,10}" CssClass="text-warning-600">*</asp:RegularExpressionValidator>
															<asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"
																ControlToCompare="txtClaveActual" ControlToValidate="txtNuevaClave"
																ErrorMessage="La nueva contraseña debe ser diferente a la actual"
																Operator="NotEqual" SetFocusOnError="True" CssClass="text-warning-600" Visible="True" Enabled="True">*</asp:CompareValidator>
														</label>
														<asp:TextBox ID="txtNuevaClave" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
													</div>
													<div class="form-group">
														<label>
															Confirmar Nueva Contraseña
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
												   ControlToValidate="txtNuevaClaveConfir" Display="Dynamic"
												   ErrorMessage="Confirmar contraseña es obligatoria." CssClass="text-warning-600">*</asp:RequiredFieldValidator>
														</label>
														<asp:TextBox ID="txtNuevaClaveConfir" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
													</div> 
													<div class="form-group">
														<label></label>
													</div>
													<div class="form-group">
														<div class="text-center">
															<asp:LinkButton ID="lbCancelar" runat="server" CausesValidation="False" SkinID="ui-link-boton-secundario" OnClick="btnCancelar_Click">
                                                            <i class="icon-undo2 position-left"></i>Cancelar
															</asp:LinkButton>
															<asp:LinkButton ID="lbGuardar" runat="server" SkinID="ui-link-boton-primario" OnClick="btnCambiarClave_Click">
                                                            <i class="icon-floppy-disk position-left"></i>Guardar
															</asp:LinkButton>
														</div>
													</div>
												</asp:Panel>
											</asp:View>
											<asp:View ID="View2" runat="server">
												<div class="panel-body alert alert-success" style="margin-top: 10px">
													<div class="form-group">
														<div class="text-center">
															<span class="label label-success label-rounded label-icon"><i class="icon-checkmark4"></i></span>
														</div>
													</div>
													<div class="form-group">
														<div class="text-center">
															Contraseña actualizada con éxito
														</div>
													</div>
												</div>
												<div class="text-center">
													<asp:LinkButton ID="lbContinuar" runat="server" SkinID="ui-link-boton-primario" OnClick="btnContinuar_Click">
                                                Aceptar
													</asp:LinkButton>
												</div>
											</asp:View>
											<asp:View ID="View3" runat="server">
												<div class="panel-body alert alert-warning" style="margin-top: 10px">
													<div class="form-group">
														<div class="text-center">
															<span class="label label-warning label-rounded label-icon"><i class="icon-warning22"></i></span>
														</div>
													</div>
													<div class="form-group">
														<div class="text-center">
															<asp:Label ID="lblMensajeUsu" runat="server" Text="Solo puede cambiar la contraseña una vez en el día."></asp:Label>
														</div>
													</div>
												</div>
												<div class="text-center">
													<asp:Button ID="btnContinuar2" runat="server" Text="Aceptar" OnClick="btnContinuar_Click" />
												</div>
											</asp:View>
										</asp:MultiView>
									</div>
								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>


				</div>
			</div>
		</div>
	</div>

	<div id="vscCambiarClave" style="display: none">
		<asp:ValidationSummary ID="vsCambiarClave" runat="server" CssClass="ValidationSummary" DisplayMode="BulletList" />
	</div>
	<script type="text/javascript">
		function WebForm_OnSubmit() {
			if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
				Mensaje('warning', $(".ValidationSummary").html());
				return false;
			}
			return true;
		}
	</script>
</asp:Content>
