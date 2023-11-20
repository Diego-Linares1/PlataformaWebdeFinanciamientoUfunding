from typing import List
from fastapi import APIRouter, HTTPException
from ..db.init_db import SessionLocal, User
from ..models.user import UserCreate, UserResponse

router = APIRouter()
db = SessionLocal()

# Ruta para registrar un usuario
@router.post("/users/", response_model=UserResponse)
def create_user(user: UserCreate):
    
    # Hashear la contraseña antes de guardarla en la base de datos
    hashed_password = bcrypt.hashpw(user.password.encode('utf-8'), bcrypt.gensalt())
    db_user = User(user_name=user.user_name, name=user.name, surname=user.surname, email=user.email, password_hash=hashed_password)
    db.add(db_user)
    db.commit()
    db.refresh(db_user)
    db.close()
    
    return UserResponse(id=db_user.id, username=db_user.user_name, email=db_user.email)

# Ruta para obtener la lista de usuarios
@router.get("/users/", response_model=List[UserResponse])
def get_users():
    users = db.query(User).all()
    db.close()
    user_list = [UserResponse(id=user.id, username=user.user_name, name=user.name, surname=user.surname, email=user.email) for user in users]
    return user_list


