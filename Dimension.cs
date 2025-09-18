using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class Dimension
    {
        //升高維度
        public string ActionToBoard(int action)
        {

            int i = (action / 21) % 13; //X
            int j = action / 273; //Y
            int k = action % 21; //Z

            return i.ToString() + ";" + j.ToString() + ";" + k.ToString();
        }

        //降低維度
        public string BoardToAction(int i, int j, int k)
        {
            int action = i * 21 + j * 13 * 21 + k;

            return action.ToString();
        }
    }
}
