using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador_1331818
{
    public enum TokenType
    {
        Plus = '+',
        Minus = '-',
        Multiplication = '*',
        Division = '/',
        LParen = '(',
        RParen = ')',
        EOF = (char)0,
        Number = (char)1
    }
}
