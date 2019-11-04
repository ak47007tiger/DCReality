## 整个技能对象的流程
- 设置参数
- life recycle
  - create
  - 时间tick
  - destroy
- event
  - get target
  - destroy

## 怎么在技能生效的时候添加buff
- 技能配置里面增加一个字段，生效时添加的buff
- 增加一个处理器，生效到目标时执行action

## 处理buff对技能的限制
- 怎么判断一个buff限制一个技能的释放
- 释放技能前判断
- 哪些buff会影响哪些技能的释放
  - 晕眩buff除了接触晕眩的技能阻止所有技能释放

## 技能类型
- class1
  - normal
    - 一般为直接添加buff
  - area
  - bullet
- class2
  - target
  - no target

## 流程和对象
- 1
  - hero, cast, actor
- 2
  - skill
- 3
  - event handler
- 4
  - effect

## 处理闪电链
- 计算cast的最大区域，cast
- 随机收集目标，根据距离排序