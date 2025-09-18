using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class Board
    {
        Parameter Parameter = new Parameter();

        public void ClearBoard()
        {
            for(int k=0;k<Parameter.BoardSize_X;k++)
            {
                if (k == 0)
                {
                    Console.Write("   0");
                }
                else if (k < 10)
                {
                    Console.Write("  "+k.ToString());
                }
                else
                {
                    Console.Write(" "+k.ToString());
                }
            }
            Console.Write("\n");
            for (int j = 0; j < Parameter.BoardSize_Y; j++)
            {
                if (j < 10) Console.Write(' ' + j.ToString());
                else Console.Write(j.ToString());
                for (int i = 0; i < Parameter.BoardSize_X; i++)
                {
                    Console.Write(" . ");
                    if (i == Parameter.BoardSize_X - 1) Console.Write('\n');
                }
            }
        }

        public void DrawBoard(int[,] ReadBoard)
        {
            for (int k = 0; k < Parameter.BoardSize_X; k++)
            {
                if (k == 0)
                {
                    Console.Write("   0");
                }
                else if (k < 10)
                {
                    Console.Write("  " + k.ToString());
                }
                else
                {
                    Console.Write(" " + k.ToString());
                }
            }
            Console.Write("\n");
            for (int j = 0; j < Parameter.BoardSize_Y; j++)
            {
                if (j < 10) Console.Write(' ' + j.ToString());
                else Console.Write(j.ToString());
                for (int i = 0; i < Parameter.BoardSize_X; i++)
                {
                    if (ReadBoard[i, j] == 1) Console.Write(" Ｘ");
                    else if (ReadBoard[i, j] == 2) Console.Write(" ●");
                    else if (ReadBoard[i, j] == 0) Console.Write(" ．");
                    if (i == Parameter.BoardSize_X - 1) Console.Write('\n');
                }
            }
        }
    }
}
