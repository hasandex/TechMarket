$(document).ready(function () {
    $(".btn-add-to-cart").click(function () {
        var productId = $(this).data("product-id");
        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: { productId: productId },
            success: function (response) {
                if (response.success == "added") {
                    
                    alert("Product added to cart successfully!");
                } else if (response.success == "isExisted") {
                   
                    alert("the product is already in your cart.");
                }
                else {
                    alert("failed to add this product!");
                }
            },
            error: function () {
                alert("An error occurred while adding the product to the cart! \nmake sure you are logged in :)");
            }
        });
    });
});