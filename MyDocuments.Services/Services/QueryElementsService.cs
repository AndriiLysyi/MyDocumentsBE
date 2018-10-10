using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDocuments.Services.Models;

namespace MyDocuments.Services.Services
{
    public class QueryElementsService
    {
        public ListQueryElementWitMessage getElements(string[] dataElements)
        {

            List<QueryElement> qElement = getFirstElement(dataElements);

            for (int k = 0; k < dataElements.Count() - 1; k++)
            {
                if ((dataElements[k] == Constants.and || dataElements[k] == Constants.or) && !(dataElements[k + 1] == Constants.and || dataElements[k + 1] == Constants.or))
                {
                    var newElement = new QueryElement();
                    newElement.operation = dataElements[k] == Constants.and ? Constants.and : Constants.or;
                    newElement.notCondition = (dataElements[k + 1] == Constants.not) ? true : false;

                    if (newElement.notCondition == true && k + 2 <= dataElements.Count() - 1)
                    {
                        if (dataElements[k + 2] == Constants.and || dataElements[k + 2] == Constants.or || dataElements[k + 2] == Constants.not)
                        {
                            continue;
                        }
                        else
                        {
                            newElement.name = dataElements[k + 2];
                            qElement.Add(newElement);
                        }

                    }
                    if (!newElement.notCondition && dataElements[k + 1] != Constants.not)
                    {
                        newElement.name = dataElements[k + 1];
                    }
                    else
                    {
                        continue;
                    }
                    qElement.Add(newElement);
                }
            }
            ListQueryElementWitMessage result = new ListQueryElementWitMessage(qElement, null);
            if (qElement.Count() > 0)
            {
                string queryString = validateQuery(dataElements, qElement);
                result.message = queryString;
                
            }
            return result;
        }
        public List<QueryElement> getFirstElement(string[] dataElements)
        {
            var qElement = new List<QueryElement>();
            if(dataElements.Count() == 1 && dataElements[0] == Constants.not)
            {
                qElement.Add(new QueryElement(dataElements[0],  false));
                return qElement;
            }
            qElement.Add(new QueryElement((dataElements[0] == Constants.not ? dataElements[1] : dataElements[0]), dataElements[0] == Constants.not ? true : false));

            for (int k = 0; k < dataElements.Count() - 1; k++)
            {
                if (dataElements[k] == Constants.or || dataElements[k] == Constants.and)
                {
                    break;
                }
                if (dataElements[k] == Constants.not && (dataElements[k + 1] != Constants.or && dataElements[k + 1] != Constants.and))
                {
                    qElement.Clear();
                    qElement.Add(new QueryElement(dataElements[k + 1], true));
                    break;
                }


            }
            return qElement;
        }
        public string  validateQuery(string[] dataElements, List<QueryElement> qElements)
        {
            string message = "";
            if (dataElements.Count()+1 > qElements.Count())
            {
                message = createQueryStringFromElements(qElements);
            }
            return message;
        }

        public string createQueryStringFromElements(List<QueryElement> qElements)
        {
            string message = "";
            foreach (var element in qElements)
            {
                if (!string.IsNullOrEmpty(element.operation))
                {
                    message = message + " {" + element.operation +"}";
                }
                if (element.notCondition)
                {
                    message = message + " {not}";
                }
                message = message + " " + element.name;
            }
            return message;
        }

    }
}
