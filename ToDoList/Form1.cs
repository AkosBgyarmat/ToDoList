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
        private int pontok = 0; // Pontok száma
        private List<string> feladatok = new List<string>(); // Feladatok listája
        private List<SzigetElem> szigetElemek;
        private FlowLayoutPanel feladatokPanel; // Panel a feladatok megjelenítésére
        private Panel szigetPanel;

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += ToDoGomb_Click;
            PontokMegtekinteseGomb.Click += PontokMegtekinteseGomb_Click;

            // Panel inicializálása
            feladatokPanel = new FlowLayoutPanel();
            feladatokPanel.Location = new System.Drawing.Point(48, 270);
            feladatokPanel.Size = new System.Drawing.Size(400, 300);
            feladatokPanel.AutoScroll = true;
            Controls.Add(feladatokPanel);

            // Sziget panel inicializálása
            szigetPanel = new Panel
            {
                Size = new Size(400, 400),
                Location = new Point(500, 50),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightBlue
            };
            Controls.Add(szigetPanel);

            // Sziget elemek inicializálása
            szigetElemek = new List<SzigetElem>
            {
                new SzigetElem("Fa", 10, Properties.Resources.fa),
                new SzigetElem("Kunyhó", 50, Properties.Resources.kunyho),
                new SzigetElem("Tó", 30, Properties.Resources.to)
            };
        }

        private void ToDoGomb_Click(object sender, EventArgs e)
        {
            // Feladat hozzáadása
            string ujFeladat = ToDoBekerese.Text;

            if (!string.IsNullOrEmpty(ujFeladat) && ujFeladat != "Adj hozzá egy feladatot")
            {
                feladatok.Add(ujFeladat);
                MessageBox.Show($"Feladat hozzáadva!\n{ujFeladat}");
                FrissitFeladatokMegjelenitese();
                ToDoBekerese.Clear();
            }
            else
            {
                MessageBox.Show("Adj meg egy érvényes feladatot!");
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
                keszGomb.Text = "Kész";
                keszGomb.AutoSize = true;
                keszGomb.CheckedChanged += (s, e) => {
                    if (keszGomb.Checked)
                    {
                        Random rand = new Random();
                        int nyertPont = rand.Next(1, 16);
                        pontok += nyertPont;
                        MessageBox.Show($"Feladat kész! {nyertPont} pontot kaptál. Pontjaid: {pontok}");
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
            // Vásárlási ablak megnyitása
            Form vasarlasAblak = new Form();
            vasarlasAblak.Text = "Vásárlások";
            vasarlasAblak.Size = new System.Drawing.Size(350, 400);
            vasarlasAblak.BackColor = System.Drawing.Color.WhiteSmoke;

            FlowLayoutPanel vasarlasPanel = new FlowLayoutPanel();
            vasarlasPanel.Dock = DockStyle.Fill;
            vasarlasPanel.FlowDirection = FlowDirection.TopDown;
            vasarlasPanel.AutoScroll = true;
            vasarlasAblak.Controls.Add(vasarlasPanel);

            Label vasarlasCim = new Label();
            vasarlasCim.Text = "Vásárlási lehetõségek";
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
                MessageBox.Show($"{elem.Nev} hozzáadva a szigetedhez! Pontjaid: {pontok}");
                PontokLabel.Text = $"Pontjaid: {pontok}";

                AddElemToSziget(elem);
            }
            else
            {
                MessageBox.Show("Nincs elég pontod ehhez a vásárláshoz!");
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
