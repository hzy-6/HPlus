using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{
    class UnaryExpressionParser : ExpressionParser<UnaryExpression>
    {
        public override void Where(UnaryExpression expr, ParserArgs args)
        {
            //Parser.Where(expr.Operand, args);
            var val = Helper.Eval_1(expr.Operand);
            args.Builder.Append(' ');
            args.AddParameter(val);
        }

        public override void GroupBy(UnaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(UnaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(UnaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(UnaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Select(UnaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

    }
}
