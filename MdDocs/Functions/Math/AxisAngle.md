# AxisAngle(vec3, float)

Outputs rotation of [angle](#angle) around [axis](#axis).

```
rot axisAngle(vec3 axis, float angle)
```

## Parameters

#### `axis`
Type: vec3

The axis to rotate around.

#### `angle`
Type: float

How much to rotate (in degrees).

## Returns

rot
## Examples

``` fcs
inspect(axisAngle(vec3(0, 1, 0), 90)) // (0, 90, 0)
```

``` fcs
inspect(axisAngle(vec3(1, 0, 0), 45)) // (45, 0, 0)
```

