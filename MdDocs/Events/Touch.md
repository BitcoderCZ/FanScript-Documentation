
# Touch(float, float, float, float)

Executes when a touch is detected.

```
on touch(out float screenX, out float screenY, const float TOUCH_STATE, const float TOUCH_FINGER) { }
```

## Parameters

#### `screenX`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [float](/MdDocs/Types/Float.md)

The x coordinate of the touch (in pixels).

#### `screenY`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [float](/MdDocs/Types/Float.md)

The y coordinate of the touch (in pixels).

#### `TOUCH_STATE`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

One of [TOUCH_STATE](/MdDocs/Constants/TOUCH_STATE.md).

#### `TOUCH_FINGER`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

One of [TOUCH_FINGER](/MdDocs/Constants/TOUCH_FINGER.md).


