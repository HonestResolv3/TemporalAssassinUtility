using AltoHttp;
using LibGit2Sharp;
using SevenZipNET;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemporalAssassinUtility
{
    public partial class frmTAU : Form
    {
        HttpDownloader downloader;
        DialogResult dialogResult;
        string fileDirectory;
        readonly string patches = "https://gitlab.com/B1gJohnson/temporal-assassin.git";
        bool installing = false;
        bool updating = false;

        public frmTAU()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A method that will load the users folder input when the program is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTAU_Load(object sender, EventArgs e)
        {
            txtGML.Text = Properties.Settings.Default.GarrysModLocation;
        }

        /// <summary>
        /// A method that will stop the current download and save the users folder input when the program is closing
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Form closing event argument</param>
        private void frmTAU_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (downloader != null)
                downloader.StopReset();
            Properties.Settings.Default.GarrysModLocation = txtGML.Text;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// A method that will generate a folder browsing dialog when the "Garry's Mod Location" button is clicked
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Form closing event argument</param>
        private void btnGML_Click(object sender, EventArgs e)
        {
            fdiagFolderBrowser.Description = "Select Garry's Mod folder";
            dialogResult =  fdiagFolderBrowser.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(fdiagFolderBrowser.SelectedPath) && File.Exists(Path.Combine(fdiagFolderBrowser.SelectedPath, "hl2.exe")))
                    txtGML.Text = fdiagFolderBrowser.SelectedPath;
                else
                    MessageBox.Show($"Please make sure the install location is where Garry's Mod is installed (hl2.exe should be there)", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// A method that will install Temporal Assassin when the "Install" button is clicked
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Form closing event argument</param>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (updating)
            {
                MessageBox.Show("Please wait until the program finishes updating to re-download Temporal Assassin", "Updating already occurring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (installing)
            {
                MessageBox.Show("Please wait until the program finishes installing to re-download Temporal Assassin", "Downloading already occurring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGML.Text))
            {
                MessageBox.Show("Enter in the location of where your Garry's Mod game is installed" +
                    $"{Environment.NewLine}{Environment.NewLine}Default: steamapps/common/GarrysMod", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(txtGML.Text))
            {
                MessageBox.Show($"Please make sure ({txtGML.Text}) is a valid path", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(Path.Combine(txtGML.Text, "hl2.exe")))
            {
                MessageBox.Show($"Please make sure the install location is where Garry's Mod is installed (hl2.exe should be there)", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fileDirectory = Path.Combine(txtGML.Text, "TA.zip");
            ResetValues();
            rtbOutput.Text = "";
            rtbOutput.Text += $"Downloading Temporal Assassin main files to \"{txtGML.Text}\"{Environment.NewLine}Note: Please do not close the utility until the installation and extraction is done!{Environment.NewLine}";
            installing = true;

            try
            {
                downloader = new HttpDownloader("http://webpages.uncc.edu/hquresh1/TA.7z", fileDirectory);
                downloader.ProgressChanged += OnDownloadProgressChanged;
                downloader.DownloadCompleted += OnDownloadCompleted;
                downloader.ErrorOccured += OnErrorReceived;
                downloader.DownloadInfoReceived += OnDownloadReceived;
                downloader.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error trying to initialize Temporal Assassin download" +
                    $"\n\nError: {ex}", "Download initialization error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                installing = false;
                ResetValues();
                return;
            }
        }

        /// <summary>
        /// A method that is run when the file was successfully able to be retrieved from the web server
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event argument</param>
        private void OnDownloadReceived(object sender, EventArgs e)
        {
            rtbOutput.ThreadSafeInvoke(() =>
            {
                rtbOutput.Text += $"Note: Size of file is {downloader.Info.Length.ToHumanReadableSize()}{Environment.NewLine}{Environment.NewLine}";
            });
        }

        /// <summary>
        /// A method that updates the progress of the download
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Progress changed event argument</param>
        private void OnDownloadProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            pbxProgress.ThreadSafeInvoke(() =>
            {
                pbxProgress.Value = (int)(((double)e.TotalBytesReceived / (double)downloader.Info.Length) * 100);
            });
        }

        /// <summary>
        /// A method that is run when the file is finished downloading
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event argument</param>
        private void OnDownloadCompleted(object sender, EventArgs e)
        {
            try
            {
                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text += $"Extracting Temporal Assassin files to \"{txtGML.Text}\"{Environment.NewLine}Note: This process will use up at least 400 MB+ of memory to extract all the contents{Environment.NewLine}{Environment.NewLine}";
                });

                SevenZipExtractor extract = new SevenZipExtractor(fileDirectory, "temporalpublicbuild");
                extract.ExtractAll(txtGML.Text, true);

                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text += $"Finished extracting Temporal Assassin files to \"{txtGML.Text}\"!{Environment.NewLine}{Environment.NewLine}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error trying to extract the Temporal Assassin contents" +
                    $"\n\nError: {ex}", "Extract error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                installing = false;
            }

            try
            {
                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text += $"Deleting {fileDirectory}, make sure to clear it from your computers Recycle Bin as well{Environment.NewLine}{Environment.NewLine}";
                });
                File.Delete(fileDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error trying to delete the Temporal Assassin contents after the extracting was done" +
                    $"\n\nError: {ex}", "File delete error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                installing = false;
            }

            ResetValues();
            installing = false;
        }

        /// <summary>
        /// A method that is run when the downloader encounters an issue
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event argument</param>
        private void OnErrorReceived(object sender, ErrorEventArgs e)
        {
            MessageBox.Show("There was an error trying download Temporal Assassin" +
                    $"\n\nError: {e.GetException().Message}", "Download error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            installing = false;
        }

        /// <summary>
        /// A method that updates the Temporal Assassin game when clicked on, must have a valid directory
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event argument</param>
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (updating)
            {
                MessageBox.Show("Please wait until the program finishes updating to re-download Temporal Assassin", "Updating already occurring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (installing)
            {
                MessageBox.Show("Please wait until the program finishes installing to re-download Temporal Assassin", "Downloading already occurring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGML.Text))
            {
                MessageBox.Show("Enter in the location of where your Garry's Mod game is installed" +
                    $"{Environment.NewLine}{Environment.NewLine}Default: steamapps/common/GarrysMod", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(txtGML.Text))
            {
                MessageBox.Show($"Please make sure ({txtGML.Text}) is a valid path", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(Path.Combine(txtGML.Text, "hl2.exe")))
            {
                MessageBox.Show($"Please make sure the install location is where Garry's Mod is installed (hl2.exe should be there)", "Invalid path entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text = "";
                    rtbOutput.Text += $"Checking if the directory has extra temporary folder to delete{Environment.NewLine}{Environment.NewLine}";
                });

                updating = true;
                string tempDir = Path.Combine(txtGML.Text, "Patch_Temp");
                MessageBox.Show(tempDir);
                if (Directory.Exists(tempDir))
                    ModifyTempDirVisibility(tempDir);

                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text += $"Downloading Temporal Assassin patch to \"{tempDir}\" (will be moved later to \"{txtGML.Text}\"){Environment.NewLine}Note: Please do not close the utility until the installation is done!{Environment.NewLine}{Environment.NewLine}";
                });

                Directory.CreateDirectory(tempDir);
                bool result = await Task.Run(() => CloneRepository(tempDir));

                if (result)
                {
                    string new7z = Path.Combine(tempDir, "TA_Patch.zip");
                    MessageBox.Show($"Temp Dir: {tempDir}\n.7z Dir: {new7z}");
                    rtbOutput.ThreadSafeInvoke(() =>
                    {
                        rtbOutput.Text += $"Creating .zip of downloaded contents in \"{tempDir}\" to \"{new7z}\"{Environment.NewLine}{Environment.NewLine}";
                    });

                    SevenZipCompressor compressor = new SevenZipCompressor(new7z);
                    compressor.CompressDirectory(tempDir);

                    string currentDir = Path.Combine(txtGML.Text.Substring(0, txtGML.Text.LastIndexOf("\\")), "TA_Patch.7z");
                    MessageBox.Show($"Temp Dir: {tempDir}\n.7z Dir: {new7z}\nWorking Dir: {currentDir}");
                    rtbOutput.ThreadSafeInvoke(() =>
                    {
                        rtbOutput.Text += $"Moving and extracting .7z of downloaded contents in \"{tempDir}\" to \"{currentDir}\"{Environment.NewLine}{Environment.NewLine}";
                    });
                    File.Move(new7z, currentDir);
                    MessageBox.Show($"Temp Dir: {tempDir}\n.7z Dir: {new7z}\nWorking Dir: {currentDir}");
                    SevenZipExtractor extractor = new SevenZipExtractor(currentDir);
                    extractor.ExtractAll(txtGML.Text + "\\Test", true, true);

                    File.Delete(currentDir);
                }

                updating = false;
                ModifyTempDirVisibility(Path.Combine(txtGML.Text, "Patch_Temp"));
                rtbOutput.ThreadSafeInvoke(() =>
                {
                    rtbOutput.Text += $"Finished downloading Temporal Assassin patch to \"{txtGML.Text}\"!{Environment.NewLine}{Environment.NewLine}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error trying to install Temporal Assassin" +
                    $"\n\nError: {ex}", "Error trying to install Temporal Assassin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updating = false;
                return;
            }
        }

        /// <summary>
        /// A method to asynchrously clone the Git repositories contents into the temporary folder
        /// </summary>
        /// <param name="tempDir">The directory of the temporary file locations as a "string"</param>
        /// <returns>A Task object with a boolean inside of it</returns>
        private Task<bool> CloneRepository(string tempDir)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            try
            {
                Repository.Clone(patches, tempDir);
                tcs.TrySetResult(true);
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
            }
            return tcs.Task;
        }

        /// <summary>
        /// A method to safely invoke updating the progress bar on the UI from another thread
        /// </summary>
        private void ResetValues()
        {
            pbxProgress.ThreadSafeInvoke(() =>
            {
                pbxProgress.Value = 0;
            });
        }

        /// <summary>
        /// A method to update all directories and files's attributes to be none/normal so they can be safely deleted
        /// 
        /// This was originally from StackOverflow but it did not support file attributes deep within a folder structure (file was in multiple directory levels)
        /// </summary>
        /// <param name="directory">The directory as a DirectoryInfo object (holds a "string" path)</param>
        private void SetFileAttributesRecursively(DirectoryInfo directory, FileAttributes attribute)
        {
            if (directory.GetDirectories().Length == 0)
                return;

            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                dir.Attributes = attribute;
                foreach (FileInfo file in dir.GetFiles())
                    file.Attributes = attribute;
                SetFileAttributesRecursively(dir, attribute);
            }
        }

        /// <summary>
        /// A helper method to look at the temporary directory, set all directories and files in it to normal, then delete all contents in said directory
        /// </summary>
        /// <param name="tempDir"></param>
        private void ModifyTempDirVisibility(string tempDir)
        {
            SetFileAttributesRecursively(new DirectoryInfo(tempDir), FileAttributes.Normal);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }
}
