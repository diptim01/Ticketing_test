using System.Collections.Generic;
using System.Linq;

namespace Ticketing_Assessment
{
    public class EvaluatedEvent
    {
        public decimal Distance { get; set; }
        
        public string CityToCity { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }
    }
    
    public static class Util{
        public static IEnumerable<EvaluatedEvent> SortEvent(this IEnumerable<EvaluatedEvent> events, string sortValue, bool isAsc)
        {
            if (isAsc)
                return events.OrderBy(e => e.GetType().GetProperty(sortValue)?.GetValue(e));
            
            return events.OrderByDescending(e => e.GetType().GetProperty(sortValue)?.GetValue(e)); 
        }
    }
}