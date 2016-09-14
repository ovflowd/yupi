// ---------------------------------------------------------------------------------
// <copyright file="JukeboxAddPlaylistItemMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Music
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class JukeboxAddPlaylistItemMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongItem> ItemRepository;

        #endregion Fields

        #region Constructors

        public JukeboxAddPlaylistItemMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null
                || !session.Room.Data.HasOwnerRights(session.Info))
            {
                return;
            }

            int itemId = message.GetInteger();

            SongItem item = session.Info.Inventory.GetFloorItem(itemId) as SongItem;

            if (item == null)
                return;

            SongMachineComponent songMachine = session.Room.Data.SongMachine;

            if (songMachine.TryAdd(item))
            {
                session.Info.Inventory.FloorItems.Remove(item);
                router.GetComposer<RemoveInventoryObjectMessageComposer>().Compose(session, item.Id);

                ItemRepository.Save(item);
            }

            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }

        #endregion Methods
    }
}