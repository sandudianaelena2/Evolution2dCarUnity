namespace Evolution.Specifications.Implementations.Specifications.Constraints
{
    class BoxConstraints
    {
        public enum Boxes
        {
            LeftBox = 3,
            RightBox
        }
        public static float MaxScale { get; } = 1.6f;
        public static float MinScale { get; } = 0.6f;

        public static float MaxMass { get; } = 0.16f;
        public static float MinMass { get; } = 0.06f;

        public static float StandardMass { get; } = 0.1f;
    }
}
