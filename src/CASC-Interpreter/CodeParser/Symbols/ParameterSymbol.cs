namespace CASC.CodeParser.Symbols
{
    public sealed class ParameterSymbol : LocalVariableSymbol {
        public ParameterSymbol(string name, TypeSymbol type)
            : base(name, isFinalized: true, type)
        {
        }

        public override SymbolKind Kind => SymbolKind.Parameter;
    }
}