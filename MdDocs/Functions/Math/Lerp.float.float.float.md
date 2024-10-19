

# Lerp(float, float, float)

Linearly interpolates between [from](#from) and [to](#to), depending on [amount](#amount).

```
float Lerp(float from, float to, float amount)
```

## Parameters

#### `from`
Type: [float](/MdDocs/Types/Float.md)

The start value.

#### `to`
Type: [float](/MdDocs/Types/Float.md)

The end value.

#### `amount`
Type: [float](/MdDocs/Types/Float.md)

How far between [from](#from) and [to](#to) to transition (0 - 1).

## Returns

[float](/MdDocs/Types/Float.md)

The value between [from](#from) and [to](#to).

## Examples

``` fcs
// ease out from "from" to "to"
float from
float to
float speed
on Play()
{
    from = 0
    to = 100
    speed = 0.05 // 5% between from and to
}
from = lerp(from, to, speed)
```


