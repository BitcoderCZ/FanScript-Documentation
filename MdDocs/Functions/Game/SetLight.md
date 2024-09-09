# SetLight(vec3, rot)

Sets the direction of light.

```
void setLight(vec3 position, rot rotation)
```

## Parameters

#### `position`
Type: vec3

Currently unused.

#### `rotation`
Type: rot

The direction of light.

## Remarks

If [rotation](#rotation) is NaN (0 / 0), inf (1 / 0) or -inf (-1 / 0) there will be no shadows.

