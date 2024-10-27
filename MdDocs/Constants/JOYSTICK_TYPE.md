
# JOYSTICK_TYPE
### Type: [float](/MdDocs/Types/Float.md)
Determines what the output value of [joystick(vec3,float)](/MdDocs/Functions/Control/Joystick.vec3.float.md) is.
### Used by

 - [joystick(vec3,float)](/MdDocs/Functions/Control/Joystick.vec3.float.md)
### Values
```
JOYSTICK_TYPE_XZ
JOYSTICK_TYPE_SCREEN
```
##### JOYSTICK_TYPE_XZ
Value: 0

Outputs XZ vector values perpendicular to camera direction (while assuming that the screen is always facing straight to a certain axis).
##### JOYSTICK_TYPE_SCREEN
Value: 1

Outputs XY vector values regardless of where the camera is facing.

