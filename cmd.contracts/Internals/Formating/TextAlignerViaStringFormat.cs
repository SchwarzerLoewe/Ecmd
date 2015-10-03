namespace cmd.Internals.Formating
{
	/// <summary>
	/// Left or right aligns a specific text using the "String.Format" method.
	/// </summary>
	public class TextAlignerViaStringFormat : ITextAligner
	{
		/// <summary>
		/// Uses the default "String.Format" method to align the specified <paramref name="text"/>
		/// using the specified <paramref name="alignment"/> value.
		/// </summary>
		/// <param name="text">The text to align.</param>
		/// <param name="alignment">The aligment value.</param>
		/// <returns>
		/// The specified <paramref name="text"/> aligned using the specified 
		/// <paramref name="alignment"/> value.
		/// </returns>
		public string Align( string text, int alignment )
		{
			return alignment == 0 ? text : string.Format( "{0," + alignment + "}", text );
		}
	}
}