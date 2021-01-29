using CASC.CodeParser.Symbols;
using CASC.CodeParser.Syntax;

namespace CASC.CodeParser.Binding
{
    internal sealed class BoundUnaryOperator
    {
        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, TypeSymbol operandType)
            : this(syntaxKind, kind, operandType, operandType)
        {
        }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, TypeSymbol operandType, TypeSymbol resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
            Type = resultType;
        }

        public SyntaxKind SyntaxKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public TypeSymbol OperandType { get; }
        public TypeSymbol Type { get; }

        private static BoundUnaryOperator[] _operators =
        {
            new BoundUnaryOperator(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, TypeSymbol.Bool),

            new BoundUnaryOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, TypeSymbol.Number),
            new BoundUnaryOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, TypeSymbol.Number),
            new BoundUnaryOperator(SyntaxKind.TildeToken, BoundUnaryOperatorKind.OnesComplement, TypeSymbol.Number)
        };

        public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, TypeSymbol operandType)
        {
            foreach (var op in _operators)
                if (op.SyntaxKind == syntaxKind && op.OperandType == operandType)
                    return op;

            return null;
        }
    }
}