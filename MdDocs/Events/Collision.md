
# Collision(obj, obj, float, vec3)

Executes when an object (object2) collides with object1.

```
on collision(obj object1, out obj object2, out float impulse, out vec3 normal) { }
```

## Parameters

#### `object1`
Type: [obj](/MdDocs/Types/Obj.md)

The object to detect collisions for.

#### `object2`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [obj](/MdDocs/Types/Obj.md)

The object which object1 collided with.

#### `impulse`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [float](/MdDocs/Types/Float.md)

Impact force of the collision.

#### `normal`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [vec3](/MdDocs/Types/Vec3.md)

Direction of impact from object2 to object1.

## Remarks

 - If object1 is colliding with multiple objects, the most forceful collision will be reported.
 - If you're overriding physics to move objects (using Set Position) then no collisions will be detected.


