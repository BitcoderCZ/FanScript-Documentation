# Set<>(array, float, generic)

Sets [array](#array) at [index](#index) to [value](#value).

```
void set<>(this array<> array, float index, generic value)
```

## Parameters

#### `array`
Type: array<>

#### `index`
Type: float

#### `value`
Type: generic

## Examples

``` fcs
array<float> arr
on Loop(0, 5, out inline float i)
{
    arr.set(i, i + 1)
}
// arr - [1, 2, 3, 4, 5]
```

