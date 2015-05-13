using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public class ListPanel : Panel
    {
        public ListPanel()
        {
            AutoScroll = true;
            BorderStyle = BorderStyle.FixedSingle;
        }
        private List<ListPanelItem> items = new List<ListPanelItem>();
        public void AddItem(ListPanelItem item)
        {
            item.Index = items.Count;
            items.Add(item);
            Controls.Add(item);
            item.BringToFront();
            item.Click += ItemClicked;
        }
        public int SelectedIndex { get; set; }
        public ListPanelItem SelectedItem { get; set; }
        private void ItemClicked(object sender, EventArgs e)
        {
            ListPanelItem item = sender as ListPanelItem;
            if (SelectedItem != null) SelectedItem.Selected = false;
            SelectedItem = item;
            SelectedIndex = item.Index;
            item.Selected = true;
            if (ItemClick != null) ItemClick(this, new ItemClickEventArgs() { Item = item });
        }
        public class ItemClickEventArgs : EventArgs
        {
            public ListPanelItem Item { get; set; }
        }
        public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);
        public event ItemClickEventHandler ItemClick;
    }
    
    public class ListPanelItem : Panel
    {
        public ListPanelItem()
        {
            DoubleBuffered = true;
            ImageSize = new Size(40, 60);
            CaptionColor = Color.Blue;
            ContentColor = Color.Red;
            CaptionFont = new Font(Font.FontFamily, 10, FontStyle.Bold);
            ContentFont = new Font(Font.FontFamily, 9, FontStyle.Regular);
            Dock = DockStyle.Top;
            SelectedColor = Color.LightGreen;
            HoverColor = Color.LightGray;
            Caption = "";
            Content = "";
        }
        private bool selected;
        public Size ImageSize { get; set; }
        public Image Image { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public Color CaptionColor { get; set; }
        public Color ContentColor { get; set; }
        public Color backgroundColor { get; set; }
        public Font CaptionFont { get; set; }
        public Font ContentFont { get; set; }
        public int Index { get; set; }
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                Invalidate();
            }
        }
        public Color SelectedColor { get; set; }
        public Color HoverColor { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            /*Color color1 = mouseOver ? Color.FromArgb(0, HoverColor) : Color.FromArgb(0, SelectedColor);
            Color color2 = mouseOver ? HoverColor : SelectedColor;
            Rectangle actualRect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, ClientRectangle.Height - 2);
            using (System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, color1, color2, 45))
            {
                if (mouseOver)
                {
                    e.Graphics.FillRectangle(brush, actualRect);
                }
                else if (Selected)
                {
                    e.Graphics.FillRectangle(brush, actualRect);
                }
            }
            
            //Draw caption
            StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(Caption, CaptionFont, new SolidBrush(CaptionColor), new RectangleF(ImageSize.Width + 10, 5, Width - ImageSize.Width - 10, CaptionFont.SizeInPoints * 1.5f), sf);
            */
            if (Image != null)
            {
                Bitmap bmpFluffy = new Bitmap(Image);
                Rectangle r = new Rectangle(Point.Empty, bmpFluffy.Size);

                using (Bitmap bmpMask = new Bitmap(r.Width, r.Height))
                using (Graphics g = Graphics.FromImage(bmpMask))
                using (GraphicsPath path = createRoundRect(
                    r.X, r.Y,
                    r.Width, r.Height,
                    Math.Min(r.Width, r.Height) / 2))
                using (Brush brush = createFluffyBrush(
                    path,
                    new float[] { 0.0f, 0.1f, 1.0f },
                    new float[] { 0.0f, 0.95f, 1.0f }))
                {
                    g.FillRectangle(Brushes.Black, r);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillPath(brush, path);
                    transferOneARGBChannelFromOneBitmapToAnother(
                        bmpMask,
                        bmpFluffy,
                        ChannelARGB.Blue,
                        ChannelARGB.Alpha);
                }

                e.Graphics.DrawImage(bmpFluffy, new Rectangle(new Point(5, 5), ImageSize));
            }
            //Draw content
            int textWidth = 400;// Width - ImageSize.Width - 10;
            SizeF textSize = TextRenderer.MeasureText(Content, ContentFont);
            float textHeight = (textSize.Width / textWidth) * textSize.Height + textSize.Height;
            int dynamicHeight = (int)(CaptionFont.SizeInPoints * 1.5) + (int)textHeight + 5;
            if (Height != dynamicHeight)
            {
                Height = dynamicHeight > ImageSize.Height + 15 ? dynamicHeight : ImageSize.Height + 10;
            }
            //e.Graphics.DrawString(Content, ContentFont, new SolidBrush(ContentColor), new RectangleF(ImageSize.Width + 10, CaptionFont.SizeInPoints * 1.5f + 5, Width - ImageSize.Width - 10, textHeight));
            //e.Graphics.DrawLine(Pens.Black, new Point(ClientRectangle.Left, ClientRectangle.Bottom - 1), new Point(ClientRectangle.Right, ClientRectangle.Bottom - 1));
            base.OnPaint(e);
        }
        bool mouseOver;
        protected override void OnMouseEnter(EventArgs e)
        {
            mouseOver = true;
            base.OnMouseEnter(e);
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            mouseOver = false;
            base.OnMouseLeave(e);
            Invalidate();
        }

        static public GraphicsPath createRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();

            if (radius == 0)
                gp.AddRectangle(new Rectangle(x, y, width, height));
            else
            {
                gp.AddLine(x + radius, y, x + width - radius, y);
                gp.AddArc(x + width - radius, y, radius, radius, 270, 90);
                gp.AddLine(x + width, y + radius, x + width, y + height - radius);
                gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
                gp.AddLine(x + width - radius, y + height, x + radius, y + height);
                gp.AddArc(x, y + height - radius, radius, radius, 90, 90);
                gp.AddLine(x, y + height - radius, x, y + radius);
                gp.AddArc(x, y, radius, radius, 180, 90);
                gp.CloseFigure();
            }
            return gp;
        }
        
        public static Brush createFluffyBrush(
            GraphicsPath gp,
            float[] blendPositions,
            float[] blendFactors)
        {
            PathGradientBrush pgb = new PathGradientBrush(gp);
            Blend blend = new Blend();
            blend.Positions = blendPositions;
            blend.Factors = blendFactors;
            pgb.Blend = blend;
            pgb.CenterColor = Color.White;
            pgb.SurroundColors = new Color[] { Color.Black };
            return pgb;
        }
        public enum ChannelARGB
        {
            Blue = 0,
            Green = 1,
            Red = 2,
            Alpha = 3
        }

        public static void transferOneARGBChannelFromOneBitmapToAnother(
            Bitmap source,
            Bitmap dest,
            ChannelARGB sourceChannel,
            ChannelARGB destChannel)
        {
            if (source.Size != dest.Size)
                throw new ArgumentException();
            Rectangle r = new Rectangle(Point.Empty, source.Size);
            BitmapData bdSrc = source.LockBits(r, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bdDst = dest.LockBits(r, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* bpSrc = (byte*)bdSrc.Scan0.ToPointer();
                byte* bpDst = (byte*)bdDst.Scan0.ToPointer();
                bpSrc += (int)sourceChannel;
                bpDst += (int)destChannel;
                for (int i = r.Height * r.Width; i > 0; i--)
                {
                    *bpDst = *bpSrc;
                    bpSrc += 4;
                    bpDst += 4;
                }
            }
            source.UnlockBits(bdSrc);
            dest.UnlockBits(bdDst);
        }
    }
}
