namespace cmd.Internals.Formating
{
    using System;
    using System.Text;
    using cmd.Internals.Formating;

	/// <summary>
	/// Provides advanced formattation API to client code.
	/// </summary>
	public class Formatter
	{
		#region Factory method

		/// <summary>
		/// Factory method that can be used to create a new <see cref="Formatter"/> instance.
		/// </summary>
		/// <returns>A new <see cref="Formatter"/> instance.</returns>
		public static Formatter CreateFormatter()
		{
			return new Formatter( new TokenizerViaRegex(), new TextFormatterStandard(), new TextAlignerViaStringFormat(), ConstantSet.CreateDefault() );
		}

		#endregion

		private readonly ITextAligner textAligner;
		private readonly IConstantSet constantSet;
		private readonly ITokenizer tokenizer;
		private readonly ITextFormatter textFormatter;

		/// <summary>
		/// Creates a new <see cref="Formatter"/> using the specified 
		/// <paramref name="tokenizer"/> and <paramref name="textAligner"/>.
		/// </summary>
		/// <param name="tokenizer">
		/// The tokenizer that will be used to extract the formatting tokens 
		/// from the format string.
		/// </param>
		/// <param name="textFormatter">
		/// The formatter that will be used to format the entity or property values
		/// before aligning and replacing the formatting tokens into the output string.
		/// </param>
		/// <param name="textAligner">
		/// A class able to align the formatted values before replacing 
		/// the formatting tokens into the output string.
		/// </param>
		/// <param name="constantSet">
		/// The default constant set.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// <i>tokenizer</i> or <i>textAligner</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public Formatter( ITokenizer tokenizer, ITextFormatter textFormatter, ITextAligner textAligner, IConstantSet constantSet )
		{
			if ( tokenizer == null )
				throw new ArgumentNullException( "tokenizer" );
			if ( textFormatter == null )
				throw new ArgumentNullException( "textFormatter" );
			if ( textAligner == null )
				throw new ArgumentNullException( "textAligner" );
			if ( constantSet == null )
				throw new ArgumentNullException( "constantSet" );

			this.tokenizer = tokenizer;
			this.textFormatter = textFormatter;
			this.textAligner = textAligner;
			this.constantSet = constantSet;

			this.Reset();
		}

		/// <summary>
		/// Resets the current <see cref="Formatter"/> configuration option to default values.
		/// </summary>
		/// <returns>
		/// The current formatter (fluent interface).
		/// </returns>
		public Formatter Reset()
		{
			this.textFormatter.Reset();
			return this;
		}

		/// <summary>
		/// Sets the <see cref="System.String"/> to display whenever an object 
		/// or a property is found to be null.
		/// </summary>
		/// <param name="value">
		/// The value to display instead of a null object or property value.
		/// </param>
		/// <returns>
		/// The current formatter (fluent interface).
		/// </returns>
		public Formatter DefaultIfNull( string value )
		{
			this.textFormatter.SetDefaultIfNull( value );
			return this;
		}

		/// <summary>
		/// Gets the current constant set.
		/// </summary>
		public IConstantSet Constants
		{
			get { return constantSet; }
		}

		/// <summary>
		/// Sets the <see cref="System.IFormatProvider"/> that will be used to format
		/// the entity or property values.
		/// </summary>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		/// <returns>
		/// The current formatter (fluent interface).
		/// </returns>
		public Formatter UseFormatProvider( IFormatProvider formatProvider )
		{
			this.textFormatter.SetFormatProvider( formatProvider );
			return this;
		}

		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="formatString"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) a corrisponding <see cref="System.Object"/> instance
		/// in the specified <paramref name="args"/> array.
		/// </summary>
		/// <param name="formatString">A format string containing zero or more formatting tokens.</param>
		/// <param name="args">An object array containing 0 or more objects to format.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the corresponding 
		/// items or property values read from the <paramref name="args"/> array.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> or <i>args</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public string Format( string formatString, params object[] args )
		{
			if ( formatString == null )
				throw new ArgumentNullException( "formatString" );

			if ( args == null )
				args = new object[] { null };

			IToken token;
			var text = formatString;
			var result = new StringBuilder();

			// gets the token
			while ( (token = tokenizer.GetFirstToken( text )) != null )
			{
				// extracts the formatting parameters from the token
				var parameters = token.GetFormatParameters( args, this.constantSet );

				// resolves the formatted property value
				var propertyValue = parameters.GetPropertyValue( args, this.textFormatter );

				// aligns the formatted property value
				var replacement = this.textAligner.Align( propertyValue, parameters.Alignment );

				// appends the replacement to the output string
				result.Append( token.TextBefore( text ) );
				result.Append( replacement );
				text = token.TextAfter( text );
			}
			result.Append( text );
			return result.ToString();
		}
	}
}
