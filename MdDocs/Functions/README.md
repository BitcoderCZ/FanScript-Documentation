# Functions

Functions are called like so:

``` fcs
functionName(arg0, arg1)
```


You can specify the generic type of a function like so (this isn't needed most of the time as the compiler can usually infer the type from the types of the arguments):

``` fcs
someGenericFunction<theGenericType>(arg0, arg1)
```


Ref and out arguments need to be prefixed with the ref/out keyword.

``` fcs
someFunc(ref arg0, out arg1)
```


Out arguments can be used with expression variable decleration (the variable is declared in the argument) and discards (the result of the out variable is ignored)


``` fcs
someFunc(out typeOfArg0 arg0, out _)
```



## Contents

- [FcComment](FcComment.md)
- [Get](Get.md)
- [GetBlockById](GetBlockById.md)
- [Inspect](Inspect.md)
- [Set](Set.md)
- [SetPtrValue](SetPtrValue.md)
- [SetRange](SetRange.md)
- [ToRot](ToRot.md)
- [ToVec](ToVec.md)
- [Control](Control/README.md)
    - [Joystick](Control/Joystick.md)
- [Game](Game/README.md)
    - [GetAccelerometer](Game/GetAccelerometer.md)
    - [GetCurrentFrame](Game/GetCurrentFrame.md)
    - [GetScreenSize](Game/GetScreenSize.md)
    - [GetScreenSize2](Game/GetScreenSize2.md)
    - [Lose](Game/Lose.md)
    - [MenuItem](Game/MenuItem.md)
    - [SetCamera](Game/SetCamera.md)
    - [SetLight](Game/SetLight.md)
    - [SetScore](Game/SetScore.md)
    - [ShopSection](Game/ShopSection.md)
    - [Win](Game/Win.md)
- [Math](Math/README.md)
    - [Abs](Math/Abs.md)
    - [AxisAngle](Math/AxisAngle.md)
    - [Ceiling](Math/Ceiling.md)
    - [Cos](Math/Cos.md)
    - [Cross](Math/Cross.md)
    - [Dist](Math/Dist.md)
    - [Dot](Math/Dot.md)
    - [Floor](Math/Floor.md)
    - [Lerp](Math/Lerp.md)
    - [Lerp2](Math/Lerp2.md)
    - [Lerp3](Math/Lerp3.md)
    - [LineVsPlane](Math/LineVsPlane.md)
    - [Log](Math/Log.md)
    - [LookRotation](Math/LookRotation.md)
    - [Max](Math/Max.md)
    - [Min](Math/Min.md)
    - [Normalize](Math/Normalize.md)
    - [Pow](Math/Pow.md)
    - [Random](Math/Random.md)
    - [Round](Math/Round.md)
    - [ScreenToWorld](Math/ScreenToWorld.md)
    - [SetRandomSeed](Math/SetRandomSeed.md)
    - [Sin](Math/Sin.md)
    - [WorldToScreen](Math/WorldToScreen.md)
    - [WorldToScreen2](Math/WorldToScreen2.md)
- [Objects](Objects/README.md)
    - [Clone](Objects/Clone.md)
    - [Destroy](Objects/Destroy.md)
    - [GetObject](Objects/GetObject.md)
    - [GetObject2](Objects/GetObject2.md)
    - [GetPos](Objects/GetPos.md)
    - [GetSize](Objects/GetSize.md)
    - [Raycast](Objects/Raycast.md)
    - [SetPos](Objects/SetPos.md)
    - [SetPos2](Objects/SetPos2.md)
    - [SetVisible](Objects/SetVisible.md)
- [Physics](Physics/README.md)
    - [AddConstraint](Physics/AddConstraint.md)
    - [AddForce](Physics/AddForce.md)
    - [AngularLimits](Physics/AngularLimits.md)
    - [AngularMotor](Physics/AngularMotor.md)
    - [AngularSpring](Physics/AngularSpring.md)
    - [GetVelocity](Physics/GetVelocity.md)
    - [LinearLimits](Physics/LinearLimits.md)
    - [LinearMotor](Physics/LinearMotor.md)
    - [LinearSpring](Physics/LinearSpring.md)
    - [SetBounciness](Physics/SetBounciness.md)
    - [SetFriction](Physics/SetFriction.md)
    - [SetGravity](Physics/SetGravity.md)
    - [SetLocked](Physics/SetLocked.md)
    - [SetMass](Physics/SetMass.md)
    - [SetVelocity](Physics/SetVelocity.md)
- [Sound](Sound/README.md)
    - [PlaySound](Sound/PlaySound.md)
    - [SetVolumePitch](Sound/SetVolumePitch.md)
    - [StopSound](Sound/StopSound.md)
