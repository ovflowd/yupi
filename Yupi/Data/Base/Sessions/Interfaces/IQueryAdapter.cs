#region

using System;

#endregion

namespace Yupi.Data.Base.Sessions.Interfaces
{
    public interface IQueryAdapter : IRegularQueryAdapter, IDisposable
    {
        void DoCommit();

        void DoRollBack();

        long InsertQuery();

        void RunQuery();
    }
}