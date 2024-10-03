# 🛒 Full-Stack E-Commerce Website

This project is a fully functional, feature-rich e-commerce platform built using **.NET 8 Web API** for the backend and **Angular 17** for the frontend. The project follows **Clean Architecture** principles and includes robust authentication, role-based authorization, payment integration, and various product management features.

## 🏗️ Architecture

The project uses **Clean Architecture** which is divided into the following layers:

- **Application Layer**: Handles business logic, service contracts, and use cases.
- **Domain Layer**: Contains the core domain entities and business rules.
- **Infrastructure Layer**: Implements external services like databases, repositories, email, and caching.
- **Presentation Layer**: Exposes the Web API endpoints for client interaction.

Additionally, I used the **Repository Pattern** to maintain separation of concerns, ensure testability, and promote scalability.

## 🚀 Features

### Backend:

- **JWT Authentication & Refresh Tokens**: The application uses **JWT Authentication** for secure user sessions. Both access and refresh tokens are implemented to ensure secure and long-lasting sessions.
- **Role-Based Authentication & Authorization**: Supports different roles (e.g., User, Seller) with distinct permissions. Roles such as **User** and **Seller** have distinct permissions within the app, ensuring that users only have access to functionalities relevant to their role.
- **Payment Integration (PayPal)**: Payments are processed through **PayPal**, tested using developer accounts for security and reliability.
- **Email Sending (SendGrid)**: Email functionality is powered by **SendGrid**, allowing the system to send emails for password recovery and other essential communication.
- **Trending Products Caching**: Improved performance by caching frequently accessed product data, reducing redundant database queries.
- **Efficient Product Pagination**: Supports large datasets by retrieving products in paginated form, reducing load times and improving scalability.
- **Product Filtering**: Allows users to easily search for products by various criteria, improving the overall shopping experience.
- **Profile Management**: Both users and sellers can update profile information and change passwords.
- **Seller Product Management**: Sellers can add, update, and delete products.
- **User Payment History**: Users can view their order payment history.
- **AutoMapper**: Simplifies object mapping between entities and DTOs, reducing boilerplate code and improving readability and maintainability.
- **DTO (Data Transfer Objects)**: Efficient and secure data transfer between frontend and backend.

### Frontend:

- **Responsive UI**: Fully responsive layout built using **Bootstrap**.
- **JWT Interceptor**: Automatically attaches JWT tokens to API requests for authenticated endpoints.
- **Route Guards**: Protects sensitive routes from unauthorized access.
- **ngx-toastr**: Provides user-friendly success and error notifications.

## 🛠️ Technologies

### Backend:

- **.NET 8 Web API**
- **Entity Framework Core** for database management.
- **Microsoft SQL Server 2022** as the database.
- **Identity Framework** (Customized for JWT).
- **SendGrid** for email sending.
- **PayPal** for payment processing.
- **AutoMapper** for object mapping.
- **JWT Authentication** for secure token-based authorization.

### Frontend:

- **Angular 17** for a reactive, single-page application.
- **Bootstrap** for a responsive, mobile-first design.
- **ngx-toastr** for notifications.
- **JWT Interceptor** for secure API requests.
- **Route Guards** for role-based page access control.

## ⚙️ Installation

### Prerequisites

- **.NET 8 SDK**
- **Node.js** and **npm**
- **Microsoft SQL Server 2022**
- **Angular CLI**

### Backend Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/abdallah203451/Full-Stack-ECommerce.git
   cd your-repo-name
   ```
2. Configure the database connection string in `appsettings.json` and add the SendGrid API key
   ```bash
     "SendGrid": {
       "SendGridKey": "Your SendGrid API Key"
     }
   ```
3. Run database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the backend API:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. Navigate to the frontend folder:
   ```bash
   cd frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the frontend:
   ```bash
   ng serve
   ```
4. Open the application at `http://localhost:4200`.

## 📦 Deployment

The project is ready for deployment. Ensure the following steps:

1. Set up the backend on a server (e.g., Azure, AWS, or any other hosting provider).
2. Deploy the frontend to a hosting platform (e.g., Netlify, Vercel, or GitHub Pages).

## 🌐 Live Demo

Check out the live website here: https://e-commerce-front-end-iota.vercel.app

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!
Feel free to check the issues page.

## 📝 License

This project is licensed under the MIT License.
