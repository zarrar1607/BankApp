import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Dashboard.css';

const Dashboard = () => {
  const navigate = useNavigate();

  // Logout handler function
  const handleLogout = () => {
    // Add logic here if needed (e.g., clearing tokens or session storage)
    navigate('/');
  };

  return (
    <div className="dashboard-container">
      <h1>Welcome to Your Dashboard!</h1>

      {/* Account Summary Section */}
      <div className="account-summary">
        <h2>Account Summary</h2>
        <p>Account Balance: $10,000.00</p>
      </div>

      {/* Recent Transactions Section */}
      <div className="recent-transactions">
        <h2>Recent Transactions</h2>
        <ul>
          <li>Transaction 1: -$200.00 on 12/01/2024</li>
          <li>Transaction 2: +$500.00 on 11/29/2024</li>
          <li>Transaction 3: -$50.00 on 11/28/2024</li>
        </ul>
      </div>

      {/* Personal Information Section */}
      <div className="personal-info">
        <h2>Personal Information</h2>
        <p>Name: John Doe</p>
        <p>Email: johndoe@example.com</p>
      </div>

      {/* Quick Actions Section */}
      <div className="quick-actions">
        <h2>Quick Actions</h2>
        <button className="btn-action">Deposit</button>
        <button className="btn-action">Withdraw</button>
        <button className="btn-action">Transfer</button>
      </div>

      {/* Logout Button */}
      <button onClick={handleLogout} className="btn-logout">Logout</button>
    </div>
  );
};

export default Dashboard;
