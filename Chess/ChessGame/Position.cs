using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Position
    {
        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }


        public static bool operator ==(Position position1, Position position2)
        {

            return (position1.X == position2.X && position1.Y == position2.Y);


        }

        public static bool operator !=(Position position1, Position position2)
        {
            return !(position1 == position2);
        }



        public int GetHashCode(object obj)
        {
            var position = obj as Position;

            if (position != null)
            {
                return position.X.GetHashCode() ^ position.Y.GetHashCode();
            }
            throw new ApplicationException("Incorrect Type");
        }

        public override string ToString()
        {
            return "X: " + X + ", Y:" + Y;
        }
    }
}
