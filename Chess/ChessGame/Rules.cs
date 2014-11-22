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

            bool forwardStep1 = !currentSqure.Piece.HasMoved ? (next.X == pos.X && next.Y == pos.Y - 2) || (next.X == pos.X && next.Y == pos.Y - 1) : (next.X == pos.X && next.Y == pos.Y - 1);
            bool forwardStep2 = !currentSqure.Piece.HasMoved ? (next.X == pos.X && next.Y == pos.Y + 2) || (next.X == pos.X && next.Y == pos.Y + 1) : (next.X == pos.X && next.Y == pos.Y + 1);

            if (currentSqure.Piece.PieceColor == PieceColor.White)
            {

                return forwardStep1 && !IsOccupied(nextSqare) ||
                       HasEnemy(currentSqure, nextSqare) && (next.X == pos.X + 1 && next.Y == pos.Y - 1) ||
                       HasEnemy(currentSqure, nextSqare) && (next.X == pos.X - 1 && next.Y == pos.Y - 1);
            }



            return forwardStep2 && !IsOccupied(nextSqare) ||
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

        public static bool Bishop(Square currentSqure, Square nextSquare)
        {
            bool canMoveTo = GetDiagonalSquares(currentSqure).Contains(nextSquare);

            return canMoveTo;

        }

        private static bool IsOccupied(Square next)
        {
            return next.Piece != null;
        }

        private static List<Square> GetVerticalAndHorizontalSquares(Square current)
        {

            var path1 = new List<Square>();
            var path2 = new List<Square>();
            var path3 = new List<Square>();
            var path4 = new List<Square>();

            for (int n = 1; n <= 8; n++)
            {
                path1.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X + n && s.Position.Y == current.Position.Y));
                path2.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X - n && s.Position.Y == current.Position.Y));
                path3.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.Y == current.Position.Y + n && s.Position.X == current.Position.X));
                path4.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.Y == current.Position.Y - n && s.Position.X == current.Position.X));
            }

            var squares = GetAccesableSquares(current, path1, path2, path3, path4);

            return squares;
        }

        private static List<Square> GetDiagonalSquares(Square current)
        {

            var path1 = new List<Square>();
            var path2 = new List<Square>();
            var path3 = new List<Square>();
            var path4 = new List<Square>();

            for (int n = 1; n <= 8; n++)
            {
                path1.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X + n && s.Position.Y == current.Position.Y + n));
                path2.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X - n && s.Position.Y == current.Position.Y - n));
                path3.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X - n && s.Position.Y == current.Position.Y + n));
                path4.Add(GameBoard.Squares.SingleOrDefault(s => s.Position.X == current.Position.X + n && s.Position.Y == current.Position.Y - n));


            }

            var squares = GetAccesableSquares(current, path1, path2, path3, path4);

            return squares;
        }

        private static List<Square> GetAccesableSquares(Square current, List<Square> path1, List<Square> path2, List<Square> path3, List<Square> path4)
        {
            var squares = new List<Square>();
            var paths = new List<List<Square>>();

            path1 = path1.Where(p => p != null).ToList();
            path2 = path2.Where(p => p != null).ToList();
            path3 = path3.Where(p => p != null).ToList();
            path4 = path4.Where(p => p != null).ToList();

            paths.Add(path1);
            paths.Add(path2);
            paths.Add(path3);
            paths.Add(path4);

            foreach (var path in paths)
            {
                foreach (var square in path)
                {
                    if (square.Piece == null)
                    {
                        squares.Add(square);
                    }
                    else if (square.Piece != null && HasEnemy(current, square))
                    {
                        squares.Add(square);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return squares;
        }

        public static bool Rook(Square currentSqure, Square nextSqare)
        {
            var pos = currentSqure.Position;
            var next = nextSqare.Position;

            return GetVerticalAndHorizontalSquares(currentSqure).Contains(nextSqare);
        }
    }
}
