﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessGame;

public class Rook : Piece
{
    public Rook(PieceColor pieceColor) : base(pieceColor)
    {
        base.MoveRule = MoveRules.Rook;
    }

    public override string ToString()
    {
        var rook = "♜";

        //var utf8Encoder = new UnicodeEncoding();
        //var bytes = Encoding.Unicode.GetBytes(rook);

        //var utf8Rook = utf8Encoder.GetString(bytes);
        Console.OutputEncoding = new UnicodeEncoding();

        return "T";
    }
}

