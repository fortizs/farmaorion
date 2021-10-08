<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Farmacia.Login" %>

<%@ Register Src="~/Controles/CargandoLogin.ascx" TagName="Cargando" TagPrefix="uc1" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>::Ally::El aliado que tu negocio necesita!</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta content="A premium admin dashboard template by Mannatthemes" name="description" />
	<meta content="Mannatthemes" name="author" />
	<link href="<%= ResolveClientUrl("~/Recursos/Font/icomoon/icomoon.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/login.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/azul.css") %>" rel="stylesheet" type="text/css" />
	<link href='<%= ResolveClientUrl("~/Recursos/assets/img/ally/favicon.ico") %>' rel="icon" type="image/x-icon">

	<style>
		.login.login-with-news-feed .right-content .login-header .brand .logo {
			background-color: #ff5200;
		}

		.btn-success {
			color: #fff;
			background-color: #25476a;
			border-color: #25476a;
			-webkit-box-shadow: 0;
			box-shadow: 0;
		}

			.btn-success:not(:disabled):not(.disabled).active, .btn-success:not(:disabled):not(.disabled):active, .show > .btn-success.dropdown-toggle {
				color: #fff;
				background-color: #25476a;
				border-color: #25476a;
			}

			.btn-success:not(:disabled):not(.disabled).active, .btn-success:not(:disabled):not(.disabled):active, .show > .btn-success.dropdown-toggle {
				color: #fff;
				background-color: #25476a;
				border-color: #25476a;
			}

		.login.login-with-news-feed .right-content .login-header {
			position: relative;
			TEXT-ALIGN: CENTER;
		}

		.login-mensaje {
			display: block;
			text-align: center;
			margin: 5px;
		}

			.login-mensaje .log-err {
				display: block;
				background: #ff9898;
				color: #b41515;
				padding: 7px;
				border-radius: 25px;
				font-size: 16px;
			}

		.form-group {
			margin-bottom: 0.5rem;
		}

		.form-control-feedback {
			text-align: right;
			margin-top: -26px;
			margin-right: 8px;
		}

		.btn-gradient-warning {
			color: white;
			background: #ff5200;
		}

		.btn:hover {
			color: #ffffff;
			text-decoration: none;
			background: #ff5200;
		}

		.icon-user, .icon-lock5 {
			color: #fff;
			position: absolute;
			right: 18px;
			top: 7px;
		}


		.logo2 {
			background-image: url('Recursos/assets/img/1.png');
		}

		.login.login-with-news-feed .right-content .login-header .brand {
			padding: 0;
			font-size: 35px;
			color: #1a2229;
			FONT-WEIGHT: 700;
		}

		.fade:not(.show) {
			opacity: 1;
		}
	</style>

</head>
<body class="pace-top">
	<div id="page-container" class="fade">
		<div class="login login-with-news-feed">
			<div class="news-feed">
				<div class="news-image" style="background-image: url('Recursos/assets/img/ally/fondo.jpg'); width: 100%"></div>
				<div class="news-caption">
					<h4 class="caption-title"></h4>
				</div>
			</div>
			<div class="right-content">
				<div class="login-header">
					<div class="brand">
						<%--
							<img src="Recursos/assets/img/empresa/logo_imperial.png" style="width: 280px;" />
							<img src="Recursos/assets/img/empresa/logo_servifarma.png" style="width: 280px;" /> 
							<img src="Recursos/assets/img/empresa/logo_quiropracticoel.jpeg" style="width: 180px;" />
							 
							<img src="Recursos/assets/img/empresa/logo_ally.png" style="width: 210px;" />
							<img src="Recursos/assets/img/empresa/logo_multisalud.jpeg" style="width: 210px;" />  
							<img src="Recursos/assets/img/empresa/logo_hvdiesel.jpeg" style="width: 330px;" /> 
							<img src="Recursos/assets/img/empresa/logo_rrtcastillo.jpeg" style="width: 350px;" />
							<img src="Recursos/assets/img/empresa/logo_ferretera.jpeg" style="width: 330px;" />
							<img src="Recursos/assets/img/empresa/logo_turbomotors.jpeg" style="width: 330px;" />
							<img src="Recursos/assets/img/empresa/logo_belenmarlab.png" style="width: 330px;" />
							<img src="Recursos/assets/img/empresa/logo_dacero.jpeg" style="width: 229px;" />

							
							
						--%>
						<img src="Recursos/assets/img/empresa/logo_dacero.jpeg" style="width: 229px;" />

					</div>
				</div>
				<div class="login-content">
					<form id="frmPrincipal" class="margin-bottom-0" runat="server">
						<asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ScriptMode="Release"></asp:ScriptManager>

						<asp:UpdatePanel ID="upLogin" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Panel ID="pnLogin" runat="server" DefaultButton="lnkIngresar">
									<div class="row">
										<div class="col-md-12">
											<div class="form-group has-feedback">
												<i class="icon-user"></i>
												<asp:TextBox ID="txtUsuario" runat="server" class="form-control" ClientIDMode="Static" Placeholder="Usuario"></asp:TextBox>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-12">
											<div class="form-group has-feedback">
												<i class="icon-lock5"></i>
												<asp:TextBox ID="txtClave" runat="server" class="form-control" TextMode="Password" ClientIDMode="Static" Placeholder="Clave"></asp:TextBox>
												
											</div>
										</div>
									</div>
									<asp:UpdateProgress ID="upPrincipal" runat="server" DisplayAfter="0">
										<ProgressTemplate>
											<div class="log-car"></div>
										</ProgressTemplate>
									</asp:UpdateProgress>
									<div class="row">
										<div class="col-md-12">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:LinkButton ID="lnkIngresar" runat="server" Width="100%" CssClass="btn btn-wide btn-primary" OnClick="lnkIngresar_Click"> Iniciar Sessión <i class="icon-enter3"></i></asp:LinkButton>
												<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
													<ProgressTemplate>
														<uc1:cargando id="ucCargando" runat="server" />
													</ProgressTemplate>
												</asp:UpdateProgress>
											</div>
										</div>
										<div class="col-md-12">
											<div class="form-group">
												<label class="etiqueta"></label>
												<br />
												<%--<asp:LinkButton ID="LinkButton1" runat="server" Width="100%" PostBackUrl="~/RecuperarClave.aspx"> Olvide mi Contraseña</asp:LinkButton>--%>
												<a href="RecuperarClave.aspx" style="display: none">Olvide mi contraseña</a>
											</div>
										</div>
									</div>
									<div class="login-mensaje">
										<asp:Literal ID="litMensaje" runat="server"></asp:Literal>
									</div>
								</asp:Panel>
							</ContentTemplate>
						</asp:UpdatePanel>
					</form>
				</div>
				<div class="login-header">
					<div class="brand">
						<img src="Recursos/assets/img/ally/logo_completo.png" style="width: 100px;" />
					</div>
				</div>

			</div>
		</div>

	</div>
</body>
</html>

