using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;

//It is event better to put the classes into a shared component
namespace NP_WCF_Auth_Service
{
    public class IdentityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if ((userName != "Alex") || (password != "pass"))
            {
                var msg = String.Format("Unknown Username {0} or incorrect password {1}", userName, password);
                throw new FaultException(msg);
            }
        }
    }
}
