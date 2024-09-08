# SetPtrValue<>(generic, generic)

Sets the value of a variable/array element.

```
void setPtrValue<>(generic pointer, generic value)
```

## Parameters

#### `pointer`
Type: generic

The variable/array element to set.

#### `value`
Type: generic

The value to set [pointer](#pointer) to.

## Examples

``` fcs
array<float> arr
// ...
inline float first = arr.get(0)
inspect(first)
setPtrValue(first, 10)
// arr - [10, ...]
```

