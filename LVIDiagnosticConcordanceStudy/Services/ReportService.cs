using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public class ReportService : IReportService
    {
        private readonly IAsyncRepository<Report> _reportRepository;

        public ReportService(IAsyncRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        private void CalculateStatistics(Report report)
        {
            int? ptAge = report.Case?.PatientAge;
            decimal? tumourSize = report?.Case?.TumourSize;
            Grade? grade = Report?.TumourGrade;
            int? numLVISeen = Report?.NumberofLVI;

            if (!ptAge.HasValue || !tumourSize.HasValue || !grade.HasValue || !numLVISeen.HasValue)
            {
                throw new System.ArgumentException("Calculate Statistics not performed. Required case report information is missing");
            }

            ProbLVIPos50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVIPos : 1 - DataConstants.BelowFiftyYearsLVIPos;
            ProbLVINeg50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVINeg : 1 - DataConstants.BelowFiftyYearsLVINeg;
            BayesForAge = CalculateBayes(DataConstants.ProbLVIPos,
                DataConstants.BelowFiftyYearsLVIPos,
                DataConstants.BelowFiftyYearsLVINeg,
                DataConstants.ProbLVINeg);

            ProbLVIPosSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVIPos
                : tumourSize > 1 ? DataConstants.OneToTwocmLVIPos
                : DataConstants.BelowOnecmLVIPos;
            ProbLVINegSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVINeg
                : tumourSize > 1 ? DataConstants.OneToTwocmLVINeg
                : DataConstants.BelowOnecmLVINeg;
            BayesForSize = CalculateBayes(BayesForAge, ProbLVIPosSize, ProbLVINegSize, 1 - BayesForAge);

            ProbLVIPosGrade = (int)grade.Value == 1 ? DataConstants.GradeOneLVIPos
                : (int)grade.Value == 2 ? DataConstants.GradeTwoLVIPos
                : DataConstants.GradeThreeLVIPos;
            ProbLVINegGrade = (int)grade.Value == 1 ? DataConstants.GradeOneLVINeg
                : (int)grade.Value == 2 ? DataConstants.GradeTwoLVINeg
                : DataConstants.GradeThreeLVINeg;
            BayesForGrade = CalculateBayes(BayesForSize, ProbLVIPosGrade, ProbLVINegGrade, 1 - BayesForSize);

            ProbLVIPosNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVIPos
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVIPos
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVIPos
                : DataConstants.FivePlusLVIImagesLVIPos;
            ProbLVINegNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVINeg
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVINeg
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVINeg
                : DataConstants.FivePlusLVIImagesLVINeg;
            BayesForNumberOfLVI = CalculateBayes(BayesForGrade, ProbLVIPosNumberOfLVI, ProbLVINegNumberOfLVI, 1 - BayesForGrade);
        }

        private decimal CalculateBayes(decimal baseProb, decimal positivePredictiveProb, decimal negativePredictiveProb, decimal baseNegativePredictiveProb)
        {
            decimal posTimesBase = positivePredictiveProb * baseProb;

            return (posTimesBase) / (posTimesBase + (negativePredictiveProb * baseNegativePredictiveProb));
        }
    }
}
