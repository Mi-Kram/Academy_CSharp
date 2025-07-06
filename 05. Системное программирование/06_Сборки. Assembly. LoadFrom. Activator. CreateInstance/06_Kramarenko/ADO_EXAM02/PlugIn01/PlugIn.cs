using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace PlugIn01
{
    public class PlugIn
    {
        SolidColorBrush darkRed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7A0101"));
        SolidColorBrush red = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0000"));
        SolidColorBrush lightRed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFD3D3D"));
        SolidColorBrush veryLightRed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF6464"));

        public void ChangeWindowColors(Window window)
        {
            window.Background = red;
            if(window.Content is Grid)
            {
                Grid grid = window.Content as Grid;
                foreach (var item in grid.Children)
                {
                    if(item is DataGrid)
                    {
                        (item as DataGrid).Background = veryLightRed;
                        (item as DataGrid).RowBackground = red;
                        (item as DataGrid).AlternatingRowBackground = lightRed;

                        Style st = new Style();
                        st.Setters.Add(new Setter(GridViewColumnHeader.BackgroundProperty, darkRed));
                        (item as DataGrid).ColumnHeaderStyle = st;
                    }
                    else if(item is Grid && (item as Grid).Name.StartsWith("QQ"))
                    {
                        (item as Grid).Background = darkRed;
                        foreach (var item2 in (item as Grid).Children)
                        {
                            if (item2 is ComboBox)
                            {
                                Style st = new Style(typeof(ComboBoxItem));
                                st.Setters.Add(new Setter(ComboBoxItem.BackgroundProperty, veryLightRed));
                                st.Setters.Add(new Setter(ComboBoxItem.BorderBrushProperty, veryLightRed));
                                (item2 as ComboBox).ItemContainerStyle = st;
                            }
                            else if (item2 is Menu)
                            {
                                foreach (var item3 in (item2 as Menu).Items)
                                {
                                    (item3 as MenuItem).Background = lightRed;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
