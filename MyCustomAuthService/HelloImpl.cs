using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;
using System.Threading;
using System.Security.Permissions;

namespace MyCustomAuthService
{
    public class HelloImpl : IHello
    {
        public string Hello(int value)
        {
            string res = string.Format("You entered: {0}", value);
            Console.WriteLine(Thread.CurrentPrincipal.Identity.Name);
            Console.WriteLine(res);
            return res;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Manager")]
        public string World(int value)
        {
            string res = string.Format("You entered: {0}", value);
            Console.WriteLine(res);
            return res;        }
    }

    public class MyCustomAuthPolicy :  IAuthorizationPolicy
    {
        private string id = Guid.NewGuid().ToString();

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
           GenericPrincipal gp = null;
           foreach(ClaimSet cls in evaluationContext.ClaimSets)
            {
                var names = cls.FindClaims(ClaimTypes.Name, Rights.PossessProperty).FirstOrDefault(c => c.Resource.ToString() == @"LONDON\Instructorx");
                if (names != null)
                {
                    GenericIdentity gi = new GenericIdentity("MyInstructor");
                    gp = new GenericPrincipal(gi, new string[] { "Manager" });
                    break;
                }
                
            }
            if (gp == null)
            {
                GenericIdentity gi = new GenericIdentity("MyStudent");
                gp = new GenericPrincipal(gi, new string[] { "Employee" });
            }
            evaluationContext.Properties["Principal"] = gp;
            return true;
        }

        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return id; }
        }
    }
}
