using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{

    public class ExcelWriter : IExcelWriter
    {

        public async Task<Byte[]> WriteToExcel<T>(IQueryable<T> items) where T: LVIStudyUser
        {
             var reportData = items
                .SelectMany(i => i.Reports)
                .Select(r => new
                {
                    Name = r.LVIStudyUser.FirstName + " " + r.LVIStudyUser.LastName,
                    UserID = r.LVIStudyUserID,
                    UserReportNumber = r.UserReportNumber,
                    CaseNumber = r.Case.CaseNumber,
                    Grade = r.TumourGrade,
                    LVINumber = r.NumberofLVI,
                    CaseComplete = r.IsSubmitted,
                    r.Statistics.ProbLVINeg50Plus,
                    r.Statistics.ProbLVIPos50Plus,
                    r.Statistics.BayesForAge,
                    r.Statistics.ProbLVINegSize,
                    r.Statistics.ProbLVIPosSize,
                    r.Statistics.BayesForSize,
                    r.Statistics.ProbLVIPosGrade,
                    r.Statistics.ProbLVINegGrade,
                    PreTestProbability = r.Statistics.BayesForGrade,
                    r.Statistics.ProbLVIPosNumberOfLVI,
                    r.Statistics.ProbLVINegNumberOfLVI,
                    PostTestProbability = r.Statistics.BayesForNumberOfLVI,
                    r.Statistics.LVIPresent,
                    r.Statistics.CumulativeCasesWithLVIPos,
                    r.Statistics.CumulativeBayesForGrade,
                    r.Statistics.CumulativeAverageBayesForGrade
                })
                .OrderBy(r => r.Name)
                .ThenBy(r => r.UserReportNumber);

            Byte[] fileBytes;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Study Users");

                worksheet.Cells["A1"].LoadFromCollection(items, true, TableStyles.Medium15);
                //LoadFromDataTable(data, true, TableStyles.Medium15);

                ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("User Reports");

                worksheet2.Cells["A1"].LoadFromCollection(reportData, true, TableStyles.Medium15);
                fileBytes = package.GetAsByteArray();
            }

            return fileBytes;

        }
    }
}
