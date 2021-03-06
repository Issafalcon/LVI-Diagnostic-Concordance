﻿using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public interface ICaseService
    {
        Task<Case> GetCaseByIdAsync(int id);
        Task<IReadOnlyList<Case>> GetOrderedCasesAsync(bool includeReports = false);
        Task ResetCasesAsync();
    }
}
