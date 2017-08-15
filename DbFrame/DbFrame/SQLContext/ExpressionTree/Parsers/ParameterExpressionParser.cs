using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{
    class ParameterExpressionParser : ExpressionParser<ParameterExpression>
    {
        public override void Where(ParameterExpression expr, ParserArgs args)
        {
            args.Builder.Append(' ');
            args.Builder.Append(expr.Name);
            args.Builder.Append('.');
        }

        public override void GroupBy(ParameterExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(ParameterExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(ParameterExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(ParameterExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Select(ParameterExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }




    }
}
