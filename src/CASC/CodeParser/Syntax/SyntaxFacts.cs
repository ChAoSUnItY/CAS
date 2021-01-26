using System.Collections.Generic;
using System;

namespace CASC.CodeParser.Syntax
{
    public static class SyntaxFacts
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.BangToken:
                    return 6;

                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.BangEqualsToken:
                case SyntaxKind.GreaterEqualsToken:
                case SyntaxKind.GreaterToken:
                case SyntaxKind.LessEqualsToken:
                case SyntaxKind.LessToken:
                    return 3;

                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;

                case SyntaxKind.PipePipeToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "正":
                case "加":
                    return SyntaxKind.PlusToken;

                case "負":
                case "減":
                    return SyntaxKind.MinusToken;

                case "乘":
                    return SyntaxKind.StarToken;

                case "除":
                    return SyntaxKind.SlashToken;

                case "且":
                    return SyntaxKind.AmpersandAmpersandToken;

                case "或":
                    return SyntaxKind.PipePipeToken;

                case "反":
                    return SyntaxKind.BangToken;

                case "是":
                    return SyntaxKind.EqualsEqualsToken;

                case "不是":
                    return SyntaxKind.BangEqualsToken;

                case "為":
                case "賦":
                    return SyntaxKind.EqualsToken;

                case "大等於":
                    return SyntaxKind.GreaterEqualsToken;
                
                case "大於":
                    return SyntaxKind.GreaterToken;

                case "小等於":
                    return SyntaxKind.LessEqualsToken;

                case "小於":
                    return SyntaxKind.LessToken;

                case "真":
                case "true":
                    return SyntaxKind.TrueKeyword;

                case "假":
                case "false":
                    return SyntaxKind.FalseKeyword;

                case "讓":
                case "使":
                case "let":
                    return SyntaxKind.LetKeyword;

                case "變數":
                case "變值":
                case "var":
                    return SyntaxKind.VarKeyword;

                case "終值":
                case "val":
                    return SyntaxKind.ValKeyword;

                case "如果":
                case "若":
                case "if":
                    return SyntaxKind.IfKeyword;

                case "否則":
                case "else":
                    return SyntaxKind.ElseKeyword;

                case "當":
                case "while":
                    return SyntaxKind.WhileKeyword;

                case "從":
                case "for":
                    return SyntaxKind.ForKeyword;

                case "到":
                case "to":
                    return SyntaxKind.ToKeyword;

                default:
                    return SyntaxKind.IdentifierToken;
            }
        }

        public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));

            foreach (var kind in kinds)
                if (GetUnaryOperatorPrecedence(kind) > 0)
                    yield return kind;
        }

        public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds()
        {
            var kinds = (SyntaxKind[])Enum.GetValues(typeof(SyntaxKind));

            foreach (var kind in kinds)
                if (GetBinaryOperatorPrecedence(kind) > 0)
                    yield return kind;
        }

        public static string GetText(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    return "+";
                case SyntaxKind.MinusToken:
                    return "-";
                case SyntaxKind.StarToken:
                    return "*";
                case SyntaxKind.SlashToken:
                    return "/";
                case SyntaxKind.BangToken:
                    return "!";
                case SyntaxKind.EqualsToken:
                    return "=";
                case SyntaxKind.GreaterEqualsToken:
                    return ">=";
                case SyntaxKind.GreaterToken:
                    return ">";
                case SyntaxKind.LessEqualsToken:
                    return "<=";
                case SyntaxKind.LessToken:
                    return "<";
                case SyntaxKind.AmpersandAmpersandToken:
                    return "&&";
                case SyntaxKind.PipePipeToken:
                    return "||";
                case SyntaxKind.EqualsEqualsToken:
                    return "==";
                case SyntaxKind.BangEqualsToken:
                    return "!=";
                case SyntaxKind.OpenParenthesesToken:
                    return "(";
                case SyntaxKind.CloseParenthesesToken:
                    return ")";
                case SyntaxKind.OpenBraceToken:
                    return "{";
                case SyntaxKind.CloseBraceToken:
                    return "}";
                case SyntaxKind.FalseKeyword:
                    return "false";
                case SyntaxKind.TrueKeyword:
                    return "true";
                case SyntaxKind.LetKeyword:
                    return "let";
                case SyntaxKind.VarKeyword:
                    return "var";
                case SyntaxKind.ValKeyword:
                    return "val";
                case SyntaxKind.IfKeyword:
                    return "if";
                case SyntaxKind.ElseKeyword:
                    return "else";
                case SyntaxKind.WhileKeyword:
                    return "while";
                case SyntaxKind.ForKeyword:
                    return "for";
                case SyntaxKind.ToKeyword:
                    return "to";
                default:
                    return null;
            }
        }

        public static IEnumerable<string> GetZHText(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                    yield return "加";
                    yield return "正";
                    break;
                case SyntaxKind.MinusToken:
                    yield return "減";
                    yield return "負";
                    break;
                case SyntaxKind.StarToken:
                    yield return "乘";
                    break;
                case SyntaxKind.SlashToken:
                    yield return "除";
                    break;
                case SyntaxKind.BangToken:
                    yield return "反";
                    break;
                case SyntaxKind.EqualsToken:
                    yield return "賦";
                    break;
                case SyntaxKind.GreaterEqualsToken:
                    yield return "大等於";
                    break;
                case SyntaxKind.GreaterToken:
                    yield return "大於";
                    break;
                case SyntaxKind.LessEqualsToken:
                    yield return "小等於";
                    break;
                case SyntaxKind.LessToken:
                    yield return "小於";
                    break;
                case SyntaxKind.AmpersandAmpersandToken:
                    yield return "且";
                    break;
                case SyntaxKind.PipePipeToken:
                    yield return "或";
                    break;
                case SyntaxKind.EqualsEqualsToken:
                    yield return "是";
                    break;
                case SyntaxKind.BangEqualsToken:
                    yield return "不是";
                    break;
                case SyntaxKind.OpenParenthesesToken:
                    yield return "(";
                    break;
                case SyntaxKind.CloseParenthesesToken:
                    yield return ")";
                    break;
                case SyntaxKind.OpenBraceToken:
                    yield return "{";
                    break;
                case SyntaxKind.CloseBraceToken:
                    yield return "}";
                    break;
                case SyntaxKind.FalseKeyword:
                    yield return "假";
                    break;
                case SyntaxKind.TrueKeyword:
                    yield return "真";
                    break;
                case SyntaxKind.LetKeyword:
                    yield return "讓";
                    yield return "使";
                    break;
                case SyntaxKind.VarKeyword:
                    yield return "變數";
                    yield return "變值";
                    break;
                case SyntaxKind.ValKeyword:
                    yield return "終值";
                    break;
                case SyntaxKind.IfKeyword:
                    yield return "如果";
                    yield return "若";
                    break;
                case SyntaxKind.ElseKeyword:
                    yield return "否則";
                    break;
                case SyntaxKind.WhileKeyword:
                    yield return "當";
                    break;
                case SyntaxKind.ForKeyword:
                    yield return "從";
                    break;
                case SyntaxKind.ToKeyword:
                    yield return "到";
                    break;
                default:
                    yield break;
            }
        }
    }
}