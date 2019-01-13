using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public struct DataConstants
    {
        public const decimal ProbLVIPos = 0.296169989506821m;
        public const decimal ProbLVINeg = 0.703830010493179m;
        public const decimal LVIRiskPreTestMin = 0.00192686314110848m;
        public const decimal LVIRiskPreTestMax = 1;
        public const decimal Interval = 0.1996146273717783m;
        public const decimal SecondInterval = 0.3992292547435566m;
        public const decimal ThirdInterval = 0.598843882115335m;
        public const decimal FourthInterval = 0.7984585094871133m;

        public const decimal BelowFiftyYearsLVIPos = 0.500886524822695m;
        public const decimal BelowFiftyYearsLVINeg = 0.376730265619155m;
        public const decimal BelowOnecmLVIPos = 0.0767123287671233m;
        public const decimal BelowOnecmLVINeg = 0.229457364341085m;
        public const decimal OneToTwocmLVIPos = 0.4337899543379m;
        public const decimal OneToTwocmLVINeg = 0.50077519379845m;
        public const decimal TwoToFivecmLVIPos = 0.489497716894977m;
        public const decimal TwoToFivecmLVINeg = 0.269767441860465m;

        // Might have numbers for grade constants back to front ? Ask Pablo
        public const decimal GradeOneLVINeg = 0.240805604203152m;        
        public const decimal GradeOneLVIPos = 0.0741503604531411m;
        public const decimal GradeTwoLVINeg = 0.387040280210158m;        
        public const decimal GradeTwoLVIPos = 0.331616889804325m;
        public const decimal GradeThreeLVIPos = 0.594232749742533m;
        public const decimal GradeThreeLVINeg = 0.37215411558669m;
        public const decimal ZeroLVIImagesLVIPos = 0.04m;
        public const decimal ZeroLVIImagesLVINeg = 0.71875m;
        public const decimal OneToTwoLVIImagesLVIPos = 0.44m;
        public const decimal OneToTwoLVIImagesLVINeg = 0.25m;
        public const decimal TwoToThreeLVIImagesLVIPos = 0.12m;
        public const decimal TwoToThreeLVIImagesLVINeg = 0.03125m;
        public const decimal FivePlusLVIImagesLVIPos = 0.4m;
        public const decimal FivePlusLVIImagesLVINeg = 0;

    }
}
