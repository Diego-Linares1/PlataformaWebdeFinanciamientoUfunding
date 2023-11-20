from fastapi import HTTPException
from sqlalchemy.exc import IntegrityError
from .init_db import SessionLocal, User
from ..models.user import UserCreate, UserResponse
import bcrypt

db = SessionLocal()

def crud_create_user(user: UserCreate) -> User :
    hashed_password = bcrypt.hashpw(user.password.encode('utf-8'), bcrypt.gensalt())
    db_user = User(user_name=user.user_name, name=user.name, surname=user.surname, email=user.email, password_hash=hashed_password)
    try:
        db.add(db_user)
        db.commit()
        db.refresh(db_user)
    except IntegrityError as e:
        db.rollback()
        raise HTTPException(status_code=400, detail="Error de unicidad en nombre de usuario o correo")
    except Exception as e:
        # Manejar otras excepciones de base de datos
        db.rollback()
        raise HTTPException(status_code=500, detail="Error interno del servidor")
    finally:
        db.close()
    return db_user

def crud_get_users() -> list[UserResponse]:
    try:
        users = db.query(User).all()
        user_list = [UserResponse(id=user.id, user_name=user.user_name, name=user.name, surname=user.surname, email=user.email) for user in users]
    finally:
        db.close()
    return user_list

def crud_get_user_by_id(user_id: int) -> User:
    return db.query(User).filter(User.id == user_id).first()