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
        private string AccessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            AccessToken = "";
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
        /// Disables the controls.
        /// </summary>
        private void disableControls()
        {
            btnChange.Enabled = false;
            btnADown.Enabled = false;
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
            Match m;

            // Check if the URL has the user id and the album id parameter
            // And store it in the list
            if (Regex.IsMatch(url, @"a\.[0-9]+\.[0-9]+\.[0-9]+(&.+)?$"))
            {
                m = Regex.Match(url, @"a\.([0-9]+)\.[0-9]+\.([0-9]+)(&.+)?$");
                details.Add(m.Groups[2].Value);
                details.Add(m.Groups[1].Value);
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
            browser.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            int count = 0, prog = 0;
            string response, pfi, loginData, acctoken = string.Empty, albumId = string.Empty, aName = string.Empty, oName = string.Empty; ;
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

            acctoken = AccessToken;

            // Retrieve the list of albums of the user
            setStatus("Getting Album List");
            response = browser.HttpGet(String.Format("https://graph.facebook.com/{0}/albums?access_token={1}&limit=500", ids[0], acctoken));

            // Decode the JSON response.
            // I used a parser from:
            // http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
            json = (Hashtable)JSON.JsonDecode(response);

            // Go through each album
            // Why? Well we need to know the *actual* album id
            foreach (Hashtable data in (ArrayList)json["data"])
            {
                string aid = data["id"].ToString();
                string name = data["name"].ToString();
                string owner = ((Hashtable)data["from"])["name"].ToString();

                // Escape invalid characters in album name and owner name
                foreach (char i in Path.GetInvalidPathChars())
                {
                    name = name.Replace(i.ToString(), string.Empty);
                    owner = owner.Replace(i.ToString(), string.Empty);
                }

                int pCount = 1;

                // Check for the count field
                try
                {
                    pCount = Int32.Parse(data["count"].ToString());
                }
                catch (Exception ex)
                {
                    // Must be single-photo albums
                }

                // For us to know which album id to pick,
                // We need to look for the short album id in the link
                if (ids[1] == aid)
                {
                    albumExists = true;
                    count = pCount;
                    albumId = aid;
                    aName = name;
                    oName = owner;
                    break;
                }
            }

            // If the album doesn't exist
            if (!albumExists)
            {
                MessageBox.Show("Album doesn't exist", "No Album Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Re enable the controls
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(enableControls));

                // Exit
                return;
            }

            // Create a directory for the owner and album
            Directory.CreateDirectory(String.Format(@"{0}\{1}", txtSv.Text, ValidateWinName(oName)));
            Directory.CreateDirectory(String.Format(@"{0}\{1}\{2}", txtSv.Text, ValidateWinName(oName), ValidateWinName(aName)));


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
                picture.Save(String.Format(@"{0}\{1}\{2}\{3}.jpg", txtSv.Text, ValidateWinName(oName), ValidateWinName(aName), pid));
                updateProgress(prog, count);
            }

            // Finished :)
            MessageBox.Show("Downloading Finished!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Re enable the controls
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(enableControls));
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkDonate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=SMSGBAUQN6QVY");
        }

        /// <summary>
        /// Handles the Click event of the pbFb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbFb_Click(object sender, EventArgs e)
        {
            // Initialize and open a modal form for the Facebook OAuth login
            FacebookLogin fbl = new FacebookLogin();
            fbl.ShowDialog(this);

            // AccessToken will be empty "" if login fails or window was closed manually
            AccessToken = fbl.AccessToken;

            // Enable 'Download' button if login is successful, and display a message instead of the previous login image button
            if (AccessToken != "")
            {
                pbFb.Visible = false;
                lblL1.Visible = true;
                btnADown.Enabled = true;
            }
        }

        /// <summary>
        /// Validates the file/directory name and removes the invalid characters.
        /// </summary>
        /// <param name="str">The string to validate.</param>
        /// <returns></returns>
        public string ValidateWinName(string str)
        {
            StringBuilder sbuilder = new StringBuilder();

            // Gets a char[] array of invalid characters
            char[] invchars = Path.GetInvalidFileNameChars();

            // Loop through each character of the given string, and basically passes the valid characters to the string builder variable
            foreach (char cur in str)
                if (!invchars.Contains(cur))
                    sbuilder.Append(cur);

            return sbuilder.ToString();
        }
    }
}
