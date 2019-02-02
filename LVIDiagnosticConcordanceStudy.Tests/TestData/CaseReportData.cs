using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LVIDiagnosticConcordanceStudy.Tests.TestData
{
    [Serializable]
    public class CaseReportData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new object[] { GetTopNReports(i + 1) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Report[] GetTopNReports(int num)
        {
            Report[] reports = new Report[num];

            for (int i = 0; i < num; i++)
            {
                reports[i] = OrderedReportSet[i];
                if (i > 0)
                {
                    reports[i - 1].IsSubmitted = true;
                }
            }

            return reports;
        }
        private Report[] OrderedReportSet => new Report[]
        {
            new Report(1, "testUser", 1, OrderedStatisticsSet[0])
            {
                TumourGrade = Grade.G3,
                NumberofLVI = 1,
                Case = new Case()
                {
                    Id = 1,
                    PatientAge = 38,
                    TumourSize = 4
                }
            },
            new Report(2, "testUser", 2, OrderedStatisticsSet[1])
            {
                TumourGrade = Grade.G2,
                NumberofLVI = 1,
                Case = new Case()
                {
                    Id = 2,
                    PatientAge = 48,
                    TumourSize = 4
                }
            },
            new Report(3, "testUser", 3, OrderedStatisticsSet[2])
            {
                TumourGrade = Grade.G2,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 3,
                    PatientAge = 67,
                    TumourSize = 3
                }
            },
            new Report(4, "testUser", 4, OrderedStatisticsSet[3])
            {
                TumourGrade = Grade.G2,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 4,
                    PatientAge = 85,
                    TumourSize = 1.5m
                }
            },
            new Report(5, "testUser", 5, OrderedStatisticsSet[4])
            {
                TumourGrade = Grade.G1,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 5,
                    PatientAge = 45,
                    TumourSize = 3.5m
                }
            },
            new Report(6, "testUser", 6, OrderedStatisticsSet[5])
            {
                TumourGrade = Grade.G1,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 6,
                    PatientAge = 85,
                    TumourSize = 4
                }
            },
            new Report(7, "testUser", 7, OrderedStatisticsSet[6])
            {
                TumourGrade = Grade.G1,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 7,
                    PatientAge = 85,
                    TumourSize = 1.8m
                }
            },
            new Report(8, "testUser", 8, OrderedStatisticsSet[7])
            {
                TumourGrade = Grade.G3,
                NumberofLVI = 1,
                Case = new Case()
                {
                    Id = 8,
                    PatientAge = 62,
                    TumourSize = 3.2m
                }
            },
            new Report(9, "testUser", 9, OrderedStatisticsSet[8])
            {
                TumourGrade = Grade.G3,
                NumberofLVI = 1,
                Case = new Case()
                {
                    Id = 9,
                    PatientAge = 55,
                    TumourSize = 2.5m
                }
            },
            new Report(10, "testUser", 10, OrderedStatisticsSet[9])
            {
                TumourGrade = Grade.G2,
                NumberofLVI = 0,
                Case = new Case()
                {
                    Id = 10,
                    PatientAge = 62,
                    TumourSize = 1m
                }
            }
        };

        private ReportStatistics[] OrderedStatisticsSet => new ReportStatistics[] 
        {
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.500886525m,
                ProbLVINeg50Plus = 0.376730266m,
                BayesForAge = 0.358759309m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.503766522m,
                ProbLVIPosGrade = 0.59423275m,
                ProbLVINegGrade = 0.372154116m,
                BayesForGrade = 0.618462966m,
                ProbLVIPosNumberOfLVI = 0.44m,
                ProbLVINegNumberOfLVI = 0.25m,
                BayesForNumberOfLVI = 0.740456621m,

                LVIPresent = true,
                CumulativeBayesForGrade = 0.618462966m,
                CumulativeAverageBayesForGrade = 0.618462966m,
                CumulativeCasesWithLVIPos = 1,

                BinomialDist = 0.618462966m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.500886525m,
                ProbLVINeg50Plus = 0.376730266m,
                BayesForAge = 0.358759309m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.503766522m,
                ProbLVIPosGrade = 0.33161689m,
                ProbLVINegGrade = 0.38704028m,
                BayesForGrade = 0.465185914m,
                ProbLVIPosNumberOfLVI = 0.44m,
                ProbLVINegNumberOfLVI = 0.25m,
                BayesForNumberOfLVI = 0.604877895m,

                LVIPresent = true,
                CumulativeBayesForGrade = 1.08364888m,
                CumulativeAverageBayesForGrade = 0.54182444m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.293573724m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.379439094m,
                ProbLVIPosGrade = 0.33161689m,
                ProbLVINegGrade = 0.38704028m,
                BayesForGrade = 0.343783633m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.028329524m,

                LVIPresent = false,
                CumulativeBayesForGrade = 1.427432514m,
                CumulativeAverageBayesForGrade = 0.475810838m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.356022915m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.433789954m,
                ProbLVINegSize = 0.500775194m,
                BayesForSize = 0.225945924m,
                ProbLVIPosGrade = 0.33161689m,
                ProbLVINegGrade = 0.38704028m,
                BayesForGrade = 0.200063993m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.01372754m,

                LVIPresent = false,
                CumulativeBayesForGrade = 1.627496506m,
                CumulativeAverageBayesForGrade = 0.406874127m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.349433981m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.500886525m,
                ProbLVINeg50Plus = 0.376730266m,
                BayesForAge = 0.358759309m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.503766522m,
                ProbLVIPosGrade = 0.07415036m,
                ProbLVINegGrade = 0.240805604m,
                BayesForGrade = 0.238153679m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.01709943m,

                LVIPresent = false,
                CumulativeBayesForGrade = 1.865650185m,
                CumulativeAverageBayesForGrade = 0.373130037m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.342967371m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.379439094m,
                ProbLVIPosGrade = 0.07415036m,
                ProbLVINegGrade = 0.240805604m,
                BayesForGrade = 0.15844755m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.010369541m,

                LVIPresent = false,
                CumulativeBayesForGrade = 2.024097735m,
                CumulativeAverageBayesForGrade = 0.337349622m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.329146707m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.433789954m,
                ProbLVINegSize = 0.500775194m,
                BayesForSize = 0.225945924m,
                ProbLVIPosGrade = 0.07415036m,
                ProbLVINegGrade = 0.240805604m,
                BayesForGrade = 0.082470726m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.004977314m,

                LVIPresent = false,
                CumulativeBayesForGrade = 2.10656846m,
                CumulativeAverageBayesForGrade = 0.300938351m,
                CumulativeCasesWithLVIPos = 2,

                BinomialDist = 0.317505866m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.269767442m,
                BayesForSize = 0.379439094m,
                ProbLVIPosGrade = 0.59423275m,
                ProbLVINegGrade = 0.372154116m,
                BayesForGrade = 0.494008626m,
                ProbLVIPosNumberOfLVI = 0.44m,
                ProbLVINegNumberOfLVI = 0.25m,
                BayesForNumberOfLVI = 0.632125747m,

                LVIPresent = true,
                CumulativeBayesForGrade = 2.600577087m,
                CumulativeAverageBayesForGrade = 0.325072136m,
                CumulativeCasesWithLVIPos = 3,

                BinomialDist = 0.269410211m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.489497717m,
                ProbLVINegSize = 0.229457364m,
                BayesForSize = 0.379439094m,
                ProbLVIPosGrade = 0.59423275m,
                ProbLVINegGrade = 0.372154116m,
                BayesForGrade = 0.494008626m,
                ProbLVIPosNumberOfLVI = 0.44m,
                ProbLVINegNumberOfLVI = 0.25m,
                BayesForNumberOfLVI = 0.632125747m,

                LVIPresent = true,
                CumulativeBayesForGrade = 3.094585713m,
                CumulativeAverageBayesForGrade = 0.343842857m,
                CumulativeCasesWithLVIPos = 4,

                BinomialDist = 0.214214999m
            },
            new ReportStatistics
            {
                ProbLVIPos50Plus = 0.499113475m,
                ProbLVINeg50Plus = 0.623269734m,
                BayesForAge = 0.252042356m,
                ProbLVIPosSize = 0.076712329m,
                ProbLVINegSize = 0.229457364m,
                BayesForSize = 0.101250747m,
                ProbLVIPosGrade = 0.33161689m,
                ProbLVINegGrade = 0.38704028m,
                BayesForGrade = 0.088028155m,
                ProbLVIPosNumberOfLVI = 0.04m,
                ProbLVINegNumberOfLVI = 0.71875m,
                BayesForNumberOfLVI = 0.005343128m,

                LVIPresent = false,
                CumulativeBayesForGrade = 3.182613868m,
                CumulativeAverageBayesForGrade = 0.318261387m,
                CumulativeCasesWithLVIPos = 4,

                BinomialDist = 0.216302956m
            }
        };
    }
}
