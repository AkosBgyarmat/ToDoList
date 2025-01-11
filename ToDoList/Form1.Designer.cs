namespace ToDoList
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            ToDoCim = new Label();
            ToDoHozzaadasa = new Label();
            FeladatNehezsegiSzint = new ComboBox();
            ToDoBekerese = new TextBox();
            ToDoGomb = new Button();
            PontokLabel = new Label();
            PontokMegtekinteseGomb = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // ToDoCim
            // 
            ToDoCim.AutoSize = true;
            ToDoCim.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            ToDoCim.ForeColor = Color.DarkSlateBlue;
            ToDoCim.Location = new Point(300, 20);
            ToDoCim.Name = "ToDoCim";
            ToDoCim.Size = new Size(176, 41);
            ToDoCim.TabIndex = 0;
            ToDoCim.Text = "To-Do Lista";
            ToDoCim.TextAlign = ContentAlignment.MiddleCenter;
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
            FeladatNehezsegiSzint.Location = new Point(40, 130);
            FeladatNehezsegiSzint.Name = "FeladatNehezsegiSzint";
            FeladatNehezsegiSzint.Size = new Size(250, 31);
            FeladatNehezsegiSzint.TabIndex = 2;
            // 
            // ToDoBekerese
            // 
            ToDoBekerese.Font = new Font("Segoe UI", 10F);
            ToDoBekerese.Location = new Point(40, 180);
            ToDoBekerese.Name = "ToDoBekerese";
            ToDoBekerese.PlaceholderText = "Add meg a feladatot...";
            ToDoBekerese.Size = new Size(250, 30);
            ToDoBekerese.TabIndex = 3;
            // 
            // ToDoGomb
            // 
            ToDoGomb.BackColor = Color.MediumSlateBlue;
            ToDoGomb.FlatStyle = FlatStyle.Flat;
            ToDoGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ToDoGomb.ForeColor = Color.White;
            ToDoGomb.Location = new Point(320, 180);
            ToDoGomb.Name = "ToDoGomb";
            ToDoGomb.Size = new Size(200, 35);
            ToDoGomb.TabIndex = 4;
            ToDoGomb.Text = "Feladat hozzáadása";
            ToDoGomb.UseVisualStyleBackColor = false;
            // 
            // PontokLabel
            // 
            PontokLabel.AutoSize = true;
            PontokLabel.Font = new Font("Segoe UI", 12F);
            PontokLabel.Location = new Point(330, 133);
            PontokLabel.Name = "PontokLabel";
            PontokLabel.Size = new Size(104, 28);
            PontokLabel.TabIndex = 5;
            PontokLabel.Text = "Pontjaid: 0";
            // 
            // PontokMegtekinteseGomb
            // 
            PontokMegtekinteseGomb.BackColor = Color.CornflowerBlue;
            PontokMegtekinteseGomb.FlatStyle = FlatStyle.Flat;
            PontokMegtekinteseGomb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            PontokMegtekinteseGomb.ForeColor = Color.White;
            PontokMegtekinteseGomb.Location = new Point(526, 180);
            PontokMegtekinteseGomb.Name = "PontokMegtekinteseGomb";
            PontokMegtekinteseGomb.Size = new Size(200, 35);
            PontokMegtekinteseGomb.TabIndex = 6;
            PontokMegtekinteseGomb.Text = "Pontok lekérdezése";
            PontokMegtekinteseGomb.UseVisualStyleBackColor = false;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(320, 242);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(200, 29);
            exitButton.TabIndex = 7;
            exitButton.Text = "Kilépés";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(exitButton);
            Controls.Add(PontokMegtekinteseGomb);
            Controls.Add(PontokLabel);
            Controls.Add(ToDoGomb);
            Controls.Add(ToDoBekerese);
            Controls.Add(FeladatNehezsegiSzint);
            Controls.Add(ToDoHozzaadasa);
            Controls.Add(ToDoCim);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "To-Do Lista";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label ToDoCim;
        private System.Windows.Forms.Label ToDoHozzaadasa;
        private System.Windows.Forms.ComboBox FeladatNehezsegiSzint;
        private System.Windows.Forms.TextBox ToDoBekerese;
        private System.Windows.Forms.Button ToDoGomb;
        private System.Windows.Forms.Label PontokLabel;
        private System.Windows.Forms.Button PontokMegtekinteseGomb;
        private Button exitButton;
    }
}
