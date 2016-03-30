using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficPoliceDesktopApp.Model
{
    class Registration
    {
        private DriverOwner driverOwner;
        private string carManufacturer;
        private string carModel;
        private string carType;
        private string carVin;
        private DateTime firstRegDate;
        private long docNum;
        private bool civilInsurance;
        private string civilInsurer;
        private DateTime civilInsuranceStartDate;
        private DateTime civilInsuranceEndDate;
        private bool hasVignette;
        private DateTime vignetteValidUntil;
        private bool damageInsurance;
        private string damageInsurer;
        private DateTime damageInsuranceStartDate;
        private DateTime damageInsuranceEndDate;
        private DateTime recentCertificateRegDate;


        
        public DriverOwner DriverOwner
        {
            get { return driverOwner; }
            set { driverOwner = value; }
        }
        public string CarManufacturer
        {
            get { return carManufacturer; }
            set { carManufacturer = value; }
        }
        public string CarModel
        {
            get { return carModel; }
            set { carModel = value; }
        }
        public string CarType
        {
            get { return carType; }
            set { carType = value; }
        }
        public string CarVin
        {
            get { return carVin; }
            set { carVin = value; }
        }
        public DateTime FirstRegDate
        {
            get { return firstRegDate; }
            set { firstRegDate = value; }
        }
        public long DocNum
        {
            get { return docNum; }
            set { docNum = value; }
        }
        public bool CivilInsurance
        {
            get { return civilInsurance; }
            set { civilInsurance = value; }
        }
        public string CivilInsurer
        {
            get { return civilInsurer; }
            set { civilInsurer = value; }
        }
        public DateTime CivilInsuranceStartDate
        {
            get { return civilInsuranceStartDate; }
            set { civilInsuranceStartDate = value; }
        }
        public DateTime CivilInsuranceEndDate
        {
            get { return civilInsuranceEndDate; }
            set { civilInsuranceEndDate = value; }
        }
        public bool HasVignette
        {
            get { return hasVignette; }
            set { hasVignette = value; }
        }
        public DateTime VignetteValidUntil
        {
            get { return vignetteValidUntil; }
            set { vignetteValidUntil = value; }
        }
        public bool DamageInsurance
        {
            get { return damageInsurance; }
            set { damageInsurance = value; }
        }
        public string DamageInsurer
        {
            get { return damageInsurer; }
            set { damageInsurer = value; }
        }
        public DateTime DamageInsuranceStartDate
        {
            get { return damageInsuranceStartDate; }
            set { damageInsuranceStartDate = value; }
        }

        public DateTime DamageInsuranceEndDate
        {
            get { return damageInsuranceEndDate; }
            set { damageInsuranceEndDate = value; }
        }
    }
}
