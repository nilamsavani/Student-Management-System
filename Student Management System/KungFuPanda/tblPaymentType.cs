//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KungFuPanda
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPaymentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPaymentType()
        {
            this.tblPayments = new HashSet<tblPayment>();
        }
    
        public int PAYMENT_TYPE_ID { get; set; }
        public string PAYMENT_TYPE { get; set; }
        public Nullable<bool> PAYMENT_TYPE_STATUS { get; set; }
        public Nullable<int> PAYMENT_TYPE_CREATED_BY { get; set; }
        public Nullable<System.DateTime> PAYMENT_TYPE_CREATED_DATE { get; set; }
        public Nullable<int> PAYMENT_TYPE_MODIFY_BY { get; set; }
        public Nullable<System.DateTime> PAYMENT_TYPE_MODIFIED_DATE { get; set; }
        public Nullable<decimal> PAYMENT_TYPE_AMOUNT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPayment> tblPayments { get; set; }
    }
}
