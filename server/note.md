# note

## op

### server

```bash
python -m venv .venv
.venv\Scripts\pip install uv
.venv\Scripts\uv run rhinomcp

uv venv
.venv/Scripts/activate
uv pip install -e .

# 开发调试（等价于 ./dev.sh）
uv run mcp dev main.py:mcp
```
