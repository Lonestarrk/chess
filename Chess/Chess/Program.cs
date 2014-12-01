using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeGameEvents();

            GameBoard.StartGame();

        }

        private static void InitializeGameEvents()
        {
            GameBoard.SelectPieceEvent += OnSelectPieceEvent;
            GameBoard.MovePieceEvent += OnMovePieceEvent;
        }

        private static void OnMovePieceEvent(object sender, MovePieceEventArgs args)
        {
            if (GameBoard.ReachableSquares != null && GameBoard.ReachableSquares.Any(r => r.Position == args.ToPosition))
            {
                GameBoard.Move(args.FromPosition, args.ToPosition);
                GameBoard.IsCheckState = GameBoard.IsCheck(GameBoard.Turn);
            }
            GameBoard.ReachableSquares = null;

        }

        private static void OnSelectPieceEvent(object sender, SelectPieceEventArgs args)
        {
            if (!GameBoard.IsCheckMateState)
            {
                if (args.Square.Piece != null && args.Square.Piece.PieceColor == GameBoard.Turn)
                {
                    GameBoard.ReachableSquares = GameBoard.GetReachableSquares(args.Square.Position);
                }
            }
        }
    }
}
