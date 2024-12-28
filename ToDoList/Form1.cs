using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Pontok száma
        private List<string> feladatok = new List<string>(); // Feladatok listája
        private FlowLayoutPanel feladatokPanel; // Panel a feladatok megjelenítésére

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
            vasarlasAblak.Size = new System.Drawing.Size(300, 300);

            FlowLayoutPanel vasarlasPanel = new FlowLayoutPanel();
            vasarlasPanel.Dock = DockStyle.Fill;
            vasarlasPanel.FlowDirection = FlowDirection.TopDown;
            vasarlasPanel.AutoScroll = true;
            vasarlasAblak.Controls.Add(vasarlasPanel);

            // Vásárlási lehetõség: Háttérszínek
            string[] szinek = { "Kék", "Zöld", "Sárga", "Rózsaszín" };
            System.Drawing.Color[] szinErtekek = { System.Drawing.Color.LightBlue, System.Drawing.Color.LightGreen, System.Drawing.Color.LightYellow, System.Drawing.Color.LightPink };

            for (int i = 0; i < szinek.Length; i++)
            {
                Button szinGomb = new Button();
                szinGomb.Text = $"{szinek[i]} háttér (50 pont)";
                int szinIndex = i; // Rögzítjük az aktuális indexet a lambda kifejezéshez
                szinGomb.Click += (s, e) => {
                    if (pontok >= 50)
                    {
                        pontok -= 50;
                        this.BackColor = szinErtekek[szinIndex];
                        MessageBox.Show($"A háttérszín megváltozott: {szinek[szinIndex]}! Pontjaid: {pontok}");
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        vasarlasAblak.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nincs elég pontod ehhez a vásárláshoz!");
                    }
                };
                vasarlasPanel.Controls.Add(szinGomb);
            }

            // Vásárlási lehetõség 2: Speciális funkció aktiválása
            Button vasarlas2 = new Button();
            vasarlas2.Text = "Speciális funkció aktiválása (100 pont)";
            vasarlas2.Click += (s, e) => {
                if (pontok >= 100)
                {
                    pontok -= 100;
                    MessageBox.Show("Speciális funkció aktiválva!");
                    PontokLabel.Text = $"Pontjaid: {pontok}";
                    vasarlasAblak.Close();
                }
                else
                {
                    MessageBox.Show("Nincs elég pontod ehhez a vásárláshoz!");
                }
            };

            vasarlasPanel.Controls.Add(vasarlas2);

            vasarlasAblak.ShowDialog();
        }
    }
}
