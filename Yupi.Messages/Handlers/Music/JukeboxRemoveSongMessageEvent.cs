// ---------------------------------------------------------------------------------
// <copyright file="JukeboxRemoveSongMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class JukeboxRemoveSongMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongItem> ItemRepository;

        #endregion Fields

        #region Constructors

        public JukeboxRemoveSongMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null)
                return;

            int itemId = message.GetInteger();

            SongMachineComponent songMachine = session.Room.Data.SongMachine;

            SongItem item = songMachine.Find(itemId);

            if (item == null)
            {
                return;
            }

            songMachine.Remove(item);
            session.Info.Inventory.FloorItems.Add(item);
            ItemRepository.Save(item);

            SongItem[] items = session.Info.Inventory.FloorItems.OfType<SongItem>().ToArray();

            router.GetComposer<UpdateInventoryMessageComposer>().Compose(session);
            router.GetComposer<SongsLibraryMessageComposer>().Compose(session, items);
            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }

        #endregion Methods
    }
}