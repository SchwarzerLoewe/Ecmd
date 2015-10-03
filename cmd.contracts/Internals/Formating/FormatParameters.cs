namespace cmd.Internals.Formating
{
	using System;
	using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using cmd.Internals.Formating;

	/// <summary>
	/// Formatting parameters extracted from a formatting token.
	/// </summary>
	public class FormatParameters
	{
		/// <summary>
		/// Creates a new <see cref="FormatParameters"/> instance.
		/// </summary>
		/// <param name="position">The index of the object in the argument list.</param>
		/// <param name="propertyPath">The property path.</param>
		/// <param name="alignment">The aligment value.</param>
		/// <param name="format">The format string.</param>
		/// <param name="constant">The constant value to be rendered.</param>
		public FormatParameters( int position, IList<string> propertyPath, int alignment, string format, object constant )
		{
			this.Position = position;
			this.PropertyPath = new ReadOnlyCollection<string>( propertyPath );
			this.Alignment = alignment;
			this.Format = format;
			this.Constant = constant;
		}

		/// <summary>
		/// Gets the index of the object in the argument list.
		/// </summary>
		public int Position { get; private set; }

		/// <summary>
		/// Gets the property path.
		/// </summary>
		public ReadOnlyCollection<string> PropertyPath { get; private set; }

		/// <summary>
		/// Gets the aligment value.
		/// </summary>
		public int Alignment { get; private set; }

		/// <summary>
		/// Gets the format string.
		/// </summary>
		public string Format { get; private set; }

		/// <summary>
		/// Gets the constant value to render.
		/// </summary>
		public object Constant { get; private set; }



		/// <summary>
		/// Evaluates and returns the property value obtained matching the current
		/// <see cref="FormatParameters" /> against the specified array of <see href="System.Object"/>s, 
		/// and formatting the result using the specified <paramref name="textFormatter"/>.
		/// </summary>
		/// <param name="args">
		/// The array of <see href="System.Object"/>s to match against the current <see cref="FormatParameters" />.
		/// </param>
		/// <param name="textFormatter">
		/// The formatter that will be used to format the entity or property values.
		/// </param>
		/// <returns>
		/// The formatted property value obtained matching the current <see cref="FormatParameters" /> 
		/// against the specified array of <see href="System.Object"/>s, and formatting the result
		/// using the specified <paramref name="textFormatter"/>.
		/// </returns>
		public string GetPropertyValue( object[] args, ITextFormatter textFormatter )
		{
			if ( args == null )
				throw new ArgumentNullException( "args" );
			if ( textFormatter == null )
				throw new ArgumentNullException( "textFormatter" );

			var instance = args[this.Position];
			return GetPropertyValue( instance, 0, textFormatter );
		}

		/// <summary>
		/// Recourse against the current property path to retrieve the property value to return.
		/// </summary>
		/// <param name="obj">The object containing the value to extract.</param>
		/// <param name="currentPathIndex">
		/// The recursion index, indicating the current position in the property path.
		/// </param>
		/// <param name="textFormatter">
		/// The formatter that will be used to format the entity or property values.
		/// </param>
		/// <returns>
		/// The formatted property value obtained matching the current <see cref="FormatParameters" /> 
		/// against the specified array of <see href="System.Object"/>s, and formatting the result
		/// using the specified <paramref name="textFormatter"/>.
		/// </returns>
		private string GetPropertyValue( object obj, int currentPathIndex, ITextFormatter textFormatter )
		{
			// if there is a constant value, returns it
			if ( this.Constant != null ) return textFormatter.Format( this.Format, Constant );

			// if object is null, cannot go ahead with the resolution.)
			if ( obj == null ) return textFormatter.Format( this.Format, null );

			// if the current property in the property path is null or empty, 
			// should return the formatted whole object.
			var propertyName = currentPathIndex < this.PropertyPath.Count ? this.PropertyPath[currentPathIndex] : null;
			if ( string.IsNullOrEmpty( propertyName ) )
			{
				return textFormatter.Format( this.Format, obj );
			}

			// else extracts the current property
			var type = obj.GetType();
			var property = type.GetProperty( propertyName );
			if ( property == null )
				throw new FormatException( "Invalid property: " + propertyName );

			// extracts the property value from the object
			var propertyValue = property.GetValue( obj, null );

			// recourse to resolve the property value;
			return GetPropertyValue( propertyValue, currentPathIndex + 1, textFormatter );
		}
	}
}