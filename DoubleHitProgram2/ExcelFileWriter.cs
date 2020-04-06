using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace DoubleHitProgram2
{
    class ExcelFileWriter
    {
        public static void CreateWorkbook(string dir, string txtDir, Dictionary<string, Data> inputFiles)
        {            
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            try
            {
                Data MyData;
                string[][] results;
                // creating new WorkBook within Excel application
                // creating new Excelsheet in workbook

                Sheets xlSheets = null;

                xlSheets = workbook.Sheets as Sheets;

                // see the excel sheet behind the program
                app.Visible = false;

                for (int i = 0; i < inputFiles.Count; i++)
                {
                    MyData = inputFiles.ElementAt(i).Value;

                    var xlNewSheet = xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                    xlNewSheet.Name = MyData.FileName.Replace("_Ch0_Lime_Results", "");

                    results = MyData.GetResults;

                    for (int col = 0; col < results.Length; col++)
                        xlNewSheet.Cells[1, col + 1].Value2 = results[col][0];

                    var startCell = (Range)xlNewSheet.Cells[2, 1];
                    var endCell = (Range)xlNewSheet.Cells[results[0].Length, results.Length];
                    var writeRange = xlNewSheet.Range[startCell, endCell];

                    writeRange.Value2 = TranformArray(results);                                        
                }
                //add the summery
                if(File.Exists(txtDir))
                {
                    Microsoft.Office.Interop.Excel._Workbook csvWorkbook = app.Workbooks.Open(txtDir);
                    Microsoft.Office.Interop.Excel._Worksheet worksheetCSV = ((Microsoft.Office.Interop.Excel._Worksheet)csvWorkbook.Worksheets[1]);

                    worksheetCSV.Copy(xlSheets[1]);
                    xlSheets[1].Name = "Summery";

                    // Exit from the application
                    csvWorkbook.Close(false);
                }

                app.Visible = true;
                workbook.SaveAs(dir, XlFileFormat.xlWorkbookDefault, Type.Missing,
                Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
            }
            app.Visible = true;
            workbook.Close(false);
            app.Quit();
        }
        private static double[,] TranformArray(string[][] input)
        {
            double[,] output = new double[input[0].Length-1, input.Length];
            double val = 0;

            for (int column = 0; column < input.Length; column++)
                for (int row = 1; row < input[0].Length; row++)
                    if(double.TryParse(input[column][row],out val))
                    output[row-1, column] = val;

            return output;
        }
       
        /*
         * //Sample excel macro for adding charts
Sub Macro1()
'
' Macro1 Macro
'
' Keyboard Shortcut: Ctrl+q
'
         Dim WS_Count As Integer
         Dim I As Integer

         ' Set WS_Count equal to the number of worksheets in the active
         ' workbook.
         WS_Count = ActiveWorkbook.Worksheets.Count
Application.ScreenUpdating = False
         ' Begin the loop.
For I = 2 To WS_Count - 1
    ActiveWorkbook.Worksheets(I).Select
     Range("A:A,AT:AZ").Select
    Range("AT1").Activate
    ActiveSheet.Shapes.AddChart2(240, xlXYScatterSmoothNoMarkers).Select
    ActiveChart.SetSourceData Source:=Range( _
        "$A:$A,$AT:$AZ")
    Dim chartName As String
    chartName = ActiveChart.Parent.Name
    ActiveSheet.Shapes(chartName).ScaleWidth 1.19375, msoFalse, _
        msoScaleFromBottomRight
    ActiveSheet.Shapes(chartName).ScaleHeight 1.73784704, msoFalse, _
        msoScaleFromBottomRight
    ActiveSheet.Shapes(chartName).ScaleWidth 2.1169284468, msoFalse, _
        msoScaleFromTopLeft
    ActiveSheet.Shapes(chartName).ScaleHeight 1.386613532, msoFalse, _
        msoScaleFromTopLeft
    ActiveChart.ChartTitle.Select
    ActiveChart.ChartTitle.Text = "Concentration"
    Selection.Format.TextFrame2.TextRange.Characters.Text = "Concentration"
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).ParagraphFormat
        .TextDirection = msoTextDirectionLeftToRight
        .Alignment = msoAlignCenter
    End With
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).Font
        .BaselineOffset = 0
        .Bold = msoFalse
        .NameComplexScript = "+mn-cs"
        .NameFarEast = "+mn-ea"
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(89, 89, 89)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 14
        .Italic = msoFalse
        .Kerning = 12
        .Name = "+mn-lt"
        .UnderlineStyle = msoNoUnderline
        .Spacing = 0
        .Strike = msoNoStrike
    End With
    ActiveChart.ChartArea.Select

         Next I
Application.ScreenUpdating = True

End Sub
      
         */

    }
}
