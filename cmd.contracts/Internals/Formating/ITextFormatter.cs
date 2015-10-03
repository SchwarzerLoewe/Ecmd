namespace cmd.Internals.Formating
{
	using System;

	/// <summary>
	/// Returns a the string equivalent representation of a specific object.
	/// </summary>
	public interface ITextFormatter
	{
		/// <summary>
		/// Resets the formatter configuration to it's original values.
		/// </summary>
		void Reset();

		/// <summary>
		/// Sets the <see cref="System.IFormatProvider"/> that will be used to format
		/// the entity or property values.
		/// </summary>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		void SetFormatProvider( IFormatProvider formatProvider );

		/// <summary>
		/// Sets the <see cref="System.String"/> to display whenever an object 
		/// or a property is found to be null.
		/// </summary>
		/// <param name="defaultIfNull">
		/// The value to display instead of a null object or property value.
		/// </param>
		void SetDefaultIfNull( string defaultIfNull );

		/// <summary>
		/// Replaces the format item in a specified <see cref="System.String"/> with the 
		/// text equivalent of the value of a specified <see cref="System.Object"/> instance.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="obj">The object to format.</param>
		/// <returns>
		///	The text equivalent of the value of a specified <see cref="System.Object"/> instance.
		/// </returns>
		string Format( string format, object obj );
	}
}