using System;

namespace ChessGame
{
    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
    }

    public class Position : IPosition
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Position)obj);
        }


        public static bool operator ==(Position position1, Position position2)
        {
            if (System.Object.ReferenceEquals(position1, position2))
            {
                return true;
            }

            if ((object)position1 == null || (object)position2 == null)
            {
                return false;
            }

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