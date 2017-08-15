using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree
{
    public abstract class ExpressionParser<T> : IExpressionParser where T : Expression
    {

        public abstract void Select(T expr, ParserArgs args);
        public abstract void Where(T expr, ParserArgs args);
        public abstract void GroupBy(T expr, ParserArgs args);
        public abstract void Having(T expr, ParserArgs args);
        public abstract void OrderBy(T expr, ParserArgs args);
        public abstract void Object(T expr, ParserArgs args);

        public void Select(Expression expr, ParserArgs args)
        {
            Select((T)expr, args);
        }

        public void Where(Expression expr, ParserArgs args)
        {
            Where((T)expr, args);
        }

        public void GroupBy(Expression expr, ParserArgs args)
        {
            GroupBy((T)expr, args);
        }

        public void Having(Expression expr, ParserArgs args)
        {
            Having((T)expr, args);
        }

        public void OrderBy(Expression expr, ParserArgs args)
        {
            OrderBy((T)expr, args);
        }

        public void Object(Expression expr, ParserArgs args)
        {
            Object((T)expr, args);
        }


    }
}
