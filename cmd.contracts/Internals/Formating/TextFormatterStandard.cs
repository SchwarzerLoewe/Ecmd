using System;
using System.Globalization;

namespace cmd.Internals.Formating
{
	/// <summary>
	/// Default text formatter.
	/// </summary>
	public class TextFormatterStandard : ITextFormatter
	{
		private IFormatProvider currentFormatProvider;
		private string currentDefaultIfNull;

		/// <summary>
		/// Creates a new <see cref="TextFormatterStandard"/> that uses <c>String.Empty</c>
		/// as current default value for <c>null</c> properties or entities, and 
		/// <c>CultureInfo.CurrentCulture</c> as default culture.
		/// </summary>
		public TextFormatterStandard()
		{
			this.Reset();
		}

		/// <summary>
		/// Resets the formatter configuration to it's original values.
		/// </summary>
		public void Reset()
		{
			this.currentFormatProvider = CultureInfo.CurrentCulture;
			this.currentDefaultIfNull = string.Empty;
		}

		/// <summary>
		/// Sets the <see cref="System.IFormatProvider"/> that will be used to format
		/// the entity or property values.
		/// </summary>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		public void SetFormatProvider( IFormatProvider formatProvider )
		{
			this.currentFormatProvider = formatProvider ?? CultureInfo.CurrentCulture;
		}

		/// <summary>
		/// Sets the <see cref="System.String"/> to display whenever an object 
		/// or a property is found to be null.
		/// </summary>
		/// <param name="defaultIfNull">
		/// The value to display instead of a null object or property value.
		/// </param>
		public void SetDefaultIfNull( string defaultIfNull )
		{
			this.currentDefaultIfNull = defaultIfNull ?? string.Empty;
		}


		/// <summary>
		/// Replaces the format item in a specified <see cref="System.String"/> with the 
		/// text equivalent of the value of a specified <see cref="System.Object"/> instance.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="obj">The object to format.</param>
		/// <returns>
		///	The text equivalent of the value of a specified <see cref="System.Object"/> instance.
		/// </returns>
		public string Format( string format, object obj )
		{
			// if the object is null, return null.
			if ( obj == null ) return this.currentDefaultIfNull;

			// if no format specified, return the ToString() value
			if ( string.IsNullOrEmpty( format ) ) return obj.ToString();

			// if the object is formattable, uses the default formatting extensions
			var formattable = obj as IFormattable;
			if ( formattable != null )
			{
				return formattable.ToString( format, this.currentFormatProvider );
			}

			// else, delegates the formatting to the default String.Format
			return string.Format( this.currentFormatProvider, "{0:" + format + "}", obj );
		}
	}
}