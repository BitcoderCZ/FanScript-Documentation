
# Out

Instead of the argument being passed to the function, it is "returned" from it.

```
out
```

## Targets

The modifier can be applied to:
 - Argument
 - Parameter

## Conflicting modifiers

These modifiers cannot be used with this one:
 - ref
 - readonly

## Examples

``` fcs
func add(float a, float b, out float res)
{
    res = a + b
}

float resA
add(2, 5, out resA)
inspect(resA)

// if a parameter has the out modifier, expression variable decleration can be used
add(30, 25, out float resB)
inspect(resB)

// if you don't need the value of the out parameter, you can use a discard
add(13, 22, out _)
```


