using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficPoliceDesktopApp.Model
{
    class Userr
    {
        private long userId;
        private string firstName;
        private string secondName;
        private string lastName;
        private bool isTrafficPoliceman;
        private string userPassword;





        //Constructor
        public Userr(long uid, string firstName, string secondName, string lastName, bool isPoliceman)
        {
            userId = uid;
            this.firstName = firstName;
            this.secondName = secondName;
            this.lastName = lastName;
            isTrafficPoliceman = isPoliceman;
        }



        public long UserId
        {
            get { return userId; }
            set { userId = value; }
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
        public bool IsTrafficPoliceman
        {
            get { return isTrafficPoliceman; }
            set { isTrafficPoliceman = value; }
        }
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
    }
}
