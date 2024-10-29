using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BytePatcherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Log.OnLog += AppendTextToRichTextBox;
        }
        // Event handler to append text to richTextBox1 with the specified color
        private void AppendTextToRichTextBox(string message, Color color)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => AppendTextToRichTextBox(message, color)));
            }
            else
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = color;
                richTextBox1.AppendText(message + Environment.NewLine);
                richTextBox1.SelectionColor = richTextBox1.ForeColor; // Reset color
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private async void button1_Click(object sender, EventArgs e)
        {

            string filePath = richTextBox4.Text;
            if (filePath == "")
            {
                Log.Warning("'" + filePath + "' " + "Is not a valid file path");
            }
            else
            {
                byte[] searchPattern = ByteArrayExtensions.ConvertHexStringToByteArray(richTextBox2.Text);
                byte[] buffer = await ByteArrayExtensions.ReadAllBytesAsync(filePath);
                int index = buffer.FindPatternIndex(searchPattern);

                if (index != -1)
                {
                    Log.Message("Search pattern found in the file.");
                }
                else
                {
                    Log.Message("Search pattern not found in the file.");
                }
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string filePath = richTextBox4.Text;

            if (!File.Exists(filePath))
            {
                Log.Warning("'" + filePath + "' " + "Is not a valid file path");
                return;
            }

            // Convert the hex string from richTextBox2 to a byte array
            byte[] searchPattern = ByteArrayExtensions.ConvertHexStringToByteArray(richTextBox2.Text);
            byte[] replacementBytes = ByteArrayExtensions.ConvertHexStringToByteArray(richTextBox3.Text); // Or convert another text box if needed

            try
            {
                byte[] buffer = await ByteArrayExtensions.ReadAllBytesAsync(filePath);
                int index = buffer.FindPatternIndex(searchPattern);

                if (index != -1)
                {
                    Log.Message("Search pattern found in the file. Patching in progress...");

                    await Task.Delay(2000);

                    Array.Copy(replacementBytes, 0, buffer, index, replacementBytes.Length);
                    await ByteArrayExtensions.WriteAllBytesAsync(filePath, buffer);

                    Log.Message("PATCHED BYTES: " + BitConverter.ToString(replacementBytes));
                    Log.Message("Byte Patching Complete.");
                }
                else
                {
                    Log.Warning("Search pattern not found in the file. No patching performed.");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
            }
        }
    }
}
