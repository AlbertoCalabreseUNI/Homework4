using Homework4.CustomForm;
using Homework4.DataObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Charts
{
    public class Scatterplot
    {
        private IEnumerable<DataPoint> sample;

        private SolidBrush blueColor;
        private SolidBrush redColor;
        private SolidBrush greenColor;
        private SolidBrush backgroundColor;
        private SolidBrush textColor;
        private Pen pen;

        public int SP_START_X { get; }
        public int SP_START_Y { get; }
        public int SP_WIDTH { get; }
        public int SP_HEIGHT { get; }

        private int gapY;
        private int maxValueGapY;
        private int minValueGapY;
        private int gapX;
        private int maxValueGapX;
        private int minValueGapX;
        private int gapLineLength;

        private int scaleDistanceX;
        private int scaleDistanceY;

        private int sphereSize;

        private Font font;
        private int textWidth;
        private int textHeight;

        private int[] weightGroup;
        private int[] heightGroup;

        //TODO FOR FUTURE: Making all the sub-variables being scaled with the scatterplot size
        public Scatterplot(IEnumerable<DataPoint> sampleIn)
        {
            this.sample = sampleIn;

            this.SP_START_X = 40;
            this.SP_START_Y = 200;
            this.SP_WIDTH = 200;
            this.SP_HEIGHT = 200;

            this.gapY = 10;
            this.minValueGapY = 0;
            this.maxValueGapY = 200;
            this.gapX = 10;
            this.minValueGapX = 0;
            this.maxValueGapX = 100;
            this.gapLineLength = 5;

            this.scaleDistanceY = this.SP_HEIGHT / this.maxValueGapY;
            this.scaleDistanceX = this.SP_WIDTH / this.maxValueGapX;

            this.font = new Font("Arial", 8);
            this.textColor = new SolidBrush(Color.Black);
            this.textWidth = 15;
            this.textHeight = 5;

            this.sphereSize = 6;

            this.blueColor = new SolidBrush(Color.Blue);
            this.redColor = new SolidBrush(Color.Red);
            this.greenColor = new SolidBrush(Color.Green);
            this.backgroundColor = new SolidBrush(Color.White);
            this.pen = new Pen(Color.Black);

            //TODO: Make this dynamic
            this.weightGroup = new int[10];
            this.heightGroup = new int[20];

            populateArrays();
        }

        public void Draw(Graphics g)
        {
            if (g == null) return;
            g.FillRectangle(this.backgroundColor, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH + (int)pen.Width, SP_HEIGHT + (int)pen.Width));
            g.DrawRectangle(this.pen, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH + (int)pen.Width, SP_HEIGHT + (int)pen.Width));

            //Let's start drawing the markers for the Y axis
            for(int i = minValueGapY; i <= maxValueGapY; i += this.gapY)
            {
                if(i != maxValueGapY)
                    g.DrawLine(this.pen, new Point(this.SP_START_X - this.gapLineLength, this.SP_START_Y + i), new Point(this.SP_START_X + this.gapLineLength, this.SP_START_Y + i));
                //This else is needed to fix a simple visualization "bug" that occurs since the pen has a width of 1 pixel
                else
                    g.DrawLine(this.pen, new Point(this.SP_START_X - this.gapLineLength, this.SP_START_Y + i + 1), new Point(this.SP_START_X + this.gapLineLength, this.SP_START_Y + i + 1));
            }
            //Let's draw the markers for the X axis
            for (int i = minValueGapX; i <= maxValueGapX; i += this.gapX)
            {
                if(i != maxValueGapX)
                    g.DrawLine(this.pen, new Point(this.SP_START_X + i * this.scaleDistanceX, this.SP_START_Y + this.SP_HEIGHT - this.gapLineLength), new Point(this.SP_START_X + i * this.scaleDistanceX, this.SP_START_Y + this.SP_HEIGHT + this.gapLineLength + 1));
                else
                    g.DrawLine(this.pen, new Point(this.SP_START_X + i * this.scaleDistanceX + 1, this.SP_START_Y + this.SP_HEIGHT - this.gapLineLength), new Point(this.SP_START_X + i * this.scaleDistanceX + 1, this.SP_START_Y + this.SP_HEIGHT + this.gapLineLength + 1));
            }

            //Drawing markers' numbers for Y axis
            for (int i = minValueGapY; i <= maxValueGapY; i += this.gapY)
            {
                Point temp = relativePointPosition(-this.textWidth, i + this.gapLineLength);
                g.DrawString(i.ToString(), this.font, this.textColor, new PointF(temp.X, temp.Y));
            }

            //Drawing markers' numbers for X axis
            for (int i = minValueGapX; i <= maxValueGapX; i += this.gapX)
            {
                Point temp = relativePointPosition(i - this.gapLineLength, -this.textWidth);
                g.DrawString(i.ToString(), this.font, this.textColor, new PointF(temp.X, temp.Y));
            }

            //Visualizing the elements in the scatterplot
            foreach (DataPoint point in this.sample)
            {
                Point test = relativePointPosition(point.x, point.y);
                g.FillEllipse(this.blueColor,test.X, test.Y, this.sphereSize, this.sphereSize);
            }
        }

        public void DrawHistogram(Graphics g)
        {
            //X Axis Histogram Vertical
            for(int i = 0; i < this.maxValueGapX / this.gapX; i++)
            {
                int height = this.weightGroup[i] * (this.SP_HEIGHT / this.gapY);
                g.FillRectangle(this.redColor, this.SP_START_X + i * this.scaleDistanceX * 10, this.SP_START_Y - height, this.SP_WIDTH / this.gapX, height);
                g.DrawRectangle(this.pen, this.SP_START_X + i * this.scaleDistanceX * 10, this.SP_START_Y - height, this.SP_WIDTH/this.gapX, height);
            }

            int j = 0;
            //Y Axis Histogram Horizontal
            for(int i = this.maxValueGapY / this.gapY; i > 0 / this.gapY; i--)
            {
                int width = this.heightGroup[i-1] * (this.SP_WIDTH / this.gapX);
                g.FillRectangle(this.redColor, this.SP_START_X + this.SP_WIDTH + this.pen.Width, this.SP_HEIGHT + this.gapY * j, width, this.gapY);
                g.DrawRectangle(this.pen,this.SP_START_X + this.SP_WIDTH + this.pen.Width,this.SP_HEIGHT + this.gapY * j, width,this.gapY);
                j++;
            }
        }

        public Point relativePointPosition(int x, int y)
        {
            int newX = this.SP_START_X + (x * this.scaleDistanceX);
            int newY = (this.SP_START_Y + this.SP_HEIGHT) - (y * this.scaleDistanceY);
            return new Point(newX, newY);
        }

        public void populateArrays()
        {
            foreach (DataPoint unit in sample)
            {
                weightGroup[unit.x / 10] += 1;
                heightGroup[unit.y / 10] += 1;
            }
        }
    }
}
