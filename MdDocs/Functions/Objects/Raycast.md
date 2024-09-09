# Raycast(vec3, vec3, bool, vec3, object)

Detects if an object intersects a line between [from](#from) and [to](#to).

```
void raycast(vec3 from, vec3 to, out bool didHit, out vec3 hitPos, out object hitObj)
```

## Parameters

#### `from`
Type: vec3

From position of the ray.

#### `to`
Type: vec3

To position of the ray.

#### `didHit`
Type: bool

If the ray hit an object.

#### `hitPos`
Type: vec3

The position at which the ray intersected [hitObj](#hitObj).

#### `hitObj`
Type: object

The object that was hit.

## Remarks

- Only detects the outside surface of a block. If it starts inside of a block, the block won't be detected.
- Won't detect object created on the same frame as the raycast is performed.
- Won't detect objects without collion or script blocks.
- If the raycast hits the floor, [hitObj](#hitObj) will be equal to null.

