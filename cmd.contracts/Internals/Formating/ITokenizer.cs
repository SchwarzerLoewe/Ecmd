namespace cmd.Internals.Formating
{
	/// <summary>
	/// Parses a format string returning the formatting tokens.
	/// </summary>
	public interface ITokenizer
	{
		/// <summary>
		/// Returns the first formatting token in the specified <paramref name="text"/>.
		/// </summary>
		/// <param name="text">The text that contains the formatting <see cref="IToken"/>s.</param>
		/// <returns>
		/// The first formatting token in the specified <paramref name="text"/> if any, 
		/// otherwise <c>null</c>.
		/// </returns>
		IToken GetFirstToken( string text );
	}
}