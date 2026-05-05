import React, { useState, useEffect } from "react";
import { apiClient } from "../services/apiClient";
import { ShoppingCart, Zap } from "lucide-react"; // Added Zap icon for "Order Now"

// Added onDirectOrder to the props
export default function ProductCatalog({ onAddToCart, onDirectOrder }) {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    try {
      setLoading(true);
      const data = await apiClient.getProducts();
      setProducts(data);
      setError(null);
    } catch (err) {
      setError("Failed to load products");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="text-center">
          <div className="spinner w-16 h-16 mx-auto mb-4"></div>
          <p className="text-gray-600 font-semibold text-lg">
            Loading amazing products...
          </p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="text-center bg-white p-8 rounded-2xl shadow-medium">
          <p className="text-red-600 text-2xl mb-6 font-bold">⚠️ Oops!</p>
          <p className="text-red-600 text-lg mb-6">{error}</p>
          <button
            onClick={loadProducts}
            className="bg-gradient-to-r from-blue-600 to-blue-700 text-white px-8 py-3 rounded-lg hover:shadow-lg font-bold transition-all duration-300 hover:scale-105"
          >
            🔄 Try Again
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="h-full overflow-y-auto bg-gradient-to-br from-slate-50 to-slate-100">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="mb-12">
          <h2 className="text-4xl font-bold text-gray-800 mb-2">
            ✨ Featured Products
          </h2>
          <p className="text-gray-600">
            Discover our amazing collection of premium items
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {products.map((product) => (
            <div
              key={product.id}
              className="bg-white rounded-2xl shadow-soft overflow-hidden hover:shadow-hard card-hover transition-all duration-300 border border-slate-100 flex flex-col group"
            >
              {/* Image Container */}
              <div className="aspect-square bg-gradient-to-br from-slate-100 to-slate-200 flex items-center justify-center relative overflow-hidden">
                {product.imageUrl ? (
                  <img
                    src={product.imageUrl}
                    alt={product.name}
                    className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500"
                  />
                ) : (
                  <div className="text-6xl group-hover:scale-125 transition-transform duration-300">
                    📦
                  </div>
                )}
                {product.stockQuantity === 0 && (
                  <div className="absolute inset-0 bg-black/40 flex items-center justify-center">
                    <span className="bg-red-500 text-white px-4 py-2 rounded-full font-bold">
                      Out of Stock
                    </span>
                  </div>
                )}
              </div>

              {/* Content Container */}
              <div className="p-5 flex flex-col flex-1">
                <h3 className="font-bold text-lg mb-2 line-clamp-2 text-gray-800 group-hover:text-blue-600 transition-colors">
                  {product.name}
                </h3>
                <p className="text-gray-600 text-sm mb-4 line-clamp-2 flex-grow">
                  {product.description || "Premium quality product"}
                </p>

                {/* Price and Stock */}
                <div className="flex justify-between items-center mb-4 pb-4 border-b border-slate-100">
                  <span className="text-3xl font-bold text-gradient">
                    ${product.price.toFixed(2)}
                  </span>
                  <span
                    className={`text-sm font-bold px-3 py-1 rounded-full ${
                      product.stockQuantity > 0
                        ? "bg-green-100 text-green-700"
                        : "bg-red-100 text-red-700"
                    }`}
                  >
                    {product.stockQuantity > 0
                      ? `${product.stockQuantity} left`
                      : "Sold Out"}
                  </span>
                </div>

                {/* Action Buttons */}
                <div className="flex gap-2">
                  <button
                    onClick={() => onAddToCart(product)}
                    disabled={product.stockQuantity === 0}
                    className="flex-1 bg-gradient-to-r from-gray-200 to-gray-300 text-gray-800 py-3 rounded-xl hover:shadow-md disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-2 text-sm font-bold transition-all duration-300 hover:scale-105 active:scale-95"
                  >
                    <ShoppingCart size={18} />
                    Add
                  </button>

                  <button
                    onClick={() => onDirectOrder(product)}
                    disabled={product.stockQuantity === 0}
                    className="flex-1 bg-gradient-to-r from-orange-500 to-red-500 text-white py-3 rounded-xl hover:shadow-lg disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-2 text-sm font-bold transition-all duration-300 hover:scale-105 active:scale-95"
                  >
                    <Zap size={18} />
                    Order Now
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
