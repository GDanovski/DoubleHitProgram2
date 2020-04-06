using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleHitProgram2
{
    class Data
    {
        private string _FileName;
        private int MP1;
        private int MP2;
        private bool UseOuterLayerROI = false;//change to true if you want to use the outer layer for bleach correction
        private byte UseMultiLayerROI = 0;//change to true for GretaPhosphorilationModel
        private bool UseStaticFree = false;
        private int[] matrix_foci1;
        private int[] matrix_foci2;
        private double[][] inputValues;
        private string[][] Results;
        //The following arrays hawe the total intensity
        private double[] CellStatic;
        private double[] CellTracking;
        private double[] Focus1;
        private double[] Focus2;
        private double[] Focus1_layer1;
        private double[] Focus1_layer2;
        private double[] Focus1_layer3;
        private double[] Focus1_layer4;
        private double[] Focus2_layer1;
        private double[] Focus2_layer2;
        private double[] Focus2_layer3;
        private double[] Focus2_layer4;
        //The following arrays are normalizet to 1 (Tracking + First + Sec = 1) for each frame
        private double[] nCellTracking;
        private double[] nFocus1;
        private double[] nFocus2;
        private double[] nFocus1_layer1;
        private double[] nFocus1_layer2;
        private double[] nFocus1_layer3;
        private double[] nFocus1_layer4;
        private double[] nFocus2_layer1;
        private double[] nFocus2_layer2;
        private double[] nFocus2_layer3;
        private double[] nFocus2_layer4;
        //The following arrays hav the concentration
        private double[] CellTracking_C;
        private double[] Focus1_C;
        private double[] Focus2_C;
        private double[] Focus1_layer1_C;
        private double[] Focus1_layer2_C;
        private double[] Focus1_layer3_C;
        private double[] Focus1_layer4_C;
        private double[] Focus2_layer1_C;
        private double[] Focus2_layer2_C;
        private double[] Focus2_layer3_C;
        private double[] Focus2_layer4_C;
        //The following arrays have the concentration normalizet to 1(where 1 is the max avaliable conc)
        private double[] nCellTracking_C;
        private double[] nFocus1_C;
        private double[] nFocus2_C;
        private double[] nFocus1_layer1_C;
        private double[] nFocus1_layer2_C;
        private double[] nFocus1_layer3_C;
        private double[] nFocus1_layer4_C;
        private double[] nFocus2_layer1_C;
        private double[] nFocus2_layer2_C;
        private double[] nFocus2_layer3_C;
        private double[] nFocus2_layer4_C;
        public string FileName
        {
            set
            {
                _FileName = value;
            }
            get
            {
                return _FileName;
            }
        }
        public byte GetUseMultiLayerROI
        {
            get
            {
                return UseMultiLayerROI;
            }
        }
        public string[][] GetResults
        {
            get
            {
                return this.Results;
            }
        }
        public int SetMP1
        {
            set
            {
                this.MP1 = value;
            }
        }
        public int SetMP2
        {
            set
            {
                this.MP2 = value;
            }
        }
        public bool SetUseStaticFree
        {
            set
            {
                this.UseStaticFree = value;
            }
        }
        public int[] SetMatrix_foci1
        {
            set
            {
                this.matrix_foci1 = value;
            }
        }
        public int[] SetMatrix_foci2
        {
            set
            {
                this.matrix_foci2 = value;
            }
        }
        public bool SetUseOuterLayerROI
        {
            set
            {
                this.UseOuterLayerROI = value;
            }
        }
        public enum ColumnIndexLayers
        {
            Time = 2,
            BackGround_Mean = 4,
            BackGround_Area = 3,
            FreeStatic_Mean = 8,
            FreeStatic_Area = 7,
            FreeTracking_Mean = 12,
            FreeTracking_Area = 11,
            FirstRoi_Mean = 16,
            FirstRoi_Area = 15,
            FirstRoi_Mean_Layer1 = 20,
            FirstRoi_Area_Layer1 = 19,
            FirstRoi_Mean_Layer2 = 36,
            FirstRoi_Area_Layer2 = 35,
            FirstRoi_Mean_Layer3 = 52,
            FirstRoi_Area_Layer3 = 51,
            SecondRoi_Mean = 68,
            SecondRoi_Area = 67,
            SecondRoi_Mean_Layer1 = 72,
            SecondRoi_Area_Layer1 = 71,
            SecondRoi_Mean_Layer2 = 88,
            SecondRoi_Area_Layer2 = 87,
            SecondRoi_Mean_Layer3 = 104,
            SecondRoi_Area_Layer3 = 103
        }
        public enum ColumnIndexLayers4
        {

            Time = 2,
            BackGround_Mean = 4,
            BackGround_Area = 3,
            FreeStatic_Mean = 8,
            FreeStatic_Area = 7,
            FreeTracking_Mean = 12,
            FreeTracking_Area = 11,
            FirstRoi_Mean = 16,
            FirstRoi_Area = 15,
            FirstRoi_Mean_Layer1 = 20,
            FirstRoi_Area_Layer1 = 19,
            FirstRoi_Mean_Layer2 = 36,
            FirstRoi_Area_Layer2 = 35,
            FirstRoi_Mean_Layer3 = 52,
            FirstRoi_Area_Layer3 = 51,
            FirstRoi_Mean_Layer4 = 68,
            FirstRoi_Area_Layer4 = 67,
            SecondRoi_Mean = 84,
            SecondRoi_Area = 83,
            SecondRoi_Mean_Layer1 = 88,
            SecondRoi_Area_Layer1 = 87,
            SecondRoi_Mean_Layer2 = 104,
            SecondRoi_Area_Layer2 = 103,
            SecondRoi_Mean_Layer3 = 120,
            SecondRoi_Area_Layer3 = 119,
            SecondRoi_Mean_Layer4 = 136,
            SecondRoi_Area_Layer4 = 135
        }
        public enum ColumnIndex
        {
            Time = 2,
            BackGround_Mean = 4,
            BackGround_Area = 3,
            FreeStatic_Mean = 8,
            FreeStatic_Area = 7,
            FreeTracking_Mean = 12,
            FreeTracking_Area = 11,
            FirstRoi_Mean = 16,
            FirstRoi_Area = 15,
            FirstRoi_Mean_Layer1 = 20,
            FirstRoi_Area_Layer1 = 19,
            SecondRoi_Mean = 36,
            SecondRoi_Area = 35,
            SecondRoi_Mean_Layer1 = 40,
            SecondRoi_Area_Layer1 = 39,
        }
        public enum Layer
        {
            None,
            LeftUp,
            RightUp,
            LeftDown,
            RightDown
        }
        public int GetColumnIndex(ColumnIndexLayers4 colInd, Layer layer = Layer.None)
        {
            if (layer == Layer.None)
                return (int)colInd + GetLayerColumn(layer);
            else
                return (int)colInd;
        }
        public int GetColumnIndex(ColumnIndexLayers colInd, Layer layer = Layer.None)
        {
            if (layer == Layer.None)
                return (int)colInd + GetLayerColumn(layer);
            else
                return (int)colInd;
        }
        public int GetColumnIndex(ColumnIndex colInd, Layer layer = Layer.None)
        {
            if (layer == Layer.None)
                return (int)colInd + GetLayerColumn(layer);
            else
                return (int)colInd;
        }
        private int GetLayerColumn(Layer layer)
        {
            switch (layer)
            {
                case Layer.None:
                    return 0;
                case Layer.LeftUp:
                    return 0;
                case Layer.RightUp:
                    return 4;
                case Layer.LeftDown:
                    return 8;
                case Layer.RightDown:
                    return 12;
                default:
                    return 0;
            }
        }
        public static Layer GetLayerFromInt(int ind)
        {
            switch (ind)
            {
                case 1:
                    return Layer.LeftUp;
                case 2:
                    return Layer.RightUp;
                case 3:
                    return Layer.LeftDown;
                case 4:
                    return Layer.RightDown;
                default:
                    return Layer.None;
            }
        }
        public void SetInputValues(string[] inputRows)
        {
            string[] vals;
            int RowLength = inputRows[0].Split(new string[] { "\t" }, StringSplitOptions.None).Length;

            if (RowLength < 103)
                this.UseMultiLayerROI = 0;
            else if (RowLength < 133)
                this.UseMultiLayerROI = 1;
            else
                this.UseMultiLayerROI = 2;

            double[][] output = new double[RowLength][];

            for (int i = 0; i < output.Length; i++)
                output[i] = new double[inputRows.Length - 1];

            for (int i = 0; i < inputRows.Length - 1; i++)
            {
                vals = inputRows[i + 1].Split(new string[] { "\t" }, StringSplitOptions.None);

                for (int j = 0; j < RowLength; j++)
                    double.TryParse(vals[j], out output[j][i]);
            }

            this.inputValues = output;
        }
        public double[] GetColumn(ColumnIndexLayers4 colInd, Layer layer = Layer.None)
        {
            int ind = GetColumnIndex(colInd, layer);

            if (ind < inputValues.Length)
                return inputValues[ind];
            else
                return null;
        }
        public double[] GetColumn(ColumnIndexLayers colInd, Layer layer = Layer.None)
        {
            int ind = GetColumnIndex(colInd, layer);

            if (ind < inputValues.Length)
                return inputValues[ind];
            else
                return null;
        }
        public double[] GetColumn(ColumnIndex colInd, Layer layer = Layer.None)
        {
            int ind = GetColumnIndex(colInd, layer);

            if (ind < inputValues.Length)
                return inputValues[ind];
            else
                return null;
        }
        public void ProcessFiles()
        {
            //assign values to the matrix if no matrix is present
            if (matrix_foci1 == null) SetMatrix_foci1 = new int[] { 1, 2, 3, 4 };
            if (matrix_foci2 == null) SetMatrix_foci2 = new int[] { 1, 2, 3, 4 };
            
            if (this.UseMultiLayerROI == 1)//calculate the values if multilayer roi is used
            {
                //Calculate layer roi intensities
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer1, out this.Focus1_layer1);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer2, out this.Focus1_layer2);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer3, out this.Focus1_layer3);

                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer1, out this.Focus2_layer1);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer2, out this.Focus2_layer2);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer3, out this.Focus2_layer3);
                //choose bleach correction roi for the foci
                double[] BleachCorectionROI_foci1 = this.UseOuterLayerROI ? this.Focus1_layer3 : GetColumn(ColumnIndexLayers.FreeStatic_Mean);
                double[] BleachCorectionROI_foci2 = this.UseOuterLayerROI ? this.Focus2_layer3 : GetColumn(ColumnIndexLayers.FreeStatic_Mean);
                //calculate total intensity
                this.CellStatic = DataCalculations.CalculateStaticCellBackground_Total(GetColumn(ColumnIndexLayers.FreeTracking_Area), GetColumn(ColumnIndexLayers.FreeStatic_Mean), GetColumn(ColumnIndexLayers.BackGround_Mean));

                this.Focus1 = DataCalculations.CalculateFirstSignal_Total(GetColumn(ColumnIndexLayers.FirstRoi_Area), GetColumn(ColumnIndexLayers.FirstRoi_Mean), BleachCorectionROI_foci1,this.MP1);
                this.Focus1_layer1 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer1,Layer.LeftDown),4d), this.Focus1_layer1, BleachCorectionROI_foci1, this.MP1);
                this.Focus1_layer2 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer2, Layer.LeftDown), 4d), this.Focus1_layer2, BleachCorectionROI_foci1, this.MP1);
                
                this.Focus2 = DataCalculations.CalculateSecondSignal_Total(GetColumn(ColumnIndexLayers.SecondRoi_Area), GetColumn(ColumnIndexLayers.SecondRoi_Mean), BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer1 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer1, Layer.LeftDown), 4d), this.Focus2_layer1, BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer2 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer2, Layer.LeftDown), 4d), this.Focus2_layer2, BleachCorectionROI_foci2, this.MP2);

                this.CellTracking = DataCalculations.CalculateTrackingCellBackground_Total(GetColumn(ColumnIndexLayers.FreeTracking_Area), GetColumn(ColumnIndexLayers.FreeTracking_Mean), GetColumn(ColumnIndexLayers.BackGround_Mean),
                    DataCalculations.SumArray(new double[][] { this.Focus1,this.Focus1_layer1,this.Focus1_layer2}), DataCalculations.SumArray(new double[][] { this.Focus2, this.Focus2_layer1, this.Focus2_layer2 }));

                BleachCorectionROI_foci1 = null;
                BleachCorectionROI_foci2 = null;
                //calculate normalized total intensity
                double[] total = DataCalculations.SumArray(new double[][] { this.UseStaticFree ? this.CellStatic : this.CellTracking,
                    this.Focus1, this.Focus1_layer1, this.Focus1_layer2, this.Focus2, this.Focus2_layer1, this.Focus2_layer2 });

                this.nCellTracking = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, total);

                this.nFocus1 = DataCalculations.DevideArrays(this.Focus1, total);
                this.nFocus1_layer1 = DataCalculations.DevideArrays(this.Focus1_layer1, total);
                this.nFocus1_layer2 = DataCalculations.DevideArrays(this.Focus1_layer2, total);

                this.nFocus2 = DataCalculations.DevideArrays(this.Focus2, total);
                this.nFocus2_layer1 = DataCalculations.DevideArrays(this.Focus2_layer1, total);
                this.nFocus2_layer2 = DataCalculations.DevideArrays(this.Focus2_layer2, total);

                total = null;
                //calculate concentration
                this.CellTracking_C = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, GetColumn(ColumnIndexLayers.FreeTracking_Area));

                this.Focus1_C = DataCalculations.DevideArrays(this.Focus1, GetColumn(ColumnIndexLayers.FirstRoi_Area));
                this.Focus1_layer1_C = DataCalculations.DevideArrays(this.Focus1_layer1, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer1),4));
                this.Focus1_layer2_C = DataCalculations.DevideArrays(this.Focus1_layer2, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer2), 4));

                this.Focus2_C = DataCalculations.DevideArrays(this.Focus2, GetColumn(ColumnIndexLayers.SecondRoi_Area));
                this.Focus2_layer1_C = DataCalculations.DevideArrays(this.Focus2_layer1, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer1), 4));
                this.Focus2_layer2_C = DataCalculations.DevideArrays(this.Focus2_layer2, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer2), 4));

                double freeFirstFrame =  this.CellTracking_C[0] != 0 ? this.CellTracking_C[0]:1d;

                this.CellTracking_C = DataCalculations.DevideArray(this.CellTracking_C,freeFirstFrame);

                this.Focus1_C = DataCalculations.DevideArray(this.Focus1_C, freeFirstFrame);
                this.Focus1_layer1_C = DataCalculations.DevideArray(this.Focus1_layer1_C, freeFirstFrame);
                this.Focus1_layer2_C = DataCalculations.DevideArray(this.Focus1_layer2_C, freeFirstFrame);

                this.Focus2_C = DataCalculations.DevideArray(this.Focus2_C, freeFirstFrame);
                this.Focus2_layer1_C = DataCalculations.DevideArray(this.Focus2_layer1_C, freeFirstFrame);
                this.Focus2_layer2_C = DataCalculations.DevideArray(this.Focus2_layer2_C, freeFirstFrame);

                //Normalize the concentration to 1
                double MaxValue = DataCalculations.FindMaxInArrays(new double[][] { this.CellTracking_C, this.Focus1_C , this.Focus1_layer1_C , this.Focus1_layer2_C, this.Focus2_C, this.Focus2_layer1_C, this.Focus2_layer2_C });

                this.nCellTracking_C = DataCalculations.DevideArray(this.CellTracking_C, MaxValue);

                this.nFocus1_C = DataCalculations.DevideArray(this.Focus1_C, MaxValue);
                this.nFocus1_layer1_C = DataCalculations.DevideArray(this.Focus1_layer1_C, MaxValue);
                this.nFocus1_layer2_C = DataCalculations.DevideArray(this.Focus1_layer2_C, MaxValue);

                this.nFocus2_C = DataCalculations.DevideArray(this.Focus2_C, MaxValue);
                this.nFocus2_layer1_C = DataCalculations.DevideArray(this.Focus2_layer1_C, MaxValue);
                this.nFocus2_layer2_C = DataCalculations.DevideArray(this.Focus2_layer2_C, MaxValue);
            }
            else if (this.UseMultiLayerROI == 2)//calculate the values if multilayer roi is used
            {
                //Calculate layer roi intensities
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer1, out this.Focus1_layer1);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer2, out this.Focus1_layer2);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer3, out this.Focus1_layer3);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer4, out this.Focus1_layer4);

                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer1, out this.Focus2_layer1);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer2, out this.Focus2_layer2);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer3, out this.Focus2_layer3);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer4, out this.Focus2_layer4);
                //choose bleach correction roi for the foci
                double[] BleachCorectionROI_foci1 = this.UseOuterLayerROI ? this.Focus1_layer4 : GetColumn(ColumnIndexLayers4.FreeStatic_Mean);
                double[] BleachCorectionROI_foci2 = this.UseOuterLayerROI ? this.Focus2_layer4 : GetColumn(ColumnIndexLayers4.FreeStatic_Mean);
                //calculate total intensity
                this.CellStatic = DataCalculations.CalculateStaticCellBackground_Total(GetColumn(ColumnIndexLayers4.FreeTracking_Area), GetColumn(ColumnIndexLayers4.FreeStatic_Mean), GetColumn(ColumnIndexLayers4.BackGround_Mean));

                this.Focus1 = DataCalculations.CalculateFirstSignal_Total(GetColumn(ColumnIndexLayers4.FirstRoi_Area), GetColumn(ColumnIndexLayers4.FirstRoi_Mean), BleachCorectionROI_foci1, this.MP1);
                this.Focus1_layer1 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer1, Layer.LeftDown), 4d), this.Focus1_layer1, BleachCorectionROI_foci1, this.MP1);
                this.Focus1_layer2 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer2, Layer.LeftDown), 4d), this.Focus1_layer2, BleachCorectionROI_foci1, this.MP1);
                this.Focus1_layer3 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer4, Layer.LeftDown), 4d), this.Focus1_layer3, BleachCorectionROI_foci1, this.MP1);
                this.Focus1_layer4 = DataCalculations.CalculateFirstSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer4, Layer.LeftDown), 4d), this.Focus1_layer4, BleachCorectionROI_foci1, this.MP1);

                this.Focus2 = DataCalculations.CalculateSecondSignal_Total(GetColumn(ColumnIndexLayers4.SecondRoi_Area), GetColumn(ColumnIndexLayers4.SecondRoi_Mean), BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer1 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer1, Layer.LeftDown), 4d), this.Focus2_layer1, BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer2 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer2, Layer.LeftDown), 4d), this.Focus2_layer2, BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer3 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer3, Layer.LeftDown), 4d), this.Focus2_layer3, BleachCorectionROI_foci2, this.MP2);
                this.Focus2_layer4 = DataCalculations.CalculateSecondSignal_Total(DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer4, Layer.LeftDown), 4d), this.Focus2_layer4, BleachCorectionROI_foci2, this.MP2);

                this.CellTracking = DataCalculations.CalculateTrackingCellBackground_Total(GetColumn(ColumnIndexLayers4.FreeTracking_Area), GetColumn(ColumnIndexLayers4.FreeTracking_Mean), GetColumn(ColumnIndexLayers4.BackGround_Mean),
                    DataCalculations.SumArray(new double[][] { this.Focus1, this.Focus1_layer1, this.Focus1_layer2, this.Focus1_layer3, this.Focus1_layer4 }), 
                    DataCalculations.SumArray(new double[][] { this.Focus2, this.Focus2_layer1, this.Focus2_layer2, this.Focus2_layer3, this.Focus2_layer4 }));

                BleachCorectionROI_foci1 = null;
                BleachCorectionROI_foci2 = null;
                //calculate normalized total intensity
                double[] total = DataCalculations.SumArray(new double[][] { this.UseStaticFree ? this.CellStatic : this.CellTracking,
                    this.Focus1, this.Focus1_layer1, this.Focus1_layer2, this.Focus1_layer3, this.Focus1_layer4, this.Focus2, this.Focus2_layer1, this.Focus2_layer2, this.Focus2_layer3, this.Focus2_layer4 });

                this.nCellTracking = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, total);

                this.nFocus1 = DataCalculations.DevideArrays(this.Focus1, total);
                this.nFocus1_layer1 = DataCalculations.DevideArrays(this.Focus1_layer1, total);
                this.nFocus1_layer2 = DataCalculations.DevideArrays(this.Focus1_layer2, total);
                this.nFocus1_layer3 = DataCalculations.DevideArrays(this.Focus1_layer3, total);
                this.nFocus1_layer4 = DataCalculations.DevideArrays(this.Focus1_layer4, total);

                this.nFocus2 = DataCalculations.DevideArrays(this.Focus2, total);
                this.nFocus2_layer1 = DataCalculations.DevideArrays(this.Focus2_layer1, total);
                this.nFocus2_layer2 = DataCalculations.DevideArrays(this.Focus2_layer2, total);
                this.nFocus2_layer3 = DataCalculations.DevideArrays(this.Focus2_layer3, total);
                this.nFocus2_layer4 = DataCalculations.DevideArrays(this.Focus2_layer4, total);

                total = null;
                //calculate concentration
                this.CellTracking_C = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, GetColumn(ColumnIndexLayers4.FreeTracking_Area));

                this.Focus1_C = DataCalculations.DevideArrays(this.Focus1, GetColumn(ColumnIndexLayers4.FirstRoi_Area));
                this.Focus1_layer1_C = DataCalculations.DevideArrays(this.Focus1_layer1, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer1), 4));
                this.Focus1_layer2_C = DataCalculations.DevideArrays(this.Focus1_layer2, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer2), 4));
                this.Focus1_layer3_C = DataCalculations.DevideArrays(this.Focus1_layer3, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer3), 4));
                this.Focus1_layer4_C = DataCalculations.DevideArrays(this.Focus1_layer4, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer4), 4));

                this.Focus2_C = DataCalculations.DevideArrays(this.Focus2, GetColumn(ColumnIndexLayers4.SecondRoi_Area));
                this.Focus2_layer1_C = DataCalculations.DevideArrays(this.Focus2_layer1, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer1), 4));
                this.Focus2_layer2_C = DataCalculations.DevideArrays(this.Focus2_layer2, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer2), 4));
                this.Focus2_layer3_C = DataCalculations.DevideArrays(this.Focus2_layer3, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer3), 4));
                this.Focus2_layer4_C = DataCalculations.DevideArrays(this.Focus2_layer4, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer4), 4));

                double freeFirstFrame = this.CellTracking_C[0] != 0 ? this.CellTracking_C[0] : 1d;

                this.CellTracking_C = DataCalculations.DevideArray(this.CellTracking_C, freeFirstFrame);

                this.Focus1_C = DataCalculations.DevideArray(this.Focus1_C, freeFirstFrame);
                this.Focus1_layer1_C = DataCalculations.DevideArray(this.Focus1_layer1_C, freeFirstFrame);
                this.Focus1_layer2_C = DataCalculations.DevideArray(this.Focus1_layer2_C, freeFirstFrame);
                this.Focus1_layer3_C = DataCalculations.DevideArray(this.Focus1_layer3_C, freeFirstFrame);
                this.Focus1_layer4_C = DataCalculations.DevideArray(this.Focus1_layer4_C, freeFirstFrame);

                this.Focus2_C = DataCalculations.DevideArray(this.Focus2_C, freeFirstFrame);
                this.Focus2_layer1_C = DataCalculations.DevideArray(this.Focus2_layer1_C, freeFirstFrame);
                this.Focus2_layer2_C = DataCalculations.DevideArray(this.Focus2_layer2_C, freeFirstFrame);
                this.Focus2_layer3_C = DataCalculations.DevideArray(this.Focus2_layer3_C, freeFirstFrame);
                this.Focus2_layer4_C = DataCalculations.DevideArray(this.Focus2_layer4_C, freeFirstFrame);

                //Normalize the concentration to 1
                double MaxValue = DataCalculations.FindMaxInArrays(new double[][] { this.CellTracking_C, this.Focus1_C, this.Focus1_layer1_C, this.Focus1_layer2_C, this.Focus1_layer3_C, this.Focus1_layer4_C
                    , this.Focus2_C, this.Focus2_layer1_C, this.Focus2_layer2_C, this.Focus2_layer3_C, this.Focus2_layer4_C });

                this.nCellTracking_C = DataCalculations.DevideArray(this.CellTracking_C, MaxValue);

                this.nFocus1_C = DataCalculations.DevideArray(this.Focus1_C, MaxValue);
                this.nFocus1_layer1_C = DataCalculations.DevideArray(this.Focus1_layer1_C, MaxValue);
                this.nFocus1_layer2_C = DataCalculations.DevideArray(this.Focus1_layer2_C, MaxValue);
                this.nFocus1_layer3_C = DataCalculations.DevideArray(this.Focus1_layer3_C, MaxValue);
                this.nFocus1_layer4_C = DataCalculations.DevideArray(this.Focus1_layer4_C, MaxValue);

                this.nFocus2_C = DataCalculations.DevideArray(this.Focus2_C, MaxValue);
                this.nFocus2_layer1_C = DataCalculations.DevideArray(this.Focus2_layer1_C, MaxValue);
                this.nFocus2_layer2_C = DataCalculations.DevideArray(this.Focus2_layer2_C, MaxValue);
                this.nFocus2_layer3_C = DataCalculations.DevideArray(this.Focus2_layer3_C, MaxValue);
                this.nFocus2_layer4_C = DataCalculations.DevideArray(this.Focus2_layer4_C, MaxValue);
            }
            else if (this.UseMultiLayerROI == 0)
            {
                //Calculate layer roi intensities
                CalculateLayersMean(matrix_foci1, ColumnIndex.FirstRoi_Mean_Layer1, out this.Focus1_layer1);

                CalculateLayersMean(matrix_foci2, ColumnIndex.SecondRoi_Mean_Layer1, out this.Focus2_layer1);
                //choose bleach correction roi for the foci
                double[] BleachCorectionROI_foci1 = this.UseOuterLayerROI ? this.Focus1_layer1 : GetColumn(ColumnIndex.FreeStatic_Mean);
                double[] BleachCorectionROI_foci2 = this.UseOuterLayerROI ? this.Focus2_layer1 : GetColumn(ColumnIndex.FreeStatic_Mean);
                //calculate total intensity
                this.CellStatic = DataCalculations.CalculateStaticCellBackground_Total(GetColumn(ColumnIndex.FreeTracking_Area), GetColumn(ColumnIndex.FreeStatic_Mean), GetColumn(ColumnIndex.BackGround_Mean));

                this.Focus1 = DataCalculations.CalculateFirstSignal_Total(GetColumn(ColumnIndex.FirstRoi_Area), GetColumn(ColumnIndex.FirstRoi_Mean), BleachCorectionROI_foci1, this.MP1);
                
                this.Focus2 = DataCalculations.CalculateSecondSignal_Total(GetColumn(ColumnIndex.SecondRoi_Area), GetColumn(ColumnIndex.SecondRoi_Mean), BleachCorectionROI_foci2, this.MP2);
                
                this.CellTracking = DataCalculations.CalculateTrackingCellBackground_Total(GetColumn(ColumnIndex.FreeTracking_Area), GetColumn(ColumnIndex.FreeTracking_Mean), GetColumn(ColumnIndex.BackGround_Mean),
                    this.Focus1,  this.Focus2);

                BleachCorectionROI_foci1 = null;
                BleachCorectionROI_foci2 = null;
                //calculate normalized total intensity
                double[] total = DataCalculations.SumArray(new double[][] { this.UseStaticFree ? this.CellStatic : this.CellTracking, this.Focus1, this.Focus2 });

                this.nCellTracking = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, total);

                this.nFocus1 = DataCalculations.DevideArrays(this.Focus1, total);

                this.nFocus2 = DataCalculations.DevideArrays(this.Focus2, total);

                total = null;
                //calculate concentration
                this.CellTracking_C = DataCalculations.DevideArrays(this.UseStaticFree ? this.CellStatic : this.CellTracking, GetColumn(ColumnIndex.FreeTracking_Area));

                this.Focus1_C = DataCalculations.DevideArrays(this.Focus1, GetColumn(ColumnIndex.FirstRoi_Area));

                this.Focus2_C = DataCalculations.DevideArrays(this.Focus2, GetColumn(ColumnIndex.SecondRoi_Area));

                double freeFirstFrame = this.CellTracking_C[0] != 0 ? this.CellTracking_C[0] : 1d;

                this.CellTracking_C = DataCalculations.DevideArray(this.CellTracking_C, freeFirstFrame);

                this.Focus1_C = DataCalculations.DevideArray(this.Focus1_C, freeFirstFrame);

                this.Focus2_C = DataCalculations.DevideArray(this.Focus2_C, freeFirstFrame);

                //Normalize the concentration to 1
                double MaxValue = DataCalculations.FindMaxInArrays(new double[][] { this.CellTracking_C, this.Focus1_C, this.Focus2_C });

                this.nCellTracking_C = DataCalculations.DevideArray(this.CellTracking_C, MaxValue);

                this.nFocus1_C = DataCalculations.DevideArray(this.Focus1_C, MaxValue);

                this.nFocus2_C = DataCalculations.DevideArray(this.Focus2_C, MaxValue);
            }
            CalculateResultsinTable();
        }
        private void CalculateLayersMean(int[] foci1_ind, ColumnIndexLayers4 foci_column, out double[] output)
        {
            List<double[]> foci_list = new List<double[]>();

            foreach (int ind in foci1_ind)
                foci_list.Add(GetColumn(foci_column, GetLayerFromInt(ind)));

            if (foci_list.Count == 0 || foci_list[0] == null)
            {
                output = null;
                return;
            }

            output = new double[foci_list[0].Length];

            for (int i = 0; i < output.Length; i++)
            {
                foreach (double[] column in foci_list)
                    output[i] += column[i];

                output[i] /= foci_list.Count;
            }
        }
        private void CalculateLayersMean(int[] foci1_ind, ColumnIndexLayers foci_column, out double[] output)
        {
            List<double[]> foci_list = new List<double[]>();

            foreach (int ind in foci1_ind)
                foci_list.Add(GetColumn(foci_column, GetLayerFromInt(ind)));

            if (foci_list.Count == 0 || foci_list[0] == null)
            {
                output = null;
                return;
            }

            output = new double[foci_list[0].Length];

            for (int i = 0; i < output.Length; i++)
            {
                foreach (double[] column in foci_list)
                    output[i] += column[i];

                output[i] /= foci_list.Count;
            }
        }
        private void CalculateLayersMean(int[] foci1_ind, ColumnIndex foci_column, out double[] output)
        {
            List<double[]> foci_list = new List<double[]>();

            foreach (int ind in foci1_ind)
                foci_list.Add(GetColumn(foci_column, GetLayerFromInt(ind)));

            if (foci_list.Count == 0 || foci_list[0] == null)
            {
                output = null;
                return;
            }

            output = new double[foci_list[0].Length];

            for (int i = 0; i < output.Length; i++)
            {
                foreach (double[] column in foci_list)
                    output[i] += column[i];

                output[i] /= foci_list.Count;
            }
        }
        public void CalculateResultsinTable()
        {
            List<string[]> output = new List<string[]>();
            string freeprotType = UseStaticFree ? "Stat_" : "Track_";
            //add raw info
            if (this.UseMultiLayerROI == 1)
            {
                //Calculate layer roi intensities
                double[] Focus1_layer1_Mean;
                double[] Focus1_layer2_Mean;
                double[] Focus1_layer3_Mean;
                double[] Focus2_layer1_Mean;
                double[] Focus2_layer2_Mean;
                double[] Focus2_layer3_Mean;

                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer1, out Focus1_layer1_Mean);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer2, out Focus1_layer2_Mean);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers.FirstRoi_Mean_Layer3, out Focus1_layer3_Mean);

                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer1, out Focus2_layer1_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer2, out Focus2_layer2_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers.SecondRoi_Mean_Layer3, out Focus2_layer3_Mean);

                output.Add(DataCalculations.DoubleToStringArray("T(sec)_" + FileName, GetColumn(ColumnIndexLayers.Time)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Area_" + FileName, GetColumn(ColumnIndexLayers.BackGround_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Area_" + FileName, GetColumn(ColumnIndexLayers.FreeStatic_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Area_" + FileName, GetColumn(ColumnIndexLayers.FreeTracking_Area)));

                output.Add(DataCalculations.DoubleToStringArray("F1_Area_" + FileName, GetColumn(ColumnIndexLayers.FirstRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer1),4d)));
                output.Add(DataCalculations.DoubleToStringArray("F1:2_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer2), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F1:3_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.FirstRoi_Area_Layer3), 4d)));

                output.Add(DataCalculations.DoubleToStringArray("F2_Area_" + FileName, GetColumn(ColumnIndexLayers.SecondRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer1), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F2:2_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer2), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F2:3_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers.SecondRoi_Area_Layer3), 4d)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Mean_" + FileName, GetColumn(ColumnIndexLayers.BackGround_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Mean_" + FileName, GetColumn(ColumnIndexLayers.FreeStatic_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Mean_" + FileName, GetColumn(ColumnIndexLayers.FreeTracking_Mean)));

                output.Add(DataCalculations.DoubleToStringArray("F1_Mean_" + FileName, GetColumn(ColumnIndexLayers.FirstRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Mean_" + FileName, Focus1_layer1_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F1:2_Mean_" + FileName, Focus1_layer2_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F1:3_Mean_" + FileName, Focus1_layer3_Mean));

                output.Add(DataCalculations.DoubleToStringArray("F2_Mean_" + FileName, GetColumn(ColumnIndexLayers.SecondRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Mean_" + FileName, Focus2_layer1_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F2:2_Mean_" + FileName, Focus2_layer2_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F2:3_Mean_" + FileName, Focus2_layer3_Mean));

            }
            else if (this.UseMultiLayerROI == 2)
            {
                //Calculate layer roi intensities
                double[] Focus1_layer1_Mean;
                double[] Focus1_layer2_Mean;
                double[] Focus1_layer3_Mean;
                double[] Focus1_layer4_Mean;
                double[] Focus2_layer1_Mean;
                double[] Focus2_layer2_Mean;
                double[] Focus2_layer3_Mean;
                double[] Focus2_layer4_Mean;

                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer1, out Focus1_layer1_Mean);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer2, out Focus1_layer2_Mean);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer3, out Focus1_layer3_Mean);
                CalculateLayersMean(matrix_foci1, ColumnIndexLayers4.FirstRoi_Mean_Layer3, out Focus1_layer4_Mean);

                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer1, out Focus2_layer1_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer2, out Focus2_layer2_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer3, out Focus2_layer3_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndexLayers4.SecondRoi_Mean_Layer4, out Focus2_layer4_Mean);
                
                output.Add(DataCalculations.DoubleToStringArray("T(sec)_" + FileName, GetColumn(ColumnIndexLayers4.Time)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Area_" + FileName, GetColumn(ColumnIndexLayers4.BackGround_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Area_" + FileName, GetColumn(ColumnIndexLayers4.FreeStatic_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Area_" + FileName, GetColumn(ColumnIndexLayers4.FreeTracking_Area)));
                
                output.Add(DataCalculations.DoubleToStringArray("F1_Area_" + FileName, GetColumn(ColumnIndexLayers4.FirstRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer1), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F1:2_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer2), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F1:3_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer3), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F1:4_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.FirstRoi_Area_Layer4), 4d)));
                
                output.Add(DataCalculations.DoubleToStringArray("F2_Area_" + FileName, GetColumn(ColumnIndexLayers4.SecondRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer1), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F2:2_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer2), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F2:3_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer3), 4d)));
                output.Add(DataCalculations.DoubleToStringArray("F2:4_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndexLayers4.SecondRoi_Area_Layer4), 4d)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Mean_" + FileName, GetColumn(ColumnIndexLayers4.BackGround_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Mean_" + FileName, GetColumn(ColumnIndexLayers4.FreeStatic_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Mean_" + FileName, GetColumn(ColumnIndexLayers4.FreeTracking_Mean)));

                output.Add(DataCalculations.DoubleToStringArray("F1_Mean_" + FileName, GetColumn(ColumnIndexLayers4.FirstRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Mean_" + FileName, Focus1_layer1_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F1:2_Mean_" + FileName, Focus1_layer2_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F1:3_Mean_" + FileName, Focus1_layer3_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F1:4_Mean_" + FileName, Focus1_layer4_Mean));

                output.Add(DataCalculations.DoubleToStringArray("F2_Mean_" + FileName, GetColumn(ColumnIndexLayers4.SecondRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Mean_" + FileName, Focus2_layer1_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F2:2_Mean_" + FileName, Focus2_layer2_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F2:3_Mean_" + FileName, Focus2_layer3_Mean));
                output.Add(DataCalculations.DoubleToStringArray("F2:4_Mean_" + FileName, Focus2_layer4_Mean));

            }
            else if (this.UseMultiLayerROI == 0)
            {
                //Calculate layer roi intensities
                double[] Focus1_layer1_Mean;
                double[] Focus2_layer1_Mean;
                CalculateLayersMean(matrix_foci1, ColumnIndex.FirstRoi_Mean_Layer1, out Focus1_layer1_Mean);
                CalculateLayersMean(matrix_foci2, ColumnIndex.SecondRoi_Mean_Layer1, out Focus2_layer1_Mean);

                output.Add(DataCalculations.DoubleToStringArray("T(sec)_" + FileName, GetColumn(ColumnIndex.Time)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Area_" + FileName, GetColumn(ColumnIndex.BackGround_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Area_" + FileName, GetColumn(ColumnIndex.FreeStatic_Area)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Area_" + FileName, GetColumn(ColumnIndex.FreeTracking_Area)));

                output.Add(DataCalculations.DoubleToStringArray("F1_Area_" + FileName, GetColumn(ColumnIndex.FirstRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndex.FirstRoi_Area_Layer1),4)));

                output.Add(DataCalculations.DoubleToStringArray("F2_Area_" + FileName, GetColumn(ColumnIndex.SecondRoi_Area)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Area_" + FileName, DataCalculations.MultiplyArray(GetColumn(ColumnIndex.SecondRoi_Area_Layer1),4)));

                output.Add(DataCalculations.DoubleToStringArray("BG_Mean_" + FileName, GetColumn(ColumnIndex.BackGround_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeStat_Mean_" + FileName, GetColumn(ColumnIndex.FreeStatic_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("FreeTrack_Mean_" + FileName, GetColumn(ColumnIndex.FreeTracking_Mean)));

                output.Add(DataCalculations.DoubleToStringArray("F1_Mean_" + FileName, GetColumn(ColumnIndex.FirstRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F1:1_Mean_" + FileName, Focus1_layer1_Mean));

                output.Add(DataCalculations.DoubleToStringArray("F2_Mean_" + FileName, GetColumn(ColumnIndex.SecondRoi_Mean)));
                output.Add(DataCalculations.DoubleToStringArray("F2:1_Mean_" + FileName, Focus2_layer1_Mean));

            }
            //The following arrays hawe the total intensity
            output.Add(DataCalculations.DoubleToStringArray("nFreeStat_" + FileName, CellStatic));
            output.Add(DataCalculations.DoubleToStringArray("nFreeTrack_" + FileName, CellTracking));
            output.Add(DataCalculations.DoubleToStringArray("nF1_" + FileName, Focus1));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nF1:1_" + FileName, Focus1_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nF1:2_" + FileName, Focus1_layer2));
            }
            else if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nF1:1_" + FileName, Focus1_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nF1:2_" + FileName, Focus1_layer2));
                output.Add(DataCalculations.DoubleToStringArray("nF1:3_" + FileName, Focus1_layer3));
                output.Add(DataCalculations.DoubleToStringArray("nF1:4_" + FileName, Focus1_layer4));
            }
            output.Add(DataCalculations.DoubleToStringArray("nF2_" + FileName, Focus2));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nF2:1_" + FileName, Focus2_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nF2:2_" + FileName, Focus2_layer2));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("nF2:1_" + FileName, Focus2_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nF2:2_" + FileName, Focus2_layer2));
                output.Add(DataCalculations.DoubleToStringArray("nF2:3_" + FileName, Focus2_layer3));
                output.Add(DataCalculations.DoubleToStringArray("nF2:4_" + FileName, Focus2_layer4));
            }
            //The following arrays are normalizet to 1 (Tracking + First + Sec = 1) for each frame
            output.Add(DataCalculations.DoubleToStringArray("nnFree" + freeprotType + FileName, nCellTracking));
            output.Add(DataCalculations.DoubleToStringArray("nnF1_" + FileName, nFocus1));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nnF1:1_" + FileName, nFocus1_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nnF1:2_" + FileName, nFocus1_layer2));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("nnF1:1_" + FileName, nFocus1_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nnF1:2_" + FileName, nFocus1_layer2));
                output.Add(DataCalculations.DoubleToStringArray("nnF1:3_" + FileName, nFocus1_layer3));
                output.Add(DataCalculations.DoubleToStringArray("nnF1:4_" + FileName, nFocus1_layer4));
            }
            output.Add(DataCalculations.DoubleToStringArray("nnF2_" + FileName, nFocus2));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nnF2:1_" + FileName, nFocus2_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nnF2:2_" + FileName, nFocus2_layer2));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("nnF2:1_" + FileName, nFocus2_layer1));
                output.Add(DataCalculations.DoubleToStringArray("nnF2:2_" + FileName, nFocus2_layer2));
                output.Add(DataCalculations.DoubleToStringArray("nnF2:3_" + FileName, nFocus2_layer3));
                output.Add(DataCalculations.DoubleToStringArray("nnF2:4_" + FileName, nFocus2_layer4));
            }
            //The following arrays hav the concentration
            output.Add(DataCalculations.DoubleToStringArray("C_Free" + freeprotType + FileName, CellTracking_C));
            output.Add(DataCalculations.DoubleToStringArray("C_F1_" + FileName, Focus1_C));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("C_F1:1_" + FileName, Focus1_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:2_" + FileName, Focus1_layer2_C));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("C_F1:1_" + FileName, Focus1_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:2_" + FileName, Focus1_layer2_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F1:3_" + FileName, Focus1_layer3_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:4_" + FileName, Focus1_layer4_C));
            }
            output.Add(DataCalculations.DoubleToStringArray("C_F2_" + FileName, Focus2_C));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("C_F2:1_" + FileName, Focus2_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:2_" + FileName, Focus2_layer2_C));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("C_F2:1_" + FileName, Focus2_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:2_" + FileName, Focus2_layer2_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:3_" + FileName, Focus2_layer3_C));
                output.Add(DataCalculations.DoubleToStringArray("C_F2:4_" + FileName, Focus2_layer4_C));
            }
            //The following arrays have the concentration normalizet to 1(where 1 is the max avaliable conc)
            output.Add(DataCalculations.DoubleToStringArray("nC_Free" + freeprotType + FileName, nCellTracking_C));
            output.Add(DataCalculations.DoubleToStringArray("nC_F1_" + FileName, nFocus1_C));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:1" + FileName, nFocus1_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:2" + FileName, nFocus1_layer2_C));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:1" + FileName, nFocus1_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:2" + FileName, nFocus1_layer2_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:3" + FileName, nFocus1_layer3_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F1:4" + FileName, nFocus1_layer4_C));
            }
            output.Add(DataCalculations.DoubleToStringArray("nC_F2_" + FileName, nFocus2_C));
            if (this.UseMultiLayerROI == 1)
            {
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:1_" + FileName, nFocus2_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:2_" + FileName, nFocus2_layer2_C));
            }
            else if (this.UseMultiLayerROI == 2)
            {
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:1_" + FileName, nFocus2_layer1_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:2_" + FileName, nFocus2_layer2_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:3_" + FileName, nFocus2_layer3_C));
                output.Add(DataCalculations.DoubleToStringArray("nC_F2:4_" + FileName, nFocus2_layer4_C));
            }
            this.Results = output.ToArray();
        }
    }
}
