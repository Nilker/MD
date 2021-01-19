## 1、安装Autofac

Nuget 安装Autofac

## 2、注入方式

### 构造函数注入(默认)

```c#

            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.RegisterType<XiaomiService>().As<IXiaomiService>();
            IContainer container = containerBuilder.Build();

            //解析
            var xiaomiService = container.Resolve<IXiaomiService>();

            xiaomiService.Open();
```

### 属性注入

```c#
var containerBuilder = new Autofac.ContainerBuilder();
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>();
containerBuilder.RegisterType<XiaomiService>().As<IXiaomiService>().PropertiesAutowired();//属性注入
IContainer container = containerBuilder.Build();

//解析
var xiaomiService = container.Resolve<IXiaomiService>();
xiaomiService.Open();
```

### 方法注入

```c#

 #region 方法注入
public IXiaoAiService funXiaoAiService = null;

public void SetService(IXiaoAiService iXiaoAiService)
{
    funXiaoAiService = iXiaoAiService;
} 
#endregion

var containerBuilder = new Autofac.ContainerBuilder();
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>();
containerBuilder.RegisterType<XiaomiService>()
    .OnActivated(s=>s.Instance.SetService(s.Context.Resolve<IXiaoAiService>()))//方法注入
    .As<IXiaomiService>();
IContainer container = containerBuilder.Build();

//解析
var xiaomiService = container.Resolve<IXiaomiService>();
xiaomiService.Open();
```

## 3、生命周期

```c#
//每次获取都是全新的实例
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>().InstancePerDependency();

//单例，进程中唯一的实例
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>().SingleInstance();

//生命周期 范围内，同一个实例
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>().InstancePerLifetimeScope();
...
using (container.BeginLifetimeScope())
{
    //生命周期范围内 唯一
}

//每个请求为一个实例 
containerBuilder.RegisterType<XiaoAiService>().As<IXiaoAiService>().InstancePerRequest();
```

## 4、Autofac 支持配置文件

- ### 引入程序集：

  ```c#
  Autofac.Extensions.DependencyInjection
  Autofac.Configuration
  Autofac
  ```

- ### 准备配置文件

  ```json
  {
    "defaultAssembly": "Autofac.Example.Calculator",
    "components": [{
      "type": "Autofac.Example.Calculator.Addition.Add, Autofac.Example.Calculator.Addition",
      "services": [{
        "type": "Autofac.Example.Calculator.Api.IOperation"
      }],
      "injectProperties": true, // 属性注入
      "instanceScope":"single-instance", //生命周期
    }, {
      "type": "实现类-全名称, 程序集",
      "services": [{
        "type": "接口类"
      }],
      "parameters": {
        "places": 4
      }
    }]
  }
  ```

- ### 载入配置文件

  ```c#
  
  var containerBuilder = new Autofac.ContainerBuilder();
  
  //获取配置资源
  IConfigurationBuilder config = new ConfigurationBuilder();
  JsonConfigurationSource autoConfigSource = new JsonConfigurationSource() {Path = "config/autofac.json"};
  config.Add(autoConfigSource);
  
  var module = new Autofac.Configuration.ConfigurationModule(config.Build());
  
  containerBuilder.RegisterModule(module);
  var container = containerBuilder.Build();
  
  //解析
  var xiaomiService = container.Resolve<IXiaomiService>();
  xiaomiService.Open();
  ```

  

## 5、.net core 整合

官网地址：https://autofaccn.readthedocs.io/zh/latest/integration/aspnetcore.html

### program.cs

```c#
 public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //启用 autofac 工厂
            .UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory());
    }
```

### StartUp.cs

```c#
//整个方法 会被 autofac自动调用； 负责注册服务，当然ConfigureServices 注册的服务也会有效

//会接管ServiceCollection 中的服务注册；
// ConfigureContainer is where you can register things directly
  // with Autofac. This runs after ConfigureServices so the things
  // here will override registrations made in ConfigureServices.
  // Don't build the container; that gets done for you by the factory.
  public void ConfigureContainer(ContainerBuilder builder)
  {
    // Register your own things directly with Autofac, like:
    builder.RegisterModule(new MyApplicationModule());
  }

```

### 控制器中支持属性注入-使用Autofac

控制器是一个类，控制器的实例创建是通过**IControllerActivator** 创建的；

让控制器 使用容器来获取实例；

```c#
//为了使用 控制器属性注入，将控制器的创建【IControllerActivator】替换为【ServiceBasedControllerActivator】
services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

 //这样注册不够，因为属性比较多，不可能全部都属性注入，有问题
//containerBuilder.RegisterType<ValuesController>().As<ControllerBase>();

  //注册所有控制器的关系+
var contrllerTypes = typeof(Startup).Assembly.GetExportedTypes()
    .Where(s=>typeof(ControllerBase).IsAssignableFrom(s)).ToArray();

 //属性注入+自定义需要注入的属性
containerBuilder.RegisterTypes(contrllerTypes).PropertiesAutowired(new IocAop.Ext.AutowiredPropertySelector());
```

```c#
public class AutowiredPropertySelector : Autofac.Core.IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            var b = propertyInfo.CustomAttributes.Any(s => s.AttributeType == typeof(AutowirdAttribute));
            return b;
        }
    }




    /// <summary>
    /// 属性注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class AutowirdAttribute : Attribute
    {
    }
```

## 6、Autofac 抽象多实现 问题

1. 一个抽象多个实例， 后面会覆盖前面的注册；

2. 多实例时，可以集合获取：

    public IEnumerable<IXiaomiService> XiaomiServices { get; set; } or 构造函数 集合

3. 一个抽象多实现：

   ```c#
    //单抽象多实现
   containerBuilder.RegisterSource(
                   new AnyConcreteTypeNotAlreadyRegisteredSource(s => s.IsAssignableTo<IXiaomiService>()));
   
   //具体实现类型
     [Autowird] public XiaomiService mi { get; set; }
     [Autowird] public XiaomiService2 mi2 { get; set; }
   ```

   

4. 注册整理 通过module进行注册

   ```c#
   containerBuilder.RegisterModule< IocAop.Ext.AutofacModule>();
   
   public class AutofacModule:Autofac.Module
       {
           protected override void Load(ContainerBuilder builder)
           {
               base.Load(builder);
           }
       }
   ```




## 7、Autofac支持AOP

AOP切面编程：不改代码的前提下，在方法进入前，或 执行后，动态添加一些功能，如日志；





