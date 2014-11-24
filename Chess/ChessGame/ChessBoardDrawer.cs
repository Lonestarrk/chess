using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessBoardDrawer
    {
        public static void DrawGameBoard(List<Square> squares, Position selectedSquarePosition = null, IEnumerable<Square> selectedCanMoveTo = null)
        {
            

            bool oddNumbersAreWhite = true;

            for (int i = 1; i <= 8; i++)
            {
                int rowNumber = i;

                DrawRow(rowNumber, oddNumbersAreWhite, false, selectedSquarePosition, selectedCanMoveTo);
                DrawRow(rowNumber, oddNumbersAreWhite, true, selectedSquarePosition, selectedCanMoveTo);
                DrawRow(rowNumber, oddNumbersAreWhite, false, selectedSquarePosition, selectedCanMoveTo);
                DrawRow(rowNumber, oddNumbersAreWhite, false, selectedSquarePosition, selectedCanMoveTo);

                oddNumbersAreWhite = !oddNumbersAreWhite;
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void DrawRow(int rowNumber, bool oddIsWhite, bool drawPiece, Position selectedSquarePosition = null, IEnumerable<Square> selectedCanMoveTo = null)
        {

            for (int i = 1; i <= 8; i++)
            {
                var square = GameBoard.Squares.SingleOrDefault(s => s.Position.X == i && s.Position.Y == rowNumber);

                if (selectedSquarePosition != null && (i == selectedSquarePosition.X && rowNumber == selectedSquarePosition.Y))
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;

                }
                else if (selectedCanMoveTo != null && selectedCanMoveTo.Any(s => s.Position.X == i && s.Position.Y == rowNumber))
                {
                    Console.BackgroundColor = oddIsWhite
                        ? (i % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Green)
                        : (i % 2 == 0 ? ConsoleColor.Green : ConsoleColor.DarkGreen);
                }
                else
                {
                    Console.BackgroundColor = oddIsWhite
                        ? (i % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.Gray)
                        : (i % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.DarkGray);
                }



                Console.ForegroundColor = square.Piece != null && square.Piece.PieceColor == PieceColor.White
                    ? ConsoleColor.White
                    : ConsoleColor.Black;


                Console.Write(new string(' ', 3));
                if (square.Piece != null && drawPiece)
                {
                    Console.Write(square.Piece);
                }
                else
                {
                    Console.Write(' ');
                }

                Console.Write(new string(' ', 3));
            }

            Console.WriteLine();
        }
    }
}
