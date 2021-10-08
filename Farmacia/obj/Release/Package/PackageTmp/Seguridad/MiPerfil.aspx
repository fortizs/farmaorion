<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Farmacia.Seguridad.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<style>
		#cphPrincipal_txtNumeroDocumento, #cphPrincipal_txtNombresCompleto, #cphPrincipal_txtTipoPersona, #cphPrincipal_ddlSexo, #cphPrincipal_ddlIDEstadoCivil,
		#cphPrincipal_txtFechaNacimiento, #cphPrincipal_txtEdad, #cphPrincipal_txtTelefono, #cphPrincipal_txtCelular, #cphPrincipal_txtUsuario, #cphPrincipal_txtEmail, #cphPrincipal_txtClave {
			font-weight: 700;
		}

		.form-control-feedback {
			top: 31px;
		}

		#cphPrincipal_btnRegistroImagenListar {
			display: none;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="panel panel-flat">
		<div class="panel-heading">
			<h3 class="panel-title">Mi Perfil</h3>
		</div>
		<div class="panel-body">
			<asp:UpdatePanel ID="upUsuarioRegistrar" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<asp:HiddenField ID="hfIDColaborador" runat="server" Value="0" />
					<asp:Panel ID="pnUsuarioRegistrar" runat="server">
						<div class="row layout-spacing">
							<div class="col-xl-4 col-lg-6 col-md-5 col-sm-12 layout-top-spacing">
								<asp:HiddenField ID="hfRutaImagen" runat="server" />
								<asp:HiddenField ID="hfRutaImagenMiniatura" runat="server" />
								<div class="row">
									<div class="col-sm-12">
										<div class="form-group margen-n">
											<asp:LinkButton ID="lnkRegistroImagenCargar" SkinID="ui-link-boton-primario" Width="100%" runat="server" OnClientClick="return ModalImagen();"><i class="icon-cloud-upload"></i> Agregar Imagen</asp:LinkButton>
										</div>
									</div>
								</div>
								<div class="row">
									<asp:Repeater ID="repRegistroImagen" runat="server">
										<ItemTemplate>
											<div class="col-sm-12">
												<div class="form-group" style="position: relative; display: block; border: 1px solid #dddddd; text-align: center;">
													<div style="padding: 5px; display: block;">
														<asp:Image ID="imgRegistroImagen" runat="server" CssClass="img-thumbnail" ImageUrl='<%# Eval("RutaNombreImagenFotoCompleto") %>' Style="border: 0;" />
													</div>
													<div style="padding: 5px; display: block">
														<asp:LinkButton ID="lnkRegistroImagenQuitar" runat="server" ClientIDMode="AutoID" CssClass="btn btn-danger btn-imagen" CommandArgument='<%# Eval("IDColaborador") %>' OnCommand="lnkRegistroImagenQuitar_Command" ToolTip="Eliminar imagen"><i class="icon-trash" aria-hidden="true"></i></asp:LinkButton>
													</div>
												</div>
											</div>
										</ItemTemplate>
									</asp:Repeater>
								</div>
								<asp:Button ID="btnRegistroImagenListar" runat="server" Text="Guardar" CssClass="hide" OnClick="btnRegistroImagenListar_Click" />

								<!-- Simple profile -->
								<div class="text-center">
									<p class="text-sm text-muted"><span id="spTipoPersona" runat="server"></span></p>

									<div class="pad-ver btn-groups">
										<a href="#" class="btn btn-icon demo-pli-facebook icon-lg add-tooltip" data-original-title="Facebook" data-container="body"></a>
										<a href="#" class="btn btn-icon demo-pli-twitter icon-lg add-tooltip" data-original-title="Twitter" data-container="body"></a>
										<a href="#" class="btn btn-icon demo-pli-google-plus icon-lg add-tooltip" data-original-title="Google+" data-container="body"></a>
										<a href="#" class="btn btn-icon demo-pli-instagram icon-lg add-tooltip" data-original-title="Instagram" data-container="body"></a>
									</div>
								</div>
							</div>
							<div class="col-xl-8 col-lg-6 col-md-7 col-sm-12 layout-top-spacing">
								<fieldset>
									<legend>Datos Principales:</legend>
									<div class="row">
										<div class="col-md-1"></div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Nro.Documento:</label>
												<asp:TextBox ID="txtNumeroDocumento" runat="server" Enabled="false"></asp:TextBox>
											</div>
										</div>
										<div class="col-md-6">
											<div class="form-group">
												<label>Apellidos y Nombres:</label>
												<asp:TextBox ID="txtNombresCompleto" runat="server" Enabled="false"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-1"></div>
										<div class="col-md-2">
											<div class="form-group">
												<label>Sexo:</label>
												<asp:DropDownList ID="ddlSexo" runat="server">
													<asp:ListItem Value="0"> -Seleccionar- </asp:ListItem>
													<asp:ListItem Value="M"> Masculino </asp:ListItem>
													<asp:ListItem Value="F"> Femenino </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
									</div>
									<hr>
									<div class="row">
										<div class="col-md-1"></div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Teléfono:</label>
												<asp:TextBox ID="txtTelefono" runat="server" MaxLength="9"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-phone"></i>
												</div>
											</div>
										</div>
										<div class="col-md-2">
											<div class="form-group has-feedback">
												<label>Celular:</label>
												<asp:TextBox ID="txtCelular" runat="server" MaxLength="9"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-mobile3"></i>
												</div>
											</div>
										</div>
										<div class="col-md-6">
											<div class="form-group has-feedback">
												<label>Email :</label>
												<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
												<div class="form-control-feedback form-control-feedback-sm">
													<i class="icon-envelop2"></i>
												</div>
											</div>
										</div>
									</div>
								</fieldset>
								<div class="row">
									<div class="col-md-12 text-right">
										<div class="form-group">
											<label class="etiqueta"></label>
											<asp:LinkButton ID="lnkGuardarUsuario" runat="server" SkinID="ui-link-boton-primario" OnClick="lnkGuardarUsuario_Click"><i class="icon-floppy-disks"></i> Guardar </asp:LinkButton>
										</div>
									</div>
								</div>

							</div>
						</div>
					</asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</div>

	<div id="ModalImagen" class="modal fade" tabindex="-1" role="dialog">
		<div class="modal-dialog" style="width: 400px;">
			<div class="modal-content">
				<div class="modal-header">
					<h6 class="modal-title">Cargar Foto</h6>
					<button type="button" class="close" data-dismiss="modal">×</button>
				</div>
				<div class="modal-body">
					<iframe id="ifrmImagenCargar" src="" style="width: 100%; height: 200px; border: none;"></iframe>
				</div>
			</div>
		</div>
	</div>

	<style type="text/css">
		.btn-imagen {
			padding: 3px;
			font-size: 12px;
			padding-left: 6px;
			padding-right: 6px;
		}

		.note-editing-area img {
			width: 100%;
		}
	</style>
	 
	<script type="text/javascript">
		function ModalImagen() {
			var IDColaborador = $("#<%= hfIDColaborador.ClientID %>").val();
			console.log("IDColaborador =" + IDColaborador);
			$("#ifrmImagenCargar").attr("src", "vUtilSubirImagen.aspx?pIDElemento=" + IDColaborador);
			$('#ModalImagen').modal('show');

			return false;
		}

		function CerrarModalImagen() {
			$('#ModalImagen').modal('hide');
			$("#cphPrincipal_btnRegistroImagenListar").click();
			//window.parent.CerrarModalImagen();
			return false;
		}

	</script>

</asp:Content>
