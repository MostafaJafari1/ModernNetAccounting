# ADR 001: Using Records for Value Objects

- **Status:** Accepted
- **Date:** 2026-06-07

## Context
In our domain layer, we frequently define Value Objects to represent conceptual types (e.g., `Money`, `Address`, `Email`). Previously, we used standard classes. This required extensive boilerplate code to implement value-based equality, `GetHashCode`, and immutability (manually setting properties as `init`-only or using private setters). This approach is error-prone and increases maintenance overhead.

## Decision
We will use C# `record` types for all Value Objects. 

## Consequences
### Pros
- **Reduced Boilerplate:** Automatically implements value-based equality, `GetHashCode`, and a descriptive `ToString()`.
- **Immutability:** Encourages the use of immutable data structures, reducing side effects in the domain model.
- **Readability:** Concise syntax makes the intent of the code clear.

### Cons
- **Minimal:** Minor learning curve for team members unfamiliar with modern C# features. Requires .NET 5+ or C# 9.0+.

---

## Implementation Example

```csharp
// Using record for Value Object
// Value Equality is handled automatically by the compiler
public record Money(decimal Amount, string Currency);

// Usage:
var price1 = new Money(100, "USD");
var price2 = new Money(100, "USD");

// Returns true due to value-based equality
bool isEqual = price1 == price2; 
```
