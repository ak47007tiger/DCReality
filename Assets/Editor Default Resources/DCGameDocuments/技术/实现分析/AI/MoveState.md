## enum
- NormalMove
  - MoveIdle
  - MovePosition
  - MoveTarget
  - MoveStop//定身
  - MoveTranslate//主动位移
- MoveForceTransalte//强制位移

## introduce
- 可以用分层状态机也可以用一个状态机，为方便先用一个状态机
- 为了可以一个状态结束后回到上一个状态，需要下推自动机

## 移动状态机要解决的问题
- 英雄移动指令监听
- 追踪目标

## 放技能导致的追击
- 2个状态机都轮询
- 追击和技能同时轮询
- 如果距离够技能放技能，一直轮询直到玩家取消技能

## 是用状态机调用移动组件还是移动组件自己轮询