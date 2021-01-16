namespace Evolution.Specifications.Implementations.Specifications.Constraints
{
    public class CarBodyConstraints
    {
        public static float ScaleXmax { get; } = 0.24f;
        public static float ScaleXmin { get; } = 0.12f;
        public static float ScaleYmax { get; } = 0.2f;
        public static float ScaleYmin { get; } = 0.12f;
        
        
        public static float MaxMotorSpeed { get; } = 1000f;
        public static float MaxMotorTorque { get; } = 800f;
        
        public static float MinMotorSpeed { get; } = 100f;
        public static float MinMotorTorque { get; } = 100f;

        public static float StandardMass { get; } = 5f;
    }
}