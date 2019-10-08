using BattleshipGame.CoordinateStuff;

namespace BattleshipGame
{
    public class TransformationSettings
    {
        public int HorizontalOffset { get; set; }
        public int VerticalOfsset { get; set; }
        public Rotation Rotation { get; set; }
        public Reflection Reflection { get; set; }
        public bool RotateFirst { get; set; } = true;

        public TransformationSettings()
        {
            Rotation = Rotation.None;
            Reflection = Reflection.None;
        }

        public TransformationSettings(
            int horizontalOffset = 0,
            int verticalOffset = 0,
            Rotation rotation = Rotation.None,
            Reflection reflection = Reflection.None,
            bool rotateFirst = true)
        {
            HorizontalOffset = horizontalOffset;
            VerticalOfsset = verticalOffset;
            Rotation = rotation;
            Reflection = reflection;
            RotateFirst = rotateFirst;
        }

        public TransformationSettings(TransformationSettings transformation)
        {
            HorizontalOffset = transformation.HorizontalOffset;
            VerticalOfsset = transformation.VerticalOfsset;
            Rotation = transformation.Rotation;
            Reflection = transformation.Reflection;
            RotateFirst = transformation.RotateFirst;
        }
    }
}
