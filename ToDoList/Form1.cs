namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Felhasználó összes pontja
        private List<(string FeladatSzoveg, RadioButton KeszGomb)> feladatok = new List<(string, RadioButton)>(); // Feladatok tárolása
        private List<(string Nev, string FajlNev, int Ar)> megvasaroltKepek = new List<(string, string, int)>(); // Vásárolt képek tárolása

        private Dictionary<string, Point> kepPoziciok = new Dictionary<string, Point>
        {
            { "Fácska", new Point(10, 10) },
            { "Házikó", new Point(220, 10) },
            { "Kutya", new Point(430, 10) } // Kutya pozíciója
        };

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += FeladatHozzaadasa; // Feladat hozzáadására gomb
            VasarlasGomb.Click += VasarlasiAblakMegnyitasa; // Vásárlási menü megnyitása
            GeneraltKepGomb.Click += GeneraltKepMegjelenitese; // Generált kép létrehozása
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
                ("Kutya", "C:\\Users\\User\\Desktop\\kutya.jpg", 25) // Új kutya kép hozzáadása
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
                pontok -= ar;
                PontokLabel.Text = $"Pontjaid: {pontok}";
                megvasaroltKepek.Add((nev, kepFajl, ar));

                if (kepPoziciok.TryGetValue(nev, out Point pozicio))
                {
                    using (Graphics g = pictureBox.CreateGraphics())
                    {
                        using (Image img = Image.FromFile(kepFajl))
                        {
                            g.DrawImage(img, pozicio.X, pozicio.Y, 200, 200);
                        }
                    }
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

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
