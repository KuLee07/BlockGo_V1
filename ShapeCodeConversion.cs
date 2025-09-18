using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class ShapeCodeConversion
    {
        public int SRto21(int k, int l)
        {
            if (k == 0 && l == 0) return 0;
            if (k == 0 && l == 1) return 1;

            if (k == 1 && l == 0) return 2;
            if (k == 1 && l == 1) return 3;
            if (k == 1 && l == 2) return 4;
            if (k == 1 && l == 3) return 5;

            if (k == 2 && l == 0) return 6;
            if (k == 2 && l == 1) return 7;
            if (k == 2 && l == 2) return 8;
            if (k == 2 && l == 3) return 9;

            if (k == 3 && l == 0) return 10;
            if (k == 3 && l == 1) return 11;
            if (k == 3 && l == 2) return 12;
            if (k == 3 && l == 3) return 13;

            if (k == 4 && l == 0) return 14;
            if (k == 4 && l == 1) return 15;

            if (k == 5 && l == 0) return 16;
            if (k == 5 && l == 1) return 17;

            if (k == 6 && l == 0) return 18;

            if (k == 7 && l == 0) return 19;

            if (k == 8 && l == 0) return 20;

            return 0;
        }

        public int[] ToShapeRoate(int SR)
        {
            int[] ReturnValue = new int[2];
            switch (SR)
            {
                case 0:
                    ReturnValue[0] = 0;
                    ReturnValue[1] = 0;
                    break;
                case 1:
                    ReturnValue[0] = 0;
                    ReturnValue[1] = 1;
                    break;
                case 2:
                    ReturnValue[0] = 1;
                    ReturnValue[1] = 0;
                    break;
                case 3:
                    ReturnValue[0] = 1;
                    ReturnValue[1] = 1;
                    break;
                case 4:
                    ReturnValue[0] = 1;
                    ReturnValue[1] = 2;
                    break;
                case 5:
                    ReturnValue[0] = 1;
                    ReturnValue[1] = 3;
                    break;
                case 6:
                    ReturnValue[0] = 2;
                    ReturnValue[1] = 0;
                    break;
                case 7:
                    ReturnValue[0] = 2;
                    ReturnValue[1] = 1;
                    break;
                case 8:
                    ReturnValue[0] = 2;
                    ReturnValue[1] = 2;
                    break;
                case 9:
                    ReturnValue[0] = 2;
                    ReturnValue[1] = 3;
                    break;
                case 10:
                    ReturnValue[0] = 3;
                    ReturnValue[1] = 0;
                    break;
                case 11:
                    ReturnValue[0] = 3;
                    ReturnValue[1] = 1;
                    break;
                case 12:
                    ReturnValue[0] = 3;
                    ReturnValue[1] = 2;
                    break;
                case 13:
                    ReturnValue[0] = 3;
                    ReturnValue[1] = 3;
                    break;
                case 14:
                    ReturnValue[0] = 4;
                    ReturnValue[1] = 0;
                    break;
                case 15:
                    ReturnValue[0] = 4;
                    ReturnValue[1] = 1;
                    break;
                case 16:
                    ReturnValue[0] = 5;
                    ReturnValue[1] = 0;
                    break;
                case 17:
                    ReturnValue[0] = 5;
                    ReturnValue[1] = 1;
                    break;
                case 18:
                    ReturnValue[0] = 6;
                    ReturnValue[1] = 0;
                    break;
                case 19:
                    ReturnValue[0] = 7;
                    ReturnValue[1] = 0;
                    break;
                case 20:
                    ReturnValue[0] = 8;
                    ReturnValue[1] = 0;
                    break;
            }
            return ReturnValue;
        }

        public string[] ShapeRoateToPosition(int Shape, int Roate)
        {
            string ShapeRoate = Shape.ToString() + Roate.ToString();
            string ReturnValue = "";
            switch (ShapeRoate)
            {
                case "00":
                    ReturnValue = "0,0,1,0,2,0,3,0";
                    break;
                case "01":
                    ReturnValue = "0,0,0,1,0,2,0,3";
                    break;
                case "10":
                    ReturnValue = "0,0,1,0,2,0,1,1";
                    break;
                case "11":
                    ReturnValue = "1,0,0,1,1,1,1,2";
                    break;
                case "12":
                    ReturnValue = "1,0,0,1,1,1,2,1";
                    break;
                case "13":
                    ReturnValue = "0,0,0,1,1,1,0,2";
                    break;
                case "20":
                    ReturnValue = "0,0,0,1,1,0,2,0";
                    break;
                case "21":
                    ReturnValue = "0,0,1,0,1,1,1,2";
                    break;
                case "22":
                    ReturnValue = "2,0,0,1,1,1,2,1";
                    break;
                case "23":
                    ReturnValue = "0,0,0,1,0,2,1,2";
                    break;
                case "30":
                    ReturnValue = "0,0,1,0,2,0,2,1";
                    break;
                case "31":
                    ReturnValue = "1,0,1,1,0,2,1,2";
                    break;
                case "32":
                    ReturnValue = "0,0,0,1,1,1,2,1";
                    break;
                case "33":
                    ReturnValue = "0,0,1,0,0,1,0,2";
                    break;
                case "40":
                    ReturnValue = "0,0,1,0,1,1,2,1";
                    break;
                case "41":
                    ReturnValue = "1,0,0,1,1,1,0,2";
                    break;
                case "50":
                    ReturnValue = "1,0,2,0,1,1,0,1";
                    break;
                case "51":
                    ReturnValue = "0,0,0,1,1,1,1,2";
                    break;
                case "60":
                    ReturnValue = "0,0,1,0,0,1,1,1";
                    break;
                case "70":
                    ReturnValue = "0,0,0,0,0,0,0,0";
                    break;
                case "80":
                    ReturnValue = "0,0,0,0,0,0,0,0";
                    break;
            }
            return ReturnValue.Split(',');
        }

        public int[,] PositionToBoard(int X, int Y, int Shape, int Roate)
        {
            int[,] ReturnValue = new int[4, 2];
            int[] intConvert = new int[8];
            string[] strConvert = ShapeRoateToPosition(Shape, Roate);
            for (int i = 0; i < 8; i++) intConvert[i] = int.Parse(strConvert[i]);

            ReturnValue[0, 0] = (X - intConvert[0]) + intConvert[0];
            ReturnValue[0, 1] = (Y - intConvert[1]) + intConvert[1];

            ReturnValue[1, 0] = (X - intConvert[0]) + intConvert[2];
            ReturnValue[1, 1] = (Y - intConvert[1]) + intConvert[3];

            ReturnValue[2, 0] = (X - intConvert[0]) + intConvert[4];
            ReturnValue[2, 1] = (Y - intConvert[1]) + intConvert[5];

            ReturnValue[3, 0] = (X - intConvert[0]) + intConvert[6];
            ReturnValue[3, 1] = (Y - intConvert[1]) + intConvert[7];

            return ReturnValue;
        }

        public string ShowShape(int code)
        {
            string shape = "";
            if (code == 0)
            {
                shape = "●●●●";
            }
            if (code == 1)
            {
                Console.WriteLine(2 + ":");
                shape = "●●●" + "\n";
                shape += "  ●";
            }
            if (code == 2)
            {
                Console.WriteLine(3 + ":");
                shape = "●●●" + "\n";
                shape += "●";
            }
            if (code == 3)
            {
                Console.WriteLine(4 + ":");
                shape = "●●●" + "\n";
                shape += "    ●";
            }
            if (code == 4)
            {
                shape = "●●" + "\n";
                shape += "  ●●";

            }
            if (code == 5)
            {
                shape = "  ●●" + "\n";
                shape += "●●";
            }
            if (code == 6)
            {
                shape = "●●" + "\n";
                shape += "●●";
            }
            if (code == 7)
            {
                shape = "●";
            }
            if (code == 8)
            {
                shape = "●";
            }
            return shape;
        }


    }
}
