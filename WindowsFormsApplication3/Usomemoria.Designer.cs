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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Textoprueba = new System.Windows.Forms.Label();
            this.Bloquesmem = new System.Windows.Forms.DataGridView();
            this.IdProc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Libre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Bloquesmem)).BeginInit();
            this.SuspendLayout();
            // 
            // Textoprueba
            // 
            this.Textoprueba.AutoSize = true;
            this.Textoprueba.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textoprueba.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Textoprueba.Location = new System.Drawing.Point(70, 24);
            this.Textoprueba.Name = "Textoprueba";
            this.Textoprueba.Size = new System.Drawing.Size(143, 18);
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
            this.Bloquesmem.Location = new System.Drawing.Point(2, 54);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Libre.DefaultCellStyle = dataGridViewCellStyle1;
            this.Libre.HeaderText = "Usado";
            this.Libre.Name = "Libre";
            this.Libre.ReadOnly = true;
            // 
            // Usado
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.Usado.DefaultCellStyle = dataGridViewCellStyle2;
            this.Usado.HeaderText = "Libre";
            this.Usado.Name = "Usado";
            this.Usado.ReadOnly = true;
            // 
            // Usomemoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Bloquesmem);
            this.Controls.Add(this.Textoprueba);
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
    }
}