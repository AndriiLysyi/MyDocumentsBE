using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.DAL.Entities;
using MyDocuments.Services.Models;

namespace MyDocuments.Services.Services
{
    public class SearchPredicateService
    {
        public Expression<System.Func<MyDocuments.DAL.Entities.Document, bool>> getExpression(string str, out string message )
        {
            #region Init
           

            
            Expression predicateBody;
            QueryElementsService service = new QueryElementsService();
            string[] dataElements = getArrayOfCleanStrings(str);
            ListQueryElementWitMessage elementsWitMessage= service.getElements(dataElements);
            message = elementsWitMessage.message;
            List<QueryElement> qElement = elementsWitMessage.qElements;
            ParameterExpression list = Expression.Parameter(typeof(Document), "res");
            var member = Expression.Property(list, "Name");
            Expression toLowerMethod = Expression.Call(member, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

            #endregion

            predicateBody = getPredicate(toLowerMethod, qElement[0]);

            var orderedElements = qElement.OrderBy(qe => qe.operation);

            foreach (var el in orderedElements)
            {
                Expression newCondition = getPredicate(toLowerMethod, el);
                if (el.operation == Constants.and)
                {
                    predicateBody = Expression.And(predicateBody, newCondition);
                }
                if (el.operation == Constants.or)
                {
                    predicateBody = Expression.Or(predicateBody, newCondition);
                }
            }

            Expression<System.Func<MyDocuments.DAL.Entities.Document, bool>> expr = Expression.Lambda<Func<Document, bool>>(predicateBody, list);

            return expr;
        }
        public string[] getArrayOfCleanStrings(string str)
        {

            var strBuilder = new StringBuilder(str);

            str = str.ToLower();
            foreach (var s in Constants.separators)
            {
                strBuilder.Replace(s, "");
            }
            strBuilder = new StringBuilder(System.Text.RegularExpressions.Regex.Replace(strBuilder.ToString(), @"\s+", " "));

            var strArray = strBuilder.ToString().Trim().Split();
            return strArray;
        }

        public Expression getPredicate(Expression predicate, QueryElement element)
        {
            Expression paramenter = Expression.Constant(element.name);
            var contain = Expression.Call(predicate, typeof(string).GetMethod("Contains", new[] { typeof(string) }), paramenter);
            Expression result = contain;
            if (element.notCondition == true)
            {
                result = Expression.Not(contain);
            }
            return result;
        }
    }
}
