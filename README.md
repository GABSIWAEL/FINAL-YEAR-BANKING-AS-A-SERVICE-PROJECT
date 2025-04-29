
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
