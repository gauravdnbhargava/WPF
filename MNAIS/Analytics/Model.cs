using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

/// <Summary>Model Class for Analytical Module</Summary>
/// <Author>Mani Sharma</Author>
/// <Date Created>26-09-2013</Date Created>
/// <Date Modified>25-10-2013</Date Modified>
/// <Modified By>Mani Sharma</Modified By>

namespace MNAIS.Analytics
{
    public class YearData
    {
        public int Year { get; set; }
        public double? Value { get; set; }
        public double Yield { get; set; }
        public double LossRatio { get; set; }
        public int Index { get; set; }
        public string Error { get; set; }
    }

    public class ThresholdYield
    {
        public string BlockName { get; set; }
        public double Yield { get; set; }
    }

    public class ModifiedUserData:ICloneable
    {
        public string BlockName { get; set; }
        public double AreaSown { get; set; }        
        public ObservableCollection<YearData> Data { get; set; }
        public double Mean { get; set; }
        public double StdDev { get; set; }
        public double Slope { get; set; }
        public double Intercept { get; set; }
        public double SST { get; set; }
        public double SSR { get; set; }
        public double SSE { get; set; }
        public double RSquare { get; set; }
        public double Sxx { get; set; }
        public double Tvalue { get; set; }
        public int Count { get; set; }
        public Trend TrendStatus { get; set; }

        public double? MeanYield { get; set; }
        public double? MeanLossRatio { get; set; }
        public double? StdDevLossRatio { get; set; }

        public double? ThresholdYield { get; set; }
        public double PML { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
        
    public class TrendTable
    {
        public int DegreeOfFreedom { get; set; }
        public double TValue1 { get; set; }
        public double TValue2 { get; set; }
        public double TValue3 { get; set; }        
    }

    public class Trend
    {
        public bool IsTrend { get; set; }
        public TrendCatagory Catagory { get; set; }
    }

    public enum TrendCatagory
    {
        NoTrend,
        Trend,
        StrongTrend,
        VeryStrongTrend
    }

    public class Result
    {
        public string BlockName { get; set; }

        public double AreaSown { get; set; }
        public double ValidYears { get; set; }
        public double Years { get; set; }

        public double MeanLossRatio { get; set; }
        public double StdDevLossRatio { get; set; }        
        
        public double Neq { get; set; }
        public double DULFactor { get; set; }
        public double DUL { get; set; }
        public double AEL { get; set; }
        public double PML { get; set; }
        public double ROC { get; set; }
        public double Frequency { get; set; }
        public double CATLoad { get; set; }
        public double TRP { get; set; } //technical risk premium
        public double GP { get; set; } //gross premium
        public double CPR { get; set; } //commercial premium rate
    }
}
