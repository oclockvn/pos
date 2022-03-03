namespace pos.common.extensions
{
    /// <summary>
    /// String extension
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Add tax to the given value
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="taxPercentage">The tax percentage</param>
        /// <returns>The result</returns>
        public static decimal WithTax(this decimal value, float taxPercentage = 0)
        {
            return value + (taxPercentage == 0 ? 0 : (decimal)taxPercentage * value / 100m);
        }
    }
}
