namespace WindowsFormsApplication3
{
    partial class DialogoPartIgtam
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
            this.BotonEst = new System.Windows.Forms.Button();
            this.TextoCP = new System.Windows.Forms.TextBox();
            this.EtiquetaCP = new System.Windows.Forms.Label();
            this.EtSumaPart = new System.Windows.Forms.Label();
            this.EtTamMemoria = new System.Windows.Forms.Label();
            this.Tampart = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BotonEst
            // 
            this.BotonEst.BackColor = System.Drawing.Color.RoyalBlue;
            this.BotonEst.ForeColor = System.Drawing.Color.LightCyan;
            this.BotonEst.Location = new System.Drawing.Point(61, 133);
            this.BotonEst.Name = "BotonEst";
            this.BotonEst.Size = new System.Drawing.Size(183, 23);
            this.BotonEst.TabIndex = 5;
            this.BotonEst.Text = "Establecer cantidad";
            this.BotonEst.UseVisualStyleBackColor = false;
            this.BotonEst.Click += new System.EventHandler(this.BotonEst_Click);
            // 
            // TextoCP
            // 
            this.TextoCP.Location = new System.Drawing.Point(203, 16);
            this.TextoCP.Name = "TextoCP";
            this.TextoCP.Size = new System.Drawing.Size(100, 20);
            this.TextoCP.TabIndex = 4;
            this.TextoCP.Text = "1";
            this.TextoCP.TextChanged += new System.EventHandler(this.Texto_TextChanged);
            // 
            // EtiquetaCP
            // 
            this.EtiquetaCP.AutoSize = true;
            this.EtiquetaCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtiquetaCP.ForeColor = System.Drawing.Color.Aquamarine;
            this.EtiquetaCP.Location = new System.Drawing.Point(12, 19);
            this.EtiquetaCP.Name = "EtiquetaCP";
            this.EtiquetaCP.Size = new System.Drawing.Size(150, 16);
            this.EtiquetaCP.TabIndex = 3;
            this.EtiquetaCP.Text = "Cantidad de particiones";
            // 
            // EtSumaPart
            // 
            this.EtSumaPart.AutoSize = true;
            this.EtSumaPart.BackColor = System.Drawing.Color.Red;
            this.EtSumaPart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtSumaPart.ForeColor = System.Drawing.Color.Aquamarine;
            this.EtSumaPart.Location = new System.Drawing.Point(23, 76);
            this.EtSumaPart.Name = "EtSumaPart";
            this.EtSumaPart.Size = new System.Drawing.Size(45, 16);
            this.EtSumaPart.TabIndex = 6;
            this.EtSumaPart.Text = "label1";
            // 
            // EtTamMemoria
            // 
            this.EtTamMemoria.AutoSize = true;
            this.EtTamMemoria.BackColor = System.Drawing.Color.Lime;
            this.EtTamMemoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EtTamMemoria.ForeColor = System.Drawing.Color.Black;
            this.EtTamMemoria.Location = new System.Drawing.Point(23, 105);
            this.EtTamMemoria.Name = "EtTamMemoria";
            this.EtTamMemoria.Size = new System.Drawing.Size(45, 16);
            this.EtTamMemoria.TabIndex = 7;
            this.EtTamMemoria.Text = "label2";
            // 
            // Tampart
            // 
            this.Tampart.AutoSize = true;
            this.Tampart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tampart.ForeColor = System.Drawing.Color.Aquamarine;
            this.Tampart.Location = new System.Drawing.Point(12, 50);
            this.Tampart.Name = "Tampart";
            this.Tampart.Size = new System.Drawing.Size(150, 16);
            this.Tampart.TabIndex = 8;
            this.Tampart.Text = "Cantidad de particiones";
            // 
            // DialogoPartIgtam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(319, 168);
            this.Controls.Add(this.Tampart);
            this.Controls.Add(this.EtTamMemoria);
            this.Controls.Add(this.EtSumaPart);
            this.Controls.Add(this.BotonEst);
            this.Controls.Add(this.TextoCP);
            this.Controls.Add(this.EtiquetaCP);
            this.Name = "DialogoPartIgtam";
            this.Text = "Cantidad de particiones";
            this.Load += new System.EventHandler(this.DialogoPlus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button BotonEst;
        public System.Windows.Forms.TextBox TextoCP;
        public System.Windows.Forms.Label EtiquetaCP;
        public System.Windows.Forms.Label EtSumaPart;
        public System.Windows.Forms.Label EtTamMemoria;
        public System.Windows.Forms.Label Tampart;
    }
}