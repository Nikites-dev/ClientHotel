//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientHotel.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservationUser
    {
        public int IdReservationUser { get; set; }
        public Nullable<int> IdReservation { get; set; }
        public Nullable<int> IdUser { get; set; }
    
        public virtual Reservation Reservation { get; set; }
        public virtual User User { get; set; }
    }
}
