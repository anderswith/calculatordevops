# CalculatorDevOps

This project focuses on implementing DevOps practices around a simple calculator application.

The main goal of the project is to build a complete CI/CD pipeline using GitHub Actions, including automated testing, code analysis, containerization, and deployment to a staging environment in Oracle Cloud.

---

## Purpose

The purpose of this project is to demonstrate how a full DevOps pipeline can be applied to an application, covering the entire lifecycle from code commit to deployment and testing.

---

## Technology

* C# / .NET
* GitHub Actions (CI/CD)
* Docker & Docker Compose
* MariaDB
* Flyway (database migrations)
* SonarQube (static code analysis)
* Stryker (mutation testing)
* k6 (performance testing)
* TestCafe (end-to-end testing)

---

## CI/CD Pipeline

The pipeline is implemented using GitHub Actions and is triggered on push to the main branch.

### Build and Test

* Automatic versioning using semantic versioning
* Build of the application
* Unit tests with code coverage collection
* Generation of coverage reports

### Code Quality

* Static code analysis using SonarQube
* Quality gate enforcement
* Mutation testing using Stryker
* Upload of test and analysis reports as artifacts

---

## Containerization

* Docker is used to build container images for both frontend and backend
* Images are versioned and pushed to GitHub Container Registry (GHCR)

---

## Deployment

* Docker images are deployed to a staging server hosted on Oracle Cloud
* Deployment is performed via SSH using GitHub Actions
* Docker Compose is used to orchestrate services on the staging server
* Services are automatically pulled and restarted

---

## Database Migration

* Database migrations are handled using Flyway
* A MariaDB instance is started in the pipeline for migration testing
* Migrations are executed automatically during the pipeline
* Migration files are also deployed to the staging environment

---

## Testing

The pipeline includes multiple levels of testing:

* Unit tests (NUnit)
* Mutation testing (Stryker)
* Performance testing (k6 spike tests)
* End-to-end testing (TestCafe)

---

## Secrets and Configuration

* GitHub Secrets and variables are used to manage credentials
* Includes configuration for:

  * SonarQube
  * Staging server access
  * Container registry authentication

---

## Workflow Overview

The pipeline performs the following steps:

1. Run unit tests and generate coverage reports
2. Perform mutation testing and static code analysis
3. Build the application
4. Build and push Docker images
5. Deploy application to staging server
6. Run performance and end-to-end tests
7. Apply database migrations

---

## Purpose of the Project

The project demonstrates how to:

* Automate build, test, and deployment processes
* Integrate multiple testing strategies into a pipeline
* Use containerization for consistent environments
* Deploy applications to a cloud-based staging environment
* Manage database migrations as part of CI/CD
