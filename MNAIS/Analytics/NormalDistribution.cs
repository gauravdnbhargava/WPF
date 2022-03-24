using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNAIS.Analytics
{
    /// <Summary>Implements the NORM.DIST Function in Excel</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>

    static class NormalDistribution
    {        
        public static double StandardNormalDistribution(double x, double mean, double std, bool cumulative)
        {
            if (cumulative)
                return Phi(x, mean, std);

            var tmp = 1 / (Math.Sqrt(2 * Math.PI) * std);
            return tmp * Math.Exp(-.5 * Math.Pow((x - mean) / std, 2));
        }

        static double Phi(double z)
        {
            return 0.5 * (1.0 + erf(z / Math.Sqrt(2.0)));
        }

        static double Phi(double z, double mu, double sigma)
        {
            return Phi((z - mu) / sigma);
        }

        static double erf(double z)
        {
            var t = 1.0 / (1.0 + 0.5 * Math.Abs(z));

            var ans = 1 - t * Math.Exp(-z * z - 1.26551223 + t * (1.00002368 + t * (0.37409196 + t * (0.09678418 + t * (-0.18628806 + t *
                (0.27886807 + t * (-1.13520398 + t * (1.48851587 + t * (-0.82215223 + t * 0.17087277)))))))));

            if (z >= 0)
            {
                return ans;
            }
            return -ans;
        }
    }
}
