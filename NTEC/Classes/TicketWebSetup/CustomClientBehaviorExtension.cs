using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace NTEC.TicketWebSetup
{
    public class CustomClientBehaviorExtension : BehaviorExtensionElement
    {
        public override System.Type BehaviorType
        {
            get { return typeof(CustomClientBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new CustomClientBehavior();
        }
    }
}