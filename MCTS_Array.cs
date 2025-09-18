using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{

    public class ArrayTree
    {
        static int MaxTreeNum = 900000;

        public int TreeNum = 0;
        public int[] Tree = new int[MaxTreeNum];
        public int[] Win = new int[MaxTreeNum];
        public int[] Visit = new int[MaxTreeNum];
        public double[] UCB = new double[MaxTreeNum];
        public int[] Child = new int[MaxTreeNum];
        public int[] Father = new int[MaxTreeNum];
    }

    internal class MCTS_Array
    {
        ArrayTree ArrayTree = new ArrayTree();
        Random Rd = new Random();
        Board Board = new Board();
        Rule Rule = new Rule();
        Parameter Parameter = new Parameter();
        ShapeCodeConversion shapeCodeConversion = new ShapeCodeConversion();


        public double CalculateUCB(float Win, int Visit, int AllVisit, bool UseCuriosity = true)
        {
            double WinningRate = Win / Visit;
            double Curiosity = 2 * Math.Sqrt(Math.Log10(AllVisit) / Visit);
            if (UseCuriosity == false) Curiosity = 0;
            double UCB = WinningRate + Curiosity;
            return UCB;
        }


        public void Input(int[,] Board, int[,] Check, int num)
        {
            int X = 0, Y = 0, S = 0, R = 0;
            decoder(ref X, ref Y, ref S, ref R, num);

            int Player = 0;
            decoder(ref Player, num);

            int[,] ToBoard = shapeCodeConversion.PositionToBoard(X, Y, S, R);
            for (int i = 0; i < 4; i++) Board[ToBoard[i, 0], ToBoard[i, 1]] = Player;
            Check[Player - 1, S] = -1;
        }

        public void Input(int[] Tree, int[,] Board, int[,] Check, int num)
        {
            ShapeCodeConversion shapeCodeConversion = new ShapeCodeConversion();

            int XYSR = Convert.ToInt32(Tree[num] % 1000000);
            int[] SR = shapeCodeConversion.ToShapeRoate(Convert.ToInt16(XYSR % 100));
            int X = XYSR / 10000;
            int Y = (XYSR / 100) % 100;
            int S = SR[0];
            int R = SR[1];
            int Player = Tree[num] / 1000000;
            int[,] ToBoard = shapeCodeConversion.PositionToBoard(X, Y, S, R);
            for (int i = 0; i < 4; i++) Board[ToBoard[i, 0], ToBoard[i, 1]] = Player;
            Check[Player - 1, S] = -1;
        }

        private void decoder(ref int X, ref int Y, ref int S, ref int R, int Num)
        {
            int XYSR = Convert.ToInt32(ArrayTree.Tree[Num] % 1000000);
            int[] SR = shapeCodeConversion.ToShapeRoate(Convert.ToInt16(XYSR % 100));
            X = XYSR / 10000;
            Y = (XYSR / 100) % 100;
            S = SR[0];
            R = SR[1];
        }

        private void decoder(ref int Player, int Num)
        {
            Player = ArrayTree.Tree[Num] / 1000000;
        }


        public void select(int MCTS_TIME, int RealPlayer, int[,] RealCheck, int[,] RealBoard)
        {
            Dimension Dimension = new Dimension();
            ArrayTree = new ArrayTree();

            for (int _ = 0; _ < MCTS_TIME; _++)
            {
                //Console.WriteLine(_.ToString());
                int SimPlayer = RealPlayer;
                int[,] SimCheck = new int[2, 9];
                int[,] SimBoard = new int[13, 13];
                Array.Copy(RealCheck, SimCheck, 2 * 9);
                Array.Copy(RealBoard, SimBoard, 13 * 13);

                if (_ == 0)
                {
                    //var currentProcess = Process.GetCurrentProcess();
                    //Console.WriteLine($"Fasr Rollout Total Threads: {currentProcess.Threads.Count}");

                    //Tree.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, -1);
                    Expansion(SimBoard, SimCheck, SimPlayer, 0);
                }
                else
                {
                    int Start = 1;
                    double MaxUCB = -10;
                    int MaxUCBnum = -1;
                    while (true)
                    {
                        int Win = ArrayTree.Win[Start];
                        int Visit = ArrayTree.Visit[Start];
                        double UCB = ArrayTree.UCB[Start];
                        int Father = ArrayTree.Father[Start];

                        if (Visit != 0)
                        {
                            //if (Father == 0) UCB = CalculateUCB(Win, Visit, _);
                            //else UCB = CalculateUCB(Win, Visit, Convert.ToInt32(Tree.Rows[Father][6].ToString()));
                            //Tree.Rows[Start][7] = UCB;

                            if (Father == 0) UCB = CalculateUCB(Win, Visit, _);
                            else UCB = CalculateUCB(Win, Visit, ArrayTree.Visit[Father]);
                            ArrayTree.UCB[Start] = UCB;
                        }

                        if (UCB > MaxUCB || (UCB == MaxUCB && Rd.Next(1, 101) >= 50))
                        {
                            MaxUCB = UCB;
                            MaxUCBnum = Start;
                        }

                        if (Start == ArrayTree.TreeNum)
                        {
                            //leaf node
                            Input(SimBoard, SimCheck, MaxUCBnum);

                            break;
                        }

                        if (Father != ArrayTree.Father[Start + 1])
                        {
                            //The last node of this layer
                            if (ArrayTree.Child[MaxUCBnum] == 0)
                            {
                                //leaf node
                                Input(SimBoard, SimCheck, MaxUCBnum);
                                if (SimPlayer == 1) SimPlayer = 2;
                                else SimPlayer = 1;

                                break;
                            }
                            else
                            {
                                //next layer
                                Input(SimBoard, SimCheck, MaxUCBnum);

                                Start = ArrayTree.Child[MaxUCBnum];
                                MaxUCB = -10;
                                MaxUCBnum = -1;
                                continue;
                            }
                        }

                        Start++;
                    }

                    decoder(ref SimPlayer, MaxUCBnum);
                    if (SimPlayer == 1) SimPlayer = 2;
                    else SimPlayer = 1;

                    int leftCheck = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (SimCheck[i, j] != -1) leftCheck++;
                        }
                    }

                    if (leftCheck == 0)
                    {
                        //已經沒有著手 但MCTS尚未結束
                        //Back(ref Tree, SimBoard, MaxUCBnum);
                    }
                    else
                    {
                        Expansion(SimBoard, SimCheck, SimPlayer, MaxUCBnum);
                    }
                }
            }

            //find the winner of the first layer
            double MaxWinner = -10;
            int MaxWinnerNum = -1;
            for (int i = 1; i <= ArrayTree.TreeNum; i++)
            {
                //X,Y,S,R,Player,Win,Visit,UCB,Father,Child

                int Father = ArrayTree.Father[i];
                if (Father != 0) break;

                int Win = ArrayTree.Win[i];
                int Visit = ArrayTree.Visit[i];

                if (Visit != 0)
                {
                    double Winner = CalculateUCB(Win, Visit, MCTS_TIME, false);
                    if (Winner > MaxWinner || (Winner == MaxWinner && Rd.Next(1, 101) >= 50))
                    {
                        MaxWinner = Winner;
                        MaxWinnerNum = i;
                    }
                }
            }

            //change the real board state
            Input(RealBoard, RealCheck, MaxWinnerNum);
        }


        private void Expansion(int[,] Board, int[,] Check, int Player, int Father)
        {
            Dimension Dimension = new Dimension();

            int num = 0;

            for (int k = 0; k < 3549; k++)
            {
                string Action = Dimension.ActionToBoard(k);
                int X = Convert.ToInt16(Action.Split(';')[0]);
                int Y = Convert.ToInt16(Action.Split(';')[1]);
                int[] SR = shapeCodeConversion.ToShapeRoate(Convert.ToInt16(Action.Split(';')[2]));

                if (Check[Player - 1, SR[0]] == 0)
                {
                    if (Rule.CanInput(Player, Check, Board, k) == true)
                    {
                        num++;

                        ArrayTree.TreeNum++;

                        //Player(1 or 2),X,Y,SR(21)
                        ArrayTree.Tree[ArrayTree.TreeNum] = Player * 1000000 + X * 10000 + Y * 100 + Convert.ToInt16(Action.Split(';')[2]);

                        //Win(擴展階段補0),Visit(擴展階段補0)
                        ArrayTree.Win[ArrayTree.TreeNum] = 0;
                        ArrayTree.Visit[ArrayTree.TreeNum] = 0;

                        //擴展階段UCB都設定成10
                        ArrayTree.UCB[ArrayTree.TreeNum] = 10;


                        ArrayTree.Father[ArrayTree.TreeNum] = Father;
                        ArrayTree.Child[ArrayTree.TreeNum] = 0;
                    }
                }
            }

            if (num > 0)
            {
                int head = (int)(ArrayTree.TreeNum + 1) - num;
                if (Father != 0) ArrayTree.Child[Father] = head;
                int UseNodeNum = head + Rd.Next(0, num);
                Rollout(Board, Check, Player, UseNodeNum);
            }
            else
            {
                //Console.WriteLine("..MCTS Expansion Down..");
                return;
            }
        }


        private void Rollout(int[,] Board, int[,] Check, int Player, int UseNodeNum)
        {
            ShapeCodeConversion shapeCodeConversion = new ShapeCodeConversion();
            Dimension Dimension = new Dimension();

            Input(Board, Check, UseNodeNum);

            while (true)
            {
                bool IsEndGame = true;
                foreach (int i in Check)
                {
                    if (i == 0) IsEndGame = false;
                    break;
                }
                if (IsEndGame == true) break;

                //X,Y,S,R,Player
                //DataTable RandomTree = new DataTable();
                //RandomTree.Columns.Add("X", typeof(int));
                //RandomTree.Columns.Add("Y", typeof(int));
                //RandomTree.Columns.Add("Shape", typeof(int));
                //RandomTree.Columns.Add("Roate", typeof(int));
                //RandomTree.Columns.Add("Player", typeof(int));

                int TreeNum = 0;
                int[] RandomTree = new int[5000];

                if (Player == 1) Player = 2;
                else Player = 1;

                for (int k = 0; k < 3549; k++)
                {
                    string Action = Dimension.ActionToBoard(k);
                    int X = Convert.ToInt16(Action.Split(';')[0]);
                    int Y = Convert.ToInt16(Action.Split(';')[1]);
                    int[] SR = shapeCodeConversion.ToShapeRoate(Convert.ToInt16(Action.Split(';')[2]));

                    if (Check[Player - 1, SR[0]] == 0)
                    {
                        if (Rule.CanInput(Player, Check, Board, k) == true)
                        {
                            TreeNum++;

                            //Player(1 or 2),X,Y,SR(21)
                            RandomTree[TreeNum] = Player * 1000000 + X * 10000 + Y * 100 + Convert.ToInt16(Action.Split(';')[2]);
                        }
                    }
                }


                if (TreeNum > 0)
                {
                    int RandomNum = Rd.Next(1, TreeNum + 1);
                    Input(RandomTree, Board, Check, RandomNum);
                }
                else
                {
                    Console.WriteLine("MCTS Rollout Down");
                    Console.WriteLine("Wait");
                    Console.ReadLine();
                }
            }

            Back(Board, UseNodeNum);
        }

        private void Back(int[,] Board, int UseNodeNum)
        {
            ScoreCa0lculation ScoreCa0lculation = new ScoreCa0lculation();
            int[] Score = ScoreCa0lculation.caldomain(Board);

            while (true)
            {
                int Father = ArrayTree.Father[UseNodeNum];
                int Player = 0;
                decoder(ref Player, UseNodeNum);

                ArrayTree.Visit[UseNodeNum]++;

                if (Player == 1 && Score[0] > Score[1]) ArrayTree.Win[UseNodeNum]++;
                else if (Player == 2 && Score[0] < Score[1]) ArrayTree.Win[UseNodeNum]++;
                //else if (Score[0] == Score[1]) Tree.Rows[UseNodeNum][5] = Convert.ToDouble(Tree.Rows[UseNodeNum][5]) + 0.2f;

                if (Father != 0) UseNodeNum = Father;
                else break;
            }
        }


    }
}
