
# Readonly

Makes the variable/parameter readonly - can be assigned only once.

Can be applied to all types of varibles.

```
readonly
```

## Targets

The modifier can be applied to:
 - Variable
 - Parameter

## Conflicting modifiers

These modifiers cannot be used with this one:
 - const
 - ref
 - out

## Remarks

 - Readonly vairables need to be initialized.

## Examples

``` fcs
readonly float a = 5 // works

readonly float b // error - A readonly/constant variable needs to be initialized.
```


