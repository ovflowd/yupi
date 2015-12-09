using System;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    /// Class GameClientMessageHandler.
    /// </summary>
    partial class GameClientMessageHandler
    {
        /// <summary>
        /// Calls the guide.
        /// </summary>
        internal void CallGuide()
        {
            Request.GetBool();

            var userId = Request.GetIntegerFromString();
            var message = Request.GetString();

            var guideManager = Yupi.GetGame().GetGuideManager();

            if (guideManager.GuidesCount <= 0)
            {
                Response.Init(LibraryParser.OutgoingRequest("OnGuideSessionError"));
                Response.AppendInteger(0);
                SendResponse();
                return;
            }

            var guide = guideManager.GetRandomGuide();

            if (guide == null)
            {
                Response.Init(LibraryParser.OutgoingRequest("OnGuideSessionError"));
                Response.AppendInteger(0);
                SendResponse();
                return;
            }

            var onGuideSessionAttached = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionAttachedMessageComposer"));
            onGuideSessionAttached.AppendBool(false);
            onGuideSessionAttached.AppendInteger(userId);
            onGuideSessionAttached.AppendString(message);
            onGuideSessionAttached.AppendInteger(30);
            Session.SendMessage(onGuideSessionAttached);

            var onGuideSessionAttached2 = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionAttachedMessageComposer"));
            onGuideSessionAttached2.AppendBool(true);
            onGuideSessionAttached2.AppendInteger(userId);
            onGuideSessionAttached2.AppendString(message);
            onGuideSessionAttached2.AppendInteger(15);
            guide.SendMessage(onGuideSessionAttached2);
            guide.GetHabbo().GuideOtherUser = Session;
            Session.GetHabbo().GuideOtherUser = guide;
        }

        /// <summary>
        /// Answers the guide request.
        /// </summary>
        internal void AnswerGuideRequest()
        {
            var state = Request.GetBool();

            if (!state)
                return;

            var requester = Session.GetHabbo().GuideOtherUser;
            var message = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionStartedMessageComposer"));

            message.AppendInteger(requester.GetHabbo().Id);
            message.AppendString(requester.GetHabbo().UserName);
            message.AppendString(requester.GetHabbo().Look);
            message.AppendInteger(Session.GetHabbo().Id);
            message.AppendString(Session.GetHabbo().UserName);
            message.AppendString(Session.GetHabbo().Look);
            requester.SendMessage(message);
            Session.SendMessage(message);
        }

        /// <summary>
        /// Opens the guide tool.
        /// </summary>
        internal void OpenGuideTool()
        {
            var guideManager = Yupi.GetGame().GetGuideManager();
            var onDuty = Request.GetBool();

            Request.GetBool();
            Request.GetBool();
            Request.GetBool();

            if (onDuty)
                guideManager.AddGuide(Session);
            else
                guideManager.RemoveGuide(Session);

            Session.GetHabbo().OnDuty = onDuty;
            Response.Init(LibraryParser.OutgoingRequest("HelperToolConfigurationMessageComposer"));
            Response.AppendBool(onDuty);
            Response.AppendInteger(guideManager.GuidesCount);
            Response.AppendInteger(guideManager.HelpersCount);
            Response.AppendInteger(guideManager.GuardiansCount);
            SendResponse();
        }

        /// <summary>
        /// Invites to room.
        /// </summary>
        internal void InviteToRoom()
        {
            var requester = Session.GetHabbo().GuideOtherUser;

            var room = Session.GetHabbo().CurrentRoom;

            var message = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionInvitedToGuideRoomMessageComposer"));

            if (room == null)
            {
                message.AppendInteger(0);
                message.AppendString(string.Empty);
            }
            else
            {
                message.AppendInteger(room.RoomId);
                message.AppendString(room.RoomData.Name);
            }

            requester.SendMessage(message);
            Session.SendMessage(message);
        }

        /// <summary>
        /// Visits the room.
        /// </summary>
        internal void VisitRoom()
        {
            if (Session.GetHabbo().GuideOtherUser == null)
                return;

            var requester = Session.GetHabbo().GuideOtherUser;
            var visitRoom = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            visitRoom.AppendInteger(requester.GetHabbo().CurrentRoomId);
            Session.SendMessage(visitRoom);
        }

        /// <summary>
        /// Guides the speak.
        /// </summary>
        internal void GuideSpeak()
        {
            var message = Request.GetString();
            var requester = Session.GetHabbo().GuideOtherUser;
            var messageC = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionMsgMessageComposer"));
            messageC.AppendString(message);
            messageC.AppendInteger(Session.GetHabbo().Id);
            requester.SendMessage(messageC);
            Session.SendMessage(messageC);
        }

        /// <summary>
        /// BETA
        /// Closes the guide request.
        /// </summary>
        internal void CloseGuideRequest()
        {
            //Request.GetBool();

            var requester = Session.GetHabbo().GuideOtherUser;
            var message = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));

            /* guide - close session  */
            message.AppendInteger(2);
            requester.SendMessage(message);

            /* user - close session */
            var message2 = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));
            message.AppendInteger(0);
            Session.SendMessage(message2);

            /* user - detach session */
            var message3 = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));
            Session.SendMessage(message3);

            /* guide - detach session */
            var message4 = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));
            requester.SendMessage(message4);

            Console.WriteLine("The Close was Called");

            requester.GetHabbo().GuideOtherUser = null;
            Session.GetHabbo().GuideOtherUser = null;
        }

        /// <summary>
        /// Cancels the call guide.
        /// BETA
        /// </summary>
        internal void CancelCallGuide()
        {
            //Response.Init(3485);

            /* user - cancell session */
            var message = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));
            message.AppendInteger(2);
            Session.SendMessage(message);

            Console.WriteLine("The Cancell was Called");
        }

        /// <summary>
        /// Guides the feedback.
        /// </summary>
        internal void GuideFeedback()
        {
            var message = new ServerMessage(LibraryParser.OutgoingRequest("OnGuideSessionDetachedMessageComposer"));

            Session.SendMessage(message);

            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_GuideFeedbackGiver", 1);
        }

        /// <summary>
        /// Ambassadors the alert.
        /// </summary>
        internal void AmbassadorAlert()
        {
            if (Session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                return;

            uint userId = Request.GetUInteger();

            GameClient user = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            user?.SendNotif("${notification.ambassador.alert.warning.message}", "${notification.ambassador.alert.warning.title}");
        }
    }
}