# BoilerCore
A unity project to showcase my skill. Public Message Show. Oh nice looking msg.

Event-Bus System (C# / Unity) ->

Designed and implemented a high-performance, modular event bus for Unity projects, combining Observer + Broadcast patterns to enable loose coupling between classes.

Highlights:

    ✅ Weak References – automatic cleanup, prevents memory leaks

    ✅ Priority Levels – critical listeners (UI) execute before background tasks (analytics)

    ✅ Once Listeners – auto-remove after first invocation, ideal for initialization events

    ✅ Thread-Safe – built with ConcurrentDictionary and locks for multi-threaded environments

    ✅ Error Isolation – one failing listener doesn’t break others

    ✅ Disposable Pattern – scoped subscriptions with using statements

    ✅ Async Support – background event dispatch with SendAsync

    ✅ Zero-Alloc Send – optimized for mobile, no boxing overhead

Impact:

    Reduced coupling across game systems, improving maintainability and scalability

    Enabled GC-zero workflows for mobile games, ensuring smooth performance

    Provided a reusable, production-ready messaging framework for Unity projects

Demo Use Cases:

    UI panels subscribing/unsubscribing dynamically

    Animation controllers triggered via one-time events

    Async document loading with safe cleanup