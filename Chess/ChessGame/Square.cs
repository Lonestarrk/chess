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

public class Square
{
    public Piece Piece { get; set; }
    public Position Position { get; set; }





    public override string ToString()
    {
        return Position + " " + Piece ?? "";
    }
}

