using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class FileDialogService : IFileDialogService
    {
        OpenFileDialog IFileDialogService.OpenFileDialog()
        {
            return new OpenFileDialog();
        }

        SaveFileDialog IFileDialogService.SaveFileDialog()
        {
            return new SaveFileDialog();
        }
    }
}
