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

