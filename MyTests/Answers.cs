//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyTests
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answers
    {
        public int IdUserAnswer { get; set; }
        public int IdQuestion { get; set; }
        public int IdUser { get; set; }
        public string Answer { get; set; }
    
        public virtual Questions Questions { get; set; }
        public virtual Users Users { get; set; }
    }
}
