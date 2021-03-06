﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{

    public delegate bool MoveRule(Square currentSquare, Square moveToSquare);
    public enum PieceColor
    {
        Black,
        White
    }

    public abstract class Piece : IPiece
    {
        public bool HasMoved { get; set; }
        public PieceColor PieceColor { get; set; }
        public Square Square { get; set; }
        public MoveRule MoveRule { get; set; }

        protected Piece(PieceColor pieceColor)
        {
            PieceColor = pieceColor;
        }

        public virtual bool CanMoveTo(Square toSquare)
        {
            var pieceCanMove = (toSquare.Piece == null || toSquare.Piece.PieceColor != this.PieceColor)
                && MoveRule.Invoke(Square, toSquare);



            //if piece is king, special rules will apply
            if (this.GetType() == typeof(King))
            {
                return !IsDangerousMove(toSquare) && pieceCanMove;
            }

            return pieceCanMove;


        }

        private bool IsDangerousMove(Square toSquare)
        {
            if (MoveRule.Invoke(Square, toSquare))
            {
                var otherSide = GameBoard.Squares.Where(s => s.Piece != null && s.Piece.PieceColor != this.PieceColor).ToArray();

                if (toSquare.Piece != null && toSquare.Piece.PieceColor != PieceColor)
                //if tries to take opponentes piece
                {
                    var piece = GameBoard.TakeGamePiece(toSquare.Position); //remove the piece

                    foreach (var enemySquare in otherSide.Where(o => o.Piece != null))
                    {
                        if (enemySquare.Piece.MoveRule.Invoke(enemySquare, toSquare))
                        {
                            GameBoard.PlaceGamePiece(toSquare.Position, piece); // place the piece back
                            return true;

                        }
                    };

                    GameBoard.PlaceGamePiece(toSquare.Position, piece); // place the piece back                   
                }

                else
                    foreach (var enemySquare in otherSide)
                    {
                        if (enemySquare.Piece.MoveRule.Invoke(enemySquare, toSquare))
                        {
                            return true;
                        }
                    }

            }
            return false;
        }
    }
}
