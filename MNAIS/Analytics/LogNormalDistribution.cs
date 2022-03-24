using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNAIS.Analytics
{
    /// <Summary>Implements the LOGNORM.DIST Function in Excel</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>

    static class LogNormalDistribution
    {
        public static double LogNormDist(double x, double zeta, double sigma)
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat; nfi.NumberDecimalDigits = 9;

            double u = (Math.Log(x) - zeta) / sigma;
            double p = NormalDistribution(u);

            return Convert.ToDouble(p.ToString("F", nfi));
        }


        static double NormalDistribution(double X)
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat; nfi.NumberDecimalDigits = 9;

            double L = 0.0;
            double K = 0.0;
            double dCND = 0.0;
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;

            L = Math.Abs(X); K = 1.0 / (1.0 + 0.2316419 * L);
            dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) * Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) + a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));

            if (X < 0)
            {
                double ff = Convert.ToDouble(dCND.ToString("F", nfi));
                return (1.0 - dCND);
            }
            else
            {
                return dCND;
            }
        }
    }
}
