namespace WindowsFormsApplication3
{
    partial class Colas_multinivel
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
            this.CantidadC = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TipoC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Datoscolas = new System.Windows.Forms.DataGridView();
            this.AColas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Est_Conf = new System.Windows.Forms.Button();
            this.Id_Cola = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Cola = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alg_Plan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.QuantumSoportado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxQuant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Datoscolas)).BeginInit();
            this.SuspendLayout();
            // 
            // CantidadC
            // 
            this.CantidadC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CantidadC.FormattingEnabled = true;
            this.CantidadC.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.CantidadC.Location = new System.Drawing.Point(243, 20);
            this.CantidadC.Name = "CantidadC";
            this.CantidadC.Size = new System.Drawing.Size(121, 21);
            this.CantidadC.TabIndex = 0;
            this.CantidadC.SelectedIndexChanged += new System.EventHandler(this.CantidadC_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cantidad de colas";
            // 
            // TipoC
            // 
            this.TipoC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoC.FormattingEnabled = true;
            this.TipoC.Items.AddRange(new object[] {
            "Retroalimentadas",
            "No retroalimentadas"});
            this.TipoC.Location = new System.Drawing.Point(243, 59);
            this.TipoC.Name = "TipoC";
            this.TipoC.Size = new System.Drawing.Size(121, 21);
            this.TipoC.TabIndex = 2;
            this.TipoC.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Aquamarine;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tipo";
            // 
            // Datoscolas
            // 
            this.Datoscolas.AllowUserToAddRows = false;
            this.Datoscolas.AllowUserToDeleteRows = false;
            this.Datoscolas.AllowUserToResizeColumns = false;
            this.Datoscolas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Datoscolas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.Datoscolas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Datoscolas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Cola,
            this.Nombre_Cola,
            this.Alg_Plan,
            this.QuantumSoportado,
            this.MaxQuant});
            this.Datoscolas.Location = new System.Drawing.Point(12, 139);
            this.Datoscolas.Name = "Datoscolas";
            this.Datoscolas.Size = new System.Drawing.Size(350, 150);
            this.Datoscolas.TabIndex = 4;
            this.Datoscolas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datoscolas_CellEnter);
            this.Datoscolas.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Datoscolas_EditingControlShowing);
            // 
            // AColas
            // 
            this.AColas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AColas.FormattingEnabled = true;
            this.AColas.Items.AddRange(new object[] {
            "Apropiativa",
            "No apropiativa"});
            this.AColas.Location = new System.Drawing.Point(243, 100);
            this.AColas.Name = "AColas";
            this.AColas.Size = new System.Drawing.Size(121, 21);
            this.AColas.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Aquamarine;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Prioridad entre colas";
            // 
            // Est_Conf
            // 
            this.Est_Conf.BackColor = System.Drawing.Color.RoyalBlue;
            this.Est_Conf.ForeColor = System.Drawing.Color.LightCyan;
            this.Est_Conf.Location = new System.Drawing.Point(117, 296);
            this.Est_Conf.Name = "Est_Conf";
            this.Est_Conf.Size = new System.Drawing.Size(139, 23);
            this.Est_Conf.TabIndex = 7;
            this.Est_Conf.Text = "Establecer configuracion";
            this.Est_Conf.UseVisualStyleBackColor = false;
            this.Est_Conf.Click += new System.EventHandler(this.Est_Conf_Click);
            // 
            // Id_Cola
            // 
            this.Id_Cola.HeaderText = "Id_Cola";
            this.Id_Cola.Name = "Id_Cola";
            this.Id_Cola.ReadOnly = true;
            this.Id_Cola.Width = 68;
            // 
            // Nombre_Cola
            // 
            this.Nombre_Cola.HeaderText = "Nombre_Cola";
            this.Nombre_Cola.Name = "Nombre_Cola";
            this.Nombre_Cola.Width = 96;
            // 
            // Alg_Plan
            // 
            this.Alg_Plan.HeaderText = "Alg. Planif";
            this.Alg_Plan.Items.AddRange(new object[] {
            "FCFS",
            "SJF",
            "SRTF",
            "Round Robin"});
            this.Alg_Plan.Name = "Alg_Plan";
            this.Alg_Plan.Width = 60;
            // 
            // QuantumSoportado
            // 
            this.QuantumSoportado.HeaderText = "Quantum";
            this.QuantumSoportado.Name = "QuantumSoportado";
            this.QuantumSoportado.ReadOnly = true;
            this.QuantumSoportado.Visible = false;
            this.QuantumSoportado.Width = 75;
            // 
            // MaxQuant
            // 
            this.MaxQuant.HeaderText = "Max Quantum";
            this.MaxQuant.Name = "MaxQuant";
            this.MaxQuant.ReadOnly = true;
            this.MaxQuant.Visible = false;
            this.MaxQuant.Width = 98;
            // 
            // Colas_multinivel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(374, 331);
            this.Controls.Add(this.Est_Conf);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AColas);
            this.Controls.Add(this.Datoscolas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TipoC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CantidadC);
            this.MaximumSize = new System.Drawing.Size(390, 370);
            this.MinimumSize = new System.Drawing.Size(390, 370);
            this.Name = "Colas_multinivel";
            this.Text = "Colas multinivel";
            this.Load += new System.EventHandler(this.Colas_multinivel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Datoscolas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CantidadC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TipoC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView Datoscolas;
        private System.Windows.Forms.ComboBox AColas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Est_Conf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Cola;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Cola;
        private System.Windows.Forms.DataGridViewComboBoxColumn Alg_Plan;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantumSoportado;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxQuant;
    }
}