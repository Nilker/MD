# 一、介绍

AOP：动态代理

​	指在程序运行期间动态的将某段代码切入到指定方法指定位置，进行运行的编程方式；

# 1、导入aop模块

Spring Aop     spring-aspects包

```xml
<dependency>
    <groupId>org.springframework</groupId>
    <artifactId>spring-aspects</artifactId>
    <version>5.1.3.RELEASE</version>
</dependency>

```

# 2、定义一个业务逻辑类（MathCalculator）

将日志进行打印，传入参数，输出参数，异常日志

```java

/**
 * 计算器
 * */

public class MathCalculator {
    public int div(int i,int j){
        System.out.println("MathCalculator---div---i="+i+",j="+j);
        return i/j;
    }
}
```



# 3、定义日志切面 类 （LogAspects），切面类里面的方法需要动态感知MathCalculator.div运行

## 通知方法：

​	前置通知(`@Before`)：logStart

​	后置通知(`@After`)：logEnd

​	返回通知(`@AfterReturning`)：logReturn

​	异常通知(`@AfterThrowing`)：logException

​	环绕通知(`@Around`)：动态代理，手动推动目标方法运行（joinPoint.procced());



# 4、给切面类的目标方法标注 何时何地（通知注解）

```java

/**
 * log切面
 * */
@Aspect //切面类
public class LogAspects {

    //抽取公共的切入点表达
    //1.类引用
    //2、其他的切面引用
    @Pointcut("execution(public int  com.lhl.aop.MathCalculator.*(..))")
    public void pointCut(){ }

    //@Before 在目标方法之前进行切入，切入点表达式（指定哪个方法切入public int  com.lhl.aop.MathCalculator. *（..））
    @Before("execution(public int  com.lhl.aop.MathCalculator.*(..))")
    //@Before("pointCut()")
    public void logStart(JoinPoint joinPoint){
        Object[] args = joinPoint.getArgs();
        System.out.println("@Before----除法运行。。。"+joinPoint.getSignature().getName()+"方法；参数列表是：{"+ Arrays.asList(args) +"}");
    }

    //@After() 执行成功，失败，都会执行
    @After("pointCut()")
    public void logEnd(JoinPoint joinPoint){
        System.out.println("@After----除法结束。。。方法名："+joinPoint.getSignature().getName());
    }

    @AfterReturning(value = "pointCut()",returning = "result")
    public void  logReturn(Object result){
        System.out.println("@AfterReturning----除法正常返回，运行结果：{"+result+"}");
    }

    @AfterThrowing(value = "pointCut()",throwing = "ex")
    public void logException(JoinPoint joinPoint,Exception ex){//JoinPoint 一定出现在参数表的 第一位
        System.out.println("@AfterThrowing----"+joinPoint.getSignature().getName()+"方法名；除法异常，异常信息：{"+ex.getLocalizedMessage()+"}");
    }
}
```



# 5、将切面类和业务逻辑类（目标方法所在类），都加入到容器中；

# 6、必须告诉Spring哪个是切面类（切面类加注解：`@Aspect`）

# 7、配置类中加`@EnableAspectJAutoProxy`开启基于注解的Aop模式

```xml
<!--开启基于xml的切面类-->
<aop:aspectj-autoproxy></aop:aspectj-autoproxy>
```

```java
@EnableAspectJAutoProxy
@Configuration
public class MainConfigOfAOP {

    //业务逻辑类 加入到容器中
    @Bean
    public MathCalculator calculator(){
        return new MathCalculator();
    }

    //切面类加入容器中
    @Bean
    public LogAspects logAspects(){
        return new LogAspects();
    }
}

```

测试：

```java
 @Test
    public void test01(){
        AnnotationConfigApplicationContext applicationContext = new AnnotationConfigApplicationContext(MainConfigOfAOP.class);

//        //不要自己创建对象
//        MathCalculator mathCalculator = new MathCalculator();
//        mathCalculator.div(1,1);

        MathCalculator mathCalculator = applicationContext.getBean(MathCalculator.class);
        mathCalculator.div(1,0);

        applicationContext.close();
    }
```

输出：

```txt
@Before----除法运行。。。div方法；参数列表是：{[1, 0]}
MathCalculator---div---i=1,j=0
@After----除法结束。。。方法名：div
@AfterThrowing----div方法名；除法异常，异常信息：{/ by zero}
```

# 二、总结

动态代理 三步：

## 1、将`业务逻辑组件`和`切面类`都加入到容器中，告诉Spring哪个是切面类（`@Aspect`）

## 2、在`切面类`上的每一个通知方法上标注`通知注解`，告诉Spring何时运行（`切入点表达式`）

## 3、开启基于注解的`Aop`模式，`@EnableAspectJAutoProxy`