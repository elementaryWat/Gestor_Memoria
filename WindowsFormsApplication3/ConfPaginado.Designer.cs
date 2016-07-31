namespace WindowsFormsApplication3
{
    partial class ConfPaginado
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
            this.Tampag = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EtTamMemoria = new System.Windows.Forms.Label();
            this.BotonEst = new System.Windows.Forms.Button();
            this.EtSumaPag = new System.Windows.Forms.Label();
            this.Cantpag = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Tampag
            // 
            this.Tampag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tampag.FormattingEnabled = true;
            this.Tampag.Location = new System.Drawing.Point(140, 39);
            this.Tampag.Name = "Tampag";
            this.Tampag.Size = new System.Drawing.Size(76, 21);
            this.Tampag.TabIndex = 0;
            this.Tampag.SelectedIndexChanged += new System.EventHandler(this.Tampag_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(38, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tamaño de pagina";
            // 
            // EtTamMemoria
            // 
            this.EtTamMemoria.AutoSize = true;
            this.EtTamMemoria.BackColor = System.Drawing.Color.Lime;
            this.EtTamMemoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtTamMemoria.ForeColor = System.Drawing.Color.Black;
            this.EtTamMemoria.Location = new System.Drawing.Point(28, 122);
            this.EtTamMemoria.Name = "EtTamMemoria";
            this.EtTamMemoria.Size = new System.Drawing.Size(45, 16);
            this.EtTamMemoria.TabIndex = 9;
            this.EtTamMemoria.Text = "label2";
            // 
            // BotonEst
            // 
            this.BotonEst.BackColor = System.Drawing.Color.RoyalBlue;
            this.BotonEst.ForeColor = System.Drawing.Color.LightCyan;
            this.BotonEst.Location = new System.Drawing.Point(52, 150);
            this.BotonEst.Name = "BotonEst";
            this.BotonEst.Size = new System.Drawing.Size(183, 23);
            this.BotonEst.TabIndex = 10;
            this.BotonEst.Text = "Establecer tamaño";
            this.BotonEst.UseVisualStyleBackColor = false;
            this.BotonEst.Click += new System.EventHandler(this.BotonEst_Click);
            // 
            // EtSumaPag
            // 
            this.EtSumaPag.AutoSize = true;
            this.EtSumaPag.BackColor = System.Drawing.Color.Red;
            this.EtSumaPag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtSumaPag.ForeColor = System.Drawing.Color.Aquamarine;
            this.EtSumaPag.Location = new System.Drawing.Point(28, 95);
            this.EtSumaPag.Name = "EtSumaPag";
            this.EtSumaPag.Size = new System.Drawing.Size(45, 16);
            this.EtSumaPag.TabIndex = 8;
            this.EtSumaPag.Text = "label1";
            // 
            // Cantpag
            // 
            this.Cantpag.AutoSize = true;
            this.Cantpag.BackColor = System.Drawing.Color.Red;
            this.Cantpag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cantpag.ForeColor = System.Drawing.Color.Aquamarine;
            this.Cantpag.Location = new System.Drawing.Point(28, 69);
            this.Cantpag.Name = "Cantpag";
            this.Cantpag.Size = new System.Drawing.Size(45, 16);
            this.Cantpag.TabIndex = 11;
            this.Cantpag.Text = "label1";
            // 
            // ConfPaginado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(269, 196);
            this.Controls.Add(this.Cantpag);
            this.Controls.Add(this.BotonEst);
            this.Controls.Add(this.EtTamMemoria);
            this.Controls.Add(this.EtSumaPag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tampag);
            this.MaximumSize = new System.Drawing.Size(285, 235);
            this.MinimumSize = new System.Drawing.Size(285, 235);
            this.Name = "ConfPaginado";
            this.Text = "Configuracion de paginado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Tampag;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label EtTamMemoria;
        public System.Windows.Forms.Button BotonEst;
        public System.Windows.Forms.Label EtSumaPag;
        public System.Windows.Forms.Label Cantpag;
    }
}