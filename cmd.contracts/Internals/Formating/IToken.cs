namespace cmd.Internals.Formating
{
	/// <summary>
	/// A formatting token
	/// </summary>
	public interface IToken
	{
		/// <summary>
		/// Gets the token extracted from the format string.
		/// </summary>
		string TokenString { get; }

		/// <summary>
		/// Generates the <see cref="FormatParameters"/> related to the specified 
		/// array of <see cref="System.Object"/>s.
		/// </summary>
		/// <param name="args">The object that will be formatted.</param>
		/// <param name="constantSet"></param>
		/// <returns>
		/// The <see cref="FormatParameters"/> related to the specified array of <see cref="System.Object"/>s.
		/// </returns>
		FormatParameters GetFormatParameters( object[] args, IConstantSet constantSet );

		/// <summary>
		/// Gets the part of the specified <paramref name="text"/> that ends 
		/// at the beginning of the current <see cref="IToken"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>
		/// The part of the specified <paramref name="text"/> that ends 
		/// at the beginning of the current <see cref="IToken"/>.
		/// </returns>
		string TextBefore( string text );

		/// <summary>
		/// Gets the part of the specified <paramref name="text"/> that starts 
		/// at the end of the current <see cref="IToken"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>
		/// The part of the specified <paramref name="text"/> that starts 
		/// at the end of the current <see cref="IToken"/>.
		/// </returns>
		string TextAfter( string text );
	}
}