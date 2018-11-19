//' Copyright © Microsoft Corporation.  All Rights Reserved.

//' This code released under the terms of the 

//' Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html)

using System;

using System.IO;

using System.Windows;

using System.Windows.Controls;


namespace LightSwitchApplication.UserCode

{

    public partial class SelectFileWindow : ChildWindow

    {

        public SelectFileWindow()

        {

            InitializeComponent();

        }


        private FileStream documentStream;


        public FileStream DocumentStream

        {

            get { return documentStream; }

            set { documentStream = value; }

        }


        /// <summary>

        /// OK Button

        /// </summary>

        private void OKButton_Click(object sender, RoutedEventArgs e)

        {

            this.DialogResult = true;

        }


        /// <summary>

        /// Cancel button

        /// </summary>

        private void CancelButton_Click(object sender, RoutedEventArgs e)

        {

            this.DialogResult = false;

        }
        public String FileName { get; set; }


        /// <summary>

        /// Browse button

        /// </summary>

        private void BrowseButton_Click(object sender, RoutedEventArgs e)

        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Limit the dialog to only show ".csv" files,

            // modify this as necessary to allow other file types

            openFileDialog.Filter = "Image Files(*.JPG;)|*.JPG;|Doc Files (*.pdf;)|*.pdf|Media Files (*.mp4;*.mp3)|*.mp4;*.mp3";

            openFileDialog.FilterIndex = 1;


            if (openFileDialog.ShowDialog() == true)

            {

                this.FileTextBox.Text = openFileDialog.File.Name;

                this.FileTextBox.IsReadOnly = true;
                this.FileName = openFileDialog.File.Name;
                System.IO.FileStream myStream = openFileDialog.File.OpenRead();

                documentStream = myStream;

            }

        }

    }

}
