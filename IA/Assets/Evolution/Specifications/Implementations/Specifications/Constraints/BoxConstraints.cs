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
        
        public static float StandardMass { get; } = 4f;
    }
}
