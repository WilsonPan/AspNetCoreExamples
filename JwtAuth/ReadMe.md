# 概述
---
认证授权是很多系统的基本功能 , 在以前PC的时代 , 通常是基于cookies-session这样的方式实现认证授权 , 在那个时候通常系统的用户量都不会很大, 所以这种方式也一直很好运行, 随着现在都软件用户量越来越大, 系统架构也从以前垂直扩展(增加服务器性能) -> 水平扩展(增加服务器数量) 

**cookies-session 工作方式**

客户端提交用户信息 -> 服务器识别用户 -> 服务端保存用户信息 -> 返回session-id客户端 -> 客户端保存session-id  -> 每次请求cookies带上session-id

这种方式也不是不能水平扩展 , 例如 , session复制/第三方保存session(数据库 , Redis) 

**名词解析**

认证 : 识别用户是否合法

授权: 赋予用户权限 (能访问哪些资源)

鉴权: 鉴定权限是否合法

## Jwt优势与劣势

**优势**

1. 无状态
> token 存储身份验证所有信息 , 服务端不需要保存用户身份验证信息, 减少服务端压力 , 服务端更容易水平扩展, 由于无状态, 又会导致它最大缺点 , 很难注销

2. 支持跨域访问
> Cookie是不允许垮域访问的，token支持

3. 跨语言
> 基于标准化的 JSON Web Token (JWT) , 不依赖特定某一个语言 , 例如生成对Token可以对多个语言使用(Net , Java , PHP ...)

**劣势**

1. Token有效性问题
> 后台很难注销已经发布的Token , 通常需要借助第三方储存(数据库/缓存) 实现注销, 这样就会失去JWT最大的优势 

2. 占带宽
> Token长度(取决存放内容) 比session_id大 , 每次请求多消耗带宽 , token只存必要信息 , 避免token过长

3. 需要实现续签
> cookies - session 通常是框架已经实现续签功能, 每次访问把过期时间更新, JWT需要自己实现, 参考OAuth2刷新Token机制实现刷新Token

4. 消耗更多CPU
> 每次请求需要对内容解密和验证签名这两步操作，典型用时间换空间

只能根据自身使用场景决定使用哪一种身份验证方案 , 没有一种方案是通用的,完美的 

# AspNetCore集成Jwt认证

1. 添加包
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

2. 添加配置
```json
"JwtOptions": {
    "Issuer": "https://localhost:5001",
    "Audience": "https://localhost:5001",
    "SecurityKey": "1G3l0yYGbOINId3A*ioEi4iyxR7$SPzm"
}
```

3. Jwt Bearer 扩展（选项）
```cs
public static AuthenticationBuilder AddJwtBearer(this IServiceCollection services, Action<JwtOptions> configureOptions)
{
    if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

    var jwtOptions = new JwtOptions()
    {
        Issuer = "Jwt Authentication",
        Audience = "Wilson Pan Web Api",
    };
    // set customs optoins
    configureOptions(jwtOptions);

    // update Options 
    services.PostConfigure<JwtOptions>(options =>
    {
        options.Issuer = jwtOptions.Issuer;
        options.Audience = jwtOptions.Audience;
        options.SecurityKey = jwtOptions.SecurityKey;
    });

    return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = jwtOptions.Issuer,
                            ValidAudience = jwtOptions.Audience,
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = jwtOptions.SymmetricSecurityKey
                        };
                    });

}
```

4. ConfigureServices

```cs
services.AddJwtBearer(options =>
{
    options.Issuer = Configuration.GetValue<string>("JwtOptions:Issuer");
    options.Audience = Configuration.GetValue<string>("JwtOptions:Audience");
    options.SecurityKey = Configuration.GetValue<string>("JwtOptions:SecurityKey");
});
```

5. Configure
```cs
app.UseAuthentication();
app.UseAuthorization();
```

6. add AuthorizeController
```cs
//define claim 
var claims = new Claim[]
{
    new Claim(ClaimTypes.Name, username),
    new Claim(ClaimTypes.Email, $"{username}@github.com"),
    new Claim(ClaimTypes.Role, username == "WilsonPan" ? "Admin" : "Reader"),
    new Claim(ClaimTypes.Hash, JwtHashHelper.GetHashString($"{username}:{password}:{System.DateTime.Now.Ticks}")),
};

//define JwtSecurityToken
var token = new JwtSecurityToken(
    issuer: _jwtOptions.Issuer,
    audience: _jwtOptions.Audience,
    claims: claims,
    expires: System.DateTime.Now.AddMinutes(5),
    signingCredentials: _jwtOptions.SigningCredentials
);

// generate token
var result = new JwtSecurityTokenHandler().WriteToken(token);
```

7. Contrller/Action 添加认证授权
```cs
[ApiController]
[Authorize]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    ...
}

[HttpPost]
[Authorize(Roles = "Admin")]
public IActionResult Post()
{
    return Ok();
}
```


