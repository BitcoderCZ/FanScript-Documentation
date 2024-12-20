

# GetVelocity(obj, vec3, vec3)

Gets the [object](#object)'s velocity.

```
void getVelocity(this obj object, out vec3 velocity, out vec3 spin)
```

## Parameters

#### `object`
Type: [obj](/MdDocs/Types/Obj.md)

#### `velocity`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [vec3](/MdDocs/Types/Vec3.md)

Linear velocity of [object](#object) (units/second).

#### `spin`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [vec3](/MdDocs/Types/Vec3.md)

Angular velocity of [object](#object) (degrees/second).

## Related

 - [setVelocity(obj,vec3,vec3)](/MdDocs/Functions/Physics/SetVelocity.obj.vec3.vec3.md)
 - [addForce(obj,vec3,vec3,vec3)](/MdDocs/Functions/Physics/AddForce.obj.vec3.vec3.vec3.md)


