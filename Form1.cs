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

        //畫面啟動
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = false;//先鎖定
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
                    btn.Click += BoardBtn_Click;// 加入 Click 事件
                }
            }

            EnableSelectGameMode(true);
            this.Enabled = true;
        }


        //玩家按下按鈕(棋盤)
        private void BoardBtn_Click(object sender, EventArgs e)
        {
            if (btnConfirm.Enabled == false) return;            
            Button btn = (Button)sender;
            if (btn.BackColor.R != 164 || btn.BackColor.G != 89 || btn.BackColor.B != 28)
            {
                ReNewMsg("*此位置無法下棋*");
                return;
            }
            
            btn.BackColor = Color.FromArgb(168, 168, 168);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            /*
            0. MCTS VS MCTS
            1. 玩家 VS MCTS
            */

            if (GameModeSelect.SelectedIndex == 0)
            {
                ReNewMsg("*模式 : " + GameModeSelect.SelectedItem + "*");
                ReNewMsg("******\nMCTS互相對戰模式，MCTS次數請從內部程式修改\n******");
                Task.Factory.StartNew(AItoAI);
            }
            else if (GameModeSelect.SelectedIndex == 1)
            {
                try
                {
                    if (Convert.ToInt16(txtMCTStime.Text) < 50 || Convert.ToInt16(txtMCTStime.Text) > 10000)
                    {
                        ReNewMsg("*MCTS次數不得低於50或是高於10000*");
                        return;
                    }
                }
                catch(Exception ex)
                {
                    ReNewMsg("*請輸入MCTS次數*");
                    return;
                }

                Form2 Form2 = new Form2();
                if (Form2.ShowDialog() == DialogResult.OK)
                {
                    ReNewMsg("*模式 : " + GameModeSelect.SelectedItem + "*");

                    if (Form2.ResultMessage == "FirstPlayer")
                    {
                        ReNewMsg("*玩家'先'手*");
                    }
                    else if (Form2.ResultMessage == "SecondPlayer")
                    {
                        ReNewMsg("*玩家'後'手*");
                    }

                    Task.Factory.StartNew(PlayerToAI, Form2.ResultMessage.ToString());
                }
                else
                {
                    ReNewMsg("**玩家關閉視窗**");
                }
            }
            else
            {
                ReNewMsg("**請先選擇遊戲模式**");
            }

        }


        int[,] TempBoard = new int[13, 13];
        int[,] TempCheck = new int[2, 9];
        int TempPlayer = 1;//1先手,2後手

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
                        ReNewMsg("超出落子數量");
                        return;
                    }

                    //玩家欲下的位置
                    InputPosition[InputCount] = i;
                    InputCount++;
                }
            }

            //------------檢測有無選擇棋型

            if (PlayerShapeBox.SelectedItem == null || PlayerShapeBox.SelectedItem.ToString().Trim() == "")
            {
                ReNewMsg("必須選擇棋型");
                return;
            }

            //------------檢測落子數量
            if (InputCount < 1 || InputCount > 4)
            {
                ReNewMsg("落子數量必須為4或是為1");
                return;
            }

            //------------檢測是否佔據星位
            Button btn43 = (Button)ButtonTable["43"];
            Button btn49 = (Button)ButtonTable["49"];
            Button btn121 = (Button)ButtonTable["121"];
            Button btn127 = (Button)ButtonTable["127"];

            if ((btn43.BackColor.R == 164 && btn43.BackColor.G == 89 && btn43.BackColor.B == 28)
                ||(btn49.BackColor.R == 164 && btn49.BackColor.G == 89 && btn49.BackColor.B == 28)
                ||(btn121.BackColor.R == 164 && btn121.BackColor.G == 89 && btn121.BackColor.B == 28)
                ||(btn127.BackColor.R == 164 && btn127.BackColor.G == 89 && btn127.BackColor.B == 28))
            {
                //只要有一星位為空，就檢查玩家是否遵守占據星位規則
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
                        ReNewMsg("星位有空，必須下在星位");
                        return;
                    }
                }
            }

            //------------檢測連氣
            //未完成

            //------------檢測選擇棋型與實際棋型是否相符
            //未完成

            //------------棋型與玩家落子更新回TempBoard
            //未完成

            for (int i = 0; i < InputCount; i++)
            {
                int XY = InputPosition[i];
                int Y = (XY - 1) / 13;
                int X = (XY - 1) % 13;
                TempBoard[X, Y] = TempPlayer;
            }
            TempCheck[TempPlayer - 1, Convert.ToInt16(PlayerShapeBox.SelectedItem.ToString()) - 1] = -1;
            ReNewBoard(TempBoard);

            ReNewMsg("落子完畢");
            EnablePlayerInput(false);
            TaskWaitControl.Set();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReNewMsg("落子取消");
            ReNewBoard(TempBoard);
        }

        //-------------------------跨執行緒更新用函式-------------------------
        private void ReNewMsg(string str)
        {
            txtMsg.Text += "\n" + str;
            txtMsg.SelectionStart = txtMsg.TextLength; // 把插入點移到最後
            txtMsg.ScrollToCaret();                    // 自動捲到最底
        }

        private void ReNewBoard(int[,] Board)
        {
            for (int j = 0; j < 13; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    //先從X開始走
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

        //-------------------------非UI執行緒工作區-------------------------

        private readonly AutoResetEvent TaskWaitControl = new(initialState: false);

        private void AItoAI()
        {
            update1 updateMsg = ReNewMsg;
            upadte2 updateBoard = ReNewBoard;
            upadte3 updateBtn = EnableSelectGameMode;

            int[,] Check_SingleMode = new int[2, 9];//目前實際玩家(AI)使用的棋型
            int[,] Board_SingleMode = new int[13, 13];//目前實際狀況的棋盤

            Invoke(updateBtn, false);//鎖住選擇遊戲模式與開始按鈕
            Invoke(updateBoard, Board_SingleMode);//先清空畫面的棋盤
            Invoke(updateMsg, "***遊戲開始***");

            //比18回合
            for (int i = 0; i < 18; i++)
            {
                if (i % 2 != 0)
                {
                    //後手(白子)
                    MCTS_Array.select(1000, 2, Check_SingleMode, Board_SingleMode);
                    Invoke(updateMsg, "\a白子思考完畢");
                }
                else
                {
                    //先手(黑子)
                    MCTS_Array.select(1000, 1, Check_SingleMode, Board_SingleMode);
                    Invoke(updateMsg, "\a黑子思考完畢");
                }

                Invoke(ReNewBoard, Board_SingleMode);
            }

            //檢查是誰勝利
            bool[] Result = new bool[3] { false, false, false };
            Result = Rule.IsWin(Check_SingleMode, Board_SingleMode);

            if (Result[0] == true)
            {
                Invoke(updateMsg, "遊戲結束，平手");
            }
            else if (Result[1] == true)
            {
                Invoke(updateMsg, "遊戲結束，黑子贏");
            }
            else if (Result[2] == true)
            {
                Invoke(updateMsg, "遊戲結束，白子贏");
            }

            Invoke(updateBtn, true);//解除鎖定遊戲模式與開始按鈕
        }

        private void PlayerToAI(object WhoFirst)
        {
            update1 updateMsg = ReNewMsg;
            upadte2 updateBoard = ReNewBoard;
            upadte3 updateBtn = EnablePlayerInput;
            upadte3 updateBtn2 = EnableSelectGameMode;

            int[,] Check_SingleMode = new int[2, 9];//目前實際玩家(AI)使用的棋型
            int[,] Board_SingleMode = new int[13, 13];//目前實際狀況的棋盤

            Invoke(updateBtn2, false);//鎖住選擇遊戲模式與開始按鈕
            Invoke(ReNewBoard, Board_SingleMode);//先清空畫面的棋盤
            Invoke(updateMsg, "***遊戲開始***");

            //比18回合
            for (int i = 0; i < 18; i++)
            {
                if (WhoFirst.ToString() == "FirstPlayer")
                {
                    //玩家先手
                    if (i % 2 != 0)
                    {
                        //後手(白子)
                        MCTS_Array.select(50, 2, Check_SingleMode, Board_SingleMode);
                        Invoke(updateMsg, "\a白子思考完畢");
                    }
                    else
                    {
                        //先手(黑子)
                        Array.Copy(Board_SingleMode, TempBoard, 13 * 13);
                        Array.Copy(Check_SingleMode, TempCheck, 2 * 9);
                        TempPlayer = 1;
                        Invoke(updateMsg, "輪到黑子(玩家)，請落子…");
                        Invoke(updateBtn, true);
                        TaskWaitControl.WaitOne();  //這裡暫停，等UI事件TaskWaitControl.Set()
                        Array.Copy(TempBoard, Board_SingleMode, 13 * 13);
                        Array.Copy(TempCheck, Check_SingleMode, 2 * 9);
                    }
                }
                else
                {
                    //玩家後手
                    if (i % 2 != 0)
                    {
                        //後手(白子)
                        Array.Copy(Board_SingleMode, TempBoard, 13 * 13);
                        Array.Copy(Check_SingleMode, TempCheck, 2 * 9);
                        TempPlayer = 2;
                        Invoke(updateMsg, "輪到白子(玩家)，請落子…");
                        Invoke(updateBtn, true);
                        TaskWaitControl.WaitOne();  //這裡暫停，等UI事件TaskWaitControl.Set()
                        Array.Copy(TempBoard, Board_SingleMode, 13 * 13);
                        Array.Copy(TempCheck, Check_SingleMode, 2 * 9);
                    }
                    else
                    {
                        //先手(黑子)
                        MCTS_Array.select(50, 1, Check_SingleMode, Board_SingleMode);
                        Invoke(updateMsg, "\a黑子思考完畢");
                    }
                }

                Invoke(ReNewBoard, Board_SingleMode);
            }

            //檢查是誰勝利
            bool[] Result = new bool[3] { false, false, false };
            Result = Rule.IsWin(Check_SingleMode, Board_SingleMode);

            if (Result[0] == true)
            {
                Invoke(updateMsg, "遊戲結束，平手");
            }
            else if (Result[1] == true)
            {
                Invoke(updateMsg, "遊戲結束，黑子贏");
            }
            else if (Result[2] == true)
            {
                Invoke(updateMsg, "遊戲結束，白子贏");
            }

            Invoke(updateBtn2, true);//解除鎖定遊戲模式與開始按鈕
        }





        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }













        //------------------------------------------------------------------------------------------------------------
    }
}
