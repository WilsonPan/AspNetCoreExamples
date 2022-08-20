# .NET常用脚手架

在学习一个新技术/新框架的时候，总是会先想到是否有脚手架。脚手架的作用是帮我们初始化大量重复性，基础性的动作，让我们可以专心在实际开发业务上，而不必浪费大量时间在项目搭建上。

但是任何事都有利有弊，如果长期依赖于脚手架去创建项目而不去了解它底层生成的东西，时间长容易忽略或遗忘项目基础东西，所以在使用脚手架便利的同时（时间忙的时候），也要弄清楚脚手架创建原理（时间空闲的时候）

下面介绍Dotnet 常用脚手架

## dotnet new
> 根据指定的模板，创建新的项目、配置文件或解决方案

```sh
dotnet new --list                           # 列出可创建模板
dotnet new --list web                       # 筛选符合条件模板
dotnet new <template> --search              # 在nuget.org搜索符合模板
dotnet new --install <path|nuget_id>        # 安装模板
dotnet new --uninstall <path|nuget_id>      # 卸载模板
dotnet new --update-check                   # 检查已安装的模板包的可用更新
dotnet new --update-apply                   # 将更新应用于已安装的模板包
dotnet new <template> -h                    # 查看对应模板参数
dotnet new console                          # 创建控制台程序
dotnet new console -o demo                  # 在指定目录创建控制台程序
```

## dotnew build
> 生成项目及其所有依赖项

```sh
dotnet build                                # 生成项目及其依赖项
dotnet build --configuration Release        # 使用Release生成项目及其依赖项
dotnet build --runtime ubuntu.18.04-x64     # 针对特定运行时生成项目及其依赖项
```

## dotnet clean
> 清除项目输出

```sh
dotnet clean                                # 清除项目的默认生成
dotnet clean --configuration Release        # 清除使用版本配置生成的项目
```

## dotnet format
> 格式化代码

```sh
dotnet format ./solution.sln                                        # 格式化解决方案所有代码文件
dotnet format ./application.csproj                                  # 格式化项目所有代码文件
dotnet format --verify-no-changes                                   # 验证但不改变，会列出需要格式化文件
dotnet format --include ./src/                                      # 格式化指定目录代码文件
dotnet format --include ./src/ --exclude ./src/submodule-a/         # 格式化src/文件夹下文件但排除submodule-a/文件夹
```

## dotnet restore
> 恢复项目的依赖项和工具。

```sh
dotnet restore                                  # 还原当前目录中项目的依赖项
dotnet restore ./project.csproj                 # 还原指定项目的依赖项
dotnet restore -s /.nuget                       # 使用指定源还原依赖性
```

## dotnet run
> 运行项目

```sh
dotnet run                                      # 运行当前目录项目
dotnet run --project ./projects.csproj          # 运行指定项目
dotnet run --property:Configuration=Release     # 运行Release模式
```

## dotnet test
> 执行单元测试

```sh
dotnet test                                     # 运行当前目录所含项目中的测试
dotnet test ~/projects.csproj                   # 运行指定项目单元测试
dotnet test --logger trx                        # 运行单元测试并生成trx格式测试结果
```

## dotnet watch
> 检测到源代码中的更改时重启或热重载指定的应用程序

```sh
dotnet watch run                                # 热启动项目
dotnet watch run --project ./project.csproj     # 热启动指定项目
```

## dotnet tool
> .Net工具管理

```sh
dotnet tool list --global                       # 列出全局安装工具
dotnet tool list --local                        # 列出当前目录安装工具
dotnet tool search <name>                       # 搜索工具
dotnet tool install                             # 当前目录安装工具
dotnet tool install -g <name>                   # 全局安装工具
dotnet tool install -g <name> --version 2.0.0   # 安装指定版本工具
dotnet tool update <name>                       # 更新指定工具
```

> Net常用工具

| package id      | 描述                        |
| --------------- | --------------------------- |
| dotnet-format   | 代码格式工具                |
| dotnet-ef       | Entity Framework Core tools |
| dotnet-dump     | 捕获dump文件于分析          |
| dotnet-trace    | 性能分析工具                |
| dotnet-counters | 性能监控工具                |
| dotnet-gcdump   | 堆分析工具                  |
| dotnet-monitor  | 诊断监控和收集实用程序      |
