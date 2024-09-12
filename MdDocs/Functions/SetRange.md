# SetRange<>(array, float, arraySegment)

Sets a range of [array](#array), starting at [index](#index).

```
void setRange<>(this array array, float index, arraySegment value)
```

## Parameters

#### `array`
Type: [array](/MdDocs/Types/Array.md)

#### `index`
Type: [float](/MdDocs/Types/Float.md)

#### `value`
Type: [arraySegment](/MdDocs/Types/ArraySegment.md)

## Examples

``` fcs
array<float> arr
arr.setRange(2, [1, 2, 3])
// arr - [0, 0, 1, 2, 3]
```

