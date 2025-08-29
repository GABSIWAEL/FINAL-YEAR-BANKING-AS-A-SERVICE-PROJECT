# 💳 Banking Solution : Bank As A Service (BAAS)

## 🧽 Overview

**Banking Solution : Bank As A Service (BAAS)** is a cloud-native microservices-based financial system that simulates core banking operations such as account handling, ATM interactions, card services, and branch management. The platform is modular, observable, and scalable — deployed via **Docker** and **Kubernetes**.

---
## 🛠 Technologies Used

### **Backend & Microservices**
- **C# / .NET 8+** – ASP.NET Core Web API powering all microservices
- **RabbitMQ** – Event-driven messaging backbone
- **MongoDB**, **PostgreSQL**, **MySQL** – Polyglot persistence layer
- **Elasticsearch** – Centralized search and log indexing
- **Kong API Gateway** – API management, routing, and security
- **Konga** – Visual dashboard for Kong configuration

### **Frontend**
- **Angular 16+** – Modern, responsive web frontend
- **HTML5**, **CSS3**, **JavaScript**, **TypeScript**
- **NGINX** – Optimized delivery of Angular SPA
- **i18n** – Multi-language internationalization support

### **Infrastructure & DevOps**
- **Docker** & **Docker Compose** – Local containerized environment
- **Kubernetes** & **kubectl** – Scalable orchestration
- **Prometheus**, **Grafana** – Monitoring & dashboarding
- **Kibana**, **Elasticsearch** – Log visualization and analytics
- **Shell scripting** & **PowerShell** – Automation and DevOps tasks

### **Monitoring, Logging & Observability**
- **Prometheus** – Time-series metrics collection
- **Grafana** – Customizable dashboards & alerts
- **Elasticsearch** – Full-text log indexing
- **Kibana** – Real-time log exploration
- **Custom App Logging** – Backend request & event logging

### **API Development & Consumption**
- **REST API** – OpenBanking-style contract
- **Swagger / Postman** – Documentation & manual testing
- **API Key Authentication** – Secured access via Kong
- **Flask (Python)** – Fintech client consumption example
- **Requests (Python)** – Lightweight HTTP calls

### **Security**
- **Token-based authentication**
- **API Key-based access control**
- **Google OAuth** & **Facebook OAuth** – Social authentication


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

| Component              | Internal Port(s) | Exposed Port(s) |
| ---------------------- | ---------------- | --------------- |
| Account DB (PostgreSQL 15) | 5432             | 5432            |
| Account Dashboard      | 18888, 18889     | 18888, 18889    |
| Elasticsearch          | 9200             | 9200            |
| Kibana                 | 5601             | 5601            |
| Account Service        | 8088, 8089       | 8088, 8089      |
| Branch DB (MySQL 5.7)  | 3306             | 3306            |
| Branch Service         | 8084, 8085       | 8084, 8085      |
| Card DB (PostgreSQL 13)| 5432             | 5433            |
| Card Service           | 8086, 8087       | 8086, 8087      |
| ATM Service            | 8082, 8083       | 8082, 8083      |
| ATM DB (MongoDB)       | 27017            | 27017           |
| Auth DB (MySQL 5.7)    | 3306             | 3307            |
| Authenticator Service  | 8090             | 8090            |
| Prometheus             | 9090             | 9090            |
| Grafana                | 3000             | 3000            |
| Redis                  | 6379             | 6379            |
| Frontend (Angular + NGINX)| 80           | 8083            |
| Notification Service   | 9094, 9095       | 9094, 9095      |
| RabbitMQ Messaging     | 5672             | 5672            |
| RabbitMQ Dashboard     | 15672            | 15672           |
| Kong DB (PostgreSQL 11)| 5432             | 5440            |
| Kong Gateway           | 8000, 8443, 8001, 8444 | 8000, 8443, 8001, 8444 |
| Konga Dashboard        | 1337             | 1337            |
| Test Client (curl)     | —                | —               |

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
```

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

---

# 🔐 Kong API Gateway Setup

### 1. Create Account Service in Kong
```bash
curl -i -X POST http://localhost:8001/services   --data name=account-service   --data url=http://account-service:8088
```

### 2. Create Route
```bash
curl -i -X POST http://localhost:8001/services/account-service/routes   --data 'paths[]=/account'
```

### 3. Enable Key Authentication
```bash
curl -i -X POST http://localhost:8001/services/account-service/plugins   --data name=key-auth
```

### 4. Create Consumer & API Key
```bash
curl -i -X POST http://localhost:8001/consumers   --data username=fintech-client

curl -i -X POST http://localhost:8001/consumers/fintech-client/key-auth   --data key=WvhG/yvZGeYKyOe7NsBprw8PKmtw8GimmF3lZiRTJcw=
```



📦 Services & Kong Routes
Service Name	Upstream URL	Route Path	Database / Messaging	Internal Port(s)	Exposed Port(s)
Account Service	http://account-service:8088	/account	PostgreSQL 15	8088, 8089	8088, 8089
ATM Service	http://atm-service:8082	/atm	MongoDB	8082, 8083	8082, 8083
Branch Service	http://branch-service:8084	/branch	MySQL 5.7	8084, 8085	8084, 8085
Card Service	http://card-service:8086	/card	PostgreSQL 13	8086, 8087	8086, 8087
Authenticator Service	http://authenticator-service:8090	/auth	MySQL 5.7	8090	8090
Notification Service	http://notification-service:8095	/notifications	RabbitMQ	9094, 9095	9094, 9095
🔧 Automated Kong Registration Script
#!/bin/sh
set -e

# Kong Admin API URL
KONG_ADMIN_URL="http://kong:8001"

echo "🔍 Checking Kong Admin API..."
curl -s $KONG_ADMIN_URL/status || { echo "❌ Cannot reach Kong"; exit 1; }
echo "✅ Kong reachable."

SERVICES="
account-service|http://account-service:8088|/account
atm-service|http://atm-service:8082|/atm
branch-service|http://branch-service:8084|/branch
card-service|http://card-service:8086|/card
authenticator-service|http://authenticator-service:8090|/auth
notification-service|http://notification-service:8095|/notifications
"

echo "$SERVICES" | while IFS='|' read SERVICE_NAME UPSTREAM_URL ROUTE_PATH; do
    echo "📦 Creating service: $SERVICE_NAME"
    curl -s -X POST $KONG_ADMIN_URL/services \
        --data name="$SERVICE_NAME" \
        --data url="$UPSTREAM_URL"

    echo "🛣 Creating route: $ROUTE_PATH"
    curl -s -X POST $KONG_ADMIN_URL/services/"$SERVICE_NAME"/routes \
        --data name="${SERVICE_NAME}-route" \
        --data paths[]="$ROUTE_PATH"

    echo "✅ $SERVICE_NAME registered with route $ROUTE_PATH"
done

echo "🚀 All services registered successfully!"

---



# 🧪  Fintech Client (Python + HTML)

## Dedicated for the accounts management 

### 📂 Structure
```
fintech-client/
├── api/
│   └── api_helper.py
├── templates/
│   └── index.html
├── app.py
├── requirements.txt
└── fintech_client.log
```

### ⚙ Config
Edit `app.py`:
```python
API_KEY = "your_api_key_here"
BASE_URL = "http://localhost:8000"
BANK_ID = "bank-008"
```

### ▶ Run
```bash
python -m venv venv
source venv/bin/activate   # Windows: venv\Scripts\activate
pip install -r requirements.txt
python app.py
```
Open browser at `http://127.0.0.1:5000`

---

## 📜 Logging
- Console + `fintech_client.log`
- Logs every request and response:
```
2025-08-12 22:14:40 [INFO] POST http://localhost:8000/account/...
2025-08-12 22:14:40 [INFO] Response 201: {...}
```

---

## 📌 Next Steps
- Deploy fintech client alongside banking stack in `docker-compose`
- Add rate-limiting & monitoring per API key
- Integrate OAuth2 with Kong


