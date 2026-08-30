# note

## op

> 上游合并后目录已重构：`rhino_mcp_server/` → `server/`，`rhino_mcp_plugin/` → `plugin/`，版本 0.3.2

```bash
cd server
uv venv
.venv/Scripts/activate
uv pip install -e .

# 开发调试（等价于 ./dev.sh）
uv run mcp dev main.py:mcp
```

客户端配置（stdio，默认模式）：

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

### streamable-http

新版 `main()` 默认走 stdio。若需要之前定制的 streamable-http，在 `server/src/rhinomcp/server.py` 中修改：

```python
mcp = FastMCP("RhinoMCP", lifespan=server_lifespan, host="0.0.0.0", port=12003)

def main():
    mcp.run(transport="streamable-http")
```

对应客户端配置：

```json
{
  "mcpServers": {
    "rhino": {
      "url": "http://127.0.0.1:12003/mcp"
    }
  }
}
```

### plugin

```bash
# build with vstudio in release mod
cd <release folder>
yak build
yak install rhinomcp-0.3.2-rh8_17-any.yak
```

### pypi

```powershell
htpasswd -c .htaccess admin
#123123

docker run --name pypiserver -d  -p 8086:8080 -v e:\docker\pypiserver\packages:/data/packages -v e:\docker\pypiserver\.htaccess:/data/.htaccess pypiserver/pypiserver:v2.3.2   run -o -P /data/.htaccess /data/packages
```

### 同步上游

```bash
git fetch upstream
git merge upstream/main
```
