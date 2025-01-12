namespace ToDoList
{
    public partial class Form1 : Form
    {
        private int pontok = 0; // Felhaszn�l� �sszes pontja
        private List<(string FeladatSzoveg, RadioButton KeszGomb)> feladatok = new List<(string, RadioButton)>(); // Feladatok t�rol�sa
        private List<(string Nev, string FajlNev, int Ar)> megvasaroltKepek = new List<(string, string, int)>(); // V�s�rolt k�pek t�rol�sa

        private Dictionary<string, Point> kepPoziciok = new Dictionary<string, Point>
        {
            { "F�cska", new Point(10, 10) },
            { "H�zik�", new Point(220, 10) },
            { "Kutya", new Point(430, 10) } // Kutya poz�ci�ja
        };

        public Form1()
        {
            InitializeComponent();
            ToDoGomb.Click += FeladatHozzaadasa; // Feladat hozz�ad�s�ra gomb
            VasarlasGomb.Click += VasarlasiAblakMegnyitasa; // V�s�rl�si men� megnyit�sa
            GeneraltKepGomb.Click += GeneraltKepMegjelenitese; // Gener�lt k�p l�trehoz�sa
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
                ("Kutya", "C:\\Users\\User\\Desktop\\kutya.jpg", 25) // �j kutya k�p hozz�ad�sa
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

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
