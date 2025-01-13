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
            MentesGomb.Click += MentesGomb_Click;
            BetoltesGomb.Click += BetoltesGomb_Click;
            exitButton.Click += exitButton_Click;
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
                Size = new Size(400, 400),
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
                ("Kutya", "C:\\Users\\User\\Desktop\\kutyaJo.jpg", 25),
                ("Cica", "C:\\Users\\User\\Desktop\\macskaJo.jpg", 40),
                //("Tájkép", "C:\\Users\\User\\Desktop\\tajkepJo.jpg", 50)
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

            int rowCount = (int)Math.Ceiling(Math.Sqrt(megvasaroltKepek.Count));
            int colCount = rowCount;
            int imageSize = 200;

            Bitmap osszkep = new Bitmap(colCount * imageSize, rowCount * imageSize);

            using (Graphics g = Graphics.FromImage(osszkep))
            {
                g.Clear(Color.White);

                int x = 0, y = 0;

                foreach (var (_, fajlNev, _) in megvasaroltKepek)
                {
                    using (Image img = Image.FromFile(fajlNev))
                    {
                        g.DrawImage(img, x * imageSize, y * imageSize, imageSize, imageSize);
                        x++;

                        if (x >= colCount)
                        {
                            x = 0;
                            y++;
                        }
                    }
                }
            }

            pictureBox.Image = osszkep;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MessageBox.Show("Az összkép elkészült!");
        }

        private void MentesGomb_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("adatok.txt"))
                {
                    writer.WriteLine("Pontok:" + pontok);

                    writer.WriteLine("Feladatok:");
                    foreach (var (FeladatSzoveg, _) in feladatok)
                    {
                        writer.WriteLine(FeladatSzoveg);
                    }

                    writer.WriteLine("MegvasaroltKepek:");
                    foreach (var (Nev, FajlNev, Ar) in megvasaroltKepek)
                    {
                        writer.WriteLine($"{Nev},{FajlNev},{Ar}");
                    }
                }

                MessageBox.Show("Adatok sikeresen mentve!", "Mentés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!File.Exists("adatok.txt"))
                {
                    MessageBox.Show("Nincs mentett adat.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (StreamReader reader = new StreamReader("adatok.txt"))
                {
                    pontok = int.Parse(reader.ReadLine().Split(':')[1]);
                    PontokLabel.Text = $"Pontjaid: {pontok}";

                    feladatok.Clear();
                    feladatokPanel.Controls.Clear();

                    reader.ReadLine(); // Feladatok:
                    string line;
                    while ((line = reader.ReadLine()) != null && line != "MegvasaroltKepek:")
                    {
                        RadioButton keszGomb = new RadioButton
                        {
                            Text = line,
                            AutoSize = true
                        };

                        keszGomb.CheckedChanged += (s, e) =>
                        {
                            if (keszGomb.Checked)
                            {
                                feladatok.Remove((line, keszGomb));
                                feladatokPanel.Controls.Remove(keszGomb);
                                pontok += PontokKiszamitasa(line);
                                PontokLabel.Text = $"Pontjaid: {pontok}";
                                MessageBox.Show($"Feladat kész! Pontjaid: {pontok}");
                            }
                        };

                        feladatok.Add((line, keszGomb));
                        feladatokPanel.Controls.Add(keszGomb);
                    }

                    megvasaroltKepek.Clear();

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string nev = parts[0];
                        string fajlNev = parts[1];
                        int ar = int.Parse(parts[2]);

                        megvasaroltKepek.Add((nev, fajlNev, ar));
                        if (File.Exists(fajlNev))
                        {
                            pictureBox.Image = Image.FromFile(fajlNev);
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
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
