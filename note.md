# note

## op

```bash
uv venv
.venv/Scripts/activate
uv pip install -e .

uv run mcp dev main.py:mcp
```

```json
{
  "mcpServers": {
    "rhino": {
      "command": "uvx",
      "args": [
        "rhinomcp"
      ]
    }
  }
}
```

### plugin

```bash
# build with vstudio in release mod
cd <release folder>
yak build
yak install rhinomcp-0.1.3.4-rh8_17-any.yak
```

### pypi

```powershell
htpasswd -c .htaccess admin
#123123

docker run --name pypiserver -d  -p 8086:8080 -v e:\docker\pypiserver\packages:/data/packages -v e:\docker\pypiserver\.htaccess:/data/.htaccess pypiserver/pypiserver:v2.3.2   run -o -P /data/.htaccess /data/packages
```
