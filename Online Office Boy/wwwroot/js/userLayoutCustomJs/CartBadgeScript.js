function updateCartBadge() {
    var cartItems = JSON.parse(localStorage.getItem("cartItems")) || [];
    var badge = document.getElementById("cartBadge");
    if (badge) {
        badge.textContent = cartItems.length.toString();
    }
}

document.addEventListener("DOMContentLoaded", function () {
    updateCartBadge();
});

