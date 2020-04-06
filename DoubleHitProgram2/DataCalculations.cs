using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleHitProgram2
{
    class DataCalculations
    {
        
        public static double[] CalculateTrackingCellBackground_Total(double[] Area, double[] Mean, double[] MeanBG, double[] FirstSignal, double[] SecondSignal)
        {
            
            double[] result = new double[Area.Length];

            for (int i = 0; i < Area.Length; i++)
            {
                result[i] = Area[i] * (Mean[i] - MeanBG[i]) - FirstSignal[i];

                if (SecondSignal != null)
                    result[i] -= SecondSignal[i];

            }

            return result;
        }
        public static double[] CalculateStaticCellBackground_Total(double[] Area, double[] Mean, double[] MeanBG)
        {           
            double[] result = new double[Area.Length];

            for (int i = 0; i < Area.Length; i++)
                result[i] = Area[i] * (Mean[i] - MeanBG[i]);

            return result;
        }
        public static double[] CalculateFirstSignal_Total(double[] Area, double[] Mean, double[] MeanBG, int MP)
        {  
            double[] result = new double[Area.Length];
            try
            {
                for (int i = 0; i < Area.Length; i++)
                    result[i] = Area[i] * (Mean[i] - MeanBG[i]);
            }
            catch
            {
                Console.WriteLine("Error 1!");
            }

            if (result.Length > MP)
            {
                double del = result[MP];
                if (del != 0)
                    for (int i = 0; i < Area.Length; i++)
                        result[i] -= del;
            }
            else
            {
                Console.WriteLine("MP frame is out of range!");
            }

            return result;
        }
        public static double[] CalculateSecondSignal_Total(double[] Area, double[] Mean, double[] MeanBG,int MP)
        {            
            double[] result = new double[Area.Length];

            int start = 0;
            for (start = 0; start < Area.Length; start++)
                if (Area[start] != 0)
                {
                    break;
                }

            try
            {
                for (int i = start; i < Area.Length; i++)
                    result[i] = Area[i] * (Mean[i] - MeanBG[i]);
            }
            catch
            {
                Console.WriteLine("Error 2!");
            }

            if (result.Length > start + MP)
            {
                double del = result[start + MP];
                if (del != 0)
                    for (int i = start; i < Area.Length; i++)
                        result[i] -= del;
            }
            else
            {
                Console.WriteLine("MP frame is out of range!");
            }

            return result;
        }
        /// <summary>
        /// Myltiply array by index
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double[] MultiplyArray(double[] input, double index)
        {
            double[] output = new double[input.Length];

            for (int i = 0; i < output.Length;i++)
                output[i] = input[i] * index;

            return output;
        }
        /// <summary>
        /// Devide array by index
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double[] DevideArray(double[] input, double index)
        {
            double[] output = new double[input.Length];

            for (int i = 0; i < output.Length; i++)
                if(index != 0)
                output[i] = input[i] / index;

            return output;
        }
        /// <summary>
        /// Sums the values from table
        /// </summary>
        /// <param name="input">double[y][x]</param>
        /// <returns></returns>
        public static double[] SumArray(double[][] input)
        {
            if (input == null) return null;

            double[] output = new double[input[0].Length];

            for (int i = 0; i < output.Length;i++)
                foreach (double[] column in input)
                    output[i] += column[i];

            return output;
        }
        /// <summary>
        /// Devides input1[i]/input2[i]
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static double[] DevideArrays(double[] input1, double[] input2)
        {
            double[] output = new double[input1.Length];

            for (int i = 0; i < output.Length; i++)
                if (input2[i] != 0)
                    output[i] = input1[i] / input2[i];

            return output;
        }
        /// <summary>
        /// Returns the maximal double value that accures in arrays
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double FindMaxInArrays(double[][] input)
        {
            double output = double.MinValue;
            
            foreach (double[] column in input)
                foreach (double value in column)
                    if (value > output)
                        output = value;

            return output;
        }
        public static string[] DoubleToStringArray(string title, double[] input)
        {
            string[] output = new string[input.Length + 1];

            output[0] = title;

            for (int i = 0; i < input.Length; i++)
                output[i + 1] = input[i].ToString();

            return output;
        }
    }
}
