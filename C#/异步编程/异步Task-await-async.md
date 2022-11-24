![异步流程跟踪](img/navigation-trace-async-program.png)

## 1、async函数的编译方式

async/await 是C# 5.0 退出的异步代码编程模型，其本质是编译为**状态机**，	



![开发代码](img/开发代码.png)

![image-20201231111342895](img/结果.png)

反编译C# 源码，会将async 和await 语法糖去掉；

![IL源码](img/状态机.png)

**调用线程**：

- 初始化【AsyncVoidMethodBuilder.Create()】，并将状态：State=-1；
- 下图执行MoveNext()，由于State为-1；num为-1，执行if语句块内容，
  - 执行**调用线程**输出"ddd5 "
  - 声明Action委托，然后，Task.Run(action) 得到GetAwaiter
  - awaiter.IsCompleted,肯定是没有完成；
  - 重置状态机状态，替换awaiter，递归调用
  - **调用线程**return
- **调用线程**执行后续代码：“aaa2”



![image-20201231105121472](img/TaskRun.png)
![多线程，异步 Task，Async Await (2)](https://user-images.githubusercontent.com/8567436/203745247-83c71fea-817c-46ee-bd67-a2fc47ac1890.png)
https://blog.csdn.net/sD7O95O/article/details/108288859
