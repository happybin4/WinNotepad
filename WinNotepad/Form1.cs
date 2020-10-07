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

namespace WinNotepad
{
    public partial class Form1 : Form
    {
        //전역변수자리
        bool dirty = false; //변경내용이 있는지 없는지
        string dirtyText = "*{0} - windows 메모장";
        string notDirtyText = "{0} - windows 메모장";
        string editingFileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "제목없음 - windows 메모장";
            this.textBox1.Focus();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                editingFileName = openFileDialog1.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader
                        (editingFileName, Encoding.UTF8))
                    {
                        textBox1.Text = sr.ReadToEnd();
                    }
                    dirty = false; //파일을 새로 열었을때는 dirty를 초기값으로 돌려나야한다
                    UpdateText();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                
            }
        }

        private void UpdateText()
        {
            string FileName = editingFileName.Substring(editingFileName.LastIndexOf('\\') + 1);
            
            if (dirty)
                this.Text = string.Format(dirtyText, FileName);
            else
                this.Text = string.Format(notDirtyText, FileName);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(! dirty)
            {
                dirty = true;
                UpdateText();
            }
            
        }
    }
}
