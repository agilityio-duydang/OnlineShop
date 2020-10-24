var AjaxShoppingCart = {
    init: function ()
    {
        AjaxShoppingCart.regEvents();
    },
    setLoadWaiting: function (display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },

    addproductocart: function (productId, quantity, isAddToCartButton) {
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: "/Cart/AddProductToCartAjax",
            data: {
                productId: productId,
                quantity: quantity,
                isAddToCartButton: isAddToCartButton
            },
            type: 'POST',
            success: function (res) {
                if (res.success == true) {
                    $('.ajaxCart').html(res.html);
                    $('.mobile-flyout-wrapper').html(res.flyoutShoppingCart);
                    $('.cart-qty').html(res.headerCart);
                    $('.ajaxCart').css('display', 'block');
                };
            },
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    addproductowishlist: function (productId, shoppingCartTypeId, quantity) {
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: "/WishList/Add",
            data: {
                productId: productId,
                shoppingCartTypeId: shoppingCartTypeId,
                quantity: quantity
            },
            type: 'POST',
            success: function (res) {
                if (res.success == true) {
                    $('.ajaxCart').html(res.html);
                    $('.wishlist-qty').html(res.headerWishList);
                    $('.ajaxCart').css('display', 'block');
                };
            },
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },
    addproductocompare: function (productId) {
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: "/CompareProducts/Add",
            data: {
                productId: productId
            },
            type: 'POST',
            success: function (res) {
                if (res.success == true) {
                    displayBarNotification(res.Message, 'success', 3500);
                };
            },
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },
    resetLoadWaiting: function () {
        AjaxShoppingCart.setLoadWaiting(false);
    },
    regEvents: function () {
        $('.continue-shopping-button').off('click').click('click', function () {
            window.location.href = '/';
        });

        $('.checkout-button').off('click').click('click', function () {
            window.location.href = "/CheckOut/BillingAddress";
        });

        $('.update-cart-button').off('click').click('click', function () {
            var listProduct = $('.qty-input');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity : $(item).val(),
                    Product: {
                        Id : $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href = "/cart"
                    }
                }
            });
        });

        $('.clear-cart-button').off('click').click('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href = "/cart"
                    }
                }
            });
        });

        $('.btnDelete').off('click').click('click', function (e) {
            e.preventDefault()
            $.ajax({
                url: '/Cart/Delete',
                data: { Id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.Status == true) {
                        window.location.href = "/cart"
                    }
                }
            });
        });
    }
}
AjaxShoppingCart.init();