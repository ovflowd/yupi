using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorFreezeTimer : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!item.GetRoom().CheckRights(session))
                return;

            int num = 0;

            if (!string.IsNullOrEmpty(item.ExtraData))
                num = int.Parse(item.ExtraData);

            if (request == 2)
            {
                if (item.PendingReset && num > 0)
                {
                    num = 0;
                    item.PendingReset = false;
                }
                else
                {
                    if (num == 0 || num == 30 || num == 60 || num == 120 || num == 180 || num == 300 || num == 600)
                    {
                        switch (num)
                        {
                            case 0:
                                num = 30;
                                break;

                            case 30:
                                num = 60;
                                break;

                            case 60:
                                num = 120;
                                break;

                            case 120:
                                num = 180;
                                break;

                            case 180:
                                num = 300;
                                break;

                            case 300:
                                num = 600;
                                break;

                            case 600:
                                num = 0;
                                break;
                        }
                    }
                    else
                        num = 0;

                    item.UpdateNeeded = false;
                }
            }
            else
            {
                if (request == 1 && !item.GetRoom().GetFreeze().GameStarted)
                {
                    item.UpdateNeeded = !item.UpdateNeeded;

                    if (item.UpdateNeeded)
                        item.GetRoom().GetFreeze().StartGame();

                    item.PendingReset = true;
                }
            }

            item.ExtraData = num.ToString();
            item.UpdateState();
        }
    }
}