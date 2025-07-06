using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_FileUpload.Models
{
	public class GaleryViewModelModel
	{
		public string Type { get; set; }
		public List<ImageModel> Images { get; set; }
	}
}
