using System;
using System.Security.Principal;
using cmd.Internals.Formating;

namespace cmd.Internals.Formating
{
	/// <summary>
	/// Default constant set
	/// </summary>
	public class ConstantSet : ConstantSetBase
	{
		/// <summary>
		/// Creates an empty constant set.
		/// </summary>
		public static IConstantSet CreateEmpty()
		{
			return new ConstantSet();
		}

		/// <summary>
		/// Creates and initializes the default constant set.
		/// </summary>
		public static IConstantSet CreateDefault()
		{
			var constantSet = CreateEmpty();
			constantSet.Add( "@NewLine", Environment.NewLine );
			constantSet.Add( "@Tab", "\t" );
			constantSet.Add( "@Today", () => DateTime.Today );
			constantSet.Add( "@Yesterday", () => DateTime.Today.AddDays( -1 ) );
			constantSet.Add( "@Tomorrow", () => DateTime.Today.AddDays( 1 ) );
			constantSet.Add( "@Now", () => DateTime.Now );
			constantSet.Add( "@User", () =>
			{
				var user = WindowsIdentity.GetCurrent();
				return user != null ? user.Name : null;
			} );
			return constantSet;
		}

		private ConstantSet()
		{
		}
	}
}