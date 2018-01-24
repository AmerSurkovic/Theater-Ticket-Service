namespace Zadaca2_Pozoriste
{
    partial class PrikaziPrograme_Form2
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
            this.Programi_groupBox1 = new System.Windows.Forms.GroupBox();
            this.InfoPrograma_groupBox2 = new System.Windows.Forms.GroupBox();
            this.InfoPredstave_button1 = new System.Windows.Forms.Button();
            this.Predstave_comboBox1 = new System.Windows.Forms.ComboBox();
            this.IzbrisiProgram_button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.KrajPrograma_textBox3 = new System.Windows.Forms.TextBox();
            this.PocetakPrograma_textBox2 = new System.Windows.Forms.TextBox();
            this.NazivPrograma_textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Programi_comboBox1 = new System.Windows.Forms.ComboBox();
            this.OdabirPredstave_errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.Programi_groupBox1.SuspendLayout();
            this.InfoPrograma_groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OdabirPredstave_errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // Programi_groupBox1
            // 
            this.Programi_groupBox1.Controls.Add(this.InfoPrograma_groupBox2);
            this.Programi_groupBox1.Controls.Add(this.label8);
            this.Programi_groupBox1.Controls.Add(this.label7);
            this.Programi_groupBox1.Controls.Add(this.Programi_comboBox1);
            this.Programi_groupBox1.Location = new System.Drawing.Point(12, 12);
            this.Programi_groupBox1.Name = "Programi_groupBox1";
            this.Programi_groupBox1.Size = new System.Drawing.Size(342, 261);
            this.Programi_groupBox1.TabIndex = 1;
            this.Programi_groupBox1.TabStop = false;
            this.Programi_groupBox1.Text = "Programi";
            // 
            // InfoPrograma_groupBox2
            // 
            this.InfoPrograma_groupBox2.Controls.Add(this.InfoPredstave_button1);
            this.InfoPrograma_groupBox2.Controls.Add(this.Predstave_comboBox1);
            this.InfoPrograma_groupBox2.Controls.Add(this.IzbrisiProgram_button1);
            this.InfoPrograma_groupBox2.Controls.Add(this.label6);
            this.InfoPrograma_groupBox2.Controls.Add(this.KrajPrograma_textBox3);
            this.InfoPrograma_groupBox2.Controls.Add(this.PocetakPrograma_textBox2);
            this.InfoPrograma_groupBox2.Controls.Add(this.NazivPrograma_textBox1);
            this.InfoPrograma_groupBox2.Controls.Add(this.label5);
            this.InfoPrograma_groupBox2.Controls.Add(this.label4);
            this.InfoPrograma_groupBox2.Controls.Add(this.label2);
            this.InfoPrograma_groupBox2.Controls.Add(this.label1);
            this.InfoPrograma_groupBox2.Location = new System.Drawing.Point(6, 43);
            this.InfoPrograma_groupBox2.Name = "InfoPrograma_groupBox2";
            this.InfoPrograma_groupBox2.Size = new System.Drawing.Size(330, 209);
            this.InfoPrograma_groupBox2.TabIndex = 1;
            this.InfoPrograma_groupBox2.TabStop = false;
            this.InfoPrograma_groupBox2.Text = "Informacije o odabranom programu:";
            // 
            // InfoPredstave_button1
            // 
            this.InfoPredstave_button1.Location = new System.Drawing.Point(139, 67);
            this.InfoPredstave_button1.Name = "InfoPredstave_button1";
            this.InfoPredstave_button1.Size = new System.Drawing.Size(166, 23);
            this.InfoPredstave_button1.TabIndex = 23;
            this.InfoPredstave_button1.Text = "Informacija o predstavi";
            this.InfoPredstave_button1.UseVisualStyleBackColor = true;
            this.InfoPredstave_button1.Click += new System.EventHandler(this.InfoPredstave_button1_Click);
            // 
            // Predstave_comboBox1
            // 
            this.Predstave_comboBox1.FormattingEnabled = true;
            this.Predstave_comboBox1.Location = new System.Drawing.Point(139, 43);
            this.Predstave_comboBox1.Name = "Predstave_comboBox1";
            this.Predstave_comboBox1.Size = new System.Drawing.Size(166, 21);
            this.Predstave_comboBox1.TabIndex = 22;
            this.Predstave_comboBox1.SelectedIndexChanged += new System.EventHandler(this.Predstave_comboBox1_SelectedIndexChanged);
            this.Predstave_comboBox1.Validating += new System.ComponentModel.CancelEventHandler(this.Predstave_comboBox1_Validating);
            // 
            // IzbrisiProgram_button1
            // 
            this.IzbrisiProgram_button1.Location = new System.Drawing.Point(90, 180);
            this.IzbrisiProgram_button1.Name = "IzbrisiProgram_button1";
            this.IzbrisiProgram_button1.Size = new System.Drawing.Size(127, 23);
            this.IzbrisiProgram_button1.TabIndex = 19;
            this.IzbrisiProgram_button1.Text = "Izbriši program";
            this.IzbrisiProgram_button1.UseVisualStyleBackColor = true;
            this.IzbrisiProgram_button1.Click += new System.EventHandler(this.IzbrisiProgram_button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 17;
            // 
            // KrajPrograma_textBox3
            // 
            this.KrajPrograma_textBox3.Location = new System.Drawing.Point(139, 122);
            this.KrajPrograma_textBox3.Name = "KrajPrograma_textBox3";
            this.KrajPrograma_textBox3.Size = new System.Drawing.Size(166, 20);
            this.KrajPrograma_textBox3.TabIndex = 16;
            // 
            // PocetakPrograma_textBox2
            // 
            this.PocetakPrograma_textBox2.Location = new System.Drawing.Point(139, 96);
            this.PocetakPrograma_textBox2.Name = "PocetakPrograma_textBox2";
            this.PocetakPrograma_textBox2.Size = new System.Drawing.Size(166, 20);
            this.PocetakPrograma_textBox2.TabIndex = 15;
            // 
            // NazivPrograma_textBox1
            // 
            this.NazivPrograma_textBox1.Location = new System.Drawing.Point(139, 18);
            this.NazivPrograma_textBox1.Name = "NazivPrograma_textBox1";
            this.NazivPrograma_textBox1.Size = new System.Drawing.Size(166, 20);
            this.NazivPrograma_textBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Kraj programa:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Početak programa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Predstave:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Naziv programa:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "ili odaberite program iz padajućeg menija.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Unesite ključnu riječ za pretragu programa,";
            // 
            // Programi_comboBox1
            // 
            this.Programi_comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Programi_comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Programi_comboBox1.FormattingEnabled = true;
            this.Programi_comboBox1.Location = new System.Drawing.Point(6, 19);
            this.Programi_comboBox1.Name = "Programi_comboBox1";
            this.Programi_comboBox1.Size = new System.Drawing.Size(311, 21);
            this.Programi_comboBox1.TabIndex = 0;
            this.Programi_comboBox1.SelectedIndexChanged += new System.EventHandler(this.Programi_comboBox1_SelectedIndexChanged);
            // 
            // OdabirPredstave_errorProvider1
            // 
            this.OdabirPredstave_errorProvider1.ContainerControl = this;
            // 
            // PrikaziPrograme_Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 285);
            this.Controls.Add(this.Programi_groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PrikaziPrograme_Form2";
            this.Text = "Prikaz programa";
            this.Programi_groupBox1.ResumeLayout(false);
            this.Programi_groupBox1.PerformLayout();
            this.InfoPrograma_groupBox2.ResumeLayout(false);
            this.InfoPrograma_groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OdabirPredstave_errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Programi_groupBox1;
        private System.Windows.Forms.GroupBox InfoPrograma_groupBox2;
        private System.Windows.Forms.ComboBox Predstave_comboBox1;
        private System.Windows.Forms.Button IzbrisiProgram_button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox KrajPrograma_textBox3;
        private System.Windows.Forms.TextBox PocetakPrograma_textBox2;
        private System.Windows.Forms.TextBox NazivPrograma_textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Programi_comboBox1;
        private System.Windows.Forms.Button InfoPredstave_button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider OdabirPredstave_errorProvider1;


    }
}