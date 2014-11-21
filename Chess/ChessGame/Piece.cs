using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame
{

    public delegate bool MoveRule(Square currentSquare, Square moveToSquare);
    public enum PieceColor
    {
        Black,
        White
    }

    public abstract class Piece
    {
        public PieceColor PieceColor { get; set; }
        public Square Square { get; set; }
        public MoveRule MoveRule { get; set; }

        protected Piece(PieceColor pieceColor)
        {
            PieceColor = pieceColor;
        }

        public virtual void MoveTo(Square square)
        {
            if (CanMoveTo(square))
            {
                var moveIsAllowed = MoveRule;

                if (moveIsAllowed(Square,square))
                {
                    square.Piece = Square.Piece;
                    Square.Piece = null;
                    return;
                }
                throw new ApplicationException("Flytten är inte tillåten");
            }
         
        }

        public virtual bool CanMoveTo(Square square)
        {
            return (square.Piece == null || square.Piece.PieceColor != this.PieceColor) 
                && MoveRule.Invoke(Square,square);
        }
      
    }
}
