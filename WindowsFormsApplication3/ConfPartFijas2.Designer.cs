namespace WindowsFormsApplication3
{
    partial class ConfPartFijas2
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
            this.label1 = new System.Windows.Forms.Label();
            this.Cantpart = new System.Windows.Forms.TextBox();
            this.Tampartic = new System.Windows.Forms.DataGridView();
            this.Id_Particion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tam_Particion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Est_tamanios = new System.Windows.Forms.Button();
            this.SumaAct = new System.Windows.Forms.Label();
            this.Tamactual = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Tampartic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightCyan;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cantidad de particiones";
            // 
            // Cantpart
            // 
            this.Cantpart.Location = new System.Drawing.Point(136, 31);
            this.Cantpart.Name = "Cantpart";
            this.Cantpart.Size = new System.Drawing.Size(119, 20);
            this.Cantpart.TabIndex = 2;
            this.Cantpart.TextChanged += new System.EventHandler(this.Cantpart_TextChanged);
            // 
            // Tampartic
            // 
            this.Tampartic.AllowUserToDeleteRows = false;
            this.Tampartic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tampartic.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Particion,
            this.Tam_Particion});
            this.Tampartic.Location = new System.Drawing.Point(12, 66);
            this.Tampartic.Name = "Tampartic";
            this.Tampartic.Size = new System.Drawing.Size(240, 150);
            this.Tampartic.TabIndex = 3;
            this.Tampartic.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tampartic_CellValueChanged);
            // 
            // Id_Particion
            // 
            this.Id_Particion.HeaderText = "Id_Particion";
            this.Id_Particion.Name = "Id_Particion";
            this.Id_Particion.ReadOnly = true;
            this.Id_Particion.Width = 70;
            // 
            // Tam_Particion
            // 
            this.Tam_Particion.HeaderText = "Tamaño de particion";
            this.Tam_Particion.Name = "Tam_Particion";
            // 
            // Est_tamanios
            // 
            this.Est_tamanios.BackColor = System.Drawing.Color.RoyalBlue;
            this.Est_tamanios.ForeColor = System.Drawing.Color.LightCyan;
            this.Est_tamanios.Location = new System.Drawing.Point(67, 282);
            this.Est_tamanios.Name = "Est_tamanios";
            this.Est_tamanios.Size = new System.Drawing.Size(131, 23);
            this.Est_tamanios.TabIndex = 4;
            this.Est_tamanios.Text = "Establecer tamaños";
            this.Est_tamanios.UseVisualStyleBackColor = false;
            this.Est_tamanios.Click += new System.EventHandler(this.Est_tamanios_Click);
            // 
            // SumaAct
            // 
            this.SumaAct.AutoSize = true;
            this.SumaAct.BackColor = System.Drawing.Color.Red;
            this.SumaAct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SumaAct.Location = new System.Drawing.Point(12, 249);
            this.SumaAct.Name = "SumaAct";
            this.SumaAct.Size = new System.Drawing.Size(132, 16);
            this.SumaAct.TabIndex = 5;
            this.SumaAct.Text = "Suma de tamaños";
            // 
            // Tamactual
            // 
            this.Tamactual.AutoSize = true;
            this.Tamactual.BackColor = System.Drawing.Color.Lime;
            this.Tamactual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tamactual.Location = new System.Drawing.Point(12, 219);
            this.Tamactual.Name = "Tamactual";
            this.Tamactual.Size = new System.Drawing.Size(197, 16);
            this.Tamactual.TabIndex = 6;
            this.Tamactual.Text = "Tamaño actual de memoria";
            // 
            // ConfPartFijas2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(279, 317);
            this.Controls.Add(this.Tamactual);
            this.Controls.Add(this.SumaAct);
            this.Controls.Add(this.Est_tamanios);
            this.Controls.Add(this.Tampartic);
            this.Controls.Add(this.Cantpart);
            this.Controls.Add(this.label1);
            this.Name = "ConfPartFijas2";
            this.Text = "Configuracion de particiones fijas";
            ((System.ComponentModel.ISupportInitialize)(this.Tampartic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Cantpart;
        private System.Windows.Forms.DataGridView Tampartic;
        private System.Windows.Forms.Button Est_tamanios;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Particion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tam_Particion;
        private System.Windows.Forms.Label SumaAct;
        private System.Windows.Forms.Label Tamactual;
    }
}