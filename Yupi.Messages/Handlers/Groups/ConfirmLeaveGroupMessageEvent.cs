using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class ConfirmLeaveGroupMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public ConfirmLeaveGroupMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var userId = request.GetInteger();

            var group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            if (group.Creator.Id == userId)
                return;
            throw new NotImplementedException();
        }
    }
}