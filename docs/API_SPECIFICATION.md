# API Specification

Complete API documentation for the E-Commerce AI Agent system.

## Base URLs

- Development: `http://localhost:5000`
- Production: `https://api.ecommerce-ai.com`

## Authentication

Currently no authentication required. For production, implement OAuth 2.0 or JWT.

---

## Products API

### List All Products

```http
GET /api/products
```

**Response:**

```json
[
  {
    "id": 1,
    "name": "Laptop Pro",
    "description": "High-performance laptop",
    "price": 1299.99,
    "imageUrl": null,
    "stockQuantity": 10,
    "createdAt": "2024-01-15T10:00:00Z"
  }
]
```

**Status Codes:**

- 200 OK - Products retrieved successfully

---

### Get Product by ID

```http
GET /api/products/{id}
```

**Parameters:**

- `id` (path, integer): Product ID

**Response:**

```json
{
  "id": 1,
  "name": "Laptop Pro",
  "price": 1299.99,
  "stockQuantity": 10
}
```

**Status Codes:**

- 200 OK - Product found
- 404 Not Found - Product doesn't exist

---

### Create Product

```http
POST /api/products
```

**Request Body:**

```json
{
  "name": "New Product",
  "description": "Product description",
  "price": 99.99,
  "stockQuantity": 50,
  "imageUrl": "https://example.com/image.jpg"
}
```

**Response:**

```json
{
  "id": 11,
  "name": "New Product",
  "price": 99.99,
  "stockQuantity": 50
}
```

**Status Codes:**

- 201 Created - Product created successfully
- 400 Bad Request - Invalid data

---

## Orders API

### Get Order Status

```http
GET /api/orders/{id}/status
```

**Parameters:**

- `id` (path, integer): Order ID

**Response:**

```json
{
  "orderId": 1,
  "status": "Shipped",
  "totalAmount": 1339.98,
  "orderDate": "2024-01-10T10:00:00Z",
  "shippedDate": "2024-01-11T15:30:00Z",
  "deliveredDate": null,
  "items": [
    {
      "productId": 1,
      "productName": "Laptop Pro",
      "quantity": 1,
      "unitPrice": 1299.99,
      "totalPrice": 1299.99
    }
  ]
}
```

**Status Codes:**

- 200 OK - Order found
- 404 Not Found - Order doesn't exist

---

### Create Order

```http
POST /api/orders
```

**Request Body:**

```json
{
  "userId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1
    },
    {
      "productId": 2,
      "quantity": 2
    }
  ]
}
```

**Response:**

```json
{
  "id": 3,
  "userId": 1,
  "orderDate": "2024-01-15T12:00:00Z",
  "status": "Processing",
  "totalAmount": 1359.97,
  "shippedDate": null,
  "deliveredDate": null,
  "canceledDate": null,
  "lastModified": "2024-01-15T12:00:00Z"
}
```

**Status Codes:**

- 201 Created - Order created successfully
- 400 Bad Request - Invalid data or insufficient stock

---

### Cancel Order

```http
POST /api/orders/{id}/cancel
```

**Parameters:**

- `id` (path, integer): Order ID

**Response:**

```json
{
  "id": 1,
  "status": "Canceled",
  "canceledDate": "2024-01-15T12:15:00Z"
}
```

**Status Codes:**

- 200 OK - Order canceled
- 400 Bad Request - Cannot cancel (already shipped/delivered)
- 404 Not Found - Order doesn't exist

---

### Process Refund

```http
POST /api/orders/{id}/refund
```

**Parameters:**

- `id` (path, integer): Order ID

**Request Body:**

```json
{
  "reason": "Defective product"
}
```

**Response:**

```json
{
  "id": 2,
  "status": "Returned",
  "lastModified": "2024-01-15T12:20:00Z"
}
```

**Status Codes:**

- 200 OK - Refund processed
- 400 Bad Request - Cannot refund (wrong status)
- 404 Not Found - Order doesn't exist

---

### Get User Orders

```http
GET /api/orders/user/{userId}
```

**Parameters:**

- `userId` (path, integer): User ID

**Response:**

```json
[
  {
    "id": 1,
    "userId": 1,
    "status": "Shipped",
    "totalAmount": 1339.98,
    "orderDate": "2024-01-10T10:00:00Z",
    "orderItems": [...]
  }
]
```

**Status Codes:**

- 200 OK - Orders retrieved
- 404 Not Found - User doesn't exist

---

## Chat API

### Initialize Chat

```http
POST /api/chat/init
```

**Response:**

```json
{
  "content": "",
  "toolCall": {
    "functionName": "DisplayActionMenu",
    "arguments": {
      "greetingText": "Hello! I'm your virtual assistant. How can I help you today?",
      "options": [
        "Check Order Status",
        "Cancel Order",
        "Process Refund",
        "View Products",
        "Talk to Support"
      ]
    }
  }
}
```

**Status Codes:**

- 200 OK - Chat initialized

---

### Send Message

```http
POST /api/chat
```

**Request Body:**

```json
{
  "message": "What's the status of order #54321?"
}
```

**Response (Text Response):**

```json
{
  "content": "Order #54321 Status: Shipped\nOrder Date: 2024-01-10\nTotal Amount: $1,339.98\nItems: Laptop Pro (Qty: 1), Wireless Mouse (Qty: 1)\nShipped on: 2024-01-11",
  "toolCall": null
}
```

**Response (Menu Response):**

```json
{
  "content": "",
  "toolCall": {
    "functionName": "DisplayActionMenu",
    "arguments": {
      "greetingText": "Here are your options:",
      "options": ["Check Order Status", "Cancel Order"]
    }
  }
}
```

**Status Codes:**

- 200 OK - Message processed
- 400 Bad Request - Error processing message

---

## Error Responses

All errors follow this format:

```json
{
  "error": "Error message",
  "details": "Additional information"
}
```

### Common Error Codes

| Code | Message               | Cause                    |
| ---- | --------------------- | ------------------------ |
| 400  | Bad Request           | Invalid input data       |
| 404  | Not Found             | Resource doesn't exist   |
| 409  | Conflict              | Business logic violation |
| 500  | Internal Server Error | Server error             |

---

## Rate Limiting

Currently not implemented. For production, implement:

- 100 requests per minute per IP
- 1000 requests per hour per API key

---

## Data Types

### Order Status

Valid values:

- `Processing` - Order received, not yet shipped
- `Shipped` - Order on its way
- `Delivered` - Order delivered
- `Canceled` - Order canceled (can't cancel if shipped)
- `Returned` - Refund initiated

### Product

```json
{
  "id": integer,
  "name": string,
  "description": string | null,
  "price": decimal,
  "imageUrl": string | null,
  "stockQuantity": integer,
  "createdAt": datetime
}
```

### Order

```json
{
  "id": integer,
  "userId": integer,
  "orderDate": datetime,
  "status": string,
  "totalAmount": decimal,
  "shippedDate": datetime | null,
  "deliveredDate": datetime | null,
  "canceledDate": datetime | null,
  "lastModified": datetime
}
```

---

## Webhooks (Future)

Plan to implement webhooks for:

- Order status changes
- Chat events
- Refund processing

---

## Example Usage

### Complete Order Flow

```bash
# 1. Get products
curl http://localhost:5000/api/products

# 2. Create order
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{
    "userId": 1,
    "items": [
      {"productId": 1, "quantity": 1},
      {"productId": 2, "quantity": 1}
    ]
  }'

# 3. Check order status
curl http://localhost:5000/api/orders/3/status

# 4. Cancel order (if still processing)
curl -X POST http://localhost:5000/api/orders/3/cancel

# 5. Or process refund
curl -X POST http://localhost:5000/api/orders/3/refund \
  -H "Content-Type: application/json" \
  -d '{"reason": "Changed my mind"}'
```

### Chat Flow

```bash
# 1. Initialize chat
curl -X POST http://localhost:5000/api/chat/init

# 2. Send message
curl -X POST http://localhost:5000/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Cancel order #1"}'

# 3. Provide additional info if needed
curl -X POST http://localhost:5000/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "Order #1"}'
```

---

## Testing

Use Swagger UI for interactive testing:

```
http://localhost:5001/swagger
```

Or use curl/Postman for automated testing.

---

Last Updated: January 15, 2024
