namespace Infra.Extensions
{
    public static class DecimalExtensions
    {
        public static string FormatMoney(this decimal valor)
        {
            return $"R$ {valor:N}";
        }
    }
}
