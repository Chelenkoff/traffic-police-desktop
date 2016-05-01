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
        private const string DEFAULT_NATIONALITY = "България";
        private const byte DEFAULT_PTS = 39;


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
        public string TelNum { get; set; }
        [DataMember]
        public string Email { get; set; }

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
         [DataMember]
         public List<Penalty> Penalties { get; set; }


        //Constructor
        public  DriverOwner()
        {
            DriverOwnerId = 0;
            FirstName =string.Empty;
            SecondName = string.Empty;
            LastName = string.Empty;

            Sex = SexEnum.Man;


            Nationality = DEFAULT_NATIONALITY;
            BirthDate = new DateTime();
            BirthPlace = string.Empty;
            Residence = string.Empty;
            TelNum = string.Empty;
            Email = string.Empty;
            RemainingPts = DEFAULT_PTS;
            LicenceIssueDate = DateTime.Now;
            LicenceExpiryDate = LicenceIssueDate.AddYears(10);
            LicenceIssuedBy = string.Empty;

            Categories = new Categories();


        }


    }

    

    [DataContract(Name = "Sex")]
    [Flags]
    public enum SexEnum
    {
        [EnumMember]
        Man = 'М',
        [EnumMember]
        Woman = 'Ж'

    }


}
