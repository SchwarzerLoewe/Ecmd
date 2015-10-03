using System;
using System.Collections.Generic;

namespace cmd.Internals.Formating
{
	/// <summary>
	/// A set of constants
	/// </summary>
	public interface IConstantSet
	{
		/// <summary>
		/// Adds a constant <paramref name="value"/> to the current set.
		/// </summary>
		/// <typeparam name="T">The type of the constant value.</typeparam>
		/// <param name="name">The name of the constant to add.</param>
		/// <param name="value">The constant value.</param>
		void Add<T>( string name, T value );

		/// <summary>
		/// Adds a constant <paramref name="valueProvider"/> to the current set.
		/// </summary>
		/// <typeparam name="T">The type of the constant value.</typeparam>
		/// <param name="name">The name of the constant to add.</param>
		/// <param name="valueProvider">A delegate that can be used to get the constant value.</param>
		void Add<T>( string name, Func<T> valueProvider );

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
		object GetValue( string name );

		/// <summary>
		/// Gets the list of constants in the current set.
		/// </summary>
		IEnumerable<KeyValuePair<string, object>> Values { get; }
	}
}