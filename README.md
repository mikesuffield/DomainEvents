# Domain Events
![Build](https://github.com/mikesuffield/DomainEvents/workflows/Build/badge.svg?branch=master&event=push)
[![GitHub license](https://img.shields.io/github/license/mikesuffield/DomainEvents?color=blue&label=License)](https://github.com/mikesuffield/DomainEvents/blob/master/LICENSE)

Exploring domain events and commands in .NET Core 3.1 using [MediatR](https://github.com/jbogard/MediatR/wiki).

## Contents
* [Events](#events)
* [Commands](#commands)
* [Using Events & Commands](#using-events-&-commands)
    * [Flow](#flow)
    * [Events vs Commands](#events-vs-commands)
* [Why use Events & Commands?](#why-use-events-&-commands?)
* [MediatR](#mediatr)
    * [Events (aka Notifications)](#events-aka-notifications)
    * [Commands (aka Requests)](#commands-aka-requests)
* [Example](#example)
    * [Flow](#flow-1)
    * [Request](#request)
* [Further Reading](#further-reading)

## Events

A Domain Event is something that has happened in one part of your system that you want other areas of your system to know about - and (probably) react to.

Domain events:
* are *published* in one part of your system and *handled* (processed) by the other area(s) of the system
* can be stored in a persistent log to provide a full audit trail

For example, if a person buys an item from a store, an `ItemPurchasedEvent` may be raised. Note the past tense verbiage, as the event represents something that has happened in the past.

Events are processed by event handlers, and multiple event handlers can listen for the same event, enabling the addition of new features by simply adding further handlers (each with a different purpose). This means that when an event is published it could be processed 0 or n times - we don't care who receives it, or even if it has been received at all - this is a "fire and forget" approach.

As well as source data, domain events can also include supplementary processing data which records what the system has actually done with the event. For example, source data may be the quantity of an item that has been purchased. Processing data may be the warehouse that has the stock of that item that will handle the distribution of the order.

## Commands

If an event is a historical record of something happening, a command can be thought of as the initial request for that something to happen. A command triggers a change in state.

This is only a request, and may be refused, usually by throwing an exception. For example, a person may send a `MakePurchaseCommand` when they want to buy an item - we are able to reject this request if the person doesn't have sufficient funds, or if the store doesn't have stock of the item.

Commands are processed by command handlers - each command should have exactly 1 command handler, meaning that commands are only processed once.

In some implementations, commands can also return data back to whoever has sent the command.

## Using Events & Commands

### Flow

A typical example of using commands and events is as follows:

![Sequence Diagram](https://github.com/mikesuffield/DomainEvents/raw/master/readme/ExampleSequenceDiagram.png)

### Events vs Commands

Although event and commands may seem similar, their intent are different.

|                 | Command                                 | Event                         |
|-----------------|-----------------------------------------|-------------------------------|
| Intent          | Initial request for something to happen | Record something has happened |
| Naming          | Imperative                              | Past participle               |
| Handlers        | Exactly 1 handler                       | 0 or many handlers            |
| Immutable?      | Yes                                     | Yes                           |
| Can return data | Yes                                     | No                            |
| Can be rejected | Yes                                     | No                            |


## Why use Events & Commands?

Separating the source of a command from any subsequent processing allows various different input types (user interfaces, message buses, external API calls and so on) to be converted into commands - the input layer in the system doesn't take any actions apart from sending a command. These commands can then be processed by an command handler which triggers what the application is supposed to do.

The application layer handles the commands and raises domain events as necessary, taking care of the subsequent processing and complex domain logic - but it doesn't care about the input source (i.e. it is input agnostic). Decoupling the domain logic in this way reduces the changes of running into issues (e.g. open/closed or single responsibility principles) as the application grows. 

Furthermore, having a clear fine-grained event stream makes it easier to amend or divert events to different systems in the future, and we can add further event handlers to implement new functionality as new requirements arise.

Finally, as previously mentioned, domain events can be stored to provide a full audit log of the system, which can provide invaluable. 

## MediatR

### Events (aka Notifications)

In MediatR, an event is referred to as a "Notification", and we make use of the `INotification` interface. As events are immutable, all source data should be set via the constructor and only accessible via getters, as shown below.

```cs
public class ItemPurchasedEvent : INotification
{
    public ItemPurchasedEvent(int itemId, int quantity, DateTime timeStamp)
    {
        ItemId = itemId;
        Quantity = quantity;
        TimeStamp = timeStamp;
    }

    public int ItemId { get; }

    public int Quantity { get; }

    public DateTime TimeStamp { get; }
}
```
Event handlers implement the `INotificationHandler<T>` interface which provides the `Handle` method, as shown below. 

```cs
public class ItemPurchasedEventHandler : INotificationHandler<ItemPurchasedEvent>
{
    public Task Handle(ItemPurchasedEvent notification, CancellationToken cancellationToken)
    {
        // Handle event here
    }
}
```

Events are raised using the `mediator.Publish(event)` method.

### Commands (aka Requests)

As well as Events (Notifications), MediatR also supports Commands, known as "Requests". Commands implement the `IRequest<T>` interface, as shown below. The type specifies the data that will be returned to the person that has make the request (this is optional).

```cs
public class MakePurchaseCommand : IRequest<string>
{
}
```

Command handlers implement the `IRequestHandler<TRequest, TResponse>` interface which provides the `Handle` method, as shown below.

```cs
public class MakePurchaseCommandHandler : IRequestHandler<MakePurchaseCommand, string>
{
    public Task<string> Handle(MakePurchaseCommand purchase, CancellationToken cancellationToken)
    {
        // Handle command here
    }
}
```

Commands are sent using the `mediator.Send(command)` method.

## Example 

### Flow

![Sequence Diagram](https://github.com/mikesuffield/DomainEvents/raw/master/readme/SequenceDiagram.png)

### Request

`GET` `/purchase/?itemId=123&itemCost=10&quantity=2&storeId=22`
```
MakePurchaseCommandHandler - called...
StoreTransactionEventHandler - called...
StoreTransactionEventHandler - store 22 made a transaction for £20 @ 31/07/2020 13:18:05...
ItemPurchasedEventHandler - called...
ItemPurchasedEventHandler - user purchased 2x 123 item @ 31/07/2020 13:18:05...
WarehouseStockProcessor - called...
WarehouseStockProcessor - ItemPurchasedEvent additional processing...
```

## Further Reading

- [Domain Event](https://martinfowler.com/eaaDev/DomainEvent.html) - Martin Fowler
- [How to easily extend your app using MediatR notifications](https://jonhilton.net/2016/08/31/how-to-easily-extend-your-app-using-mediatr-notifications/) - Jon Hilton
- [Command vs. Event in Domain Driven Design](https://medium.com/ingeniouslysimple/command-vs-event-in-domain-driven-design-be6c45be52a9) - Chen Chen
- [Domain-Driven Design](https://cqrs.nu/Faq) - Edument
- [Domain events: design and implementation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation) - Microsoft Docs

