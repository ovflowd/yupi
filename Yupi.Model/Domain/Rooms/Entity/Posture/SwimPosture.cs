namespace Yupi.Model
{
    public class SwimPosture : EntityPosture
    {
        public static readonly SwimPosture Default = new SwimPosture();

        private SwimPosture()
        {
            // Don't allow external initialization (useless)
        }

        public override string ToStatusString()
        {
            return "swim";
        }
    }
}