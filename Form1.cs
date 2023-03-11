// создаём объекты в програм цс, чтобы их было видно из вне. (mvc)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
/*using System.Reflection.Emit;*/
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// ЦЕНЫ УЛИЦЫ НА ОБР СТОРОНЕ.
// При ходе в цикле игрок передвигается по полям, а в конце подсветка
namespace Monopoly
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            // размер считается в зависимости от высоты экрана
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            double s = Math.Ceiling(resolution.Height / 11.0);
            generation((int)s);
            // квадраты размером (y, y), где y - высота экрана
            InitializeComponent();
        }
        // СОЗДАНИЕ КАРТЫ
        void generation(int size)
        {
            Panel[,] mas = new Panel[10, 10];
            int num = 0;
            int X = 0;
            int Y = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    mas[i, j] = new Panel();
                    mas[i, j].BorderStyle = BorderStyle.FixedSingle;
                    mas[i, j].BackColor = Color.Black;
                    mas[i, j].Name = Convert.ToString(num);
                    mas[i, j].Size = new System.Drawing.Size(size, size);
                    mas[i, j].TabIndex = num++;
                    mas[i, j].Click += new System.EventHandler(this.pictureBox1_Click);
                    this.Controls.Add(mas[i, j]);

                    if (i == 0 | i == 9) mas[i, j].Location = new System.Drawing.Point(X, Y);
                    else if (j == 0) mas[i, j].Location = new System.Drawing.Point(X, Y);
                    else if (j == 9) mas[i, j].Location = new System.Drawing.Point(X, Y);
                    X += size;
                }
                X = 0;
                Y += size;
            }
            // КИНУТЬ КУБИК
            /*Button drop = new Button();
            drop.Text = "Кинуть кубик";
            drop.Size = new System.Drawing.Size(size, size);
            drop.Location = new System.Drawing.Point(0, 0);
            drop.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 8), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            drop.Click += new System.EventHandler(this.pictureBox1_Click);
            mas[5, 5].Controls.Add(drop);*/
            // СТАРТ
            Label start = new Label();
            start.Text = "START";
            start.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 8), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            start.ForeColor = Color.White;
            mas[0, 0].Controls.Add(start);
            // РАЗМЕЩЕНИЕ ИГРОКОВ (ПЕРЕХОД ПРОСТО НОВЫЙ control.Add)

            Player player1 = new Player(Color.Red); Player player2 = new Player(Color.Purple);
            Player player3 = new Player(Color.Aqua); Player player4 = new Player(Color.Green);
            List<Player> players = new List<Player>() { player1, player2, player3, player4 };

            PictureBox[] players1 = new PictureBox[4];
            int x = size / 10;
            int y = size / 2;
            for (int i = 0; i < 4; i++)
            {
                players1[i] = new PictureBox();
                players1[i].BackColor = Color.Green;
                players1[i].Size = new System.Drawing.Size(size / 10, size / 10);
                players1[i].Location = new System.Drawing.Point(x, y);
                players1[i].BackColor = players[i].Color;
                
                x += size / 5;
                mas[0, 0].Controls.Add(players1[i]);
            }
            // РАЗМЕЩЕНИЕ УЛИЦ
            mas[0, 1].Controls.Add(createStreet("Житная", Color.SaddleBrown, size));
            mas[0, 3].Controls.Add(createStreet("Нагалинская", Color.SaddleBrown, size));
            mas[0, 5].Controls.Add(createStreet("Варшавское шоссе", Color.LightBlue, size));
            mas[0, 7].Controls.Add(createStreet("Огарёво", Color.LightBlue, size));
            mas[0, 8].Controls.Add(createStreet("Первая парковая", Color.LightBlue, size));
            mas[1, 0].Controls.Add(createStreet("Арбат", Color.Blue, size));
            mas[1, 9].Controls.Add(createStreet("Полянка", Color.Purple, size));
            mas[2, 0].Controls.Add(createStreet("Малая бронная", Color.Blue, size));
            mas[2, 9].Controls.Add(createStreet("Сретенка", Color.Purple, size));
            mas[4, 9].Controls.Add(createStreet("Ростовская наб.", Color.Purple, size));
            mas[5, 0].Controls.Add(createStreet("Кутузовский проспект", Color.Green, size));
            mas[5, 9].Controls.Add(createStreet("Рязанский проспект", Color.Orange, size));
            mas[7, 0].Controls.Add(createStreet("Гоголевский бульвар", Color.Green, size));
            mas[7, 9].Controls.Add(createStreet("Вавилова", Color.Orange, size));
            mas[8, 0].Controls.Add(createStreet("Щусева", Color.Green, size));
            mas[8, 9].Controls.Add(createStreet("Рублевское шоссе", Color.Orange, size));
            mas[9, 1].Controls.Add(createStreet("Смоленская площадь", Color.LightSlateGray, size));
            mas[9, 2].Controls.Add(createStreet("Новинский бульвар", Color.LightSlateGray, size));
            mas[9, 4].Controls.Add(createStreet("Грузинский вал", Color.LightSlateGray, size));
            mas[9, 5].Controls.Add(createStreet("Площадь Маяковского", Color.DarkRed, size));
            mas[9, 7].Controls.Add(createStreet("Пушкинская", Color.DarkRed, size));
            mas[9, 8].Controls.Add(createStreet("Тверская", Color.DarkRed, size));
            // ШАНС
            mas[0, 2].Controls.Add(chance(size));
            mas[0, 6].Controls.Add(chance(size));
            mas[4, 0].Controls.Add(chance(size));
            mas[6, 0].Controls.Add(chance(size));
            mas[6, 9].Controls.Add(chance(size));
            mas[9, 6].Controls.Add(chance(size));
            // ДОРОГА
            mas[0, 4].Controls.Add(road(size));
            mas[3, 9].Controls.Add(road(size));
            mas[3, 0].Controls.Add(road(size));
            mas[9, 3].Controls.Add(road(size));
            // ТЮРЬМА
            mas[0, 9].Controls.Add(prison(0, size));
            mas[9, 0].Controls.Add(prison(1, size));
            // СТОЯНКА
            mas[9, 9].Controls.Add(parking(size));
        }


        // функция создания улиц 
        private Label createStreet(string streetName, Color color, int size)
        {
            Label streetLabel = new Label();
            streetLabel.Text = streetName;
            streetLabel.ForeColor = Color.White;
            streetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 10), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            streetLabel.Size = new System.Drawing.Size(size, size / 2);
            streetLabel.Location = new System.Drawing.Point(0, 0);
            streetLabel.BackColor = color;
            return streetLabel;
        }
        // функция создания карточки шанс
        private Panel chance(int size)
        {
            Panel chance = new Panel();

            Label chanceLabel = new Label();
            chanceLabel.Text = "Шанс";
            chanceLabel.ForeColor = Color.White;
            chanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 8), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chanceLabel.Size = new System.Drawing.Size(size, size / 2);
            chanceLabel.Location = new System.Drawing.Point(size / 10, 0);
            chance.Controls.Add(chanceLabel);

            Label chanceLabel1 = new Label();
            chanceLabel1.Text = "?";
            chanceLabel1.ForeColor = Color.White;
            chanceLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 4), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chanceLabel1.Size = new System.Drawing.Size(size / 2, size / 2);
            chanceLabel1.Location = new System.Drawing.Point(size / 3, size / 2);
            chance.Controls.Add(chanceLabel1);

            return chance;
        }
        // ДОРОГА
        private Label road(int size)
        {
            Label roatLabel = new Label();
            roatLabel.Text = "🚂";
            roatLabel.ForeColor = Color.White;
            roatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 3), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            roatLabel.Size = new System.Drawing.Size(size, size);
            roatLabel.Location = new System.Drawing.Point(size / 20, size / 10);
            return roatLabel;
        }
        // ТЮРЬМА
        private Panel prison(int ind, int size)
        {

            Panel prison = new Panel();
            String name;
            if (ind == 0) name = "Тюрьма";
            else name = "В тюрьму";

            Label prisonLabel1 = new Label();
            prisonLabel1.Text = name;
            prisonLabel1.ForeColor = Color.White;
            prisonLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 10), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            prisonLabel1.Size = new System.Drawing.Size(size, size / 2);
            prisonLabel1.Location = new System.Drawing.Point(0, 0);
            prison.Controls.Add(prisonLabel1);

            Label prisonLabel = new Label();
            prisonLabel.Text = "⛓️⛓️";
            prisonLabel.ForeColor = Color.White;
            prisonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 6), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            prisonLabel.Size = new System.Drawing.Size(size, size / 2);
            prisonLabel.Location = new System.Drawing.Point(0, size / 2);
            prison.Controls.Add(prisonLabel);
            return prison;
        }
        // СТОЯНКА
        private Panel parking(int size)
        {
            Panel parking = new Panel();

            Label parkingLabel = new Label();
            parkingLabel.Text = "Стоянка";
            parkingLabel.ForeColor = Color.White;
            parkingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 8), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parkingLabel.Size = new System.Drawing.Size(size, size / 2);
            parkingLabel.Location = new System.Drawing.Point(0, 0);
            parking.Controls.Add(parkingLabel);

            Label parkingLabel1 = new Label();
            parkingLabel1.Text = "🏕️";
            parkingLabel1.ForeColor = Color.White;
            parkingLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", (size / 4), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parkingLabel1.Size = new System.Drawing.Size(size / 2, size / 2);
            parkingLabel1.Location = new System.Drawing.Point(size / 5, size / 2);
            parking.Controls.Add(parkingLabel1);

            return parking;
        }
        public void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("" + this.game.players[0].Color);
            /*Game gameMaster = new Game();
            gameMaster.step();*/
            /*Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            Panel pictureBox = (Panel)sender;
            MessageBox.Show("" + this.Size.Height + ", " + pictureBox.Size.Height);*/
            /*generation(60);*/
        }

  
    }
}