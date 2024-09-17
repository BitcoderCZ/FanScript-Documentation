# Constant

Makes the variable constant - when compiled, references to this variable get replaced by it's value.

```
const
```

## Targets

The modifier can be applied to:
 - Variable

## Conflicting modifiers

These modifiers cannot be used with this one:
 - readonly
 - saved
 - inline

## Remarks

- Only constant value can be assigned to variables with this modifier.
- Constant variables need to be initialized.

## Examples

``` fcs
const float a = 5 // works

float b = 2
const float c = b // error - Value must be constant.

const float d // error - A readonly/constant variable needs to be initialized.
```

