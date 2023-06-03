using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult Show(string text, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Asterisk, MessageBoxResult defaultResult = MessageBoxResult.None, MessageBoxOptions options = MessageBoxOptions.None)
        {
            return MessageBox.Show(text, caption, button, icon, defaultResult, options);
        }
    }
}
