using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTypeCode
{
    internal static class CrmConnect365
    {
        internal static IOrganizationService CreateConnection()
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["adminName"];
            credentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["adminPassword"];
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["CRM-COM-IFD"]))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Uri serviceUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["organizationUri"]);
            OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            proxy.EnableProxyTypes();
            var _service = (IOrganizationService)proxy;
            return _service;
        }
        /*
        internal static IOrganizationService CreateConnection()
        {
            Uri OrganizationUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["organizationUri"]);
            string userName = System.Configuration.ConfigurationManager.AppSettings["adminName"];
            string password = System.Configuration.ConfigurationManager.AppSettings["adminPassword"];
            string domain = userName.Split('\\')[0];
            userName = userName.Split('\\')[1];
            ClientCredentials userCredentials = new ClientCredentials();
            userCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(userName, password, domain);
            var _serviceProxy = new OrganizationServiceProxy(OrganizationUri, null, userCredentials, null);
            return (IOrganizationService)_serviceProxy;
        }
        */
        internal static IOrganizationService CreateConnection(string url, string username, string password, string domain)
        {
            Uri OrganizationUri = new Uri(url);
            ClientCredentials userCredentials = new ClientCredentials();
            userCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(username, password, domain);
            var _serviceProxy = new OrganizationServiceProxy(OrganizationUri, null, userCredentials, null);
            return (IOrganizationService)_serviceProxy;
        }
    }
}
