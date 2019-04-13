using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public interface IExcelWriter
    {
        Task<Byte[]> WriteToExcel<T>(IQueryable<T> items) where T: LVIStudyUser;
    }
}
