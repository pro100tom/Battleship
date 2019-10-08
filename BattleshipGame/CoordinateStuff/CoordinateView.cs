using BattleshipGame.BattlefieldStuff;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BattleshipGame.CoordinateStuff
{
    public class CoordinateViewAddedEventArgs : EventArgs
    {
        public Coordinate Coordinate { get; }
        public CoordinateView CoordinateView { get; }

        public Battlefield Battlefield { get; }
        public BattlefieldView BattlefieldView { get; }

        public CoordinateViewAddedEventArgs(Coordinate coordinate, CoordinateView coordinateView, 
            Battlefield battlefield, BattlefieldView battlefieldView)
        {
            Coordinate = coordinate;
            CoordinateView = coordinateView;
            Battlefield = battlefield;
            BattlefieldView = battlefieldView;
        }
    }

    public class CoordinateView : Border
    {
        public Coordinate Coordinate { get; }

        public CoordinateView()
        {
            Child = new TextBlock();
        }

        public CoordinateView(Coordinate coordinate) : this()
        {
            Coordinate = coordinate;
        }

        public new Style Style
        {
            get => base.Style;

            set
            {
                base.Style = value;
                (Child as TextBlock).Text = Text;
                (Child as TextBlock).HorizontalAlignment = TextHorizontalAlignment;
                (Child as TextBlock).VerticalAlignment = TextVerticalAlignment;
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);

            set
            {
                SetValue(TextBlock.TextProperty, value);
            }
        }

        public HorizontalAlignment TextHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty);

            set
            {
                SetValue(HorizontalAlignmentProperty, value);
            }
        }

        public VerticalAlignment TextVerticalAlignment
        {
            get => (VerticalAlignment)GetValue(TextVerticalAlignmentProperty);

            set
            {
                SetValue(TextVerticalAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(CoordinateView)
        );

        public static readonly DependencyProperty TextHorizontalAlignmentProperty = DependencyProperty.Register(
            nameof(TextHorizontalAlignment), typeof(HorizontalAlignment), typeof(CoordinateView)
        );

        public static readonly DependencyProperty TextVerticalAlignmentProperty = DependencyProperty.Register(
            nameof(TextVerticalAlignment), typeof(VerticalAlignment), typeof(CoordinateView)
        );
    }
}
