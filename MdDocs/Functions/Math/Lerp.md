# Lerp(rot, rot, float)

Linearly interpolates between [from](#from) and [to](#to), depending on [amount](#amount).

```
rot lerp(rot from, rot to, float amount)
```

## Parameters

#### `from`
Type: rot

The start value.

#### `to`
Type: rot

The end value.

#### `amount`
Type: float

How far between [from](#from) and [to](#to) to transition (0-1).

## Returns

rot

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

