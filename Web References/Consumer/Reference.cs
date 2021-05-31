﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace CuBES_Maintenance.Consumer {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MaintenanceSoap", Namespace="http://localhost/WSConsumer")]
    public partial class Maintenance : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GenerateMenuOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Maintenance() {
            this.Url = "http://localhost/WSConsumer/Maintenance.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GenerateMenuCompletedEventHandler GenerateMenuCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/WSConsumer/GenerateMenu", RequestNamespace="http://localhost/WSConsumer", ResponseNamespace="http://localhost/WSConsumer", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void GenerateMenu(string groupID, string path, string menuDir, string connString, string moduleID) {
            this.Invoke("GenerateMenu", new object[] {
                        groupID,
                        path,
                        menuDir,
                        connString,
                        moduleID});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGenerateMenu(string groupID, string path, string menuDir, string connString, string moduleID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GenerateMenu", new object[] {
                        groupID,
                        path,
                        menuDir,
                        connString,
                        moduleID}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndGenerateMenu(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void GenerateMenuAsync(string groupID, string path, string menuDir, string connString, string moduleID) {
            this.GenerateMenuAsync(groupID, path, menuDir, connString, moduleID, null);
        }
        
        /// <remarks/>
        public void GenerateMenuAsync(string groupID, string path, string menuDir, string connString, string moduleID, object userState) {
            if ((this.GenerateMenuOperationCompleted == null)) {
                this.GenerateMenuOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateMenuOperationCompleted);
            }
            this.InvokeAsync("GenerateMenu", new object[] {
                        groupID,
                        path,
                        menuDir,
                        connString,
                        moduleID}, this.GenerateMenuOperationCompleted, userState);
        }
        
        private void OnGenerateMenuOperationCompleted(object arg) {
            if ((this.GenerateMenuCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateMenuCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GenerateMenuCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591