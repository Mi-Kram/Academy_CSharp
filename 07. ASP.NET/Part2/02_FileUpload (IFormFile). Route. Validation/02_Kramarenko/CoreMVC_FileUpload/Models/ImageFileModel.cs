using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreMVC_FileUpload.Models
{
  public class ImageFileModel
  {
    public IFormFileCollection files { get; set; }
    public string image_type { get; set; }
    public string current_type { get; set; }
  }
}