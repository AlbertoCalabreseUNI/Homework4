using Homework4.DataObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Charts
{
    public class ContingentyTable
    {
        private IEnumerable<DataPoint> sample;
        private int cellWidth;
        private int cellHeight;

        private Pen pen;
        private Font font;
        private SolidBrush blackBrush;

        private int[,] matrix;
        public ContingentyTable(IEnumerable<DataPoint> sampleIn)
        {
            this.sample = sampleIn;

            //TEMPORARY SOLUTION TO CHANGE WITH DYNAMIC VALUES
            this.cellWidth = 50;
            this.cellHeight = 23;

            this.pen = new Pen(Color.Black);
            this.blackBrush = new SolidBrush(Color.Black);
            this.font = new Font("Arial", 8);

            this.matrix = new int[10,20];
            populateMatrix();
        }


        public void Draw(Graphics g)
        {
            //Creating edges of the table
            for (int i = 1; i < 10; i++)
            {
                String toWrite = "" + ((i * 10) + 1).ToString() + " - " + "" + ((i+1) * 10).ToString();

                g.DrawString(toWrite, this.font, this.blackBrush, i * this.cellWidth, 0);
                g.DrawRectangle(this.pen, new Rectangle(i * this.cellWidth, 0, this.cellWidth, this.cellHeight));
            }

            for (int i = 1; i < 20; i++)
            {
                String toWrite = "" + ((i * 10) + 1).ToString() + " - " + "" + ((i+1) * 10).ToString();

                g.DrawString(toWrite, this.font, this.blackBrush, 0, i * this.cellHeight);
                g.DrawRectangle(this.pen, new Rectangle(0,i*this.cellHeight, this.cellWidth, this.cellHeight));
            }

            for(int i = 1; i < 10; i++)
            {
                for(int j = 1; j < 20; j++)
                {
                    String toWrite = matrix[i,j].ToString();
                    g.DrawString(toWrite, this.font, this.blackBrush, i * this.cellWidth, j * this.cellHeight);
                    g.DrawRectangle(this.pen, new Rectangle(i * this.cellWidth, j * this.cellHeight, this.cellWidth, this.cellHeight));
                }
            }
            
            
        }

        public void populateMatrix()
        {
            foreach (DataPoint unit in this.sample)
                matrix[unit.x / 10, unit.y / 10] += 1;
        }
    }
}
