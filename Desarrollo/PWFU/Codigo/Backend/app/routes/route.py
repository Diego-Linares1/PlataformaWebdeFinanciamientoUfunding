from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from typing import List
import bcrypt  # Asegúrate de que bcrypt esté instalado

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
    password = Column(String)  # Agregar campo de contraseña

Base.metadata.create_all(bind=engine)

# Modelos Pydantic
class UserCreate(BaseModel):
    username: str
    email: str
    password: str  # Agregar campo de contraseña

class UserResponse(BaseModel):
    id: int
    username: str
    email: str

# Creación de la aplicación FastAPI
app = FastAPI()

# Ruta para registrar un usuario
@app.post("/users/", response_model=UserResponse)
def create_user(user: UserCreate):
    db = SessionLocal()
    
    # Hashear la contraseña antes de guardarla en la base de datos
    hashed_password = bcrypt.hashpw(user.password.encode('utf-8'), bcrypt.gensalt())
    
    db_user = User(username=user.username, email=user.email, password=hashed_password.decode('utf-8'))
    db.add(db_user)
    db.commit()
    db.refresh(db_user)
    db.close()
    
    return UserResponse(id=db_user.id, username=db_user.username, email=db_user.email)

# Ruta para obtener la lista de usuarios
@app.get("/users/", response_model=List[UserResponse])
def get_users():
    db = SessionLocal()
    users = db.query(User).all()
    db.close()
    user_list = [UserResponse(id=user.id, username=user.username, email=user.email) for user in users]
    
    return user_list

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
