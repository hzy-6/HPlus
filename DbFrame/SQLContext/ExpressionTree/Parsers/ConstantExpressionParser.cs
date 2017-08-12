using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{
    class ConstantExpressionParser : ExpressionParser<ConstantExpression>
    {
        public override void Where(ConstantExpression expr, ParserArgs args)
        {
            args.Builder.Append(' ');
            args.AddParameter(expr.Value);
        }

        public override void GroupBy(ConstantExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(ConstantExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(ConstantExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(ConstantExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Select(ConstantExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }



    }
}
