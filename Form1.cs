using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_pdi


{
    public partial class mainS : Form
    {
        bool Dragging;
        int xPos;
        int yPos;
        Image imagem0;
        public mainS()
        {
            InitializeComponent();
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Enabled = false;
            Pimagem.AllowDrop = true;


        }

        public static int Gdialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.BackColor = Color.FromArgb(46, 51, 50);
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            textLabel.Width = 200;
            textLabel.Text = text;
            Font fonte = new Font(DefaultFont, 0);
            textLabel.ForeColor = Color.White;
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 200 };
            inputBox.Maximum = 300;
            inputBox.Minimum = -300;
            Button confirmation = new Button() { Text = "Ok", Left = 90, Width = 100, Top = 80 };
            confirmation.ForeColor = Color.White;
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return (int)inputBox.Value;
        }
        public static float[] Gdialog(string text, string text2, string caption)
        {
            Form prompt = new Form();
            float[] valor = new float[2];
            prompt.BackColor = Color.FromArgb(46, 51, 50);
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.Width = 300;
            prompt.Height = 300;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            textLabel.Width = 200;
            textLabel.Text = text;
            Label textLabel2 = new Label { Left = 50, Top = 110, Text = text2 };
            textLabel2.Width = 200;
            textLabel2.Text = text2;
            Font fonte = new Font(DefaultFont, 0);
            textLabel.ForeColor = Color.White;
            textLabel2.ForeColor = Color.White;
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 200 };
            NumericUpDown inputBox2 = new NumericUpDown() { Left = 50, Top = 150, Width = 200 };
            inputBox.Maximum = 300;
            inputBox.Minimum = -300;
            inputBox2.Maximum = 300;
            inputBox2.Minimum = -300;
            inputBox.DecimalPlaces = 2;
            inputBox2.DecimalPlaces = 2;
            Button confirmation = new Button() { Text = "Ok", Left = 90, Width = 100, Top = 220 };
            confirmation.ForeColor = Color.White;
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(textLabel2);
            prompt.Controls.Add(inputBox2);
            prompt.ShowDialog();
            valor[0] = (float)inputBox.Value;
            valor[1] = (float)inputBox2.Value;
            return valor;
        }
        public static void showDesafio(int total,int quebrados, int capsulas, int redondos)
        {
            Form prompt = new Form();
            prompt.BackColor = Color.FromArgb(46, 51, 50);
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.Width = 300;
            prompt.Height = 200;
            prompt.Text = "Desafio";

            Label TotalLabel = new Label() { Left = 30, Top = 20, Text = "Total de comprimidos na esteira: " + total };
            TotalLabel.Width = 220;
            TotalLabel.ForeColor = Color.White;
            prompt.Controls.Add(TotalLabel);

            Label nQuebrados = new Label() { Left = 30, Top = 45, Text = "Quantidade de comprimidos quebrados: " + quebrados };
            nQuebrados.Width = 220;
            nQuebrados.ForeColor = Color.White;
            prompt.Controls.Add(nQuebrados);

            Label Lredondos = new Label() { Left = 30, Top = 70, Text = "Quantidade de comprimidos Redondos: " + redondos };
            Lredondos.Width = 220;
            Lredondos.ForeColor = Color.White;
            prompt.Controls.Add(Lredondos);

            Label Lcapsulas = new Label() { Left = 30, Top = 95, Text = "Quantidade de Capsulas: " + capsulas };
            Lcapsulas.Width = 220;
            Lcapsulas.ForeColor = Color.White;
            prompt.Controls.Add(Lcapsulas);

            prompt.ShowDialog();
            return;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                if (!string.IsNullOrEmpty(filename))
                {
                    Image imagem = Image.FromFile(filename);
                    imagem = ScaleImage(imagem, Pimagem.Width, Pimagem.Height);
                    Pimagem.Image = imagem;
                    imagem0 = imagem;
                }
                else
                {
                    MessageBox.Show("Nenhum arquivo selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void Pimagem_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (Dragging && c != null)
            {
                c.Top = e.Y + c.Top - yPos;
                c.Left = e.X + c.Left - xPos;
            }
        }

        private void Pimagem_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
        private void move_imagem(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }
        private void Pimagem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button botaoClicado = sender as Button;

                if (botaoClicado != null)
                {
                    botaoClicado.DoDragDrop(botaoClicado, DragDropEffects.Copy);
                }
            }
        }
        private void Pbox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Button)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void Pbox_DragDrop(object sender, DragEventArgs e)
        {
            Button draggedButton = e.Data.GetData(typeof(Button)) as Button;
            if (draggedButton != null)

            {
                Eventos(draggedButton.Name);
            }
        }

        private void Eventos(string name)
        {
            if (name == "bGrayscale")
            {
                grayscale();
            }
            else if (name == "bRedimensionamento")
            {
                float[] valor = new float[2];
                valor = Gdialog("valor de x", "valor de y", "Redimensionamento ");
                redimensionamento(valor[0], valor[1]);
            }

            else if (name == "bEspelhamentov")
            {
                espelhamentoV();
            }
            else if (name == "bEspelhamentoh")
            {
                espelhamentoH();
            }
            else if (name == "rotacao")
            {
                int valor = Gdialog("insira um valor para a rotação ", "ROTAÇÃO");
                rotaçao(valor);
            }
            else if (name == "bbrilho")
            {
                int valor = Gdialog("insira um valor para o brilho", "Brilho");
                Brilho(valor);
            }
            else if (name == "bContraste")
            {
                float valor = Gdialog("insira um valor para Constraste", "Contraste");
                Contraste(valor);
            }
            else if (name == "Btranslaçao")
            {
                float[] valor = new float[2];
                valor = Gdialog("valor de x", "valor de y", "Translação");
                tranlaçao((int)valor[0], (int)valor[1]);
            }
            else if (name == "bMediana")
            {
                
                fMediana();
            }
            else if (name == "bKirsch")
            {
                int valor = Gdialog("insira um valor para o threshold", "KIRSCH");
                Kirsch(Pimagem.Image,valor);
               
            }
            else if (name == "bErosao")
            {
                erosao();
            }
            else if (name == "bDilatacao")
            {
                dilatacao();
            }
            else if (name == "bThinning")
            {
                ZhangSuenThinning();
            }
            else if (name == "bDesafio")
            {
                Desafio();
            }
        }
        private void grayscale()
        {
            if (Pimagem.Image == null) return;

            Bitmap bmp = new Bitmap(Pimagem.Image);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                    Color originalColor = bmp.GetPixel(x, y);
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    bmp.SetPixel(x, y, grayColor);


                }
            }

            Pimagem.Image = bmp;
        }
        private Bitmap grayscale(Image imagem)
        {

            Bitmap bmp = new Bitmap(imagem);
            Bitmap bitmap = new Bitmap(bmp);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                    Color originalColor = bmp.GetPixel(x, y);
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    bitmap.SetPixel(x, y, grayColor);


                }
            }

            return bitmap;
        }

        private void tranlaçao(int plusX, int plusY)
        {
            if (Pimagem.Image == null) return;

            Bitmap bmp = new Bitmap(Pimagem.Image);
            Bitmap bmpTransladada = new Bitmap(bmp.Width + plusX, bmp.Height + plusY);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                    Color originalColor = bmp.GetPixel(x, y);
                    int newpixelx = x + plusX;
                    int newpixely = y + plusY;
                    bmpTransladada.SetPixel(newpixelx, newpixely, originalColor);

                }
            }
            Pimagem.Image = bmpTransladada;
        }

        private void redimensionamento(float plusX, float plusY)
        {
            if (Pimagem.Image == null) return;

            Bitmap bmp = new Bitmap(Pimagem.Image);
            int nWidth = (int)((int)bmp.Width * plusX);
            int nHeight = (int)((int)bmp.Height * plusY);
            Bitmap bmpTransladada = new Bitmap(nWidth, nHeight);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                    Color originalColor = bmp.GetPixel(x, y);
                    int newpixelx = (int)((int)x * plusX);
                    int newpixely = (int)((int)y * plusY);
                    bmpTransladada.SetPixel(newpixelx, newpixely, originalColor);
                    if (plusX > 1 | plusY > 1)
                    {
                        if (newpixelx < bmpTransladada.Width - 2 && newpixely < bmpTransladada.Height - 2)
                        {
                            bmpTransladada.SetPixel(newpixelx + 2, newpixely + 1, originalColor);
                            bmpTransladada.SetPixel(newpixelx + 2, newpixely + 2, originalColor);
                            bmpTransladada.SetPixel(newpixelx + 1, newpixely + 2, originalColor);
                        }
                        if (newpixelx < bmpTransladada.Width - 3 && newpixely < bmpTransladada.Height - 3)
                        {
                            bmpTransladada.SetPixel(newpixelx + 3, newpixely + 2, originalColor);
                            bmpTransladada.SetPixel(newpixelx + 3, newpixely + 3, originalColor);
                            bmpTransladada.SetPixel(newpixelx + 2, newpixely + 3, originalColor);
                        }

                        if (newpixelx > 0 && newpixely > 0)
                        {
                            bmpTransladada.SetPixel(newpixelx - 1, newpixely, originalColor);
                            bmpTransladada.SetPixel(newpixelx - 1, newpixely - 1, originalColor);
                            bmpTransladada.SetPixel(newpixelx, newpixely - 1, originalColor);
                        }
                        if (newpixelx > 1 && newpixely > 1)
                        {
                            bmpTransladada.SetPixel(newpixelx - 2, newpixely - 1, originalColor);
                            bmpTransladada.SetPixel(newpixelx - 2, newpixely - 2, originalColor);
                            bmpTransladada.SetPixel(newpixelx - 1, newpixely - 2, originalColor);
                        }
                        if (newpixelx > 2 && newpixely > 2)
                        {
                            bmpTransladada.SetPixel(newpixelx - 3, newpixely - 2, originalColor);
                            bmpTransladada.SetPixel(newpixelx - 3, newpixely - 3, originalColor);
                            bmpTransladada.SetPixel(newpixelx - 2, newpixely - 3, originalColor);
                        }
                    }
                }
            }
            Pimagem.Image = bmpTransladada;
        }
        private void rotaçao(float grau)
        {
            Bitmap bmp = new Bitmap(Pimagem.Image);
            Bitmap bmprotacionada = new Bitmap(bmp.Width, bmp.Height);

            double radians = grau * Math.PI / 180.0;
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);

            int centerX = bmp.Width / 2;
            int centerY = bmp.Height / 2;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int translatedX = x - centerX;
                    int translatedY = y - centerY;

                    double sourceX = translatedX * cosTheta + translatedY * sinTheta + centerX;
                    double sourceY = -translatedX * sinTheta + translatedY * cosTheta + centerY;

                    if (sourceX >= 0 && sourceX < bmp.Width - 1 && sourceY >= 0 && sourceY < bmp.Height - 1)
                    {
                        int x1 = (int)Math.Floor(sourceX);
                        int y1 = (int)Math.Floor(sourceY);
                        int x2 = x1 + 1;
                        int y2 = y1 + 1;

                        double dx = sourceX - x1;
                        double dy = sourceY - y1;

                        Color c11 = bmp.GetPixel(x1, y1);
                        Color c12 = bmp.GetPixel(x1, y2);
                        Color c21 = bmp.GetPixel(x2, y1);
                        Color c22 = bmp.GetPixel(x2, y2);

                        int red = (int)((c11.R * (1 - dx) * (1 - dy)) + (c21.R * dx * (1 - dy)) +
                                        (c12.R * (1 - dx) * dy) + (c22.R * dx * dy));
                        int green = (int)((c11.G * (1 - dx) * (1 - dy)) + (c21.G * dx * (1 - dy)) +
                                          (c12.G * (1 - dx) * dy) + (c22.G * dx * dy));
                        int blue = (int)((c11.B * (1 - dx) * (1 - dy)) + (c21.B * dx * (1 - dy)) +
                                         (c12.B * (1 - dx) * dy) + (c22.B * dx * dy));

                        bmprotacionada.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }
            }

            Pimagem.Image = bmprotacionada;
        }
        private void espelhamentoH()
        {
            if (Pimagem.Image == null) return;

            Bitmap bmp = new Bitmap(Pimagem.Image);
            Bitmap bmpTransladada = new Bitmap(bmp.Width * 2, bmp.Height);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color originalColor = bmp.GetPixel(x, y);

                    bmpTransladada.SetPixel(x, y, originalColor);

                    int newpixelx = (bmp.Width - 1) - x + bmp.Width;
                    bmpTransladada.SetPixel(newpixelx, y, originalColor);
                }
            }
            Pimagem.Image = bmpTransladada;
        }
        private void espelhamentoV()
        {
            if (Pimagem.Image == null) return;

            Bitmap bmp = new Bitmap(Pimagem.Image);
            Bitmap bmpTransladada = new Bitmap(bmp.Width, bmp.Height * 2);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color originalColor = bmp.GetPixel(x, y);

                    bmpTransladada.SetPixel(x, y, originalColor);

                    int newpixely = (bmp.Height - 1) - y + bmp.Height;
                    bmpTransladada.SetPixel(x, newpixely, originalColor);
                }
            }
            Pimagem.Image = bmpTransladada;
        }

        private void Brilho(float brilho)
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = new Bitmap(grayscale(Pimagem.Image));
            double bAplic = (brilho / 100) + 1;
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {

                    Color pixel = original.GetPixel(x, y);

                    int r = (int)(pixel.R * (bAplic));
                    int g = (int)(pixel.G * (bAplic));
                    int b = (int)(pixel.B * (bAplic));

                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    Color final = Color.FromArgb(r, g, b);
                    saida.SetPixel(x, y, final);

                }


            }
            Pimagem.Image = saida;
        }

        private void Contraste(float contraste)
        {

            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = new Bitmap(original.Width, original.Height);

            float contrast = (259 * (contraste + 255)) / (255 * (259 - contraste));

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);

                    int r = (int)(contrast * (pixel.R - 128) + 128);
                    int g = (int)(contrast * (pixel.G - 128) + 128);
                    int b = (int)(contrast * (pixel.B - 128) + 128);

                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    saida.SetPixel(x, y, Color.FromArgb(pixel.A, r, g, b));
                }
            }

            Pimagem.Image = saida;
        }
        private void fMediana()
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = original;
            int offset = 3 / 2;

            for (int x = offset; x < original.Width - offset; x++)
            {
                for (int y = offset; y < original.Height - offset; y++)
                {
                    Color[] pixels = new Color[9];
                    int index = 0;

                    for (int i = -offset; i <= offset; i++)
                    {
                        for (int j = -offset; j <= offset; j++)
                        {
                            pixels[index++] = original.GetPixel(x + i, y + j);
                        }
                    }
                    int medianaR = pixels.Select(p => p.R).OrderBy(p => p).ElementAt(pixels.Length / 2);
                    int medianaG = pixels.Select(p => p.G).OrderBy(p => p).ElementAt(pixels.Length / 2);
                    int medianaB = pixels.Select(p => p.B).OrderBy(p => p).ElementAt(pixels.Length / 2);

                    saida.SetPixel(x, y, Color.FromArgb(medianaR, medianaG, medianaB));
                }
            }
            Pimagem.Image = saida;
        }

        private void botao_salvar_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp";
            sfd.Title = "Salvar imagem";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {

                System.Drawing.Imaging.ImageFormat formato = System.Drawing.Imaging.ImageFormat.Png;
                switch (System.IO.Path.GetExtension(sfd.FileName).ToLower())
                {
                    case ".jpg":
                        formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        formato = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                }

                Pimagem.Image.Save(sfd.FileName, formato);

            }
        }
        private void Kirsch(Image entrada, int Threshold)
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = grayscale(original);
            double v, g1, g2, g3, g4, g5, g6, g7, g8, g = 0;
            int[,] kernel = new int[,]
            {
                { 5, -3, -3 },
                { 5,  0, -3 },
                { 5, -3, -3 }
            };
            int[,] kernel2 = new int[,]
            {
                { -3, -3, -3 },
                { 5,   0, -3 },
                { 5,   5, -3 }
            };
            int[,] kernel3 = new int[,]
            {
                { -3, -3,-3 },
                { -3, 0, -3 },
                { 5, 5,   5 }
            };
            int[,] kernel4 = new int[,]
            {
                { -3,-3,-3 },
                { -3, 0, 5 },
                { -3, 5, 5 }
            };
            int[,] kernel5 = new int[,]
            {
                { -3, -3,5 },
                { -3, 0, 5 },
                { -3, -3,5 }
            };
            int[,] kernel6 = new int[,]
            {
                { -3, 5, 5 },
                { -3, 0, 5 },
                { -3,-3,-3 }
            };
            int[,] kernel7 = new int[,]
            {
                { 5, 5, 5 },
                { -3,0,-3 },
                {-3,-3,-3 }
            };
            int[,] kernel8 = new int[,]
            {
                { 5, 5, -3 },
                { 5, 0, -3 },
                {-3,-3, -3 }
            };
            for (int i = 1; i < saida.Height - 1; i++)
            {
                for (int j = 1; j < saida.Width - 1; j++)
                {
                    g1 = g2 = g3 = g4 = g5 = g6 = g7 = g8 = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            v = kernel[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g1 += v;
                            v = kernel2[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g2 += v;
                            v = kernel3[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g3 += v;
                            v = kernel4[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g4 += v;
                            v = kernel5[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g5 += v;
                            v = kernel6[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g6 += v;
                            v = kernel7[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g7 += v;
                            v = kernel8[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g8 += v;
                        }
                    }
                    g = g1;
                    if (g2 > g)
                    {
                        g = g2;
                    }
                    if (g3 > g)
                    {
                        g = g3;
                    }
                    if (g4 > g)
                    {
                        g = g4;
                    }
                    if (g5 > g)
                    {
                        g = g5;
                    }
                    if (g6 > g)
                    {
                        g = g6;
                    }
                    if (g7 > g)
                    {
                        g = g7;
                    }
                    if (g8 > g)
                    {
                        g = g8;
                    }
                    byte p = 0;
                    if (g > Threshold)
                    {
                        p = 255;
                    }
                    saida.SetPixel(j, i, Color.FromArgb(p, p, p));
                }
                Pimagem.Image = saida;
            }
        }
        private Bitmap Kirsch(Bitmap entrada, int Threshold)
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = grayscale(original);
            double v, g1, g2, g3, g4, g5, g6, g7, g8, g = 0;
            int[,] kernel = new int[,]
            {
                { 5, -3, -3 },
                { 5,  0, -3 },
                { 5, -3, -3 }
            };
            int[,] kernel2 = new int[,]
            {
                { -3, -3, -3 },
                { 5,   0, -3 },
                { 5,   5, -3 }
            };
            int[,] kernel3 = new int[,]
            {
                { -3, -3,-3 },
                { -3, 0, -3 },
                { 5, 5,   5 }
            };
            int[,] kernel4 = new int[,]
            {
                { -3,-3,-3 },
                { -3, 0, 5 },
                { -3, 5, 5 }
            };
            int[,] kernel5 = new int[,]
            {
                { -3, -3,5 },
                { -3, 0, 5 },
                { -3, -3,5 }
            };
            int[,] kernel6 = new int[,]
            {
                { -3, 5, 5 },
                { -3, 0, 5 },
                { -3,-3,-3 }
            };
            int[,] kernel7 = new int[,]
            {
                { 5, 5, 5 },
                { -3,0,-3 },
                {-3,-3,-3 }
            };
            int[,] kernel8 = new int[,]
            {
                { 5, 5, -3 },
                { 5, 0, -3 },
                {-3,-3, -3 }
            };
            for (int i = 1; i < saida.Height - 1; i++)
            {
                for (int j = 1; j < saida.Width - 1; j++)
                {
                    g1 = g2 = g3 = g4 = g5 = g6 = g7 = g8 = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            v = kernel[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g1 += v;
                            v = kernel2[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g2 += v;
                            v = kernel3[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g3 += v;
                            v = kernel4[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g4 += v;
                            v = kernel5[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g5 += v;
                            v = kernel6[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g6 += v;
                            v = kernel7[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g7 += v;
                            v = kernel8[k, l] * original.GetPixel(j + k - 1, i + l - 1).R;
                            g8 += v;
                        }
                    }
                    g = g1;
                    if (g2 > g)
                    {
                        g = g2;
                    }
                    if (g3 > g)
                    {
                        g = g3;
                    }
                    if (g4 > g)
                    {
                        g = g4;
                    }
                    if (g5 > g)
                    {
                        g = g5;
                    }
                    if (g6 > g)
                    {
                        g = g6;
                    }
                    if (g7 > g)
                    {
                        g = g7;
                    }
                    if (g8 > g)
                    {
                        g = g8;
                    }
                    byte p = 0;
                    if (g > Threshold)
                    {
                        p = 255;
                    }
                    saida.SetPixel(j, i, Color.FromArgb(p, p, p));
                }
                return saida;
            }
            return null;
        }

        private void erosao() 
        { 
        Bitmap original = new Bitmap(Pimagem.Image);
        Bitmap saida = grayscale(original);
        Bitmap resultado = new Bitmap(original.Width, original.Height);

            for (int y = 1; y<original.Height - 1; y++)
            {
               for (int x = 1; x<original.Width - 1; x++)
               {
                   int menor = 255;

                   for (int ky = -1; ky <= 1; ky++)
                   {
                      for (int kx = -1; kx <= 1; kx++)
                      {
                         int pixel = saida.GetPixel(x + kx, y + ky).R;
                         if (pixel<menor)
                         {
                           menor = pixel;
                         }
                      }
                   }

                 resultado.SetPixel(x, y, Color.FromArgb(menor, menor, menor));
               }
            }
         Pimagem.Image = resultado;
        }
        private void dilatacao()
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = grayscale(original);
            Bitmap resultado = new Bitmap(original.Width, original.Height);

            for (int y = 1; y < original.Height - 1; y++)
            {
                for (int x = 1; x < original.Width - 1; x++)
                {
                    int maior = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixel = saida.GetPixel(x + kx, y + ky).R;
                            if (pixel > maior)
                            {
                                maior = pixel;
                            }
                        }
                    }

                    resultado.SetPixel(x, y, Color.FromArgb(maior,maior,maior));
                }
            }
            Pimagem.Image = resultado;
        }
        private Bitmap dilatacao(Image imagem)
        {
            Bitmap original = new Bitmap(Pimagem.Image);
            Bitmap saida = grayscale(original);
            Bitmap resultado = new Bitmap(original.Width, original.Height);

            for (int y = 1; y < original.Height - 1; y++)
            {
                for (int x = 1; x < original.Width - 1; x++)
                {
                    int maior = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixel = saida.GetPixel(x + kx, y + ky).R;
                            if (pixel > maior)
                            {
                                maior = pixel;
                            }
                        }
                    }

                    resultado.SetPixel(x, y, Color.FromArgb(maior, maior, maior));
                }
            }
            return resultado;
        }



        // holt thinning nao funcionando

        //private bool BateMascara(int[,] janela, int[,] mascara)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            if (mascara[i, j] != -1 && janela[i, j] != mascara[i, j])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        //private bool ComparaKernel(int[,] janela, int[][,] mascaras)
        //{
        //    int count = 0;
        //    int transicao = 0;

        //    // P1 é central, P2-P9 são vizinhos
        //    int p1 = janela[1, 1];
        //    int p2 = janela[0, 1];
        //    int p3 = janela[0, 2];
        //    int p4 = janela[1, 2];
        //    int p5 = janela[2, 2];
        //    int p6 = janela[2, 1];
        //    int p7 = janela[2, 0];
        //    int p8 = janela[1, 0];
        //    int p9 = janela[0, 0];

        //    int[] vP = new int[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 };

        //    if (p1 == 255) return false;

        //    for (int i = 1; i < 9; i++)
        //    {
        //        if (vP[i] == 0)
        //            count++;
        //    }
        //    // Adicione no final de ComparaKernel
        //    if (count == 1) return false; // Preserva terminações (evita apagar pontas)


        //    // Conta transições 0→1
        //    for (int i = 1; i < 8; i++)
        //    {
        //        if (vP[i] == 0 && vP[i + 1] == 255)
        //            transicao++;
        //    }

        //    // Checa condições de remoção
        //    if (count >= 2 && count <= 6 && transicao == 1)
        //    {
        //        foreach (var mascara in mascaras)
        //        {
        //            if (BateMascara(janela, mascara))
        //                return false; // NÃO remove se bateu com máscara
        //        }

        //        return true; // pode remover
        //    }

        //    return false;
        //}




        //private void thinning_holt()
        //{
        //    Bitmap original = new Bitmap(Pimagem.Image);
        //    Bitmap saida = Binarizaçao(original, 125);
        //    Bitmap resultado = new Bitmap(saida);

        //    int[,] janela = new int[3, 3];

        //    int[][,] mascaras = new int[][,]
        //    {
        // new int[,] { {-1, 0, 255}, {0, 0, 255}, {255, 255, -1} },
        // new int[,] { {255, -1, 0}, {255, 0, 0}, {-1, 255, 255} },
        // new int[,] { {255, 255, -1}, {255, 0, 0}, {-1, 0, 255} },
        // new int[,] { {-1, 255, 255}, {0, 0, 255}, {255, -1, 0} },

        // new int[,] { {255, 0, -1}, {255, 0, 0}, {-1, 255, 255} },
        // new int[,] { {-1, 255, 255}, {0, 0, 255}, {255, 0, -1} },
        // new int[,] { {-1, 0, 255}, {0, 0, 255}, {255, 255, -1} },
        // new int[,] { {255, 255, -1}, {255, 0, 0}, {-1, 0, 255} },

        // new int[,] { {-1, 255, 255}, {255, 0, 0}, {255, 0, -1} },
        // new int[,] { {255, -1, 0}, {255, 0, 0}, {-1, 255, 255} },
        // new int[,] { {255, 0, -1}, {255, 0, 0}, {-1, 255, 255} },
        // new int[,] { {-1, 255, 255}, {0, 0, 255}, {255, 0, -1} },

        // new int[,] { {255, 255, -1}, {0, 0, 255}, {-1, 0, 255} },
        // new int[,] { {-1, 0, 255}, {0, 0, 255}, {255, 255, -1} },
        // new int[,] { {-1, 255, 255}, {255, 0, 0}, {255, 0, -1} },
        // new int[,] { {255, 0, -1}, {255, 0, 0}, {-1, 255, 255} }};

        //    bool alterado = true;
        //    while (alterado)
        //    {
        //        alterado = false;

        //        Bitmap novo = new Bitmap(resultado); // copia atual

        //        for (int y = 1; y < resultado.Height - 1; y++)
        //        {
        //            for (int x = 1; x < resultado.Width - 1; x++)
        //            {
        //                for (int i = -1; i <= 1; i++)
        //                    for (int j = -1; j <= 1; j++)
        //                        janela[i + 1, j + 1] = resultado.GetPixel(x + j, y + i).R;

        //                if (ComparaKernel(janela, mascaras))
        //                {
        //                    novo.SetPixel(x, y, Color.FromArgb(255, 255, 255));
        //                    alterado = true;
        //                }
        //            }
        //        }

        //        resultado = novo;
        //    }
        //    Pimagem.Image = resultado;
        //}
        private void ZhangSuenThinning()
        {
            Bitmap input = new Bitmap(Pimagem.Image);
            Bitmap bin = Binarizaçao(input, 125); // binariza a imagem
            Bitmap img = new Bitmap(bin);
            bool pixelRemovido;

            do
            {
                pixelRemovido = false;
                List<Point> pontosParaRemover = new List<Point>();

                // Etapa 1
                for (int y = 1; y < img.Height - 1; y++)
                {
                    for (int x = 1; x < img.Width - 1; x++)
                    {
                        if (img.GetPixel(x, y).R == 0 && ZhangSuenCondicao(x, y, img, etapa: 1))
                            pontosParaRemover.Add(new Point(x, y));
                    }
                }

                foreach (Point p in pontosParaRemover)
                {
                    img.SetPixel(p.X, p.Y, Color.White);
                    pixelRemovido = true;
                }

                pontosParaRemover.Clear();

                // Etapa 2
                for (int y = 1; y < img.Height - 1; y++)
                {
                    for (int x = 1; x < img.Width - 1; x++)
                    {
                        if (img.GetPixel(x, y).R == 0 && ZhangSuenCondicao(x, y, img, etapa: 2))
                            pontosParaRemover.Add(new Point(x, y));
                    }
                }

                foreach (Point p in pontosParaRemover)
                {
                    img.SetPixel(p.X, p.Y, Color.White);
                    pixelRemovido = true;
                }

            } while (pixelRemovido);

            Pimagem.Image = img;
        }
        private bool ZhangSuenCondicao(int x, int y, Bitmap img, int etapa)
        {
            int[,] viz = new int[3, 3];
            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    viz[j + 1, i + 1] = img.GetPixel(x + i, y + j).R == 0 ? 1 : 0;
                }
            }

            int[] p = {
        viz[1, 1], // P1 (centro)
        viz[0, 1], // P2
        viz[0, 2], // P3
        viz[1, 2], // P4
        viz[2, 2], // P5
        viz[2, 1], // P6
        viz[2, 0], // P7
        viz[1, 0], // P8
        viz[0, 0]  // P9
    };

            int A = 0;
            for (int i = 1; i <= 7; i++)
                if (p[i] == 0 && p[i + 1] == 1) A++;
            if (p[8] == 0 && p[1] == 1) A++;

            int B = p[1] + p[2] + p[3] + p[4] + p[5] + p[6] + p[7] + p[8];

            if (A == 1 && B >= 2 && B <= 6)
            {
                if (etapa == 1)
                {
                    if (p[1] * p[3] * p[5] == 0 && p[3] * p[5] * p[7] == 0)
                        return true;
                }
                else if (etapa == 2)
                {
                    if (p[1] * p[3] * p[7] == 0 && p[1] * p[5] * p[7] == 0)
                        return true;
                }
            }

            return false;
        }


        private Bitmap Binarizaçao(Image imagem, byte threshold)
        {
            Bitmap original = new Bitmap(imagem);
            Bitmap saida = grayscale(original);

            for (int i = 0; i < saida.Width; i++)
            {
                for (int j = 0; j < saida.Height; j++)
                {
                    Color pixel = saida.GetPixel(i, j);
                    if (pixel.R > threshold)
                    {
                        saida.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        saida.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return saida;
        }
        private int floodfill(Bitmap entrada, int x, int y, Color subs )
        {
            if (x < 0 || x >= entrada.Width || y < 0 || y >= entrada.Height)
            {
                return 0;
            }
            if(entrada.GetPixel(x, y).B != 255)
            {
                return 0;
            }
            entrada.SetPixel(x, y, subs);
            int count = 1;
            // Chama recursivamente para os pixels vizinhos
            count += floodfill(entrada, x + 1, y, subs);
            count += floodfill(entrada, x - 1, y, subs);
            count += floodfill(entrada, x, y + 1, subs);
            count += floodfill(entrada, x, y - 1, subs);

            return count;
        }
        private void Desafio() 
        {
            Bitmap saida = new Bitmap(Binarizaçao(Pimagem.Image, 120));
            saida = dilatacao(saida);
            saida = dilatacao(saida); 
            saida = Binarizaçao(saida, 150);
            byte count = 254;
            int Area;
            List<int> Comprimidos = new List<int>();

            for (int i = 0; i < saida.Width; i++)
            {
                for (int j = 0; j < saida.Height; j++)
                {
                    Color cor = saida.GetPixel(i, j);
                    if (cor.R == 255)
                    {
                        Area = 0;
                        Color Caplic = Color.FromArgb(count, count, count);
                        Area = floodfill(saida, i, j,Caplic);
                        Comprimidos.Add(Area);
                        count--;
                    }
                }    
            }
            Comprimidos.Sort();
            int totais = Comprimidos.Count;
            int redondos = 0;
            int capsulas = 0;
            int quebrados = 0;
            // calcular media minima
            for (int i = 0; i< Comprimidos.Count; i++)
            {
                double media = Comprimidos.Average();
                // verifica se a media é compativel com um comprimido inteiro
                if (media < (saida.Width * saida.Height) * 0.013 || media > (saida.Width * saida.Height) *0.017)
                    media = (saida.Width * saida.Height) * 0.014;
                if (Comprimidos.ElementAt(i) < media * 0.8)
                {
                    quebrados++;
                    Comprimidos.RemoveAt(i);
                    i--;
                }
                else if (Comprimidos.ElementAt(i) > media * 1.14) capsulas++;
                else redondos++;
            }
            showDesafio(totais,quebrados, capsulas,redondos);
        }
    }
}
