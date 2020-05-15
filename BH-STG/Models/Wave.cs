using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH_STG.Models
{
    public class Wave
    {
        public Wave(int sTimer, int mTimer, int dirX, int dirY, int posX, int posY, string className)
        {
            _sTimer = sTimer;
            _mTimer = mTimer;
            _dirX = dirX;
            _dirY = dirY;
            _posX = posX;
            _posY = posY;
            _ClassName = className;

        }
        public int _sTimer { get; set; }
        public int _mTimer { get; set; }
        public int _dirX { get; set; }
        public int _dirY { get; set; }
        public int _posX { get; set; }
        public int _posY { get; set; }
        public string _ClassName  { get; set; }

    }
}
