//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Geodeta.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Line
    {
        public Line()
        {
            this.ListOfPoints = new HashSet<ListOfPoints>();
        }
    
        public int ID { get; set; }
        public int AreaId { get; set; }
        public Nullable<int> NoteId { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Note Note { get; set; }
        public virtual ICollection<ListOfPoints> ListOfPoints { get; set; }
    }
}
