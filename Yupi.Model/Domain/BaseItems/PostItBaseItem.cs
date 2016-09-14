using System;

namespace Yupi.Model.Domain
{
    public class PostItBaseItem : WallBaseItem
    {
        public override Item CreateNew()
        {
            return new PostItItem()
            {
                BaseItem = this
            };
        }
    }
}