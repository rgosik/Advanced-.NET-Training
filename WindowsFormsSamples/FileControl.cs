using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsSamples
{
    public partial class FileControl : Form
    {
        public FileControl()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var currentPath = Environment.CurrentDirectory;
            openFileDialog1.InitialDirectory = currentPath;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Text files | *.txt";
            var dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                using (var stream = openFileDialog1.OpenFile())
                {
                    var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();
                    textBox1.Text = content;
                    lblFile.Text = openFileDialog1.FileName;
                    reader.Dispose();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var currentPath = Environment.CurrentDirectory;
            saveFileDialog1.InitialDirectory = currentPath;
            saveFileDialog1.Filter = "Text files | *.txt";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CreatePrompt = false;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.FileName = openFileDialog1.FileName;
            var dialogResult = saveFileDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                using (var writer = new StreamWriter(saveFileDialog1.FileName, false))
                {
                    writer.Write(textBox1.Text);
                }
                lblFile.Text = "SAVED!";
            }
        }
    }
}
