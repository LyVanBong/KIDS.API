//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KIDS.API.Database
{
    using System;
    
    public partial class sp_Teacher_Communications_sel_Result
    {
        public System.Guid CommunicationID { get; set; }
        public string StudentID { get; set; }
        public Nullable<System.Guid> TeacherID { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> Type { get; set; }
        public string Content { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string NguoiGui { get; set; }
        public string Picture { get; set; }
        public string Approver { get; set; }
        public Nullable<System.DateTime> ApproverDate { get; set; }
    }
}
