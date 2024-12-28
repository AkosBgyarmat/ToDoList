using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Pontok sz�ma
        private List<string> feladatok = new List<string>(); // Feladatok list�ja
        private FlowLayoutPanel feladatokPanel; // Panel a feladatok megjelen�t�s�re

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
            vasarlasAblak.Size = new System.Drawing.Size(300, 300);

            FlowLayoutPanel vasarlasPanel = new FlowLayoutPanel();
            vasarlasPanel.Dock = DockStyle.Fill;
            vasarlasPanel.FlowDirection = FlowDirection.TopDown;
            vasarlasPanel.AutoScroll = true;
            vasarlasAblak.Controls.Add(vasarlasPanel);

            // V�s�rl�si lehet�s�g: H�tt�rsz�nek
            string[] szinek = { "K�k", "Z�ld", "S�rga", "R�zsasz�n" };
            System.Drawing.Color[] szinErtekek = { System.Drawing.Color.LightBlue, System.Drawing.Color.LightGreen, System.Drawing.Color.LightYellow, System.Drawing.Color.LightPink };

            for (int i = 0; i < szinek.Length; i++)
            {
                Button szinGomb = new Button();
                szinGomb.Text = $"{szinek[i]} h�tt�r (50 pont)";
                int szinIndex = i; // R�gz�tj�k az aktu�lis indexet a lambda kifejez�shez
                szinGomb.Click += (s, e) => {
                    if (pontok >= 50)
                    {
                        pontok -= 50;
                        this.BackColor = szinErtekek[szinIndex];
                        MessageBox.Show($"A h�tt�rsz�n megv�ltozott: {szinek[szinIndex]}! Pontjaid: {pontok}");
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        vasarlasAblak.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nincs el�g pontod ehhez a v�s�rl�shoz!");
                    }
                };
                vasarlasPanel.Controls.Add(szinGomb);
            }

            // V�s�rl�si lehet�s�g 2: Speci�lis funkci� aktiv�l�sa
            Button vasarlas2 = new Button();
            vasarlas2.Text = "Speci�lis funkci� aktiv�l�sa (100 pont)";
            vasarlas2.Click += (s, e) => {
                if (pontok >= 100)
                {
                    pontok -= 100;
                    MessageBox.Show("Speci�lis funkci� aktiv�lva!");
                    PontokLabel.Text = $"Pontjaid: {pontok}";
                    vasarlasAblak.Close();
                }
                else
                {
                    MessageBox.Show("Nincs el�g pontod ehhez a v�s�rl�shoz!");
                }
            };

            vasarlasPanel.Controls.Add(vasarlas2);

            vasarlasAblak.ShowDialog();
        }
    }
}
