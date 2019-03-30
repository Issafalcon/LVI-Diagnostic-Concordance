﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public interface IExcelWriter
    {
        Byte[] WriteToExcel<T>(List<T> items);
    }
}
