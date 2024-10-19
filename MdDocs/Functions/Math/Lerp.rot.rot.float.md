

# Lerp(rot, rot, float)

Linearly interpolates between [from](#from) and [to](#to), depending on [amount](#amount).

```
rot lerp(rot from, rot to, float amount)
```

## Parameters

#### `from`
Type: [rot](/MdDocs/Types/Rot.md)

The start value.

#### `to`
Type: [rot](/MdDocs/Types/Rot.md)

The end value.

#### `amount`
Type: [float](/MdDocs/Types/Float.md)

How far between [from](#from) and [to](#to) to transition (0 - 1).

## Returns

[rot](/MdDocs/Types/Rot.md)

The value between [from](#from) and [to](#to).

## Examples

``` fcs
// ease out from "from" to "to"
rot from
rot to
float speed
on Play()
{
    from = rot(0, 0, 0)
    to = rot(0, 90, 0)
    speed = 0.05 // 5% between from and to
}
from = lerp(from, to, speed)
```


