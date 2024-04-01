using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace QR_CODE
{
    public partial class Form1 : Form
    {
        private TextBox dataTextBox;
        private Button generateButton;
        private Button saveButton; // Новая кнопка для сохранения QR-кода
        private PictureBox qrCodePictureBox;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Создание и настройка элемента TextBox
            dataTextBox = new TextBox();
            dataTextBox.Location = new System.Drawing.Point(10, 10);
            dataTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(dataTextBox);

            // Создание и настройка кнопки "Генерировать"
            generateButton = new Button();
            generateButton.Text = "Генерация";
            generateButton.Location = new System.Drawing.Point(10, 40);
            generateButton.Click += GenerateButton_Click;
            this.Controls.Add(generateButton);

            // Создание и настройка кнопки "Сохранить"
            saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Location = new System.Drawing.Point(120, 40);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            // Создание и настройка PictureBox
            qrCodePictureBox = new PictureBox();
            qrCodePictureBox.Location = new System.Drawing.Point(10, 70);
            qrCodePictureBox.Size = new System.Drawing.Size(300, 300);
            qrCodePictureBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(qrCodePictureBox);
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            // Текст или данные для кодирования в QR-коде
            string data = dataTextBox.Text;

            // Создание экземпляра генератора QR-кодов
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Рендеринг QR-кода в изображение Bitmap
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(5);

            // Отображение QR-кода в PictureBox
            qrCodePictureBox.Image = qrCodeImage;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.png, *.jpg, *.bmp)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                qrCodePictureBox.Image.Save(fileName);
                MessageBox.Show("QR-код успешно сохранен.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}