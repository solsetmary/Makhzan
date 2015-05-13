using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    class speechBalloon
    {

        /// <summary>
	/// This event is fired when the speech balloon requires redrawing
	/// </summary>
	/// <remarks></remarks>
	public event EventHandler RedrawRequired;

	#region "Properties"

	#region "BorderColor"
	private Color MyBorderColor = Color.Black;
	/// <summary>
	/// The color of the border surrounding the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public Color BorderColor {
		get { return MyBorderColor; }
		set {
			bool changed = value != MyBorderColor;

			MyBorderColor = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "BorderWidth"
	private float MyBorderWidth = 2.0f;
	/// <summary>
	/// The width of the border surrounding the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public float BorderWidth {
		get { return MyBorderWidth; }
		set {
			bool changed = MyBorderWidth != value;

			MyBorderWidth = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "BorderVisible"
	private bool MyBorderVisible = true;
	/// <summary>
	/// Determines whether the border is drawn around the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public bool BorderVisible {
		get { return MyBorderVisible; }
		set {
			bool changed = MyBorderVisible != value;

			MyBorderVisible = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "Bounds"
	private Rectangle MyBounds = new Rectangle(0, 0, 200, 100);
	/// <summary>
	/// The bounding rectangle of the balloon, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public Rectangle Bounds {
		get { return MyBounds; }
		set {
			bool changed = value != MyBounds;

			MyBounds = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "BubbleSize"
	private int MyBubbleSize = 0;
	/// <summary>
	/// The size (depth) of the bubbles/bulges around the perimeter of the balloon. Specify 0 for a basic ellipse
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int BubbleSize {
		get { return MyBubbleSize; }
		set {
			bool changed = MyBubbleSize != value;

			MyBubbleSize = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "BubbleSmootheness"
	private float MyBubbleSmoothness = 0.0f;
	/// <summary>
	/// The smoothness of the individual bulges/bubbles around the perimeter of the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public float BubbleSmoothness {
		get { return MyBubbleSmoothness; }
		set {
			bool changed = MyBubbleSmoothness != value;

			MyBubbleSmoothness = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "BubbleWidth"
	private int MyBubbleWidth = 20;
	/// <summary>
	/// The width of each bulge/bubble around the perimeter of the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int BubbleWidth {
		get { return MyBubbleWidth; }
		set {
			bool changed = MyBubbleWidth != value;

			MyBubbleWidth = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "FillColor"
	private Color MyFillColor = Color.White;
	/// <summary>
	/// The fill (background) color of the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public Color FillColor {
		get { return MyFillColor; }
		set {
			bool changed = value != MyFillColor;

			MyFillColor = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "Font"
	private Font MyFont = new Font("Comic Sans MS", 9.0f);
	/// <summary>
	/// The font for the text in the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public Font Font {
		get { return MyFont; }
		set {
			bool changed = !object.ReferenceEquals(value, MyFont);

			MyFont = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "Height"
	/// <summary>
	/// Gets or sets the height of the balloon, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int Height {
		get { return MyBounds.Height; }
		set {
			bool changed = value != MyBounds.Height;

			MyBounds.Height = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "Left"
	/// <summary>
	/// Gets or sets the Left/X co-ordinate of the balloon, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int Left {
		get { return MyBounds.X; }
		set {
			bool changed = MyBounds.X != value;

			MyBounds.X = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "Path"
	private GraphicsPath MyPath = new GraphicsPath();
	/// <summary>
	/// The GraphicsPath that defines the bubble, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public GraphicsPath Path {
		get { return MyPath; }
	}
	#endregion

	#region "RectangleCornerRadius"
	private int MyRectangleCornerRadius = 0;
	public int RectangleCornerRadius {
		get { return MyRectangleCornerRadius; }
		set {
			bool changed = value != MyRectangleCornerRadius;

			MyRectangleCornerRadius = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "Shape"
	private BalloonShape MyShape = BalloonShape.Ellipse;
	/// <summary>
	/// The overall shape of the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public BalloonShape Shape {
		get { return MyShape; }
		set {
			bool changed = MyShape != value;

			MyShape = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "TailBaseWidth"
	private int MyTailBaseWidth = 25;
	/// <summary>
	/// The width of the tail at its base
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int TailBaseWidth {
		get { return MyTailBaseWidth; }
		set {
			bool changed = MyTailBaseWidth != value;

			MyTailBaseWidth = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "TailLength"
	private int MyTailLength = 75;
	/// <summary>
	/// The length of the tail, as measured from the top of the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int TailLength {
		get { return MyTailLength; }
		set {
			bool changed = MyTailLength != value;

			MyTailLength = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "TailRotation"
	private float MyTailRotation = 115.0f;
	/// <summary>
	/// The location (in degrees) of the tail around the bubble
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public float TailRotation {
		get { return MyTailRotation; }
		set {
			bool changed = MyTailRotation != value;

			MyTailRotation = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "TailVisible"
	private bool MyTailVisible = true;
	/// <summary>
	/// Determines whether or not the tail is drawn
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public bool TailVisible {
		get { return MyTailVisible; }
		set {
			bool changed = MyTailVisible != value;

			MyTailVisible = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "Text"
	private string MyText = "TEXT GOES HERE!!!";
	/// <summary>
	/// The text in the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public string Text {
		get { return MyText; }
		set {
			bool changed = MyText != value;

			MyText = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "TextColor"
	private Color MyTextColor = Color.Red;
	/// <summary>
	/// The color of the text in the balloon
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public Color TextColor {
		get { return MyTextColor; }
		set {
			bool changed = value != MyTextColor;

			MyTextColor = value;

			if (changed)
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
		}
	}
	#endregion

	#region "Top"
	/// <summary>
	/// Gets or sets the Top/Y co-ordinate of the balloon, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int Top {
		get { return MyBounds.Top; }
		set {
			bool changed = MyBounds.Y != value;

			MyBounds.Y = value;

			if (changed) {
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#region "Width"
	/// <summary>
	/// Gets or sets the width of the balloon, excluding the tail
	/// </summary>
	/// <value></value>
	/// <returns></returns>
	/// <remarks></remarks>
	public int Width {
		get { return MyBounds.Width; }
		set {
			bool changed = value != MyBounds.Width;

			MyBounds.Width = value;

			if (changed) {
				RecreatePath();
				if (RedrawRequired != null) {
					RedrawRequired(this, EventArgs.Empty);
				}
			}
		}
	}
	#endregion

	#endregion

	#region "Constructor"
	public speechBalloon()
	{
		//Create the balloon path according to the defaults
		RecreatePath();
	}
	#endregion

	#region "RecreatePath"
	/// <summary>
	/// Creates the GraphicsPath for the balloon
	/// </summary>
	/// <remarks></remarks>
	private void RecreatePath()
	{
		//NOTE: To make creating the path easier, I assume the origin is (0, 0)
		//when adding the points to the GraphicsPath. When it comes time to 
		//actually draw the balloon, I'll call TranslateTransform on the
		//Graphics object to shift the origin to the actual location as
		//determined in the Bounds property.

		//Empty the path:
		Path.Reset();

		//If the BubbleSize is 0, we'll just create an ellipse:
		switch (Shape) {
			case BalloonShape.Ellipse:
				if (BubbleSize == 0) {
					Path.AddEllipse(0, 0, Width, Height);
				} else {
					int theta = 0;

					//Do an angle sweep around the circle moving the BubbleWidth in each iteration:
					for (theta = 0; theta <= (360 - BubbleWidth); theta += +BubbleWidth) {
						Point[] points = new Point[3];

						float radianTheta = (float)(theta * Math.PI / 180);
						float radianTheta2 = (float)((theta + (BubbleWidth / 2)) * Math.PI / 180);
						float radianTheta3 = (float)((theta + BubbleWidth) * Math.PI / 180);

						float x = (float)((Width / 2) + (Width / 2) * Math.Cos(radianTheta));
						float y = (float)((Height / 2) + (Height / 2) * Math.Sin(radianTheta));

						int xdelta = 0;
						int ydelta = 0;

						if (Math.Cos(radianTheta2) < 0 && Math.Sin(radianTheta2) < 0) {
							//Top-left:
							xdelta = -BubbleSize;
							ydelta = -BubbleSize;
						} else if (Math.Cos(radianTheta2) > 0 && Math.Sin(radianTheta2) > 0) {
							//Bottom-Right, Good
							xdelta = BubbleSize;
							ydelta = BubbleSize;
						} else if (Math.Cos(radianTheta2) < 0 && Math.Sin(radianTheta2) > 0) {
							//Bottom-Left, Good
							xdelta = -BubbleSize;
							ydelta = BubbleSize;
						} else if (Math.Cos(radianTheta2) > 0 && Math.Sin(radianTheta2) < 0) {
							//Top-Right
							xdelta = BubbleSize;
							ydelta = -BubbleSize;
						}

						float x2 = (float)(((Width / 2) + (Width / 2) * Math.Cos(radianTheta2)) + xdelta);
						float y2 = (float)(((Height / 2) + (Height / 2) * Math.Sin(radianTheta2)) + ydelta);

						float x3 = (float)((Width / 2) + (Width / 2) * Math.Cos(radianTheta3));
						float y3 = (float)((Height / 2) + (Height / 2) * Math.Sin(radianTheta3));

						//Build the triangle between the start angle, the point away from the balloon 
						//(as determined by the BubbleSize), and the sweep angle:

                        points[0] = new Point((int)x, (int)y);
                        points[1] = new Point((int)x2, (int)y2);
                        points[2] = new Point((int)x3, (int)y3);

						//The BubbleSmoothness value determines how curve-like the lines
						//between the three points will be:
						Path.AddCurve(points, BubbleSmoothness);
					}
				}
				break;
			case BalloonShape.Rectangle:
				if (RectangleCornerRadius == 0) {
					//Do the easy one:
					Path.AddRectangle(new Rectangle(0, 0, Width, Height));
				} else {
					//Round Rectangle code adapter from http://www.bobpowell.net/roundrects.htm
					Path.AddLine(0 + RectangleCornerRadius, 0, 0 + Width - (RectangleCornerRadius * 2), 0);

					Path.AddArc(0 + Width - (RectangleCornerRadius * 2), 0, RectangleCornerRadius * 2, RectangleCornerRadius * 2, 270, 90);

					Path.AddLine(0 + Width, 0 + RectangleCornerRadius, 0 + Width, 0 + Height - (RectangleCornerRadius * 2));

					Path.AddArc(0 + Width - (RectangleCornerRadius * 2), 0 + Height - (RectangleCornerRadius * 2), RectangleCornerRadius * 2, RectangleCornerRadius * 2, 0, 90);

					Path.AddLine(0 + Width - (RectangleCornerRadius * 2), 0 + Height, 0 + RectangleCornerRadius, 0 + Height);

					Path.AddArc(0, 0 + Height - (RectangleCornerRadius * 2), RectangleCornerRadius * 2, RectangleCornerRadius * 2, 90, 90);

					Path.AddLine(0, 0 + Height - (RectangleCornerRadius * 2), 0, 0 + RectangleCornerRadius);

					Path.AddArc(0, 0, RectangleCornerRadius * 2, RectangleCornerRadius * 2, 180, 90);
				}
				break;
		}

		//Finish off the path:
		Path.CloseAllFigures();
	}
	#endregion

	#region "Draw"
	/// <summary>
	/// Draws the balloon into the provided graphics object:
	/// </summary>
	/// <param name="g"></param>
	/// <remarks></remarks>
	public void Draw(Graphics g)
	{
		GraphicsState gstate = default(GraphicsState);
		GraphicsPath tail = new GraphicsPath();
		//The path for the tail
		SolidBrush fillBrush = new SolidBrush(FillColor);
		//The background color of the balloon and tail
		SolidBrush textBrush = new SolidBrush(TextColor);
		//The color of the text
		Pen borderPen = new Pen(BorderColor, BorderWidth);
		//The border color & width
		StringFormat sf = new StringFormat();

		//Set the quality to high to make everything look nice and pretty:
		g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		g.SmoothingMode = SmoothingMode.HighQuality;

		//Create the tail's path:
		//Note: To make drawing easier, I assume the tail is centered
		//around the center point of the balloon (from an origin of [0, 0])
		//and that it sticks straight up as far as TailLength.
		//When it comes time to actually draw the tail, I'll
		//call TranslateTransform and RotateTransform to adjust the origin
		//and rotation to where we really want it:

		tail.AddLine(-TailBaseWidth, 0, TailBaseWidth, 0);
		tail.AddLine(TailBaseWidth, 0, 0, -(TailLength + (Height / 2)));

		tail.CloseFigure();

		//Here are the steps to drawing this balloon:
		//1. Draw the tail's border, twice as thick as the balloon's border
		//   When the balloon is filled it will cover up the tail border that
		//   is drawn under the balloon itself
		//2. Fill the balloon's path using the background color
		//3. Draw the balloon's border
		//4. Fill the tail's path using the background color. This ensures
		//   that the outer border of the balloon (where it meets the tail
		//   is colored over with the background color, giving the illusion
		//   that the tail and the balloon are all one big happy object)

		//1. Draw the tail border first (if the border is visible):
		if (TailVisible && BorderVisible) {
			//We double the pen's size because we're going to fill the
			//tail's color overtop half of the border:
			Pen thickPen = new Pen(BorderColor, BorderWidth * 2.0f);

			//Save the graphic state:
			gstate = g.Save();

			//Move to our tail's origin (center of the balloon):
			g.TranslateTransform(Left + (Width / 2), Top + (Height / 2));

			//Rotate the tail around its origin:
			g.RotateTransform(TailRotation);

			//Draw the border:
			g.DrawPath(thickPen, tail);

			//Restore the previous graphic state:
			g.Restore(gstate);
		}

		//Save the state again:
		gstate = g.Save();

		//Move to our balloon's origin:
		g.TranslateTransform(Left, Top);

		//2. Fill the balloon's path using the background brush:
		g.FillPath(fillBrush, Path);

		//3. Draw the balloon's border using the border pen (if the border is visible):
		if (BorderVisible) {
			g.DrawPath(borderPen, Path);
		}

		//Restore the previous graphic state:
		g.Restore(gstate);

		//4. Fill the tail's path using the background brush:
		if (TailVisible) {
			//Save the state yet again:
			gstate = g.Save();

			//Move to our tail's origin (center of the balloon):
			g.TranslateTransform(Left + (Width / 2), Top + (Height / 2));

			//Rotate the tail around its origin:
			g.RotateTransform(TailRotation);

			//Fill 'er up:
			//   This will cover up half of the tail border (thus our need for doubling it above)
			//   and will cover up the balloon border where the balloon and the tail intersect
			g.FillPath(fillBrush, tail);

			//Restore the graphics state:
			g.Restore(gstate);
		}

		//Set our text alignment within the bounds of the balloon, excluding the tail
		sf.LineAlignment = StringAlignment.Center;
		sf.Alignment = StringAlignment.Center;

        int textWidth = Width - 5;
        SizeF textSize = TextRenderer.MeasureText(Text, Font);
        float textHeight = (textSize.Width / textWidth) * textSize.Height + textSize.Height;
        int dynamicHeight = (int)(Font.SizeInPoints * 1.5f) + (int)textHeight - 15;
        if (Height != dynamicHeight)
        {
            Height = dynamicHeight >  10 ? dynamicHeight : 10;
        }
        g.DrawString(Text, Font, new SolidBrush(Color.Black), new RectangleF(55, Font.SizeInPoints * 1.0f - 5, Width - 0, textHeight));

		//Draw out our text using the font and text color brush:
		//g.DrawString(Text, Font, textBrush, Bounds, sf);
        
		//Cleaning up the mess:
		fillBrush.Dispose();
		textBrush.Dispose();
		borderPen.Dispose();
	}
	#endregion

    }

    public enum BalloonShape
    {
        Ellipse,
        Rectangle
    }
}
