namespace pos.core
{
    public enum StatusCode
    {
        Wholesale_price_should_not_greater_than_saleprice = 1,
        Product_name_already_exist = 2,
        Shopping_cart_product_price_mis_match = 3,
        Shopping_cart_product_sku_mis_match = 4,
        Sku_already_exist = 5,
        Sku_must_not_contains_pos_prefix = 6,
    }
}
