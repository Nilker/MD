# 1、概念

ZooKeeper是一个分布式的，开放源码的分布式应用程序协调服务，是Google的Chubby一个开源的实现，它是集群的管理者，监视着集群中各个节点的状态根据节点提交的反馈进行下一步合理操作。最终，将简单易用的接口和性能高效、功能稳定的系统提供给用户；

## ​zookeeper 提供了什么?

- 文件系统
- 通知机制



# 2、安装

```shell
//获取 zookeeper 的image
docker pull zookeeper

//安装启动
docker run -d -p 2181:2181 --restart always  --name my-zk d6f23d89fbee
2181:客户端端口
2888:
```

# 3、master选举与leader选举

## 1、master选举

