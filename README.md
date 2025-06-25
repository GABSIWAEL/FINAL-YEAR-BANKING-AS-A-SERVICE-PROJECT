
# 💳 Banking Solution

## 🧭 Overview
**Banking Solution** is a microservices-based financial system engineered to manage and streamline core banking operations. The platform is modular, scalable, and designed to be deployed in cloud-native environments using **Docker** and **Kubernetes**.

### ✅ Core Microservices:
- `OpenBanking_ACCOUNT_V1` – Customer account management (balances, transactions)
- `OpenBanking_ATM_V1` – ATM operations (withdrawals, deposits, geo-location)
- `OpenBanking_BRANCH_V1` – Branch information and metadata
- `OpenBanking_CARD_V1` – Card issuance and status tracking
- `OpenBanking_ANGULAR_V1` – The Frontend microservice

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
> ✅ Use **only for local testing**, never in production.

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

## 🐳 Docker Compose Services and Exposed Ports

### 1. **Account Service**
- Ports: `8088:8088`, `8089:8089`
- DB: PostgreSQL (`5432:5432`)

### 2. **ATM Service**
- Ports: `8082:8082`, `8083:8083`
- DB: MongoDB (`27017:27017`)

### 3. **Branch Service**
- Ports: `8084:8084`, `8085:8085`
- DB: MySQL (`3306:3306`)

### 4. **Card Service**
- Ports: `8086:8086`, `8087:8087`
- DB: PostgreSQL (`5433:5433`)

---

## 🖥️ Frontend Service (STB BANK)

### 🌐 Supported Languages:
- English 🇬🇧
- French 🇫🇷
- Arabic 🇸🇦
- Spanish 🇪🇸
- German 🇩🇪
- Russian 🇷🇺
- Chinese 🇨🇳

### 🚪 Authentication:
- Email-based login and registration
- Password protection with visibility toggle
- Social login via Google and Facebook

### 🎨 UI/UX:
- Built using Angular 16+
- Fully responsive (mobile-first)
- Key sections: Hero, About, Features, Pricing, Language Switch
- Real-time form validation

### 🔐 Security:
- Frontend form validation
- Token-based session management via backend

### 🌍 Internationalization (i18n):
- Dynamic multi-language switch
- `assets/i18n/` folder for translations
- Language dropdown integrated in navbar

### 🧪 Development
```bash
npm install
ng serve
```

### 🧱 Build & Deploy
```bash
ng build --configuration=production
```
> Deployed via Dockerized NGINX with SPA routing support

### 📁 Folder Structure
```
src/
├── app/
│   ├── components/       # All Angular components (login, landing, etc.)
│   └── services/         # Auth and API services
├── assets/i18n/          # Language translation files
├── environments/         # Environment configs
```

---

## 📡 API Access

Each service runs on its own endpoint. Ensure each pod/container is healthy before invoking APIs.

---

## 🤝 Contribution

Contributions are welcome:
- Submit pull requests
- Report issues
- Suggest improvements or new features

---

## 📝 License

This project is licensed under the **MIT License**.
