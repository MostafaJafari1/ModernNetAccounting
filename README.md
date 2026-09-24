# ModernNetAccounting

## 📖 Project Story
Almost 10 years ago, I built a small web-based accounting application called 'Net Accounting' for my father to use in his work. The application is still actively used today. The project was built with ASP.NET Framework on the backend and AngularJS on the frontend. Working on this project also gave me my first practical experience with the accounting domain and helped me understand its core concepts and workflows. 

Over the years, I have developed and worked on Event-Driven Microservices and Domain-Driven Design (DDD) projects in the companies I have worked for. I have always wanted to find the time to evolve my monolithic accounting application into a modern, scalable system and apply the architectures, patterns and knowledge I have gained throughout my career. That is why I started this project called 'Modern Net Accounting'.

## 🏗️ Architecture Overview
This project demonstrates a distributed accounting system designed for scalability, maintainability, and reliable event-driven communication.

* **Microservices:** Core Accounting and Reporting.
* **Architecture & Patterns:** DDD (Domain-Driven Design), Clean Architecture, Event Sourcing, Transactional Outbox, and reusable Building Blocks.
* **Persistence:** Marten and MongoDB.
* **Messaging:** Wolverine with Apache Kafka for event-driven communication.
* **Testing:** Integration tests powered by Testcontainers.
* **Technology:** .NET 10 / C#.


## 🚀 Key Technical Features

* **Modular Design:** Self-contained microservices organized in a Monorepo.
* **Domain Architecture:** DDD and Clean Architecture with clear separation of business and infrastructure concerns.
* **Event-Driven:** Asynchronous communication using Apache Kafka and domain events.
* **Reliability:** Transactional Outbox pattern for reliable event publishing and data consistency.
* **Integration Testing:** Real infrastructure integration tests powered by Testcontainers.
* **Architecture Documentation:** Architecture Decision Records (ADRs) for documenting key technical decisions.
* **CI/CD:** Automated workflows and continuous integration using GitHub Actions.
* **Cloud-Native:** Designed with scalable, independently deployable services and cloud-native principles.
