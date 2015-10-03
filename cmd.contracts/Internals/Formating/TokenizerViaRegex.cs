namespace cmd.Internals.Formating
{
    using System.Text.RegularExpressions;
    using cmd.Internals.Formating;

	/// <summary>
	/// Parses a format string using a <see cref="Regex"/>, and returns the formatting tokens.
	/// </summary>
	public class TokenizerViaRegex : ITokenizer
	{
		private static readonly Regex regex = new Regex( @"\{(?:(?<position>\d+)|(?<position>\d+)\.(?<property>[\w-[0-9]]\w*(\.\w+)*)|(?<property>[\w-[0-9\:\;]]\w*(\.\w+)*)|(?<constant>@[\w-[0-9\:\;]][\w\.]*))(?:,(?<alignment>-?\d+))?(?:\:(?<format>[^\}\n\r\t]+))?\}",
														 RegexOptions.CultureInvariant | RegexOptions.Compiled );

		/// <summary>
		/// Returns the first formatting token in the specified <paramref name="text"/>.
		/// </summary>
		/// <param name="text">The text that contains the formatting <see cref="IToken"/>s.</param>
		/// <returns>
		/// The first formatting token in the specified <paramref name="text"/> if any, 
		/// otherwise <c>null</c>.
		/// </returns>
		public IToken GetFirstToken( string text )
		{
			if ( string.IsNullOrEmpty( text ) ) return null;

			var match = regex.Match( text );
			if ( !match.Success ) return null;
			return new TokenMatch( match );
		}
	}
}