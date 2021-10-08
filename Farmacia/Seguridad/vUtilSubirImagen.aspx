<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vUtilSubirImagen.aspx.cs" Inherits="Farmacia.Seguridad.vUtilSubirImagen" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<title></title>
	<link href="<%= ResolveClientUrl("~/Recursos/Font/icomoon/icomoon.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/componentes.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%= ResolveClientUrl("~/Recursos/assets/css/plugins.css") %>" rel="stylesheet" type="text/css" />

	<script src='<%= ResolveClientUrl("~/Recursos/assets/js/libs/jquery-3.1.1.min.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/sweetalert/sweetalert.js") %>' type="text/javascript"></script>
	<script src='<%= ResolveClientUrl("~/Recursos/plugins/app.js") %>' type="text/javascript"></script>

	<style>
		body {
			background: #FFF;
		}

		.espacio {
			height: 6px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="smPrincipal" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ScriptMode="Release"></asp:ScriptManager>
		<asp:UpdatePanel ID="upSustento" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:HiddenField ID="hfIDPersona" runat="server" Value="0" />
				<asp:HiddenField ID="hfMaximoArchivoByte" runat="server" Value="80000000" />
				<div class="container">
					<div class="row">
						<div class="col-sm-10">
							<div class="alert alert-info">
								<strong>Adjuntar!</strong> Foto tamaño max.(128 x 128)
							</div>
						</div>
					</div>
					<div class="row" runat="server" id="DivLogo">
						<div class="col-md-12">
							<label>Archivo:</label>
							<div class="uploader">
								<asp:FileUpload ID="fuCarga" runat="server" Alt="Seleccionar" ClientIDMode="Static" CssClass="file-styled" Title="Seleccionar" onchange="fUploadValue('fusCargaNombreArchivo',this); " />
								<span id="fusCargaNombreArchivo" class="filename" style="-webkit-user-select: none;">Seleccionar...</span><span class="action btn btn-primary" style="-webkit-user-select: none;"><i class="icon-folder-search position-left"></i></span>
							</div>
						</div>
					</div>
					<div class="espacio"></div>
					<div class="row">
						<div class="col-sm-12 text-right">
							<div class="form-group">
								<asp:Button ID="btnProcesar" runat="server" Text="Guardar" OnClick="btnProcesar_Click" />
							</div>
						</div>
					</div>
				</div>
			</ContentTemplate>
			<Triggers>
				<asp:PostBackTrigger ControlID="btnProcesar" />
			</Triggers>
		</asp:UpdatePanel>
		<script>
			function fUploadValue(txtControlValue, pFUpload) {
				var nArchivo = pFUpload.value;
				//Validar Tipo Archivo----------------  
				var val = false;
				var ext = nArchivo.split('.').pop().toLowerCase();
				if ($.inArray(ext, ['exe', 'ddl', 'bat', 'js']) == -1) {
					val = true;
				}

				if (val == true) {
					if (nArchivo === "") {
						nArchivo = "...";
					} else {
						nArchivo = nArchivo.split(/[\/\\]+/);
						nArchivo = nArchivo[(nArchivo.length - 1)];
					}
					$("#" + txtControlValue).text(nArchivo);
				} else {
					pFUpload.value = '';
					$("#" + txtControlValue).text('Seleccionar...');
					Mensaje('warning', 'Archivo no válido');
				}
			}

			function CerrarModalImagen() {
				window.parent.CerrarModalImagen();
			}
		</script>
	</form>
</body>
</html>
