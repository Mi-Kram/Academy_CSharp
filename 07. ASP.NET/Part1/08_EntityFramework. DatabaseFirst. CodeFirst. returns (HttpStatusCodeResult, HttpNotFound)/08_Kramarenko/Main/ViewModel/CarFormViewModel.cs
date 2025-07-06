using Main.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.ViewModel
{
  public class CarFormViewModel
  {
    public bool IsNew { get; set; } = false;
    public List<CarType> AllCarTypes { get; } = new List<CarType>();
    public int ID { get; set; }
    public string ImgSrc { get; set; }



    [Required(ErrorMessage = "Please, enter car's brand.")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "Please, enter car's model.")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Please, enter car's speed.")]
    [Range(0, 1250, ErrorMessage= "Please, enter valid car's speed.")]
    public double Speed { get; set; }

    [Required(ErrorMessage = "Please, enter car's price.")]
    [Range(0, double.MaxValue, ErrorMessage = "Please, enter valid car's speed.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Please, enter car's date.")]
    public string DateStr { get; set; }

    [Required(ErrorMessage = "Please, choose car's type.")]
    //[Range(1, 4, ErrorMessage = "Please, choose car's type.")]
    public int TypeID { get; set; }

    [Required(ErrorMessage = "Please, choose car's image.")]
    public HttpPostedFileBase[] Files { get; set; }

    public CarFormViewModel() { }
    
    public CarFormViewModel(bool _isNew, List<CarType> carTypes)
    {
      IsNew = _isNew;
      AllCarTypes = carTypes;
    }
  }
}
