namespace Yupi.Protocol
{
    using System;

    public interface IRouter
    {
        #region Methods

        T GetComposer<T>();

        #endregion Methods
    }
}