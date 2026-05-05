const API_BASE_URL =
  process.env.REACT_APP_API_URL || "http://localhost:5000/api";

export const apiClient = {
  // Products
  getProducts: async () => {
    const response = await fetch(`${API_BASE_URL}/products`);
    return response.json();
  },

  getProduct: async (id) => {
    const response = await fetch(`${API_BASE_URL}/products/${id}`);
    return response.json();
  },

  // Orders
  createOrder: async (userId, items) => {
    const response = await fetch(`${API_BASE_URL}/orders`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ userId, items }),
    });
    return response.json();
  },

  getOrders: async (userId) => {
    const response = await fetch(`${API_BASE_URL}/orders/user/${userId}`);
    return response.json();
  },

  getOrderStatus: async (orderId) => {
    const response = await fetch(`${API_BASE_URL}/orders/${orderId}/status`);
    return response.json();
  },

  cancelOrder: async (orderId) => {
    const response = await fetch(`${API_BASE_URL}/orders/${orderId}/cancel`, {
      method: "POST",
    });
    return response.json();
  },

  refundOrder: async (orderId, reason) => {
    const response = await fetch(`${API_BASE_URL}/orders/${orderId}/refund`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ reason }),
    });
    return response.json();
  },

  // Chat
  sendMessage: async (message) => {
    const response = await fetch(`${API_BASE_URL}/chat`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ message }),
    });
    return response.json();
  },

  initChat: async () => {
    const response = await fetch(`${API_BASE_URL}/chat/init`, {
      method: "POST",
    });
    return response.json();
  },
};
