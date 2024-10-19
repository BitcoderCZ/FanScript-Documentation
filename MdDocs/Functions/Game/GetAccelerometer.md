

# GetAccelerometer()

Gets the phone's current acceleration.

```
vec3 getAccelerometer()
```

## Returns

[vec3](/MdDocs/Types/Vec3.md)

The acceleration, can be used to determine the phone's tilt.

## Examples

``` fcs
// the acceleration can be smoothed out like this:
vec3 smooth
smooth += (getAccelerometer() - smooth) * 0.1
```


