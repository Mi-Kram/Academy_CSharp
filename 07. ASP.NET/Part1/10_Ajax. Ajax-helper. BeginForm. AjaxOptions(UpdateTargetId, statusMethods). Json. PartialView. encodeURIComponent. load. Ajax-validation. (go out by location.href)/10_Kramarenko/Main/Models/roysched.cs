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
    
    public partial class roysched
    {
        public string title_id { get; set; }
        public Nullable<int> lorange { get; set; }
        public Nullable<int> hirange { get; set; }
        public Nullable<int> royalty { get; set; }
    
        public virtual titles titles { get; set; }
    }
}
