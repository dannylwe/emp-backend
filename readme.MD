# intEmp API Documentation

## How to run the project
- clone repo
- cd intEmp
- dotnet build
- dotnet run
- Application will be running on http://localhost:5103

## Overview
Welcome to the intEmp API documentation. This API provides endpoints for employee management and authentication. 

## Authentication
JWT Bearer Token must be provided for all requests except for creating an employee and logging in. The token should be included in the Authorization header using the Bearer scheme.

```
Authorization: Bearer {token}
```

## Endpoints

### Authentication

#### Login
Authenticate an employee and obtain a JWT token.

**URL:** `/api/Auth/login`

**Method:** `POST`

**Request Body:**
```json
{
  "email": "string",
  "password": "string"
}
```

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X POST "https://api.example.com/api/Auth/login" \
-H "Content-Type: application/json" \
-d '{
  "email": "user@example.com",
  "password": "password123"
}'
```

### Employee

#### Get All Employees
Retrieve a list of all employees.

**URL:** `/api/Employee`

**Method:** `GET`

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X GET "https://api.example.com/api/Employee" \
-H "Authorization: Bearer {token}"
```

#### Create Employee
Create a new employee. No authentication is required for this endpoint.

**URL:** `/api/Employee`

**Method:** `POST`

**Request Body:**
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "department": "string",
  "password": "string",
  "baseSalary": 0
}
```

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X POST "https://api.example.com/api/Employee" \
-H "Content-Type: application/json" \
-d '{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phone": "123-456-7890",
  "department": "Engineering",
  "password": "securepassword",
  "baseSalary": 60000
}'
```

#### Get Employee by ID
Retrieve an employee by their ID.

**URL:** `/api/Employee/{id}`

**Method:** `GET`

**Path Parameters:**
- `id` (required): The ID of the employee.

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X GET "https://api.example.com/api/Employee/1" \
-H "Authorization: Bearer {token}"
```

#### Update Employee
Update an existing employee's details.

**URL:** `/api/Employee/{id}`

**Method:** `PUT`

**Path Parameters:**
- `id` (required): The ID of the employee.

**Request Body:**
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "department": "string",
  "password": "string",
  "baseSalary": 0,
  "bonus": 0
}
```

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X PUT "https://api.example.com/api/Employee/1" \
-H "Authorization: Bearer {token}" \
-H "Content-Type: application/json" \
-d '{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phone": "123-456-7890",
  "department": "Engineering",
  "password": "newpassword",
  "baseSalary": 65000,
  "bonus": 5000
}'
```

#### Delete Employee
Delete an employee by their ID.

**URL:** `/api/Employee/{id}`

**Method:** `DELETE`

**Path Parameters:**
- `id` (required): The ID of the employee.

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X DELETE "https://api.example.com/api/Employee/1" \
-H "Authorization: Bearer {token}"
```

#### Export Employees
Export the list of employees.

**URL:** `/api/Employee/export`

**Method:** `GET`

**Response:**
- `200 OK` - Success

**Example cURL Request:**
```sh
curl -X GET "https://api.example.com/api/Employee/export" \
-H "Authorization: Bearer {token}"
```

## Schemas

### CreateEmployeeDto
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "department": "string",
  "password": "string",
  "baseSalary": 0
}
```

### Employee
```json
{
  "id": 1,
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "department": "string",
  "passwordHash": "string",
  "createdAt": "2021-01-01T00:00:00Z",
  "updatedAt": "2021-01-01T00:00:00Z",
  "salary": {
    "id": 1,
    "employeeId": 1,
    "baseSalary": 0,
    "bonus": 0,
    "date": "2021-01-01"
  }
}
```

### LoginEmployeeDto
```json
{
  "email": "string",
  "password": "string"
}
```

### UpdateEmployeeDto
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "department": "string",
  "password": "string",
  "baseSalary": 0,
  "bonus": 0
}
```

### Salary
```json
{
  "id": 1,
  "employeeId": 1,
  "baseSalary": 0,
  "bonus": 0,
  "date": "2021-01-01",
  "employee": {
    "id": 1,
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phone": "string",
    "department": "string",
    "passwordHash": "string",
    "createdAt": "2021-01-01T00:00:00Z",
    "updatedAt": "2021-01-01T00:00:00Z"
  }
}
```