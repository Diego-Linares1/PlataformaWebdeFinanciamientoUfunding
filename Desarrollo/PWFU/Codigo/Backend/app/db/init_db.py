from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from sqlalchemy import create_engine, Column, ForeignKey, Integer, String, BLOB, DECIMAL, DateTime, Text

# Configuración de la base de datos

DATABASE_URL = "sqlite:///./ufunding.db"
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()

# Declaración de la tablas

class StudyCenter(Base):
    __tablename__ = "study_center"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    name = Column(String)
    email_domain = Column(String, unique=True)

class StudyCenterSpeciality(Base):
    __tablename__ = "speciality"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    name = Column(String)
    study_center_id = Column(Integer, ForeignKey('study_center.id'))

class StudentInfo(Base):
    __tablename__ = "student_info"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    speciality_id = Column(Integer, ForeignKey('speciality.id'))
    year_of_entry = Column(Integer)
    semester = Column(Integer)

class User(Base):
    __tablename__ = "user"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    user_name = Column(String, unique=True, index=True, nullable=False)
    name = Column(String, nullable=False)
    surname = Column(String, nullable=False)
    email = Column(String, unique=True, nullable=False)
    password_hash = Column(String, nullable=False)
    about = Column(String)
    phone = Column(String)
    dni = Column(String)
    age = Column(Integer)
    student_info_id = Column(Integer, ForeignKey('student_info.id'), nullable=True)

class ProjectHistory(Base):
    __tablename__ = "project_history"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    history = Column(Text)
    image = Column(BLOB)

class Project(Base):
    __tablename__ = "project"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    name = Column(String, unique=True, nullable=False)
    image = Column(BLOB)
    goal = Column(DECIMAL, nullable=False)
    deadline = Column(DateTime, nullable=False)
    bank_account_number = Column(String, nullable=False)
    email_contact = Column(String, nullable=False)
    project_history_id = Column(Integer, ForeignKey('project_history.id'), nullable=True)

class Category(Base):
    __tablename__ = "category"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    name = Column(String, unique=True, nullable=False)

class ProjectCategory(Base):
    __tablename__ = "project_category"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey('project.id'), nullable=False)
    category_id = Column(Integer, ForeignKey('category.id'), nullable=False)

class Reward(Base):
    __tablename__ = "reward"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    proyect_id = Column(Integer, ForeignKey('project.id'), nullable=False)
    amount = Column(DECIMAL)
    description = Column(Text, nullable=False)
    image = Column(BLOB)

class Colaborators(Base):
    __tablename__ = "colaborators"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    proyect_id = Column(Integer, ForeignKey('project.id'), nullable=False)
    user_id = Column(Integer, ForeignKey('user.id'), nullable=False)

class PaymentMethod(Base):
    __tablename__ = "payment_method"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    name = Column(String, unique=True, nullable=False)

class Donation(Base):
    __tablename__ = "donation"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    proyect_id = Column(Integer, ForeignKey('project.id'), nullable=False)
    user_id = Column(Integer, ForeignKey('user.id'), nullable=False)
    amount = Column(DECIMAL, nullable=False)
    date = Column(DateTime, nullable=False)
    payment_method_id = Column(Integer, ForeignKey('payment_method.id'), nullable=False)

class Comment(Base):
    __tablename__ = "comment"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    proyect_id = Column(Integer, ForeignKey('project.id'), nullable=False)
    user_id = Column(Integer, ForeignKey('user.id'), nullable=False)
    text = Column(Text, nullable=False)
    created_at = Column(DateTime, nullable=False)

class CommentResponse(Base):
    __tablename__ = "comment_response"
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    commentary_id = Column(Integer, ForeignKey('comment.id'), nullable=False)
    user_id = Column(Integer, ForeignKey('user.id'), nullable=False)
    text = Column(Text, nullable=False)
    created_at = Column(DateTime, nullable=False)

Base.metadata.create_all(bind=engine)