namespace MainXBN
{
    partial class XbnMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XbnMain));
            this.ListViewXBN = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.SNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.XMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListViewXBN
            // 
            this.ListViewXBN.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.ListViewXBN.AllowDrop = true;
            this.ListViewXBN.AutoArrange = false;
            this.ListViewXBN.BackColor = System.Drawing.SystemColors.Window;
            this.ListViewXBN.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ListViewXBN.GridLines = true;
            this.ListViewXBN.HideSelection = false;
            this.ListViewXBN.HoverSelection = true;
            this.ListViewXBN.Location = new System.Drawing.Point(12, 42);
            this.ListViewXBN.MultiSelect = false;
            this.ListViewXBN.Name = "ListViewXBN";
            this.ListViewXBN.Size = new System.Drawing.Size(408, 157);
            this.ListViewXBN.TabIndex = 0;
            this.ListViewXBN.UseCompatibleStateImageBehavior = false;
            this.ListViewXBN.View = System.Windows.Forms.View.List;
            this.ListViewXBN.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewXBN_DragDrop);
            this.ListViewXBN.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewXBN_DragEnter);
            this.ListViewXBN.DragLeave += new System.EventHandler(this.ListViewXBN_DragLeave);
            this.ListViewXBN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GeneralKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Arrastra archivos a convertir aquí:";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "xbn";
            this.SaveFileDialog.Filter = "(*.XBN)|*.xbn";
            this.SaveFileDialog.RestoreDirectory = true;
            this.SaveFileDialog.Title = "Guardar Archivo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(9, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 42);
            this.label2.TabIndex = 2;
            this.label2.Text = "Remover archivo: Ctrl + Shift + R\r\nConvertir archivo(s): Ctrl + Shift + C\r\nCrédit" +
    "os: Ctrl + Shift + A";
            // 
            // SNotifyIcon
            // 
            this.SNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.SNotifyIcon.ContextMenuStrip = this.XMenuStrip;
            this.SNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SNotifyIcon.Icon")));
            this.SNotifyIcon.Text = "S4League XBN";
            this.SNotifyIcon.Visible = true;
            // 
            // XMenuStrip
            // 
            this.XMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.minimizarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.XMenuStrip.Name = "XMenuStrip";
            this.XMenuStrip.Size = new System.Drawing.Size(128, 70);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuItemClick);
            // 
            // minimizarToolStripMenuItem
            // 
            this.minimizarToolStripMenuItem.Name = "minimizarToolStripMenuItem";
            this.minimizarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.minimizarToolStripMenuItem.Text = "Minimizar";
            this.minimizarToolStripMenuItem.Click += new System.EventHandler(this.DownMenuItemClick);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
            // 
            // XbnMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 267);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListViewXBN);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "XbnMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S4League: XbnConverter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XbnMain_FormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.XMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListViewXBN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon SNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip XMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}

