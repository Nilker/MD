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

clearInterval 清除定时器



## V-Model 双向数据绑定

只能用于表单元素：

- Input：text，radio，address，email，.....
- select
- checkbox
- textArea



## Class样式

1. 数组

   ```vue
   <h1 :class="['red','thin']">1111111</h1>
   <h1 class="red thin">1111111</h1>
   ```

2. 数组中  三元表达式

   ```vue
   <h1 :class="['red','thin',true?'active':'']">1111111</h1>
   <h1 class="red thin active">1111111</h1>
   ```

3. 数组中  嵌套对象

   ```vue
   <h1 :class="['red','thin',{'active':true}]">1111111</h1>
   <h1 class="red thin active">1111111</h1>
   ```

4. 直接使用对象

   ```vue
   <h1 :class="{red:true,thin:true,active:true,tt:false}">1111111</h1>
   <h1 class="red thin active">1111111</h1>
   
   <h1 :class="classObj">11111</h1>
   data:{
   	classObj:{red:true,thin:true,active:true,tt:false}
   }
   ```



## v-for 循环

1. 数组集合

   ```vue
    <li v-for="(item, index) in items">
       {{ parentMessage }} - {{ index }} - {{ item.message }}
     </li>
   ```

   2.2+版本后，:key必须

   - key：属性只能是 string /num类型
   - key：在使用时，必须使用v-bind 属性绑定方式

   

2. 对象

```html
<ul id="v-for-object" class="demo">
  <li v-for="value in object">
    {{ value }}
  </li>
</ul>
```

```js
new Vue({
  el: '#v-for-object',
  data: {
    object: {
      firstName: 'John',
      lastName: 'Doe',
      age: 30
    }
  }
})
```

结果：	John

​		Doe

​		30		

```html
<div v-for="(value, key, index) in object">
  {{ index }}. {{ key }}: {{ value }}
</div>
```

结果：	0.firstName:John

​		1.lastName:Doe

​		2.age:30	



## v-If   v-Show

v-if：移除Dom元素     频繁切换不可用

v-show：添加隐藏样式 display:none  ，创建后，从不显示， 不可用

## 集合操作

注意：  forEach   some   filter   findIndex   这些都属于数组的新方法

注意 ： ES6中，为字符串提供了一个新方法，叫做  String.prototype.includes('要包含的字符串')

​            //  如果包含，则返回 true ，否则返回 false

```js
search(keywords) { // 根据关键字，进行数据的搜索
          /* var newList = []
          this.list.forEach(item => {
            if (item.name.indexOf(keywords) != -1) {
              newList.push(item)
            }
          })
          return newList */

          // 注意：  forEach   some   filter   findIndex   这些都属于数组的新方法，
          //  都会对数组中的每一项，进行遍历，执行相关的操作；
          return this.list.filter(item => {
            // if(item.name.indexOf(keywords) != -1)

            // 注意 ： ES6中，为字符串提供了一个新方法，叫做  String.prototype.includes('要包含的字符串')
            //  如果包含，则返回 true ，否则返回 false
            //  contain
            if (item.name.includes(keywords)) {
              return item
            }
          })

          // return newList
        }
```



## filter：过滤器

私有过滤器

```js
filters: { // 私有局部过滤器，只能在 当前 VM 对象所控制的 View 区域进行使用

    dataFormat(input, pattern = "") { // 在参数列表中 通过 pattern="" 来指定形参默认值，防止报错

      var dt = new Date(input);

      // 获取年月日

      var y = dt.getFullYear();

      var m = (dt.getMonth() + 1).toString().padStart(2, '0');

      var d = dt.getDate().toString().padStart(2, '0');



      // 如果 传递进来的字符串类型，转为小写之后，等于 yyyy-mm-dd，那么就返回 年-月-日

      // 否则，就返回  年-月-日 时：分：秒

      if (pattern.toLowerCase() === 'yyyy-mm-dd') {

        return `${y}-${m}-${d}`;

      } else {

        // 获取时分秒

        var hh = dt.getHours().toString().padStart(2, '0');

        var mm = dt.getMinutes().toString().padStart(2, '0');

        var ss = dt.getSeconds().toString().padStart(2, '0');



        return `${y}-${m}-${d} ${hh}:${mm}:${ss}`;

      }

    }

  }
```

全局过滤器



```js
// 定义一个全局过滤器

Vue.filter('dataFormat', function (input, pattern = '') {

  var dt = new Date(input);

  // 获取年月日

  var y = dt.getFullYear();

  var m = (dt.getMonth() + 1).toString().padStart(2, '0');

  var d = dt.getDate().toString().padStart(2, '0');



  // 如果 传递进来的字符串类型，转为小写之后，等于 yyyy-mm-dd，那么就返回 年-月-日

  // 否则，就返回  年-月-日 时：分：秒

  if (pattern.toLowerCase() === 'yyyy-mm-dd') {

    return `${y}-${m}-${d}`;

  } else {

    // 获取时分秒

    var hh = dt.getHours().toString().padStart(2, '0');

    var mm = dt.getMinutes().toString().padStart(2, '0');

    var ss = dt.getSeconds().toString().padStart(2, '0');



    return `${y}-${m}-${d} ${hh}:${mm}:${ss}`;

  }

});
```



## directive：指令

全局指令：

```js
// 使用  Vue.directive() 定义全局的指令  v-focus
    // 其中：参数1 ： 指令的名称，注意，在定义的时候，指令的名称前面，不需要加 v- 前缀, 
    // 但是： 在调用的时候，必须 在指令名称前 加上 v- 前缀来进行调用
    //  参数2： 是一个对象，这个对象身上，有一些指令相关的函数，这些函数可以在特定的阶段，执行相关的操作
    Vue.directive('focus', {
      bind: function (el) { // 每当指令绑定到元素上的时候，会立即执行这个 bind 函数，只执行一次
        // 注意： 在每个 函数中，第一个参数，永远是 el ，表示 被绑定了指令的那个元素，这个 el 参数，是一个原生的JS对象
        // 在元素 刚绑定了指令的时候，还没有 插入到 DOM中去，这时候，调用 focus 方法没有作用
        //  因为，一个元素，只有插入DOM之后，才能获取焦点
        // el.focus()
      },
      inserted: function (el) {  // inserted 表示元素 插入到DOM中的时候，会执行 inserted 函数【触发1次】
        el.focus()
        // 和JS行为有关的操作，最好在 inserted 中去执行，放置 JS行为不生效
      },
      updated: function (el) {  // 当VNode更新的时候，会执行 updated， 可能会触发多次

      }
    })



    // 自定义一个 设置字体颜色的 指令
    Vue.directive('color', {
      // 样式，只要通过指令绑定给了元素，不管这个元素有没有被插入到页面中去，这个元素肯定有了一个内联的样式
      // 将来元素肯定会显示到页面中，这时候，浏览器的渲染引擎必然会解析样式，应用给这个元素
      bind: function (el, binding) {
        // el.style.color = 'red'
        // console.log(binding.name)
        // 和样式相关的操作，一般都可以在 bind 执行

        // console.log(binding.value)
        // console.log(binding.expression)

        el.style.color = binding.value
      }
    })
```

私有指令：

```js
directives: { // 自定义私有指令
        'fontweight': { // 设置字体粗细的
          bind: function (el, binding) {
            el.style.fontWeight = binding.value
          }
        },
        'fontsize': function (el, binding) { // 注意：这个 function 等同于 把 代码写到了 bind 和 update 中去
          el.style.fontSize = parseInt(binding.value) + 'px'
        }
      }
```



## 生命周期:

```html
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <title>Document</title>
  <script src="./lib/vue-2.4.0.js"></script>
</head>

<body>
  <div id="app">
    <input type="button" value="修改msg" @click="msg='No'">
    <h3 id="h3">{{ msg }}</h3>
  </div>

  <script>
    // 创建 Vue 实例，得到 ViewModel
    var vm = new Vue({
      el: '#app',
      data: {
        msg: 'ok'
      },
      methods: {
        show() {
          console.log('执行了show方法')
        }
      },
      beforeCreate() { // 这是我们遇到的第一个生命周期函数，表示实例完全被创建出来之前，会执行它
        // console.log(this.msg)
        // this.show()
        // 注意： 在 beforeCreate 生命周期函数执行的时候，data 和 methods 中的 数据都还没有没初始化
      },
      created() { // 这是遇到的第二个生命周期函数
        // console.log(this.msg)
        // this.show()
        //  在 created 中，data 和 methods 都已经被初始化好了！
        // 如果要调用 methods 中的方法，或者操作 data 中的数据，最早，只能在 created 中操作
      },
      beforeMount() { // 这是遇到的第3个生命周期函数，表示 模板已经在内存中编辑完成了，但是尚未把 模板渲染到 页面中
        // console.log(document.getElementById('h3').innerText)
        // 在 beforeMount 执行的时候，页面中的元素，还没有被真正替换过来，只是之前写的一些模板字符串
      },
      mounted() { // 这是遇到的第4个生命周期函数，表示，内存中的模板，已经真实的挂载到了页面中，用户已经可以看到渲染好的页面了
        // console.log(document.getElementById('h3').innerText)
        // 注意： mounted 是 实例创建期间的最后一个生命周期函数，当执行完 mounted 就表示，实例已经被完全创建好了，此时，如果没有其它操作的话，这个实例，就静静的 躺在我们的内存中，一动不动
      },


      // 接下来的是运行中的两个事件
      beforeUpdate() { // 这时候，表示 我们的界面还没有被更新【数据被更新了吗？  数据肯定被更新了】
        /* console.log('界面上元素的内容：' + document.getElementById('h3').innerText)
        console.log('data 中的 msg 数据是：' + this.msg) */
        // 得出结论： 当执行 beforeUpdate 的时候，页面中的显示的数据，还是旧的，此时 data 数据是最新的，页面尚未和 最新的数据保持同步
      },
      updated() {
        console.log('界面上元素的内容：' + document.getElementById('h3').innerText)
        console.log('data 中的 msg 数据是：' + this.msg)
        // updated 事件执行的时候，页面和 data 数据已经保持同步了，都是最新的
      }
    });
  </script>
</body>

</html>
```



