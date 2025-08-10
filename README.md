# 💳 Banking Solution

## 🧽 Overview

**Banking Solution** is a cloud-native microservices-based financial system that simulates core banking operations such as account handling, ATM interactions, card services, and branch management. The platform is modular, observable, and scalable — deployed via **Docker** and **Kubernetes**.

---

## 🧱 Architecture Highlights

### ✅ Core Microservices:

* `OpenBanking_ACCOUNT_V1` – Customer account management (balances, transactions)
* `OpenBanking_ATM_V1` – ATM operations (withdrawals, deposits, geo-location)
* `OpenBanking_BRANCH_V1` – Branch and employee metadata
* `OpenBanking_CARD_V1` – Card issuance and status tracking
* `OpenBanking_AUTHENTICATOR_V1` – User authentication and identity management
* `OpenBanking_NOTIFICATION_V1` – RabbitMQ-powered event-based notifications
* `OpenBanking_ANGULAR_V1` – Fully responsive frontend

### 📆 Supporting Infrastructure:

* RabbitMQ (messaging)
* PostgreSQL, MySQL, MongoDB (data storage)
* Prometheus & Grafana (monitoring)
* Elasticsearch & Kibana (log aggregation)

---

## ⚙️ Setup Instructions

### 🔧 Prerequisites

* Docker Desktop (with Kubernetes enabled)
* kubectl CLI
* Node.js (for Angular dev)
* Angular CLI (for frontend work)

### 📅 Clone the Repository

```bash
git clone https://github.com/GABSIWAEL/Banking_Solution.git
cd Banking_Solution
```

---

### 🐳 Option A: Run with Docker Compose (for local dev)

```bash
docker-compose up -d --build
```

### ☘️ Option B: Deploy via Kubernetes (recommended)

```bash
kubectl apply -f k8s/
```

📁 Folder structure:

```
k8s/
├── account/
├── atm/
├── branch/
├── card/
├── authenticator/
├── angular/
└── rabbitmq/
```

Each folder contains:

* `deployment.yaml`
* `service.yaml`
* (Optional) `postgres.yaml`, `mysql.yaml`, or `mongo.yaml`

---

## 📄 Microservice-to-Database Mapping

| Microservice    | Description                         | Database   |
| --------------- | ----------------------------------- | ---------- |
| `ATM`           | ATM info, geo-location, status logs | MongoDB    |
| `CARD`          | Card creation, linkage, status      | PostgreSQL |
| `BRANCH`        | Branch metadata, employee info      | MySQL      |
| `ACCOUNT`       | Accounts, balances, transactions    | PostgreSQL |
| `AUTHENTICATOR` | Auth and user sessions              | MySQL      |

> 📝 Each DB is containerized and configured via its own manifest.

---

## 💣 Docker Ports Summary

| Component          | Internal Port | Exposed Port |
| ------------------ | ------------- | ------------ |
| Account Service    | 8088, 8089    | 8088, 8089   |
| ATM Service        | 8082, 8083    | 8082, 8083   |
| Branch Service     | 8084, 8085    | 8084, 8085   |
| Card Service       | 8086, 8087    | 8086, 8087   |
| Authenticator      | 8090, 8091    | 8090, 8091   |
| RabbitMQ Dashboard | 15672         | 15672        |
| MongoDB            | 27017         | 27017        |
| PostgreSQL         | 5432, 5433    | 5432, 5433   |
| MySQL              | 3306, 3307    | 3306, 3307   |
| Angular Frontend   | 80            | 8083         |
| Prometheus         | 9090          | 9090         |
| Grafana            | 3000          | 3000         |
| Kibana             | 5601          | 5601         |
| Elasticsearch      | 9200          | 9200         |

---

## 📂 Observability Stack

* 📊 **Prometheus**: Time-series monitoring
* 📊 **Grafana**: Dashboards and alerts
* 📄 **Elasticsearch**: Central log indexing
* 🔍 **Kibana**: Log visualization
* 📨 **RabbitMQ**: Internal service messaging

---
## 📨 RabbitMQ Integration

The **Banking Solution** uses **RabbitMQ** as its messaging backbone for event-driven communication between microservices.  
It handles asynchronous events such as **account creation**, **ATM creation**, and **attribute updates**, enabling the **Notification Service** to trigger email or system alerts.

### 🔌 Configuration

RabbitMQ is containerized and configured with the following credentials:

| Parameter   | Value   |
| ----------- | ------- |
| HostName    | `rabbitmq` |
| UserName    | `kalo`  |
| Password    | `kalo`  |

---

### 📦 Exchanges & Routing Keys

| Service/Event  | Exchange            | Routing Key              | Queue Name                              |
| -------------- | ------------------- | ------------------------ | --------------------------------------- |
| AccountCreated | `account_exchange`  | `account.created`        | `account_queue_for_notifications`      |
| ATMCreated     | `atm_exchange`      | `atm.created`            | `atm_queue_for_notifications`          |

---

### 🚀 Publishing Events

**Account Service** and **ATM Service** publish messages to RabbitMQ exchanges using `RabbitMqPublisher`.

Example – Publishing an **Account Created** event:

```csharp
public void PublishAccountCreated(AccountCreatedEvent accountEvent)
{
    Publish(accountEvent, "account.created");
}
```

---

### 📥 Consuming Events

**Notification Service** subscribes to the queues and processes events via background services.

Example – Consuming an **Account Created** event:

```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    await ListenAsync(
        "notification_account_queue",
        _config["RabbitMQConnections:AccountEvents:RoutingKey"],
        async (msg) =>
        {
            var evt = JsonConvert.DeserializeObject<AccountCreatedEvent>(msg);
            await _emailSender.SendAccountCreatedEmail(evt.AccountId, evt.Label);
        },
        stoppingToken
    );
}
```

---

### 📧 Email Notifications

When an event is received, **EmailSenderService** logs or sends an email using **MailKit**:

```csharp
public async Task SendAccountCreatedEmail(string accountId, string label)
{
    Console.WriteLine($"New Account Created: {label} (ID: {accountId})");
    await Task.CompletedTask;
}
```

## 📜 Example Logs

```plaintext
=====================================================
=== [LOG] Atm Attribute Created Notification ===
To: System
Subject: New Atm Attribute Created
Body:
✅ A new atm attribute was added:

ID: 7041d4c2-e09a-4847-82c4-e08306f8dd0d
Name: bank-008
Value: string
=====================================================

---

### 🗂 Deployment

RabbitMQ is deployed as a Kubernetes service (`k8s/rabbitmq/`) and exposed for internal microservice communication.

```bash
kubectl apply -f k8s/rabbitmq/
```

RabbitMQ Dashboard is available at:

```
http://localhost:15672
Username: kalo
Password: kalo
```

---


## 🖥️ Frontend (STB BANK)

### 🌐 Supported Languages:

English 🇬🇧 | French 🇫🇷 | Arabic 🇸🇦 | Spanish 🇪🇸 | German 🇩🇪 | Russian 🇷🇺 | Chinese 🇨🇳

### 🛡️ Authentication:

* Email/password login & registration
* Google & Facebook login (social auth)
* Backend token-based session management

### 🎨 UI/UX:

* Built with Angular 16+
* Responsive & mobile-first
* Language switcher with `assets/i18n/`

### 🔧 Development

```bash
cd OpenBanking_ANGULAR_V1
npm install
ng serve
```

### 📦 Build & Deploy

```bash
ng build --configuration=production
```

> Dockerized and served via NGINX SPA image.

---

## 📱 API Usage

Each microservice exposes REST endpoints. Use Postman, Swagger, or your frontend to consume them after deployment. Ensure Kubernetes pods are running:

```bash
kubectl get pods
```

---

## 🥺 Sample Exception Handling (C#)

### ACCOUNT

```csharp
public static ObpException AccountNotFound() =>
  new ObpException("OBP-30018", "Bank Account not found.");
```

### ATM

```csharp
public static ObpExceptionATM ATMNotFound() =>
  new ObpExceptionATM("OBP-30009", "ATM not found.");
```

### CARD

```csharp
public static ObpExceptionCARD CardStatusNotReturned() =>
  new ObpExceptionCARD("OBP-50212", "Connector did not return card statuses.");
```

---

## 🤝 Contribution

All contributions welcome:

* Pull requests
* Bug reports
* Architecture suggestions

---

## 📌 License

MIT License. See [`LICENSE`](./LICENSE) file.
