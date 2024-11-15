import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Dashboard.css';

const Dashboard = () => {
  const [personalInfo, setPersonalInfo] = useState(null);
  const navigate = useNavigate();
  const username = localStorage.getItem('username');  // Get the username from localStorage
  
  useEffect(() => {
    // Fetch user information when the dashboard loads
    const fetchUserInfo = async () => {
      try {
        if (username) {
          // Fetching user data from the backend
          const response = await axios.get(`http://localhost:5011/api/login/userinfo/${username}`);
          setPersonalInfo(response.data);
          console.log("User Information:", response.data);
        }
      } catch (error) {
        console.error('Error fetching user information:', error);
      }
    };

    fetchUserInfo();
  }, [username]);

  // Logout handler function
  const handleLogout = () => {
    // Clear localStorage and redirect to login
    localStorage.removeItem('username');
    navigate('/');
  };

  return (
    <div className="dashboard-container">
      <h1>Welcome to Your Dashboard!</h1>

      {/* Account Summary Section */}
      <div className="account-summary">
        <h2>Account Summary</h2>
        {personalInfo ? (
          <p>Account Balance: ${personalInfo.accountBalance.toFixed(2)}</p>
        ) : (
          <p>Loading...</p>
        )}
      </div>

      {/* Recent Transactions Section */}
      <div className="recent-transactions">
        <h2>Recent Transactions</h2>
        {personalInfo && personalInfo.recentTransactions && personalInfo.recentTransactions.length > 0 ? (
          <ul>
            {personalInfo.recentTransactions.map((transaction, index) => (
              <li key={index}>
                {transaction.description}: ${transaction.amount.toFixed(2)} on {transaction.date}
              </li>
            ))}
          </ul>
        ) : (
          <p>Loading...</p>
        )}
      </div>

      {/* Personal Information Section */}
      <div className="personal-info">
        <h2>Personal Information</h2>
        {personalInfo ? (
          <>
            <p>Name: {personalInfo.username}</p>
            <p>Email: {personalInfo.email}</p>
          </>
        ) : (
          <p>Loading...</p>
        )}
      </div>

      {/* Quick Actions Section */}
      <div className="quick-actions">
        <h2>Quick Actions</h2>
        <button className="btn-action">Deposit</button>
        <button className="btn-action">Withdraw</button>
      </div>

      {/* Logout Button */}
      <button onClick={handleLogout} className="btn-logout">Logout</button>
    </div>
  );
};

export default Dashboard;
