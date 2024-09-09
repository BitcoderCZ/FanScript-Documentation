# MenuItem(float, obj, string, float, float)

Adds an item to the [shop](https://www.fancade.com/wiki/script/how-to-use-the-shop-system).

```
void menuItem(ref float variable, obj picture, string NAME, float MAX_ITEMS, float PRICE_INCREASE)
```

## Parameters

#### `variable`
Type: float

Which variable to store the value of times bought in, should have saved modifier.

#### `picture`
Type: obj

Which object to display for the item.

#### `NAME`
Type: string

Name of the item, must be constant.

#### `MAX_ITEMS`
Type: float

Maximum number of times the item can be bought, can be 2-100 or one of [MAX_ITEMS](/MdDocs/Constants/MAX_ITEMS.md), must be constant.

#### `PRICE_INCREASE`
Type: float

Specifies what the initial price is and how it increases, one of [PRICE_INCREASE](/MdDocs/Constants/PRICE_INCREASE.md), must be constant.

## Related

 - [shopSection(string)](/MdDocs/Functions/Game/ShopSection.md)

