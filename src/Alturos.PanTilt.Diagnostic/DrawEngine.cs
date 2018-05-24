using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;

namespace Alturos.PanTilt.Diagnostic
{
    public class DrawEngine : IDisposable
    {
        private Bitmap _drawImage;
        private Bitmap _backgroundImage;
        private Bitmap _finalImage;
        private float _multiplier;
        private int _width;
        private int _originalWidth;
        private int _height;
        private int _originalHeight;

        public DrawEngine(int multiplier)
        {
            this._multiplier = multiplier;

            this._originalWidth = 180; //180 grad nach links und rechts
            this._originalHeight = 90; //90 grad nach oben und unten

            this._width = this._originalWidth * multiplier;
            this._height = this._originalHeight * multiplier;

            var imageWidth = this._width * 2;
            var imageHeight = this._height * 2;

            this._backgroundImage = new Bitmap(imageWidth,imageHeight);
            this._drawImage = new Bitmap(imageWidth, imageHeight);
            this._finalImage = new Bitmap(imageWidth, imageHeight);
            DrawRaster(5);
        }

        public void Dispose()
        {
            this._finalImage.Dispose();
            this._drawImage.Dispose();
            this._backgroundImage.Dispose();
        }

        public void Clear()
        {
            using (var graphics = Graphics.FromImage(this._drawImage))
            {
                graphics.Clear(Color.Transparent);
            }
        }

        public Bitmap GetImage()
        {
            using (var graphics = Graphics.FromImage(this._finalImage))
            {
                graphics.DrawImage(this._backgroundImage, 0, 0);
                graphics.DrawImage(this._drawImage, 0, 0);
            }

            return new Bitmap(this._finalImage);
        }
     
        public void DrawPtHeadLimits(PanTiltLimit panTiltLimit)
        {
            var leftUp = new PanTiltPosition(panTiltLimit.PanMin, panTiltLimit.TiltMin);
            var leftDown = new PanTiltPosition(panTiltLimit.PanMin, panTiltLimit.TiltMax);
            var rightUp = new PanTiltPosition(panTiltLimit.PanMax, panTiltLimit.TiltMin);
            var rightDown = new PanTiltPosition(panTiltLimit.PanMax, panTiltLimit.TiltMax);

            var positions = new PanTiltPosition[] { leftUp, rightUp, rightDown, leftDown };

            var textPos = positions.OrderBy(o => this.Distance(o, new PanTiltPosition(-180, 90))).FirstOrDefault();
            this.DrawText(textPos, "PT Unit Limits", Brushes.Crimson, false);
            this.DrawPolygon(positions, Color.Crimson);
        }

        private double Distance(PanTiltPosition position1, PanTiltPosition position2)
        {
            return Math.Pow(position1.Pan - position2.Pan, 2) + Math.Pow(position1.Tilt - position2.Tilt, 2);
        }

        public void DrawLine(object lastPosition, object currentPosition, object hotPink, int v)
        {
            throw new NotImplementedException();
        }

        public void DrawText(PanTiltPosition position, string text, Brush brush, bool centerText)
        {
            var point = this.ConvertPanTilt2Point(position);

            using (var graphics = Graphics.FromImage(this._drawImage))
            {
                graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                var font = new Font("Verdana", 4 * this._multiplier, FontStyle.Bold);

                float x, y;
                if (centerText)
                {
                    var textSize = graphics.MeasureString(text, font);
                    x = point.X - (textSize.Width / 2);
                    y = point.Y - (textSize.Height / 2);
                }
                else
                {
                    x = point.X;
                    y = point.Y;
                }

                graphics.DrawString(text, font, brush, x, y);
            }
        }
        
        private void DrawPolygon(PanTiltPosition[] positions, Color color)
        {
            var points = new List<PointF>();
            foreach (var position in positions)
            {
                var point = this.ConvertPanTilt2Point(position);
                points.Add(point);
            }

            if (points.Count == 0)
            {
                return;
            }

            using (var graphics = Graphics.FromImage(this._drawImage))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawPolygon(new Pen(color, 3), points.ToArray());
            }
        }

        public void DrawLine(PanTiltPosition pt1, PanTiltPosition pt2, Color color, int width)
        {
            var pen = new Pen(color, width);

            var point1 = this.ConvertPanTilt2Point(pt1);
            var point2 = this.ConvertPanTilt2Point(pt2);

            this.DrawLine(this._drawImage, point1, point2, pen);
        }

        private void DrawLine(Bitmap image, PointF p1, PointF p2, Pen pen)
        {
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawLine(pen, p1, p2);
            }
        }

        public void DrawLineWithArrow(PanTiltPosition pt1, PanTiltPosition pt2, Color color, int width)
        {
            var pen = new Pen(color, width);

            var point1 = this.ConvertPanTilt2Point(pt1);
            var point2 = this.ConvertPanTilt2Point(pt2);

            this.DrawLineWithArrow(this._drawImage, point1, point2, pen);
        }

        private void DrawLineWithArrow(Bitmap image, PointF p1, PointF p2, Pen pen)
        {
            // Draw line to screen.
            using (var graphics = Graphics.FromImage(image))
            {
                using (var capPath = new GraphicsPath())
                {
                    // A triangle
                    capPath.AddLine(-5, 0, 5, 0);
                    capPath.AddLine(-5, 0, 0, 5);
                    capPath.AddLine(0, 5, 5, 0);

                    pen.CustomEndCap = new CustomLineCap(null, capPath);
                }

                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawLine(pen, p1, p2);
            }
        }

        private void DrawRaster(int edge)
        {
            var customeEdge = edge * this._multiplier;

            var countHorizontal = (this._width * 2) / customeEdge;
            var countVertical = (this._height * 2) / customeEdge;

            using (var graphics = Graphics.FromImage(this._backgroundImage))
            {
                graphics.FillRectangle(Brushes.DarkGray, 0, 0, this._finalImage.Width, this._finalImage.Height);
                graphics.FillRectangle(Brushes.White, 0, 0, this._width * 2, this._height * 2);

                for (int x = 0; x < countHorizontal; x++)
                {
                    for (int y = 0; y < countVertical; y++)
                    {
                        var calculatedX =  x * customeEdge;
                        var calculatedY =  y * customeEdge;
                        graphics.DrawRectangle(Pens.LightGray, calculatedX, calculatedY, customeEdge, customeEdge);
                    }
                }

                var font = new Font("Verdana", 3 * this._multiplier, FontStyle.Bold);
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                var positiveWidthSize = graphics.MeasureString(this._originalWidth.ToString(), font);
                var negativeHeightSize = graphics.MeasureString((this._originalHeight * -1).ToString(), font);
                var positiveHeightSize = graphics.MeasureString(this._originalHeight.ToString(), font);

                var linePen = new Pen(Color.DarkGray, 2);
                var textColor = Brushes.Black;

                //Horizontal
                graphics.DrawLine(linePen, 0, this._height, this._width * 2, this._height);
                graphics.DrawString((-this._originalWidth).ToString(), font, textColor, 0,this._height);
            
                graphics.DrawString(this._originalWidth.ToString(), font, textColor,(this._width * 2) - positiveWidthSize.Width, this._height);

                //Vertical
                graphics.DrawLine(linePen, this._width, 0, this._width,this._height*2);
                graphics.DrawString((-this._originalHeight).ToString(), font, textColor, this._width - negativeHeightSize.Width, (this._height*2) - negativeHeightSize.Height);
                graphics.DrawString(this._originalHeight.ToString(), font, textColor,this._width - positiveHeightSize.Width,0);
            }
        }

        private PointF ConvertPanTilt2Point(PanTiltPosition item)
        {
            var x = (item.Pan + this._originalWidth) * this._multiplier;
            var y = -((item.Tilt - this._originalHeight)) * this._multiplier;

            return new PointF((float) x, (float) y);
        }

        public void DrawCrossHair(PanTiltPosition position, Brush brush)
        {
            var point = this.ConvertPanTilt2Point(position);
            var size1 = 7 * this._multiplier;
            var size2 = 4 * this._multiplier;
            var length = 5 * this._multiplier;

            using (var graphics = Graphics.FromImage(this._drawImage))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawEllipse(new Pen(brush, 3), point.X - (size1 / 2), point.Y - (size1 / 2), size1, size1);
                graphics.DrawEllipse(new Pen(brush, 2), point.X - (size2 / 2), point.Y - (size2 / 2), size2, size2);

                graphics.DrawLine(new Pen(brush, 2), point.X + 5, point.Y, point.X + length, point.Y);
                graphics.DrawLine(new Pen(brush, 2), point.X - 5, point.Y, point.X - length, point.Y);
                graphics.DrawLine(new Pen(brush, 2), point.X, point.Y + 5, point.X, point.Y + length);
                graphics.DrawLine(new Pen(brush, 2), point.X, point.Y - 5, point.X, point.Y - length);
            }
        }

        public void DrawCircle(PanTiltPosition centerPt, float dimension, Brush brush)
        {
            var point = this.ConvertPanTilt2Point(centerPt);
            using (var graphics = Graphics.FromImage(this._drawImage))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawEllipse(new Pen(brush, 3), point.X - (dimension / 2), point.Y - (dimension / 2), dimension, dimension);
            }
        }
    }
}
