using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections;
using System.Collections.Generic;


namespace LVIDiagnosticConcordanceStudy.Tests.TestData
{
    public class InterventionTestData
    {
        public static InterventionData[] OrderedInterventionDataSet => new InterventionData[]
        {
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.381537034m, 0.618462966m },
                ObservedYValues = new decimal[]{ 0, 0.618462966m },
                ChartXAxis = new int[]{0, 1},
                PreTestProbability = 0.618462966m,
                PostTestProbability = 0.740456621m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.209924844m, 0.496501432m, 0.293573724m },
                ObservedYValues = new decimal[]{ 0, 0, 0.293573724m },
                ChartXAxis = new int[]{0, 1, 2},
                PreTestProbability = 0.465185914m,
                PostTestProbability = 0.604877895m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.144033698m, 0.392221738m, 0.356022915m, 0.107721648m },
                ObservedYValues = new decimal[]{ 0, 0, 0.356022915m, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3},
                PreTestProbability = 0.343783633m,
                PostTestProbability = 0.028329524m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.123762045m, 0.33959452m, 0.349433981m, 0.159803792m, 0.027405662m },
                ObservedYValues = new decimal[]{ 0, 0, 0.349433981m, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4},
                PreTestProbability = 0.200063993m,
                PostTestProbability = 0.01372754m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.096802663m, 0.288097877m, 0.342967371m, 0.204143499m, 0.060755879m, 0.00723271m },
                ObservedYValues = new decimal[]{ 0, 0, 0.342967371m, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5},
                PreTestProbability = 0.238153679m,
                PostTestProbability = 0.01709943m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.084665543m, 0.25861501m, 0.329146707m, 0.22342102m, 0.085306293m, 0.017371481m,
                    0.001473945m },
                ObservedYValues = new decimal[]{ 0, 0, 0.329146707m, 0, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5, 6},
                PreTestProbability = 0.15844755m,
                PostTestProbability = 0.010369541m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.081584628m, 0.245848996m, 0.317505866m, 0.227804639m, 0.098067392m, 0.02533016m,
                    0.003634785m, 0.000223534m },
                ObservedYValues = new decimal[]{ 0, 0, 0.317505866m, 0, 0, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5, 6, 7},
                PreTestProbability = 0.082470726m,
                PostTestProbability = 0.004977314m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.043058497m, 0.165909494m, 0.279680167m, 0.269410211m, 0.162198357m, 0.06249695m,
                    0.01505051m, 0.002071121m, 0.000124692m },
                ObservedYValues = new decimal[]{ 0, 0, 0, 0.269410211m, 0, 0, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8},
                PreTestProbability = 0.494008626m,
                PostTestProbability = 0.632125747m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.022546065m, 0.106332348m, 0.222883306m, 0.272525078m, 0.214214999m, 0.112254051m,
                    0.039215965m, 0.008807208m, 0.0011538m, 0.00006718m },
                ObservedYValues = new decimal[]{ 0, 0, 0, 0, 0.214214999m, 0, 0, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                PreTestProbability = 0.494008626m,
                PostTestProbability = 0.632125747m
            },
            new InterventionData()
            {
                TheoreticalYValues = new decimal[] {0.021685974m, 0.101238333m, 0.212678485m, 0.264763643m, 0.216302956m, 0.121174088m,
                    0.047140542m, 0.012575422m, 0.002201506m, 0.000228388m, 0.000010662m },
                ObservedYValues = new decimal[]{ 0, 0, 0, 0, 0.216302956m, 0, 0, 0, 0, 0, 0 },
                ChartXAxis = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
                PreTestProbability = 0.088028155m,
                PostTestProbability = 0.005343128m
            }
        };
    }
}
