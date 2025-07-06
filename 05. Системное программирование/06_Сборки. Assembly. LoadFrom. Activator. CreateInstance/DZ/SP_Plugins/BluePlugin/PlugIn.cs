using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BluePlugin
{
    public class PlugIn
    {
        public void ChangeWindow(Window window)
        {
            window.Background = new SolidColorBrush(Colors.LightBlue);

            // Сформировать стиль для кнопок главного окна
            Style st = new Style(typeof(Button));
            Setter setter1 = new Setter(Button.BackgroundProperty, Brushes.LightSalmon);
            Setter setter2 = new Setter(Button.CursorProperty, Cursors.Cross);
            st.Setters.Add(setter1);
            st.Setters.Add(setter2);

            // Применить стиль для кнопок главного окна
            if(window.Content is Grid)
            {
                Grid mainContainer = (Grid)window.Content;
                foreach (UIElement currentElement in mainContainer.Children)
                {
                    if (currentElement is Button)
                        ((Button)currentElement).Style = st;
                }
            }
        }
    }
}
