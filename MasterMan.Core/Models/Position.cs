using MasterMan.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.Core.Models
{
    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position()
        {
            X = Y = -1;
        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Validate(int minX, int maxX, int minY, int maxY)
        {
            return X >= minX && X <= maxX && Y >= minY && Y <= maxY;
        }

        public bool Equals(Position other)
        {
            bool equals = false;

            if (other != null)
            {
                equals = X.Equals(other.X) && Y.Equals(other.Y);
            }

            return equals;
        }

        public Position NextPosition(Direction direction)
        {
            Position nextPosition;
            switch (direction)
            {
                case Direction.Down:
                    nextPosition = new Position(X, Y + 1);
                    break;
                case Direction.Up:
                    nextPosition = new Position(X, Y - 1);
                    break;
                case Direction.Left:
                    nextPosition = new Position(X - 1, Y);
                    break;
                case Direction.Right:
                    nextPosition = new Position(X + 1, Y);
                    break;
                case Direction.UpLeft:
                    nextPosition = new Position(X - 1, Y - 1);
                    break;
                case Direction.UpRight:
                    nextPosition = new Position(X + 1, Y - 1);
                    break;
                case Direction.DownLeft:
                    nextPosition = new Position(X - 1, Y + 1);
                    break;
                case Direction.DownRight:
                    nextPosition = new Position(X + 1, Y + 1);
                    break;
                case Direction.None:
                default:
                    nextPosition = new Position(X, Y);
                    break;
            }

            return nextPosition;
        }
    }
}
