//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Main.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employee
    {
        public string emp_id { get; set; }
        public string fname { get; set; }
        public string minit { get; set; }
        public string lname { get; set; }
        public short job_id { get; set; }
        public Nullable<byte> job_lvl { get; set; }
        public string pub_id { get; set; }
        public System.DateTime hire_date { get; set; }
    
        public virtual jobs jobs { get; set; }
        public virtual publishers publishers { get; set; }
    }
}
