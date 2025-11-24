from fastapi.testclient import TestClient
from src.main import app

client = TestClient(app)

def test_add():
    response = client.get("/add", params={"a": 3, "b": 5})
    assert response.status_code == 200
    assert response.json() == {"result": 8}

def test_subtract():
    response = client.get("/subtract", params={"a": 10, "b": 4})
    assert response.status_code == 200
    assert response.json() == {"result": 6}

def test_multiply():
    response = client.get("/multiply", params={"a": 6, "b": 7})
    assert response.status_code == 200
    assert response.json() == {"result": 42}

def test_divide():
    response = client.get("/divide", params={"a": 20, "b": 5})
    assert response.status_code == 200
    assert response.json() == {"result": 4}

def test_divide_by_zero():
    response = client.get("/divide", params={"a": 10, "b": 0})
    assert response.status_code == 200
    assert response.json() == {"error": "Cannot divide by zero"}

def test_power():
    response = client.get("/power", params={"a": 2, "b": 3})
    assert response.status_code == 200
    assert response.json() == {"result": 8}

def test_modulo():
    response = client.get("/modulo", params={"a": 10, "b": 3})
    assert response.status_code == 200
    assert response.json() == {"result": 1}

def test_modulo_by_zero():
    response = client.get("/modulo", params={"a": 10, "b": 0})
    assert response.status_code == 200
    assert response.json() == {"error": "Cannot modulo by zero"}

def test_average():
    response = client.get("/average", params={"a": 10, "b": 20})
    assert response.status_code == 200
    assert response.json() == {"result": 15}
