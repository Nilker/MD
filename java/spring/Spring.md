## Spring是什么？

Spring 是解决企业级应用开发的复杂性问题，即简化 java开发；

Spring 底层 两个核心特性

​	依赖注入：（Dependency Injection ,DI)

​	切面编程：（aspect-oriented programming ,AOP)



## Spring的优缺点是什么？

### 优点

- 方便解耦，简化开发

  Spring就是一个大工厂，可以将所有对象的创建和依赖关系的维护，交给Spring管理。

- AOP编程的支持

  Spring提供面向切面编程，可以方便的实现对程序进行权限拦截、运行监控等功能。

- 声明式事务的支持

  只需要通过配置就可以完成对事务的管理，而无需手动编程。

- 方便程序的测试

  Spring对Junit4支持，可以通过注解方便的测试Spring程序。

- 方便集成各种优秀框架

  Spring不排斥各种优秀的开源框架，其内部提供了对各种优秀框架的直接支持（如：Struts、Hibernate、MyBatis等）。

- 降低JavaEE API的使用难度

  Spring对JavaEE开发中非常难用的一些API（JDBC、JavaMail、远程调用等），都提供了封装，使这些API应用难度大大降低。

### 缺点

- Spring明明一个很轻量级的框架，却给人感觉大而全
- Spring依赖反射，反射影响性能
- 使用门槛升高，入门Spring需要较长时间

## Spring 框架中用到的设计模式

- 工厂模式：BeanFactory就是简单的工厂模式体现，用来创建对象实例











## BeanFactory，ApplicationContext，FactoryBean 区别[(阿里面试)](https://www.cnblogs.com/aspirant/p/9082858.html)

*BeanFactory*：是个接口，**是IOC容器的核心接口，它的职责包括：实例化、定位、配置应用程序中的对象及建立这些对象间的依赖**；

*ApplicationContext*：接口,它由BeanFactory接口派生而来，ApplicationContext包含BeanFactory的所有功能，优先使用BeanFactory

​	还提供以下功能：

​		MessageSource：国际化消息访问

​		ResourcePatternResolver：资源访问，如URL和文件；

​		事件传播；

*FactoryBean*:也就是工厂 bean，我们通过 `getBean(id)` 获得是该工厂所产生的 Bean 的实例，而不是该 `FactoryBean` 的实例，功能貌似更像是一种代理

​		