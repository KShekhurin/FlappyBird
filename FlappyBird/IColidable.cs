﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    interface IColidable
    {
        void Colide(ref Bird bird);
    }
}
