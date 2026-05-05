import React, { useState, useEffect, useRef } from "react";
import { Send } from "lucide-react";
import { apiClient } from "../services/apiClient";

export default function ChatInterface() {
  const [messages, setMessages] = useState([]);
  const [inputValue, setInputValue] = useState("");
  const [loading, setLoading] = useState(false);
  const messagesEndRef = useRef(null);

  // 1. Initialize chat when component mounts
  useEffect(() => {
    initializeChat();
  }, []);

  // 2. Auto-scroll to bottom whenever messages change
  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const initializeChat = async () => {
    try {
      setLoading(true);
      const response = await apiClient.initChat();

      if (response.toolCall?.functionName === "DisplayActionMenu") {
        setMessages([
          {
            id: Date.now(),
            role: "assistant",
            content: response.toolCall.arguments.greetingText,
            isMenu: true,
            options: response.toolCall.arguments.options,
          },
        ]);
      } else {
        setMessages([
          {
            id: Date.now(),
            role: "assistant",
            content: response.content,
          },
        ]);
      }
    } catch (error) {
      console.error("Error initializing chat:", error);
      setMessages([
        {
          id: Date.now(),
          role: "assistant",
          content: "Sorry, I encountered an error. Please try again.",
        },
      ]);
    } finally {
      setLoading(false);
    }
  };

  const sendMessage = async (text = null) => {
    const messageText = typeof text === "string" ? text : inputValue.trim();
    if (!messageText) return;

    // Add user message to state
    const userMessage = {
      id: Date.now(),
      role: "user",
      content: messageText,
    };

    setMessages((prev) => [...prev, userMessage]);
    setInputValue("");

    try {
      setLoading(true);
      const response = await apiClient.sendMessage(messageText);

      if (response.toolCall?.functionName === "DisplayActionMenu") {
        setMessages((prev) => [
          ...prev,
          {
            id: Date.now() + 1,
            role: "assistant",
            content: response.toolCall.arguments.greetingText,
            isMenu: true,
            options: response.toolCall.arguments.options,
          },
        ]);
      } else {
        setMessages((prev) => [
          ...prev,
          {
            id: Date.now() + 1,
            role: "assistant",
            content: response.content,
          },
        ]);
      }
    } catch (error) {
      console.error("Error sending message:", error);
      setMessages((prev) => [
        ...prev,
        {
          id: Date.now() + 1,
          role: "assistant",
          content: "Sorry, I encountered an error. Please try again.",
        },
      ]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="h-full flex flex-col bg-gradient-to-b from-slate-50 to-slate-100">
      {/* Chat Header - Reduced Width */}
      <div className="bg-gradient-to-r from-blue-600 to-purple-600 text-white p-6 shadow-soft">
        <div className="max-w-2xl mx-auto">
          <h2 className="text-2xl font-bold">💬 Support Chat</h2>
          <p className="text-blue-100 text-sm mt-1">
            AI-Powered Customer Support
          </p>
        </div>
      </div>

      {/* Messages Container */}
      <div className="flex-1 overflow-y-auto p-6 space-y-4">
        {messages.length === 0 && !loading ? (
          <div className="flex items-center justify-center h-full text-center">
            <div>
              <p className="text-5xl mb-4">🤖</p>
              <p className="text-xl text-gray-600 font-semibold">
                How can I help you?
              </p>
              <p className="text-gray-500 text-sm mt-2">
                Ask me about your orders or refunds.
              </p>
            </div>
          </div>
        ) : (
          <>
            {messages.map((message) => (
              <div
                key={message.id}
                className={`flex ${message.role === "user" ? "justify-end" : "justify-start"} animate-in fade-in duration-300`}
              >
                <div
                  className={`max-w-sm lg:max-w-2xl rounded-2xl p-4 shadow-sm transition-all ${
                    message.role === "user"
                      ? "bg-gradient-to-r from-blue-600 to-blue-700 text-white rounded-br-none"
                      : "bg-white text-gray-800 border border-slate-200 rounded-bl-none"
                  }`}
                >
                  <p className="whitespace-pre-wrap text-sm md:text-base leading-relaxed">
                    {message.content}
                  </p>

                  {/* AI Agent Menu Options */}
                  {message.isMenu && message.options && (
                    <div className="mt-4 space-y-2">
                      {message.options.map((option, idx) => (
                        <button
                          key={idx}
                          onClick={() => sendMessage(option)}
                          className="w-full text-left bg-white text-blue-600 hover:bg-blue-50 px-4 py-3 rounded-lg border-2 border-blue-300 font-semibold transition-all hover:shadow-md hover:scale-[1.02] active:scale-95"
                        >
                          👉 {option}
                        </button>
                      ))}
                    </div>
                  )}
                </div>
              </div>
            ))}

            {/* Loading Dots */}
            {loading && (
              <div className="flex justify-start">
                <div className="bg-white p-4 rounded-2xl shadow-sm border border-slate-200 rounded-bl-none">
                  <div className="flex gap-2">
                    <div className="w-2 h-2 bg-gray-400 rounded-full animate-bounce"></div>
                    <div className="w-2 h-2 bg-gray-400 rounded-full animate-bounce [animation-delay:0.2s]"></div>
                    <div className="w-2 h-2 bg-gray-400 rounded-full animate-bounce [animation-delay:0.4s]"></div>
                  </div>
                </div>
              </div>
            )}
            <div ref={messagesEndRef} />
          </>
        )}
      </div>

      {/* Input Area */}
      <div className="bg-white border-t border-slate-200 p-6 shadow-[0_-4px_6px_-1px_rgba(0,0,0,0.05)]">
        <div className="flex gap-3">
          <input
            type="text"
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && sendMessage()}
            placeholder="Type your message... (e.g., 'Cancel my order')"
            className="flex-1 border-2 border-slate-200 rounded-xl px-5 py-3 focus:outline-none focus:border-blue-600 focus:ring-2 focus:ring-blue-200 transition-all font-medium"
            disabled={loading}
          />
          <button
            onClick={() => sendMessage()}
            disabled={loading || !inputValue.trim()}
            className="bg-gradient-to-r from-blue-600 to-blue-700 text-white px-6 py-3 rounded-xl hover:shadow-lg disabled:opacity-50 disabled:cursor-not-allowed flex items-center gap-2 font-bold transition-all hover:scale-105 active:scale-95"
          >
            <Send size={20} />
          </button>
        </div>
        <p className="text-xs text-gray-500 mt-2 text-center">
          💡 Tip: You can check status, cancel, or request refunds!
        </p>
      </div>
    </div>
  );
}
