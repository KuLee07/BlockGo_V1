using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class ScoreCa0lculation
    {
        public int[] caldomain(int[,] board)
        {
            int[,] black_board = new int[13, 13];
            int[,] white_board = new int[13, 13];
            int[,] all_board = new int[13, 13];
            int[] num = new int[2];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (board[i, j] == 0)
                    {
                        black_board[i, j] = 0;
                        white_board[i, j] = 0;
                    }
                    else if (board[i, j] == 1)
                    {
                        black_board[i, j] = 1;
                        white_board[i, j] = 0;
                    }
                    else if (board[i, j] == 2)
                    {
                        black_board[i, j] = 0;
                        white_board[i, j] = 2;
                    }
                    all_board[i, j] = board[i, j];
                }
            }
            int b_mark = 3, w_mark = 3, all_mark = 3;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (black_board[i, j] == 0)
                    {
                        mark(black_board, i, j, b_mark);
                        b_mark++;
                    }
                    if (white_board[i, j] == 0)
                    {
                        mark(white_board, i, j, w_mark);
                        w_mark++;
                    }
                    if (all_board[i, j] == 0)
                    {
                        mark(all_board, i, j, all_mark);
                        all_mark++;
                    }
                }
            }
            //draw(black_board);
            //draw(white_board);
            //draw(all_board);

            num[0] = 0;
            num[1] = 4;
            all_mark = 3;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (all_board[i, j] == all_mark)
                    {
                        num[0] += getdomain(all_board, black_board, black_board[i, j], all_mark, 2);
                        num[1] += getdomain(all_board, white_board, white_board[i, j], all_mark, 1);
                        all_mark++;
                    }

                }
            }
            if (num[0] == 139 && (num[1] - 4) == 139)
            {
                num[0] = 0;
                num[1] = 0;
            }
            return num;

        }

        private void mark(int[,] board, int x, int y, int mark_color)
        {
            int[] move_x = new int[4] { 1, -1, 0, 0 };
            int[] move_y = new int[4] { 0, 0, 1, -1 };
            board[x, y] = mark_color;
            for (int i = 0; i < 4; i++)
            {
                if (x + move_x[i] >= 0 && x + move_x[i] < 13 && y + move_y[i] >= 0 && y + move_y[i] < 13)
                {

                    if (board[x + move_x[i], y + move_y[i]] == 0)
                    {
                        mark(board, x + move_x[i], y + move_y[i], mark_color);
                    }
                }
            }
        }

        private int getdomain(int[,] allboard, int[,] board, int board_mark, int all_mark, int oppcolor)
        {
            int board_num = 0, all_num = 0;

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (board[i, j] == board_mark)
                    {
                        board_num++;
                    }
                    if (allboard[i, j] == all_mark || (board[i, j] == board_mark && allboard[i, j] == oppcolor))
                    {
                        all_num++;
                    }
                }
            }
            //Console.WriteLine(all_mark);
            //Console.WriteLine(board_num + " " + all_num);
            if (board_num == all_num)
            {
                return board_num;
            }
            return 0;
        }

    }
}
