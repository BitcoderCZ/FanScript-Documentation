# Rot

Represents a rotation using euler angles, internaly uses quaternion.

```
rot
```

## How to create

``` fcs
// rot(float x, float y, float z) - euler angle in degrees
rot a = rot(45, 90, 45) // constant - uses the vector block

float y = 30
rot b = rot(60, y, 180) // uses the make vector block

const float x = 45
rot c = rot(x, 60, 10) // constant - uses the vector block
```

## Related

 - [vec3](/MdDocs/Types/Vec3.md)
