using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Main.ValidationAttributes
{
	public class UserBirthdayCheckAttribute : ValidationAttribute
	{
        public override bool IsValid(object value)
        {
            try
            {
                if (value is DateTime)
                {
                    DateTime date = (DateTime)value;
                    DateTime max = DateTime.Now;
                    DateTime min = max.AddYears(-100);
                    return date > min && date < max;
                }
            }
            catch { }
            return false;
        }
    }
}
