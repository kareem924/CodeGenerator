using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace NTEC.TicketWebSetup
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomClientBehavior : Attribute, IEndpointBehavior
    {


        public void AddBindingParameters(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            ApplyInspectorsToRuntime(ref clientRuntime);
        }

        public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            AddInspectorToEndPoint(ref endpointDispatcher);
        }


        public void Validate(System.ServiceModel.Description.ServiceEndpoint endpoint)
        {
        }
        #region "Inspectors"
        /// <summary>
        /// Will Add The Inspectors to The client endpoint
        /// </summary>
        /// <param name="endpointDispatcher"></param>
        /// <remarks></remarks>
        private void AddInspectorToEndPoint(ref System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            //ChannelDispatcher cDisp = endpointDispatcher.ChannelDispatcher;
            //if (cDisp != null)
            //{
            //    foreach (var ed in cDisp.Endpoints)
            //    {
            //        ed.DispatchRuntime.MessageInspectors.Add(new ClientMessageInspector());
            //    }
            //}
        }
        /// <summary>
        /// Will Apply The Insepctors to the client runtime
        /// </summary>
        /// <param name="clientRuntime"></param>
        /// <remarks></remarks>
        private void ApplyInspectorsToRuntime(ref ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ClientMessageInspector());
        }
        #endregion

    }
}