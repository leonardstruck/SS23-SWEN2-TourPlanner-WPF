using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
