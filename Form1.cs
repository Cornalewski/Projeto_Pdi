using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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


    private void Form1_Load(object sender, EventArgs e)
    {

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
    private void Form1_DoubleClick(object sender, EventArgs e)
    {

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


        private void barra_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

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
}
}
