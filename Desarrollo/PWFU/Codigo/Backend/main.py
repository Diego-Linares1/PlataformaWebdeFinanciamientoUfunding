from fastapi import FastAPI
from starlette.middleware.cors import CORSMiddleware

app = FastAPI(title="Ufunding")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Permitir a todos los dominios
    allow_credentials=True,
    allow_methods=["*"],  # Permitir todos los métodos (GET, POST, etc.)
    allow_headers=["*"],  # Permitir todos los encabezados
)

from app.api import route as api_router
app.include_router(api_router.router)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app,host="127.0.0.1", port=8000)