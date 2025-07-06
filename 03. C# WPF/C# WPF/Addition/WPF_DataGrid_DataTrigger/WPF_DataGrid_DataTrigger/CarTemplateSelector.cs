using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_DataGrid_DataTrigger
{
    public class CarTemplateSelector: DataTemplateSelector
    {
        public DataTemplate FastTemplate { get; set; }

        public DataTemplate AverageTemplate { get; set; }

        public DataTemplate SlowTemplate { get; set; }

        public DataTemplate UltraFastTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Car car = item as Car;

            if (car != null)
            {
                if (car.Speed > 400)
                    return UltraFastTemplate;
                else if (car.Speed >= 300)
                    return FastTemplate;
                else if(car.Speed >= 200)
                    return AverageTemplate;
                else return SlowTemplate;
            }
            else return base.SelectTemplate(item, container);
        }
    }
}
