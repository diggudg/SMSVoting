//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpKeywordMap
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string Band { get; set; }
        public string Keyword { get; set; }
        public Nullable<int> Votes { get; set; }
        public string Name { get; set; }
    }
}