﻿namespace CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors
{
    using System.Collections.Generic;

    using CarbonCore.Processing.Processors.Excel;

    public interface IExcelFormatter
    {
        string Format(IList<ExcelData> data);
    }
}
