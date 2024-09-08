# SetLocked(object, vec3, vec3)

Restricts [object](#object)'s movement, forces applied to [object](#object) are multiplied by [position](#position) and [rotation](#rotation).

```
void setLocked(this object object, vec3 position, vec3 rotation)
```

## Parameters

#### `object`
Type: object

The object to restrict the movement of.

#### `position`
Type: vec3

The movement multiplier.

#### `rotation`
Type: vec3

The rotation multiplier.

## Remarks

Negative numbers reverse physics and numbers bigger than 1 increase them.

