import React, { useEffect, useState } from "react";
import { apiClient } from "../services/apiClient";
import { Package, Clock, CheckCircle, XCircle } from "lucide-react";

const MyOrders = ({ onIdClick }) => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const data = await apiClient.getOrders(1);
        setOrders(data);
      } catch (error) {
        console.error("Error fetching orders:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchOrders();
  }, []);

  const getStatusIcon = (status) => {
    switch (status?.toLowerCase()) {
      case "delivered":
        return {
          icon: <CheckCircle className="text-green-500" size={20} />,
          color: "bg-green-100 text-green-700",
        };
      case "canceled":
        return {
          icon: <XCircle className="text-red-500" size={20} />,
          color: "bg-red-100 text-red-700",
        };
      default:
        return {
          icon: <Clock className="text-blue-500" size={20} />,
          color: "bg-blue-100 text-blue-700",
        };
    }
  };

  if (loading)
    return (
      <div className="flex items-center justify-center h-full bg-gradient-to-br from-slate-50 to-slate-100">
        <div className="text-center">
          <div className="spinner w-16 h-16 mx-auto mb-4"></div>
          <p className="text-gray-600 font-semibold text-lg">
            Loading your orders...
          </p>
        </div>
      </div>
    );

  return (
    <div className="max-w-5xl mx-auto p-6 overflow-y-auto h-full bg-gradient-to-br from-slate-50 to-slate-100">
      {/* Header */}
      <div className="mb-8">
        <h2 className="text-4xl font-bold mb-2 flex items-center gap-3 text-gray-800">
          📦 Order History
        </h2>
        <p className="text-gray-600">Manage and track all your orders</p>
      </div>

      {orders.length === 0 ? (
        <div className="bg-white p-12 rounded-2xl shadow-soft text-center border border-slate-200">
          <p className="text-6xl mb-4">🛒</p>
          <p className="text-gray-600 text-xl font-semibold mb-2">
            No Orders Yet
          </p>
          <p className="text-gray-500">
            Start shopping to see your orders here!
          </p>
        </div>
      ) : (
        <div className="grid gap-4">
          {orders.map((order, index) => {
            const statusInfo = getStatusIcon(order.status);
            return (
              <div
                key={order.id}
                className="bg-white p-6 rounded-2xl shadow-soft hover:shadow-medium border border-slate-200 transition-all duration-300 card-hover group"
              >
                <div className="flex flex-col md:flex-row justify-between md:items-center gap-4">
                  {/* Left Section: Order ID and Date */}
                  <div className="flex-1">
                    <div className="flex items-center gap-3 mb-2">
                      <button
                        onClick={() => onIdClick(order.id)}
                        className="text-2xl font-bold text-blue-600 hover:text-blue-700 hover:underline transition-colors"
                      >
                        Order #{order.id}
                      </button>
                      <span className="text-gray-400">•</span>
                      <span className="text-gray-600 font-medium">
                        📅{" "}
                        {new Date(order.orderDate).toLocaleDateString("en-US", {
                          year: "numeric",
                          month: "short",
                          day: "numeric",
                        })}
                      </span>
                    </div>
                    <div className="text-lg font-bold text-gray-800">
                      Total:{" "}
                      <span className="text-gradient">
                        ${order.totalAmount.toFixed(2)}
                      </span>
                    </div>
                  </div>

                  {/* Right Section: Status and Action */}
                  <div className="flex flex-col md:flex-row items-start md:items-center gap-4">
                    <div
                      className={`flex items-center gap-2 font-bold px-4 py-2 rounded-lg ${statusInfo.color}`}
                    >
                      {statusInfo.icon}
                      <span className="capitalize">{order.status}</span>
                    </div>
                    <button
                      onClick={() => onIdClick(order.id)}
                      className="bg-gradient-to-r from-blue-500 to-blue-600 hover:from-blue-600 hover:to-blue-700 text-white px-6 py-2 rounded-lg font-bold transition-all duration-300 hover:shadow-lg hover:scale-105 active:scale-95 whitespace-nowrap"
                    >
                      💬 Ask AI
                    </button>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
      )}

      {orders.length > 0 && (
        <div className="mt-8 bg-gradient-to-r from-blue-50 to-purple-50 p-6 rounded-2xl border border-blue-200">
          <p className="text-sm text-gray-700 text-center">
            <span className="font-semibold">💡 Quick Tip:</span> Click on any
            Order ID or "Ask AI" button to check status, cancel, or request a
            refund using our support chat!
          </p>
        </div>
      )}
    </div>
  );
};

export default MyOrders;
