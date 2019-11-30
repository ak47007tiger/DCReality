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