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


If the inline modifier is applied (or the function is only called 1 time), the call to the function gets replaced by the code of the function.

## Contents

- [FcComment](FcComment.string.md)
- [Get](Get.array.float.md)
- [GetBlockById](GetBlockById.float.md)
- [Inspect](Inspect.generic.md)
- [Set](Set.array.float.generic.md)
- [SetPtrValue](SetPtrValue.generic.generic.md)
- [SetRange](SetRange.array.float.arraySegment.md)
- [ToRot](ToRot.vec3.md)
- [ToVec](ToVec.rot.md)
- [Control](Control/README.md)
    - [Joystick](Control/Joystick.vec3.float.md)
- [Game](Game/README.md)
    - [GetAccelerometer](Game/GetAccelerometer.md)
    - [GetCurrentFrame](Game/GetCurrentFrame.md)
    - [GetScreenSize](Game/GetScreenSize.md)
    - [GetScreenSize](Game/GetScreenSize.float.float.md)
    - [Lose](Game/Lose.float.md)
    - [MenuItem](Game/MenuItem.float.obj.string.float.float.md)
    - [SetCamera](Game/SetCamera.vec3.rot.float.bool.md)
    - [SetLight](Game/SetLight.vec3.rot.md)
    - [SetScore](Game/SetScore.float.float.float.md)
    - [ShopSection](Game/ShopSection.string.md)
    - [Win](Game/Win.float.md)
- [Math](Math/README.md)
    - [Abs](Math/Abs.float.md)
    - [AxisAngle](Math/AxisAngle.vec3.float.md)
    - [Ceiling](Math/Ceiling.float.md)
    - [Cos](Math/Cos.float.md)
    - [Cross](Math/Cross.vec3.vec3.md)
    - [Dist](Math/Dist.vec3.vec3.md)
    - [Dot](Math/Dot.vec3.vec3.md)
    - [Floor](Math/Floor.float.md)
    - [Lerp](Math/Lerp.float.float.float.md)
    - [Lerp](Math/Lerp.rot.rot.float.md)
    - [Lerp](Math/Lerp.vec3.vec3.float.md)
    - [LineVsPlane](Math/LineVsPlane.vec3.vec3.vec3.vec3.md)
    - [Log](Math/Log.float.float.md)
    - [LookRotation](Math/LookRotation.vec3.vec3.md)
    - [Max](Math/Max.float.float.md)
    - [Min](Math/Min.float.float.md)
    - [Normalize](Math/Normalize.vec3.md)
    - [Pow](Math/Pow.float.float.md)
    - [Random](Math/Random.float.float.md)
    - [Round](Math/Round.float.md)
    - [ScreenToWorld](Math/ScreenToWorld.float.float.vec3.vec3.md)
    - [SetRandomSeed](Math/SetRandomSeed.float.md)
    - [Sin](Math/Sin.float.md)
    - [WorldToScreen](Math/WorldToScreen.vec3.md)
    - [WorldToScreen](Math/WorldToScreen.vec3.float.float.md)
- [Objects](Objects/README.md)
    - [Clone](Objects/Clone.obj.obj.md)
    - [Destroy](Objects/Destroy.obj.md)
    - [GetObject](Objects/GetObject.float.float.float.md)
    - [GetObject](Objects/GetObject.vec3.md)
    - [GetPos](Objects/GetPos.obj.vec3.rot.md)
    - [GetSize](Objects/GetSize.obj.vec3.vec3.md)
    - [Raycast](Objects/Raycast.vec3.vec3.bool.vec3.obj.md)
    - [SetPos](Objects/SetPos.obj.vec3.md)
    - [SetPos](Objects/SetPos.obj.vec3.rot.md)
    - [SetVisible](Objects/SetVisible.obj.bool.md)
- [Physics](Physics/README.md)
    - [AddConstraint](Physics/AddConstraint.obj.obj.vec3.constr.md)
    - [AddForce](Physics/AddForce.obj.vec3.vec3.vec3.md)
    - [AngularLimits](Physics/AngularLimits.constr.vec3.vec3.md)
    - [AngularMotor](Physics/AngularMotor.constr.vec3.vec3.md)
    - [AngularSpring](Physics/AngularSpring.constr.vec3.vec3.md)
    - [GetVelocity](Physics/GetVelocity.obj.vec3.vec3.md)
    - [LinearLimits](Physics/LinearLimits.constr.vec3.vec3.md)
    - [LinearMotor](Physics/LinearMotor.constr.vec3.vec3.md)
    - [LinearSpring](Physics/LinearSpring.constr.vec3.vec3.md)
    - [SetBounciness](Physics/SetBounciness.obj.float.md)
    - [SetFriction](Physics/SetFriction.obj.float.md)
    - [SetGravity](Physics/SetGravity.vec3.md)
    - [SetLocked](Physics/SetLocked.obj.vec3.vec3.md)
    - [SetMass](Physics/SetMass.obj.float.md)
    - [SetVelocity](Physics/SetVelocity.obj.vec3.vec3.md)
- [Sound](Sound/README.md)
    - [PlaySound](Sound/PlaySound.float.float.float.bool.float.md)
    - [SetVolumePitch](Sound/SetVolumePitch.float.float.float.md)
    - [StopSound](Sound/StopSound.float.md)
