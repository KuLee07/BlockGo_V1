using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockGo_ControlPanel
{
    internal class Parameter
    {
        //13 * 13 * 21 = 3549
        public int BoardSize_X = 13;
        public int BoardSize_Y = 13;
        public int BoardSize_Z = 21;//0-20 

        public int MCTS_TIME_TRAIN = 10;//訓練用MCTS次數
        public int MCTS_TIME_SELF = 10;//對弈用MCTS次數
        public int ITERATE_TIME = 20;
        public int TRAIN_TIME = 30;
        public int SELF_PLAY_TIME = 60;
        public double CPUCT = 1;

        public string Local_IP = "127.0.0.1";
        public int Reccive_PORT = 1750;
        public int Send_PORT = 1751;
    }
}
