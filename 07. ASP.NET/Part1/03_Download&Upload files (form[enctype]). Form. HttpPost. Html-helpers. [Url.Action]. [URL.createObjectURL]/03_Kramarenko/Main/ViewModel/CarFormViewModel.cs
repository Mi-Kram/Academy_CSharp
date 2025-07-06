using Main.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Main.ViewModel
{
    public class CarFormViewModel
    {
        public bool IsNew { get; set; } = false;
        public List<CarType> AllCarTypes { get; } = CarTypes.Types;



        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Speed { get; set; }
        public double Price { get; set; }
        public string DateStr { get; set; }
        public int TypeID { get; set; }
        public string ImgSrc { get; set; }
        public HttpPostedFileBase[] Files { get; set; }

        public CarFormViewModel() { }

        public CarFormViewModel(bool _isNew = false)
        {
            IsNew = _isNew;
        }


    }
}