$(document).ready(function () {
    $(".increaseLikes").click(function () {
        var commentId = $('#commentId').val();
        $.ajax({
            url: "/User/IncreaseLikes",
            type: "POST",
            data: { commentId: commentId },
            success: function (response) {
                if (response.success == "ok") {
                    alert("liked!");
                } else if (response.success == "isExisted") {

                    alert("!");
                }
                else {
                    alert("failed to like this product!");
                }
            },
            error: function () {
                alert("An error occurred while adding the product to the cart! \nmake sure you are logged in :)");
            }
        });
    });
});