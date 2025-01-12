using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Felhaszn�l� �sszes pontja
        private FlowLayoutPanel feladatokPanel; // Feladatok megjelen�t�s�re szolg�l� panel
        private List<(string FeladatSzoveg, string NehezsegiSzint)> feladatok = new List<(string, string)>(); // Feladatok �s neh�zs�gi szintek t�rol�sa
        private Random random = new Random(); // V�letlensz�m gener�tor

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += FeladatHozzaadasa; // Feladat hozz�ad�s�ra gomb
            //PontokMegtekinteseGomb.Click += PontokMegtekintese; // Pontok megtekint�se gomb

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
            string ujFeladat = ToDoBekerese.Text; // Feladat sz�vege
            string nehezsegiSzint = FeladatNehezsegiSzint.SelectedItem as string; // Kiv�lasztott neh�zs�gi szint

            if (string.IsNullOrEmpty(nehezsegiSzint))
            {
                MessageBox.Show("K�rlek, v�lassz neh�zs�gi szintet!");
                return;
            }

            if (!string.IsNullOrEmpty(ujFeladat))
            {
                feladatok.Add((ujFeladat, nehezsegiSzint)); // �j feladat ment�se
                MessageBox.Show($"Feladat hozz�adva!\n{ujFeladat} ({nehezsegiSzint})");
                FeladatokMegjelenitese(); // Feladatok friss�t�se a megjelen�t�sben
                ToDoBekerese.Clear();
                FeladatNehezsegiSzint.SelectedIndex = -1; // Vissza�ll�tjuk az alap�rtelmezett �llapotra
            }
            else
            {
                MessageBox.Show("Adj meg egy �rv�nyes feladatot!");
            }
        }

        private void FeladatokMegjelenitese()
        {
            feladatokPanel.Controls.Clear(); // Kor�bbi feladatok t�rl�se a panelr�l

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
                    Text = "K�sz",
                    AutoSize = true
                };
                keszGomb.CheckedChanged += (s, e) =>
                {
                    if (keszGomb.Checked)
                    {
                        int nyertPont = PontokKiszamitasa(NehezsegiSzint); // Pontok kisz�m�t�sa a neh�zs�gi szint alapj�n
                        pontok += nyertPont;
                        MessageBox.Show($"Feladat k�sz! {nyertPont} pontot kapt�l. Pontjaid: {pontok}");
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        feladatok.Remove((FeladatSzoveg, NehezsegiSzint)); // Feladat elt�vol�t�sa
                        FeladatokMegjelenitese(); // Lista friss�t�se
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
                "K�nny�" => random.Next(1, 6), // 1-5 pont
                "K�zepes" => random.Next(6, 16), // 6-15 pont
                "Neh�z" => random.Next(15, 26), // 15-25 pont
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
                        MessageBox.Show($"�j h�tt�rsz�nt v�lasztott�l! {szinPontKoltseg} pontot vontunk le. Pontjaid: {pontok}");
                        this.BackColor = szinDialogus.Color;
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                    }
                    else
                    {
                        MessageBox.Show("Nincs el�g pontod a sz�nv�laszt�shoz!");
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
