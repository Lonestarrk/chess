namespace ChessGame
{
    public interface IPiece
    {
        bool HasMoved { get; set; }
        PieceColor PieceColor { get; set; }
        Square Square { get; set; }
        MoveRule MoveRule { get; set; }
        bool CanMoveTo(Square toSquare);
    }
}