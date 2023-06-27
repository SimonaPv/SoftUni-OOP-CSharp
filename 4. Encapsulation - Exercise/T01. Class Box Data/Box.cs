using System;
using System.Collections.Generic;
using System.Text;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get { return length; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(this.Length)} cannot be zero or negative.");
                }
                length = value;
            }
        }
        public double Width
        {
            get { return width; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(this.Width)} cannot be zero or negative.");
                }
                width = value;
            }
        }
        public double Height
        {
            get { return height; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(this.Height)} cannot be zero or negative.");
                }
                height = value;
            }
        }

        private double SurfaceArea()
        {
            return 2 * (this.Length * this.Width) + 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
        }
        private double LateralSurfaceArea()
        {
            return 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
        }
        private double Volume()
        {
            return this.Length * this.Width * this.Height;
        }

        public override string ToString()
        {
            return $"Surface Area - {this.SurfaceArea():f2}{Environment.NewLine}" +
                $"Lateral Surface Area - {this.LateralSurfaceArea():f2}{Environment.NewLine}" +
                $"Volume - {Volume():f2}"
                .TrimEnd();
        }
    }
}
