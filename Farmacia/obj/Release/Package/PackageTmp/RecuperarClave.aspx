<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="Farmacia.RecuperarClave" %>

 <!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>::SmartSystem: Tu sistema inteligente para Farmacias ::</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta content="A premium admin dashboard template by Mannatthemes" name="description" />
	<meta content="Mannatthemes" name="author" />

	<link href="<%= ResolveClientUrl("~/Recursos/Font/icomoon/icomoon.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveClientUrl("~/Recursos/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet" type="text/css" /> 
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/login.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/rojooscuro.css") %>" rel="stylesheet" type="text/css" />
	<link href='<%= ResolveClientUrl("~/Recursos/favicon.ico") %>' rel="icon" type="image/x-icon">
  
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
				background: #ffecec;
				color: #cc4e4e;
				padding: 7px;
				border-radius: 25px;
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

		/**azul*/

		/*******/


		/**AZUL: #1b55e2
		   ROJO:#b7213b
		   VERDE-OSCURO:#145b02
		*/
	</style>

</head>
<body class="pace-top">

	<div id="page-loader" class="fade show">
		<span class="spinner"></span>
	</div>

	<div id="page-container" class="fade">
		<div class="login login-with-news-feed">
			<div class="news-feed">
				<div class="news-image" style="background-image: url('Recursos/assets/img/fondo-Farmacia.jpg'); width: 100%"></div>
				<div class="news-caption">
					<h4 class="caption-title"></h4>
				</div>
			</div>
			<div class="right-content">
				<div class="login-header">
					<div class="brand">
						<img src="Recursos/assets/img/LOGO-FARMAORION.png" style="width: 273px;" />
					</div>
				</div>
				<div class="login-content">
					<form id="frmPrincipal" class="margin-bottom-0" runat="server">
						<asp:ScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ScriptMode="Release"></asp:ScriptManager>

						<asp:UpdatePanel ID="upResetPassword" runat="server" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:Panel ID="pnReset" runat="server" DefaultButton="lnkEnviar">
									<div class="row">
										<div class="col-md-12">
											<div class="form-group has-feedback">
												<i class="icon-user"></i>
												<asp:TextBox ID="txtUser" runat="server" class="form-control"  Placeholder="Usuario"></asp:TextBox>
											</div>
										</div>
									</div>  
									<div class="row">
										<div class="col-md-12">
											<div class="form-group">
												<label class="etiqueta"></label>
												<asp:LinkButton ID="lnkEnviar" runat="server" Width="100%" CssClass="btn btn-wide btn-primary" OnClick="lnkEnviar_Click"><i class="icon-sign"></i> Enviar al Correo</asp:LinkButton>
											</div>
										</div>
                                        <div class="col-md-12">
											<div class="form-group">
												<label class="etiqueta"></label>
                                                <br />
                                                <a href="Login.aspx">Iniciar Sesión</a>
                                                <%-- <asp:LinkButton ID="LinkButton1" runat="server" Width="100%" PostBackUrl="Login.aspx"> Iniciar Sesión</asp:LinkButton>--%>
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
			</div>
		</div>
		<a href="javascript:;" class="btn btn-icon btn-circle btn-success btn-scroll-to-top fade" data-click="scroll-top"><i class="fa fa-angle-up"></i></a>
	</div>
     
    <script src='<%= ResolveClientUrl("~/Recursos/assets/js/app.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/app.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/pagerequest.js") %>' type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Recursos/plugins/sweetalert/sweetalert.js") %>" type="text/javascript"></script>
	<script src="https://seantheme.com/color-admin/admin/assets/js/app.min.js" type="cb278263dab32db5540cacce-text/javascript"></script>
	<script src="https://ajax.cloudflare.com/cdn-cgi/scripts/7089c43e/cloudflare-static/rocket-loader.min.js" data-cf-settings="cb278263dab32db5540cacce-|49" defer=""></script>
 
</body>
</html>
