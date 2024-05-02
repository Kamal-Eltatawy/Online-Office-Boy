function updateCartBadge() {
    var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
    var badge = document.getElementById("cartBadge");
    if (badge) {
        badge.textContent = cartItems.length.toString();
    }
}

function addToCart(productId, productName, price, pictureurl) {
    var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];

    var existingProduct = cartItems.find(item => item.productId === productId);

    if (existingProduct) {
        existingProduct.quantity++;
    } else {
        cartItems.push({
            productId: productId,
            productName: productName,
            price: price,
            quantity: 1,
            pictureurl: pictureurl
        });
    }

    localStorage.setItem("cartItems", JSON.stringify(cartItems));

    updateCartBadge();

}

function handleAddToCart(productId, productName, price, isAvailable, pictureurl) {
    if (isAvailable) {
        addToCart(productId, productName, price, pictureurl);
    } else {
        // Display Bootstrap alert for not available products
        var alertDiv = document.createElement("div");
        alertDiv.className = "alert alert-danger alert-dismissible fade show";
        alertDiv.setAttribute("role", "alert");
        alertDiv.textContent = "This product is not available. You cannot add it to the cart.";

        var closeButton = document.createElement("button");
        closeButton.type = "button";
        closeButton.className = "close";
        closeButton.setAttribute("data-dismiss", "alert");
        closeButton.innerHTML = "<span aria-hidden='true'>&times;</span>";

        alertDiv.appendChild(closeButton);

        var alertContainer = document.getElementById("alertContainer");
        alertContainer.innerHTML = "";
        alertContainer.appendChild(alertDiv);
    }
}

var addToCartButtons = document.querySelectorAll(".add-to-cart-btn");
addToCartButtons.forEach(function (button) {
    button.addEventListener("click", function (event) {
        
        var productId = event.target.dataset.productid;
        var productName = event.target.dataset.productname;
        var price = event.target.dataset.price;
        var isAvailable = event.target.dataset.isavailable === "True";
        var pictureurl = event.target.dataset.pictureurl;

        handleAddToCart(productId, productName, price, isAvailable, pictureurl);
    });
});


