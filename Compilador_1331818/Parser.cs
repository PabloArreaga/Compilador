using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador_1331818
{
	public class Parser
	{
        Scanner _scanner;
        Token _token;
        string msgerror;

        private string Q()
        {
            switch (_token.Tag)
            {
                case TokenType.Number:
                    return GetNumber();
                default:
                    return "";
            }
        }
        private double P()
        {
            switch (_token.Tag)
            {
                case TokenType.LParen:
                    msgerror = "Se esperaba un >> ) << o >> ( <<";
                    Match(TokenType.LParen);
                    var result = E();
                    Match(TokenType.RParen);
                    return result;
                case TokenType.Number:
                    return Convert.ToInt32(GetNumber());
                default:
                    throw new Exception("Error sintactico: " + msgerror);
            }
        }

        private double F()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    msgerror = "Se esperaba un >> digito << despues del >> - <<";
                    Match(TokenType.Minus);
                    return -F();
                case TokenType.LParen:
                    msgerror = "Se esperaba un >> ) <<";
                    return P();
                case TokenType.Number:
                    return P();
                default:
                    throw new Exception("Error sintactico: " + msgerror);
            }
        }

        private double TP()
        {
            switch (_token.Tag)
            {
                case TokenType.Multiplication:
                    msgerror = "Se esperaba un >> digito << despues del >> * <<";
                    Match(TokenType.Multiplication);
                    return F() * TP();
                case TokenType.Division:
                    msgerror = "Se esperaba un >> digito << despues del >> / <<";
                    Match(TokenType.Division);
                    return 1 / F() * TP();
                default:
                    return 1;
            }
        }

        private double T()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    msgerror = "Se esperaba un >> digito << despues del >> - <<";
                    return F() * TP();
                case TokenType.LParen:
                    msgerror = "Se esperaba un >> ) <<";
                    return F() * TP();
                case TokenType.Number:
                    return F() * TP();
                default:
                    throw new Exception("Error sintactico: " + msgerror);
            }
        }

        private double EP()
        {
            switch (_token.Tag)
            {
                case TokenType.Plus:
                    msgerror = "Se esperaba un >> digito << despues del >> + <<";
                    Match(TokenType.Plus);
                    return T() + EP();
                case TokenType.Minus:
                    msgerror = "Se esperaba un >> digito << despues del >> - <<";
                    Match(TokenType.Minus);
                    return -T() + EP();
                default:

                    return 0;
            }
        }

        private double E()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    msgerror = "Se esperaba un >> digito << despues del >> - <<";
                    return T() + EP();
                case TokenType.LParen:
                    msgerror = "Se esperaba un >> ) <<";
                    return T() + EP();
                case TokenType.Number:
                    return T() + EP();
                default:
                    throw new Exception("Error sintactico: " + msgerror);
            }
        }

        private string GetNumber()
        {
            var tokenValue = new string(_token.Value, 1);
            Match(TokenType.Number);
            var num = Q();
            return tokenValue + num;
        }

        private void Match(TokenType tag)
        {
            if (_token.Tag == tag)
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception("Error sintactico: " + msgerror);
            }
        }

        public double Parse(string regexp)
        {
            _scanner = new Scanner(regexp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            
            var result = 0.00;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    msgerror = "Se esperaba un >> digito << despues del >> - <<";
                    result = E();
                    break;
                case TokenType.LParen:
                    msgerror = "Se esperaba un >> ) <<";
                    result = E();
                    break;
                case TokenType.Number:
                    result = E();
                    break;
                default:
                    msgerror = "se inicio con un caracter no valido";
                    throw new Exception("Error sintactico: " + msgerror);
                    
            }
            Match(TokenType.EOF);
            return result;
        }



    } // End class Parser
}
