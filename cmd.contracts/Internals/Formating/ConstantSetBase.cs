using System;
using System.Collections.Generic;
using System.Linq;
using cmd.Internals.Formating;

namespace cmd.Internals.Formating
{
	/// <summary>
	/// Base class for constant sets.
	/// </summary>
	public abstract class ConstantSetBase : IConstantSet
	{
		private readonly Dictionary<string, Delegate> dictionary = new Dictionary<string, Delegate>();

		/// <summary>
		/// Adds a constant <paramref name="value"/> to the current set.
		/// </summary>
		/// <typeparam name="T">The type of the constant value.</typeparam>
		/// <param name="name">The name of the constant to add.</param>
		/// <param name="value">The constant value.</param>
		public void Add<T>( string name, T value )
		{
			this.Add( name, () => value );
		}

		/// <summary>
		/// Adds a constant <paramref name="valueProvider"/> to the current set.
		/// </summary>
		/// <typeparam name="T">The type of the constant value.</typeparam>
		/// <param name="name">The name of the constant to add.</param>
		/// <param name="valueProvider">A delegate that can be used to get the constant value.</param>
		public void Add<T>( string name, Func<T> valueProvider )
		{
			dictionary[name] = valueProvider;
		}

		/// <summary>
		/// Gets the value of the constant with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name of the constant to retrieve.</param>
		/// <returns>
		/// The value of the constant with the specified <paramref name="name"/>.
		/// </returns>
		/// <exception cref="System.InvalidOperationException">
		/// If the specified constant <paramref name="name"/> is not present in the current set.
		/// </exception>
		public object GetValue( string name )
		{
			if ( !dictionary.ContainsKey( name ) )
				throw new InvalidOperationException( "Invalid constant: " + name );

			var provider = dictionary[name];
			var value = provider.DynamicInvoke();
			return value;
		}

		/// <summary>
		/// Gets the list of constants in the current set.
		/// </summary>
		public IEnumerable<KeyValuePair<string, object>> Values
		{
			get
			{
				return this.dictionary.OrderBy( x => x.Key )
					.Select( x => new KeyValuePair<string, object>( x.Key, x.Value.DynamicInvoke() ) );
			}
		}
	}
}