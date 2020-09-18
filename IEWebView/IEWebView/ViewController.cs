using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using WebKit;

namespace IEWebView
{
    public partial class ViewController : UIViewController
    {
        CustomWebView webView;

        public ViewController(IntPtr handle) : base(handle)
        {
        }


        private void Btn_TouchDown(object sender, EventArgs e)
        {
            var height = webView.Frame.Height;
            var width = webView.Frame.Width;
            webView.Frame = new CGRect(0, 0, width - 10, height - 10);
        }

        private void Btn_TouchDown1(object sender, EventArgs e)
        {
            var height = webView.Frame.Height;
            var width = webView.Frame.Width;
            webView.Frame = new CGRect(0, 0, width + 10, height + 10);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            webView = new CustomWebView(new CGRect(30, 30, 300, 300), new WKWebViewConfiguration());

            webView.Layer.BorderColor = UIColor.Black.CGColor;
            webView.Layer.BorderWidth = 1;

            var path = NSBundle.MainBundle.PathForResource("Images/" + "Typogy1", "svg");

            if (System.IO.File.Exists(path))
            {
                webView.LoadFileUrl(new NSUrl("file://" + path), new NSUrl("file://" + path));
            }

            this.Add(webView);

            UIButton btn = new UIButton();
            btn.Frame = new CGRect(0, 700, 200, 100);
            btn.SetTitleColor(UIColor.Black, UIControlState.Normal);
            btn.SetTitle("Decrease", UIControlState.Normal);
            btn.TouchDown += Btn_TouchDown;
            this.Add(btn);

            btn = new UIButton();
            btn.Frame = new CGRect(500, 700, 200, 100);
            btn.SetTitleColor(UIColor.Black, UIControlState.Normal);
            btn.SetTitle("Increase", UIControlState.Normal);
            btn.TouchDown += Btn_TouchDown1;
            this.Add(btn);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    public class CustomWebView : WKWebView
    {
        public CustomWebView(CGRect frame, WKWebViewConfiguration configuration) : base(frame, configuration)
        {
            BackgroundColor = UIColor.Clear;
            Opaque = false;
            UserInteractionEnabled = false;
        }
        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                var tempFrame = base.Frame;
                base.Frame = new CGRect(0, 0, Math.Round(value.Width), Math.Round(value.Height));
                if (tempFrame != value && value != CGRect.Empty)
                    UpdateScrollViewFrame();
            }
        }

        void UpdateScrollViewFrame()
        {
            if (ScrollView != null)
            {
                ScrollView.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
                ScrollView.ContentSize = new CGSize(Frame.Width, Frame.Height);
                if (ScrollView.Subviews.Length > 0)
                {
                    ScrollView.Subviews[0].Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
                }
            }
        }
    }
}