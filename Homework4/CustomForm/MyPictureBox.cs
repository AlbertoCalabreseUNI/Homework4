using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Homework4.CustomForm
{
	public class MyPictureBox : PictureBox
	{
		Point point;
		private bool MouseIsInLeftEdge;
		private bool MouseIsInRightEdge;
		private bool MouseIsInTopEdge;
		private bool MouseIsInBottomEdge;

		private int currentHeight;
		private int currentWidth;

		public MyPictureBox(IContainer container)
		{
			container.Add(this);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			point = e.Location;
			MouseIsInLeftEdge = Math.Abs(point.X) <= 10;
			MouseIsInRightEdge = Math.Abs(point.X - this.Width) <= 10;
			MouseIsInTopEdge = Math.Abs(point.Y) <= 10;
			MouseIsInBottomEdge = Math.Abs(point.Y - this.Height) <= 10;

			currentHeight = this.Height;
			currentWidth = this.Width;

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (MouseIsInLeftEdge)
				{
					if (MouseIsInTopEdge)
					{
						this.Width -= (e.X - point.X);
						this.Left += (e.X - point.X);
						this.Height -= (e.Y - point.Y);
						this.Top += (e.Y - point.Y);
					}
					else if (MouseIsInBottomEdge)
					{
						this.Width -= (e.X - point.X);
						this.Left += (e.X - point.X);
						this.Height = (e.Y - point.Y) + this.currentHeight;
					}
					else
					{
						this.Width -= (e.X - point.X);
						this.Left += (e.X - point.X);
					}
				}
				else if (MouseIsInRightEdge)
				{
					if (MouseIsInTopEdge)
					{
						this.Width = (e.X - point.X) + this.currentWidth;
						this.Height -= (e.Y - point.Y);
						this.Top += (e.Y - point.Y);

					}
					else if (MouseIsInBottomEdge)
					{
						this.Width = (e.X - point.X) + this.currentWidth;
						this.Height = (e.Y - point.Y) + this.currentHeight;
					}
					else
					{
						this.Width = (e.X - point.X) + this.currentWidth;
					}
				}
				else if (MouseIsInTopEdge)
				{
					this.Height -= (e.Y - point.Y);
					this.Top += (e.Y - point.Y);
				}
				else if (MouseIsInBottomEdge)
				{
					this.Height = (e.Y - point.Y) + this.currentHeight;
				}
				else
				{
					this.Left += e.X - point.X;
					this.Top += e.Y - point.Y;
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
	}
}
