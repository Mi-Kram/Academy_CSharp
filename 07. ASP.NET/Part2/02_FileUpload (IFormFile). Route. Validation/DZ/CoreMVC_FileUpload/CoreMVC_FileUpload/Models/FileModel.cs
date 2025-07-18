﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreMVC_FileUpload.Models
{
    public class FileModel
    {
        [Required(ErrorMessage = "Вы должны выбрать хотя бы один файл")]
        [Display(Name = "Выбор файлов")]
        public IFormFileCollection files { get; set; }
    }
}