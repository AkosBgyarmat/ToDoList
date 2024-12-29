using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public class SzigetElem
    {
        public string Nev { get; set; }
        public int PontKoltseg { get; set; }
        public Image Kep { get; set; }

        public SzigetElem(string nev, int pontKoltseg, Image kep)
        {
            Nev = nev;
            PontKoltseg = pontKoltseg;
            Kep = kep;
        }
    }

    public partial class Form1 : Form
    {
        private int pontok = 0; // Pontok sz�ma
        private List<string> feladatok = new List<string>(); // Feladatok list�ja
        private List<SzigetElem> szigetElemek;
        private FlowLayoutPanel feladatokPanel; // Panel a feladatok megjelen�t�s�re
        private Panel szigetPanel;

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += ToDoGomb_Click;
            PontokMegtekinteseGomb.Click += PontokMegtekinteseGomb_Click;

            // Panel inicializ�l�sa
            feladatokPanel = new FlowLayoutPanel();
            feladatokPanel.Location = new System.Drawing.Point(48, 270);
            feladatokPanel.Size = new System.Drawing.Size(400, 300);
            feladatokPanel.AutoScroll = true;
            Controls.Add(feladatokPanel);

            // Sziget panel inicializ�l�sa
            szigetPanel = new Panel
            {
                Size = new Size(400, 400),
                Location = new Point(500, 50),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightBlue
            };
            Controls.Add(szigetPanel);

            // Sziget elemek inicializ�l�sa
            szigetElemek = new List<SzigetElem>
            {
                new SzigetElem("Fa", 10, Properties.Resources.fa),
                new SzigetElem("Kunyh�", 50, Properties.Resources.kunyho),
                new SzigetElem("T�", 30, Properties.Resources.to)
            };
        }

        private void ToDoGomb_Click(object sender, EventArgs e)
        {
            // Feladat hozz�ad�sa
            string ujFeladat = ToDoBekerese.Text;

            if (!string.IsNullOrEmpty(ujFeladat) && ujFeladat != "Adj hozz� egy feladatot")
            {
                feladatok.Add(ujFeladat);
                MessageBox.Show($"Feladat hozz�adva!\n{ujFeladat}");
                FrissitFeladatokMegjelenitese();
                ToDoBekerese.Clear();
            }
            else
            {
                MessageBox.Show("Adj meg egy �rv�nyes feladatot!");
            }
        }

        private void FrissitFeladatokMegjelenitese()
        {
            feladatokPanel.Controls.Clear();

            foreach (var feladat in feladatok)
            {
                FlowLayoutPanel sorPanel = new FlowLayoutPanel();
                sorPanel.FlowDirection = FlowDirection.LeftToRight;
                sorPanel.AutoSize = true;

                Label feladatLabel = new Label();
                feladatLabel.Text = feladat;
                feladatLabel.AutoSize = true;

                RadioButton keszGomb = new RadioButton();
                keszGomb.Text = "K�sz";
                keszGomb.AutoSize = true;
                keszGomb.CheckedChanged += (s, e) => {
                    if (keszGomb.Checked)
                    {
                        Random rand = new Random();
                        int nyertPont = rand.Next(1, 16);
                        pontok += nyertPont;
                        MessageBox.Show($"Feladat k�sz! {nyertPont} pontot kapt�l. Pontjaid: {pontok}");
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        feladatok.Remove(feladat);
                        FrissitFeladatokMegjelenitese();
                    }
                };

                sorPanel.Controls.Add(feladatLabel);
                sorPanel.Controls.Add(keszGomb);

                feladatokPanel.Controls.Add(sorPanel);
            }
        }

        private void PontokMegtekinteseGomb_Click(object sender, EventArgs e)
        {
            // V�s�rl�si ablak megnyit�sa
            Form vasarlasAblak = new Form();
            vasarlasAblak.Text = "V�s�rl�sok";
            vasarlasAblak.Size = new System.Drawing.Size(350, 400);
            vasarlasAblak.BackColor = System.Drawing.Color.WhiteSmoke;

            FlowLayoutPanel vasarlasPanel = new FlowLayoutPanel();
            vasarlasPanel.Dock = DockStyle.Fill;
            vasarlasPanel.FlowDirection = FlowDirection.TopDown;
            vasarlasPanel.AutoScroll = true;
            vasarlasAblak.Controls.Add(vasarlasPanel);

            Label vasarlasCim = new Label();
            vasarlasCim.Text = "V�s�rl�si lehet�s�gek";
            vasarlasCim.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            vasarlasCim.AutoSize = true;
            vasarlasCim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            vasarlasPanel.Controls.Add(vasarlasCim);

            foreach (var elem in szigetElemek)
            {
                Button vasarlasGomb = new Button
                {
                    Text = $"{elem.Nev} ({elem.PontKoltseg} pont)",
                    AutoSize = true
                };
                vasarlasGomb.Click += (s, e) => Vasarlas(elem);
                vasarlasPanel.Controls.Add(vasarlasGomb);
            }

            vasarlasAblak.ShowDialog();
        }

        private void Vasarlas(SzigetElem elem)
        {
            if (pontok >= elem.PontKoltseg)
            {
                pontok -= elem.PontKoltseg;
                MessageBox.Show($"{elem.Nev} hozz�adva a szigetedhez! Pontjaid: {pontok}");
                PontokLabel.Text = $"Pontjaid: {pontok}";

                AddElemToSziget(elem);
            }
            else
            {
                MessageBox.Show("Nincs el�g pontod ehhez a v�s�rl�shoz!");
            }
        }

        private void AddElemToSziget(SzigetElem elem)
        {
            PictureBox elemKep = new PictureBox
            {
                Image = elem.Kep,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(new Random().Next(10, szigetPanel.Width - 50), new Random().Next(10, szigetPanel.Height - 50))
            };
            szigetPanel.Controls.Add(elemKep);
        }
    }
}
