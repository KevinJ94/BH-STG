using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH_STG.States;

namespace BH_STG.Interface
{
    public interface ICollidable
    {
        void OnCollide(Sprite sprite);
    }
}
