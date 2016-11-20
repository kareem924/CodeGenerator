

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using NTEC.Classes.Security;

namespace NTEC.TicketWebSetup
{
    public class ClientMessageInspector : IClientMessageInspector
    {


        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
        /// <summary>
        /// The class is used to add data from the client before calling the WCF Method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            AddTicketHeader(ref request);
            return null;
        }
        /// <summary>
        /// Will Add The Ticket Header From The Current Logged In user
        /// </summary>
        /// <param name="request"></param>
        /// <remarks></remarks>
        private void AddTicketHeader(ref System.ServiceModel.Channels.Message request)
        {
            var curUser = SessionManager.CurrentUser;
            //if(curUser!=null && curUser.iLeadUserTicket!=null)
            {
                //iLeadCommon.SharedModel.Authentication.UserTicket tick = new iLeadCommon.SharedModel.Authentication.UserTicket();
                //tick.TicketNo=curUser.iLeadUserTicket.TicketNo;
                //tick.UserId=curUser.iLeadUserTicket.UserId;
                //tick.LogEvents=curUser.iLeadUserTicket.LogEvents;
                //tick.RepositoryId=curUser.iLeadUserTicket.RepositoryId;
                //tick.SOUT=curUser.iLeadUserTicket.SOUT;
                
                //MessageHeader<iLeadCommon.SharedModel.Authentication.UserTicket> ticketHeader = new MessageHeader<iLeadCommon.SharedModel.Authentication.UserTicket>(tick);
                //MessageHeader unTypedTicketHeader = ticketHeader.GetUntypedHeader(WCFVariables.TicketHeaderName.ToString(), WCFVariables.TicketHeaderNameSpace.ToString());
                //request.Headers.Add(unTypedTicketHeader);
            }
            //if (SessionManager.CurrentUser != null && SessionManager.CurrentIDOXUser.iLeadUserTicket != null)
            //{
              
            //}
        }
    }
}