using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    internal struct DataConstants
    {
        internal const decimal ProbLVIPos = 0.296169989506821m;
        internal const decimal ProbLVINeg = 0.703830010493179m;
        internal const decimal LVIRiskPreTestMin = 0.00192686314110848m;
        internal const decimal LVIRiskPreTestMax = 1;
        internal const decimal Interval = 0.1996146273717783m;
        internal const decimal SecondInterval = 0.3992292547435566m;
        internal const decimal ThirdInterval = 0.598843882115335m;
        internal const decimal FourthInterval = 0.7984585094871133m;

        internal const decimal BelowFiftyYearsLVIPos = 0.500886524822695m;
        internal const decimal BelowFiftyYearsLVINeg = 0.376730265619155m;
        internal const decimal BelowOnecmLVIPos = 0.0767123287671233m;
        internal const decimal BelowOnecmLVINeg = 0.229457364341085m;
        internal const decimal OneToTwocmLVIPos = 0.4337899543379m;
        internal const decimal OneToTwocmLVINeg = 0.50077519379845m;
        internal const decimal TwoToFivecmLVIPos = 0.489497716894977m;
        internal const decimal TwoToFivecmLVINeg = 0.269767441860465m;

        // Might have numbers for grade constants back to front ? Ask Pablo
        internal const decimal GradeOneLVINeg = 0.0741503604531411m;
        internal const decimal GradeOneLVIPos = 0.240805604203152m;
        internal const decimal GradeTwoLVINeg = 0.331616889804325m;
        internal const decimal GradeTwoLVIPos = 0.387040280210158m;
        internal const decimal GradeThreeLVIPos = 0.594232749742533m;
        internal const decimal GradeThreeLVINeg = 0.37215411558669m;
        internal const decimal ZeroLVIImagesLVIPos = 0.04m;
        internal const decimal ZeroLVIImagesLVINeg = 0.71875m;
        internal const decimal OneToTwoLVIImagesLVIPos = 0.44m;
        internal const decimal OneToTwoLVIImagesLVINeg = 0.25m;
        internal const decimal TwoToThreeLVIImagesLVIPos = 0.12m;
        internal const decimal TwoToThreeLVIImagesLVINeg = 0.03125m;
        internal const decimal FivePlusLVIImagesLVIPos = 0.4m;
        internal const decimal FivePlusLVIImagesLVINeg = 0;

    }
}
