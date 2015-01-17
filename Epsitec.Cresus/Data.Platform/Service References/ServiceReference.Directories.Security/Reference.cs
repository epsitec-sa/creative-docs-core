﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Epsitec.Data.Platform.ServiceReference.Directories.Security {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directories/administration/v4/security", ConfigurationName="ServiceReference.Directories.Security.SecuritySoap")]
    public interface SecuritySoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directories/administration/v4/security/Login", ReplyAction="*")]
        string Login(string loginParam);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directories/administration/v4/security/Logout", ReplyAction="*")]
        string Logout(string authenticationParam);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directories/administration/v4/security/KeepSessionAlive", ReplyAction="*")]
        string KeepSessionAlive(string authenticationParam);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directories/administration/v4/security/Version", ReplyAction="*")]
        string Version();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SecuritySoapChannel : Epsitec.Data.Platform.ServiceReference.Directories.Security.SecuritySoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SecuritySoapClient : System.ServiceModel.ClientBase<Epsitec.Data.Platform.ServiceReference.Directories.Security.SecuritySoap>, Epsitec.Data.Platform.ServiceReference.Directories.Security.SecuritySoap {
        
        public SecuritySoapClient() {
        }
        
        public SecuritySoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SecuritySoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecuritySoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecuritySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Login(string loginParam) {
            return base.Channel.Login(loginParam);
        }
        
        public string Logout(string authenticationParam) {
            return base.Channel.Logout(authenticationParam);
        }
        
        public string KeepSessionAlive(string authenticationParam) {
            return base.Channel.KeepSessionAlive(authenticationParam);
        }
        
        public string Version() {
            return base.Channel.Version();
        }
    }
}
