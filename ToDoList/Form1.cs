using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0;
        private List<(string FeladatSzoveg, RadioButton KeszGomb)> feladatok = new List<(string, RadioButton)>();
        private List<(string Nev, string FajlNev, int Ar)> megvasaroltKepek = new List<(string, string, int)>();

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += FeladatHozzaadasa;
            VasarlasGomb.Click += VasarlasiAblakMegnyitasa;
            GeneraltKepGomb.Click += GeneraltKepMegjelenitese;
        }

        private void FeladatHozzaadasa(object sender, EventArgs e)
        {
            if (feladatok.Count >= 10)
            {
                MessageBox.Show("El�bb v�gezz el feladatokat, hogy �jakat vehess fel!");
                return;
            }

            string ujFeladat = ToDoBekerese.Text;
            string nehezsegiSzint = FeladatNehezsegiSzint.SelectedItem as string;

            if (string.IsNullOrEmpty(nehezsegiSzint))
            {
                MessageBox.Show("K�rlek, v�lassz neh�zs�gi szintet!");
                return;
            }

            if (!string.IsNullOrEmpty(ujFeladat))
            {
                RadioButton keszGomb = new RadioButton
                {
                    Text = $"{ujFeladat} ({nehezsegiSzint})",
                    AutoSize = true
                };

                keszGomb.CheckedChanged += (s, e) =>
                {
                    if (keszGomb.Checked)
                    {
                        feladatok.Remove((ujFeladat, keszGomb));
                        feladatokPanel.Controls.Remove(keszGomb);
                        pontok += PontokKiszamitasa(nehezsegiSzint);
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                        MessageBox.Show($"Feladat k�sz! Pontjaid: {pontok}");
                    }
                };

                feladatok.Add((ujFeladat, keszGomb));
                feladatokPanel.Controls.Add(keszGomb);

                ToDoBekerese.Clear();
                FeladatNehezsegiSzint.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Adj meg egy �rv�nyes feladatot!");
            }
        }

        private int PontokKiszamitasa(string nehezsegiSzint)
        {
            return nehezsegiSzint switch
            {
                "K�nny�" => new Random().Next(1, 6),
                "K�zepes" => new Random().Next(6, 16),
                "Neh�z" => new Random().Next(15, 26),
                _ => 0
            };
        }

        private void VasarlasiAblakMegnyitasa(object sender, EventArgs e)
        {
            Form vasarlasiAblak = new Form
            {
                Text = "K�pek v�s�rl�sa",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            FlowLayoutPanel kepekPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            vasarlasiAblak.Controls.Add(kepekPanel);

            List<(string Nev, string FajlNev, int Ar)> kepek = new List<(string, string, int)>
            {
                ("F�cska", "C:\\Users\\User\\Desktop\\f�cskaJo.jpg", 20),
                ("H�zik�", "C:\\Users\\User\\Desktop\\hazikoJo.jpg", 30),
                ("Kutya", "C:\\Users\\User\\Desktop\\kutyaJo.jpg", 25)
            };

            foreach (var (Nev, FajlNev, Ar) in kepek)
            {
                FlowLayoutPanel kepSor = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true
                };

                Label kepLabel = new Label
                {
                    Text = $"{Nev} - {Ar} pont",
                    AutoSize = true
                };

                Button megveszGomb = new Button
                {
                    Text = "V�s�rl�s",
                    AutoSize = true
                };
                megveszGomb.Click += (s, e) => KepVasarlas(FajlNev, Nev, Ar);

                kepSor.Controls.Add(kepLabel);
                kepSor.Controls.Add(megveszGomb);
                kepekPanel.Controls.Add(kepSor);
            }

            vasarlasiAblak.ShowDialog();
        }

        private void KepVasarlas(string kepFajl, string nev, int ar)
        {
            if (pontok >= ar)
            {
                if (!File.Exists(kepFajl))
                {
                    MessageBox.Show($"A f�jl nem tal�lhat�: {kepFajl}");
                    return;
                }

                pontok -= ar;
                PontokLabel.Text = $"Pontjaid: {pontok}";
                megvasaroltKepek.Add((nev, kepFajl, ar));

                try
                {
                    pictureBox.Image = Image.FromFile(kepFajl);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nem siker�lt bet�lteni a k�pet: {ex.Message}");
                }

                MessageBox.Show($"Sikeresen megv�s�roltad a(z) {nev} k�pet!");
            }
            else
            {
                MessageBox.Show("Nincs el�g pontod a k�p megv�s�rl�s�hoz!");
            }
        }

        private void GeneraltKepMegjelenitese(object sender, EventArgs e)



        {
            if (megvasaroltKepek.Count == 0)
            {
                MessageBox.Show("Nincs el�rhet� k�p az �sszk�p gener�l�s�hoz!");
                return;
            }

            int width = 200 * megvasaroltKepek.Count;
            int height = 200;
            Bitmap osszkep = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(osszkep))
            {
                g.Clear(Color.White);

                int x = 0;
                foreach (var (_, fajlNev, _) in megvasaroltKepek)
                {
                    using (Image img = Image.FromFile(fajlNev))
                    {
                        g.DrawImage(img, x, 0, 200, 200);
                        x += 200;
                    }
                }
            }

            pictureBox.Image = osszkep;
            MessageBox.Show("Az �sszk�p elk�sz�lt!");
        }

        private void MentesGomb_Click(object sender, EventArgs e)
        {
            try
            {
                string mentesiUtvonal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eredmenyek.txt");

                using (StreamWriter writer = new StreamWriter(mentesiUtvonal))
                {
                    writer.WriteLine($"Pontok: {pontok}");
                    writer.WriteLine("Feladatok:");
                    foreach (var (FeladatSzoveg, _) in feladatok)
                    {
                        writer.WriteLine($"- {FeladatSzoveg}");
                    }

                    writer.WriteLine("Megv�s�rolt k�pek:");
                    foreach (var (Nev, FajlNev, Ar) in megvasaroltKepek)
                    {
                        writer.WriteLine($"- {Nev}:{FajlNev}:{Ar}");
                    }
                }

                MessageBox.Show($"Ment�s sikeres: {mentesiUtvonal}", "Ment�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba t�rt�nt a ment�s sor�n: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BetoltesGomb_Click(object sender, EventArgs e)
        {
            try
            {
                string mentesiUtvonal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eredmenyek.txt");

                if (!File.Exists(mentesiUtvonal))
                {
                    MessageBox.Show("Nem tal�lhat� mentett adat!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] sorok = File.ReadAllLines(mentesiUtvonal);

                pontok = 0;
                feladatok.Clear();
                feladatokPanel.Controls.Clear();
                megvasaroltKepek.Clear();

                string szekcio = "";
                foreach (string sor in sorok)
                {
                    if (sor.StartsWith("Pontok:"))
                    {
                        pontok = int.Parse(sor.Substring(8).Trim());
                        PontokLabel.Text = $"Pontjaid: {pontok}";
                    }
                    else if (sor.StartsWith("Feladatok:"))
                    {
                        szekcio = "Feladatok";
                    }
                    else if (sor.StartsWith("Megv�s�rolt k�pek:"))
                    {
                        szekcio = "Megv�s�rolt k�pek";
                    }
                    else if (szekcio == "Feladatok" && sor.StartsWith("- "))
                    {
                        string feladatSzoveg = sor.Substring(2).Trim();
                        RadioButton keszGomb = new RadioButton
                        {
                            Text = feladatSzoveg,
                            AutoSize = true
                        };

                        keszGomb.CheckedChanged += (s, e) =>
                        {
                            if (keszGomb.Checked)
                            {
                                feladatok.Remove((feladatSzoveg, keszGomb));
                                feladatokPanel.Controls.Remove(keszGomb);
                                pontok += PontokKiszamitasa(feladatSzoveg);
                                PontokLabel.Text = $"Pontjaid: {pontok}";
                                MessageBox.Show($"Feladat k�sz! Pontjaid: {pontok}");
                            }
                        };

                        feladatok.Add((feladatSzoveg, keszGomb));
                        feladatokPanel.Controls.Add(keszGomb);
                    }
                    else if (szekcio == "Megv�s�rolt k�pek" && sor.StartsWith("- "))
                    {
                        string[] kepAdatok = sor.Substring(2).Split(':');
                        string nev = kepAdatok[0];
                        string fajlNev = kepAdatok[1];
                        int ar = int.Parse(kepAdatok[2]);

                        megvasaroltKepek.Add((nev, fajlNev, ar));
                        pictureBox.Image = Image.FromFile(fajlNev);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

                MessageBox.Show("Adatok sikeresen bet�ltve!", "Bet�lt�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba t�rt�nt az adatok bet�lt�se sor�n: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
