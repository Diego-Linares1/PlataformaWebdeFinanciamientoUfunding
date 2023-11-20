from fastapi import APIRouter, HTTPException
from typing import List

from ..models.user import UserCreate, UserResponse
from ..db.crud import *

router = APIRouter()

# Ruta para registrar un usuario
@router.post("/users/", response_model=UserResponse)
def create_user(user: UserCreate):
    db_user = crud_create_user(user)
    return UserResponse(id=db_user.id, user_name=db_user.user_name, name=db_user.name, surname=db_user.surname, email=db_user.email)

# Ruta para obtener la lista de usuarios
@router.get("/users/", response_model=List[UserResponse])
def get_users():
    return crud_get_users()

@router.get("/users/{user_id}", response_model=UserResponse)
def get_user_by_id(user_id: int):
    db_user = crud_get_user_by_id(user_id)
    if db_user is None:
        raise HTTPException(status_code=404, detail="Usuario no encontrado")
    return UserResponse(
        id=db_user.id,
        user_name=db_user.user_name,
        name=db_user.name,
        surname=db_user.surname,
        email=db_user.email
    )