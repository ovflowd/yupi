using System;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorBanzaiTimer : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            int result;

            if (!int.TryParse(item.ExtraData, out result))
            {
                item.ExtraData = "0";
                result = 0;
            }

            if (request == 0 && result == 0)
                result = 30;
            else
            {
                switch (request)
                {
                    case 2:
                        if (item.GetRoom().GetBanzai().IsBanzaiActive && item.PendingReset && result > 0)
                        {
                            result = 0;
                            item.PendingReset = false;
                        }
                        else
                        {
                            result = result >= 30
                                ? (result != 30
                                    ? (result != 60
                                        ? (result != 120 ? (result != 180 ? (result != 300 ? 0 : 600) : 300) : 180)
                                        : 120)
                                    : 60)
                                : 30;
                            item.UpdateNeeded = false;
                        }
                        break;

                    case 0:
                    case 1:
                        if (!item.GetRoom().GetBanzai().IsBanzaiActive)
                        {
                            item.UpdateNeeded = !item.UpdateNeeded;

                            if (item.UpdateNeeded)
                                item.GetRoom().GetBanzai().BanzaiStart();

                            item.PendingReset = true;
                        }
                        else
                        {
                            item.UpdateNeeded = !item.UpdateNeeded;

                            if (item.UpdateNeeded)
                                item.GetRoom().GetBanzai().BanzaiEnd();

                            item.PendingReset = true;
                        }
                        break;
                }
            }

            item.ExtraData = Convert.ToString(result);
            item.UpdateState();
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            if (!item.GetRoom().GetBanzai().IsBanzaiActive)
            {
                item.UpdateNeeded = !item.UpdateNeeded;

                if (item.UpdateNeeded)
                    item.GetRoom().GetBanzai().BanzaiStart();

                item.PendingReset = true;
            }

            item.UpdateNeeded = !item.UpdateNeeded;

            if (item.UpdateNeeded)
                item.GetRoom().GetBanzai().BanzaiEnd();

            item.PendingReset = true;
        }
    }
}