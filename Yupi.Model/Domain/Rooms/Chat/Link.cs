namespace Yupi.Model.Domain
{
    public class Link
    {
        public virtual int Id { get; protected set; }
        public virtual string URL { get; set; }
        public virtual string Text { get; set; }

        // Internal will open in window with id habboMain
        public virtual bool IsInternal { get; set; }
    }
}