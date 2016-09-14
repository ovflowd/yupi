using System.Globalization;

namespace Yupi.Model
{
    public class WalkPosture : EntityPosture
    {
        public readonly Vector3 Target;

        public WalkPosture(Vector3 target)
        {
            Target = target;
        }

        public override string ToStatusString()
        {
            return "mv " +
                   string.Join(",", (int) Target.X, (int) Target.Y, Target.Z.ToString(CultureInfo.InvariantCulture));
        }
    }
}