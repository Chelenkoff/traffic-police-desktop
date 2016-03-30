using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficPoliceDesktopApp.Model
{
    public enum Sex { M, F};
    class DriverOwner
    {
        private long driverOwnerId;
        private string firstName;
        private string secondName;
        private string lastName;
        private Sex sex;
        private string nationality;
        private DateTime birthDate;
        private string birthPlace;
        private string residence;
        private long licenceNum;
        private byte remainingPts;
        private DateTime licenceIssueDate;
        private DateTime licenceExpiryDate;
        private string licenceIssuedBy;

        private Categories categories;


        public DriverOwner()
        {
            categories = new Categories();
        }




        public long DriverOwnerId
        {
            get { return driverOwnerId; }
            set { driverOwnerId = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string SecondName
        {
            get { return secondName; }
            set { secondName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public Sex Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        public string BirthPlace
        {
            get { return birthPlace; }
            set { birthPlace = value; }
        }
        public string Residence
        {
            get { return residence; }
            set { residence = value; }
        }
        public long LicenceNum
        {
            get { return licenceNum; }
            set { licenceNum = value; }
        }
        public byte RemainingPts
        {
            get { return remainingPts; }
            set { remainingPts = value; }
        }

        public DateTime LicenceIssueDate
        {
            get { return licenceIssueDate; }
            set { licenceIssueDate = value; }
        }
        public DateTime LicenceExpiryDate
        {
            get { return licenceExpiryDate; }
            set { licenceExpiryDate = value; }
        }

        public string LicenceIssuedBy
        {
            get { return licenceIssuedBy; }
            set { licenceIssuedBy = value; }
        }
        public Categories Categories
        {
            get { return categories; }
            set { categories = value; }
        }
    }
        
}
