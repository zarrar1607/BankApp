import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Dashboard.css';

const Dashboard = () => {
  // Set default values to avoid undefined issues
  const [personalInfo, setPersonalInfo] = useState({
    username: '',
    email: '',
    accountBalance: 0,
    recentTransactions: []
  });
  const [amount, setAmount] = useState(0);
  const [showAllTransactions, setShowAllTransactions] = useState(false);
  const navigate = useNavigate();
  const username = localStorage.getItem('username');  // Get the username from localStorage

  useEffect(() => {
    // Fetch user information when the dashboard loads
    const fetchUserInfo = async () => {
      try {
        if (username) {
          const response = await axios.get(`http://localhost:5011/api/login/userinfo/${username}`);
          setPersonalInfo({
            ...response.data,
            recentTransactions: response.data.recentTransactions.reverse() // Reverse to show most recent first
          });
          console.log("User Information on initial fetch:", response.data);
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

  // Handle Deposit
  const handleDeposit = async () => {
    if (amount <= 0) {
      alert("Please enter a valid amount greater than 0.");
      return;
    }

    try {
      const response = await axios.post(`http://localhost:5011/api/login/deposit`, {
        username: username,
        amount: amount
      });
      console.log("API Response after deposit:", response.data);

      // Update balance and recent transactions immediately after deposit
      setPersonalInfo(prevInfo => ({
        ...prevInfo,
        accountBalance: response.data.newBalance, // make sure the field is correctly named
        recentTransactions: [
          {
            date: new Date().toISOString().split('T')[0], // Current date
            amount: amount,
            description: "Deposit"
          },
          ...prevInfo.recentTransactions // Add new transaction at the start
        ]
      }));
    } catch (error) {
      console.error('Error during deposit:', error);
    }
  };

  // Handle Withdraw
  const handleWithdraw = async () => {
    if (amount <= 0) {
      alert("Please enter a valid amount greater than 0.");
      return;
    }

    try {
      const response = await axios.post(`http://localhost:5011/api/login/withdraw`, {
        username: username,
        amount: amount
      });
      console.log("API Response after withdrawal:", response.data);

      // Update balance and recent transactions immediately after withdrawal
      setPersonalInfo(prevInfo => ({
        ...prevInfo,
        accountBalance: response.data.newBalance, // make sure the field is correctly named
        recentTransactions: [
          {
            date: new Date().toISOString().split('T')[0], // Current date
            amount: -amount,
            description: "Withdrawal"
          },
          ...prevInfo.recentTransactions // Add new transaction at the start
        ]
      }));
    } catch (error) {
      console.error('Error during withdrawal:', error);
    }
  };

  return (
    <div className="dashboard-container">
      <h1>Welcome to Your Dashboard!</h1>

      {/* Account Summary Section */}
      <div className="account-summary">
        <h2>Account Summary</h2>
        {typeof personalInfo.accountBalance === 'number' ? (
          <p>Account Balance: ${personalInfo.accountBalance.toFixed(2)}</p>
        ) : (
          <p>Loading...</p>
        )}
      </div>

      {/* Recent Transactions Section */}
      <div className="recent-transactions">
        <h2>Recent Transactions</h2>
        {personalInfo.recentTransactions && personalInfo.recentTransactions.length > 0 ? (
          <ul>
            {(showAllTransactions
              ? [...personalInfo.recentTransactions] // Creating a copy of the array before displaying
              : personalInfo.recentTransactions.slice(0, 5) // Show top 5 transactions by default
            ).map((transaction, index) => (
              <li key={index}>
                {transaction.description}: ${transaction.amount.toFixed(2)} on {transaction.date}
              </li>
            ))}
          </ul>
        ) : (
          <p>Loading...</p>
        )}
        {personalInfo.recentTransactions.length > 5 && (
          showAllTransactions ? (
            <button className="view-button" onClick={() => setShowAllTransactions(false)}>View Less</button>
          ) : (
            <button className="view-button" onClick={() => setShowAllTransactions(true)}>View More</button>
          )
        )}
      </div>

      {/* Personal Information Section */}
      <div className="personal-info">
        <h2>Personal Information</h2>
        {personalInfo.username ? (
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
        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(parseFloat(e.target.value))}
          placeholder="Enter amount"
        />
        <button className="btn-action" onClick={handleDeposit}>Deposit</button>
        <button className="btn-action" onClick={handleWithdraw}>Withdraw</button>
      </div>

      {/* Logout Button */}
      <button onClick={handleLogout} className="btn-logout">Logout</button>
    </div>
  );
};

export default Dashboard;
