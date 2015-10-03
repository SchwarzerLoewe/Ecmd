using System;
using ECLang.AST;

namespace ECLang.BuildInTypes
{
    public class Null : Statement
    {
        public override Statement Interprete(string src, int line)
        {
            return null;
        }

        public override void Accept(IVisitor v)
        {
            v.Visit(this);
        }

        public override bool IsStatement(string src)
        {
            return src == "null";
        }
    }
}
