# 组件：

## 全局组件

1. ### 使用 Vue.extend 配合 Vue.component 方法：``

   ```vue
   var login = Vue.extend({
         template: '<h1>登录</h1>'
       });
       Vue.component('login', login);
   ```

   

2. ### 直接使用 Vue.component 方法：

   ```vue
   Vue.component('register', {
         template: '<h1>注册</h1>'
       });
   ```

   

3. ### 将模板字符串，定义到script标签种：

   ```vue
   <script id="tmpl" type="x-template">
         <div><a href="#">登录</a> | <a href="#">注册</a></div>
       </script>
   
   Vue.component('account', {
         template: '#tmpl'
       });
   ```

   ==注意==： 组件中的DOM结构，有且只能有唯一的根元素（Root Element）来进行包裹！

## 生命周期

```vue
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
    <script src="node_modules/vue/dist/vue.js"></script>
</head>

<body>
    <div id="app">
        <h3 id="h3">{{msg}}</h3>
    </div>
    <script>
        var vm=new Vue({
           el:'#app',
           data:function (){
               return {
                   msg:'VM--->Dom渲染完成'
               }
           },
           methods:{},
           //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓创建阶段↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
           //1、 data 、methods -----》  没有初始化；
           beforeCreate() {
               
           },
           //2、data 、methods -----》已经初始化；
           created() {
               
           },
           //3、Mount(挂载) -----》Vm(编译模板到)内存中到页面Dom的操作叫Mount    故：beforeMount :挂载页面前,未渲染到页面DOM
           //页面中的元素还没有真正的替换 
           beforeMount() {
               console.log( document.getElementById('h3').innerText) //输出：{{msg}}
           },
           //4、VM编译的模板----》输出到页面DOM中去，之后、之后、之后、之后、 
           //可以进行 DOM 节点操作，VUE初始化完毕；
           mounted() {
            console.log( document.getElementById('h3').innerText) //输出：VM--->Dom渲染完成
           },
           //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑创建阶段↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
           

           //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓运行阶段↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
           //触发条件：data 数据发生改变  
           // a、数据已经被更新；b、页面未更新
           beforeUpdate() {
            console.log("beforeUpdate====>页面："+ document.getElementById('h3').innerText) ;
            console.log("beforeUpdate====>Data："+ this.msg); 
           },
           //数据和页面都已经被更新
           updated() {
               
           },

        });
    </script>
</body>

</html>
```

