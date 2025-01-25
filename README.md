# MassTransit RabbitMQ POC - Clean Code & Best Practices

This repository contains a **Proof of Concept (POC)** demonstrating the integration of **MassTransit** with **RabbitMQ**, following **clean code** and **best practices** principles. The project focuses on the dynamic creation of queues with custom names based on the environment (e.g. `dev.order.v1`, `prod.order.v1`), ensuring scalability and ease of maintenance.

## 🚀 Features

- 🛠 **Clean Architecture:** Organized structure with clear separation of responsibilities.
- 🐇 **MassTransit with RabbitMQ:** Efficient message-based communication for distributed systems.
- 🏷 **Dynamic queue names:**
- Automatic prefixing based on the environment (`dev`, `prod`, `staging`, etc.).
- Example: `dev.order.v1`, `prod.order.v1`.
- 🏗 **Scalability and maintainability:** Modular and extensible architecture.
- 🛡 **File-based configuration:** Use of `appsettings.json` for flexible adjustments.
- 📦 **Dependency Injection:** Efficient DI implementation for MassTransit consumers and services.
- 📊 **Logging and Monitoring:** Use of Serilog for event tracking.

## 🛠 Technologies Used

* .NET Core 8.0 – Modern framework for cloud-native applications.
* MassTransit – Library for implementing message bus.
* RabbitMQ – Robust message broker for asynchronous communication.
* Docker – Facilitates the deployment and execution of RabbitMQ.
* Serilog – Structured logging for monitoring.

  ![image](https://github.com/user-attachments/assets/02907df2-15fe-407a-9675-850cdb5a9c7d)

## ⭐ Give a Star
