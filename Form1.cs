using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BlockGo_ControlPanel
{
    public partial class Form1 : Form
    {

        delegate void update1(string str);
        delegate void upadte2(int[,] Board);
        delegate void upadte3(bool en);

        Hashtable ButtonTable = new Hashtable();
        Board Board = new Board();
        Rule Rule = new Rule();
        MCTS_Array MCTS_Array = new MCTS_Array();


        public Form1()
        {
            InitializeComponent();
        }

        //�e���Ұ�
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = false;//����w
            EnablePlayerInput(false);
            EnableSelectGameMode(false);

            int startX = 10;
            int startY = 10;
            int gap = 1;
            int sizeX = 45, sizeY = 45;

            for (int r = 0; r < 13; r++)
            {
                for (int c = 0; c < 13; c++)
                {
                    int x = startX + c * (sizeX + gap);
                    int y = startY + r * (sizeY + gap);

                    var btn = new Button();
                    btn.BackColor = Color.FromArgb(164, 89, 28);
                    btn.Text = ((c + 1) + (r * 13)).ToString();
                    btn.Size = new Size(sizeX, sizeY);
                    btn.Location = new Point(x, y);
                    Controls.Add(btn);
                    ButtonTable.Add(btn.Text, btn);
                    btn.Click += BoardBtn_Click;// �[�J Click �ƥ�
                }
            }

            EnableSelectGameMode(true);
            this.Enabled = true;
        }


        //���a���U���s(�ѽL)
        private void BoardBtn_Click(object sender, EventArgs e)
        {
            if (btnConfirm.Enabled == false) return;            
            Button btn = (Button)sender;
            if (btn.BackColor.R != 164 || btn.BackColor.G != 89 || btn.BackColor.B != 28)
            {
                ReNewMsg("*����m�L�k�U��*");
                return;
            }
            
            btn.BackColor = Color.FromArgb(168, 168, 168);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /*
            0. MCTS VS MCTS
            1. ���a VS MCTS
            */

            if (GameModeSelect.SelectedIndex == 0)
            {
                ReNewMsg("*�Ҧ� : " + GameModeSelect.SelectedItem + "*");
                ReNewMsg("******\nMCTS���۹�ԼҦ��AMCTS���ƽбq�����{���ק�\n******");
                Task.Factory.StartNew(AItoAI);
            }
            else if (GameModeSelect.SelectedIndex == 1)
            {
                try
                {
                    if (Convert.ToInt16(txtMCTStime.Text) < 50 || Convert.ToInt16(txtMCTStime.Text) > 10000)
                    {
                        ReNewMsg("*MCTS���Ƥ��o�C��50�άO����10000*");
                        return;
                    }
                }
                catch(Exception ex)
                {
                    ReNewMsg("*�п�JMCTS����*");
                    return;
                }

                Form2 Form2 = new Form2();
                if (Form2.ShowDialog() == DialogResult.OK)
                {
                    ReNewMsg("*�Ҧ� : " + GameModeSelect.SelectedItem + "*");

                    if (Form2.ResultMessage == "FirstPlayer")
                    {
                        ReNewMsg("*���a'��'��*");
                    }
                    else if (Form2.ResultMessage == "SecondPlayer")
                    {
                        ReNewMsg("*���a'��'��*");
                    }

                    Task.Factory.StartNew(PlayerToAI, Form2.ResultMessage.ToString());
                }
                else
                {
                    ReNewMsg("**���a��������**");
                }
            }
            else
            {
                ReNewMsg("**�Х���ܹC���Ҧ�**");
            }

        }


        int[,] TempBoard = new int[13, 13];
        int[,] TempCheck = new int[2, 9];
        int TempPlayer = 1;//1����,2���

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int InputCount = 0;
            int[] InputPosition = new int[4] { 0, 0, 0, 0 };

            for (int i = 1; i <= 169; i++)
            {
                Button btn = (Button)ButtonTable[i.ToString()];
                if (btn.BackColor.R == 168 && btn.BackColor.G == 168 && btn.BackColor.B == 168)
                {
                    if(InputCount == 4)
                    {
                        ReNewMsg("�W�X���l�ƶq");
                        return;
                    }

                    //���a���U����m
                    InputPosition[InputCount] = i;
                    InputCount++;
                }
            }

            //------------�˴����L��ܴѫ�

            if (PlayerShapeBox.SelectedItem == null || PlayerShapeBox.SelectedItem.ToString().Trim() == "")
            {
                ReNewMsg("������ܴѫ�");
                return;
            }

            //------------�˴����l�ƶq
            if (InputCount < 1 || InputCount > 4)
            {
                ReNewMsg("���l�ƶq������4�άO��1");
                return;
            }

            //------------�˴��O�_���ڬP��
            Button btn43 = (Button)ButtonTable["43"];
            Button btn49 = (Button)ButtonTable["49"];
            Button btn121 = (Button)ButtonTable["121"];
            Button btn127 = (Button)ButtonTable["127"];

            if ((btn43.BackColor.R == 164 && btn43.BackColor.G == 89 && btn43.BackColor.B == 28)
                ||(btn49.BackColor.R == 164 && btn49.BackColor.G == 89 && btn49.BackColor.B == 28)
                ||(btn121.BackColor.R == 164 && btn121.BackColor.G == 89 && btn121.BackColor.B == 28)
                ||(btn127.BackColor.R == 164 && btn127.BackColor.G == 89 && btn127.BackColor.B == 28))
            {
                //�u�n���@�P�쬰�šA�N�ˬd���a�O�_��u�e�ڬP��W�h
                for (int i = 0; i < InputCount; i++)
                {
                    if(InputPosition[i]  == 43 
                       || InputPosition[i] == 49
                       || InputPosition[i] == 121
                       || InputPosition[i] == 127)
                    {
                        break;
                    }

                    if(i == InputCount-1)
                    {
                        ReNewMsg("�P�즳�šA�����U�b�P��");
                        return;
                    }
                }
            }

            //------------�˴��s��
            //������

            //------------�˴���ܴѫ��P��ڴѫ��O�_�۲�
            //������

            //------------�ѫ��P���a���l��s�^TempBoard
            //������

            for (int i = 0; i < InputCount; i++)
            {
                int XY = InputPosition[i];
                int Y = (XY - 1) / 13;
                int X = (XY - 1) % 13;
                TempBoard[X, Y] = TempPlayer;
            }
            TempCheck[TempPlayer - 1, Convert.ToInt16(PlayerShapeBox.SelectedItem.ToString()) - 1] = -1;
            ReNewBoard(TempBoard);

            ReNewMsg("���l����");
            EnablePlayerInput(false);
            TaskWaitControl.Set();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReNewMsg("���l����");
            ReNewBoard(TempBoard);
        }

        //-------------------------��������s�Ψ禡-------------------------
        private void ReNewMsg(string str)
        {
            txtMsg.Text += "\n" + str;
            txtMsg.SelectionStart = txtMsg.TextLength; // �ⴡ�J�I����̫�
            txtMsg.ScrollToCaret();                    // �۰ʱ���̩�
        }

        private void ReNewBoard(int[,] Board)
        {
            for (int j = 0; j < 13; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    //���qX�}�l��
                    if (Board[i, j] == 0)
                    {
                        Button btn = (Button)ButtonTable[((i + 1) + (j * 13)).ToString()];
                        btn.BackColor = Color.FromArgb(164, 89, 28);
                    }
                    else if (Board[i, j] == 1)
                    {
                        Button btn = (Button)ButtonTable[((i + 1) + (j * 13)).ToString()];
                        btn.BackColor = Color.FromArgb(42, 42, 42);
                    }
                    else if (Board[i, j] == 2)
                    {
                        Button btn = (Button)ButtonTable[((i + 1) + (j * 13)).ToString()];
                        btn.BackColor = Color.FromArgb(255, 255, 255);
                    }

                }
            }
        }

        private void EnablePlayerInput(bool en)
        {
            PlayerShapeBox.Items.Clear();
            if (en == true)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (TempCheck[TempPlayer - 1, i] != -1)
                    {
                        PlayerShapeBox.Items.Add((i + 1).ToString());
                    }
                }
            }
            PlayerShapeBox.Enabled = en;
            btnConfirm.Enabled = en;
            btnCancel.Enabled = en;
        }

        private void EnableSelectGameMode(bool en)
        {
            btnOK.Enabled = en;
            GameModeSelect.Enabled = en;
            txtMCTStime.Enabled = en;
        }

        //-------------------------�DUI������u�@��-------------------------

        private readonly AutoResetEvent TaskWaitControl = new(initialState: false);

        private void AItoAI()
        {
            update1 updateMsg = ReNewMsg;
            upadte2 updateBoard = ReNewBoard;
            upadte3 updateBtn = EnableSelectGameMode;

            int[,] Check_SingleMode = new int[2, 9];//�ثe��ڪ��a(AI)�ϥΪ��ѫ�
            int[,] Board_SingleMode = new int[13, 13];//�ثe��ڪ��p���ѽL

            Invoke(updateBtn, false);//����ܹC���Ҧ��P�}�l���s
            Invoke(updateBoard, Board_SingleMode);//���M�ŵe�����ѽL
            Invoke(updateMsg, "***�C���}�l***");

            //��18�^�X
            for (int i = 0; i < 18; i++)
            {
                if (i % 2 != 0)
                {
                    //���(�դl)
                    MCTS_Array.select(1000, 2, Check_SingleMode, Board_SingleMode);
                    Invoke(updateMsg, "\a�դl��ҧ���");
                }
                else
                {
                    //����(�¤l)
                    MCTS_Array.select(1000, 1, Check_SingleMode, Board_SingleMode);
                    Invoke(updateMsg, "\a�¤l��ҧ���");
                }

                Invoke(ReNewBoard, Board_SingleMode);
            }

            //�ˬd�O�ֳӧQ
            bool[] Result = new bool[3] { false, false, false };
            Result = Rule.IsWin(Check_SingleMode, Board_SingleMode);

            if (Result[0] == true)
            {
                Invoke(updateMsg, "�C�������A����");
            }
            else if (Result[1] == true)
            {
                Invoke(updateMsg, "�C�������A�¤lĹ");
            }
            else if (Result[2] == true)
            {
                Invoke(updateMsg, "�C�������A�դlĹ");
            }

            Invoke(updateBtn, true);//�Ѱ���w�C���Ҧ��P�}�l���s
        }

        private void PlayerToAI(object WhoFirst)
        {
            update1 updateMsg = ReNewMsg;
            upadte2 updateBoard = ReNewBoard;
            upadte3 updateBtn = EnablePlayerInput;
            upadte3 updateBtn2 = EnableSelectGameMode;

            int[,] Check_SingleMode = new int[2, 9];//�ثe��ڪ��a(AI)�ϥΪ��ѫ�
            int[,] Board_SingleMode = new int[13, 13];//�ثe��ڪ��p���ѽL

            Invoke(updateBtn2, false);//����ܹC���Ҧ��P�}�l���s
            Invoke(ReNewBoard, Board_SingleMode);//���M�ŵe�����ѽL
            Invoke(updateMsg, "***�C���}�l***");

            //��18�^�X
            for (int i = 0; i < 18; i++)
            {
                if (WhoFirst.ToString() == "FirstPlayer")
                {
                    //���a����
                    if (i % 2 != 0)
                    {
                        //���(�դl)
                        MCTS_Array.select(50, 2, Check_SingleMode, Board_SingleMode);
                        Invoke(updateMsg, "\a�դl��ҧ���");
                    }
                    else
                    {
                        //����(�¤l)
                        Array.Copy(Board_SingleMode, TempBoard, 13 * 13);
                        Array.Copy(Check_SingleMode, TempCheck, 2 * 9);
                        TempPlayer = 1;
                        Invoke(updateMsg, "����¤l(���a)�A�и��l�K");
                        Invoke(updateBtn, true);
                        TaskWaitControl.WaitOne();  //�o�̼Ȱ��A��UI�ƥ�TaskWaitControl.Set()
                        Array.Copy(TempBoard, Board_SingleMode, 13 * 13);
                        Array.Copy(TempCheck, Check_SingleMode, 2 * 9);
                    }
                }
                else
                {
                    //���a���
                    if (i % 2 != 0)
                    {
                        //���(�դl)
                        Array.Copy(Board_SingleMode, TempBoard, 13 * 13);
                        Array.Copy(Check_SingleMode, TempCheck, 2 * 9);
                        TempPlayer = 2;
                        Invoke(updateMsg, "����դl(���a)�A�и��l�K");
                        Invoke(updateBtn, true);
                        TaskWaitControl.WaitOne();  //�o�̼Ȱ��A��UI�ƥ�TaskWaitControl.Set()
                        Array.Copy(TempBoard, Board_SingleMode, 13 * 13);
                        Array.Copy(TempCheck, Check_SingleMode, 2 * 9);
                    }
                    else
                    {
                        //����(�¤l)
                        MCTS_Array.select(50, 1, Check_SingleMode, Board_SingleMode);
                        Invoke(updateMsg, "\a�¤l��ҧ���");
                    }
                }

                Invoke(ReNewBoard, Board_SingleMode);
            }

            //�ˬd�O�ֳӧQ
            bool[] Result = new bool[3] { false, false, false };
            Result = Rule.IsWin(Check_SingleMode, Board_SingleMode);

            if (Result[0] == true)
            {
                Invoke(updateMsg, "�C�������A����");
            }
            else if (Result[1] == true)
            {
                Invoke(updateMsg, "�C�������A�¤lĹ");
            }
            else if (Result[2] == true)
            {
                Invoke(updateMsg, "�C�������A�դlĹ");
            }

            Invoke(updateBtn2, true);//�Ѱ���w�C���Ҧ��P�}�l���s
        }





        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }













        //------------------------------------------------------------------------------------------------------------
    }
}
