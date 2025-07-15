using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    [ToolboxBitmap(typeof(Panel))]
    public partial class CQPGradientPanel : Panel
    {
        private float _rotation;
        private Color _gradientColor;

        //private TimeSpan paintTime = new TimeSpan(250);   // Time to paint, default=250ms
        //private TimeSpan resizeTime = new TimeSpan(100);  // Time to update layout, default=100ms



        internal CQPGradientPanel() : base()
        {
            InitializeComponent();
            
        }

        //protected override void OnLoad(EventArgs e)
        //{
            
        //    DoubleBuffered = true;
        //}

        [Description("GradientColor"),
        Category("Appearance"),
        DefaultValue(typeof(Color), "Color.FromArgb(194, 194, 194)"),
        Browsable(true)]
        public Color GradientColor
        {
            get { return this._gradientColor; }
            set { this._gradientColor = value; }
        }


        [Description("GradientRotation"),
        Category("Appearance"),
        DefaultValue(0),
        Browsable(true)]
        public float GradientRotation
        {
            get { return this._rotation; }
            set { this._rotation = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (e.ClipRectangle.IsEmpty) return; //why draw if non-visible?

            using (LinearGradientBrush lgb = new
                           LinearGradientBrush(this.ClientRectangle,
                      this.BackColor,
                      this.GradientColor,
                      this.GradientRotation))
            {
                e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }

            base.OnPaint(e); //right, want anything handled to be drawn too.
        }

        //bool IsResizing { get; set; }
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    //Stopwatch sw = new Stopwatch();
        //    //sw.Start();
        //    // Do your painting here, or call base.OnPaint

        //    if (e.ClipRectangle.IsEmpty) return; //why draw if non-visible?
            
        //    using (LinearGradientBrush lgb = new
        //                   LinearGradientBrush(this.ClientRectangle,
        //              this.BackColor,
        //              this.GradientColor,
        //              this.GradientRotation))
        //    {
        //        e.Graphics.FillRectangle(lgb, this.ClientRectangle);
        //    }

        //    base.OnPaint(e); //right, want anything handled to be drawn too.

        //    //sw.Stop();
        //    //paintTime = sw.Elapsed;
        //}


        protected override void OnResize(EventArgs e)
        {
            // The "Stop" is not redundant - it will force the timer to "reset"
            // if it is already running.
            //resizeTimer.Stop();
            //base.OnResize(e);
            //int interval = Math.Max(1, (int)(paintTime.TotalMilliseconds + resizeTime.TotalMilliseconds));
            //resizeTimer.Interval = interval;
            
            //resizeTimer.Start();

            this.Invalidate();

            foreach (Control c in Controls)
            {
                if (c.Dock == DockStyle.Fill)
                {
                    c.Size = this.ClientSize;
                }
            }
        }


        private void UpdateSize()
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //this.Invalidate();

            //foreach (Control c in Controls)
            //{
            //    if (c.Dock == DockStyle.Fill)
            //    {
            //        c.Size = this.ClientSize;
            //    }
            //}

            //sw.Stop();
            //resizeTime = sw.Elapsed;
        }

        //private void resizeTimer_Tick(object sender, EventArgs e)
        //{
        //    resizeTimer.Stop();
        //    UpdateSize();
        //}

        
    }
}