using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlugIn03
{
    public class PlugIn
    {
        SolidColorBrush darkBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF070B67"));
        SolidColorBrush blue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1219CC"));
        SolidColorBrush lightBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3A40DA"));
        SolidColorBrush veryLightBlue = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF676BE3"));

        public void ChangeWindowColors(Window window)
        {
            window.Background = blue;
            if (window.Content is Grid)
            {
                Grid grid = window.Content as Grid;
                foreach (var item in grid.Children)
                {
                    if (item is DataGrid)
                    {
                        (item as DataGrid).Background = veryLightBlue;
                        (item as DataGrid).RowBackground = blue;
                        (item as DataGrid).AlternatingRowBackground = lightBlue;

                        Style st = new Style();
                        st.Setters.Add(new Setter(GridViewColumnHeader.BackgroundProperty, darkBlue));
                        (item as DataGrid).ColumnHeaderStyle = st;
                    }
                    else if (item is Grid && (item as Grid).Name.StartsWith("QQ"))
                    {
                        (item as Grid).Background = darkBlue;
                        foreach (var item2 in (item as Grid).Children)
                        {
                            if (item2 is ComboBox)
                            {
                                Style st = new Style(typeof(ComboBoxItem));
                                st.Setters.Add(new Setter(ComboBoxItem.BackgroundProperty, veryLightBlue));
                                st.Setters.Add(new Setter(ComboBoxItem.BorderBrushProperty, veryLightBlue));
                                (item2 as ComboBox).ItemContainerStyle = st;
                            }
                            else if (item2 is Menu)
                            {
                                foreach (var item3 in (item2 as Menu).Items)
                                {
                                    (item3 as MenuItem).Background = lightBlue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
