namespace cmd.Internals.Formating
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
	/// A formatting token that relies on a <see cref="Regex"/> <see cref="Match"/>.
	/// </summary>
	public class TokenMatch : IToken
	{
		private readonly Match match;

		/// <summary>
		/// Creates a new <see cref="TokenMatch"/> that wraps a specific <see cref="Match"/> instance.
		/// </summary>
		/// <param name="match">The <see cref="Match"/> to wrap.</param>
		public TokenMatch( Match match )
		{
			this.match = match;
		}

		/// <summary>
		/// Gets the token extracted from the format string.
		/// </summary>
		public string TokenString
		{
			get { return this.match.Value; }
		}

		/// <summary>
		/// Gets the part of the specified <paramref name="text"/> that ends 
		/// at the beginning of the current <see cref="IToken"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>
		/// The part of the specified <paramref name="text"/> that ends 
		/// at the beginning of the current <see cref="IToken"/>.
		/// </returns>
		public string TextBefore( string text )
		{
			if ( string.IsNullOrEmpty( text ) )
				throw new ArgumentNullException( "text" );

			return this.match.Index <= 0 ? string.Empty : text.Substring( 0, this.match.Index );
		}

		/// <summary>
		/// Gets the part of the specified <paramref name="text"/> that starts 
		/// at the end of the current <see cref="IToken"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>
		/// The part of the specified <paramref name="text"/> that starts 
		/// at the end of the current <see cref="IToken"/>.
		/// </returns>
		public string TextAfter( string text )
		{
			if ( string.IsNullOrEmpty( text ) )
				throw new ArgumentNullException( "text" );

			var offset = this.match.Index + this.match.Length;
			return text.Length <= offset ? string.Empty : text.Substring( offset );
		}

		/// <summary>
		/// Generates the <see cref="FormatParameters"/> related to the specified 
		/// array of <see cref="System.Object"/>s.
		/// </summary>
		/// <param name="args">The object that will be formatted.</param>
		/// <param name="constantSet"></param>
		/// <returns>
		/// The <see cref="FormatParameters"/> related to the specified array of <see cref="System.Object"/>s.
		/// </returns>
		public FormatParameters GetFormatParameters( object[] args, IConstantSet constantSet )
		{
			if ( args == null )
				throw new ArgumentNullException( "args" );

			var positionString = match.Groups["position"].Value;
			positionString = string.IsNullOrEmpty( positionString ) ? "0" : positionString;
			int position;
			if ( !int.TryParse( positionString, out position ) )
				throw new FormatException( "Invalid position: " + positionString );

			if ( position < 0 || position >= args.Length )
				throw new FormatException( "Invalid position: " + positionString + " should be between 0 and " + (args.Length - 1) );

			var alignmentString = match.Groups["alignment"].Value;
			alignmentString = string.IsNullOrEmpty( alignmentString ) ? "0" : alignmentString;
			int alignment;
			if ( !int.TryParse( alignmentString, out alignment ) )
				throw new FormatException( "Invalid alignment: " + alignmentString );

			var propertyName = match.Groups["property"].Value;

			var propertyPath = (propertyName.StartsWith( "." ) ? propertyName.Substring( 1 ) : propertyName).Split( '.' );
			if ( propertyPath.Length == 1 && string.IsNullOrEmpty( propertyPath[0] ) ) propertyPath = new string[0];
			if ( propertyPath.Length > 1 && propertyPath.Any( property => string.IsNullOrEmpty( property ) ) )
				throw new FormatException( "Invalid property: double dot in property path." );

			var format = match.Groups["format"].Value;

			object constantValue = null;
			var constantGroup = match.Groups["constant"];
			if ( constantGroup.Success )
			{
				var constantName = constantGroup.Value;
				constantValue = constantSet.GetValue( constantName );
			}

			return new FormatParameters( position, propertyPath, alignment, format, constantValue );
		}


		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return this.TokenString;
		}
	}
}