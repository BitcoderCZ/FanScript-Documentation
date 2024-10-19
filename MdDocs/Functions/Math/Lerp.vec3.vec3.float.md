

# Lerp(vec3, vec3, float)

Linearly interpolates between [from](#from) and [to](#to), depending on [amount](#amount).

```
vec3 Lerp(vec3 from, vec3 to, float amount)
```

## Parameters

#### `from`
Type: [vec3](/MdDocs/Types/Vec3.md)

The start value.

#### `to`
Type: [vec3](/MdDocs/Types/Vec3.md)

The end value.

#### `amount`
Type: [float](/MdDocs/Types/Float.md)

How far between [from](#from) and [to](#to) to transition (0 - 1).

## Returns

[vec3](/MdDocs/Types/Vec3.md)

The value between [from](#from) and [to](#to).

## Examples

``` fcs
// ease out from "from" to "to"
vec3 from
vec3 to
float speed
on Play()
{
    from = vec3(0, 0, 0)
    to = vec3(0, 90, 0)
    speed = 0.05 // 5% between from and to
}
from = lerp(from, to, speed)
```


