using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SQLContext.ExpressionTree
{
    public interface IExpressionParser
    {
        void Select(Expression expr, ParserArgs args);
        void Where(Expression expr, ParserArgs args);
        void GroupBy(Expression expr, ParserArgs args);
        void Having(Expression expr, ParserArgs args);
        void OrderBy(Expression expr, ParserArgs args);
        void Object(Expression expr, ParserArgs args);

    }
}
