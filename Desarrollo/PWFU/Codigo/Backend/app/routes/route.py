from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from typing import List

# Configuración de la base de datos
DATABASE_URL = "sqlite:///./test.db"
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

# Declaración de la tabla de usuarios
Base = declarative_base()

class User(Base):
    __tablename__ = "users"
    id = Column(Integer, primary_key=True, index=True)
    username = Column(String, unique=True, index=True)
    email = Column(String, unique=True, index=True)

Base.metadata.create_all(bind=engine)

# Modelo Pydantic para la creación de usuarios
class UserCreate(BaseModel):
    username: str
    email: str

# Modelo Pydantic para la respuesta de usuarios (GET)
class UserResponse(BaseModel):
    id: int
    username: str
    email: str

# Creación de la aplicación FastAPI
app = FastAPI()

# Ruta para registrar un usuario (POST)
@app.post("/users/", response_model=UserCreate)
def create_user(user: UserCreate):
    db = SessionLocal()
    db_user = User(**user.dict())
    db.add(db_user)
    db.commit()
    db.refresh(db_user)
    db.close()
    return user

# Ruta para obtener la lista de usuarios (GET)
@app.get("/users/", response_model=List[UserResponse])
def get_users():
    db = SessionLocal()
    users = db.query(User).all()
    db.close()
    # Transformar objetos SQLAlchemy en diccionarios
    user_list = [{"id": user.id, "username": user.username, "email": user.email} for user in users]
    return user_list

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
