namespace ToDoList
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ToDoGomb = new Button();
            ToDoCim = new Label();
            ToDoHozzaadasa = new Label();
            ToDoBekerese = new TextBox();
            PontokLabel = new Label();
            PontokMegtekinteseGomb = new Button();
            SuspendLayout();

            // 
            // ToDoGomb
            // 
            ToDoGomb.Location = new Point(221, 110);
            ToDoGomb.Name = "ToDoGomb";
            ToDoGomb.Size = new Size(164, 27);
            ToDoGomb.TabIndex = 2;
            ToDoGomb.Text = "Feladat hozzáadása";
            ToDoGomb.UseVisualStyleBackColor = true;
            // 
            // ToDoCim
            // 
            ToDoCim.AutoSize = true;
            ToDoCim.Location = new Point(345, 21);
            ToDoCim.Name = "ToDoCim";
            ToDoCim.Size = new Size(85, 20);
            ToDoCim.TabIndex = 3;
            ToDoCim.Text = "To-Do Lista";
            // 
            // ToDoHozzaadasa
            // 
            ToDoHozzaadasa.AutoSize = true;
            ToDoHozzaadasa.Location = new Point(48, 89);
            ToDoHozzaadasa.Name = "ToDoHozzaadasa";
            ToDoHozzaadasa.Size = new Size(160, 20);
            ToDoHozzaadasa.TabIndex = 4;
            ToDoHozzaadasa.Text = "Adj hozzá új feladatot!";
            // 
            // ToDoBekerese
            // 
            ToDoBekerese.Location = new Point(48, 128);
            ToDoBekerese.Name = "ToDoBekerese";
            ToDoBekerese.Size = new Size(167, 27);
            ToDoBekerese.TabIndex = 5;
            // 
            // PontokLabel
            // 
            PontokLabel.AutoSize = true;
            PontokLabel.Location = new Point(48, 200);
            PontokLabel.Name = "PontokLabel";
            PontokLabel.Size = new Size(200, 20);
            PontokLabel.TabIndex = 6;
            PontokLabel.Text = "Pontjaid: 0";
            // 
            // PontokMegtekinteseGomb
            // 
            PontokMegtekinteseGomb.Location = new Point(48, 230);
            PontokMegtekinteseGomb.Name = "PontokMegtekinteseGomb";
            PontokMegtekinteseGomb.Size = new Size(164, 27);
            PontokMegtekinteseGomb.TabIndex = 7;
            PontokMegtekinteseGomb.Text = "Pontok elköltése";
            PontokMegtekinteseGomb.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(869, 619);
            Controls.Add(PontokMegtekinteseGomb);
            Controls.Add(PontokLabel);
            Controls.Add(ToDoBekerese);
            Controls.Add(ToDoHozzaadasa);
            Controls.Add(ToDoCim);
            Controls.Add(ToDoGomb);
            Name = "Form1";
            Text = "To-Do Lista";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ToDoGomb;
        private Label ToDoCim;
        private Label ToDoHozzaadasa;
        private TextBox ToDoBekerese;
        private Label PontokLabel;
        private Button PontokMegtekinteseGomb;
    }
}
