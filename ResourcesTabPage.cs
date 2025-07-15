using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace CounselQuickPlatinum
{
    public partial class ResourcesTabPage : CQPTabPage
    {
        string homeSite;
        string errorSite;

        public ResourcesTabPage()
        {
            homeSite = "http://asktop.net/tag/counseling/?json=1&htmlfix=1&count=3";
            //homeSite = "http://www.google.com";
            errorSite = Directory.GetCurrentDirectory().ToString() + @"\error.html";

            Load += ResourcesTabPage_Load;
            InitializeComponent();

            this.Font = base.Font;
        }


        void ResourcesTabPage_Load(object sender, EventArgs e)
        {
            System.Threading.Thread loadWebsiteThread = new System.Threading.Thread(LoadWebSite);
            loadWebsiteThread.Start();
            loadWebsiteThread.IsBackground = true;
            //LoadWebSite();
            webBrowser1.Navigating += webBrowser1_Navigating;
        }


        private void LoadWebSite()
        {
            try
            {
                //System.Threading.Thread.Sleep(10000);
                webBrowser1.AllowNavigation = true;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(homeSite);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (webBrowser1.IsDisposed == false)
                        webBrowser1.Navigate(homeSite);
                }
                else
                {
                    if (webBrowser1.IsDisposed == false)
                        webBrowser1.Navigate(errorSite);
                }


            }
            catch (Exception)
            {
                string cwd = Directory.GetCurrentDirectory();
                //webBrowser1.Url = new Uri("file://" + cwd + "/error.html");
                //webBrowser1.Url = new Uri(cwd + "/error.html");
                if(webBrowser1.IsDisposed == false)
                    webBrowser1.Navigate(errorSite);
            }


        }

        private void ACOPictureBoxOnClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ArmyCounselingOnline.com");
        }

        private void adobeReaderLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://get.adobe.com/reader");
        }

        private void lotusFormsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.e-publishing.af.mil/viewerdownload.asp");
        }

        private void apdLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("ArmyPubs.Army.mil");
        }

        private void capeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://CAPE.army.mil");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://AskTOP.net");
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Uri url = e.Url;

            if (url != new Uri(homeSite) && url != new Uri(errorSite))
            {
                System.Diagnostics.Process.Start(url.ToString());

                e.Cancel = true;
            }
        }
    }
}
