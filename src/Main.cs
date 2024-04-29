using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XbnConvert;

namespace MainXBN
{
    public partial class XbnMain : Form
    {
        private readonly XbnService _xbnService; 

        public XbnMain()
        {
            InitializeComponent();
            _xbnService = new XbnService();
        }

        private void ListViewXBN_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            ListViewXBN.BackColor = Color.LightGray;
        }

        private void ListViewXBN_DragDrop(object sender, DragEventArgs e)
        {
            var archivos = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var f in archivos)
            {
                var extension = Path.GetExtension(f);
                if (extension != ".x7"  && extension != ".xml")
                {
                    MessageBox.Show("Este programa solo admite archivos xml", "Advertencia!");
                    break;
                }

                ListViewXBN.Items.Add(f);
            }
            
            ListViewXBN.BackColor = Color.White;
        }

        private void ListViewXBN_DragLeave(object sender, EventArgs e)
        {
            ListViewXBN.BackColor = Color.White;
        }

        private void GeneralKeyDown(object sender, KeyEventArgs e)
        {
            Control ex = (Control)sender;
            if (e.Control && e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        {
                            if (ListViewXBN.Items.Count < 1)
                            {
                                MessageBox.Show("No hay elementos a convertir", "Información!");
                                break;
                            }

                            foreach (var item in ListViewXBN.Items)
                            {
                                var item_path = (ListViewItem)item;
                                _xbnService.StartAsync(item_path.Text);
                                ListViewXBN.Items.RemoveAt(item_path.Index);
                            }
                            break;
                        }

                    case Keys.R:
                        {
                            if (ListViewXBN.Items.Count < 1)
                            {
                                MessageBox.Show("No hay elementos a remover", "Información!");
                                break;
                            }

                            MessageBox.Show("Revoming xbnfiles", "Procesando!");
                            ListViewXBN.Items.RemoveAt(ex.TabIndex);
                            break;
                        }

                    case Keys.A:
                        {
                            var msg = "[CREDITOS]\nWindowForms by: S4Archivos\nPlugin by: Wtfblub";
                            MessageBox.Show(msg, "ABOUT US!");
                            break;
                        }
                }
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
        }

        private void OpenMenuItemClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void DownMenuItemClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void XbnMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
