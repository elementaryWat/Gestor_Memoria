namespace WindowsFormsApplication3
{
    partial class Usomemoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usomemoria));
            this.Textoprueba = new System.Windows.Forms.Label();
            this.Bloquesmem = new System.Windows.Forms.DataGridView();
            this.IdProc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Libre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FragT = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Bloquesmem)).BeginInit();
            this.SuspendLayout();
            // 
            // Textoprueba
            // 
            this.Textoprueba.AutoSize = true;
            this.Textoprueba.Font = new System.Drawing.Font("Lucida Sans", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textoprueba.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Textoprueba.Location = new System.Drawing.Point(70, 9);
            this.Textoprueba.Name = "Textoprueba";
            this.Textoprueba.Size = new System.Drawing.Size(152, 18);
            this.Textoprueba.TabIndex = 0;
            this.Textoprueba.Text = "Mapa de memoria";
            // 
            // Bloquesmem
            // 
            this.Bloquesmem.AllowUserToAddRows = false;
            this.Bloquesmem.AllowUserToDeleteRows = false;
            this.Bloquesmem.BackgroundColor = System.Drawing.Color.MidnightBlue;
            this.Bloquesmem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Bloquesmem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdProc,
            this.Libre,
            this.Usado});
            this.Bloquesmem.Location = new System.Drawing.Point(1, 84);
            this.Bloquesmem.Name = "Bloquesmem";
            this.Bloquesmem.Size = new System.Drawing.Size(280, 195);
            this.Bloquesmem.TabIndex = 1;
            // 
            // IdProc
            // 
            this.IdProc.HeaderText = "Id_Proceso";
            this.IdProc.Name = "IdProc";
            this.IdProc.ReadOnly = true;
            this.IdProc.Width = 50;
            // 
            // Libre
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Libre.DefaultCellStyle = dataGridViewCellStyle7;
            this.Libre.HeaderText = "Usado";
            this.Libre.Name = "Libre";
            this.Libre.ReadOnly = true;
            // 
            // Usado
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            this.Usado.DefaultCellStyle = dataGridViewCellStyle8;
            this.Usado.HeaderText = "Libre";
            this.Usado.Name = "Usado";
            this.Usado.ReadOnly = true;
            // 
            // FragT
            // 
            this.FragT.AutoSize = true;
            this.FragT.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FragT.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FragT.Location = new System.Drawing.Point(12, 48);
            this.FragT.Name = "FragT";
            this.FragT.Size = new System.Drawing.Size(171, 16);
            this.FragT.TabIndex = 2;
            this.FragT.Text = "Fragmentacion externa: ";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "200px-Greek_uc_sigma.svg.png");
            // 
            // Usomemoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(284, 281);
            this.Controls.Add(this.FragT);
            this.Controls.Add(this.Bloquesmem);
            this.Controls.Add(this.Textoprueba);
            this.MaximumSize = new System.Drawing.Size(300, 320);
            this.MinimumSize = new System.Drawing.Size(300, 320);
            this.Name = "Usomemoria";
            this.Text = "Uso de memoria";
            ((System.ComponentModel.ISupportInitialize)(this.Bloquesmem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Textoprueba;
        public System.Windows.Forms.DataGridView Bloquesmem;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Libre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usado;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Label FragT;
    }
}