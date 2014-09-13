using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Text;
using System.Windows.Forms;
using PDFToolz;

namespace PdfKit
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int start = int.Parse(txtStart.Text);
                int end = int.Parse(txtEnd.Text);
                string fileName = txtFileName.Text;
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("请选择PDF文件!");
                }
                string[] items = new string[3]{fileName,start.ToString(),end.ToString()};  

                OperateItem sf = new OperateItem(fileName, start, end);

                ListViewItem lv = new ListViewItem(items);
                lv.Tag = sf;
                lvItems.Items.Add(lv);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFileName.Text = dlgOpen.FileName;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lv in lvItems.SelectedItems)
            {
                if (lv != null)
                {
                    lvItems.Items.Remove(lv);
                }
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count >= 0)
            {  
                int first = lvItems.SelectedIndices[0];
                if (first > 0)
                {
                    foreach (ListViewItem li in lvItems.SelectedItems)
                    {
                        int index = lvItems.Items.IndexOf(li);
                        lvItems.Items.Remove(li);
                        lvItems.Items.Insert(index - 1, li);
                    }
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count >= 0)
            {
                int last = lvItems.SelectedIndices[lvItems.SelectedItems.Count-1];
                if (last < lvItems.Items.Count -1)
                {
                    foreach (ListViewItem li in lvItems.SelectedItems)
                    {
                        int index = lvItems.Items.IndexOf(li);
                        lvItems.Items.Remove(li);
                        lvItems.Items.Insert(index + 1, li);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTarget.Text = dlgSave.FileName;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                PDFFactory pf = new PDFFactory();
                foreach (ListViewItem li in lvItems.Items)
                {
                    OperateItem sf = li.Tag as OperateItem;
                    if (sf != null)
                    {
                        pf.AddDocument(sf.FileName, sf.StartPage, sf.EndPage);
                    }
                }
                pf.Merge(txtTarget.Text);
                MessageBox.Show("合并文件成功!");
            }
            catch(iTextSharp.text.pdf.BadPasswordException ex)  
            {
                MessageBox.Show("合并文件失败" + ex.Message);
            }
        }

        private void btnOpenSource_Click(object sender, EventArgs e)
        {

            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSource.Text = dlgOpen.FileName;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSource.Text))
                {
                    MessageBox.Show("请选择文件");
                }
                int pageSize = int.Parse(txtPageSize.Text);
                PDFFactory pf = new PDFFactory();
                pf.Splite(txtSource.Text, pageSize);
                MessageBox.Show("拆分文件成功");
            }
            catch
            {
                MessageBox.Show("拆分文件失败");
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                PDFFactory pf = new PDFFactory();
                foreach (ListViewItem li in lvItems.Items)
                {
                    OperateItem sf = li.Tag as OperateItem;
                    if (sf != null)
                    {
                        pf.AddDocument(sf.FileName,"123456");
                    }
                }
                pf.Merge(txtTarget.Text);
                MessageBox.Show("合并文件成功!");
            }
            catch
            {
                MessageBox.Show("合并文件失败");
            }

        }  
    }
}
