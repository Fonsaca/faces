# FACES

![Badge](https://img.shields.io/badge/Status-In%20Development-orange)

Fictional Administration of Company Employees (FACES)

An app to manage employees of a company.

## 🛠 Employee Requirements

An employee should have the following attributes:

- **First and last name** (Required)
- **E-mail** (Required)
- **Doc number** (Unique and Required)
- **Phone** (Should have more than one)
- **Manager name** (*A manager can also be an employee*)
- **Password**
- **Must validate that the person is not a minor.**

### Permission Restrictions

- **You cannot create a user with higher permissions than the current one.**
- **An employee cannot create a leader.**
- **A leader cannot create a director.**

## 🚀 Technologies Used

- .NET 8
- Angular
- PostgreSQL
- Entity Framework
- Serilog
- PrimeNg

## 📦 Installation

### Download latest postgres docker image
docker pull postgres


### Build .NET API


### Build Angular App



## 📖 How to Use

### Run API
docker run --name facesapi -p 80:8080 -d facesapi


### Run postgres
docker run --name postgres -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -p 25100:5432 -d postgres


## ✅ Tests

### TODO

- Add test execution details.

---

Made by Gabriel Fonsaca!

