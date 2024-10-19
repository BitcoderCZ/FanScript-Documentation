

# Get<>(array, float)

Returns the value (reference) from [array](#array) at [index](#index).

```
generic get<>(this array array, float index)
```

## Parameters

#### `array`
Type: [array](/MdDocs/Types/Array.md)

#### `index`
Type: [float](/MdDocs/Types/Float.md)

## Returns

generic

The value at [index](#index).

## Examples

``` fcs
array<float> arr = [3, 2, 1]
inspect(arr.get(1)) // 2
```


