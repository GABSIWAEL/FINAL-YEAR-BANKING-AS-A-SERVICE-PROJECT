# Banking Solution - README v1

## Overview
Banking Solution is a microservices-based financial system designed to handle various banking operations. The system consists of multiple services, including:

- **OpenBanking_ACCOUNT_V1** - Manages customer accounts.
- **OpenBanking_ATM_V1** - Handles ATM transactions.
- **OpenBanking_BRANCH_V1** - Manages branch-related operations.
- **OpenBanking_CARD_V1** - Handles credit/debit card services.

Additionally, the system includes other microservices that support various banking functionalities, and we will be adding more services to enhance and expand the platform further, ensuring a comprehensive financial solution.

## Project Structure
The system follows a microservices architecture using Docker and Kubernetes for deployment and scalability.

## Setup Instructions
### Prerequisites
Ensure you have the following installed:
- [Docker](https://www.docker.com/get-started)
- [Kubernetes](https://kubernetes.io/docs/tasks/tools/)
- [Kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)

### Clone the Repository
```bash
git clone https://github.com/GABSIWAEL/Banking_Solution.git
cd Banking_Solution
```

### Build and Run with Docker
```bash
docker-compose up -d --build
```

### Deploy to Kubernetes
```bash
kubectl apply -f deployments.yaml
```

## Usage
Each microservice runs independently and can be accessed via REST APIs. Ensure services are running and accessible before making API calls.

## Contribution
Feel free to contribute by opening pull requests and reporting issues.

## License
This project is licensed under the MIT License.

