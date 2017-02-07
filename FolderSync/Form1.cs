﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using Unclassified.UI;

namespace FolderSync
{
    public partial class mainFolderSync : Form
    {
        private SystemMenu systemMenu;

        private string sourcePath;
        private string destPath;
        
        public mainFolderSync()
        {
            InitializeComponent();
            lblDest.Text = null;
            lblSource.Text = null;
            txtStatus.AppendText("...no status");
            Properties.Settings.Default.Reload();

            // Create instance and connect it with the Form
            systemMenu = new SystemMenu(this);

            // Define commands and handler methods
            // (Deferred until HandleCreated if it's too early)
            // IDs are counted internally, separator is optional
            systemMenu.AddCommand("&About…", OnSysMenuAbout, true);

        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);

            // Let it know all messages so it can handle WM_SYSCOMMAND
            // (This method is inlined)
            systemMenu.HandleMessage(ref msg);
        }

        private void OnSysMenuAbout()
        {
            MessageBox.Show("My about message");
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
                lblSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                lblDest.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            txtStatus.Clear();
            destPath = lblDest.Text;
            sourcePath = lblSource.Text;
            Properties.Settings.Default.Save();
            FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                    FileSyncOptions.RecycleDeletedFiles | FileSyncOptions.RecyclePreviousFileOnUpdates | FileSyncOptions.RecycleConflictLoserFiles;
            
            FileSyncScopeFilter filter = new FileSyncScopeFilter();
            filter.FileNameExcludes.Add("*.metadata");

            DetectChangesOnFileSystemReplica(
                sourcePath, filter, options);
            DetectChangesOnFileSystemReplica(
                destPath, filter, options);
                
           SyncFileSystemReplicasOneWay(sourcePath, destPath, filter, options);
           
        }

        public void DetectChangesOnFileSystemReplica(
           string replicaRootPath,FileSyncScopeFilter filter, 
           FileSyncOptions options)
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

                sourceProvider.Configuration.ConflictResolutionPolicy = ConflictResolutionPolicy.SourceWins;
                destinationProvider.Configuration.ConflictResolutionPolicy = ConflictResolutionPolicy.SourceWins;

                sourceProvider.Configuration.CollisionConflictResolutionPolicy = CollisionConflictResolutionPolicy.SourceWins;
                destinationProvider.Configuration.CollisionConflictResolutionPolicy = CollisionConflictResolutionPolicy.SourceWins;        

                destinationProvider.AppliedChange +=
                    new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);
                destinationProvider.SkippedChange +=
                    new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);

                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destinationProvider;
                agent.Direction = SyncDirectionOrder.Upload; // Sync source to destination

                txtStatus.AppendText("Synchronizing changes to replica: " +
                    destinationProvider.RootDirectoryPath);
                txtStatus.AppendText(Environment.NewLine);
                agent.Synchronize();
            }
            finally
            {
                // Release resources
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destinationProvider != null) destinationProvider.Dispose();

                FileSyncReset();
            }
        }

        public void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    txtStatus.AppendText("Applied CREATE for file " + args.NewFilePath);
                    txtStatus.AppendText(Environment.NewLine);
                    break;
                case ChangeType.Delete:
                    txtStatus.AppendText("Applied DELETE for file " + args.OldFilePath);
                    txtStatus.AppendText(Environment.NewLine);
                    break;
                case ChangeType.Update:
                    txtStatus.AppendText("Applied OVERWRITE for file " + args.OldFilePath);
                    txtStatus.AppendText(Environment.NewLine);
                    break;
                case ChangeType.Rename:
                    txtStatus.AppendText("Applied RENAME for file " + args.OldFilePath +
                                      " as " + args.NewFilePath);
                    txtStatus.AppendText(Environment.NewLine);
                    break;
            }
        }

        public void OnSkippedChange(object sender, SkippedChangeEventArgs args)
        {
            txtStatus.AppendText("Skipped applying " + args.ChangeType.ToString().ToUpper()
                  + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ?
                                args.CurrentFilePath : args.NewFilePath) + " due to error");
            txtStatus.AppendText(Environment.NewLine);

            if (args.Exception != null)
                txtStatus.AppendText("   [" + args.Exception.Message + "]");
                txtStatus.AppendText(Environment.NewLine);
        }

		public void FileSyncReset()
		{
			if (File.Exists(sourcePath + "\\filesync.metadata"))
            {
				File.Delete(sourcePath + "\\filesync.metadata");
			}
            if (File.Exists(destPath + "\\filesync.metadata"))
            {
				File.Delete(destPath + "\\filesync.metadata");
			}
		}
    }
}

