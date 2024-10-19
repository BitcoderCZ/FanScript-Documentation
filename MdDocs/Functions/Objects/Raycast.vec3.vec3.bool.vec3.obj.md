

# Raycast(vec3, vec3, bool, vec3, obj)

Detects if an object intersects a line between [from](#from) and [to](#to).

```
void raycast(vec3 from, vec3 to, out bool didHit, out vec3 hitPos, out obj hitObj)
```

## Parameters

#### `from`
Type: [vec3](/MdDocs/Types/Vec3.md)

From position of the ray.

#### `to`
Type: [vec3](/MdDocs/Types/Vec3.md)

To position of the ray.

#### `didHit`
Type: [bool](/MdDocs/Types/Bool.md)

If the ray hit an object.

#### `hitPos`
Type: [vec3](/MdDocs/Types/Vec3.md)

The position at which the ray intersected [hitObj](#hitObj).

#### `hitObj`
Type: [obj](/MdDocs/Types/Obj.md)

The object that was hit.

## Remarks

 - Only detects the outside surface of a block. If it starts inside of a block, the block won't be detected.
 - Won't detect object created on the same frame as the raycast is performed.
 - Won't detect objects without collion or script blocks.
 - If the raycast hits the floor, [hitObj](#hitObj) will be equal to null.


