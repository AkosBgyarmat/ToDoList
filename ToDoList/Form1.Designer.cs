namespace ToDoList
{
    partial class Form1
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
            ToDoCim = new Label();
            ToDoHozzaadasa = new Label();
            FeladatNehezsegiSzint = new ComboBox();
            ToDoBekerese = new TextBox();
            PontokLabel = new Label();
            exitButton = new Button();
            ToDoGomb = new Button();
            feladatokPanel = new FlowLayoutPanel();
            pictureBox = new PictureBox();
            VasarlasGomb = new Button();
            GeneraltKepGomb = new Button();
            MentesGomb = new Button();
            BetoltesGomb = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // ToDoCim
            // 
            ToDoCim.AutoSize = true;
            ToDoCim.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            ToDoCim.Location = new Point(300, 20);
            ToDoCim.Name = "ToDoCim";
            ToDoCim.Size = new Size(176, 41);
            ToDoCim.TabIndex = 0;
            ToDoCim.Text = "To-Do Lista";
            // 
            // ToDoHozzaadasa
            // 
            ToDoHozzaadasa.AutoSize = true;
            ToDoHozzaadasa.Font = new Font("Segoe UI", 12F);
            ToDoHozzaadasa.Location = new Point(40, 90);
            ToDoHozzaadasa.Name = "ToDoHozzaadasa";
            ToDoHozzaadasa.Size = new Size(230, 28);
            ToDoHozzaadasa.TabIndex = 1;
            ToDoHozzaadasa.Text = "Válassz nehézségi szintet:";
            // 
            // FeladatNehezsegiSzint
            // 
            FeladatNehezsegiSzint.DropDownStyle = ComboBoxStyle.DropDownList;
            FeladatNehezsegiSzint.Font = new Font("Segoe UI", 10F);
            FeladatNehezsegiSzint.FormattingEnabled = true;
            FeladatNehezsegiSzint.Items.AddRange(new object[] { "Könnyű", "Közepes", "Nehéz" });
            FeladatNehezsegiSzint.Location = new Point(40, 121);
            FeladatNehezsegiSzint.Name = "FeladatNehezsegiSzint";
            FeladatNehezsegiSzint.Size = new Size(250, 31);
            FeladatNehezsegiSzint.TabIndex = 2;
            // 
            // ToDoBekerese
            // 
            ToDoBekerese.Font = new Font("Segoe UI", 10F);
            ToDoBekerese.Location = new Point(40, 168);
            ToDoBekerese.Name = "ToDoBekerese";
            ToDoBekerese.PlaceholderText = "Add meg a feladatot...";
            ToDoBekerese.Size = new Size(250, 30);
            ToDoBekerese.TabIndex = 3;
            // 
            // PontokLabel
            // 
            PontokLabel.AutoSize = true;
            PontokLabel.Font = new Font("Segoe UI", 12F);
            PontokLabel.Location = new Point(613, 121);
            PontokLabel.Name = "PontokLabel";
            PontokLabel.Size = new Size(104, 28);
            PontokLabel.TabIndex = 5;
            PontokLabel.Text = "Pontjaid: 0";
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Red;
            exitButton.Location = new Point(267, 590);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(114, 37);
            exitButton.TabIndex = 7;
            exitButton.Text = "Kilépés";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            // 
            // ToDoGomb
            // 
            ToDoGomb.BackColor = Color.MediumSlateBlue;
            ToDoGomb.FlatStyle = FlatStyle.Flat;
            ToDoGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ToDoGomb.ForeColor = Color.White;
            ToDoGomb.Location = new Point(312, 121);
            ToDoGomb.Name = "ToDoGomb";
            ToDoGomb.Size = new Size(200, 35);
            ToDoGomb.TabIndex = 4;
            ToDoGomb.Text = "Feladat hozzáadása";
            ToDoGomb.UseVisualStyleBackColor = false;
            // 
            // feladatokPanel
            // 
            feladatokPanel.AutoScroll = true;
            feladatokPanel.BorderStyle = BorderStyle.FixedSingle;
            feladatokPanel.Location = new Point(40, 220);
            feladatokPanel.Name = "feladatokPanel";
            feladatokPanel.Size = new Size(300, 300);
            feladatokPanel.TabIndex = 8;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(400, 220);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(400, 300);
            pictureBox.TabIndex = 9;
            pictureBox.TabStop = false;
            // 
            // VasarlasGomb
            // 
            VasarlasGomb.BackColor = Color.LightCoral;
            VasarlasGomb.FlatStyle = FlatStyle.Flat;
            VasarlasGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            VasarlasGomb.ForeColor = Color.White;
            VasarlasGomb.Location = new Point(40, 540);
            VasarlasGomb.Name = "VasarlasGomb";
            VasarlasGomb.Size = new Size(200, 35);
            VasarlasGomb.TabIndex = 10;
            VasarlasGomb.Text = "Pontok elköltése";
            VasarlasGomb.UseVisualStyleBackColor = false;
            // 
            // GeneraltKepGomb
            // 
            GeneraltKepGomb.BackColor = Color.Green;
            GeneraltKepGomb.FlatStyle = FlatStyle.Flat;
            GeneraltKepGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            GeneraltKepGomb.ForeColor = Color.White;
            GeneraltKepGomb.Location = new Point(600, 540);
            GeneraltKepGomb.Name = "GeneraltKepGomb";
            GeneraltKepGomb.Size = new Size(200, 35);
            GeneraltKepGomb.TabIndex = 11;
            GeneraltKepGomb.Text = "Összkép generálása";
            GeneraltKepGomb.UseVisualStyleBackColor = false;
            // 
            // MentesGomb
            // 
            MentesGomb.BackColor = Color.CornflowerBlue;
            MentesGomb.FlatStyle = FlatStyle.Flat;
            MentesGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MentesGomb.ForeColor = Color.White;
            MentesGomb.Location = new Point(400, 540);
            MentesGomb.Name = "MentesGomb";
            MentesGomb.Size = new Size(150, 35);
            MentesGomb.TabIndex = 12;
            MentesGomb.Text = "Mentés";
            MentesGomb.UseVisualStyleBackColor = false;
            // 
            // BetoltesGomb
            // 
            BetoltesGomb.BackColor = Color.Gold;
            BetoltesGomb.FlatStyle = FlatStyle.Flat;
            BetoltesGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            BetoltesGomb.ForeColor = Color.Black;
            BetoltesGomb.Location = new Point(600, 590);
            BetoltesGomb.Name = "BetoltesGomb";
            BetoltesGomb.Size = new Size(200, 35);
            BetoltesGomb.TabIndex = 13;
            BetoltesGomb.Text = "Adatok betöltése";
            BetoltesGomb.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 650);
            Controls.Add(ToDoCim);
            Controls.Add(ToDoHozzaadasa);
            Controls.Add(FeladatNehezsegiSzint);
            Controls.Add(ToDoBekerese);
            Controls.Add(PontokLabel);
            Controls.Add(exitButton);
            Controls.Add(ToDoGomb);
            Controls.Add(feladatokPanel);
            Controls.Add(pictureBox);
            Controls.Add(VasarlasGomb);
            Controls.Add(GeneraltKepGomb);
            Controls.Add(MentesGomb);
            Controls.Add(BetoltesGomb);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "To-Do Lista";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label ToDoCim;
        private System.Windows.Forms.Label ToDoHozzaadasa;
        private System.Windows.Forms.ComboBox FeladatNehezsegiSzint;
        private System.Windows.Forms.TextBox ToDoBekerese;
        private System.Windows.Forms.Label PontokLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button ToDoGomb;
        private System.Windows.Forms.FlowLayoutPanel feladatokPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button VasarlasGomb;
        private System.Windows.Forms.Button GeneraltKepGomb;
        private System.Windows.Forms.Button MentesGomb;
        private System.Windows.Forms.Button BetoltesGomb;
    }
}
