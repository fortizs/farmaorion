using System;
using System.Data;
using NPOI.SS.UserModel;

namespace Farmacia 
{
	public static class HelperExcel
	{
		public static void GenerarHoja(ref IWorkbook workbook, DataTable Lista, String NombreHoja = "Hoja1")
		{
			var Hoja = workbook.CreateSheet(NombreHoja);
			IDataFormat format = workbook.CreateDataFormat();
			//---Estilo Fecha---------------------
			ICellStyle sFecha = workbook.CreateCellStyle();
			sFecha.DataFormat = format.GetFormat("dd/mm/yyyy");
			sFecha.Alignment = HorizontalAlignment.Center;
			sFecha.VerticalAlignment = VerticalAlignment.Center;
			//---Estilo Cabecera----------------
			ICellStyle sCabecera = workbook.CreateCellStyle();
			sCabecera.Alignment = HorizontalAlignment.Center;
			sCabecera.VerticalAlignment = VerticalAlignment.Center;
			IFont hlFont = workbook.CreateFont();
			hlFont.Boldweight = (short)FontBoldWeight.Bold;
			sCabecera.SetFont(hlFont);
			//---Estilo Centrar-----------------
			ICellStyle sCentrarCelda = workbook.CreateCellStyle();
			sCentrarCelda.Alignment = HorizontalAlignment.Center;
			sCentrarCelda.VerticalAlignment = VerticalAlignment.Center;
			//---Estilo Número---------------------
			ICellStyle sNumero = workbook.CreateCellStyle();
			sNumero.DataFormat = format.GetFormat("#,##0.00");
			ICellStyle sNumeroEntero = workbook.CreateCellStyle();
			sNumeroEntero.DataFormat = format.GetFormat("#,##0");
			sNumeroEntero.Alignment = HorizontalAlignment.Center;
			sNumeroEntero.VerticalAlignment = VerticalAlignment.Center;
			using (Lista)
			{
				IRow headerRow = Hoja.CreateRow(0);
				String[] TipoDatoColumna = new String[Lista.Columns.Count];
				//Cabecera
				foreach (DataColumn column in Lista.Columns)
				{
					headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
					headerRow.Cells[column.Ordinal].CellStyle = sCabecera;
					Hoja.AutoSizeColumn(column.Ordinal);
					Hoja.SetColumnWidth(column.Ordinal, Hoja.GetColumnWidth(column.Ordinal) + 512);
					TipoDatoColumna[column.Ordinal] = GetTypeColumnDT(column.DataType);
				}

				int rowIndex = 1;
				foreach (DataRow row in Lista.Rows)
				{
					IRow dataRow = Hoja.CreateRow(rowIndex);
					foreach (DataColumn column in Lista.Columns)
					{
						switch (TipoDatoColumna[column.Ordinal])
						{
							case "String":
								dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
								break;
							case "Numeric":
								dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column]));
								dataRow.Cells[column.Ordinal].CellStyle = sNumero;
								break;
							case "Int":
								dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column]));
								dataRow.Cells[column.Ordinal].CellStyle = sNumeroEntero;
								break;
							case "DateTime":
								dataRow.CreateCell(column.Ordinal).CellStyle = sFecha;
								dataRow.Cells[column.Ordinal].SetCellValue(Convert.ToDateTime(row[column]));
								break;
						}
					}
					rowIndex++;
				}
			}
		}

		private static String GetTypeColumnDT(Type DataType)
		{
			String retorno = "";
			TypeCode yourTypeCode = Type.GetTypeCode(DataType);
			switch (yourTypeCode)
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
					retorno = "Int";
					break;
				case TypeCode.Double:
				case TypeCode.Decimal:
					retorno = "Numeric";
					break;
				case TypeCode.Boolean:
					retorno = "Bool";
					break;
				case TypeCode.DateTime:
					retorno = "DateTime";
					break;
				case TypeCode.String:
					retorno = "String";
					break;
				case TypeCode.Empty:
					retorno = "Null";
					break;
				default: // TypeCode.DBNull, TypeCode.Char and TypeCode.Object
					retorno = "Unknown";
					break;
			}
			return retorno;
		}
	}
}