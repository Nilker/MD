# 指令

## v-cloak

解决闪烁问题，为解决{{}}表达式而生

## v-text

1. 没有闪烁问题
2. 直接覆盖元素中原有的内容；{{}}只替换内部内容

## v-html

将html内容进行替换

## v-bind 绑定属性 简写 ：

用于提供绑定属性的指令；

可以写合法的表达式；

Eg：	

​	v-bind:title=""

​	v-bind:title="mytitle+'123'"

简写  :title="mytitle+'123'"

## v-on 绑定事件  简写 @

v-on:click="show"

v-on:mouseOver=""

.....

```javascript
<script>
    var vm=new Vue({
        el:'#app',
        data:{
            mytitle:'ttttttttt',            
        },
        methods:{
            //定义方法
            show:function(){
                alert('123')
            }
        }
    });
</script>
```

## 事件修饰符

- .Stop	阻止冒泡(从内--->外)   ----阻止所有冒泡

  ```vue
   <div class="inner" @click="divClick" >
       <input type="button" value="点他" @click.stop="btnClick">
  </div>
  ```

  

- .Prevent   阻止默认事件

  ```vue
  <a href="http://www.baidu.com"  @click.prevent="linkClick" >百度</a>
  ```

  

- .Capture    添加时间监听器时使用时间捕获模式（从外--->内）

  ```vue
   <div class="inner" @click.capture="divClick" >
       <input type="button" value="点他" @click.stop="btnClick">
  </div>
  ```

  

- .Self    只当事件在该元素本身（比如不是子元素）才触发函数  ---只阻止当前元素冒泡

  ```vue
   <div class="inner" @click.self="divClick" >
       <input type="button" value="点他" @click.stop="btnClick">
  </div>
  ```

  

- .once    事件只触发一次   注意：`前后是有区别的`

  ```vue
  <a href="http://www.baidu.com"  @click.prevent.once="linkClick" >百度</a>
  ```

- 区别  .stop  .self

   .stop ----阻止所有冒泡

  .self   ----只阻止自己冒泡，真正的冒泡还是存在的



## vm实例中

需要获取data上的数据/methods中的方法，必须通过 this.数据属性名，this.方法名

## SetInterval 定时器

var  intervalId=SetInterval(function (){},500);

var  intervalId=SetInterval(()=>{//解决this传递问题},500);

## clearInterval 清除定时器



