using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;


namespace FolderSync
{
    public partial class mainFolderSync : Form
    {
        private string sourcePath;
        private string destPath;

        public mainFolderSync()
        {
            InitializeComponent();
            lblDest.Text = null;
            lblSource.Text = null;
            lblStatus.Text = "...no status";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                sourcePath = lblSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                destPath = lblDest.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                    FileSyncOptions.RecycleDeletedFiles | FileSyncOptions.RecyclePreviousFileOnUpdates | FileSyncOptions.RecycleConflictLoserFiles;
            

            FileSyncScopeFilter filter = new FileSyncScopeFilter();
            filter.FileNameExcludes.Add("*.lnk"); // Exclude all *.lnk files

            DetectChangesOnFileSystemReplica(
                sourcePath, filter, options);
            DetectChangesOnFileSystemReplica(
                destPath, filter, options);

            SyncFileSystemReplicasOneWay(sourcePath, destPath, null, options);

        }

        public void DetectChangesOnFileSystemReplica(
           string replicaRootPath,
           FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider provider = null;

            try
            {
                provider = new FileSyncProvider(replicaRootPath, filter, options);
                provider.DetectChanges();
            }
            finally
            {
                // Release resources
                if (provider != null)
                    provider.Dispose();
            }
        }

        public void SyncFileSystemReplicasOneWay(
            string sourceReplicaRootPath, string destinationReplicaRootPath,
            FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destinationProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(
                    sourceReplicaRootPath, filter, options);
                destinationProvider = new FileSyncProvider(
                    destinationReplicaRootPath, filter, options);

                destinationProvider.AppliedChange +=
                    new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
                destinationProvider.SkippedChange +=
                    new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destinationProvider;
                agent.Direction = SyncDirectionOrder.Upload; // Sync source to destination

                this.lblStatus.Text = "Synchronizing changes to replica: " +
                    destinationProvider.RootDirectoryPath;
                agent.Synchronize();
            }
            finally
            {
                // Release resources
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destinationProvider != null) destinationProvider.Dispose();
            }
        }

        public void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    lblStatus.Text = "-- Applied CREATE for file " + args.NewFilePath;
                    break;
                case ChangeType.Delete:
                    lblStatus.Text = "-- Applied DELETE for file " + args.OldFilePath;
                    break;
                case ChangeType.Update:
                    lblStatus.Text = "-- Applied OVERWRITE for file " + args.OldFilePath;
                    break;
                case ChangeType.Rename:
                    lblStatus.Text = "-- Applied RENAME for file " + args.OldFilePath +
                                      " as " + args.NewFilePath;
                    break;
            }
        }

        public void OnSkippedChange(object sender, SkippedChangeEventArgs args)
        {
            lblStatus.Text = "-- Skipped applying " + args.ChangeType.ToString().ToUpper()
                  + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ?
                                args.CurrentFilePath : args.NewFilePath) + " due to error";

            if (args.Exception != null)
                lblStatus.Text = "   [" + args.Exception.Message + "]";
        }
    }
}

