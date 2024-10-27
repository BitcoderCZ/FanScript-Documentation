

# MenuItem(float, obj, string, float, float)

Adds an item to the shop. Info about the shop can be found [here](https://www.fancade.com/wiki/script/how-to-use-the-shop-system).

```
void menuItem(ref float variable, obj picture, const string NAME, const float MAX_ITEMS, const float PRICE_INCREASE)
```

## Parameters

#### `variable`
Modifiers: [ref](/MdDocs/Modifiers/Ref.md)

Type: [float](/MdDocs/Types/Float.md)

Which variable to store the value of times bought in, should have [saved](/MdDocs/Modifiers/Saved.md) modifier.

#### `picture`
Type: [obj](/MdDocs/Types/Obj.md)

Which object to display for the item.

#### `NAME`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [string](/MdDocs/Types/String.md)

Name of the item.

#### `MAX_ITEMS`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

Maximum number of times the item can be bought, can be 2-100 or one of [MAX_ITEMS](/MdDocs/Constants/MAX_ITEMS.md).

#### `PRICE_INCREASE`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

Specifies what the initial price is and how it increases, one of [PRICE_INCREASE](/MdDocs/Constants/PRICE_INCREASE.md).

## Related

 - [shopSection(string)](/MdDocs/Functions/Game/ShopSection.string.md)


