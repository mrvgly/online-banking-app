
# OnlineBankingCaseStudy

This project is developed for Gringotts Bank and simulates a simple online banking App. Using this app, you can create customers, create accounts for any customer and make withdrawing & deposit transactions.

  

## Requirements

  

* Install Docker to your local machine.

* Download SQL Server 2019 image from Docker Hub and run.

* Install .NET 5 and Visual Studio (at least version must be 2019)

  

## APIs

  

* GET /api/Accounts/Detail/{id}

This API provides that getting account details of given id.

  

* POST /api/Accounts

This API provides that creating a new account.

  

* POST /api/Customers

This API provides that creating a new customer.

  

* GET /api/Customers/Detail/{id}

This API provides that getting customer details with accounts.

  

* POST /api/Customers/Login

This API provides that creating access token for authentication of APIs.

  

* GET /api/Transactions/{accountId}

This API provides that getting transactions of account.

  

* GET /api/Transactions/WithPeriods/{customerId}

This API provides that getting transactions of a customer with given periods.

  

* GET /api/Transactions/MakeDeposit

This API provides that making a deposit for a given account of customer.

  

* GET /api/Transactions/MakeWithdrawal

This API provides that making withdrawal a the given account of customer.