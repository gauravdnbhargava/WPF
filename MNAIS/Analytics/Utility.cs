using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;
using mnais = MNAIS.Utility;
using core = MNAIS.Analytics;

namespace MNAIS.Analytics
{
    /// <Summary>Utility Class for Analytical Module</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>
    
    public static class Utility
    {
        /// <summary>
        /// This method converts the input data from the UI understood format to the format understood by the Analytics.
        /// This method should be the first method to be called when analytics is run.
        /// </summary>
        /// <param name="importedData"></param>
        /// <param name="movingAvgYears"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> ConvertInputData(ObservableCollection<ImportedData> importedData, int movingAvgYears)
        {
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            int startYear = 0;            
            int endYear = 0;
            Int32.TryParse(importedData.First().YearData.First().Header, out startYear);
            Int32.TryParse(importedData.Last().YearData.Last().Header, out endYear);
            ObservableCollection<YearData> yieldData;

            foreach (ImportedData entry in importedData)
            {
                int noOfMovingAvgYears = movingAvgYears;                             
                yieldData = new ObservableCollection<YearData>();
                foreach (YearClass year in entry.YearData)
                {
                    int YearValue;
                    Int32.TryParse(year.Header,out YearValue);
                                        
                    if (year.Data == null)
                        yieldData.Add(new YearData() { Index = noOfMovingAvgYears, Value = mnais.Constants.Invalid, Year = YearValue, Error = year.Error });
                    else
                        yieldData.Add(new YearData() { Index = noOfMovingAvgYears, Value = year.Data, Year = YearValue, Error = year.Error });
                    
                    noOfMovingAvgYears++;
                }
                double areaSown = 0.0;
                System.Double.TryParse(entry.AreaSown, out areaSown);
                returnData.Add(new ModifiedUserData() { AreaSown = areaSown, BlockName = entry.BlockName, Data = yieldData });
            }            
            return returnData;
        }
                
        /// <summary>
        /// Calculate Correlation between 2 arrays
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <returns></returns>
        internal static double? Correlation(double?[] Xs, double?[] Ys)
        {
            double? sumX = 0;
            double? sumX2 = 0;
            double? sumY = 0;
            double? sumY2 = 0;
            double? sumXY = 0;

            int n = Xs.Length < Ys.Length ? Xs.Length : Ys.Length;
            int count = 0;

            for (int i = 0; i < n; ++i)
            {
                double? x = Xs[i];
                double? y = Ys[i];

                if ((x != mnais.Constants.Invalid) && (y != mnais.Constants.Invalid))
                {
                    sumX += x;
                    sumX2 += x * x;
                    sumY += y;
                    sumY2 += y * y;
                    sumXY += x * y;
                }
                else
                {
                    count++;
                }
            }

            n = n - count;

            double? stdX = SquareRoot(sumX2 / n - sumX * sumX / n / n);
            double? stdY = SquareRoot(sumY2 / n - sumY * sumY / n / n);
            double? covariance = (sumXY / n - sumX * sumY / n / n);

            double? result = covariance / stdX / stdY;

            if (double.IsNaN(Double(result)))
                result = 1;

            return result;
        }

        /// <summary>
        /// Generate simulation points wrt the user selection.
        /// </summary>
        /// <param name="simulationPoints"></param>
        /// <returns></returns>
        internal static List<double> GetSimulationPoints(SimulationPoints simulationPoints)
        {
            double NoOfPoints = ConvertSimulationPoints(simulationPoints);
            double interval = 1 / NoOfPoints;
            double startPoint = interval / 2;            
            double point = startPoint;

            List<double> points = new List<double>();
            
            for (int i = 0; i < NoOfPoints; i++)
            {
                points.Add(point);                
                point = point + interval;
            }

            return points;
        }

        /// <summary>
        /// Calculate Mean for values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static double? Mean(this IEnumerable<double?> values)
        {
            double? mean = 0;
            int n = 0;

            foreach (double? val in values)
            {
                if (val != mnais.Constants.Invalid)
                {
                    n++;
                    double? delta = val - mean;
                    mean += delta / n;
                }
            }

            return mean;
        }

        /// <summary>
        /// Calculate Standard Deviation for the values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static double? StdDev(this IEnumerable<double?> values)
        {            
            double? mean = 0.0;
            double? sum = 0.0;
            double? stdDev = 0.0;
            int n = 0;

            foreach (double? val in values)
            {
                if (val != mnais.Constants.Invalid)
                {
                    n++;
                    double? delta = val - mean;
                    mean += delta / n;
                    sum += delta * (val - mean);
                }
            }

            if (1 < n)
                stdDev =  SquareRoot((sum / (n - 1)));

            return stdDev;
        }

        /// <summary>
        /// Compute Indemnity level
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        internal static double ConvertIndemnity(IndemnityLevel selectedValue)
        {
            switch (selectedValue)
            {
                case IndemnityLevel.Seventy:
                    return 0.7;
                    
                case  IndemnityLevel.Eighty:
                    return 0.8;
                    
                case IndemnityLevel.Ninety:
                    return 0.9;                    

                default:
                    return 0.0;
            }            
        }

        /// <summary>
        /// Convert Simulation Points
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static double ConvertSimulationPoints(SimulationPoints value)
        {
            switch (value)
            {
                case SimulationPoints.FiveHundred:
                    return 500;

                case SimulationPoints.OneThousand:
                    return 1000;

                case SimulationPoints.TwoThousand:
                    return 2000;
                    
                default:
                    return 0;
            }
        }
        
        /// <summary>
        /// Sort YearData according to user selection.
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="isDescending"></param>
        internal static void SortData(ObservableCollection<ModifiedUserData> userData, bool isDescending)
        {            
            ObservableCollection<YearData> yearData;

            if (isDescending)
            {
                foreach (ModifiedUserData entry in userData)
                {
                    yearData = new ObservableCollection<YearData>();
                    IEnumerable<YearData> result = from data in entry.Data
                                                   orderby data.Year descending
                                                   select data;

                    entry.Data = new ObservableCollection<YearData>();
                    entry.Data.AddRange(result);
                }
            }
            else
            {
                foreach (ModifiedUserData entry in userData)
                {
                    yearData = new ObservableCollection<YearData>();
                    IEnumerable<YearData> result = from data in entry.Data
                                                   orderby data.Year
                                                   select data;

                    entry.Data = new ObservableCollection<YearData>();
                    entry.Data.AddRange(result);
                }
            }
        }

        /// <summary>
        /// Fill Static Trend table for trend calculation
        /// </summary>
        /// <returns></returns>
        internal static ObservableCollection<TrendTable> FillTrendTable()
        {
            ObservableCollection<TrendTable> trendTable = new ObservableCollection<TrendTable>();
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 1, TValue1 = 3.077684, TValue2 = 6.313752, TValue3 = 31.82052 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 2, TValue1 = 1.885618, TValue2 = 2.919986, TValue3 = 6.96456 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 3, TValue1 = 1.637744, TValue2 = 2.353363, TValue3 = 4.5407 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 4, TValue1 = 1.533206, TValue2 = 2.131847, TValue3 = 3.74695 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 5, TValue1 = 1.475884, TValue2 = 2.015048, TValue3 = 3.36493 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 6, TValue1 = 1.439756, TValue2 = 1.94318, TValue3 = 3.14267 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 7, TValue1 = 1.414924, TValue2 = 1.894579, TValue3 = 2.99795 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 8, TValue1 = 1.396815, TValue2 = 1.859548, TValue3 = 2.89646 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 9, TValue1 = 1.383029, TValue2 = 1.833113, TValue3 = 2.82144 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 10, TValue1 = 1.372184, TValue2 = 1.812461, TValue3 = 2.76377 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 11, TValue1 = 1.36343, TValue2 = 1.795885, TValue3 = 2.71808 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 12, TValue1 = 1.356217, TValue2 = 1.782288, TValue3 = 2.681 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 13, TValue1 = 1.350171, TValue2 = 1.770933, TValue3 = 2.65031 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 14, TValue1 = 1.34503, TValue2 = 1.76131, TValue3 = 2.62449 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 15, TValue1 = 1.340606, TValue2 = 1.75305, TValue3 = 2.60248 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 16, TValue1 = 1.336757, TValue2 = 1.745884, TValue3 = 2.58349 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 17, TValue1 = 1.333379, TValue2 = 1.739607, TValue3 = 2.56693 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 18, TValue1 = 1.330391, TValue2 = 1.734064, TValue3 = 2.55238 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 19, TValue1 = 1.327728, TValue2 = 1.729133, TValue3 = 2.53948 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 20, TValue1 = 1.325341, TValue2 = 1.724718, TValue3 = 2.52798 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 21, TValue1 = 1.323188, TValue2 = 1.720743, TValue3 = 2.51765 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 22, TValue1 = 1.321237, TValue2 = 1.717144, TValue3 = 2.50832 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 23, TValue1 = 1.31946, TValue2 = 1.713872, TValue3 = 2.49987 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 24, TValue1 = 1.317836, TValue2 = 1.710882, TValue3 = 2.49216 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 25, TValue1 = 1.316345, TValue2 = 1.708141, TValue3 = 2.48511 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 26, TValue1 = 1.314972, TValue2 = 1.705618, TValue3 = 2.47863 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 27, TValue1 = 1.313703, TValue2 = 1.703288, TValue3 = 2.47266 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 28, TValue1 = 1.312527, TValue2 = 1.701131, TValue3 = 2.46714 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 29, TValue1 = 1.311434, TValue2 = 1.699127, TValue3 = 2.46202 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 30, TValue1 = 1.310415, TValue2 = 1.697261, TValue3 = 2.45726 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 31, TValue1 = 1.309, TValue2 = 1.695, TValue3 = 2.453 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 32, TValue1 = 1.309, TValue2 = 1.694, TValue3 = 2.449 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 33, TValue1 = 1.308, TValue2 = 1.692, TValue3 = 2.445 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 34, TValue1 = 1.307, TValue2 = 1.691, TValue3 = 2.441 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 35, TValue1 = 1.306, TValue2 = 1.69, TValue3 = 2.438 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 36, TValue1 = 1.306, TValue2 = 1.688, TValue3 = 2.434 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 37, TValue1 = 1.305, TValue2 = 1.687, TValue3 = 2.431 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 38, TValue1 = 1.304, TValue2 = 1.686, TValue3 = 2.429 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 39, TValue1 = 1.304, TValue2 = 1.685, TValue3 = 2.426 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 40, TValue1 = 1.303, TValue2 = 1.684, TValue3 = 2.423 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 42, TValue1 = 1.302, TValue2 = 1.682, TValue3 = 2.418 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 44, TValue1 = 1.301, TValue2 = 1.68, TValue3 = 2.414 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 46, TValue1 = 1.3, TValue2 = 1.679, TValue3 = 2.41 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 48, TValue1 = 1.299, TValue2 = 1.677, TValue3 = 2.407 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 50, TValue1 = 1.299, TValue2 = 1.676, TValue3 = 2.403 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 60, TValue1 = 1.296, TValue2 = 1.671, TValue3 = 2.39 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 70, TValue1 = 1.294, TValue2 = 1.667, TValue3 = 2.381 });
            trendTable.Add(new TrendTable() { DegreeOfFreedom = 80, TValue1 = 1.292, TValue2 = 1.664, TValue3 = 2.374 });
            return trendTable;
        }

        /// <summary>
        /// Method to prevent Convert happening everywhere.
        /// </summary>
        /// <param name="value1">nullable double</param>
        /// <param name="value2">double</param>
        /// <returns></returns>
        internal static double Power(double? value1, double value2)
        {
            return Math.Pow(Double(value1), value2);
        }

        /// <summary>
        /// Stop Conversion from happening everywhere.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static double? SquareRoot(double? value)
        {
            return Math.Sqrt(Double(value));
        }

        /// <summary>
        /// Prevent Convert.ToDouble
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static double Double(double? value)
        {
            double returnValue = 0;
            System.Double.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }

        /// <summary>
        /// Return the double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static double Double(string value)
        {
            double returnValue = 0;
            System.Double.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }

        /// <summary>
        /// Extension method for adding values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        internal static IEnumerable<T> GetRange<T>(this ObservableCollection<T> collection, int skip, int take)
        {
            return collection.Skip(skip).Take(take);
        }

        /// <summary>
        /// Extension method for adding values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        internal static void AddRange<T>(this ObservableCollection<T> collection, ObservableCollection<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }

        /// <summary>
        /// Extension method for adding values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        internal static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }
        
        /// <summary>
        /// Write Temp files for Debugging purpose
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        internal static void WriteFile(ObservableCollection<ModifiedUserData> data, string path)
        {
            SortData(data, false);
            
            List<StringBuilder> items = new List<StringBuilder>();
            StringBuilder header = new StringBuilder();
            
            header.Append("Block Name,");
            header.Append("Area Sown,");
            header.Append("Mean,");
            header.Append("StdDev,");
            header.Append("Slope,");
            header.Append("Intercept,");
            header.Append("SST,");
            header.Append("SSR,");
            header.Append("SSE,");
            header.Append("RSquare,");
            header.Append("Sxx,");
            header.Append("TValue,");
            header.Append("MeanYield,");
            header.Append("MeanLossRatio,");
            header.Append("StdDevLossRatio,");
            header.Append("PML,");

            foreach (YearData yr in data.FirstOrDefault().Data)                
                 header.Append( yr.Year + ",");                        
            
            items.Add(header);
            StringBuilder values;

            foreach (ModifiedUserData entry in data)
            {
                values = new StringBuilder();
                values.Append(entry.BlockName + ",");
                values.Append(entry.AreaSown + ",");
                values.Append(entry.Mean + ",");
                values.Append(entry.StdDev + ",");
                values.Append(entry.Slope + ",");
                values.Append(entry.Intercept + ",");
                values.Append(entry.SST + ",");
                values.Append(entry.SSR + ",");
                values.Append(entry.SSE + ",");
                values.Append(entry.RSquare + ",");
                values.Append(entry.Sxx + ",");
                values.Append(entry.Tvalue + ",");
                values.Append(entry.MeanYield + ",");
                values.Append(entry.MeanLossRatio + ",");
                values.Append(entry.StdDevLossRatio + ",");
                values.Append(entry.PML + ",");
                
                foreach (YearData yrData in entry.Data)
                {
                    values.Append(yrData.Value + ",");     
                }
                items.Add(values);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (StringBuilder entry in items)            
                    writer.WriteLine(entry);
                
                writer.Close();
            }
        }

        /// <summary>
        /// Overloaded method to write simulation values.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        internal static void WriteFileForSimulation(ObservableCollection<ModifiedUserData> data, string path)
        {
            SortData(data, false);

            List<StringBuilder> items = new List<StringBuilder>();
            StringBuilder header = new StringBuilder();

            header.Append("Block Name,");
            header.Append("Area Sown,");
            header.Append("Mean,");
            header.Append("StdDev,");
            header.Append("Slope,");
            header.Append("Intercept,");
            header.Append("SST,");
            header.Append("SSR,");
            header.Append("SSE,");
            header.Append("RSquare,");
            header.Append("Sxx,");
            header.Append("TValue,");
            header.Append("MeanYield,");
            header.Append("MeanLossRatio,");
            header.Append("StdDevLossRatio,");
            header.Append("PML,");
            header.Append("ThresholdYield,");

            foreach (YearData yr in data.FirstOrDefault().Data)
                header.Append(yr.Year + ",");

            items.Add(header);
            StringBuilder values;

            foreach (ModifiedUserData entry in data)
            {
                values = new StringBuilder();
                values.Append(entry.BlockName + ",");
                values.Append(entry.AreaSown + ",");
                values.Append(entry.Mean + ",");
                values.Append(entry.StdDev + ",");
                values.Append(entry.Slope + ",");
                values.Append(entry.Intercept + ",");
                values.Append(entry.SST + ",");
                values.Append(entry.SSR + ",");
                values.Append(entry.SSE + ",");
                values.Append(entry.RSquare + ",");
                values.Append(entry.Sxx + ",");
                values.Append(entry.Tvalue + ",");
                values.Append(entry.MeanYield + ",");
                values.Append(entry.MeanLossRatio + ",");
                values.Append(entry.StdDevLossRatio + ",");
                values.Append(entry.PML + ",");
                values.Append(entry.ThresholdYield + ",");

                foreach (YearData yrData in entry.Data)
                {
                    values.Append(yrData.LossRatio + ",");
                }
                items.Add(values);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (StringBuilder entry in items)
                    writer.WriteLine(entry);

                writer.Close();
            }
        }

        /// <summary>
        /// Write the Results
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        internal static void WriteFile(ObservableCollection<Result> data, string path)
        {
            List<StringBuilder> items = new List<StringBuilder>();
            StringBuilder header = new StringBuilder();

            header.Append("Neq,");
            header.Append("DULFactor,");
            header.Append("DUL,");
            header.Append("Adjusted Mean Loss,");
            header.Append("PML,");
            header.Append("ROC,");
            header.Append("CAT Load,");
            header.Append("Technical Risk Premium,");
            header.Append("Gross Premium,");
            header.Append("Commercial Premium Rate,");

            items.Add(header);
            StringBuilder values;

            foreach (Result entry in data)
            {
                values = new StringBuilder();
                values.Append(entry.Neq + ",");
                values.Append(entry.DULFactor + ",");
                values.Append(entry.DUL + ",");
                values.Append(entry.AEL + ",");
                values.Append(entry.PML + ",");
                values.Append(entry.ROC + ",");
                values.Append(entry.CATLoad + ",");
                values.Append(entry.TRP + ",");
                values.Append(entry.GP + ",");
                values.Append(entry.CPR + ",");
                items.Add(values);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (StringBuilder entry in items)
                    writer.WriteLine(entry);

                writer.Close();
            }
        }

        /// <summary>
        /// Writes the correlation matrix to a file
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="path"></param>
        internal static void WriteCorrelationMatrixToFile(double?[,] matrix, string path)
        {
            int row, col;
            row = col = matrix.GetUpperBound(0);
            List<string> values = new List<string>();
            
            for (int i = 0; i <= row; i++)
                for (int j = 0; j <= col; j++)
                    values.Add(i.ToString() + "," + j.ToString() + "," + matrix[i, j].ToString());
                
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach(string entry in values)
                    writer.WriteLine(entry);

                writer.Close();
            }
        }

        /// <summary>
        /// Write Aggregated values to file.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="path"></param>
        internal static void WriteAggregationTofile(ModifiedUserData values, string path)
        {
            List<string> value = new List<string>();
            value.Add("Mean : " + "," + values.Mean.ToString());
            value.Add("StdDev : " + "," + values.StdDev.ToString());

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string entry in value)
                    writer.WriteLine(entry);

                writer.Close();
            }
        }
    } 
}