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
                MessageBox.Show("Elõbb végezz el feladatokat, hogy újakat vehess fel!");
                return;
            }

            string ujFeladat = ToDoBekerese.Text;
            string nehezsegiSzint = FeladatNehezsegiSzint.SelectedItem as string;

            if (string.IsNullOrEmpty(nehezsegiSzint))
            {
                MessageBox.Show("Kérlek, válassz nehézségi szintet!");
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
                        MessageBox.Show($"Feladat kész! Pontjaid: {pontok}");
                    }
                };

                feladatok.Add((ujFeladat, keszGomb));
                feladatokPanel.Controls.Add(keszGomb);

                ToDoBekerese.Clear();
                FeladatNehezsegiSzint.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Adj meg egy érvényes feladatot!");
            }
        }

        private int PontokKiszamitasa(string nehezsegiSzint)
        {
            return nehezsegiSzint switch
            {
                "Könnyû" => new Random().Next(1, 6),
                "Közepes" => new Random().Next(6, 16),
                "Nehéz" => new Random().Next(15, 26),
                _ => 0
            };
        }

        private void VasarlasiAblakMegnyitasa(object sender, EventArgs e)
        {
            Form vasarlasiAblak = new Form
            {
                Text = "Képek vásárlása",
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
                ("Fácska", "C:\\Users\\User\\Desktop\\fácskaJo.jpg", 20),
                ("Házikó", "C:\\Users\\User\\Desktop\\hazikoJo.jpg", 30),
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
                    Text = "Vásárlás",
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
                    MessageBox.Show($"A fájl nem található: {kepFajl}");
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
                    MessageBox.Show($"Nem sikerült betölteni a képet: {ex.Message}");
                }

                MessageBox.Show($"Sikeresen megvásároltad a(z) {nev} képet!");
            }
            else
            {
                MessageBox.Show("Nincs elég pontod a kép megvásárlásához!");
            }
        }

        private void GeneraltKepMegjelenitese(object sender, EventArgs e)



        {
            if (megvasaroltKepek.Count == 0)
            {
                MessageBox.Show("Nincs elérhetõ kép az összkép generálásához!");
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
            MessageBox.Show("Az összkép elkészült!");
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

                    writer.WriteLine("Megvásárolt képek:");
                    foreach (var (Nev, FajlNev, Ar) in megvasaroltKepek)
                    {
                        writer.WriteLine($"- {Nev}:{FajlNev}:{Ar}");
                    }
                }

                MessageBox.Show($"Mentés sikeres: {mentesiUtvonal}", "Mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BetoltesGomb_Click(object sender, EventArgs e)
        {
            try
            {
                string mentesiUtvonal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eredmenyek.txt");

                if (!File.Exists(mentesiUtvonal))
                {
                    MessageBox.Show("Nem található mentett adat!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else if (sor.StartsWith("Megvásárolt képek:"))
                    {
                        szekcio = "Megvásárolt képek";
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
                                MessageBox.Show($"Feladat kész! Pontjaid: {pontok}");
                            }
                        };

                        feladatok.Add((feladatSzoveg, keszGomb));
                        feladatokPanel.Controls.Add(keszGomb);
                    }
                    else if (szekcio == "Megvásárolt képek" && sor.StartsWith("- "))
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

                MessageBox.Show("Adatok sikeresen betöltve!", "Betöltés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt az adatok betöltése során: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
