﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNAIS.Analytics
{
    /// <Summary>Implementes the BETA functionality of EXCEL</Summary>
    /// <Author>Mani Sharma</Author>
    /// <Date Created>12-11-2013</Date Created>
    /// <Date Modified></Date Modified>
    /// <Modified By></Modified By>

    public static class BetaInverse
    {
        const int MAXIT = 100;
        const double EPS = 0.0000003;
        const double FPMIN = 1.0E-30;
        
        /// <summary>
        /// Implementes the BETA.INV functionality of EXCEL
        /// </summary>
        /// <param name="p"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double InverseBeta(double p, double alpha, double beta, double A, double B)
        {
            double x = 0;
            double a = 0;
            double b = 1;
            double precision = Math.Pow(10, -6);

            while ((b - a) > precision)
            {
                x = (a + b) / 2;

                if (IncompleteBetaFunction(x, alpha, beta) > p)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
            }

            if ((B >= 0) && (A >= 0))
            {
                x = x * (B - A) + A;
            }

            return x;
        }

        /// <summary>
        /// Implementes the BETADIST functionality of EXCEL
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double IncompleteBetaFunction(double x, double a, double b)
        {
            double bt = 0;

            if (x <= 0.0)
            {
                return 0;
            }

            if (x >= 1)
            {
                return 1;
            }

            bt = System.Math.Exp(Gammln(a + b) - Gammln(a) - Gammln(b) + a * System.Math.Log(x) + b * System.Math.Log(1.0 - x));

            if (x < ((a + 1.0) / (a + b + 2.0)))
                return (bt * betacf(a, b, x) / a);
            else
                return (1.0 - bt * betacf(b, a, 1.0 - x) / b);
        }
                
        static double betacf(double a, double b, double x)
        {
            int m, m2;
            double aa, c, d, del, h, qab, qam, qap;

            qab = a + b;
            qap = a + 1.0;
            qam = a - 1.0;

            c = 1.0;

            d = 1.0 - qab * x / qap;

            if (System.Math.Abs(d) < FPMIN)
            {
                d = FPMIN;
            }

            d = 1.0 / d;
            h = d;

            for (m = 1; m <= MAXIT; ++m)
            {
                m2 = 2 * m;
                aa = m * (b - m) * x / ((qam + m2) * (a + m2));
                d = 1.0 + aa * d;

                if (System.Math.Abs(d) < FPMIN)
                {
                    d = FPMIN;
                }

                c = 1.0 + aa / c;

                if (System.Math.Abs(c) < FPMIN)
                {
                    c = FPMIN;
                }

                d = 1.0 / d;
                h *= d * c;

                aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
                d = 1.0 + aa * d;

                if (System.Math.Abs(d) < FPMIN)
                {
                    d = FPMIN;
                }

                c = 1.0 + aa / c;

                if (System.Math.Abs(c) < FPMIN)
                {
                    c = FPMIN;
                }

                d = 1.0 / d;
                del = d * c;
                h *= del;

                if (System.Math.Abs(del - 1.0) < EPS)
                {
                    break;
                }
            }

            if (m > MAXIT)
            {
                return 0;
            }
            else
            {
                return h;
            }
        }
              
        static double Gammln(double xx)
        {
            double x, y, tmp, ser;

            double[] cof = new double[] { 76.180091729471457, -86.505320329416776, 24.014098240830911, -1.231739572450155, 0.001208650973866179, -0.000005395239384953 };

            y = xx;
            x = xx;
            tmp = x + 5.5;
            tmp -= (x + 0.5) * System.Math.Log(tmp);

            ser = 1.0000000001900149;

            for (int j = 0; j <= 5; ++j)
            {
                y += 1;
                ser += cof[j] / y;
            }

            return -tmp + System.Math.Log(2.5066282746310007 * ser / x);
        }
    }
}
