# ADR 003: Selecting Wolverine as the Foundation for Mediator and Messaging Patterns

- **Status:** Accepted
- **Date:** 2026-06-08

## Context

I am architecting a new .NET service from the ground up. To ensure the system is production-ready and built to scale, I require a high-performance framework that handles mediator patterns while natively supporting distributed systems, event-driven architectures, and reliable messaging (Outbox pattern). While MediatR has historically been the industry standard, its shift toward a commercial licensing model and its purely in-process limitations do not align with my long-term technical requirements for this project.

## Decision

I have decided to adopt Wolverine as the primary framework for both in-process dispatching and distributed messaging. This will serve as the core architectural foundation for this new project.

## Rationale

### 1. Superior Performance via Source Generation

MediatR relies heavily on **Reflection** for handler discovery and dispatching, which introduces runtime overhead and increased latency. Wolverine utilizes **Source Generators** to compile the dispatch pipeline at compile-time. This results in significantly lower latency, performing close to direct method calls, which is critical for our high-throughput requirements.

### 2. Unified In-Process and Distributed Messaging

Moving to Wolverine allows us to use a single, consistent abstraction for both in-process dispatch and distributed messaging (via RabbitMQ/Azure Service Bus). Unlike MediatR, which requires bolting on separate libraries to handle distributed messaging, Wolverine provides this out-of-the-box, simplifying our dependency graph and operational complexity.

### 3. Built-in Reliability Patterns

Wolverine includes first-class support for enterprise messaging patterns, specifically:

* **Outbox Pattern:** Ensures atomic persistence of messages without manual implementation.
* **Saga Orchestration:** Simplifies stateful, long-running processes.
* **Scheduled Jobs:** Handles background task execution without external dependencies.

### 4. Licensing and Future-Proofing

Given MediatR’s shift toward a commercial licensing model in upcoming versions, choosing Wolverine—which remains under the **MIT license**—mitigates long-term legal and financial risk for the enterprise.

## Consequences

### Positive

* **Efficiency:** Reduced CPU and memory overhead by eliminating runtime reflection.
* **Consistency:** A unified model for local and distributed communication improves developer velocity and reduces cognitive load.
* **Observability:** Built-in OpenTelemetry integration ensures we have standardized observability from day one.

### Negative

* **Learning Curve**
* **Paradigm Shift:** The team must adapt to a "convention over configuration" mindset, which is a departure from the explicit interface-driven approach of MediatR.
