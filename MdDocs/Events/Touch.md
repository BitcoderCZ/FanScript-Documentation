# Touch(float, float, float, float)

Executes when a touch of index [TOUCH_FINGER](#TOUCHFINGER) is detected.

```
on touch(out float screenX, out float screenY, const float TOUCH_STATE, const float TOUCH_FINGER) { }
```

## Parameters

#### `screenX`
Type: float

The x coordinate of the touch (in pixels).

#### `screenY`
Type: float

The y coordinate of the touch (in pixels).

#### `TOUCH_STATE`
Type: float
Value must be constant.


One of [TOUCH_STATE](/MdDocs/Constants/TOUCH_STATE.md).

#### `TOUCH_FINGER`
Type: float
Value must be constant.


One of [TOUCH_FINGER](/MdDocs/Constants/TOUCH_FINGER.md).
