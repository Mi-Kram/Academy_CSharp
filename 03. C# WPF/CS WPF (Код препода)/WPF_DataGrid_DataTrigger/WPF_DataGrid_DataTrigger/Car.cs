using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DataGrid_DataTrigger
{
    public class Car : INotifyPropertyChanged
    {
        public Car(int id, string brand, string model, double speed, double price)
        {
            ID = id;
            Brand = brand;
            Model = model;
            Speed = speed;
            Price = price;
        }

        public int ID { get; set; }

        public string Brand { get; set; }
        
        public string Model { get; set; }

        double price;

        public double Price
        {
            get { return price; }
            set
            {
                price = value;

                // уведомить элементы управление об изменении свойства
                OnPropertyChanged("Price");
            }
        }

        public double Speed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
