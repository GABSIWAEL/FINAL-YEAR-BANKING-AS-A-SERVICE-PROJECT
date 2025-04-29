
# 💳 Banking Solution

## 🧭 Overview
**Banking Solution** is a microservices-based financial system engineered to manage and streamline core banking operations. The platform is modular, scalable, and designed to be deployed in cloud-native environments using **Docker** and **Kubernetes**.

### ✅ Core Microservices:
- `OpenBanking_ACCOUNT_V1` – Customer account management (balances, transactions)
- `OpenBanking_ATM_V1` – ATM operations (withdrawals, deposits, geo-location)
- `OpenBanking_BRANCH_V1` – Branch information and metadata
- `OpenBanking_CARD_V1` – Card issuance and status tracking

---

## ⚙️ Project Setup

### 🔧 Prerequisites
- Docker
- Kubernetes
- kubectl

### 📥 Clone the Repository
```bash
git clone https://github.com/GABSIWAEL/Banking_Solution.git
cd Banking_Solution
```

### 🐳 Run with Docker Compose
```bash
docker-compose up -d --build
```

### 🚀 Deploy to Kubernetes
```bash
kubectl apply -f deployments.yaml
```

---

## 🗄 Microservice-to-Database Mapping

| Microservice | Description                          | Recommended Database | Reason |
|--------------|--------------------------------------|----------------------|--------|
| `ATM`        | ATM info, geo-location, status logs  | MongoDB              | Schema flexibility for logs/geo data |
| `CARD`       | Card creation, linkage, status       | PostgreSQL           | Secure storage, strong relational support |
| `BRANCH`     | Branch metadata, employee info       | MySQL / MariaDB      | Optimized for structured lightweight data |
| `ACCOUNT`    | Accounts, balances, transactions     | PostgreSQL           | ACID compliance, consistent indexing |

> ❌ **Why Not SQLite?**
> - Not concurrency-safe
> - No user roles or network access
> - Poor scalability and no failover support
> Use **only for local testing**, never in production.

---

## 🚨 Exception Definitions

### ACCOUNT Exceptions
```csharp
public static ObpException UserNotLoggedIn() => new ObpException("OBP-20001", "User not logged in. Authentication is required!");
public static ObpException UserNotFound() => new ObpException("OBP-20057", "User not found by userId.");
public static ObpException AccountNotFound() => new ObpException("OBP-30018", "Bank Account not found.");
public static ObpException CustomerNotFound() => new ObpException("OBP-30002", "Customer not found.");
```

### ATM Exceptions
```csharp
public static ObpExceptionATM ATMNotFound() => new ObpExceptionATM("OBP-30009", "ATM not found.");
```

### BRANCH Exceptions
```csharp
public static ObpExceptionBRANCH BranchNotFound() => new ObpExceptionBRANCH("OBP-300010", "Branch not found.");
```

### CARD Exceptions
```csharp
public static ObpExceptionCARD CardStatusNotReturned() => new ObpExceptionCARD("OBP-50212", "Connector did not return card statuses.");
```

---
# Docker Compose Services and Exposed Ports

This project uses Docker Compose to manage multiple services. Below are the details of the services and the ports they expose.

## Services and Exposed Ports:

### 1. **Account Service**
- **Container Name:** `account-service`
- **Ports Exposed:**
  - `8088:8088` – Exposes the Account Service API on port 8088 (host:container)
  - `8089:8089` – Exposes an additional port for the Account Service (host:container)
- **Database:** PostgreSQL (`account-db`), accessible on `5432:5432`.

### 2. **ATM Service**
- **Container Name:** `atm-service`
- **Ports Exposed:**
  - `8082:8082` – Exposes the ATM Service API on port 8082 (host:container)
  - `8083:8083` – Exposes an additional port for the ATM Service (host:container)
- **Database:** MongoDB (`atm-db`), accessible on `27017:27017`.

### 3. **Branch Service**
- **Container Name:** `branch-service`
- **Ports Exposed:**
  - `8084:8084` – Exposes the Branch Service API on port 8084 (host:container)
  - `8085:8085` – Exposes an additional port for the Branch Service (host:container)
- **Database:** MySQL (`branch-db`), accessible on `3306:3306`.

### 4. **Card Service**
- **Container Name:** `card-service`
- **Ports Exposed:**
  - `8086:8086` – Exposes the Card Service API on port 8086 (host:container)
  - `8087:8087` – Exposes an additional port for the Card Service (host:container)
- **Database:** PostgreSQL (`card-db`), accessible on `5433:5433`.

## Database Services:

### 1. **Account DB (PostgreSQL)**
- **Container Name:** `account-db`
- **Ports Exposed:**
  - `5432:5432` – PostgreSQL exposed on port 5432.

### 2. **ATM DB (MongoDB)**
- **Container Name:** `atm-db`
- **Ports Exposed:**
  - `27017:27017` – MongoDB exposed on port 27017.

### 3. **Branch DB (MySQL)**
- **Container Name:** `branch-db`
- **Ports Exposed:**
  - `3306:3306` – MySQL exposed on port 3306.

### 4. **Card DB (PostgreSQL)**
- **Container Name:** `card-db`
- **Ports Exposed:**
  - `5433:5433` – PostgreSQL exposed on port 5433.



## 📡 API Access

Each service runs on an independent endpoint. Make sure the relevant pod/container is active before sending requests.

---

## 🤝 Contribution

Want to help?
- Open a pull request for fixes or features
- Submit issues or ideas
- Enhance documentation

---

## 📝 License

This project is licensed under the MIT License.
