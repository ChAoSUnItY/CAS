using CASC.CodeParser.Utilities;
using CASC.CodeParser.Text;

namespace CASC.CodeParser.Syntax
{
    internal sealed class Lexer
    {

        private readonly SourceText _text;
        private readonly DiagnosticPack _diagnostics = new DiagnosticPack();

        private int _position;

        private int _start;
        private SyntaxKind _kind;
        private object _value;

        public Lexer(SourceText text)
        {
            _text = text;
        }

        public DiagnosticPack Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char LookAhead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;

            if (index >= _text.Length)
                return '\0';

            return _text[index];
        }

        public SyntaxToken Lex()
        {
            _start = _position;
            _kind = SyntaxKind.BadToken;
            _value = null;

            switch (Current)
            {
                case '\0':
                    _kind = SyntaxKind.EndOfFileToken;
                    break;

                case '+':
                    _kind = SyntaxKind.PlusToken;
                    _position++;
                    break;

                case '-':
                    _kind = SyntaxKind.MinusToken;
                    _position++;
                    break;

                case '*':
                    _kind = SyntaxKind.StarToken;
                    _position++;
                    break;

                case '/':
                    _kind = SyntaxKind.SlashToken;
                    _position++;
                    break;

                case '(':
                    _kind = SyntaxKind.OpenParenthesesToken;
                    _position++;
                    break;

                case ')':
                    _kind = SyntaxKind.CloseParenthesesToken;
                    _position++;
                    break;

                case '{':
                    _kind = SyntaxKind.OpenBraceToken;
                    _position++;
                    break;

                case '}':
                    _kind = SyntaxKind.CloseBraceToken;
                    _position++;
                    break;

                case '&':
                    if (LookAhead == '&')
                    {
                        _kind = SyntaxKind.AmpersandAmpersandToken;
                        _position += 2;
                        break;
                    }
                    break;

                case '|':
                    if (LookAhead == '|')
                    {
                        _kind = SyntaxKind.PipePipeToken;
                        _position += 2;
                        break;
                    }
                    break;

                case '!':
                    if (LookAhead == '=')
                    {
                        _kind = SyntaxKind.BangEqualsToken;
                        _position += 2;
                        break;
                    }
                    _kind = SyntaxKind.BangToken;
                    _position++;
                    break;

                case '=':
                    if (LookAhead != '=')
                    {
                        _kind = SyntaxKind.EqualsToken;
                        _position++;
                    }
                    _kind = SyntaxKind.EqualsEqualsToken;
                    _position += 2;
                    break;

                case '>':
                    _position++;
                    if (Current != '=')
                    {
                        _kind = SyntaxKind.GreaterToken;
                    }
                    else
                    {
                        _kind = SyntaxKind.GreaterEqualsToken;
                        _position++;
                    }
                    break;
                case '<':
                    _position++;
                    if (Current != '=')
                    {
                        _kind = SyntaxKind.LessToken;
                    }
                    else
                    {
                        _kind = SyntaxKind.LessEqualsToken;
                        _position++;
                    }
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '零':
                case '一':
                case '二':
                case '三':
                case '四':
                case '五':
                case '六':
                case '七':
                case '八':
                case '九':
                case '壹':
                case '貳':
                case '參':
                case '肆':
                case '伍':
                case '陆':
                case '柒':
                case '捌':
                case '玖':
                case '拾':
                case '十':
                case '百':
                case '千':
                case '萬':
                case '億':
                    ReadNumberToken();
                    break;

                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    ReadWhiteSpaceToken();
                    break;

                default:
                    if (char.IsLetter(Current))
                        ReadIdentifierOrKeyword();
                    else if (char.IsWhiteSpace(Current))
                        ReadWhiteSpaceToken();
                    else
                    {
                        _diagnostics.ReportBadCharacter(_position, Current);
                        _position++;
                    }
                    break;
            }

            var length = _position - _start;
            var text = SyntaxFacts.GetText(_kind);
            if (text == null)
                text = _text.ToString(_start, length);

            return new SyntaxToken(_kind, _start, text, _value);
        }

        private void ReadNumberToken()
        {
            while (ChineseParser.isDigit(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            if (!ChineseParser.tryParseDigits(text, out var value))
                _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), text, typeof(decimal));

            _value = value;
            _kind = SyntaxKind.NumberToken;
        }

        private void ReadWhiteSpaceToken()
        {
            while (char.IsWhiteSpace(Current))
                _position++;

            _kind = SyntaxKind.WhiteSpaceToken;
        }

        private string ReadIdentifierOrKeyword()
        {
            while (char.IsLetter(Current))
                _position++;

            var length = _position - _start;
            var text = _text.ToString(_start, length);
            _kind = SyntaxFacts.GetKeywordKind(text);
            return text;
        }
    }

}