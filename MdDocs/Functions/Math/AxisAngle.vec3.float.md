

# AxisAngle(vec3, float)

Outputs rotation of [angle](#angle) around [axis](#axis).

```
rot axisAngle(vec3 axis, float angle)
```

## Parameters

#### `axis`
Type: [vec3](/MdDocs/Types/Vec3.md)

The axis to rotate around.

#### `angle`
Type: [float](/MdDocs/Types/Float.md)

How much to rotate (in degrees).

## Returns

[rot](/MdDocs/Types/Rot.md)
## Examples

``` fcs
inspect(axisAngle(vec3(0, 1, 0), 90)) // (0, 90, 0))

inspect(axisAngle(vec3(1, 0, 0), 45)) // (45, 0, 0)
```


