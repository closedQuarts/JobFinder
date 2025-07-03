# JobFinder - SE4458 Büyük Efe Final Project

This project is a job search platform similar to Kariyer.net, designed for SE4458 Final Project.  
It includes backend REST APIs, Service Bus messaging, distributed caching with Redis, an API Gateway, and a frontend built in React.js.

## 🚀 Final Deployed URLs

- **API Gateway:** [https://jobfindergateway-eshpcwedgjc6f6hs.northeurope-01.azurewebsites.net](https://jobfindergateway-eshpcwedgjc6f6hs.northeurope-01.azurewebsites.net)
- **JobFinder API:** [https://jobfinderapi4458-d3g6fkgca2cfaahb.northeurope-01.azurewebsites.net](https://jobfinderapi4458-d3g6fkgca2cfaahb.northeurope-01.azurewebsites.net)
- **Frontend (React App):** *deploy edilirse link buraya gelecek*

---

## 🛠 Design, Assumptions, and Decisions

### Architecture

- Microservice Architecture
- Ocelot API Gateway used for routing between services.
- Azure Service Bus for queue messaging between Job Posting creation and Notification Service.
- Redis distributed caching for JobSearch caching.
- Entity Framework Core for database operations.
- React.js frontend communicating via Gateway.
- Docker containers for backend and frontend

### Assumptions

- Authentication is simple; no JWT yet. Users log in with name and email.
- AI Chatbox integration is not fully implemented but planned with OpenAI.
- Frontend and backend are separately deployed and communicate via Gateway.

### Decisions

- Used Azure SQL for relational storage.
- Redis Cache on Azure.
- Deployment on Azure App Service.
- Frontend built with Create React App.
- Responsive UI inspired by kariyer.net.

---

## 🗃 Data Models (ER Diagram)

![image](https://github.com/user-attachments/assets/77197fd7-45ca-43b1-9744-b4221c4295fe)


---

## 🎯 Use Cases Covered

✅ Job Posting CRUD  
✅ Job Search (with caching in Redis)  
✅ Notifications for new job postings based on search history  
✅ User login and profile header  
✅ Applying for jobs  
✅ AI Chatbot (backend API) for job search queries  
✅ API Gateway routing with Ocelot

---

## ⚠️ Issues Encountered

- **Service Bus Consumption**
  - Consuming scoped services (like NotificationRepository) from singleton background services caused DI issues. Solved by restructuring service lifetimes.
- **Redis Authentication**
  - Initial Redis connection failed due to missing authentication config. Solved by updating connection string.
- **Frontend CORS Errors**
  - Solved by configuring CORS on API Gateway.
- **Deployment Errors**
  - 500.30 errors on Azure due to unhandled exceptions in Program.cs. Fixed by handling async calls properly.

---

## 💡 Future Improvements

- Real AI Chat integration with OpenAI.
- JWT authentication & authorization.
- More advanced UI with styling frameworks.
- Load testing with k6 or JMeter.
