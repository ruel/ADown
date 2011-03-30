using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using BasicReq;
using Procurios.Public;

namespace ADown
{
    public partial class frmMain : Form
    {

        /// <summary>
        /// Control for the threads.
        /// </summary>
        private bool stopAll = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the lblDev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lblDev_Click(object sender, EventArgs e)
        {
            Process.Start("http://ruel.me");
        }

        /// <summary>
        /// Handles the Click event of the btnADown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnADown_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("You must provide a Facebook login information.", "Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtAl.Text == "")
            {
                MessageBox.Show("Invalid Album URL", "Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            // Start the main sub thread
            Thread mSub = new Thread(unused => ADinit());
            mSub.Start();

            disableControls();
        }

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// Create the directory where the images will be saved.
        /// Additional directories will be created for different
        /// users and albums.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtSv.Text = String.Format(@"{0}\ADown", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Directory.CreateDirectory(txtSv.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnChange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            fbdSv.ShowDialog();
            if (fbdSv.SelectedPath != "")
            {
                txtSv.Text = fbdSv.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop all threads.
            stopAll = true;
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void disableControls()
        {
            btnChange.Enabled = false;
            btnADown.Enabled = false;
            txtEmail.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtSv.ReadOnly = true;
            txtAl.ReadOnly = true;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void enableControls()
        {
            btnChange.Enabled = true;
            btnADown.Enabled = true;
            txtEmail.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtSv.ReadOnly = false;
            txtAl.ReadOnly = false;

            // Yes, because enableControls is only called
            // When all threads (should be) finished.
            setStatus("Idle");
            updateProgress(0, 0);
        }

        /// <summary>
        /// Sets the status in the form.
        /// And since this will only be called on threads,
        /// I'll put the invoke code here.
        /// </summary>
        /// <param name="status">The string to display.</param>
        private void setStatus(string status)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    stStat.Text = status;
                }));
            }
            else
            {
                stStat.Text = status;
            }
        }

        /// <summary>
        /// Updates the progress status text.
        /// </summary>
        /// <param name="prog">The number of photos processed.</param>
        /// <param name="total">The total number of photos.</param>
        private void updateProgress(int prog, int total)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    stProg.Text = String.Format("{0} of {1}", prog, total);
                }));
            }
            else
            {
                stProg.Text = String.Format("{0} of {1}", prog, total);
            }
        }

        /// <summary>
        /// Parses the album URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A string list containing the short album ID, user ID and the validity</returns>
        private List<string> ParseAlbumUrl(string url)
        {
            List<string> details = new List<string>();
            bool valid = true;
            Match m;

            // Check if the URL has the user id parameter
            // And store it in the list
            if (Regex.IsMatch(url, "id=[0-9]+"))
            {
                m = Regex.Match(url, "id=([0-9]+)");
                details.Add(m.Groups[1].Value);
            } else {
                valid = false;
            }

            // Check if the URL containts the album id
            // And store it aswell
            if (Regex.IsMatch(url, "id=[0-9]+"))
            {
                m = Regex.Match(url, "aid=([0-9]+)");
                details.Add(m.Groups[1].Value);
            }
            else
            {
                valid = false;
            }


            // Check if the URL is valid
            if (valid)
            {
                // If it's valid add the word "valid"
                details.Add("valid");
            }
            else
            {
                // If it's not, clear the list and add null, and invalid.
                details.Clear();
                details.AddRange(new string[] { "null", "null", "invalid" });
            }

            return details;
        }



        /// <summary>
        /// Initializes ADown.
        /// Checks if the login information is valid, and gets
        /// the access token for the Graph API.
        /// </summary>
        private void ADinit()
        {
            // Initialize the Basic Request Class, and other variables.
            BReq browser = new BReq();
            int count = 0, prog = 0;
            string response, pfi, loginData, acctoken, albumId = string.Empty, aName = string.Empty;
            bool albumExists = false;
            Hashtable json;
            List<string> ids, albums = new List<string>();

            // Validate the Album URL.
            ids = ParseAlbumUrl(txtAl.Text);
            if (ids[2] != "valid")
            {
                MessageBox.Show("The URL of the album you entered is invalid.", "Invalid Album URL", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Re enable the controls
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(enableControls));

                return;
            }
            
            // Get the Post form ID in facebook login page.
            // Regular expression is used because this is a
            // simple data capture, no need for external
            // HTML parsers.
            setStatus("Getting PFI");
            response = browser.HttpGet("http://m.facebook.com/index.php");
            pfi = Regex.Match(response, @"name=""post_form_id"" value=""(\w+)""").Groups[1].Value;
            
            // Then we use the PFI to initialize the post data
            // for the login.
            loginData = "lsd=";
            loginData += "&post_form_id=" + pfi;
            loginData += "&charset_test=" + HttpUtility.UrlDecode("%E2%82%AC%2C%C2%B4%2C%E2%82%AC%2C%C2%B4%2C%E6%B0%B4%2C%D0%94%2C%D0%84");
            loginData += "&email=" + txtEmail.Text;
            loginData += "&pass=" + txtPassword.Text;
            loginData += "&login=Login";

            // Login to facebook.
            // NOTE that there's a great chance for this to FAIL
            // in the future. As for now, March 11, 2011, this
            // works just great.
            setStatus("Logging in to Facebook");
            response = browser.HttpPost("https://www.facebook.com/login.php?m=m&refsrc=http%3A%2F%2Fm.facebook.com%2Findex.php&refid=8", loginData);
            if (!response.Contains("Logout"))
            {
                MessageBox.Show("Please make sure the credentials are valid.", "Unable to login to account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Re enable the controls
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(enableControls));

                // Log the HTML file (for debugging)
                browser.DebugHtml(response);
                return;
            }

            // Get the Access Token
            // If this one fails, please email me
            // bugs@ruel.me
            setStatus("Getting Access Token");
            response = browser.HttpGet("http://developers.facebook.com/docs/reference/api");
            acctoken = Regex.Match(response, @"access_token=(.*?)""").Groups[1].Value;
            
            // Fail handler (this should not be happening)
            if (acctoken == "")
            {
                MessageBox.Show("Access Token not found, This should not be happening.\nPlease send an email to bugs@ruel.me", "Can't get Access Token", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Re enable the controls
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(enableControls));

                // Log the HTML file (for debugging)
                browser.DebugHtml(response);
                return;
            }

            // Retrieve the list of albums of the user
            setStatus("Getting Album List");
            response = browser.HttpGet(String.Format("https://graph.facebook.com/{0}/albums?access_token={1}", ids[0], acctoken));

            // Decode the JSON response.
            // I used a parser from:
            // http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
            json = (Hashtable)JSON.JsonDecode(response);

            // Go through each album
            // Why? Well we need to know the *actual* album id
            foreach (Hashtable data in (ArrayList)json["data"])
            {
                string aid = data["id"].ToString();
                string link = data["link"].ToString();
                string name = data["name"].ToString();
                int pCount = Int32.Parse(data["count"].ToString());

                // For us to know which album id to pick,
                // We need to look for the short album id in the link
                if (Regex.IsMatch(link, String.Format("aid={0}", ids[1])))
                {
                    albumExists = true;
                    count = pCount;
                    albumId = aid;
                    aName = name;
                }
            }

            // If the album doesn't exist
            if (!albumExists)
            {
                // Re enable the controls
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(enableControls));

                // Exit
                return;
            }

            // Create a directory for that album
            Directory.CreateDirectory(String.Format(@"{0}\{1}", txtSv.Text, aName));

            // Update the progress total
            updateProgress(0, count);

            // Get all pictures in the album
            setStatus("Getting Picture List");
            response = browser.HttpGet(String.Format("https://graph.facebook.com/{0}/photos?access_token={1}&limit=500", albumId, acctoken));

            // Decode the JSON again
            json = (Hashtable)JSON.JsonDecode(response);

            // Finally Download each picture
            setStatus("Downloading Photos");
            foreach (Hashtable photo in (ArrayList)json["data"])
            {
                string source = photo["source"].ToString();
                string pid = photo["id"].ToString();
                Image picture;

                // Download and save image
                prog++;
                picture = browser.GetImage(source);
                picture.Save(String.Format(@"{0}\{1}\{2}.jpg", txtSv.Text, aName, pid));
                updateProgress(prog, count);
            }

            // Finished :)
            MessageBox.Show("Downloading Finished!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Re enable the controls
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(enableControls));
        }
    }
}
