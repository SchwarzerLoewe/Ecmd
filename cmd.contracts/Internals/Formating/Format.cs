using System;
using cmd.Internals.Formating;

namespace cmd.Internals.Formating
{
	/// <summary>
	/// Static helper class for advanced object formatting.
	/// </summary>
	public static class Format
	{
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="System.Object"/>.
		/// </summary>
		/// <param name="obj">The current <see cref="System.Object"/>.</param>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="System.Object"/>.
		/// </returns>
		public static string ToSmartString( this object obj )
		{
			return ToString( "{0}", obj );
		}


		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="format"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/>
		/// </summary>
		/// <param name="obj">The current <see cref="System.Object"/>.</param>
		/// <param name="format">A format string containing zero or more formatting tokens.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/>
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public static string ToSmartString( this object obj, string format )
		{
			return ToString( format, obj );
		}


		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="format"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/> or 
		/// from (a property of) one of the objects in the current <paramref name="args"/> array.
		/// </summary>
		/// <param name="obj">The current <see cref="System.Object"/>.</param>
		/// <param name="format">A format string containing zero or more formatting tokens.</param>
		/// <param name="args">An object array containing 0 or more objects to format.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/> or 
		/// from (a property of) one of the objects in the current <paramref name="args"/> array.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public static string ToSmartString( this object obj, string format, params object[] args )
		{
			return ToSmartString( obj, null, format, args );
		}


		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="format"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/> or 
		/// from (a property of) one of the objects in the current <paramref name="args"/> array.
		/// </summary>
		/// <param name="formatProvider">A format provider that will be used to format localized values.</param>
		/// <param name="obj">The current <see cref="System.Object"/>.</param>
		/// <param name="format">A format string containing zero or more formatting tokens.</param>
		/// <param name="args">An object array containing 0 or more objects to format.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the values extracted 
		/// from (a property of) the current <see cref="System.Object"/> or 
		/// from (a property of) one of the objects in the current <paramref name="args"/> array.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public static string ToSmartString( this object obj, IFormatProvider formatProvider, string format, params object[] args )
		{
			object[] newArgs;
			if ( args == null )
			{
				newArgs = new[] { obj };
			}
			else
			{
				newArgs = new object[args.Length + 1];
				newArgs[0] = obj;
				for ( var i = 0; i < args.Length; i++ )
				{
					newArgs[i + 1] = args[i];
				}
			}

			return ToString( formatProvider, format, newArgs );
		}


		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="format"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) a corrisponding <see cref="System.Object"/> instance
		/// in the specified <paramref name="args"/> array.
		/// </summary>
		/// <param name="format">A format string containing zero or more formatting tokens.</param>
		/// <param name="args">An object array containing 0 or more objects to format.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the corresponding 
		/// items or property values read from the <paramref name="args"/> array.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> or <i>args</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public static string ToString( string format, params object[] args )
		{
			return ToString( null, format, args );
		}


		/// <summary>
		/// Replaces the formatting tokens in a specified <paramref name="format"/> 
		/// <see cref="System.String"/> with the text equivalent of the values extracted 
		/// from (a property of) a corrisponding <see cref="System.Object"/> instance
		/// in the specified <paramref name="args"/> array.
		/// </summary>
		/// <param name="formatProvider">A format provider that will be used to format localized values.</param>
		/// <param name="format">A format string containing zero or more formatting tokens.</param>
		/// <param name="args">An object array containing 0 or more objects to format.</param>
		/// <returns>
		/// A copy of the format string in which the formatting tokens have been replaced 
		/// by the <see cref="T:System.String"/> equivalent of the corresponding 
		/// items or property values read from the <paramref name="args"/> array.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// <i>format</i> or <i>args</i> is a null reference (<b>Nothing</b> in Visual Basic).
		/// </exception>
		public static string ToString( IFormatProvider formatProvider, string format, params object[] args )
		{
			var formatter = Formatter.CreateFormatter();
			if ( formatProvider != null )
			{
				formatter.UseFormatProvider( formatProvider );
			}
			return formatter.Format( format, args );
		}
	}
}
