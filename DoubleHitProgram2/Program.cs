using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoubleHitProgram2
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintInfo();
            string input;
            Dictionary<string, Data> inputFiles = new Dictionary<string, Data>();
            do
            {
                Console.WriteLine("Select input directory:");
                input = Console.ReadLine();

                if (!input.EndsWith(@"\")) input += @"\";
            }
            while (!Directory.Exists(input));

            int MP1 = 0;
            do
            {
                Console.WriteLine("Foci 1 - Micropoint at frame:");
            }
            while (!int.TryParse(Console.ReadLine(), out MP1));
            Console.WriteLine();

            int MP2 = 0;
            do
            {
                Console.WriteLine("Foci 2 - Micropoint at frame:");
            }
            while (!int.TryParse(Console.ReadLine(), out MP2));
            Console.WriteLine();

            try
            {
                Console.WriteLine("Do you want to use last layer for bleach correction? (Y/N)");
                bool UseOuterLayerROI = Console.ReadKey().Key == ConsoleKey.Y ? true : false;
                Console.WriteLine();
                Console.WriteLine("Do you want to use the information from the static free protein ROI for normalization to 1? (Y/N)");
                bool UseStaticFree = Console.ReadKey().Key == ConsoleKey.Y ? true : false;
                Console.WriteLine();
                Console.WriteLine("Finding files...");
                GetAllFiles("_Results.txt", input, inputFiles);
                GetLayerMatrix(input, inputFiles);
                //PrintFiles(inputFiles);
                Console.WriteLine("Processing files...");
                ProcessFiles(inputFiles, UseOuterLayerROI, UseStaticFree, MP1, MP2);

                input += "Results.txt";
                Console.WriteLine();
                Console.WriteLine("Saving file to " + input + "...");
                SaveFileWithResults(input, inputFiles);
                Console.WriteLine();

                string excelDir = input.Replace(".txt", ".xlsx");
                Console.WriteLine("Saving summery file to file to " + excelDir + "...");

                ExcelFileWriter.CreateWorkbook(excelDir, input, inputFiles);
            }
            catch (Exception e)
            {
                Console.WriteLine("Errpor!\t" + e.Message);
            }
            Console.WriteLine("Done!");

            if (inputFiles.ElementAt(0).Value.GetUseMultiLayerROI == 1)
                Console.WriteLine(ExelMacro_Layer3);
            else if (inputFiles.ElementAt(0).Value.GetUseMultiLayerROI == 2)
                Console.WriteLine(ExelMacro_Layer4);
            
            Console.ReadLine();

        }
        private static void PrintInfo()
        {
            Console.WriteLine(@"Input: Results files from CellTool;
ROIs:   1 - BG; 
        2 - Static Cell BG; 
        3 - Tracking Cell BG; 
        4 - First hit - Tracking Oval with 3 or one layers; 
        5 - Second Hit - Tracking Oval with 3 or one layers (zeros before the second video);
matrix.txt:
        contains the folowing :
        (Name without extention)    tab 1234    tab 1234
        where 1234 is the selected roi layer side:
        1 - Left Up;
        2 - Right Up;
        3 - Left Down;
        4 - Right Down;
-------------------------------------------------------------------------------------------
Program setup:
        Select input directory: directory with all the images (matrix.txt must be in the 
        main directory - not in the subfolders);

        Micropoint at frame:    Index of freme after witch is the MP (zero-based);

        Do you want to use last layer for bleach correction? (Y/N):  select Y for using 
        the last layer for bleach correction and N - for using the 2nd roi;

        Do you want to use the information from the static free protein ROI for normalization 
        to 1? (Y/N):  choose Y to use the 2nd roi and N to use the 3th roi for total intensity 
        normalization to 1;
-------------------------------------------------------------------------------------------
Output:
        FreeTrack - Mean of Free tracking;
        FreeStat - mean of Free static;
        Fi - mean of foci;
        Fi:j - mean of layer;
        
        nFreeTrack - total of Free tracking - (FreeTrack*AreaTrack - nFi - nFi;j);
        nFreeStat - total of Free static - FreeStat*AreaTrack;
        nFi - total of foci - Fi*AreaFi;
        nFi:j - total of layer - Fi:j*AreaFi:j;

        nnFreeTrack - total of Free tracking normalized to 1 - nFreeTrack/(nFree + Sum(nFi) + Sum (nFi:j));
        nnFreeStat - total of Free static normalized to 1 - nFreeStat/(nFree + Sum(nFi) + Sum (nFi:j));
        nnFi - total of foci normalized to 1 - nFi/(nFree + Sum(nFi) + Sum (nFi:j));
        nnFi:j - total of layer normalized to 1 - nFi:j/(nFree + Sum(nFi) + Sum (nFi:j));

        C_FreeTrack - concentration of free protein - nnFreeTrack/AreaTrack;
        C_FreeStat - concentration of free protein - nnFreeStat/AreaTrack;
        C_Fi - concentration of protein in focus - nnFi/Area;
        C_Fi:j - concentration of protein in layer - nnFi:j/Area;

        nC_FreeTrack - normalized concentration of free protein - C_FreeTrack / MAX(C_FreeTrack, C_FreeStat, C_Fi, C_Fi:j);
        nC_FreeStat - normalized concentration of free protein - C_FreeStat / MAX(C_FreeTrack, C_FreeStat, C_Fi, C_Fi:j);
        nC_Fi - normalized concentration of protein in focus - C_Fi / MAX(C_FreeTrack, C_FreeStat, C_Fi, C_Fi:j);
        nC_Fi:j - normalized concentration of protein in layer - C_Fi:j / MAX(C_FreeTrack, C_FreeStat, C_Fi, C_Fi:j);
-------------------------------------------------------------------------------------------");
        }
        private static string ExelMacro_Layer4
        {
            get
            {
                return @"

-------------------------------------------------------------------------------------------



         Dim WS_Count As Integer
         Dim I As Integer

         WS_Count = ActiveWorkbook.Worksheets.Count
Application.ScreenUpdating = False
        
For I = 2 To WS_Count - 1
    ActiveWorkbook.Worksheets(I).Select
     Range('A:A,BF:BP').Select
    Range('AT1').Activate
    ActiveSheet.Shapes.AddChart2(240, xlXYScatterSmoothNoMarkers).Select
    ActiveChart.SetSourceData Source:=Range( _
        '$A:$A,$BF:$BP')
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
    ActiveChart.ChartTitle.Text = 'Concentration'
    Selection.Format.TextFrame2.TextRange.Characters.Text = 'Concentration'
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).ParagraphFormat
        .TextDirection = msoTextDirectionLeftToRight
        .Alignment = msoAlignCenter
    End With
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).Font
        .BaselineOffset = 0
        .Bold = msoFalse
        .NameComplexScript = '+mn-cs'
        .NameFarEast = '+mn-ea'
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(89, 89, 89)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 14
        .Italic = msoFalse
        .Kerning = 12
        .Name = '+mn-lt'
        .UnderlineStyle = msoNoUnderline
        .Spacing = 0
        .Strike = msoNoStrike
    End With
    ActiveChart.ChartArea.Select

         Next I
Application.ScreenUpdating = True

-------------------------------------------------------------------------------------------


".Replace("'", "\"");
            }
        }
        private static string ExelMacro_Layer3
        {
            get
            {
                return @"

-------------------------------------------------------------------------------------------



         Dim WS_Count As Integer
         Dim I As Integer

         WS_Count = ActiveWorkbook.Worksheets.Count
Application.ScreenUpdating = False
         
For I = 2 To WS_Count - 1
    ActiveWorkbook.Worksheets(I).Select
     Range('A:A,AT:AZ').Select
    Range('AT1').Activate
    ActiveSheet.Shapes.AddChart2(240, xlXYScatterSmoothNoMarkers).Select
    ActiveChart.SetSourceData Source:=Range( _
        '$A:$A,$AT:$AZ')
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
    ActiveChart.ChartTitle.Text = 'Concentration'
    Selection.Format.TextFrame2.TextRange.Characters.Text = 'Concentration'
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).ParagraphFormat
        .TextDirection = msoTextDirectionLeftToRight
        .Alignment = msoAlignCenter
    End With
    With Selection.Format.TextFrame2.TextRange.Characters(1, 13).Font
        .BaselineOffset = 0
        .Bold = msoFalse
        .NameComplexScript = '+mn-cs'
        .NameFarEast = '+mn-ea'
        .Fill.Visible = msoTrue
        .Fill.ForeColor.RGB = RGB(89, 89, 89)
        .Fill.Transparency = 0
        .Fill.Solid
        .Size = 14
        .Italic = msoFalse
        .Kerning = 12
        .Name = '+mn-lt'
        .UnderlineStyle = msoNoUnderline
        .Spacing = 0
        .Strike = msoNoStrike
    End With
    ActiveChart.ChartArea.Select

         Next I
Application.ScreenUpdating = True


-------------------------------------------------------------------------------------------


".Replace("'","\"");
            }
        }
        private static void GetAllFiles(string ext, string dir, Dictionary<string, Data> inputFiles)
        {
            foreach (string file in Directory.GetFiles(dir))
                if (file.EndsWith(ext))
                    inputFiles.Add(file, new Data());

            foreach (string folder in Directory.GetDirectories(dir))
                GetAllFiles(ext, folder, inputFiles);
        }
        private static void PrintFiles(List<string> inputFiles)
        {
            foreach (string str in inputFiles)
                Console.WriteLine(Path.GetFileNameWithoutExtension(str));
        }
        private static void ProcessFiles(Dictionary<string, Data> inputFiles, bool UseOuterLayerROI, bool UseStaticFree,int MP1, int MP2)
        {

            Parallel.ForEach(inputFiles, (file) => {
                file.Value.FileName = Path.GetFileNameWithoutExtension(file.Key);
                file.Value.SetMP1 = MP1;
                file.Value.SetMP2 = MP2;
                file.Value.SetUseOuterLayerROI = UseOuterLayerROI;
                file.Value.SetUseStaticFree = UseStaticFree;
                file.Value.SetInputValues(File.ReadAllLines(file.Key));
                file.Value.ProcessFiles();
            });
        }
        private static void GetLayerMatrix(string dir, Dictionary<string, Data> inputFiles)
        {
            if (!File.Exists(dir + @"\matrix.txt"))
            {
                Console.WriteLine("Missing file: " + dir + @"\matrix.txt");
                return;
            }

            string[] input = File.ReadAllLines(dir + @"\matrix.txt");

            Dictionary<string, string[]> matrix = new Dictionary<string, string[]>();

            foreach(string row in input)
            {
                string[] vals = row.Split(new string[] { "\t" }, StringSplitOptions.None);
                
                if(vals.Length == 3)
                    matrix.Add(vals[0], vals);
            }

            foreach(KeyValuePair<string, Data> kvp in inputFiles)               
            {
                string filename = Path.GetFileNameWithoutExtension(kvp.Key).Replace("_Ch0_Lime_Results", "");//Change the extention from here
                if (!matrix.Keys.Contains(filename))
                {
                    Console.WriteLine("Missing file: " + filename);
                    continue;
                }

                kvp.Value.SetMatrix_foci1 = StringToIntArray(matrix[filename][1]);
                kvp.Value.SetMatrix_foci2 = StringToIntArray(matrix[filename][2]);
            }
        }
        private static int[] StringToIntArray(string input)
        {
            int[] output = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
                int.TryParse(input[i].ToString(), out output[i]);
            
            return output;
        }
        private static void SaveFileWithResults(string dir, Dictionary<string, Data> inputFiles)
        {
            int first = 0;
            int last = inputFiles.Count-1;
            int totalLength = last+2;

            string[] output = new string[FindMaxColumnLength(inputFiles)];
            string[] row = new string[inputFiles.Count+2];
            string[] Avg;
            string[] StDev;

            for (int i = 0; i < inputFiles.ElementAt(0).Value.GetResults.Length; i++)
            {
                string sufix = inputFiles.ElementAt(0).Value.GetResults[i][0].Replace("_"+inputFiles.ElementAt(0).Value.FileName, "");
                Avg = GetExcelCommand("Avg_" + sufix, "AVERAGE", first, last, output.Length);
                StDev = GetExcelCommand("StDev_" + sufix, "STDEV.S", first, last, output.Length);

                for (int j = 0; j < output.Length; j++)
                {
                    for (int cell = 0; cell < inputFiles.Count; cell++)
                        if (inputFiles.ElementAt(cell).Value.GetResults[i].Length > j)
                        {
                            row[cell] = inputFiles.ElementAt(cell).Value.GetResults[i][j];
                        }
                        else
                        {
                            row[cell] = "";
                        }

                    row[row.Length - 2] = Avg[j];
                    row[row.Length - 1] = StDev[j];

                    output[j] += string.Join("\t", row)+"\t";
                }

                first += totalLength+1;
                last += totalLength+1;
            }

            File.WriteAllLines(dir, output);
        }
        private static int FindMaxColumnLength(Dictionary<string, Data> inputFiles)
        {
            int output = int.MinValue;

            foreach (var kvp in inputFiles)
                if (kvp.Value.GetResults[0].Length > output)
                    output = kvp.Value.GetResults[0].Length;

            return output;
        }
        private static string GetColumnName(int index) // zero-based
        {
            const byte BASE = 'Z' - 'A' + 1;
            string name = String.Empty;

            do
            {
                name = Convert.ToChar('A' + index % BASE) + name;
                index = index / BASE - 1;
            }
            while (index >= 0);

            return name;
        }
        private static string[] GetExcelCommand(string title, string command , int first, int last, int length)
        {
            string[] output = new string[length];
            output[0] = title;
            string prefix = "=" + command + "(" + GetColumnName(first);
            string suffix = ":" + GetColumnName(last);

            for (int i = 1; i < length; i++)
                output[i] = prefix + (i + 1).ToString() + suffix + (i + 1).ToString() + ")";

            return output;
        }
    }
}
