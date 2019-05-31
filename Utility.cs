using System;

namespace PathOfThal
{
    public class Utility
    {
        

        public static bool IsChar(char c){
            return Char.IsLetter(c);
        }

        public static bool IsQuote(char c){
            return c == '\'';
        }

        public static bool IsDoubleQuote(char c){
            return c == '\"';
        }

        public static bool isOpenBracket(char c){
            return c == '[';
        }

        static public bool isClosedCurlBracket(char c){
            return c == '}';
        }

        static public bool isOpenCurlBracket(char c){
            return c == '{';
        }

        static public bool isClosedBracket(char c){
            return c == ']';
        }

        static public bool isUseless(char c){
            return c == '\r' || c == '\n';
        }
        static public bool isComment(char c){
            return c == '#';
        }

        static public bool isStar(char c){
            return c == '*';
        }

        static public bool isAnd(char c){
            return c == '&';
        }

        static public bool isDash(char c){
            return c == '-';
        }

        static public bool isNum(char c){
            return Char.IsDigit(c);
        }

        static public bool isComma(char c){
            return c == ',';
        }
    }
}