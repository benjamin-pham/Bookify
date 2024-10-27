# Clean Architecture

**Clean Architecture** is a software design approach created by **Robert C. Martin (Uncle Bob)**. It emphasizes separation of concerns and organization of code to make the system flexible, maintainable, and testable. Clean Architecture builds on principles from other architectures such as **Onion Architecture**, **Hexagonal Architecture**, and **Layered Architecture**.

## Key Goals of Clean Architecture
- **Independence** from frameworks and external libraries.
- **Testability**, making it easy to test business rules without reliance on external components.
- **Ease of maintenance** by separating code into organized layers.
- **Flexibility** to change or replace frameworks or external dependencies without affecting the business logic.

## Clean Architecture Layers

The Clean Architecture model is often represented as concentric circles with different layers:

1. **Entities (Domain Model)**: The innermost layer containing the core business logic, entities, and enterprise-wide rules. It holds the critical and immutable part of the business.
2. **Use Cases**: Defines the specific application logic for carrying out specific operations or functionalities. It’s about implementing the logic that executes the application’s workflows.
3. **Interface Adapters (Presenters, Controllers, Gateways)**: Responsible for converting data between the use cases and external systems like databases or user interfaces. This layer helps achieve decoupling between core logic and external implementations.
4. **Infrastructure**: Outermost layer, including frameworks, databases, and external interfaces. These are implementation details that can be swapped without affecting the core layers.

## Principles of Clean Architecture
1. **Dependency Rule**: Code dependencies point inward. Inner layers (core) don’t know about outer layers.
2. **SOLID Principles**: Clean Architecture embraces SOLID principles to maintain modular, understandable, and scalable code.
3. **Separation of Concerns**: Each layer is isolated with specific responsibilities.
4. **Abstractions over Implementations**: Interface-driven design to decouple core logic from implementations, making it easier to swap components.

## Benefits of Clean Architecture
- High **testability** since core logic is separated.
- Easy to **maintain** as changes to frameworks or UI don't affect core logic.
- Facilitates **evolution** over time, accommodating new technologies and changing business requirements.
- Better **separation of concerns** and independence from specific frameworks.

## Clean Architecture in Practice (Example in C#)
For a C# project using Clean Architecture, you can structure the project with these layers:

1. **Domain Layer**: Contains entity classes, enums, and core business logic.
2. **Application Layer**: Contains use case classes, interfaces for the domain repository, and services.
3. **Infrastructure Layer**: Implements repository interfaces, integrates with databases, APIs, and other services.
4. **Presentation Layer**: Contains the UI layer like ASP.NET MVC controllers or API controllers, handling user interaction or HTTP requests.

The main idea is to ensure your **domain and application layers** remain independent of specific technologies, frameworks, or infrastructure choices.
