using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Services
{
    public interface IAppSettings
    {
        ObservableCollection<Report> Reports { get; set; }
        void Save();
    }
}
