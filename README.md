# ModernNetAccounting

## 📖 Project Story
Almost 10 years ago, I built a small web-based accounting application called 'Net Accounting' for my father to use in his work. The application is still actively used today. The project was built with ASP.NET Framework on the backend and AngularJS on the frontend. Working on this project also gave me my first practical experience with the accounting domain and helped me understand its core concepts and workflows. 

Over the years, I have developed and worked on Event-Driven Microservices and Domain-Driven Design (DDD) projects in the companies I have worked for. I have always wanted to find the time to evolve my monolithic accounting application into a modern, scalable system and apply the architectures, patterns and knowledge I have gained throughout my career. That is why I started this project called 'Modern Net Accounting'.

## 🏗️ Architecture Overview
This project demonstrates a distributed accounting system designed for scalability, maintainability, and reliable event-driven communication.

* **Microservices:** Accounting, Inventory, and Reporting.
* **Architecture & Patterns:** DDD (Domain-Driven Design), Clean Architecture, Event Sourcing, Transactional Outbox, and reusable Building Blocks.
* **Persistence:** Marten and MongoDB.
* **Messaging:** Wolverine with Apache Kafka for event-driven communication.
* **Testing:** Integration tests powered by Testcontainers.
* **Technology:** .NET 10 / C#.


## 🚀 Key Technical Features
* **Modular Design:** Self-contained microservices (Monorepo).
* **Reliability:** Transactional Outbox pattern to ensure data consistency.
* **Infrastructure as Code:** Designed for cloud-native deployment.
* **Quality Assurance:** CI/CD integration with GitHub Actions & SonarCloud.

