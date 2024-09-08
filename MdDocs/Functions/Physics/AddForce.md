# AddForce(object, vec3, vec3, vec3)

Adds [force](#force) and/or [torque](#torque) to [object](#object).

```
void addForce(this object object, vec3 force, vec3 applyAt, vec3 torque)
```

## Parameters

#### `object`
Type: object

The object that the force will be applied to.

#### `force`
Type: vec3

The force to apply to [object](#object).

#### `applyAt`
Type: vec3

Where on [object](#object) should [force](#force) be applied at (center of mass by default).

#### `torque`
Type: vec3

The rotational force to apply to [object](#object).

## Related

 - [setVelocity(object,vec3,vec3)](/MdDocs/Functions/Physics/SetVelocity.md)
 - [getVelocity(object,vec3,vec3)](/MdDocs/Functions/Physics/GetVelocity.md)

