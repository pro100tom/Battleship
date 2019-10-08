using BattleshipGame.CoordinateStuff;
using BattleshipGame.ShipStuff;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace BattleshipGame.BattlefieldStuff
{
    public enum MouseState { Away, Hover, Down }

    public class BattlefieldViewStyleFactory
    {
        public Style CreateBattlefieldViewStyle(Dimensions dimensions)
        {
            var style = new Style();
            style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(50.0d)));
            style.Setters.Add(new Setter(UniformGrid.ColumnsProperty, dimensions.Columns));
            style.Setters.Add(new Setter(UniformGrid.RowsProperty, dimensions.Rows));

            return style;
        }

        public Style CreateCoordinateViewStyle(
            MouseState mouseState, bool hideShips, Coordinate coordinate, ShipInfo shipInfo = null, bool highlight = false
        )
        {
            switch (mouseState)
            {
                case MouseState.Hover:
                    return CreateCoordinateViewMouseOverStyle(hideShips, coordinate, shipInfo, highlight);
                case MouseState.Down:
                    return CreateCoordinateViewMouseDownStyle(hideShips, coordinate, shipInfo, highlight);
                case MouseState.Away:
                default:
                    return CreateCoordinateViewMouseAwayStyle(hideShips, coordinate, shipInfo, highlight);
            }
        }

        private Style CreateCoordinateViewMouseAwayStyle(
            bool hideShips, Coordinate coordinate, ShipInfo shipInfo = null, bool highlight = false
        )
        {
            var style = CreateCoordinateViewBaseStyle();
            var text = "";
            if (coordinate.IsChecked)
                text = shipInfo != null ? "✕" : "⚪";

            style.Setters.Add(new Setter(CoordinateView.TextProperty, text));
            var color = GenerateCoordinateViewColor(coordinate.IsChecked, hideShips, shipInfo);
            style.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(color)));

            return style;
        }

        private Style CreateCoordinateViewMouseOverStyle(
            bool hideShips, Coordinate coordinate, ShipInfo shipInfo = null, bool highlight = false
        )
        {
            var style = CreateCoordinateViewBaseStyle(highlight);
            var text = GenerateCoordinateViewLabel(coordinate);
            if (coordinate.IsChecked)
                text = shipInfo != null ? "✕" : "⚪";

            style.Setters.Add(new Setter(CoordinateView.TextProperty, text));
            var baseColor = GenerateCoordinateViewColor(coordinate.IsChecked, hideShips, shipInfo);
            var color = CoordinateViewColorMapper.IdleMouseOverColorPairs[baseColor];
            style.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(color)));

            return style;
        }

        private Style CreateCoordinateViewMouseDownStyle(
            bool hideShips, Coordinate coordinate, ShipInfo shipInfo = null, bool highlight = false
        )
        {
            var style = CreateCoordinateViewBaseStyle(highlight);
            var text = hideShips && !coordinate.IsChecked ? "?" : GenerateCoordinateViewLabel(coordinate);
            style.Setters.Add(new Setter(CoordinateView.TextProperty, text));
            var baseColor = GenerateCoordinateViewColor(coordinate.IsChecked, hideShips, shipInfo);
            var color = CoordinateViewColorMapper.IdleMouseDownColorPairs[baseColor];
            style.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(color)));

            return style;
        }

        private Style CreateCoordinateViewBaseStyle(bool highlight = false)
        {
            var style = new Style();

            style.Setters.Add(new Setter(FrameworkElement.WidthProperty, 50.0d));
            style.Setters.Add(new Setter(FrameworkElement.HeightProperty, 50.0d));
            style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(-1.0d)));
            style.Setters.Add(new Setter(Control.BorderBrushProperty, new SolidColorBrush(highlight ? Colors.DarkBlue : Colors.Black)));
            style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(1.0d)));
            style.Setters.Add(new Setter(CoordinateView.TextHorizontalAlignmentProperty, HorizontalAlignment.Center));
            style.Setters.Add(new Setter(CoordinateView.TextVerticalAlignmentProperty, VerticalAlignment.Center));

            return style;
        }

        private Color GenerateCoordinateViewColor(bool isChecked, bool hideShips, ShipInfo shipInfo)
        {
            var colour = Colors.AliceBlue;
            if (shipInfo == null)
                return colour;

            if (isChecked && shipInfo.IsSunk)
                colour = Colors.DarkSlateGray;
            else if (isChecked && shipInfo.IsDamaged)
                colour = Colors.Orange;
            else if (!hideShips)
                colour = Colors.Green;

            return colour;
        }

        private string GenerateCoordinateViewLabel(Coordinate coordinate)
        {
            var letter = (char)(coordinate.Column + 65);
            var digit = coordinate.Row + 1;

            return string.Join("", letter.ToString(), digit.ToString());
        }
    }
}
