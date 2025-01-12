using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Felhasználó összes pontja
        private FlowLayoutPanel feladatokPanel; // Feladatok megjelenítésére szolgáló panel
        private List<(string FeladatSzoveg, string NehezsegiSzint)> feladatok = new List<(string, string)>(); // Feladatok és nehézségi szintek tárolása
        private Random random = new Random(); // Véletlenszám generátor

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += FeladatHozzaadasa; // Feladat hozzáadására gomb
            //PontokMegtekinteseGomb.Click += PontokMegtekintese; // Pontok megtekintése gomb

            feladatokPanel = new FlowLayoutPanel
            {
                Location = new Point(48, 270),
                Size = new Size(400, 300),
                AutoScroll = true
            };
            Controls.Add(feladatokPanel);
        }

        private void FeladatHozzaadasa(object sender, EventArgs e)
        {
            string ujFeladat = ToDoBekerese.Text; // Feladat szövege
            string nehezsegiSzint = FeladatNehezsegiSzint.SelectedItem as string; // Kiválasztott nehézségi szint

            if (string.IsNullOrEmpty(nehezsegiSzint))
            {
                MessageBox.Show("Kérlek, válassz nehézségi szintet!");
                return;
            }

            if (!string.IsNullOrEmpty(ujFeladat))
            {
                feladatok.Add((ujFeladat, nehezsegiSzint)); // Új feladat mentése
                MessageBox.Show($"Feladat hozzáadva!\n{ujFeladat} ({nehezsegiSzint})");
                FeladatokMegjelenitese(); // Feladatok frissítése a megjelenítésben
                ToDoBekerese.Clear();
                FeladatNehezsegiSzint.SelectedIndex = -1; // Visszaállítjuk az alapértelmezett állapotra
            }
            else
            {
                MessageBox.Show("Adj meg egy érvényes feladatot!");
            }
        }

        private void FeladatokMegjelenitese()
        {
            feladatokPanel.Controls.Clear(); // Korábbi feladatok törlése a panelrõl

            foreach (var (FeladatSzoveg, NehezsegiSzint) in feladatok)
            {
                FlowLayoutPanel feladatSor = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true
                };

                Label feladatLabel = new Label
                {
                    Text = $"{FeladatSzoveg} ({NehezsegiSzint})",
                    AutoSize = true
                };

                RadioButton keszGomb = new RadioButton
                {
                    Text = "Kész",
                    AutoSize = true
                };
                keszGomb.CheckedChanged += (s, e) =>
                {
                    if (keszGomb.Checked)
                    {
                        int nyertPont = PontokKiszamitasa(NehezsegiSzint); // Pontok kiszámítása a nehézségi szint alapján
                        pontok += nyertPont;
                        MessageBox.Show($"Feladat kész! {nyertPont} pontot kaptál. Pontjaid: {pontok}");
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        feladatok.Remove((FeladatSzoveg, NehezsegiSzint)); // Feladat eltávolítása
                        FeladatokMegjelenitese(); // Lista frissítése
                    }
                };

                feladatSor.Controls.Add(feladatLabel);
                feladatSor.Controls.Add(keszGomb);

                feladatokPanel.Controls.Add(feladatSor);
            }
        }

        private int PontokKiszamitasa(string nehezsegiSzint)
        {
            return nehezsegiSzint switch
            {
                "Könnyû" => random.Next(1, 6), // 1-5 pont
                "Közepes" => random.Next(6, 16), // 6-15 pont
                "Nehéz" => random.Next(15, 26), // 15-25 pont
                _ => 0
            };
        }

        private void HatterSzinValtoztatas(object sender, EventArgs e)
        {
            using (ColorDialog szinDialogus = new ColorDialog())
            {
                if (szinDialogus.ShowDialog() == DialogResult.OK)
                {
                    int szinPontKoltseg = 10;

                    if (pontok >= szinPontKoltseg)
                    {
                        pontok -= szinPontKoltseg;
                        MessageBox.Show($"Új háttérszínt választottál! {szinPontKoltseg} pontot vontunk le. Pontjaid: {pontok}");
                        this.BackColor = szinDialogus.Color;
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                    }
                    else
                    {
                        MessageBox.Show("Nincs elég pontod a színválasztáshoz!");
                    }
                }
            }
        }

        private void PontokMegtekintese(object sender, EventArgs e)
        {
            MessageBox.Show($"Jelenlegi pontjaid: {pontok}");
        }

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
