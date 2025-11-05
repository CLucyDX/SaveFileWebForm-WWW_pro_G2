# SaveFileWebForm & WWW_pro_G2

本项目是一个结合 **ASP.NET Web 应用** 与 **FRP 内网穿透工具** 的综合系统，旨在提供：
- **文件上传与管理** 的网页端解决方案；
- **本地服务公网访问** 的支持；
- 可扩展的 Web 服务基础架构，适合个人开发者或小型团队部署与扩展。

---

## 项目总体结构

SaveFileWebForm/        # C# 编写的 ASP.NET Web 应用
 ├─ Controllers/        # MVC 控制器
 ├─ Views/              # 页面视图
 ├─ WebService/         # WebForm 服务（文件上传接口）
 ├─ Content/ Scripts/   # 前端资源
 ├─ App_Data/ File/     # 文件存储目录
 ├─ Global.asax         # 应用启动入口
 ├─ Web.config          # ASP.NET 配置文件
 └─ SaveFileWebForm.sln # 解决方案文件

WWW_pro_G2/             # FRP 客户端配置目录
 ├─ frpc.exe            # FRP 客户端主程序
 ├─ frpc.ini            # 内网穿透配置文件
 ├─ strat.bat           # 一键启动脚本
 └─ favicon.ico         # 图标

---

## 核心功能

### 1. SaveFileWebForm — 文件管理与上传 Web 系统
基于 **C# + ASP.NET MVC + WebForms** 架构构建，支持文件上传、保存与访问。

**功能特性：**
- 支持通过 Web 表单上传文件；
- 将文件保存至服务器指定目录（如 /File/ 或 App_Data/）；
- MVC + WebForms 混合架构，兼顾现代路由与传统页面；
- 结构清晰，便于二次开发；
- 支持 Web.config 中的安全与版本控制机制。

**运行环境：**
- Windows 10 / 11 / Server
- Visual Studio 2017+  
- .NET Framework 4.7.2  
- IIS Express / IIS 服务器  
- 无需数据库依赖（默认直接保存文件到磁盘）

---

### 2. WWW_pro_G2 — FRP 内网穿透客户端
该部分用于将本地网站（如 SaveFileWebForm）暴露至公网访问。

**核心文件说明：**
- frpc.exe：FRP 客户端程序；
- frpc.ini：配置文件，指定服务器与端口；
- strat.bat：启动脚本，执行 frpc.exe -c frpc.ini；
- 支持自动启动与后台常驻运行。

**典型配置（frpc.ini 示例）：**
[common]
server_addr = <你的FRP服务器IP>
server_port = 7000

[web]
type = http
local_port = 8080
custom_domains = <你的域名>

通过该配置，你可以让本地运行的 Web 项目在公网被访问，实现轻量级的云端托管与演示。

---

## 项目优点

| 类别 | 优势说明 |
|------|-----------|
| 架构简洁 | 项目基于 MVC 与 WebForm 混合结构，兼容性强，学习成本低。 |
| 部署灵活 | 可在 IIS、本地调试器或通过 FRP 映射至公网访问。 |
| 功能可扩展 | 支持新增 Web API 接口、数据库存储、身份验证等。 |
| 安全控制 | 可利用 ASP.NET 自带过滤器与配置文件控制访问权限。 |
| 实用性强 | 适合在局域网快速搭建文件服务、上传平台或演示网站。 |

---

## 项目发展方向

| 阶段 | 发展方向 |
|------|-----------|
| 阶段一（当前） | 文件上传与存储服务（已完成基础功能） |
| 阶段二 | 增加前端文件列表、下载功能与数据库记录 |
| 阶段三 | 提供 Web API 接口，实现 RESTful 上传访问 |
| 阶段四 | 集成云存储（如阿里云 OSS / AWS S3）及多用户管理 |
| 阶段五 | 支持登录认证与访问权限控制，打造完整云文件平台 |

---

## 开发与部署指南

### 运行 SaveFileWebForm

1. 在 Windows 上打开：
   SaveFileWebForm.sln

2. 使用 Visual Studio 自动恢复 NuGet 包。
3. 运行项目（F5）或发布到 IIS。
4. 若要从公网访问，可使用 FRP 客户端（见下方）。

### 启动 FRP 客户端
在 WWW_pro_G2 文件夹下执行：
strat.bat

系统将自动连接远程 FRP 服务器，并开放映射端口。

---

## 系统依赖

| 组件 | 版本 / 说明 |
|------|--------------|
| .NET Framework | 4.7.2 |
| Visual Studio | 2017 / 2019 / 2022 |
| IIS / IIS Express | 支持 ASP.NET 4.x |
| FRP | 客户端 0.51+ |
| Windows | 7 / 10 / 11 / Server 均可 |

---

## 未来计划

- 增加文件管理前端界面（Vue / React）；
- 集成 SignalR 实现实时上传进度；
- 支持 REST API 方式上传与下载；
- 可选数据库支持（SQL Server / SQLite）；
- 提供 Docker 镜像部署方案；
- 通过 FRP 或 Ngrok 一键公网访问。

---

## License

本项目用于学习与研究目的，可自由扩展和二次开发。  
