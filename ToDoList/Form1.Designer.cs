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
            this.ToDoGomb = new System.Windows.Forms.Button();
            this.ToDoCim = new System.Windows.Forms.Label();
            this.ToDoHozzaadasa = new System.Windows.Forms.Label();
            this.ToDoBekerese = new System.Windows.Forms.TextBox();
            this.PontokLabel = new System.Windows.Forms.Label();
            this.PontokMegtekinteseGomb = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // ToDoCim
            // 
            this.ToDoCim.AutoSize = true;
            this.ToDoCim.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ToDoCim.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.ToDoCim.Location = new System.Drawing.Point(300, 20);
            this.ToDoCim.Name = "ToDoCim";
            this.ToDoCim.Size = new System.Drawing.Size(165, 41);
            this.ToDoCim.TabIndex = 0;
            this.ToDoCim.Text = "To-Do Lista";
            this.ToDoCim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // ToDoHozzaadasa
            // 
            this.ToDoHozzaadasa.AutoSize = true;
            this.ToDoHozzaadasa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ToDoHozzaadasa.Location = new System.Drawing.Point(40, 90);
            this.ToDoHozzaadasa.Name = "ToDoHozzaadasa";
            this.ToDoHozzaadasa.Size = new System.Drawing.Size(210, 28);
            this.ToDoHozzaadasa.TabIndex = 1;
            this.ToDoHozzaadasa.Text = "Adj hozzá új feladatot:";

            // 
            // ToDoBekerese
            // 
            this.ToDoBekerese.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ToDoBekerese.Location = new System.Drawing.Point(40, 130);
            this.ToDoBekerese.Name = "ToDoBekerese";
            this.ToDoBekerese.Size = new System.Drawing.Size(250, 30);
            this.ToDoBekerese.TabIndex = 2;

            // 
            // ToDoGomb
            // 
            this.ToDoGomb.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ToDoGomb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToDoGomb.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ToDoGomb.ForeColor = System.Drawing.Color.White;
            this.ToDoGomb.Location = new System.Drawing.Point(320, 128);
            this.ToDoGomb.Name = "ToDoGomb";
            this.ToDoGomb.Size = new System.Drawing.Size(200, 35);
            this.ToDoGomb.TabIndex = 3;
            this.ToDoGomb.Text = "Feladat hozzáadása";
            this.ToDoGomb.UseVisualStyleBackColor = false;

            // 
            // PontokLabel
            // 
            this.PontokLabel.AutoSize = true;
            this.PontokLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PontokLabel.Location = new System.Drawing.Point(40, 200);
            this.PontokLabel.Name = "PontokLabel";
            this.PontokLabel.Size = new System.Drawing.Size(95, 28);
            this.PontokLabel.TabIndex = 4;
            this.PontokLabel.Text = "Pontjaid: 0";

            // 
            // PontokMegtekinteseGomb
            // 
            this.PontokMegtekinteseGomb.BackColor = System.Drawing.Color.CornflowerBlue;
            this.PontokMegtekinteseGomb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PontokMegtekinteseGomb.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PontokMegtekinteseGomb.ForeColor = System.Drawing.Color.White;
            this.PontokMegtekinteseGomb.Location = new System.Drawing.Point(40, 240);
            this.PontokMegtekinteseGomb.Name = "PontokMegtekinteseGomb";
            this.PontokMegtekinteseGomb.Size = new System.Drawing.Size(200, 35);
            this.PontokMegtekinteseGomb.TabIndex = 5;
            this.PontokMegtekinteseGomb.Text = "Pontok elköltése";
            this.PontokMegtekinteseGomb.UseVisualStyleBackColor = false;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PontokMegtekinteseGomb);
            this.Controls.Add(this.PontokLabel);
            this.Controls.Add(this.ToDoGomb);
            this.Controls.Add(this.ToDoBekerese);
            this.Controls.Add(this.ToDoHozzaadasa);
            this.Controls.Add(this.ToDoCim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "To-Do Lista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button ToDoGomb;
        private System.Windows.Forms.Label ToDoCim;
        private System.Windows.Forms.Label ToDoHozzaadasa;
        private System.Windows.Forms.TextBox ToDoBekerese;
        private System.Windows.Forms.Label PontokLabel;
        private System.Windows.Forms.Button PontokMegtekinteseGomb;
    }
}
