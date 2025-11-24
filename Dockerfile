# ---- builder ----
FROM python:3.11-slim AS builder
WORKDIR /build

RUN apt-get update && apt-get install -y --no-install-recommends build-essential gcc && rm -rf /var/lib/apt/lists/*

COPY requirements.txt .
RUN python -m pip install --upgrade pip wheel \
    && pip wheel --no-deps --wheel-dir /wheels -r requirements.txt

# ---- runtime ----
FROM python:3.11-slim AS runtime
WORKDIR /app

RUN useradd -m appuser

COPY --from=builder /wheels /wheels
COPY requirements.txt .
RUN python -m pip install --upgrade pip \
    && pip install --no-index --find-links=/wheels -r requirements.txt \
    && rm -rf /wheels

COPY src/ ./src
ENV PYTHONPATH=/app/src

USER appuser
EXPOSE 8000

# Use Gunicorn for multiple workers in production (adjust worker count as needed)
CMD ["gunicorn", "src.main:app", "-k", "uvicorn.workers.UvicornWorker", "--bind", "0.0.0.0:8000", "--workers", "3"]