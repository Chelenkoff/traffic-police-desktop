﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrafficPoliceDesktopApp.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateUser", ReplyAction="http://tempuri.org/IService1/UpdateUserResponse")]
        int UpdateUser(WCFDBService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUserByIdAndPass", ReplyAction="http://tempuri.org/IService1/GetUserByIdAndPassResponse")]
        WCFDBService.User GetUserByIdAndPass(string id, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetReadOnlyUserById", ReplyAction="http://tempuri.org/IService1/GetReadOnlyUserByIdResponse")]
        WCFDBService.User GetReadOnlyUserById(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertUserAndGetGeneratedId", ReplyAction="http://tempuri.org/IService1/InsertUserAndGetGeneratedIdResponse")]
        string InsertUserAndGetGeneratedId(WCFDBService.User usr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertCat", ReplyAction="http://tempuri.org/IService1/InsertCatResponse")]
        int InsertCat(WCFDBService.Categories categ);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertRegistration", ReplyAction="http://tempuri.org/IService1/InsertRegistrationResponse")]
        int InsertRegistration(WCFDBService.Registration reg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterDriverOwner", ReplyAction="http://tempuri.org/IService1/RegisterDriverOwnerResponse")]
        int RegisterDriverOwner(WCFDBService.DriverOwner drOwner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDriverOwnerById", ReplyAction="http://tempuri.org/IService1/GetDriverOwnerByIdResponse")]
        WCFDBService.DriverOwner GetDriverOwnerById(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/removePenalty", ReplyAction="http://tempuri.org/IService1/removePenaltyResponse")]
        int removePenalty(WCFDBService.Penalty pen);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getAvailableCarTypes", ReplyAction="http://tempuri.org/IService1/getAvailableCarTypesResponse")]
        string[] getAvailableCarTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getRegByRegNum", ReplyAction="http://tempuri.org/IService1/getRegByRegNumResponse")]
        WCFDBService.Registration getRegByRegNum(string regNum);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : TrafficPoliceDesktopApp.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<TrafficPoliceDesktopApp.ServiceReference1.IService1>, TrafficPoliceDesktopApp.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int UpdateUser(WCFDBService.User user) {
            return base.Channel.UpdateUser(user);
        }
        
        public WCFDBService.User GetUserByIdAndPass(string id, string password) {
            return base.Channel.GetUserByIdAndPass(id, password);
        }
        
        public WCFDBService.User GetReadOnlyUserById(string id) {
            return base.Channel.GetReadOnlyUserById(id);
        }
        
        public string InsertUserAndGetGeneratedId(WCFDBService.User usr) {
            return base.Channel.InsertUserAndGetGeneratedId(usr);
        }
        
        public int InsertCat(WCFDBService.Categories categ) {
            return base.Channel.InsertCat(categ);
        }
        
        public int InsertRegistration(WCFDBService.Registration reg) {
            return base.Channel.InsertRegistration(reg);
        }
        
        public int RegisterDriverOwner(WCFDBService.DriverOwner drOwner) {
            return base.Channel.RegisterDriverOwner(drOwner);
        }
        
        public WCFDBService.DriverOwner GetDriverOwnerById(string id) {
            return base.Channel.GetDriverOwnerById(id);
        }
        
        public int removePenalty(WCFDBService.Penalty pen) {
            return base.Channel.removePenalty(pen);
        }
        
        public string[] getAvailableCarTypes() {
            return base.Channel.getAvailableCarTypes();
        }
        
        public WCFDBService.Registration getRegByRegNum(string regNum) {
            return base.Channel.getRegByRegNum(regNum);
        }
    }
}
