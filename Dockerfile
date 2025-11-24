# Use official Python 3.11 image
FROM python:3.11-slim

# Set environment variables
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Set working directory
WORKDIR /app

# Copy requirements if you have a requirements.txt
# Otherwise install packages directly
COPY requirements.txt .
RUN pip install --upgrade pip \
    && pip install --no-cache-dir -r requirements.txt

# Install dependencies
RUN pip install --upgrade pip \
    && pip install --no-cache-dir -r requirements.txt

# Copy application code
COPY src/ ./src
COPY tests/ ./tests

# Set PYTHONPATH
ENV PYTHONPATH=/app/src

# Expose port for FastAPI
EXPOSE 8000

# Default command to run FastAPI
CMD ["uvicorn", "src.main:app", "--host", "0.0.0.0", "--port", "8000"]
