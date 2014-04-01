using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MineSweeperForm : Form
    {
        #region Variablen
        //Array zugriff durch <Array>[y][x]
        private int[][] Spielfeld;
        private Label[][] LabelArray;
        private Button[][] ButtonArray;
        private List<Point> checkedFields = new List<Point>();
        private ToolStripLabel tslTime = new ToolStripLabel();
        private ToolStripLabel tslMine = new ToolStripLabel();
        private bool gameStarted = false;
        private const int iMine = 10;
        #endregion

        #region Formcontrol
        public MineSweeperForm()
        {
            InitializeComponent();
        }

        private void leichtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameStarted)
                deleteGame();
            Spielfeld = new int[9][];
            LabelArray = new Label[9][];
            ButtonArray = new Button[9][];
            for (int i = 0; i < 9; i++)
            {
                Spielfeld[i] = new int[9];
                LabelArray[i] = new Label[9];
                ButtonArray[i] = new Button[9];
            }
            start();
        }

        private void mittelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameStarted)
                deleteGame();
            Spielfeld = new int[16][];
            LabelArray = new Label[16][];
            ButtonArray = new Button[16][];
            for (int i = 0; i < 16; i++)
            {
                Spielfeld[i] = new int[16];
                LabelArray[i] = new Label[16];
                ButtonArray[i] = new Button[16];
            }
            start();
        }

        private void schwerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameStarted)
                deleteGame();
            Spielfeld = new int[16][];
            LabelArray = new Label[16][];
            ButtonArray = new Button[16][];
            for (int i = 0; i < 16; i++)
            {
                Spielfeld[i] = new int[30];
                LabelArray[i] = new Label[30];
                ButtonArray[i] = new Button[30];
            }
            start();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameStarted)
                deleteGame();
            this.Close();
        }

        private void MineSweeperForm_Paint(object sender, PaintEventArgs e)
        {
            if (gameStarted)
            {
                int height = Spielfeld.Length * 20;
                int length = Spielfeld[0].Length * 20;
                Pen blackPen = new Pen(Color.Black, 1);
                //7+(x*20), 27+(y*20)
                for (int i = 0; i <= Spielfeld.Length; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(7, 27 + (i * 20)), new Point(7 + length, 27 + (i * 20)));
                }
                for (int i = 0; i <= Spielfeld[0].Length; i++)
                {
                    e.Graphics.DrawLine(blackPen, new Point(7 + (i * 20), 27), new Point(7 + (i * 20), 27 + height));
                }
            }
        }

        private void Mine_Click(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (b.BackgroundImage != null)
                {
                    b.BackgroundImage = null;
                    tslMine.Text = Convert.ToString(Convert.ToInt32(tslMine.Text) + 1).PadLeft(2, '0');
                }
                else if(Convert.ToInt32(tslMine.Text) > 0)
                {
                    b.BackgroundImage = MineSweeper.Properties.Resources.flag;
                    tslMine.Text = Convert.ToString(Convert.ToInt32(tslMine.Text) - 1).PadLeft(2, '0');
                }
            }
            else if(b.BackgroundImage == null)
            {
                int x=0, y=0;
                this.Controls.Remove(b);
                getCordinates(b.Name, ref x, ref y);
                if (Spielfeld[y][x] == iMine)
                    endGame();
                else
                {
                    openClick(x, y);
                    checkedFields.Clear();
                    checkGameWon();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslTime.Text = Convert.ToString(Convert.ToInt32(tslTime.Text) + 1).PadLeft(2, '0');
        }
        #endregion

        #region private Method's
        private void checkGameWon()
        {
            bool bcheck = true;
            for (int y = 0; y < Spielfeld.Length && bcheck; y++)
                for (int x = 0; x < Spielfeld[0].Length; x++)
                    if (this.Controls.Contains(ButtonArray[y][x]) && Spielfeld[y][x] != iMine)
                    {
                        bcheck = false;
                        break;
                    }
            if (bcheck)
                endGame(true);
        }

        private void deleteGame()
        {
            for (int y = 0; y < Spielfeld.Length; y++)
            {
                for (int x = 0; x < Spielfeld[y].Length; x++)
                {
                    this.Controls.Remove(ButtonArray[y][x]);
                    this.Controls.Remove(LabelArray[y][x]);
                }
                ButtonArray[y] = null;
                LabelArray[y] = null;
                Spielfeld[y] = null;
            }
            ButtonArray = null;
            LabelArray = null;
            Spielfeld = null;
            gameStarted = false;
        }

        private void endGame(bool won=false)
        {
            tmr_game.Enabled = false;
            for (int y = 0; y < Spielfeld.Length; y++)
                for (int x = 0; x < Spielfeld[0].Length; x++)
                    if (Spielfeld[y][x] == iMine)
                        LabelArray[y][x].BringToFront();
            EndForm endForm = new EndForm(this.Location.X + this.Size.Width, this.Location.Y);
            endForm.Won = won;
            endForm.Time = Convert.ToInt32(tslTime.Text);
            if (endForm.ShowDialog() == System.Windows.Forms.DialogResult.Retry)
                retry();
            else
                this.Close();
        }

        private void generateMines()
        {
            int cnt = 0;
            int max=0;
            Random rnd = new Random();
            switch(Spielfeld[0].Length)
            {
                case 9:
                    max = 10;
                    break;
                case 16:
                    max = 40;
                    break;
                case 30:
                    max = 99;
                    break;
            }
            tslMine.Text = max.ToString();
            while (cnt <= max)
            {
                Spielfeld[rnd.Next()%Spielfeld.Length][rnd.Next()%Spielfeld[0].Length] = iMine;
                cnt++;
            }
        }

        private int generateMinesAround(int x, int y)
        {
            int cnt = 0;
            bool top=true, bottom=true, left=true, right=true;
            if (x == 0)
                left = false;
            if (x == Spielfeld[0].Length - 1)
                right = false;
            if (y == 0)
                top = false;
            if (y == Spielfeld.Length - 1)
                bottom = false;

            if(top)
            {
                if (left)
                    if (Spielfeld[y - 1][x - 1] == iMine)
                        cnt++;
                if (Spielfeld[y - 1][x] == iMine)
                    cnt++;
                if (right)
                    if (Spielfeld[y - 1][x + 1] == iMine)
                        cnt++;
            }
            if (left)
                if (Spielfeld[y][x - 1] == iMine)
                    cnt++;
            if (right)
                if (Spielfeld[y][x + 1] == iMine)
                    cnt++;
            if (bottom)
            {
                if (left)
                    if (Spielfeld[y + 1][x - 1] == iMine)
                        cnt++;
                if (Spielfeld[y + 1][x] == iMine)
                    cnt++;
                if (right)
                    if (Spielfeld[y + 1][x + 1] == iMine)
                        cnt++;
            }
            return cnt;
        }

        private void generateSpielfeld()
        {
            generateMines();
            for (int y = 0; y < Spielfeld.Length; y++)
                for (int x = 0; x < Spielfeld[0].Length; x++)
                    if (Spielfeld[y][x] != iMine)
                        Spielfeld[y][x] = generateMinesAround(x, y);
        }

        private void getCordinates(string s, ref int x, ref int y)
        {
            string[] ss = s.Split('_');
            y = Convert.ToInt32(ss[0]);
            x = Convert.ToInt32(ss[1]);
        }

        private void openClick(int x, int y, int depth = 0)
        {
            if (Spielfeld[y][x] == 0)
            {
                this.Controls.Remove(ButtonArray[y][x]);
                checkedFields.Add(new Point(x, y));
                bool top = true, bottom = true, left = true, right = true;
                if (x == 0)
                    left = false;
                if (x == Spielfeld[0].Length - 1)
                    right = false;
                if (y == 0)
                    top = false;
                if (y == Spielfeld.Length - 1)
                    bottom = false;

                if (top)
                {
                    if (left)
                        if (Spielfeld[y - 1][x - 1] < 10 && !checkedFields.Contains(new Point(x - 1, y - 1)))
                            openClick(x - 1, y - 1, depth + 1);
                    if (Spielfeld[y - 1][x] < 10 && !checkedFields.Contains(new Point(x, y - 1)))
                        openClick(x, y - 1, depth + 1);
                    if (right)
                        if (Spielfeld[y - 1][x + 1] < 10 && !checkedFields.Contains(new Point(x + 1, y - 1)))
                            openClick(x + 1, y - 1, depth + 1);
                }
                if (left)
                    if (Spielfeld[y][x - 1] < 10 && !checkedFields.Contains(new Point(x - 1, y)))
                        openClick(x - 1, y, depth + 1);
                if (right)
                    if (Spielfeld[y][x + 1] < 10 && !checkedFields.Contains(new Point(x + 1, y)))
                        openClick(x + 1, y, depth + 1);
                if (bottom)
                {
                    if(left)
                        if (Spielfeld[y + 1][x - 1] < 10 && !checkedFields.Contains(new Point(x - 1, y + 1)))
                            openClick(x - 1, y + 1, depth + 1);
                    if (Spielfeld[y + 1][x] < 10 && !checkedFields.Contains(new Point(x, y + 1)))
                        openClick(x, y + 1, depth + 1);
                    if(right)
                        if (Spielfeld[y + 1][x + 1] < 10 && !checkedFields.Contains(new Point(x + 1, y + 1)))
                            openClick(x + 1, y + 1, depth + 1);
                }
            }
            if (Spielfeld[y][x] < 10)
            {
                this.Controls.Remove(ButtonArray[y][x]);
                checkedFields.Add(new Point(x, y));
            }
        }

        private void paintLines()
        {
            
        }

        private void retry()
        {
            switch (Spielfeld[0].Length)
            {
                case 9:
                    leichtToolStripMenuItem_Click(leichtToolStripMenuItem, new EventArgs());
                    break;
                case 16:
                    mittelToolStripMenuItem_Click(mittelToolStripMenuItem, new EventArgs());
                    break;
                case 30:
                    schwerToolStripMenuItem_Click(schwerToolStripMenuItem, new EventArgs());
                    break;
            }
        }

        private void start()
        {
            gameStarted = true;
            generateSpielfeld();
            for (int y = 0; y < Spielfeld.Length; y++)
            {
                for (int x = 0; x < Spielfeld[0].Length; x++)
                {
                    LabelArray[y][x] = new Label();
                    LabelArray[y][x].AutoSize = false;
                    LabelArray[y][x].Location = new System.Drawing.Point((7+(x*20))+2, (27+(y*20))+2);
                    LabelArray[y][x].Name = x.ToString() + "_" + y.ToString();
                    LabelArray[y][x].Size = new System.Drawing.Size(16, 16);
                    LabelArray[y][x].TabIndex = ((Spielfeld.Length * y) + x) * 100;
                    if (Spielfeld[y][x] == iMine)
                        LabelArray[y][x].Image = MineSweeper.Properties.Resources.mine;
                    else if (Spielfeld[y][x] == 0)
                        LabelArray[y][x].Text = "";
                    else
                    {
                        LabelArray[y][x].Text = Spielfeld[y][x].ToString();
                        switchColor(x, y);
                    }
                    LabelArray[y][x].TextAlign = ContentAlignment.MiddleCenter;
                    ButtonArray[y][x] = new Button();
                    ButtonArray[y][x].Location = new System.Drawing.Point(7+(x*20), 27+(y*20));
                    ButtonArray[y][x].FlatStyle = FlatStyle.Flat;
                    ButtonArray[y][x].BackColor = Color.FromArgb(65, 84, 194);
                    ButtonArray[y][x].FlatAppearance.MouseOverBackColor = Color.FromArgb(88, 138, 244);
                    ButtonArray[y][x].Name = y.ToString() + "_" + x.ToString();
                    ButtonArray[y][x].Size = new System.Drawing.Size(20, 20);
                    ButtonArray[y][x].TabIndex = (Spielfeld.Length * y) + x;
                    ButtonArray[y][x].Text = "";
                    ButtonArray[y][x].UseVisualStyleBackColor = true;
                    ButtonArray[y][x].MouseUp += new MouseEventHandler(Mine_Click);
                    ButtonArray[y][x].BringToFront();
                }
                this.Controls.AddRange(ButtonArray[y]);
                this.Controls.AddRange(LabelArray[y]);
            }
            menuStrip1.Items.AddRange(new ToolStripItem[] { tslTime, tslMine });
            tslTime.Text = "00";
            tmr_game.Enabled = true;
        }

        private void switchColor(int x, int y)
        {
            switch (Spielfeld[y][x])
            {
                case 1:
                    LabelArray[y][x].ForeColor = Color.Blue;
                    break;
                case 2:
                    LabelArray[y][x].ForeColor = Color.Green;
                    break;
                case 3:
                    LabelArray[y][x].ForeColor = Color.Red;
                    break;
                case 4:
                    LabelArray[y][x].ForeColor = Color.DarkBlue;
                    break;
                case 5:
                    LabelArray[y][x].ForeColor = Color.Brown;
                    break;
                case 6:
                    LabelArray[y][x].ForeColor = Color.Cyan;
                    break;
                case 7:
                    LabelArray[y][x].ForeColor = Color.Purple;
                    break;
                case 8:
                    LabelArray[y][x].ForeColor = Color.Black;
                    break;
            }
            LabelArray[y][x].Font = new Font("Arial", (float)10, FontStyle.Bold);
        }
        #endregion
    }
}
