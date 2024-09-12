# AddForce(obj, vec3, vec3, vec3)

Adds [force](#force) and/or [torque](#torque) to [object](#object).

```
void addForce(this obj object, vec3 force, vec3 applyAt, vec3 torque)
```

## Parameters

#### `object`
Type: [obj](/MdDocs/Types/Obj.md)

The object that the force will be applied to.

#### `force`
Type: [vec3](/MdDocs/Types/Vec3.md)

The force to apply to [object](#object).

#### `applyAt`
Type: [vec3](/MdDocs/Types/Vec3.md)

Where on [object](#object) should [force](#force) be applied at (center of mass by default).

#### `torque`
Type: [vec3](/MdDocs/Types/Vec3.md)

The rotational force to apply to [object](#object).

## Related

 - [setVelocity(obj,vec3,vec3)](/MdDocs/Functions/Physics/SetVelocity.md)
 - [getVelocity(obj,vec3,vec3)](/MdDocs/Functions/Physics/GetVelocity.md)

