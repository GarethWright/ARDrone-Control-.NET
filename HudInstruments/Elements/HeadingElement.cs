﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ARDrone.Hud.Elements
{
    public class HeadingElement : LineBasedElement
    {
        public HeadingElement(HudConstants constants)
            : base(constants)
        { }

        protected override void GetBaseVariables(Bitmap bitmap, HudState currentState)
        {
            currentValue = currentState.Yaw;
        }

        protected override void DrawArrow(Graphics graphics)
        {
            int startPositionY = lineLength + GetTextSize(graphics).Height + 6;

            Point point1 = new Point(currentWidth / 2, 25);
            Point point2 = new Point(point1.X - 5, point1.Y + 10);
            Point point3 = new Point(point1.X + 5, point1.Y + 10);

            graphics.DrawLine(hudPen, point1, point2);
            graphics.DrawLine(hudPen, point1, point3);
        }

        protected override void DrawIndicatorText(Graphics graphics, double value, double relativePosition)
        {
            if (value < 0.0)
                value = 360 + value;

            int markerPositionX = GetMarkerPosition(relativePosition);
            int startPositionY = lineLength + 4;

            String directionText = String.Format("{0:000}", value);

            SizeF size = graphics.MeasureString(directionText, hudFont);
            Point fontPoint = new Point(markerPositionX - (int)size.Width / 2, startPositionY);

            graphics.DrawString(directionText, hudFont, hudBrush, fontPoint);
        }

        protected override void DrawIndicatorLine(Graphics graphics, double relativePosition)
        {
            int markerPositionX = GetMarkerPosition(relativePosition);
            int startPositionY = 2;

            Point point1 = new Point(markerPositionX, startPositionY);
            Point point2 = new Point(markerPositionX, startPositionY + lineLength);
            graphics.DrawLine(hudPen, point1, point2);
        }

        private int GetMarkerPosition(double relativePosition)
        {
            return currentWidth / 2 + (int)Math.Round(relativePosition * currentWidth * 0.75);
        }

        private Size GetTextSize(Graphics graphics)
        {
            SizeF size = graphics.MeasureString(String.Format("{0:000}", 0), hudFont);
            return new Size((int)size.Width, (int)size.Height);
        }

        protected override double ValueRange
        {
            get { return 50.0; }
        }

        protected override double MarkerDistance
        {
            get { return 10.0; }
        }

        protected override int CountBetweenNamedMarkers
        {
            get { return 1; }
        }
    }
}
