# XExten
--------------
#### 针对Linq，Expression，CommonUtil，HttpClient，DynamicType，Redis，MongoDB，RuntimeCache进行了拓展的工具类
[![](https://img.shields.io/badge/build-success-brightgreen.svg)](https://github.com/EmilyEdna/XExten)
[![](https://img.shields.io/badge/nuget-v1.2.0-blue.svg)](https://www.nuget.org/packages/XExten/1.2.0)
![](https://img.shields.io/badge/dependencies-Net461-blue.svg)
![](https://img.shields.io/badge/dependencies-NetStandard2-blue.svg)
### 怎么使用呢
--------------
##### 使用linq的拓展需要引入XExten.XCore域名空间
``` c#
[Fact]
public void ToOver_Test()
{
  List<TestA> Li = new List<TestA>();
  Li.ToOver(t => t.Name);
}
```
--------------
##### 使用expression的拓展需要引入XExten.XExpres域名空间
```c#
[Fact]
public void GetExpression_Test()
{
   string[] arr = new[] { "Id", "Name" };
   var res = XExp.GetExpression<TestA>(arr);
}
```
--------------
##### 使用Commom工具类需要引入XExten.XPlus域名空间
```c#
[Fact]
public void XBarHtml_Test()
{
    var res = XPlusEx.XBarHtml("ABC", 3, 50);
}
```
--------------
##### 使用HttpClient需要XExten.HttpFactory,使用DynamicType需要XExten.DynamicType.
##### 在项目中同时支持使用momeryCache,redis,mongodb
