using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlugIn02
{
    public class PlugIn
    {
        SolidColorBrush darkGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF024F00"));
        SolidColorBrush green = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF11AA0D"));
        SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2DE029"));
        SolidColorBrush veryLightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF60F35D"));

        public void ChangeWindowColors(Window window)
        {
            window.Background = green;
            if (window.Content is Grid)
            {
                Grid grid = window.Content as Grid;
                foreach (var item in grid.Children)
                {
                    if (item is DataGrid)
                    {
                        (item as DataGrid).Background = veryLightGreen;
                        (item as DataGrid).RowBackground = green;
                        (item as DataGrid).AlternatingRowBackground = lightGreen;

                        Style st = new Style();
                        st.Setters.Add(new Setter(GridViewColumnHeader.BackgroundProperty, darkGreen));
                        (item as DataGrid).ColumnHeaderStyle = st;
                    }
                    else if (item is Grid && (item as Grid).Name.StartsWith("QQ"))
                    {
                        (item as Grid).Background = darkGreen;
                        foreach (var item2 in (item as Grid).Children)
                        {
                            if (item2 is ComboBox)
                            {
                                Style st = new Style(typeof(ComboBoxItem));
                                st.Setters.Add(new Setter(ComboBoxItem.BackgroundProperty, veryLightGreen));
                                st.Setters.Add(new Setter(ComboBoxItem.BorderBrushProperty, veryLightGreen));
                                (item2 as ComboBox).ItemContainerStyle = st;
                            }
                            else if (item2 is Menu)
                            {
                                foreach (var item3 in (item2 as Menu).Items)
                                {
                                    (item3 as MenuItem).Background = lightGreen;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
