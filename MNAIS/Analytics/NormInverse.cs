﻿using System;

namespace MNAIS.Analytics
{
    /// <Summary>NormInverse Calculation</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>26-09-2013</Date Created>
    /// <Date Modified>25-10-2013</Date Modified>
    /// <Modified By>Mani Sharma</Modified By>

    public static class NormInverse
    {
        private static double NormInv(double probability)
        {
            const double a1 = -39.6968302866538;
            const double a2 = 220.946098424521;
            const double a3 = -275.928510446969;
            const double a4 = 138.357751867269;
            const double a5 = -30.6647980661472;
            const double a6 = 2.50662827745924;

            const double b1 = -54.4760987982241;
            const double b2 = 161.585836858041;
            const double b3 = -155.698979859887;
            const double b4 = 66.8013118877197;
            const double b5 = -13.2806815528857;

            const double c1 = -7.78489400243029E-03;
            const double c2 = -0.322396458041136;
            const double c3 = -2.40075827716184;
            const double c4 = -2.54973253934373;
            const double c5 = 4.37466414146497;
            const double c6 = 2.93816398269878;

            const double d1 = 7.78469570904146E-03;
            const double d2 = 0.32246712907004;
            const double d3 = 2.445134137143;
            const double d4 = 3.75440866190742;

            const double pLow = 0.02425;

            const double pHigh = 1 - pLow;

            double q;
            double result = 0;

            if (probability <= 0)
                probability = pLow;

            if (probability >= 1)
                probability = pHigh;

            if (probability < pLow)
            {
                q = Math.Sqrt(-2 * Math.Log(probability));
                result = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            else if (probability <= pHigh)
            {
                q = probability - 0.5;
                double r = q * q;
                result = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q /
                         (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1);
            }
            else if (probability < 1)
            {
                q = Math.Sqrt(-2 * Math.Log(1 - probability));
                result = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }

            return result;
        }

        public static double NormInv(double probability, double mean, double sigma)
        {
            double x = NormInv(probability);
            return sigma * x + mean;
        }
    }
}
