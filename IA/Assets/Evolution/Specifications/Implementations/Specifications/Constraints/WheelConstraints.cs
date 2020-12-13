namespace Evolution.Specifications.Implementations.Specifications.Constraints
{
    public class WheelConstraints
    {
        public enum Wheels
        {
            BackWheel = 1,
            FrontWheel
        } 
        public static float MaxScale { get; } = 0.8f;
        public static float MinScale { get; } = 0.4f;
    }
}