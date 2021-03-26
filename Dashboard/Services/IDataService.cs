using Dashboard.Models;
using Dashboard.Models.Interfaces;
using System.Collections.Generic;

namespace Dashboard.Services
{
    public interface IDataService
    {
        Report GetData(string filePath);
    }
}