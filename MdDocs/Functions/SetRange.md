# SetRange<>(array, float, arraySegment)

Sets a range of [array](#array), starting at [index](#index).

```
void setRange<>(this array<> array, float index, arraySegment<> value)
```

## Parameters

#### `array`
Type: array<>

#### `index`
Type: float

#### `value`
Type: arraySegment<>

## Examples

``` fcs
array<float> arr
arr.setRange(2, [1, 2, 3])
// arr - [0, 0, 1, 2, 3]
```

