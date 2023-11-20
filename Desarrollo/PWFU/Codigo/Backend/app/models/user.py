from pydantic import BaseModel

# Modelos Pydantic
class UserCreate(BaseModel):
    user_name: str
    name: str
    surname: str
    email: str
    password: str

class UserResponse(BaseModel):
    id: int
    user_name: str
    name: str
    surname: str
    email: str
