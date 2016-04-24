using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDBService
{
     [DataContract]
    public  class Categories
    {
        [DataMember]
        public DateTime? a1AcquiryDate { get; set; }
        [DataMember]
        public DateTime? a1ExpiryDate { get; set; }
        [DataMember]
        public string a1Restrictions { get; set; }

        [DataMember]
        public DateTime? aAcquiryDate { get; set; }
        [DataMember]
        public DateTime? aExpiryDate { get; set; }
        [DataMember]
        public string aRestrictions { get; set; }

        [DataMember]
        public DateTime? b1AcquiryDate { get; set; }
        [DataMember]
        public DateTime? b1ExpiryDate { get; set; }
        [DataMember]
        public string b1Restrictions { get; set; }

        [DataMember]
        public DateTime? bAcquiryDate { get; set; }
        [DataMember]
        public DateTime? bExpiryDate { get; set; }
        [DataMember]
        public string bRestrictions { get; set; }

        [DataMember]
        public DateTime? c1AcquiryDate { get; set; }
        [DataMember]
        public DateTime? c1ExpiryDate { get; set; }
        [DataMember]
        public string c1Restrictions { get; set; }

        [DataMember]
        public DateTime? cAcquiryDate { get; set; }
        [DataMember]
        public DateTime? cExpiryDate { get; set; }
        [DataMember]
        public string cRestrictions { get; set; }

        [DataMember]
        public DateTime? d1AcquiryDate { get; set; }
        [DataMember]
        public DateTime? d1ExpiryDate { get; set; }
        [DataMember]
        public string d1Restrictions { get; set; }

        [DataMember]
        public DateTime? dAcquiryDate { get; set; }
        [DataMember]
        public DateTime? dExpiryDate { get; set; }
        [DataMember]
        public string dRestrictions { get; set; }

        [DataMember]
        public DateTime? beAcquiryDate { get; set; }
        [DataMember]
        public DateTime? beExpiryDate { get; set; }
        [DataMember]
        public string beRestrictions { get; set; }

        [DataMember]
        public DateTime? c1eAcquiryDate { get; set; }
        [DataMember]
        public DateTime? c1eExpiryDate { get; set; }
        [DataMember]
        public string c1eRestrictions { get; set; }

        [DataMember]
        public DateTime? ceAcquiryDate { get; set; }
        [DataMember]
        public DateTime? ceExpiryDate { get; set; }
        [DataMember]
        public string ceRestrictions { get; set; }

        [DataMember]
        public DateTime? d1eAcquiryDate { get; set; }
        [DataMember]
        public DateTime? d1eExpiryDate { get; set; }
        [DataMember]
        public string d1eRestrictions { get; set; }

        [DataMember]
        public DateTime? deAcquiryDate { get; set; }
        [DataMember]
        public DateTime? deExpiryDate { get; set; }
        [DataMember]
        public string deRestrictions { get; set; }

        [DataMember]
        public DateTime? ttbAcquiryDate { get; set; }
        [DataMember]
        public DateTime? ttbExpiryDate { get; set; }
        [DataMember]
        public string ttbRestrictions { get; set; }

        [DataMember]
        public DateTime? ttmAcquiryDate { get; set; }
        [DataMember]
        public DateTime? ttmExpiryDate { get; set; }
        [DataMember]
        public string ttmRestrictions { get; set; }

        [DataMember]
        public DateTime? tktAcquiryDate { get; set; }
        [DataMember]
        public DateTime? tktExpiryDate { get; set; }
        [DataMember]
        public string tktRestrictions { get; set; }

        [DataMember]
        public DateTime? mAcquiryDate { get; set; }
        [DataMember]
        public DateTime? mExpiryDate { get; set; }
        [DataMember]
        public string mRestrictions { get; set; }

        [OperationBehavior]
        void Clr()
        {
            a1AcquiryDate = null;
            a1ExpiryDate = null;
            a1Restrictions = "";

            aAcquiryDate = null;
            aExpiryDate = null;
            aRestrictions = "";

            b1AcquiryDate = null;
            b1ExpiryDate = null;
            b1Restrictions = "";

            bAcquiryDate = null;
            bExpiryDate = null;
            bRestrictions = "";

            c1AcquiryDate = null;
            c1ExpiryDate = null;
            c1Restrictions = "";

            cAcquiryDate = null;
            cExpiryDate = null;
            cRestrictions = "";

            d1AcquiryDate = null;
            d1ExpiryDate = null;
            d1Restrictions = "";

            dAcquiryDate = null;
            dExpiryDate = null;
            dRestrictions = "";

            beAcquiryDate = null;
            beExpiryDate = null;
            beRestrictions = "";

            c1eAcquiryDate = null;
            c1eExpiryDate = null;
            c1eRestrictions = "";

            ceAcquiryDate = null;
            ceExpiryDate = null;
            ceRestrictions = "";

            d1eAcquiryDate = null;
            d1eExpiryDate = null;
            d1eRestrictions = "";

            deAcquiryDate = null;
            deExpiryDate = null;
            deRestrictions = "";

            ttbAcquiryDate = null;
            ttbExpiryDate = null;
            ttbRestrictions = "";

            ttmAcquiryDate = null;
            ttmExpiryDate = null;
            ttmRestrictions = "";

            tktAcquiryDate = null;
            tktExpiryDate = null;
            tktRestrictions = "";

            mAcquiryDate = null;
            mExpiryDate = null;
            mRestrictions = "";
        }
    }
}
