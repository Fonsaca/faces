# FACES



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
- Docker
- Entity Framework
- Serilog
- PrimeNg
- Jwt
- Font Awesome Icons

## 📦 Requirements before Installation

Before starting the installation, ensure you have the following installed:

- Docker Desktop
- Visual Studio 2022
- .NET 8 SDK
- Angular 19
- NodeJS

## 📦 Installation

### Create a docker network

```bash
docker network create faces-network
```

### Download latest PostgreSQL Docker image and Run

```bash
docker pull postgres

docker run --network faces-network --name postgres -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -p 25003:5432 -d postgres 
```

### Create the database

```bash
cd ./API/Faces

docker cp ./create_db.sql postgres:/tmp/create_db.sql

docker exec -it postgres psql -U admin -f /tmp/create_db.sql

docker cp ./migration_init.sql postgres:/tmp/migration_init.sql

docker exec -it postgres psql -U admin -f /tmp/migration_init.sql -d Faces
```

### Build the Backend Project

```bash
dotnet build "Faces.sln"
```

### Build .NET API Docker image

```bash
docker build -t facesapi .
```

### Create Angular Docker image

```bash
cd ../../Web/faces

docker build -t facesweb .
```


### Run
```bash
docker run --network faces-network --name facesapi -p 25001:8080 -d facesapi

docker run --name facesweb -p 80:80 -d facesweb
```
## 📖 How to Use

### Login

Log in with the admin user:

- Document: 0001
- Password: Admin@123

---

Made by Gabriel Fonsaca!

