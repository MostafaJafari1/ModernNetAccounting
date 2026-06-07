# ADR 002: Using Enumeration Instead Of Enum

- **Status:** Accepted
- **Date:** 2026-06-06

## Context
Using enums for control flow or more robust abstractions can be a code smell. This type of usage leads to fragile code with many control flow statements checking values of the enum.

## Decision
You can create Enumeration classes that enable all the rich features of an bject-oriented language.
Simple enums that are purely technical (not domain concepts) and require no behavior may still use C# built-in enum.

## Consequences

Positive:
- Domain enumerations become first-class domain objects
- Business rules live inside the enumeration, not scattered across services
- Lookup from database (int) and API (string) is straightforward
- Equality works correctly with == operator
- All valid values are discoverable via GetValues()

Negative:
- More boilerplate than a plain C# enum
- Requires developers to understand the pattern before contributing
- Slightly more complex serialization setup needed in the API layer

