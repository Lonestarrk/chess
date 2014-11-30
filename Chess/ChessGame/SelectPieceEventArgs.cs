using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame
{
    public class SelectPieceEventArgs : EventArgs
    {
        public Square Square { get; set; }
        public SelectPieceEventArgs(Square square)
        {
            Square = square;
        }
    }
}
