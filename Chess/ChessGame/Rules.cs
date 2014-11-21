using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public static class MoveRules
    {
        public static bool King(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            return Offset(pos, next, -1, -1) ||
                   Offset(pos, next, 0, -1) ||
                   Offset(pos, next, 1, -1) ||
                   Offset(pos, next, -1, 0) ||
                   Offset(pos, next, 1, 0) ||
                   Offset(pos, next, -1, 1) ||
                   Offset(pos, next, 0, 1) ||
                   Offset(pos, next, 1, 1);
        }

        public static bool Offset(Position current, Position next, int x, int y)
        {
            return (next.X == current.X + x && next.Y == current.Y + y);
        }
        public static bool Pawn(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            if (currentSqure.Piece.PieceColor == PieceColor.White)
            {
                return (next.X == pos.X && next.Y == pos.Y - 1) ||
                       HasEnemy(currentSqure, nextSqare) && (next.X == pos.X + 1 && next.Y == pos.Y - 1) ||
                       HasEnemy(currentSqure, nextSqare) && (next.X == pos.X - 1 && next.Y == pos.Y - 1);
            }

            return (next.X == pos.X && next.Y == pos.Y + 1) ||
                   HasEnemy(currentSqure, nextSqare) && (next.X == pos.X + 1 && next.Y == pos.Y + 1) ||
                   HasEnemy(currentSqure, nextSqare) && (next.X == pos.X - 1 && next.Y == pos.Y + 1);
        }


        private static bool HasEnemy(Square currentSqure, Square nextSqare)
        {
            if (currentSqure.Piece != null && nextSqare.Piece != null)
            {
                return currentSqure.Piece.PieceColor != nextSqare.Piece.PieceColor;
            }
            return false;
        }


        public static bool Knight(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            return (next.X == pos.X + 2 && next.Y == pos.Y + 1) ||
                   (next.X == pos.X + 1 && next.Y == pos.Y + 2) ||
                   (next.X == pos.X - 2 && next.Y == pos.Y - 1) ||
                   (next.X == pos.X - 1 && next.Y == pos.Y - 2) ||
                   (next.X == pos.X - 2 && next.Y == pos.Y + 1) ||
                   (next.X == pos.X + 2 && next.Y == pos.Y - 1) ||
                   (next.X == pos.X + 1 && next.Y == pos.Y - 2) ||
                   (next.X == pos.X - 1 && next.Y == pos.Y + 2);
        }

        public static bool Queen(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            return Bishop(currentSqure, nextSqare) || Rook(currentSqure, nextSqare);
        }

        public static bool Bishop(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            bool previousSqureWasEmpty = false;

            for (int n = 1; n <= 8; n++)
            {
                bool canMoveTo = BishopCanMoveTo(currentSqure, nextSqare, n) || BishopCanTake(currentSqure, nextSqare);

                if (canMoveTo)
                {

                    return true;
                }

                if (HasEnemy(currentSqure, nextSqare))
                {
                    break;
                }
            }

            return false;

        }


        private static bool BishopCanMoveTo(Square current, Square nextSquare, int n)
        {
            var pos = current.Position;
            var next = nextSquare.Position;

            var canMove = Offset(pos, next, n, n) ||
                   Offset(pos, next, -n, -n) ||
                   Offset(pos, next, n, -n) ||
                   Offset(pos, next, -n, n);



            return canMove;
        }
        private static bool BishopCanTake(Square current, Square nextSquare)
        {

            return HasEnemy(current, nextSquare) && IsInUnobstructedDiagonalLine(current, nextSquare);
        }

        private static bool IsInUnobstructedDiagonalLine(Square current, Square next)
        {
            var squaresBeween = GetSquaresBetween(current, next);
            
            return squaresBeween.All(s => s.Piece == null) && squaresBeween.Count > 0;
        }

        private static List<Square> GetSquaresBetween(Square current, Square next)
        {
            var diagonalSquares = GetDiagonalSquares(current);
            var verticalAndHorizontalSquares = GetVerticalAndHorizontalSquares(current);

            //var distansX = Math.Abs(current.Position.X - next.Position.X);
            //var distansY = Math.Abs(current.Position.Y - next.Position.Y);

            var smallestX = Math.Min(current.Position.X, next.Position.X)+1;
            var smallestY = Math.Min(current.Position.Y, next.Position.Y)+1;

            var biggestX = Math.Max(current.Position.X, next.Position.X)-1;
            var biggestY = Math.Max(current.Position.Y, next.Position.Y)-1;


            var squares = new List<Square>();

            if (diagonalSquares.Any(s => s.Position == next.Position))
            {
                for (int n = smallestX; n <= biggestX; n++)
                {
                    var square = GameBoard.Squares.SingleOrDefault(s => s.Position.X == n && s.Position.Y == n);

                    if (IsOccupied(current,next))
                    {
                        return squares;
                    }

                    squares.Add(square);

                    if (HasEnemy(current,next))
                    {
                        return squares;
                    }

                }
                   
            }

            //if (verticalAndHorizontalSquares.Any(s => s.Position == next.Position))
            //{
            //    for (int x = smallestX; x < biggestX; x++)
            //    {
            //        for (int y = smallestY; y < biggestY; y++)
            //        {
            //            squares.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == x && s.Position.Y == y));
            //        }
            //    }
            //    squares.RemoveAt(squares.Count - 1);
            //    squares.RemoveAt(0);
            //}


            return squares;
        }

        private static bool IsOccupied(Square current, Square next)
        {
            return next.Piece != null && next.Piece.PieceColor != current.Piece.PieceColor;
        }

        private static List<Square> GetVerticalAndHorizontalSquares(Square current)
        {
            var horizontal = GameBoard.Squares
                .Where(s => s.Position.Y == current.Position.Y && s.Position.X != current.Position.X);

            var vertical = GameBoard.Squares
                .Where(s => s.Position.X == current.Position.X && s.Position.Y != current.Position.Y);

            return vertical.Union(horizontal).ToList();
        }

        private static List<Square> GetDiagonalSquares(Square current)
        {
            var squares = new List<Square>();
            for (int n = 1; n < 8; n++)
            {
                squares = squares.Union(GameBoard.Squares.Where(s => s.Position.X == current.Position.X + n && s.Position.Y == current.Position.Y + n)).ToList();
                squares = squares.Union(GameBoard.Squares.Where(s => s.Position.X == current.Position.X - n && s.Position.Y == current.Position.Y - n)).ToList();
                squares = squares.Union(GameBoard.Squares.Where(s => s.Position.X == current.Position.X - n && s.Position.Y == current.Position.Y + n)).ToList();
                squares = squares.Union(GameBoard.Squares.Where(s => s.Position.X == current.Position.X + n && s.Position.Y == current.Position.Y - n)).ToList();

            }
            return squares;
        }

        public static bool Rook(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            throw new NotImplementedException();
        }
    }
}
