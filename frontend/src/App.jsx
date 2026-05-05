import React, { useState, useEffect } from "react";
import { Send, ShoppingCart, Menu, X, ClipboardList } from "lucide-react";
import { apiClient } from "./services/apiClient";
import ProductCatalog from "./components/ProductCatalog";
import ChatInterface from "./components/ChatInterface";
import MyOrders from "./components/MyOrders";
import "./index.css";

export default function App() {
  const [currentView, setCurrentView] = useState("products"); // 'products', 'chat', or 'orders'
  const [cartItems, setCartItems] = useState([]);
  const [showCart, setShowCart] = useState(false);
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [initialChatMessage, setInitialChatMessage] = useState(null);

  const handleAddToCart = (product) => {
    setCartItems((prev) => {
      const existing = prev.find((item) => item.id === product.id);
      if (existing) {
        return prev.map((item) =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item,
        );
      }
      return [...prev, { ...product, quantity: 1 }];
    });
  };

  const handleRemoveFromCart = (productId) => {
    setCartItems((prev) => prev.filter((item) => item.id !== productId));
  };

  const handleCheckout = async () => {
    if (cartItems.length === 0) {
      alert("Your cart is empty");
      return;
    }

    try {
      const items = cartItems.map((item) => ({
        productId: item.id,
        quantity: item.quantity,
      }));

      const result = await apiClient.createOrder(1, items);
      alert(`Order placed! Order ID: ${result.id}`);
      setCartItems([]);
      setShowCart(false);
      setCurrentView("orders");
    } catch (error) {
      alert("Error placing order");
    }
  };

  // NEW: Direct Order Functionality
  const handleDirectOrder = async (product) => {
    const confirmOrder = window.confirm(
      `Do you want to order ${product.name} immediately?`,
    );
    if (!confirmOrder) return;

    try {
      const items = [
        {
          productId: product.id,
          quantity: 1,
        },
      ];

      const result = await apiClient.createOrder(1, items);
      alert(`Success! Order #${result.id} placed.`);

      // Automatically show the user their new order
      setCurrentView("orders");
    } catch (error) {
      alert("Error placing direct order");
    }
  };

  const handleOrderClick = (orderId) => {
    setCurrentView("chat");
    setInitialChatMessage(`Check status for Order #${orderId}`);
  };

  return (
    <div className="flex h-screen bg-gradient-to-br from-slate-50 to-slate-100">
      {/* Premium Header */}
      <div className="fixed top-0 left-0 right-0 bg-gradient-to-r from-blue-600 via-blue-500 to-purple-600 shadow-hard z-40 backdrop-blur-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center h-20">
            <div className="flex items-center gap-4">
              <button
                onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
                className="lg:hidden text-white hover:bg-white/20 p-2 rounded-lg transition-colors"
              >
                {mobileMenuOpen ? <X size={24} /> : <Menu size={24} />}
              </button>
              <div>
                <h1 className="text-3xl font-bold text-white drop-shadow-lg">
                  🛍️ ShopHub
                </h1>
                <p className="text-blue-100 text-xs">Premium E-Commerce</p>
              </div>
            </div>

            <div className="hidden lg:flex gap-2">
              <button
                onClick={() => setCurrentView("products")}
                className={`px-6 py-2.5 rounded-lg font-semibold transition-all duration-300 ${
                  currentView === "products"
                    ? "bg-white text-blue-600 shadow-lg scale-105"
                    : "text-white hover:bg-white/20"
                }`}
              >
                🏪 Shop
              </button>
              <button
                onClick={() => setCurrentView("orders")}
                className={`px-6 py-2.5 rounded-lg font-semibold transition-all duration-300 ${
                  currentView === "orders"
                    ? "bg-white text-blue-600 shadow-lg scale-105"
                    : "text-white hover:bg-white/20"
                }`}
              >
                📦 My Orders
              </button>
              <button
                onClick={() => setCurrentView("chat")}
                className={`px-6 py-2.5 rounded-lg font-semibold transition-all duration-300 ${
                  currentView === "chat"
                    ? "bg-white text-blue-600 shadow-lg scale-105"
                    : "text-white hover:bg-white/20"
                }`}
              >
                💬 Support
              </button>
            </div>

            <button
              onClick={() => setShowCart(!showCart)}
              className="relative p-3 bg-white/20 text-white rounded-xl hover:bg-white/30 transition-all duration-300 hover:scale-110 backdrop-blur-sm"
            >
              <ShoppingCart size={24} />
              {cartItems.length > 0 && (
                <span className="absolute top-1 right-1 bg-red-500 text-white text-xs font-bold rounded-full w-7 h-7 flex items-center justify-center shadow-lg animate-pulse">
                  {cartItems.length}
                </span>
              )}
            </button>
          </div>

          {/* Mobile Menu */}
          {mobileMenuOpen && (
            <div className="lg:hidden pb-4 flex flex-col gap-2 fade-in">
              <button
                onClick={() => {
                  setCurrentView("products");
                  setMobileMenuOpen(false);
                }}
                className={`w-full px-4 py-3 rounded-lg font-semibold transition-all ${currentView === "products" ? "bg-white text-blue-600 shadow-lg" : "bg-white/20 text-white hover:bg-white/30"}`}
              >
                🏪 Shop
              </button>
              <button
                onClick={() => {
                  setCurrentView("orders");
                  setMobileMenuOpen(false);
                }}
                className={`w-full px-4 py-3 rounded-lg font-semibold transition-all ${currentView === "orders" ? "bg-white text-blue-600 shadow-lg" : "bg-white/20 text-white hover:bg-white/30"}`}
              >
                📦 Orders
              </button>
              <button
                onClick={() => {
                  setCurrentView("chat");
                  setMobileMenuOpen(false);
                }}
                className={`w-full px-4 py-3 rounded-lg font-semibold transition-all ${currentView === "chat" ? "bg-white text-blue-600 shadow-lg" : "bg-white/20 text-white hover:bg-white/30"}`}
              >
                💬 Support
              </button>
            </div>
          )}
        </div>
      </div>

      {/* Main Content Area */}
      <div className="flex-1 pt-20 overflow-hidden">
        {currentView === "products" ? (
          <ProductCatalog
            onAddToCart={handleAddToCart}
            onDirectOrder={handleDirectOrder}
          />
        ) : currentView === "orders" ? (
          <MyOrders onIdClick={handleOrderClick} />
        ) : (
          <ChatInterface
            initialMessage={initialChatMessage}
            onMessageSent={() => setInitialChatMessage(null)}
          />
        )}
      </div>

      {/* Premium Cart Sidebar */}
      {showCart && (
        <div className="fixed right-0 top-20 bottom-0 w-96 bg-gradient-to-b from-white to-slate-50 shadow-hard border-l border-slate-200 overflow-y-auto z-50 slide-in-right">
          <div className="p-6 sticky top-0 bg-gradient-to-r from-blue-50 to-purple-50 border-b border-slate-200">
            <div className="flex justify-between items-center">
              <h2 className="text-2xl font-bold text-gradient">
                🛒 Shopping Cart
              </h2>
              <button
                onClick={() => setShowCart(false)}
                className="text-gray-500 hover:text-gray-700 hover:bg-gray-200 p-2 rounded-lg transition-colors"
              >
                <X size={20} />
              </button>
            </div>
          </div>

          {cartItems.length === 0 ? (
            <div className="flex items-center justify-center py-16 text-center">
              <div>
                <p className="text-5xl mb-4">🛍️</p>
                <p className="text-gray-600 font-semibold">
                  Your cart is empty
                </p>
                <p className="text-gray-400 text-sm mt-2">
                  Add some items to get started!
                </p>
              </div>
            </div>
          ) : (
            <>
              <div className="space-y-4 p-6">
                {cartItems.map((item) => (
                  <div
                    key={item.id}
                    className="bg-white p-4 rounded-xl shadow-soft hover:shadow-medium transition-all duration-300 border border-slate-100"
                  >
                    <div className="flex justify-between mb-2">
                      <span className="font-semibold text-gray-800">
                        {item.name}
                      </span>
                      <button
                        onClick={() => handleRemoveFromCart(item.id)}
                        className="text-red-500 hover:text-red-700 hover:bg-red-50 px-2 py-1 rounded transition-colors font-bold"
                      >
                        ✕
                      </button>
                    </div>
                    <div className="text-sm text-gray-600 mb-2">
                      Qty:{" "}
                      <span className="font-semibold">{item.quantity}</span> × $
                      {item.price.toFixed(2)}
                    </div>
                    <div className="text-lg font-bold text-blue-600">
                      ${(item.quantity * item.price).toFixed(2)}
                    </div>
                  </div>
                ))}
              </div>

              <div className="sticky bottom-0 bg-gradient-to-t from-white via-white p-6 border-t border-slate-200 shadow-hard">
                <div className="flex justify-between font-bold text-2xl mb-6 text-gray-800">
                  <span>Total:</span>
                  <span className="text-transparent bg-clip-text bg-gradient-to-r from-blue-600 to-purple-600">
                    $
                    {cartItems
                      .reduce(
                        (sum, item) => sum + item.quantity * item.price,
                        0,
                      )
                      .toFixed(2)}
                  </span>
                </div>
                <button
                  onClick={handleCheckout}
                  className="w-full bg-gradient-to-r from-green-500 to-emerald-500 text-white py-3 rounded-xl hover:shadow-lg font-bold text-lg transition-all duration-300 hover:scale-105 active:scale-95"
                >
                  ✓ Checkout Now
                </button>
              </div>
            </>
          )}
        </div>
      )}
    </div>
  );
}
