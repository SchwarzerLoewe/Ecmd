﻿namespace ECLang.Internal
{
    using System;
    using System.Diagnostics;

    internal class Brainfuck
    {
        // Instance info.
        // Instance of Brainfuck interpreter, actually handles interpreting.

        /** The array that acts as the interpreter's virtual memory */

        /** The string containing the comands to be executed */

        #region Static Fields

        public static int EXIT_FAILURE = -1;

        public static int EXIT_SUCCESS = 1;

        #endregion

        #region Fields

        private readonly int EOF; //End Of File

        private readonly char[] com;

        private readonly char[] mem;

        /** The instruction pointer */

        private int ip;

        private int mp;

        #endregion

        /**
        * Create the Brainfuck VM and give it the string to be interpreted.
        * @param s The string to be interpreted.
        */

        #region Constructors and Destructors

        public Brainfuck(string s)
        {
            this.mem = new char[30000];
            this.mp = 0;
            this.com = s.ToCharArray();
            this.EOF = this.com.Length;
        }

        #endregion

        /**
        * Run the interpreter with its given string
        */

        #region Public Methods and Operators

        public void run()
        {
            while (this.ip < this.EOF)
            {
                // Get the current command
                char c = this.com[this.ip];

                // Act based on the current command and the brainfuck spec
                switch (c)
                {
                    case '>':
                        this.mp++;
                        break;
                    case '<':
                        this.mp--;
                        break;
                    case '+':
                        this.mem[this.mp]++;
                        break;
                    case '-':
                        this.mem[this.mp]--;
                        break;
                    case '.':
                        Console.Write((this.mem[this.mp]));
                        break;
                    case ',':
                        try
                        {
                            this.mem[this.mp] = (char)Console.Read();
                        }
                        catch (Exception e)
                        {
                            Debug.Write(e.StackTrace);
                        }
                        break;
                    case '[':
                        if (this.mem[this.mp] == 0)
                        {
                            while (this.com[this.ip] != ']')
                            {
                                this.ip++;
                            }
                        }
                        break;

                    case ']':
                        if (this.mem[this.mp] != 0)
                        {
                            while (this.com[this.ip] != '[')
                            {
                                this.ip--;
                            }
                        }
                        break;
                }

                // increment instruction mp
                this.ip++;
            }
        }

        #endregion

        // Static stuff - boilerplate code and file reading

        /**
        * Set up a single instance of the brainfuck interpreter, and run it, with the given string or file.
        * @param args Command line arguments
        */

        #region Methods

        /**
        * Called when incorrect parameters are used.
        */

        private static void Usage()
        {
            Console.Write("Usage:\n\tbrainfuck -f <filename>\n\tbrainfuck -i <string>\n");
            Console.Write("For help:\n\tbrainfuck -h\n\tbrainfuck --help\n");
            Console.Read(); // Pause before closing console window
            Environment.Exit(EXIT_FAILURE);
        }

        #endregion
    }
}