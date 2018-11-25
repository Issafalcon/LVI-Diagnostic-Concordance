using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public static struct DataConstants
    {
        public const decimal ProbLVIPos = 0.296169989506821;
        public const decimal ProbLVINeg = 0.703830010493179;
        public const decimal LVIRiskPreTestMin = 0.00192686314110848;
        public const decimal LVIRiskPreTestMax = 1;
        public const decimal Interval = 0.1996146273717783;
        public const decimal SecondInterval = 0.3992292547435566;
        public const decimal ThirdInterval = 0.598843882115335;
        public const decimal FourthInterval = 0.7984585094871133;

        public const decimal BelowFiftyYearsLVIPos = 0.500886524822695;
        public const decimal BelowFiftyYearsLVINeg = 0.376730265619155;
        public const decimal BelowOnecmLVIPos = 0.0767123287671233;
        public const decimal BelowOnecmLVINeg = 0.229457364341085;
        public const decimal OneToTwocmLVIPos = 0.4337899543379;
        public const decimal OneToTwocmLVINeg = 0.50077519379845;
        public const decimal TwoToFivecmLVIPos = 0.489497716894977;
        public const decimal TwoToFivecmLVINeg = 0.269767441860465;
        public const decimal GradeOneLVINeg = 0.0741503604531411;
        public const decimal GradeOneLVIPos = 0.240805604203152;
        public const decimal GradeTwoLVINeg = 0.331616889804325;
        public const decimal GradeTwoLVIPos = 0.387040280210158;
        public const decimal GradeThreeLVIPos = 0.594232749742533;
        public const decimal GradeThreeLVINeg = 0.376730265619155;
        public const decimal ZeroLVIImagesLVIPos = 0.04;
        public const decimal ZeroLVIImagesLVINeg = 0.71875;
        public const decimal OneToTwoLVIImagesLVIPos = 0.44;
        public const decimal OneToTwoLVIImagesLVINeg = 0.25;
        public const decimal TwoToThreeLVIImagesLVIPos = 0.12;
        public const decimal TwoToThreeLVIImagesLVINeg = 0.03125;
        public const decimal FivePlusLVIImagesLVIPos = 0.4;
        public const decimal FivePlusLVIImagesLVINeg = 0;

    }
}
