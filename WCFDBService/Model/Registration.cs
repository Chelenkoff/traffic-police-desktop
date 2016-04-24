using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDBService
{
    [DataContract]
    public class Registration
    {
        [DataMember]
        public string RegNum { get; set; }
        [DataMember]
        public long DriverOwnerId { get; set; }
        [DataMember]
        public string CarManufacturer { get; set; }
        [DataMember]
        public string CarModel { get; set; }
        [DataMember]
        public string CarType { get; set; }
        [DataMember]
        public string CarColor { get; set; }

        [DataMember]
        public int CarCubage { get; set; }
        [DataMember]
        public int CarHp { get; set; }
        [DataMember]
        public string CarVin { get; set; }
        [DataMember]
        public DateTime FirstRegDate { get; set; }
        [DataMember]
        public DateTime RecentRegDate { get; set; }
        [DataMember]
        public bool HasCivilInsurance { get; set; }

        [DataMember]
        public string CivilInsurer { get; set; }
        [DataMember]
        public DateTime? CivilInsuranceStartDate { get; set; }
        [DataMember]
        public DateTime? CivilInsuranceEndDate { get; set; }

        [DataMember]
        public bool HasVignette { get; set; }
        [DataMember]
        public DateTime? VignetteValidUntil { get; set; }

        [DataMember]
        public bool HasDamageInsurance { get; set; }
        [DataMember]
        public string DamageInsurer { get; set; }
        [DataMember]
        public DateTime? DamageInsuranceStartDate { get; set; }
        [DataMember]
        public DateTime? DamageInsuranceEndDate { get; set; }
    }


}
