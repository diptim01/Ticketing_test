// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Ticketing_Assessment;

var events = new List<Event>
{
    new Event {Name = "Phantom of the Opera", City = "New York", EventDate = new DateTime(2022, 1, 3)},
    new Event {Name = "Metallica", City = "Los Angeles", EventDate = new DateTime(2024, 5, 13)},
    new Event {Name = "Metallica", City = "New York", EventDate = new DateTime(2023, 12, 23)},
    new Event {Name = "Metallica", City = "Boston", EventDate = new DateTime(2023, 5, 4)},
    new Event {Name = "LadyGaGa", City = "New York", EventDate = new DateTime(2023, 2, 22)},
    new Event {Name = "LadyGaGa", City = "Boston", EventDate = new DateTime(2023, 7, 24)},
    new Event {Name = "LadyGaGa", City = "Chicago", EventDate = new DateTime(2023, 8, 12)},
    new Event {Name = "LadyGaGa", City = "San Francisco", EventDate = new DateTime(2022, 9, 2)},
    new Event {Name = "LadyGaGa", City = "Washington", EventDate = new DateTime(2023, 1, 18)}
};


var customer = new Customer {Name = "Mr. Fake", City = "New York", DateOfBirth = new DateTime(2022,10, 15)};


//***** Question 1 ******

var closestEvents = GetEventsClosestToCustomer(customer);
foreach (var @event in closestEvents)
{
    GivenMethods.AddToEmail(customer, @event);
}

IEnumerable<Event> GetEventsClosestToCustomer(Customer customer)
{
    return events.Where(e => e.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase));
}


//***** Question 2 ******
Console.WriteLine("***** Question 2 ******");
var fiveclosestEvents = GetClosestEventDistance(customer);

foreach (var @event in fiveclosestEvents)
{
    Console.WriteLine("{0} {1}", @event.CityToCity, @event.Distance);

    //if I want to send an email
    //GivenMethods.AddToEmail(customer, @event.Event);
}

IEnumerable<EvaluatedEvent> GetClosestEventDistance(Customer customer)
{
    var evaluatedEvent = new List<EvaluatedEvent>();

    foreach (var @event in events)
    {
        evaluatedEvent.Add(new EvaluatedEvent
        {
            Distance = GivenMethods.GetDistance(customer.City, @event.City),
            CityToCity = $"{customer.City}-{@event.City}",
            Event = @event
        });
    }

    var fiveClosestevaluatedEvents = evaluatedEvent.OrderBy(c => c.Distance).Take(5);

    return fiveClosestevaluatedEvents;
}

//***** Question 3 ******
Console.WriteLine("***** Question 3 ******");
var fiveCachedclosestEvents = GetClosestEventDistance(customer);

foreach (var @event in fiveCachedclosestEvents)
{
    Console.WriteLine("{0} {1}", @event.CityToCity, @event.Distance);
    //if I want to send an email
    //GivenMethods.AddToEmail(customer, @event.Event);
}

IEnumerable<EvaluatedEvent> GetCachedClosestEventDistance(Customer customer)
{
    var evaluatedEvent = new List<EvaluatedEvent>();

    foreach (var @event in events)
    {
        try
        {
            evaluatedEvent.Add(new EvaluatedEvent
            {
                Distance = new CacheSystem<string>().GetDistance($"{customer.City}-{@event.City}",
                    () => GivenMethods.GetDistance(customer.City, @event.City)),
                CityToCity = $"{customer.City}-{@event.City}",
                Event = @event
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    var fiveClosestevaluatedEvents = evaluatedEvent.OrderBy(c => c.Distance).Take(5);

    return fiveClosestevaluatedEvents;
}


//***** Question 5 ******
Console.WriteLine("***** Question 5 ******");
var pricedEvents = GetEventBasedOnPrice(customer);

foreach (var @event in pricedEvents.SortEvent("Price", false))
{
    GivenMethods.AddToEmail(customer, @event.Event, Convert.ToInt32(@event.Price));
}

IEnumerable<EvaluatedEvent> GetEventBasedOnPrice(Customer customer)
{
    var evaluatedEvents = new List<EvaluatedEvent>();

    foreach (var @event in events)
    {
        try
        {
            var evaluatedEvent = new EvaluatedEvent
            {
                Distance = new CacheSystem<string>().GetDistance($"{customer.City}-{@event.City}",
                    () => GivenMethods.GetDistance(customer.City, @event.City)),
                CityToCity = $"{customer.City}-{@event.City}",
                Event = @event
            };

            evaluatedEvent.Price = .2M * evaluatedEvent.Distance;

            evaluatedEvents.Add(evaluatedEvent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    return evaluatedEvents;
}


//***** Question 6 ******
Console.WriteLine("***** Question 6 ******");

var closestBirthdayWithEvents = GetClosestBirthdayWithEvents(customer);

foreach (var @event in closestBirthdayWithEvents)
{
    Console.Write("{0} - ", @event.EventDate);
    GivenMethods.AddToEmail(customer, @event);
}

IEnumerable<Event> GetClosestBirthdayWithEvents(Customer customer)
{
    var closeBirthdays = events.Where(e => e.EventDate >= customer.DateOfBirth).OrderBy(e => e.EventDate);
    return closeBirthdays;
}