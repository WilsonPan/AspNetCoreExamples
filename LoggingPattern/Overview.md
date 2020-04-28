# 概述
---
今天看AspNetCore源代码发现日志模块的设计模式（提供者模式），特此记录

设计模式的好处是，我们可以容易扩展它达到我们要求，除了要知道如何扩展它，还应该在其他地方应用它

若测试Mongodb，需要配置Mongodb链接，MongodbLoggingConnectionString

Rest/api.rest 需要插件REST Client测试

