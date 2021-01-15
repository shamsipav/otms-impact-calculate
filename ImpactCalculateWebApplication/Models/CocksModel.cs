using System;
using System.Collections.Generic;
using System.Text;

namespace ImpactCalculateWebApplication.Models
{
    public class CocksModel
    {
        public static Cocks Kemerovo_3_4 { get; set; } = new Cocks()
        {
            Name = "Кемерово-3-4",
            Q = 6911,

            W = 0.70,
            A = 11.98,
            C = 83.37,
            H = 1.08,
            O = 1.32,
            N = 1.14,
            S = 0.41
        };

        public static Cocks Kemerovo_60_1_2 { get; set; } = new Cocks()
        {
            Name = "Кемерово-60-1-2",
            Q = 6999,

            W = 0.31,
            A = 11.37,
            C = 83.37,
            H = 1.01,
            O = 1.32,
            N = 1.14,
            S = 0.34
        };

        public static Cocks Gubaha_3_4 { get; set; } = new Cocks()
        {
            Name = "Губаха-3-4",
            Q = 6956,

            W = 0.59,
            A = 11.53,
            C = 83.37,
            H = 0.81,
            O = 1.32,
            N = 1.14,
            S = 0.65
        };
        public static Cocks Gubaha_60_1_2 { get; set; } = new Cocks()
        {
            Name = "Губаха-60-1-2",
            Q = 7003,

            W = 0.19,
            A = 11.03,
            C = 83.37,
            H = 0.92,
            O = 1.32,
            N = 1.14,
            S = 0.48
        };

        public static Cocks Chehiya_3_4 { get; set; } = new Cocks()
        {
            Name = "Чехия-3-4",
            Q = 7149,

            W = 0.79,
            A = 8.40,
            C = 83.37,
            H = 0.97,
            O = 1.32,
            N = 1.14,
            S = 0.61
        };
        public static Cocks Chehiya_60_90_1_2 { get; set; } = new Cocks()
        {
            Name = "Чехия-60-90-1-2",
            Q = 7027,

            W = 1.12,
            A = 9.73,
            C = 83.37,
            H = 1.34,
            O = 1.32,
            N = 1.14,
            S = 0.52
        };

        public static Cocks Harkov_3_4 { get; set; } = new Cocks()
        {
            Name = "Харьков-3-4",
            Q = 6931,

            W = 0.47,
            A = 11.66,
            C = 83.37,
            H = 0.82,
            O = 1.32,
            N = 1.14,
            S = 1.64
        };
        public static Cocks Harkov_60_1_2 { get; set; } = new Cocks()
        {
            Name = "Харьков-60-1-2",
            Q = 6985,

            W = 0.53,
            A = 10.75,
            C = 83.37,
            H = 0.92,
            O = 1.32,
            N = 1.14,
            S = 1.37
        };

        public static Cocks Alza_Invest_1_2 { get; set; } = new Cocks()
        {
            Name = "Алза-Инвест-1-2",
            Q = 6909,

            W = 0.54,
            A = 11.81,
            C = 83.37,
            H = 0.75,
            O = 1.32,
            N = 1.14,
            S = 0.51
        };

    }

    public class Cocks
    {
        public string Name { get; set; }
        public double Q, W, A, C, H, O, N, S;
    }
}
