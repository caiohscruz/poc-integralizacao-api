﻿using System.Reflection;

namespace APIMock.Services.ExcelService
{
    public interface IExcelService
    {
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
