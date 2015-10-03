namespace cmd.Internals.Formating
{
	/// <summary>
	/// Left or right aligns a specific text.
	/// </summary>
	public interface ITextAligner
	{
		/// <summary>
		/// Left or right aligns the specified <paramref name="text"/> 
		/// using the specified <paramref name="alignment"/> value.
		/// The absolute value of the alignment represents the minimum size of the field
		/// that will contain the text.
		/// The sign of the aligment represents the position of the padding chars 
		/// (- stands for left-aligment, + stands for right-aligment).
		/// </summary>
		/// <param name="text">The text to align.</param>
		/// <param name="alignment">The aligment value.</param>
		/// <returns>
		/// The specified <paramref name="text"/> aligned using the specified 
		/// <paramref name="alignment"/> value.
		/// </returns>
		string Align( string text, int alignment );
	}
}