
namespace ChessGame
{
    public interface ISquare
    {

        Piece Piece { get; set; }
        Position Position { get; set; }
        string ToString();
    }
}