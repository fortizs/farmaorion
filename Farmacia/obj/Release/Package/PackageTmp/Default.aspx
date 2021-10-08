<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Farmacia.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
	<script src="https://code.highcharts.com/highcharts.js"></script>
	<script src="https://code.highcharts.com/modules/exporting.js"></script>
	<script src="https://code.highcharts.com/modules/accessibility.js"></script>





	<style>
		#chartdiv1, #chartdiv2, #chartdiv4 {
			width: 100%;
			height: 250px;
		}

		#chartdiv3 {
			width: 100%;
			height: 400px;
		}

		.text-semibold {
			font-weight: bold;
			font-size: 1.2em;
		}

		.text-2x {
			font-weight: bolder;
			font-size: 1.8em;
		}

		.panel-colorful {
			background-color: #b7213b;
		}

		.pad-all {
			background-color: whitesmoke;
			color: #22a6b3;
		}
	</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPrincipal" runat="server">
	<div class="layout-px-spacing">
	</div> 
</asp:Content>

