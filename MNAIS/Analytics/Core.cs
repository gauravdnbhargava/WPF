using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using mnais = MNAIS.Utility;
using core = MNAIS.Analytics;

namespace MNAIS.Analytics
{
    /// <Summary>Core Class for Analytical Module</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>

    public static class Core
    {
        /// <summary>
        /// This adds remaining years to the input data based on the contract year
        /// </summary>
        /// <param name="userdata"></param>
        /// <param name="contractYear"></param>
        static void AddYearsForHistoricalAnalysis(ObservableCollection<ModifiedUserData> userdata, int contractYear)
        {
            ObservableCollection<YearData> yearData;
            int yr = contractYear - 1;
            int count = yr - userdata.LastOrDefault().Data.LastOrDefault().Year;
            int tempIndex = userdata.LastOrDefault().Data.LastOrDefault().Index;

            bool addYears = false;

            if (yr != userdata.FirstOrDefault().Data.LastOrDefault().Year)
                addYears = true;

            foreach (ModifiedUserData entry in userdata)
            {
                yearData = new ObservableCollection<YearData>();
                int endYear = userdata.FirstOrDefault().Data.FirstOrDefault().Year - 1;
                int X = userdata.FirstOrDefault().Data.FirstOrDefault().Index - 1;
                for (int j = X; j >= 0; j--)
                {
                    yearData.Add(new YearData() { Year = endYear, Index = j, Value = mnais.Constants.Invalid });
                    endYear--;
                }
                entry.Data.AddRange(yearData);
            }

            if (addYears)
            {
                foreach (ModifiedUserData entry in userdata)
                {
                    yearData = new ObservableCollection<YearData>();
                    int endYr = yr;
                    int X = tempIndex + count;

                    for (int j = count; j > 0; j--)
                    {
                        yearData.Add(new YearData() { Year = endYr, Index = X, Value = mnais.Constants.Invalid });
                        endYr--;
                        X--;
                    }
                    entry.Data.AddRange(yearData);
                }
            }
        }

        /// <summary>
        /// Compute modelled yields for the user data
        /// </summary>
        /// <param name="userdata"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> ComputeModelledYields(ObservableCollection<ModifiedUserData> userdata, int startDate, int endDate)
        {
            ObservableCollection<YearData> yearData;
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();

            if (startDate > 0 && endDate > 0)
            {
                //add missing years to the input data
                AddYearsForHistoricalAnalysis(userdata, startDate);
                int blocksCount = userdata.Count;

                for (int i = 0; i < blocksCount; i++)
                {
                    int endYear = endDate - 1;
                    int X = userdata.Last().Data.Last().Index + (endYear - userdata.Last().Data.Last().Year);
                    yearData = new ObservableCollection<YearData>();

                    for (int j = X; j >= 0; j--)
                    {
                        yearData.Add(new YearData() { Year = endYear, Index = j });
                        endYear--;
                    }
                    returnData.Add(new ModifiedUserData() { Data = yearData, BlockName = userdata[i].BlockName });
                }

                foreach (ModifiedUserData entry in returnData)
                {
                    IEnumerable<ModifiedUserData> returnValue = from data in userdata
                                                                where data.BlockName == entry.BlockName
                                                                select data;

                    double tempIntercept = returnValue.First().Intercept;
                    double tempSlope = returnValue.First().Slope;

                    foreach (YearData year in entry.Data)
                    {
                        year.Value = (tempSlope * year.Index) + tempIntercept;
                    }
                }
            }
            else
            {
                foreach (ModifiedUserData entry in userdata)
                {
                    IEnumerable<ModifiedUserData> returnValue = from data in userdata
                                                                where data.BlockName == entry.BlockName
                                                                select data;

                    double tempIntercept = returnValue.First().Intercept;
                    double tempSlope = returnValue.First().Slope;
                    yearData = new ObservableCollection<YearData>();

                    foreach (YearData year in entry.Data)
                    {
                        yearData.Add(new YearData() { Value = (tempSlope * year.Index) + tempIntercept, Index = year.Index, Year = year.Year });
                    }

                    returnData.Add(new ModifiedUserData() { Data = yearData, BlockName = entry.BlockName });
                }
            }

            return returnData;
        }

        /// <summary>
        /// Compute SST SSR SSE R Square Sxx TValue
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="YModel"></param>
        public static void ComputeParams(ObservableCollection<ModifiedUserData> Y, ObservableCollection<ModifiedUserData> YModel)
        {
            if (YModel == null)
            {
                foreach (ModifiedUserData entry in Y)
                {
                    double? tempMean = 0;
                    int Count = 0;
                    double xy = 0;
                    double x = 0;
                    double y = 0;
                    double xsquare = 0;

                    foreach (YearData year in entry.Data)
                    {
                        if (year.Value != mnais.Constants.Invalid)
                        {
                            tempMean += year.Value;
                            Count++;
                            xy += Utility.Double(year.Value * year.Index);
                            x += year.Index;
                            y += Utility.Double(year.Value);
                            xsquare += year.Index * year.Index;
                        }
                    }
                    entry.Mean = Utility.Double(tempMean / Count);
                    entry.Slope = ((Count * xy) - (x * y)) / ((Count * xsquare) - (x * x));
                    entry.Intercept = (y - (entry.Slope * x)) / Count;
                }
            }
            else
            {
                if (Y.Count > 0)
                {
                    foreach (ModifiedUserData entry in Y)
                    {
                        double tempSST = 0;
                        foreach (YearData data in entry.Data)
                        {
                            tempSST += Utility.Power(data.Value - entry.Mean, 2);
                        }
                        entry.SST = tempSST;
                    }
                }
                else
                    throw new Exception("Error in the data");


                if (YModel.Count == Y.Count)
                {
                    //SSR = (YModel - YMean)^2                
                    foreach (KeyValuePair<ModifiedUserData, ModifiedUserData> entry in mnais.Utility.Zip(YModel, Y))
                    {
                        double tempSSR = 0;
                        foreach (KeyValuePair<YearData, YearData> data in mnais.Utility.Zip(entry.Key.Data, entry.Value.Data))
                        {
                            if (data.Key.Value != mnais.Constants.Invalid && data.Value.Value != mnais.Constants.Invalid)
                                tempSSR += Utility.Power(data.Key.Value - entry.Value.Mean, 2);
                        }
                        entry.Value.SSR = tempSSR;
                    }
                }
                else
                    throw new Exception("Error in the data");

                if (Y.Count == YModel.Count)
                {
                    //SSE = (Y- YModel)^2                
                    foreach (KeyValuePair<ModifiedUserData, ModifiedUserData> entry in mnais.Utility.Zip(Y, YModel))
                    {
                        double tempSSE = 0;
                        foreach (KeyValuePair<YearData, YearData> data in mnais.Utility.Zip(entry.Key.Data, entry.Value.Data))
                        {
                            if (data.Key.Value != mnais.Constants.Invalid && data.Value.Value != mnais.Constants.Invalid)
                                tempSSE += Utility.Power(data.Key.Value - data.Value.Value, 2);
                        }
                        entry.Key.SSE = tempSSE;
                    }
                }
                else
                    throw new Exception("Error in the data");

                if (Y.Count > 0)
                {
                    foreach (ModifiedUserData entry in Y)
                        entry.RSquare = 1 - (entry.SSE / entry.SST);
                }
                else
                    throw new Exception("Error in the data");

                if (Y.Count > 0)
                {
                    foreach (ModifiedUserData entry in Y)
                    {
                        double tempSXX = 0;
                        double tempYAdd = 0;
                        int count = 0;

                        foreach (YearData yr in entry.Data)
                        {
                            if (yr.Value != mnais.Constants.Invalid)
                            {
                                tempYAdd += yr.Index;
                                count++;
                            }
                        }

                        double XMean = tempYAdd / count;

                        foreach (YearData year in entry.Data)
                        {
                            if (year.Value != mnais.Constants.Invalid)
                                tempSXX += Utility.Power(year.Index - XMean, 2);
                        }
                        entry.Sxx = tempSXX;
                    }
                }
                else
                    throw new Exception("Error in the data");

                if (Y.Count > 0)
                {
                    foreach (ModifiedUserData entry in Y)
                    {
                        int count = 0;
                        foreach (YearData year in entry.Data)
                        {
                            if (year.Value != mnais.Constants.Invalid)
                                count++;
                        }
                        entry.Count = count - 2; //N-2
                        entry.Tvalue = Math.Abs(entry.Slope * Math.Sqrt(entry.Count * (entry.Sxx / entry.SSE)));
                    }
                }
                else
                    throw new Exception("Error in the data");
            }
        }

        /// <summary>
        /// Checks for Trend in the data and de trends the data for a pre selected significance.
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="contractYr"></param>
        /// <param name="significance"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> DetrendData(ObservableCollection<ModifiedUserData> Y, int contractYr, SignificanceForTrending significance)
        {
            ObservableCollection<TrendTable> trendTableData = Utility.FillTrendTable();
            Trend obj;

            foreach (ModifiedUserData entry in Y)
            {
                IEnumerable<TrendTable> trend = from data in trendTableData
                                                where data.DegreeOfFreedom == entry.Count
                                                select data;

                obj = new Trend();

                if (significance == SignificanceForTrending.Ninety)
                {
                    if (entry.Tvalue > trend.FirstOrDefault().TValue1)
                        obj.IsTrend = true;
                    else
                        obj.IsTrend = false;
                }
                else if (significance == SignificanceForTrending.Ninetyfive)
                {
                    if (entry.Tvalue > trend.FirstOrDefault().TValue2)
                        obj.IsTrend = true;
                    else
                        obj.IsTrend = false;
                }
                else if (significance == SignificanceForTrending.NientyNine)
                {
                    if (entry.Tvalue > trend.FirstOrDefault().TValue3)
                        obj.IsTrend = true;
                    else
                        obj.IsTrend = false;
                }

                if (entry.Tvalue < trend.FirstOrDefault().TValue1)
                    obj.Catagory = TrendCatagory.NoTrend;
                else if (entry.Tvalue > trend.FirstOrDefault().TValue1 && entry.Tvalue < trend.FirstOrDefault().TValue2)
                    obj.Catagory = TrendCatagory.Trend;
                else if (entry.Tvalue > trend.FirstOrDefault().TValue2 && entry.Tvalue < trend.FirstOrDefault().TValue3)
                    obj.Catagory = TrendCatagory.StrongTrend;
                else if (entry.Tvalue > trend.FirstOrDefault().TValue3)
                    obj.Catagory = TrendCatagory.VeryStrongTrend;

                entry.TrendStatus = obj;
            }

            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yearData;

            foreach (ModifiedUserData entry in Y)
            {
                yearData = new ObservableCollection<YearData>();
                foreach (YearData year in entry.Data)
                {
                    if (year.Value == mnais.Constants.Invalid)
                        yearData.Add(year);
                    else
                        yearData.Add(new YearData() { Value = year.Value + entry.Slope * (contractYr - year.Year), Index = year.Index, Year = year.Year });                        
                }

                returnData.Add(new ModifiedUserData() { Data = yearData, BlockName = entry.BlockName });
            }
            return returnData;
        }

        /// <summary>
        /// Generate Simulated Yield for the raw data.
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="detrendedData"></param>
        /// <param name="correlationInput"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> GenerateSimulatedYield(ObservableCollection<ModifiedUserData> rawData,
            ObservableCollection<ModifiedUserData> detrendedData, ResultYieldGraphs correlationInput)
        {
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yearData;
            List<double?> dataValues;

            if (correlationInput == ResultYieldGraphs.Raw)
            {
                foreach (ModifiedUserData entry in rawData)
                {
                    yearData = new ObservableCollection<YearData>();
                    dataValues = new List<double?>();
                    foreach (YearData yrData in entry.Data)
                    {
                        yearData.Add(yrData);
                        dataValues.Add(yrData.Value);
                    }
                    double mean = Utility.Double(dataValues.Mean());
                    double stdDev = Utility.Double(dataValues.StdDev());

                    returnData.Add(new ModifiedUserData() { BlockName = entry.BlockName, Data = yearData, Mean = mean, StdDev = stdDev, AreaSown= entry.AreaSown });
                }
            }
            else if (correlationInput == ResultYieldGraphs.Detrended)
            {
                foreach (KeyValuePair<ModifiedUserData, ModifiedUserData> entry in mnais.Utility.Zip(rawData, detrendedData))
                {
                    yearData = new ObservableCollection<YearData>();
                    dataValues = new List<double?>();
                    foreach (KeyValuePair<YearData, YearData> yearValue in mnais.Utility.Zip(entry.Key.Data, entry.Value.Data))
                    {
                        if (entry.Key.TrendStatus.IsTrend == true)
                        {
                            yearData.Add(new YearData() { Index = yearValue.Key.Index, Year = yearValue.Key.Year, Value = yearValue.Value.Value });
                            dataValues.Add(yearValue.Value.Value);
                        }
                        else
                        {
                            yearData.Add(yearValue.Key);
                            dataValues.Add(yearValue.Key.Value);
                        }
                    }

                    double mean = Utility.Double(dataValues.Mean());
                    double stdDev = Utility.Double(dataValues.StdDev());

                    returnData.Add(new ModifiedUserData() { BlockName = entry.Key.BlockName, Data = yearData, Mean = mean, StdDev = stdDev, AreaSown = entry.Key.AreaSown });
                }
            }
            return returnData;
        }

        /// <summary>
        /// Generate Historical Yield for the raw data
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="modelledData"></param>
        /// <param name="dataProcessed"></param>
        /// <param name="dataGaps"></param>
        /// <param name="startYear"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> GenerateHistoricalYield(ObservableCollection<ModifiedUserData> rawData,
            ObservableCollection<ModifiedUserData> modelledData, DataProcessed dataProcessed, DataGaps dataGaps, int startYear)
        {
            ObservableCollection<ModifiedUserData> returnData =
                new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yearData;

            if (dataProcessed == DataProcessed.Raw)
            {
                foreach (ModifiedUserData entry in rawData)
                {
                    yearData = new ObservableCollection<YearData>();

                    foreach (YearData year in entry.Data)
                    {
                        if (year.Value != mnais.Constants.Invalid)
                        {
                            yearData.Add(year); //use raw data
                        }
                        else
                        {
                            if (dataGaps == DataGaps.Filled)
                            {
                                if (entry.TrendStatus.IsTrend == true) //if trend is present
                                {
                                    //fill all missing years with modelled data
                                    if (year.Value == mnais.Constants.Invalid)
                                    {
                                        IEnumerable<ModifiedUserData> tempData = from data in modelledData
                                                                                 where data.BlockName == entry.BlockName
                                                                                 select data;

                                        IEnumerable<YearData> tempYearData = from data in tempData.FirstOrDefault().Data
                                                                             where data.Year == year.Year
                                                                             select data;

                                        yearData.Add(new YearData()
                                        {
                                            Index = year.Index,
                                            Year = year.Year,
                                            Value = tempYearData.FirstOrDefault().Value
                                        });
                                    }
                                }
                                else //if trend is not present
                                {
                                    //fill all missing years with an avg of raw yields for the block
                                    if (year.Value == mnais.Constants.Invalid)
                                    {
                                        yearData.Add(new YearData()
                                        {
                                            Index = year.Index,
                                            Year = year.Year,
                                            Value = entry.Mean
                                        });
                                    }
                                    else //non missing years with raw data
                                    {
                                        yearData.Add(year);
                                    }
                                }
                            }
                            else if (dataGaps == DataGaps.NotFilled)
                            {
                                //if year < start year then fill 
                                if (year.Year < startYear)
                                {
                                    if (entry.TrendStatus.IsTrend == true) //if trend is present
                                    {
                                        //fill all missing years with modelled data
                                        if (year.Value == mnais.Constants.Invalid)
                                        {
                                            IEnumerable<ModifiedUserData> tempData = from data in modelledData
                                                                                     where data.BlockName == entry.BlockName
                                                                                     select data;

                                            IEnumerable<YearData> tempYearData = from data in tempData.FirstOrDefault().Data
                                                                                 where data.Year == year.Year
                                                                                 select data;

                                            yearData.Add(new YearData()
                                            {
                                                Index = year.Index,
                                                Year = year.Year,
                                                Value = tempYearData.FirstOrDefault().Value
                                            });
                                        }
                                    }
                                    else //if trend is not present
                                    {
                                        //fill all missing years with an avg of raw yields for the block
                                        if (year.Value == mnais.Constants.Invalid)
                                        {
                                            yearData.Add(new YearData()
                                            {
                                                Index = year.Index,
                                                Year = year.Year,
                                                Value = entry.Mean
                                            });
                                        }
                                        else //non missing years with raw data
                                        {
                                            yearData.Add(year);
                                        }
                                    }
                                }
                                else
                                {
                                    //else do not fill
                                    yearData.Add(year);
                                }
                            }
                        }
                    }
                    returnData.Add(new ModifiedUserData() { BlockName = entry.BlockName, Data = yearData, AreaSown = entry.AreaSown });
                }
            }
            else if (dataProcessed == DataProcessed.Detrended)
            {
                foreach (ModifiedUserData entry in rawData)
                {
                    yearData = new ObservableCollection<YearData>();

                    if (entry.TrendStatus.IsTrend == true)
                    {
                        foreach (YearData year in entry.Data)
                        {
                            if (year.Value != mnais.Constants.Invalid)
                            {
                                //use de trended value
                                IEnumerable<ModifiedUserData> tempData = from data in modelledData
                                                                         where data.BlockName == entry.BlockName
                                                                         select data;

                                IEnumerable<YearData> tempYearData = from data in tempData.FirstOrDefault().Data
                                                                     where data.Year == year.Year
                                                                     select data;

                                yearData.Add(new YearData()
                                {
                                    Year = year.Year,
                                    Index = year.Index,
                                    Value = tempYearData.FirstOrDefault().Value
                                });
                            }
                            else
                            {
                                if (dataGaps == DataGaps.Filled)
                                {
                                    //fill with mean yield of fully de trended data set
                                    IEnumerable<ModifiedUserData> tempData = from data in modelledData
                                                                             where data.BlockName == entry.BlockName
                                                                             select data;

                                    yearData.Add(new YearData()
                                    {
                                        Year = year.Year,
                                        Index = year.Index,
                                        Value = tempData.FirstOrDefault().Mean
                                    });
                                }
                                else if (dataGaps == DataGaps.NotFilled)
                                {
                                    yearData.Add(year); //keep same value
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (YearData year in entry.Data)
                        {
                            if (year.Value != mnais.Constants.Invalid)
                            {
                                yearData.Add(year); //use raw data.
                            }
                            else
                            {
                                if (dataGaps == DataGaps.Filled)
                                {
                                    //fill missing data with mean yield of raw data set
                                    yearData.Add(new YearData()
                                    {
                                        Year = year.Year,
                                        Index = year.Index,
                                        Value = entry.Mean
                                    });
                                }
                                else if (dataGaps == DataGaps.NotFilled)
                                {
                                    if (year.Year < startYear)
                                    {
                                        //fill missing data with mean yield of raw data set
                                        yearData.Add(new YearData()
                                        {
                                            Year = year.Year,
                                            Index = year.Index,
                                            Value = entry.Mean
                                        });
                                    }
                                    else
                                    {
                                        yearData.Add(year); //keep same value
                                    }
                                }
                            }
                        }
                    }
                    returnData.Add(new ModifiedUserData() { BlockName = entry.BlockName, Data = yearData, AreaSown = entry.AreaSown });
                }
            }
            else if (dataProcessed == DataProcessed.MovingAverage)
            {
                throw new NotImplementedException("Not Supported Currently.");
            }

            return returnData;
        }

        /// <summary>
        /// Compute threshold yield for historical data
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="contractYear"></param>
        /// <param name="Nm"></param>
        /// <param name="CalamityYears"></param>
        /// <param name="selectedIndemnity"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> HistoricalLossAnalysisThresholdYield(ObservableCollection<ModifiedUserData> userData,
            int contractYear, int Nm, ObservableCollection<int> CalamityYears, IndemnityLevel selectedIndemnity)
        {            
            ObservableCollection<ModifiedUserData> returnValues = new ObservableCollection<ModifiedUserData>();
            IEnumerable<YearData> temp;
            Utility.SortData(userData, true);

            foreach (ModifiedUserData entry in userData)
            {
                int cntrctYr = contractYear;
                int skip = 0;
                double? aggregateValue = 0;

                ObservableCollection<YearData> yearData = new ObservableCollection<YearData>();

                do
                {
                    temp = entry.Data.GetRange(skip, Nm);
                    ObservableCollection<YearData> excludingCalamityYears = new ObservableCollection<YearData>();

                    foreach (YearData yr in temp)
                    {
                        bool isPresent = false;
                        foreach (int calamityYr in CalamityYears)
                        {
                            if (calamityYr == yr.Year)
                                isPresent = true;                            
                        }
                        if (!isPresent && yr.Value != mnais.Constants.Invalid)
                            excludingCalamityYears.Add(yr);
                    }

                    aggregateValue = 0;
                    foreach (YearData yrValue in excludingCalamityYears)
                        aggregateValue += yrValue.Value;
                                        
                    if (excludingCalamityYears.Count == 0)
                        yearData.Add(new YearData() { Value = mnais.Constants.Invalid, Year = cntrctYr });
                    else
                        yearData.Add(new YearData() { Value = Utility.ConvertIndemnity(selectedIndemnity) * (aggregateValue / excludingCalamityYears.Count), Year = cntrctYr });

                    skip = skip + 1;
                    cntrctYr = cntrctYr - 1;
                } while (temp.Count() == Nm);

                returnValues.Add(new ModifiedUserData() { BlockName = entry.BlockName, Data = yearData, AreaSown = entry.AreaSown });
            }

            return returnValues;
        }
             
        /// <summary>
        /// Threshold Yields for Simulation data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="calamityYears"></param>
        /// <param name="Nm"></param>
        /// <param name="indemnityLevel"></param>
        /// <returns></returns>
        public static ObservableCollection<ThresholdYield> SimulatedLossAnalysisThresholdYield(ObservableCollection<ModifiedUserData> data,
            ObservableCollection<int> calamityYears, int Nm, IndemnityLevel indemnityLevel)
        {
            ObservableCollection<ThresholdYield> returnData = new ObservableCollection<ThresholdYield>();
            Utility.SortData(data, true);
            IEnumerable<YearData> temp;

            foreach (ModifiedUserData entry in data)
            {
                temp = entry.Data.GetRange(0, Nm);
                ObservableCollection<YearData> excludingCalamityYears = new ObservableCollection<YearData>();
                double aggregateValue = 0;
                double thresholdYield = 0;

                foreach (YearData yr in temp)
                {
                    bool isPresent = false;
                    foreach (int calamityYr in calamityYears)
                    {
                        if (calamityYr == yr.Year)
                        {
                            isPresent = true;
                        }
                    }
                    if (!isPresent && yr.Value != mnais.Constants.Invalid)
                        excludingCalamityYears.Add(yr);
                }

                foreach (YearData yrValue in excludingCalamityYears)
                    aggregateValue += Utility.Double(yrValue.Value);

                thresholdYield = Utility.ConvertIndemnity(indemnityLevel) * (aggregateValue / excludingCalamityYears.Count);

                returnData.Add(new ThresholdYield() { BlockName = entry.BlockName, Yield = thresholdYield });
            }

            return returnData;
        }

        /// <summary>
        /// Compute the historical loss analysis
        /// </summary>
        /// <param name="ThresholdYield"></param>
        /// <param name="Yield"></param>
        /// <param name="contractYear"></param>
        /// <param name="dataStartYear"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> HistoricalLossAnalysis(ObservableCollection<ModifiedUserData> ThresholdYield,
            ObservableCollection<ModifiedUserData> Yield, int contractYear, int dataStartYear, PricingOptionsViewModel pricingVM)
        {
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yearData;

            foreach (ModifiedUserData entry in ThresholdYield)
            {
                yearData = new ObservableCollection<YearData>();
                foreach (YearData year in entry.Data)
                {
                    if (year.Year != contractYear && year.Year >= dataStartYear) //loop from contract year -1 to data start year
                    {
                        IEnumerable<ModifiedUserData> queryYield = from data in Yield
                                                                   where data.BlockName == entry.BlockName
                                                                   select data;

                        IEnumerable<YearData> queryYear = from data in queryYield.FirstOrDefault().Data
                                                          where data.Year == year.Year
                                                          select data;

                        if (year.Value == mnais.Constants.Invalid || queryYear.FirstOrDefault().Value == mnais.Constants.Invalid)
                        {
                            yearData.Add(new YearData() { Value = mnais.Constants.Invalid, Year = year.Year });
                        }
                        else if (queryYear.FirstOrDefault().Value > year.Value)
                        {
                            yearData.Add(new YearData() { Value = 0, Year = year.Year });
                        }
                        else
                        {
                            double? tempValue = (year.Value - queryYear.FirstOrDefault().Value) / year.Value;
 
                            if (pricingVM.SelectedFranchise == Franchise.Deductible)
                                yearData.Add(new YearData() { Year = year.Year, Value = tempValue - pricingVM.Franchise });                            
                            else
                                yearData.Add(new YearData() { Year = year.Year, Value = tempValue });                                                     
                        }
                    }
                }

                if (yearData.Count > 0)
                    returnData.Add(new ModifiedUserData() { BlockName = entry.BlockName, AreaSown = entry.AreaSown, Data = yearData });
            }


            //compute mean lossratio, stddev lossratio.            
            List<double?> lossratio;
            foreach (ModifiedUserData entry in returnData)
            {
                lossratio = new List<double?>();
                foreach (YearData yrData in entry.Data)
                {
                    if (yrData.Value != mnais.Constants.Invalid)
                        lossratio.Add(yrData.Value);
                }
                entry.MeanLossRatio = lossratio.Mean();
                entry.StdDevLossRatio = lossratio.StdDev();
            }

            return returnData;
        }

        /// <summary>
        /// District Level loss analaysis
        /// </summary>
        /// <param name="lossRatios"></param>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> HistoricalDistrictLossAnalysis(ObservableCollection<ModifiedUserData> lossRatios,
            ObservableCollection<ModifiedUserData> rawData)
        {
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yrData;
            double? lossRatio = 0;
            double Area = 0;

            Utility.SortData(lossRatios, false);

            int startYear = lossRatios.FirstOrDefault().Data.FirstOrDefault().Year;
            int endYear = lossRatios.LastOrDefault().Data.LastOrDefault().Year;

            yrData = new ObservableCollection<YearData>();

            for (int i = startYear; i <= endYear; i++)
            {
                lossRatio = 0;
                Area = 0;

                foreach (ModifiedUserData entry in lossRatios)
                {
                    IEnumerable<YearData> yearData = from data in entry.Data
                                                     where data.Year == i
                                                     select data;

                    IEnumerable<ModifiedUserData> userData = from data in rawData
                                                             where data.BlockName == entry.BlockName
                                                             select data;

                    if (userData.FirstOrDefault().AreaSown == 0 || yearData.FirstOrDefault().Value < 0)
                    { }
                    else
                    {
                        lossRatio += yearData.FirstOrDefault().Value * Utility.Double(userData.FirstOrDefault().AreaSown);
                        Area += Utility.Double(userData.FirstOrDefault().AreaSown);
                    }
                }

                yrData.Add(new YearData() { Value = lossRatio / Area, Year = i });
            }

            returnData.Add(new ModifiedUserData() { BlockName = "Total District", Data = yrData });
            return returnData;
        }

        /// <summary>
        /// Simulation calculation for the user data wrt threshold yield data
        /// </summary>
        /// <param name="thresholdYieldData"></param>
        /// <param name="userData"></param>
        /// <param name="selectedSimulation"></param>
        /// <param name="simulationPts"></param>
        /// <param name="pmlReturnPeriod"></param>
        /// <returns></returns>
        public static ObservableCollection<ModifiedUserData> Simulation(ObservableCollection<ThresholdYield> thresholdYieldData, 
            ObservableCollection<ModifiedUserData> userData,ModellingOptionsViewModel modellingVM, PricingOptionsViewModel pricingVM)
        {
            ObservableCollection<ModifiedUserData> returnData = new ObservableCollection<ModifiedUserData>();
            ObservableCollection<YearData> yearData;
            List<double> simulationPoints = Utility.GetSimulationPoints(modellingVM.SelectedSimulationPoint);

            foreach (ModifiedUserData entry in userData)
            {   
                //temp sorted set for min and max yields.
                SortedSet<double> minTemp = new SortedSet<double>();
                SortedSet<double> maxTemp = new SortedSet<double>();

                foreach (YearData data in entry.Data)
                {
                    if (data.Value != mnais.Constants.Invalid)
                    {
                        minTemp.Add(Utility.Double(data.Value));
                        maxTemp.Add(Utility.Double(data.Value));
                    }
                }

                //calculate min and max yields
                //TODO: Check formula
                double minYield = 0;
                double maxYield = maxTemp.LastOrDefault();

                IEnumerable<ThresholdYield> thresholdData = from data in thresholdYieldData
                                                            where data.BlockName == entry.BlockName
                                                            select data;

                double thresholdYield = thresholdData.FirstOrDefault().Yield;
                yearData = new ObservableCollection<YearData>();

                foreach (double pt in simulationPoints)
                {
                    YearData obj = new YearData();

                    //below Value is storing the simulation point, if needed to be separate then structure change to be done.
                    if (entry.Mean.ToString() == string.Empty || entry.StdDev.ToString() == string.Empty || thresholdYield == mnais.Constants.Invalid)
                    {
                        obj.Value = pt;
                        obj.LossRatio = mnais.Constants.Invalid;
                        obj.Yield = mnais.Constants.Invalid;
                    }
                    else
                    {
                        double tempYield = 0.0;

                        if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.Normal)
                            tempYield = NormInverse.NormInv(pt, entry.Mean, entry.StdDev);
                        else if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.LogNormal)
                        {
                            double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(entry.StdDev / entry.Mean, 2)));
                            double lnMean = Math.Log(entry.Mean) - Math.Pow(lnStdDev, 2) / 2;
 
                            double LogMin = 0;
                            double LogMax = 0;

                            if (modellingVM.SelectedExtremeBoundMethodology == ExtremeBoundMethod.Sigma)
                            {
                                LogMax = Math.Exp(lnMean + 4 * lnStdDev);//TODO: Check formula
                                LogMin = Math.Exp(lnMean - 4 * lnStdDev);//TODO: Check formula
                            }
                            else                
                            {
                                LogMin = minYield;
                                LogMax = maxYield;
                            }

                            tempYield = Math.Min(LogMax, Math.Max(LogMin, Math.Exp(NormInverse.NormInv(pt, lnMean, lnStdDev))));
                        }
                        else if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.Beta)
                        {
                            double betaT = ((entry.Mean - minYield) * (maxYield - entry.Mean) / Utility.Power(entry.StdDev, 2)) - 1;
                            double betaR = betaT * (entry.Mean - minYield) / (maxYield - minYield);
                            tempYield = BetaInverse.InverseBeta(pt, betaR, betaT - betaR, minYield, maxYield);                            
                        }

                        //Compute Yields
                        if (tempYield < minYield)
                        {
                            obj.Value = pt;
                            obj.Yield = minYield;
                        }
                        else if (tempYield > maxYield)
                        {
                            obj.Value = pt;
                            obj.Yield = maxYield;
                        }
                        else
                        {
                            obj.Value = pt;
                            obj.Yield = tempYield;
                        }

                        //Compute Loss Ratios
                        if (tempYield > thresholdYield)
                            obj.LossRatio = 0;
                        else
                        {
                            obj.LossRatio = -((tempYield / thresholdYield) - 1);
                            
                            //Implement Deductible
                            if (pricingVM.SelectedFranchise == Franchise.Deductible)
                            {
                                obj.LossRatio -= Utility.Double(pricingVM.Franchise);
                            }
                        }
                    }                    
                    yearData.Add(obj);
                }

                //calculate PML                
                double temp = 0;

                if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.Normal)
                    temp = NormInverse.NormInv(1 / Utility.ConvertSimulationPoints(modellingVM.SelectedSimulationPoint) , entry.Mean, entry.StdDev);
                else if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.LogNormal)
                {
                    double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(entry.StdDev / entry.Mean, 2)));
                    double lnMean = Math.Log(entry.Mean) - Math.Pow(lnStdDev, 2) / 2;
                    temp = Math.Exp(NormInverse.NormInv(1 / Utility.ConvertSimulationPoints(modellingVM.SelectedSimulationPoint), lnMean, lnStdDev));
                }
                else if (modellingVM.SelectedSimulationDistribution == SimulationDistribution.Beta)
                {
                    double betaT = ((entry.Mean - minYield) * (maxYield - entry.Mean) / Utility.Power(entry.StdDev, 2)) - 1;
                    double betaR = betaT * (entry.Mean - minYield) / (maxYield - minYield);
                    temp = BetaInverse.InverseBeta(1 / Utility.ConvertSimulationPoints(modellingVM.SelectedSimulationPoint), betaR, betaT - betaR, minYield, maxYield);
                }                

                if (temp < minYield)
                    temp = minYield;
                else if (temp > maxYield)
                    temp = maxYield;

                if (temp > thresholdYield)
                {
                    returnData.Add(new ModifiedUserData()
                    {
                        Data = yearData,
                        Mean = entry.Mean,
                        StdDev = entry.StdDev,
                        BlockName = entry.BlockName,
                        PML = 0,
                        AreaSown = entry.AreaSown,
                        ThresholdYield= thresholdYield
                    });
                }
                else
                {
                    returnData.Add(new ModifiedUserData()
                    {
                        Data = yearData,
                        Mean = entry.Mean,
                        StdDev = entry.StdDev,
                        BlockName = entry.BlockName,
                        PML = -((temp / thresholdYield) - 1),
                        AreaSown = entry.AreaSown,
                        ThresholdYield= thresholdYield
                    });
                }
            }

            //compute mean yield and lossratio, stddev lossratio.
            List<double?> yield;
            List<double?> lossratio;

            foreach (ModifiedUserData entry in returnData)
            {
                yield = new List<double?>();
                lossratio = new List<double?>();
                foreach (YearData yrData in entry.Data)
                {
                    yield.Add(yrData.Yield);
                    lossratio.Add(yrData.LossRatio);
                }

                entry.MeanYield = yield.Mean();
                entry.MeanLossRatio = lossratio.Mean();
                entry.StdDevLossRatio = lossratio.StdDev();
            }

            return returnData;
        }

        /// <summary>
        /// Pricing mechanism for Simulated Yields.
        /// </summary>
        /// <param name="modellingVM"></param>
        /// <param name="pricingVM"></param>
        /// <param name="simulatedData"></param>
        public static ObservableCollection<Result> SimulatedPricing(ModellingOptionsViewModel modellingVM, PricingOptionsViewModel pricingVM,
            ObservableCollection<ModifiedUserData> simulatedData, ObservableCollection<ModifiedUserData> historicalData)
        {
            ObservableCollection<Result> pricingResult = new ObservableCollection<Result>();
            Result result;

            foreach (ModifiedUserData entry in simulatedData)
            {
                result = new Result();
                                
                double validYears = 0;
                double count = 0; 

                var currentHistoricalBlockData = from data in historicalData
                                       where data.BlockName == entry.BlockName
                                       select data;

                var currentSimulationBlockData = from data in simulatedData
                                                 where data.BlockName == entry.BlockName
                                                 select data;

                foreach (YearData yr in currentSimulationBlockData.FirstOrDefault().Data)
                {
                    if (yr.LossRatio > entry.MeanLossRatio)
                        count++;
                }

                foreach (YearData yr in currentHistoricalBlockData.FirstOrDefault().Data)
                {
                    if (yr.Value >= 0)
                        validYears++;                    
                }

                if (validYears == 0)
                    validYears = mnais.Constants.Invalid;
                
                //calculation of params                                
                result.BlockName = entry.BlockName;
                result.AreaSown = entry.AreaSown;
                result.ValidYears = validYears;
                result.Years = validYears / Utility.Double(pricingVM.OptimalNoOfYears);

                result.MeanLossRatio = Utility.Double(entry.MeanLossRatio);
                result.StdDevLossRatio = Utility.Double(entry.StdDevLossRatio);

                result.Neq = validYears * Math.Min(1, validYears / Utility.Double(pricingVM.OptimalNoOfYears));
                result.DULFactor = Utility.Double(entry.StdDevLossRatio * NormInverse.NormInv(Utility.Double(pricingVM.ConfidenceLevel), 0, 1) / Utility.SquareRoot(result.Neq));

                if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Calculated)
                    result.DUL = result.DULFactor;
                else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Straight)
                    result.DUL = Utility.Double(pricingVM.UncertainityValue);
                else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Proportional)
                    result.DUL = Utility.Double(pricingVM.UncertainityValue * entry.MeanLossRatio);

                result.AEL = Math.Min(1, (Utility.Double(entry.MeanLossRatio + result.DUL)));

                result.PML = entry.PML;

                if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.UserSpecified)
                    result.ROC = Utility.Double(pricingVM.ReturnOnCaptial);
                else if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.Calculated)
                    result.ROC = (count / Utility.ConvertSimulationPoints(modellingVM.SelectedSimulationPoint)) / 2;

                result.Frequency = count / Utility.ConvertSimulationPoints(modellingVM.SelectedSimulationPoint);

                double temp = 0;
                if (result.PML > result.AEL)
                    temp = result.PML - result.AEL;
                else
                    temp = result.AEL - result.PML;

                result.CATLoad = result.ROC * temp;

                result.TRP = Math.Max(result.AEL, result.AEL + result.CATLoad);

                result.GP = result.TRP * (1 + Utility.Double(pricingVM.ServiceTax)) / (1 - Utility.Double(pricingVM.AOExpenses));

                result.CPR = result.GP * (1 + Utility.Double(pricingVM.ProfitMargin));

                pricingResult.Add(result);
            }
            return pricingResult;
        }

        /// <summary>
        /// Pricing Mechanism for Historical Yields.
        /// </summary>
        /// <param name="modellingVM"></param>
        /// <param name="pricingVM"></param>
        /// <param name="historicalData"></param>
        /// <returns></returns>
        public static ObservableCollection<Result> HistoricalPricing(ModellingOptionsViewModel modellingVM, PricingOptionsViewModel pricingVM, 
            ObservableCollection<ModifiedUserData> historicalData)
        {
            ObservableCollection<Result> pricingResult = new ObservableCollection<Result>();
            Result result;

            foreach (ModifiedUserData entry in historicalData)
            {
                result = new Result();                
                double validYears = 0; //calculate no of valid years from historical data
                double count = 0; //required in ROC Calculation

                foreach (YearData yr in entry.Data)
                {
                    if (yr.Value >= 0)
                        validYears++;

                    if (yr.Value > entry.MeanLossRatio)
                        count++;
                }

                if (validYears == 0)
                    validYears = mnais.Constants.Invalid;

                //calculation of params                 
                result.BlockName = entry.BlockName;
                result.AreaSown = entry.AreaSown;
                result.ValidYears = validYears;
                result.Years = validYears / Utility.Double(pricingVM.OptimalNoOfYears);

                result.MeanLossRatio = Utility.Double(entry.MeanLossRatio);
                result.StdDevLossRatio = Utility.Double(entry.StdDevLossRatio);

                result.Neq = validYears * Math.Min(1, validYears / Utility.Double(pricingVM.OptimalNoOfYears));
                result.DULFactor = Utility.Double(entry.StdDevLossRatio * NormInverse.NormInv(Utility.Double(pricingVM.ConfidenceLevel), 0, 1) / Utility.SquareRoot(result.Neq));

                if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Calculated)
                    result.DUL = result.DULFactor;
                else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Straight)
                    result.DUL = Utility.Double(pricingVM.UncertainityValue);
                else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Proportional)
                    result.DUL = Utility.Double(pricingVM.UncertainityValue * entry.MeanLossRatio);

                result.AEL = Math.Min(1, (Utility.Double(entry.MeanLossRatio + result.DUL)));

                double? value = entry.Data.FirstOrDefault().Value;
                foreach (YearData yr in entry.Data)
                {
                    if (yr.Value > value)
                        value = yr.Value;
                }

                result.PML = Utility.Double(value);

                if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.UserSpecified)
                    result.ROC = Utility.Double(pricingVM.ReturnOnCaptial);
                else if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.Calculated)
                    result.ROC = (count / validYears) / 2;

                result.Frequency = count / validYears;

                double temp = 0;
                if (result.PML > result.AEL)
                    temp = result.PML - result.AEL;
                else
                    temp = result.AEL - result.PML;

                result.CATLoad = result.ROC * temp;

                result.TRP = Math.Max(result.AEL, result.AEL + result.CATLoad);

                result.GP = result.TRP * (1 + Utility.Double(pricingVM.ServiceTax)) / (1 - Utility.Double(pricingVM.AOExpenses));

                result.CPR = result.GP * (1 + Utility.Double(pricingVM.ProfitMargin));

                pricingResult.Add(result);
            }
            return pricingResult;
        }

        /// <summary>
        /// Aggregate the Simulated Results for Pricing for Aggregate block.
        /// </summary>
        /// <param name="modellingVM"></param>
        /// <param name="pricingVM"></param>
        /// <param name="mean"></param>
        /// <param name="stdDev"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static Result AggregateSimulatedPricing(ModellingOptionsViewModel modellingVM, PricingOptionsViewModel pricingVM, int count, double mean , double stdDev)
        {            
            Result result = new Result();

            result.BlockName = "Aggregated District";
            double tempArea = 0;

            //calculate total area.
            foreach (ImportedData entry in modellingVM.mainVM.ImportDataVM.import)
                tempArea += Utility.Double(entry.AreaSown);
            
            result.AreaSown = tempArea;
            result.ValidYears = count;
            result.Years = count / Utility.Double(pricingVM.OptimalNoOfYears);

            result.MeanLossRatio = mean;
            result.StdDevLossRatio = stdDev;

            result.Neq = count * Math.Min(1, count / Utility.Double(pricingVM.OptimalNoOfYears));
            result.DULFactor = Utility.Double(stdDev * NormInverse.NormInv(Utility.Double(pricingVM.ConfidenceLevel), 0, 1) / Utility.SquareRoot(result.Neq));

            if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Calculated)
                result.DUL = result.DULFactor;
            else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Straight)
                result.DUL = Utility.Double(pricingVM.UncertainityValue);
            else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Proportional)
                result.DUL = Utility.Double(pricingVM.UncertainityValue * mean);

            result.AEL = Math.Min(1, (mean + result.DUL));

            if (modellingVM.SelectedModel == Model.Normal)
                result.PML = NormInverse.NormInv(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), mean, stdDev);
            else if (modellingVM.SelectedModel == Model.LogNormal)
            {  
                double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(stdDev / mean, 2)));
                double lnMean = Math.Log(mean) - Math.Pow(lnStdDev, 2) / 2;

                result.PML = Math.Exp(NormInverse.NormInv(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), lnMean, lnStdDev));                
            }
            else if (modellingVM.SelectedModel == Model.Beta)
            {
                double betaT = ((mean - 0) * (1 - mean) / Utility.Power(stdDev, 2)) - 1;
                double betaR = betaT * (mean - 0) / (1 - 0);

                result.PML = BetaInverse.InverseBeta(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), betaR, betaT - betaR, 0, 1);
            }

            double frequency = 0;
            if (modellingVM.SelectedModel == Model.Normal)
                frequency = NormalDistribution.StandardNormalDistribution(mean, mean, stdDev, true);
            else if (modellingVM.SelectedModel == Model.LogNormal)
            {
                double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(stdDev / mean, 2)));
                double lnMean = Math.Log(mean) - Math.Pow(lnStdDev, 2) / 2;

                frequency = LogNormalDistribution.LogNormDist(mean, lnMean, lnStdDev);
            }
            else if (modellingVM.SelectedModel == Model.Beta)
            {
                double betaT = ((mean - 0) * (1 - mean) / Utility.Power(stdDev, 2)) - 1;
                double betaR = betaT * (mean - 0) / (1 - 0);

                frequency = BetaInverse.IncompleteBetaFunction(mean, betaR, betaT - betaR);
            }

            if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.UserSpecified)
            {
                frequency = Utility.Double(pricingVM.ReturnOnCaptial);
                result.ROC = frequency;
            }
            else if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.Calculated)
            {
                frequency = 1 - frequency;
                result.ROC = frequency / 2;
            }            

            result.Frequency = frequency;

            double temp = 0;
            if (result.PML > result.AEL)
                temp = result.PML - result.AEL;
            else
                temp = result.AEL - result.PML;

            result.CATLoad = result.ROC * temp;

            result.TRP = Math.Max(result.AEL, result.AEL + result.CATLoad);

            result.GP = result.TRP * (1 + Utility.Double(pricingVM.ServiceTax)) / (1 - Utility.Double(pricingVM.AOExpenses));

            result.CPR = result.GP * (1 + Utility.Double(pricingVM.ProfitMargin));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modellingVM"></param>
        /// <param name="pricingVM"></param>
        /// <param name="mean"></param>
        /// <param name="stdDev"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static Result AggregateHistoricalPricing(ModellingOptionsViewModel modellingVM, PricingOptionsViewModel pricingVM, int count, double mean ,
            double stdDev, ObservableCollection<ModifiedUserData> historicalDistrictLossAnalysis)
        {
            Result result = new Result();

            result.BlockName = "Aggregated District";
            double tempArea = 0;

            //calculate total area.
            foreach (ImportedData entry in modellingVM.mainVM.ImportDataVM.import)
                tempArea += Utility.Double(entry.AreaSown);

            result.AreaSown = tempArea;

            result.ValidYears = count;
            result.Years = count / Utility.Double(pricingVM.OptimalNoOfYears);

            result.MeanLossRatio = mean;
            result.StdDevLossRatio = stdDev;

            result.Neq = count * Math.Min(1, count / Utility.Double(pricingVM.OptimalNoOfYears));
            result.DULFactor = Utility.Double(stdDev * NormInverse.NormInv(Utility.Double(pricingVM.ConfidenceLevel), 0, 1) / Utility.SquareRoot(result.Neq));

            if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Calculated)
                result.DUL = result.DULFactor;
            else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Straight)
                result.DUL = Utility.Double(pricingVM.UncertainityValue);
            else if (pricingVM.SelectedDataUncertainityLoading == DataLoading.Proportional)
                result.DUL = Utility.Double(pricingVM.UncertainityValue * mean);

            result.AEL = Math.Min(1, (mean + result.DUL));

            if (pricingVM.SelectedHistoricalPML == HistoricalPML.ReturnPeriod)
            {
                if (modellingVM.SelectedModel == Model.Normal)
                    result.PML = NormInverse.NormInv(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), mean, stdDev);
                else if (modellingVM.SelectedModel == Model.LogNormal)
                {
                    double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(stdDev / mean, 2)));
                    double lnMean = Math.Log(mean) - Math.Pow(lnStdDev, 2) / 2;

                    result.PML = Math.Exp(NormInverse.NormInv(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), lnMean, lnStdDev));
                }
                else if (modellingVM.SelectedModel == Model.Beta)
                {
                    double betaT = ((mean - 0) * (1 - mean) / Utility.Power(stdDev, 2)) - 1;
                    double betaR = betaT * (mean - 0) / (1 - 0);

                    result.PML = BetaInverse.InverseBeta(1 - 1 / Utility.Double(pricingVM.ReturnPeriod), betaR, betaT-betaR, 0, 1);
                }
            }
            else if (pricingVM.SelectedHistoricalPML == HistoricalPML.Maximum)
            {                
                double? tempLR = 0;
                foreach (YearData entry in historicalDistrictLossAnalysis.FirstOrDefault().Data)                
                {   
                    if (entry.Value > tempLR)
                        tempLR = entry.Value;                 
                }
                result.PML = Utility.Double(tempLR);
            }

            double frequency = 0;

            if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.UserSpecified)
                result.ROC = Utility.Double(pricingVM.ReturnOnCaptial);
            else if (pricingVM.SelectedReturnOnCapital == ReturnOnCapital.Calculated)
            {
                if (modellingVM.SelectedHistoricalMethod == HistoricalMethod.Historical)
                {
                    double tempCount = 0;
                    foreach (YearData entry in historicalDistrictLossAnalysis.FirstOrDefault().Data)
                    {
                        if (entry.Value > mean)
                            tempCount++;
                    }
                    frequency = tempCount / count;                    
                }
                else
                {
                    if (modellingVM.SelectedModel == Model.Normal)
                        frequency = NormalDistribution.StandardNormalDistribution(mean, mean, stdDev, true);
                    else if (modellingVM.SelectedModel == Model.LogNormal)
                    {
                        double lnStdDev = Math.Sqrt(Math.Log(1 + Math.Pow(stdDev / mean, 2)));
                        double lnMean = Math.Log(mean) - Math.Pow(lnStdDev, 2) / 2;

                        frequency = LogNormalDistribution.LogNormDist(mean, lnMean, lnStdDev);
                    }
                    else if (modellingVM.SelectedModel == Model.Beta)
                    {
                        double betaT = ((mean - 0) * (1 - mean) / Utility.Power(stdDev, 2)) - 1;
                        double betaR = betaT * (mean - 0) / (1 - 0);

                        frequency = BetaInverse.IncompleteBetaFunction(mean, betaR, betaT - betaR);
                    }
                    frequency = 1 - frequency;
                }
            }

            result.ROC = frequency / 2;

            result.Frequency = frequency;

            double temp = 0;
            if (result.PML > result.AEL)
                temp = result.PML - result.AEL;
            else
                temp = result.AEL - result.PML;

            result.CATLoad = result.ROC * temp;

            result.TRP = Math.Max(result.AEL, result.AEL + result.CATLoad);

            result.GP = result.TRP * (1 + Utility.Double(pricingVM.ServiceTax)) / (1 - Utility.Double(pricingVM.AOExpenses));

            result.CPR = result.GP * (1 + Utility.Double(pricingVM.ProfitMargin));

            return result;
        }

        /// <summary>
        /// Calculate the correlation matrix
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static double?[,] CorrelationMatrix(ObservableCollection<ModifiedUserData> userData)
        {
            int count = userData.Count;
            double?[,] matrix = new double?[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i != j)
                    {
                        ObservableCollection<YearData> tempI = userData[i].Data;
                        ObservableCollection<YearData> tempJ = userData[j].Data;

                        List<double?> I = new List<double?>();
                        List<double?> J = new List<double?>();

                        foreach (YearData yr in tempI)
                            I.Add(yr.Value);

                        foreach (YearData yr in tempJ)
                            J.Add(yr.Value);

                        matrix[i, j] = Utility.Correlation(I.ToArray(), J.ToArray());
                    }
                    else
                        matrix[i, j] = 1.0;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Returns the object containing count, mean and std dev
        /// </summary>
        /// <param name="districtLevelHistoricalLossRatios"></param>
        /// <returns></returns>
        public static ModifiedUserData HistoricalAggregation(ObservableCollection<ModifiedUserData> districtLevelHistoricalLossRatios)
        {
            ModifiedUserData returnValue = new ModifiedUserData();
            List<double?> values = new List<double?>();
            int count = 0;

            foreach (YearData entry in districtLevelHistoricalLossRatios.FirstOrDefault().Data)
            { 
                values.Add(entry.Value);

                if (entry.Value >= 0)
                    count++;
            }

            returnValue.Count = count;            
            returnValue.Mean = Utility.Double(values.Mean());
            returnValue.StdDev = Utility.Double(values.StdDev());
            return returnValue;
        }

        /// <summary>
        /// Aggregation for Simulation
        /// </summary>
        /// <param name="simulationData"></param>
        /// <param name="correlationMatrix"></param>
        public static ModifiedUserData SimulationAggregation(ObservableCollection<ModifiedUserData> simulationData, double?[,] correlationMatrix)
        {
            ModifiedUserData returnValue = new ModifiedUserData();

            //total area sown
            double totalArea = 0;
            foreach (ModifiedUserData entry in simulationData)
                totalArea += entry.AreaSown;

            //Aggregate Mean
            double totalAreaSown = 0.0;
            foreach (ModifiedUserData entry in simulationData)
            {
                if (entry.AreaSown != 0)
                    totalAreaSown += entry.AreaSown;
            }

            double? totalMean = 0.0;
            foreach (ModifiedUserData entry in simulationData)
            {
                if (entry.Mean != 0.0 && entry.AreaSown != 0.0)
                    totalMean += entry.MeanLossRatio * entry.AreaSown;
            }

            double? aggregateMean = totalMean / totalAreaSown;

            //Aggregate Std Dev           
            double? tempAggregateStdDev = 0;
            int i, j;
            i = j = simulationData.Count;

            for (int row = 0; row < i; row++)
            {
                for (int col = 0; col < j; col++)
                {
                    double? matrix = correlationMatrix[row, col];
                    tempAggregateStdDev += ((simulationData[row].AreaSown / totalArea) * simulationData[row].StdDevLossRatio) * matrix * ((simulationData[col].AreaSown / totalArea) * simulationData[col].StdDevLossRatio);
                }
            }

            double? aggregateStdDev = Utility.SquareRoot(tempAggregateStdDev);
                        
            returnValue.Mean = Utility.Double(aggregateMean);
            returnValue.StdDev = Utility.Double(aggregateStdDev);
            return returnValue;
        }
    }
}
