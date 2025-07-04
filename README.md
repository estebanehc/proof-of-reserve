# Proof of Reserve Service

## Table of Contents

- [Overview](#overview)
- [Motivation](#motivation)
- [Technologies and Tools](#technologies-and-tools)
- [Project Structure](#project-structure)
- [Installation Instructions](#installation-instructions)
    - [Prerequisites](#prerequisites)
    - [Clone and Build](#clone-and-build)
    - [Run Tests](#run-tests)
    - [Run API Locally](#run-api-locally)
- [API Endpoints](#api-endpoints)
    - [Get Merkle Root](#get-merkle-root)
    - [Get Merkle Proof for a User](#get-merkle-proof-for-a-user)
- [Design and Architecture Notes](#design-and-architecture-notes)
- [Suggestions for Improvement](#suggestions-for-improvement)
- [Versioning and Tags](#versioning-and-tags)
- [Contribution Guide](#contribution-guide)

---

## Overview

This repository contains a **Proof of Reserve** solution for cryptocurrency backend services.  
It consists of two main components:

1. **MerkleLibrary**: .NET class library for calculating Merkle Roots and generating Merkle Proofs using BIP340-compatible tagged hashes.
2. **ProofOfReserveAPI**: .NET Web API exposing endpoints to obtain the Merkle Root of user balances and the Merkle Proof for individual users.

---

## Motivation

Proof of Reserve is an essential cryptographic method that allows cryptocurrency exchanges and custodians to prove they hold assets equivalent to user balances, without exposing sensitive information.  
This project implements the concept following industry standards for hashing (BIP340) and data integrity.

---

## Technologies and Tools

- **C# / .NET 9** — For high performance, type safety, and maintainability.
- **xUnit** — Unit and integration tests to validate cryptographic calculations.
- **Swashbuckle / Swagger** — Interactive API documentation.
- **Git** — Version control and semantic versioning using tags.
- **BIP340 Tagged Hashing** — Merkle Tree hashing according to the Bitcoin standard.

---

## Project Structure

```
ProofOfReserveSolution.sln
├── MerkleLibrary/           # Core Merkle Tree and hashing logic
├── MerkleLibrary.Tests/     # Unit tests for hashing and Merkle logic
├── ProofOfReserveAPI/       # Web API with Proof of Reserve endpoints
├── ProofOfReserveTests/     # Unit tests for the Proof of Reserve
└── README.md                # Documentation
```

---

## Installation Instructions

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Clone and Build

```bash
git clone https://github.com/estebanehc/proof-of-reserve.git
cd proof-of-reserve
dotnet build
```

### Run Tests

To run all project tests:

```bash
dotnet test
```

**Expected output:**
```
Total tests: 8
Passed: 8
```

### Run API Locally

Navigate to the API folder and run:

```bash
cd ProofOfReserveAPI
dotnet run
```

The server starts by default at `https://localhost:5001` (or `http://localhost:5000`).  
Access the interactive documentation at: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## API Endpoints

### Get Merkle Root

**GET** `/api/proof/merkle-root`

Returns the Merkle Root hash calculated from all user balances.

**Example response:**
```json
{
    "merkleRoot": "9d7f79fa8e788d4a32c9c674b67dcfaf0885f539ac2699129e3c4d88c11c76e7"
}
```

### Get Merkle Proof for a User

**GET** `/api/proof/{userId}`

Returns the serialized balance and Merkle Proof path for the specified user.

**Example response for `userId=1`:**
```json
{
    "userBalance": "(1,1111)",
    "proofPath": [
        {
            "hash": "04bd4a356d675cc13ea5b0fc83e0736a3fbf3067980de9e8e0553c934f5906b8",
            "direction": 1
        },
        {
            "hash": "d185af244042b0fecba7ee16c9933d73b10c5482104538274dd777b6b120eae1",
            "direction": 1
        },
        {
            "hash": "9d7f79fa8e788d4a32c9c674b67dcfaf0885f539ac2699129e3c4d88c11c76e7",
            "direction": 1
        }
    ]
}
```

---

## Design and Architecture Notes

- The Merkle Tree follows the structure used in Bitcoin transactions, including leaf and branch hashing with BIP340 for enhanced security and domain separation.
- Hashing logic is encapsulated in the `TaggedHasher` class, making maintenance and testing easier.
- The API is implemented with ASP.NET Core (minimal API), using dependency injection and best practices.
- Tests cover full Merkle Proof validation by reconstructing the root from the proofs.

---

## Suggestions for Improvement

If the Proof of Reserve Web API is to be offered as a production-grade service, consider the following enhancements to improve scalability, security, maintainability, and usability:

1. **Extend MerkleLibrary with Merkle Tree Generation and Caching**  
    - Expose or internally cache the full Merkle Tree structure for efficient proof retrieval.
    - Support incremental updates as user balances change.
    - Enable partial tree verification and advanced auditing features.

2. **Persistent and Scalable Data Storage**  
    - Integrate with robust databases (e.g., PostgreSQL, Redis) for user balances and Merkle Tree data.
    - Employ caching layers to reduce recomputation and database load.

3. **Authentication and Rate Limiting**  
    - Add authentication mechanisms (API keys, OAuth tokens) to restrict access.
    - Implement rate limiting to prevent abuse and ensure fair usage.

4. **Monitoring and Logging**  
    - Integrate structured logging for requests, errors, and health metrics.
    - Use monitoring and alerting tools (e.g., Prometheus, Grafana) to track service availability and performance.

5. **Horizontal Scalability and Load Balancing**  
    - Design the API to be stateless for horizontal scaling behind load balancers.
    - Consider containerization (Docker) and orchestration (Kubernetes) for flexible deployments.

6. **API Versioning and Documentation**  
    - Implement versioning for API endpoints to support backward-compatible changes.
    - Enhance Swagger documentation with examples, error codes, and usage guidelines.

7. **Security Enhancements**  
    - Enforce HTTPS and secure headers.
    - Rigorously validate all inputs to prevent injection or denial of service attacks.
    - Regularly audit cryptographic components for vulnerabilities.

8. **Additional Features**  
    - Support batch queries for multiple users in a single request.
    - Provide clients with mechanisms to verify proofs offline.
    - Add webhook notifications for balance updates or Merkle root changes.

---

## Versioning and Tags

This repository uses semantic versioning with git tags to mark releases:

- `v1.0.0-merkle-lib` — First version of the Merkle Library.
- `v1.0.0-api` — First version of the Web API.

To create tags locally:

```bash
git tag v1.0.0-merkle-lib
git tag v1.0.0-api
git push --tags
```

---

## Contribution Guide

- Write clear and descriptive commit messages following the [Conventional Commits](https://www.conventionalcommits.org/) style.
- Maintain strict separation between library logic and the API presentation layer.
- Keep tests up to date and comprehensive.

---