# ModernNetAccounting

A modular, event-driven accounting platform built with .NET 9, DDD, and Clean Architecture.

## 🏗️ Architecture Overview
This project demonstrates a distributed accounting system designed for scalability and maintainability.

- **Microservices:** Accounting, Inventory, and Reporting.
- **Patterns:** DDD (Domain-Driven Design), Clean Architecture, Transactional Outbox.
- **Communication:** Event-Driven Architecture using Apache Kafka.
- **Testing:** Integration tests powered by Testcontainers.

## 🚀 Key Technical Features
* **Modular Design:** Self-contained microservices (Monorepo).
* **Reliability:** Transactional Outbox pattern to ensure data consistency.
* **Infrastructure as Code:** Designed for cloud-native deployment.
* **Quality Assurance:** CI/CD integration with GitHub Actions & SonarCloud.

## 📂 Project Structure
```text
ModernNetAccounting/
├── src/
│   ├── Accounting/         # Accounting core service
│   ├── Inventory/          # Inventory management service
│   └── Shared/             # Common contracts and events
├── tests/                  # Integration tests with Testcontainers
└── docs/                   # Architectural Decision Records (ADR)