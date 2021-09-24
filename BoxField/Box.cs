using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class Box
    {
        public int x, y, size, speed, colour;

        public Box(int _x, int _y, int _size, int _speed, int _colour)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            colour = _colour;
        }

        public void Move()
        {
            y += speed;
        }

        public void Move(string direction)
        {
            if (direction == "left")
            {
                x -= speed;
            }
            if (direction == "right")
            {
                x += speed;
            }
        }

        public bool Collision(Box b)
        {
            Rectangle player = new Rectangle(x, y, size, size);

            Rectangle fallingBox = new Rectangle(b.x, b.y, b.size, b.size);

            if (fallingBox.IntersectsWith(player))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
