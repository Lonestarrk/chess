using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessGame
{
    public class MovePieceEventArgs : EventArgs
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }
        public MovePieceEventArgs(Position fromPosition, Position toPosition)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
      
    }
}
