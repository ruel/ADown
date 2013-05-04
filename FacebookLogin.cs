using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ADown
{
    public partial class FacebookLogin : Form
    {
        public string AccessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookLogin"/> class.
        /// Automatically navigates to the OAuth login page
        /// </summary>
        public FacebookLogin()
        {
            InitializeComponent();
            AccessToken = "";
            wbMain.Navigate("https://www.facebook.com/dialog/oauth/?client_id=122417977901113&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html&display=popup&scope=user_photos,friends_photos");

        }

        /// <summary>
        /// Handles the Navigated event of the wbMain control.
        /// This will catch all post-navigation events and will check for the access token on each page URL
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebBrowserNavigatedEventArgs"/> instance containing the event data.</param>
        private void wbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // Match the access_token variable in the URL
            Match m = Regex.Match(e.Url.ToString(), "#access_token=(.+?)&");

            // If access_token variable is found, assign it to AccessToken attribute
            if (m.Success)
            {
                AccessToken = m.Groups[1].Value.ToString();
                this.Close();
            }
        }


    }
}
