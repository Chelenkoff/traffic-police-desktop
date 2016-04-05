using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFDBService
{
    [DataContract]
    public class DriverOwner
    {
        [DataMember]
        public long DriverOwnerId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public SexEnum Sex { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string BirthPlace { get; set; }
        [DataMember]
        public string Residence { get; set; }

        [DataMember]
        public byte RemainingPts { get; set; }
        [DataMember]
        public DateTime LicenceIssueDate { get; set; }
        [DataMember]
        public DateTime LicenceExpiryDate { get; set; }
        [DataMember]
        public string LicenceIssuedBy { get; set; }
         [DataMember]
        public Categories Categories { get; set; }

    }
    [DataContract(Name = "Sex")]
    public enum SexEnum
    {
        [EnumMember]
        Man = 'М',
        [EnumMember]
        Woman = 'Ж'

    }
}
