using System;
using System.IO;
using cmd.contracts;
using cmd.contracts.Help;

namespace MenuPlugin
{
	public class MenuCommand : ICommand
	{
		public override bool Interact(ref DirectoryInfo path, Command command)
		{
			Console.WriteLine("Menu sample: Simple text formatting.");
			Menu m = new Menu();
			m.Add("Convert to Upper-case", new MenuCallback(DoUppercase));
			m.Add("Convert to Lower-case", new MenuCallback(DoLowercase));
			m.Add("Capitalize", new MenuCallback(DoCapitalize));
			m.Show();

			return true;
		}

		public override HelpBuilder Help
		{
			get
			{
				var b = new HelpBuilder();

				b.Add("menu", "A Menu Sample",
					"menu");

				return b;
			}
		}

		public override bool Accept(Command command)
		{
			return command.Name == "menu";
		}

		private static void DoUppercase()
		{
			Console.WriteLine();
			Console.WriteLine("Enter the text to convert to upper-case:");
		
			string text = Console.ReadLine();
		
			text = text.ToUpper();
		
			Console.WriteLine("Upper-case text::");
		
			Console.WriteLine(text);
		}
	
		private static void DoLowercase()
		{
			Console.WriteLine();
			Console.WriteLine("Enter the text to convert to lower-case:");
		
			string text = Console.ReadLine();
		
			text = text.ToLower();
		
			Console.WriteLine("Lower-case text:");
		
			Console.WriteLine(text);		
		}

		private static void DoCapitalize()
		{
			Console.WriteLine();
			Console.WriteLine("Enter the text to be capitalized:");

			char[] text = Console.ReadLine().ToCharArray();

			string capitalized = "";

			bool IsNextCapital = true;

			for (int i = 0; i < text.Length; ++i)
			{
				string curchar = text[i].ToString();

				string pairchar = "";
				string lastchar = "";

				try
				{
					pairchar = text[i - 2].ToString() + text[i - 1].ToString();
					lastchar = text[i - 1].ToString();
				}
				catch
				{
					pairchar = "  ";
					lastchar = " ";
				}

				if ((i == 0) || (lastchar == "(") || (pairchar == "? ") || (pairchar == "! ") || (pairchar == ". "))
				{
					IsNextCapital = true;
				}
				else
				{
					IsNextCapital = false;
				}
				capitalized += IsNextCapital ? curchar.ToUpper() : curchar.ToLower();
			}

			Console.WriteLine("Capitalized text:");

			Console.WriteLine(capitalized);
		}
	}
}