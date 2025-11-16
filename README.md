# 🧠 AI Goal Coach  
A full-stack AI-powered application that converts raw user goals into **SMART actionable goals and measurable key results** using either **OpenAI** or **Ollama (local & free)**—all switchable through configuration.

This project includes:
- 🔐 Authentication (Register/Login)
- 🎯 Goal management dashboard
- 🤖 AI-based goal refinement
- ✔️ Task completion tracking
- 📌 Intent classification
- ⚡ Dynamic AI provider selection (OpenAI or Ollama)
- 🎨 Angular UI with modals, toasts & loader

---

# 🚀 Features

### ✔️ Frontend (Angular)
- Login / Register
- My Goals Page (auto-refresh)
- Add Goal using AI refinement modal
- Refine raw goal → SMART goal + KR
- Goal detail page
- Mark KR tasks as complete/incomplete
- Bootstrap UI + Toast notifications
- Global HTTP Interceptor loader

### ✔️ Backend (.NET 8)
- Clean Architecture
- JWT Authentication
- EF Core + SQL Server
- Repository pattern
- Microsoft.Extensions.AI
- Dynamic AI provider selection
- Rule-based Intent Classifier
- AI JSON parsing with graceful fallback

---

# 🛠️ Tech Stack
### Backend
- ASP.NET Core 8 Web API  
- Microsoft.Extensions.AI  
- Entity Framework Core  
- SQL Server  
- JWT Auth  
- Ollama / OpenAI  

### Frontend
- Angular 17  
- Bootstrap 5  
- ngx-toastr  
- RxJS  


---

# 📥 Clone the Repository

```sh
git clone https://github.com/<your-repo>/AIGoalCoach.git
cd AIGoalCoach
```
---
Backend Setup
```
cd AIGoalCoach.API
```

Install dependencies and Update Database
```
dotnet restore
dotnet ef database update

```

# Configure API Provider
Open AppSettings.json
```
"AISettings": {
  "Provider": "Ollama",
  "Model": "phi4-mini:latest",
  "OllamaEndpoint": "http://localhost:11434",
  "OpenAIKey": "",
  "OpenAIModel": "gpt-4o-mini"
}
```
To use Ollama Set Provider to "Ollama", for OpenAI set Provider to "OpenAI"

#🤖 Running Ollama Locally (If You Don’t Want OpenAI)
1️⃣ Install Ollama
```
Download: https://ollama.com/download
```

2️⃣ Pull the model
```
ollama pull phi4-mini
```

3️⃣ Start the Ollama server
```
ollama serve
```


It now responds on:
```
http://localhost:11434/api/chat
```
▶️ Run the Backend
```
dotnet run
```

🌐 Frontend Setup (Angular)
Move to Angular project:
```
cd AngularUI/ai-goal-coach
```

1️⃣ Install dependencies
```
npm install
```

2️⃣ Run the Angular app
```
ng serve --open
```


Frontend runs at:
```
http://localhost:4200
```

Set the Base Url Property in Services to that of Backend URL
You can run and test the application

# 📚 Future Enhancements (Optional Section)

Replace rule-based intent classifier with AI-based classifier

Auto-correct malformed JSON responses

Vector embeddings for personalized recommendations

Edit existing goals

Add notifications & reminders

Deploy on cloud (Azure App Service + Azure SQL + Azure OpenAI)


