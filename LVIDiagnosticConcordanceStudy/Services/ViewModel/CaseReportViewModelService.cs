﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using LVIDiagnosticConcordanceStudy.Data;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using Microsoft.EntityFrameworkCore;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class CaseReportViewModelService : ICaseReportViewModelService
    {
        private readonly IAsyncRepository<Case> _caseRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IReportService _reportService;

        public CaseReportViewModelService(
            IAsyncRepository<Case> caseRepository,
            IReportRepository reportRepository,
            IReportService reportService)
        {
            _caseRepository = caseRepository;
            _reportRepository = reportRepository;
            _reportService = reportService;
        }

        public async Task<CaseReportViewModel> GetCaseReportForUser(string userId, int caseId)
        {
            
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);

            Report report = GetExistingReport(userId, caseId);

            if (currentCase == null)
            {
                return null;
            }

            CaseReportViewModel caseReport = new CaseReportViewModel
            {
                CaseNumber = currentCase.CaseNumber,
                PatientAge = currentCase.PatientAge,
                TumourSize = currentCase.TumourSize,
                SlideUrl = currentCase.SlideURL,
                IsSubmitted = false
            };

            if (report != null)
            {
                caseReport.TumourGrade = report.TumourGrade;
                caseReport.NumberofLVI = report.NumberofLVI;
                caseReport.IsSubmitted = report.IsSubmitted;
            }

            return caseReport;
        }

        public Report GetExistingReport (string userId, int caseId)
        {
            var reportFilter = new ReportFilterSpecification(userId, caseId);
            return _reportRepository.GetSingleBySpec(reportFilter);
        }

        public async Task CreateOrUpdateCaseReport(CaseReportViewModel caseReport, Report existingReport, int caseId, string userId, bool isSubmitted = false)
        {
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);
            await _reportService.CreateOrUpdateReportFromCase(currentCase, existingReport, caseReport.TumourGrade, caseReport.NumberofLVI, userId, isSubmitted);
        }

        public async Task<IReadOnlyList<Case>> GetOrderedCasesAsync()
        {
            return await _caseRepository.ListAsync(new CaseFilterSpecification(null, false, true));
        }

        public async Task<int> GetCaseCount()
        {
            var allCasesSpecification = new CaseFilterSpecification(null);
            return await _caseRepository.CountAsync(allCasesSpecification);
        }

        public int[] GetSubmittedCaseReportIds(string userId)
        {
            return _reportRepository.GetSubmittedReportIdsForUser(userId);
        }

        public async Task GetPreTestProbabilityData(CaseReportViewModel caseReportViewModel, ReportStatistics statistics)
        {
            if (statistics.BayesForSize > 0)
            {
                await Task.Run(() =>_reportService.CalculatePreTestProbability(statistics, caseReportViewModel.TumourGrade));
            }
            else
            {
                // Assume we haven't yet calculated the first two bayes theorum values (probabilities based on age and tumour size)
                await Task.Run(() => _reportService.CalculateAgeBasedStatistics(statistics, caseReportViewModel.PatientAge));
                await Task.Run(() => _reportService.CalculateSizeBasedStatistics(statistics, caseReportViewModel.TumourSize));
                await Task.Run(() => _reportService.CalculatePreTestProbability(statistics, caseReportViewModel.TumourGrade));
            }

            return;            
        }

        public async Task<InterventionData> GetInterventionDataForCaseReport(CaseReportViewModel caseReportViewModel, string userId)
        {
            int[] chartXAxis;
            decimal[] theoreticalYValues;
            decimal[] observedYValues;

            Report previousReport = _reportService.GetPreviousUserReport(userId);

            int currentReportNumber = previousReport != null ? previousReport.UserReportNumber + 1 : 1;

            // Calculate statistics for the current case report on the fly
            ReportStatistics currentStatistics = await Task.Run(() => _reportService.CalculateStatistics(
                caseReportViewModel.PatientAge,
                caseReportViewModel.TumourSize,
                caseReportViewModel.TumourGrade,
                caseReportViewModel.NumberofLVI,
                previousReport));

            // Get the theoretical series values (x and y) 
            theoreticalYValues = new decimal[currentReportNumber + 1];
            observedYValues = new decimal[currentReportNumber + 1];
            chartXAxis = new int[currentReportNumber + 1];

            BinomialDistribution binomDist = new BinomialDistribution(currentReportNumber, (double)currentStatistics.CumulativeAverageBayesForGrade);

            for (int i = 0; i <= currentReportNumber; i++)
            {
                theoreticalYValues[i] = (decimal)binomDist.ProbabilityMassFunction(i);
                observedYValues[i] = currentStatistics.CumulativeCasesWithLVIPos == i ? currentStatistics.BinomialDist : 0;
                chartXAxis[i] = i;
            }

            return new InterventionData()
            {
                TheoreticalYValues = theoreticalYValues,
                ObservedYValues = observedYValues,
                ChartXAxis = chartXAxis,
                PreTestProbability = currentStatistics.BayesForGrade,
                PostTestProbability = currentStatistics.BayesForNumberOfLVI,
                LVIReported = caseReportViewModel.NumberofLVI > 0 ? true : false
            };
        }
    }
}
