using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Shared
{
    public sealed class Currency : ValueObject
    {
        public string Code { get; }
        public string Symbol { get; }

        private Currency(string code, string symbol)
        {
            Code = code;
            Symbol = symbol;
        }

        public static readonly Currency None = new("", "");
        public static readonly Currency Php = new("PESO", "₱");

        public static Currency FromCode(string code)
        {
            return code.ToUpper() switch
            {
                "PESO" => Php,
                _ => None
            };
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
        }

        public override string ToString() => Code;
    }
}
