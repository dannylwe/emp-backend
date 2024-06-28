#### Auth

##### POST /api/Auth/login
- **Description:** Logs in a user with email and password.
- **Request Body:** `LoginEmployeeDto`
- **Response:** 200 Success

##### Example Curl Command:
```bash
curl -X POST "http://your-api-url/api/Auth/login" -H "Content-Type: application/json" -d '{
  "email": "example@example.com",
  "password": "your_password"
}'
```

#### Employee

##### GET /api/Employee
- **Description:** Retrieves a list of all employees.
- **Response:** Array of `Employee`
- **Response Content Types:** text/plain, application/json, text/json

##### Example Curl Command (with Bearer Token):
```bash
curl -X GET "http://your-api-url/api/Employee" -H "Authorization: Bearer {token}"
```

##### POST /api/Employee
- **Description:** Creates a new employee.
- **Request Body:** `CreateEmployeeDto`
- **Response:** `Employee`
- **Response Content Types:** text/plain, application/json, text/json

##### Example Curl Command:
```bash
curl -X POST "http://your-api-url/api/Employee" -H "Content-Type: application/json" -d '{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "password123",
  "baseSalary": 50000
}'
```

##### GET /api/Employee/{id}
- **Description:** Retrieves details of a specific employee by ID.
- **Parameters:** `id` (integer, path, required)
- **Response:** `Employee`
- **Response Content Types:** text/plain, application/json, text/json

##### Example Curl Command (with Bearer Token):
```bash
curl -X GET "http://your-api-url/api/Employee/{id}" -H "Authorization: Bearer {token}"
```
Replace `{id}` with the actual ID of the employee you want to retrieve.

##### PUT /api/Employee/{id}
- **Description:** Updates details of a specific employee by ID.
- **Parameters:** `id` (integer, path, required)
- **Request Body:** `UpdateEmployeeDto`
- **Response:** `Employee`
- **Response Content Types:** text/plain, application/json, text/json

##### Example Curl Command (with Bearer Token):
```bash
curl -X PUT "http://your-api-url/api/Employee/{id}" -H "Authorization: Bearer {token}" -H "Content-Type: application/json" -d '{
  "firstName": "Updated John"
}'
```
Replace `{id}` with the actual ID of the employee you want to update.

##### DELETE /api/Employee/{id}
- **Description:** Deletes a specific employee by ID.
- **Parameters:** `id` (integer, path, required)
- **Response:** 200 Success

##### Example Curl Command (with Bearer Token):
```bash
curl -X DELETE "http://your-api-url/api/Employee/{id}" -H "Authorization: Bearer {token}"
```
Replace `{id}` with the actual ID of the employee you want to delete.

##### GET /api/Employee/export
- **Description:** Exports data related to employees.
- **Response:** 200 Success

##### Example Curl Command (with Bearer Token):
```bash
curl -X GET "http://your-api-url/api/Employee/export" -H "Authorization: Bearer {token}"
```

### Security
- **Bearer Token:** JWT Authorization header using the Bearer scheme. Example: `"Authorization: Bearer {token}"`

### Example Bearer Token Usage with Curl
Replace `{token}` with an actual JWT token obtained after a successful login.

```bash
curl -X GET "http://your-api-url/api/Employee" -H "Authorization: Bearer {token}"
```

This documentation now clearly indicates where the Bearer token needs to be included in the `curl` commands, ensuring proper authentication for accessing protected endpoints. Adjust `your-api-url` with the actual base URL of your API endpoint.