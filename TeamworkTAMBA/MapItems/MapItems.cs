﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamworkTAMBA
{
    using System.Drawing;

    public class MapItems : GameObject
    {
        public MapItems(SpriteType sriteType, Point location, int id) :
            base(sriteType, location, id)
        {

        }
        public MapItems()
        {
        }
    }
}
