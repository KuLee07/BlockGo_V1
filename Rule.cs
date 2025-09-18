using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class Rule
    {
        /// <summary>
        /// 用來確認是否為合法步
        /// </summary>
        /// <param name="Player">先手1,後手2</param>
        /// <param name="ReadCheck">未落子之棋型</param>
        /// <param name="ReadBoard">未落子之棋盤</param>
        /// <param name="ActionNum">欲落子之位置與棋型代碼</param>
        public bool CanInput(int Player, int[,] ReadCheck, int[,] ReadBoard, int ActionNum)
        {
            ShapeCodeConversion ShapeCodeConversion = new ShapeCodeConversion();
            Dimension Dimension = new Dimension();

            string Action = Dimension.ActionToBoard(ActionNum);
            int X = Convert.ToInt16(Action.Split(';')[0]);
            int Y = Convert.ToInt16(Action.Split(';')[1]);
            int[] SR = ShapeCodeConversion.ToShapeRoate(Convert.ToInt16(Action.Split(';')[2]));
            int Shape = SR[0];
            int Roate = SR[1];

            bool NeedTouchStar = false, IsTouchStar = false;
            int WeightCount = 0;
            int[,] WeightBoard = Weight(ReadBoard, Player);

            int[,] ToBoard = ShapeCodeConversion.PositionToBoard(X, Y, Shape, Roate);

            if (ReadCheck[Player - 1, Shape] != 0)
            {
                return false;
            }

            if (ReadBoard[3, 3] == 0 || ReadBoard[9, 3] == 0 || ReadBoard[3, 9] == 0 || ReadBoard[9, 9] == 0)
            {
                NeedTouchStar = true;
            }

            for (int i = 0; i < 4; i++)
            {
                int tX = ToBoard[i, 0], tY = ToBoard[i, 1];

                if (tX < 0 || tX > 12 || tY < 0 || tY > 12) return false;

                if (ReadBoard[tX, tY] != 0) return false;

                if (NeedTouchStar == true &&
                    ((tX == 3 && tY == 3) ||
                    (tX == 9 && tY == 3) ||
                    (tX == 3 && tY == 9) ||
                    (tX == 9 && tY == 9)))
                {
                    IsTouchStar = true;
                }

                if (NeedTouchStar == false) WeightCount += WeightBoard[tX, tY];
            }

            if (NeedTouchStar == true && IsTouchStar == false) return false;
            else if (NeedTouchStar == false && WeightCount < 5) return false;

            return true;
        }


        public int[,] Weight(int[,] ReadBoard, int Player)
        {
            int[,] WeightBoard = new int[13, 13];
            int[] move_X = new int[4] { 0, 0, 1, -1 };
            int[] move_Y = new int[4] { 1, -1, 0, 0 };
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if ((i == 0 || i == 12 || j == 0 || j == 12) && ReadBoard[i, j] == 0)
                    {
                        WeightBoard[i, j] = 1;
                    }
                    else if (ReadBoard[i, j] == Player)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int tX = i + move_X[k], tY = j + move_Y[k];
                            if (tX >= 0 && tX <= 12 && tY >= 0 && tY <= 12)
                            {
                                if (ReadBoard[tX, tY] == 0) WeightBoard[tX, tY] = 2;
                            }
                        }
                    }
                    else if (WeightBoard[i, j] == 0) WeightBoard[i, j] = 1;
                }
            }
            return WeightBoard;
        }


        public bool[] IsWin(int[,] ReadCheck, int[,] ReadBoard)
        {
            //Player = 1 黑子
            //Player = 2 白子
            bool[] Result = new bool[3] { false, false, false };

            //先檢查是不是雙方都下完了
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (ReadCheck[i, j] == 0)
                    {
                        return Result;
                    }
                }
            }

            ScoreCa0lculation ScoreCa0lculation = new ScoreCa0lculation();
            int[] Score = ScoreCa0lculation.caldomain(ReadBoard);

            if (Score[0] == Score[1]) Result[0] = true;
            else if (Score[0] > Score[1]) Result[1] = true;
            else Result[2] = true;

            return Result;
        }
    }
}
