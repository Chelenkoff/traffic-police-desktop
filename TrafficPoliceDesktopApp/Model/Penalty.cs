using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficPoliceDesktopApp.Model
{
    class Penalty
    {
        private long penaltyId;
        private Userr user;
        private DriverOwner driverOwner;
        private DateTime penaltyIssuedDateTime;
        private DateTime penaltyDateTime;
        private string location;
        private string description;
        private string disagreement;

        public long PenaltyId
        {
            get { return penaltyId; }
            set { penaltyId = value; }
        }
        public Userr User
        {
            get { return user; }
            set { user = value; }
        }
        public DriverOwner DriverOwner
        {
            get { return driverOwner; }
            set { driverOwner = value; }
        }
        public DateTime PenaltyIssuedDateTime
        {
            get { return penaltyIssuedDateTime; }
            set { penaltyIssuedDateTime = value; }
        }
        public DateTime PenaltyDateTime
        {
            get { return penaltyDateTime; }
            set { penaltyDateTime = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Disagreement
        {
            get { return disagreement; }
            set { disagreement = value; }
        }
    }
}
