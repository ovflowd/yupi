namespace Yupi.Model.Domain
{
    public class MoodlightPreset
    {
        public virtual int Id { get; protected set; }

        public virtual bool BackgroundOnly { get; set; }

        public virtual string ColorCode { get; set; }

        public virtual int ColorIntensity { get; set; }
    }
}