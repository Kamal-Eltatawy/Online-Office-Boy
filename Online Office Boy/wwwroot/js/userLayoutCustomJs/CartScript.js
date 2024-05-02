
function renderCartItems() {
    var cartItems = JSON.parse(localStorage.getItem("cartItems"));
    var cartTable = document.getElementById("cartTable");

    if (cartItems && cartItems.length > 0) {
        var tbody = cartTable.querySelector("tbody");
        tbody.innerHTML = "";
        cartItems.forEach(function (item, index) {
            var row = "<tr>";
            row += "<td class='thumbnail-img'><img class='img-fluid' src='/" + item.pictureurl + "' alt='" + item.productName + "' class='product-image'></td>";
            row += "<td>" + item.productName + "</td>";
            row += "<td>" + item.price + "</td>";
            row += "<td><input type='number' value='" + item.quantity + "' min='1' data-index='" + index + "' class='quantity-input form-control form-control-sm'></td>";
            //row += "<td><input type='checkbox' id='checkbox1' " + (item.isPaid ? "checked" : "") + " data-index='" + index + "' class='paid-checkbox form-check'></td>";
            row += "<td><input type='checkbox' id='checkbox2' " + (item.isGuest ? "checked" : "") + " data-index='" + index + "' class='guest-checkbox form-check'></td>";

            row += "<td><select id='officeSelect-" + index + "' class='office-select form-control form-control-sm'></select></td>";

            row += "<td><select id='officeBoySelect-" + index + "' class='office-boy-select form-control form-control-sm'></select></td>";

            row += "<td><select id='departmentSelect-" + index + "' class='department-select form-control form-control-sm'></select></td>";

            row += "<td><button class='remove-button btn mt-2 btn-danger' data-index='" + index + "' onclick='removeItem(" + index + ")'>Remove</button></td>";
            row += "<td><input type='number' hidden value='" + item.productId + "' min='1' data-index='" + index + "' class='id-input form-control form-control-sm'></td>";
            row += "</tr>";

            tbody.innerHTML += row;
        });

        document.getElementById("checkoutBtn").removeAttribute("disabled");

        fetchAndRenderOffices();
        fetchAndRenderDestintationOffices();

    } else {
        cartTable.innerHTML = "<p>Your cart is empty.</p>";
        document.getElementById("checkoutBtn").setAttribute("disabled", "disabled");
    }
}

function fetchAndRenderOffices() {
    var rowOfficeSelects = document.querySelectorAll('.office-select');
    var promises = []; 

    rowOfficeSelects.forEach(function (select, index) {
        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Office/GetOffices',
                method: 'GET',
                success: function (response) {
                    if (response.code === 1) {
                        var offices = response.offices;

                        renderOfficeSelect(offices, select);
                        resolve(); 
                    } else {
                        reject('Error fetching offices.');
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                    reject('Error fetching offices.');
                }
            });
        });
        promises.push(promise);
    });

    return Promise.all(promises);
}
function renderOfficeSelect(offices, select) {
    var optionSelect = document.createElement('option');
    optionSelect.value = 0;
    optionSelect.textContent = "Select Office";
    select.appendChild(optionSelect);
    offices.forEach(function (office) {
        var option = document.createElement('option');
        option.value = office.id;
        option.textContent = office.location;
        select.appendChild(option);
    });


    select.style.display = 'block';

    select.addEventListener('change', function () {
        var officeId = this.value;

        var officeBoySelect = this.closest('tr').querySelector('.office-boy-select');
        fetchOfficeBoys(officeId, officeBoySelect);
    });
}

function fetchOfficeBoys(officeId, officeBoySelect) {
    if (officeId == 0) {
        renderOfficeBoySelect([], officeBoySelect);
        return; 
    }

    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/User/GetOfficeBoys',
            method: 'GET',
            data: { officeId: officeId },
            success: function (response) {
                if (response.code === 1) {
                    var officeBoys = response.officeboys;

                    renderOfficeBoySelect(officeBoys, officeBoySelect);
                    resolve(); 
                } else {
                    reject('Error fetching office boys.');
                }
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
                reject('Error fetching office boys.');
            }
        });
    });
}
function renderOfficeBoySelect(officeBoys, officeBoySelect) {
    officeBoySelect.innerHTML = '';
    var optionSelect = document.createElement('option');
    optionSelect.value = 0;
    optionSelect.textContent = "Select Office Boy";
    officeBoySelect.appendChild(optionSelect);
    officeBoys.forEach(function (officeBoy) {
        var option = document.createElement('option');
        option.value = officeBoy.id;
        option.textContent = officeBoy.name;
        officeBoySelect.appendChild(option);
    });
}

function fetchAndRenderDestintationOffices() {
    var rowDepartmentSelects = document.querySelectorAll('.department-select');
    rowDepartmentSelects.forEach(function (select, index) {
        $.ajax({
            url: '/Office/GetAll',
            method: 'GET',
            success: function (response) {
                if (response.code === 1) {
                    var departments = response.departments;

                    renderDepartmentSelect(departments, select);
                } else {
                    alert('Error fetching departments.');
                }
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
                alert('Error fetching departments.');
            }
        });
    });
}

function renderDepartmentSelect(departments, select) {
    var optionSelect = document.createElement('option');
    optionSelect.value = 0;
    optionSelect.textContent = "Select Department To Delivery";
    select.appendChild(optionSelect);
    departments.forEach(function (department) {
        var option = document.createElement('option');
        option.value = department.id;
        option.textContent = department.name;
        select.appendChild(option);
    });

    select.style.display = 'block';
}

function checkout() {
    var cartItems = [];
    var cartTable = document.getElementById("cartTable");
    var tbody = cartTable.querySelector("tbody");
    var rows = tbody.querySelectorAll("tr");
    var guestTotalPrice = 0;
    var employeeTotalPrice = 0;


    rows.forEach(function (row, index) {
        var item = {};
        var idInput = row.querySelector(".id-input");
        item.ProductId = parseInt(idInput.value);

        var quantityInput = row.querySelector(".quantity-input");
        item.Quantity = parseInt(quantityInput.value);

        var priceCell = row.cells[2];
        var price = parseInt(priceCell.textContent);

        var guestCheckbox = row.querySelector(".guest-checkbox");
        item.IsGuest = guestCheckbox.checked;

        var officeSelect = row.querySelector(".office-select");
        item.OfficeId = parseInt(officeSelect.value);

        var officeBoySelect = row.querySelector(".office-boy-select");
        item.OfficeBoyId = officeBoySelect.value;

        var departmentSelect = row.querySelector(".department-select");
        item.DepartmentId = parseInt(departmentSelect.value);


        if (!item.IsGuest) {
            employeeTotalPrice += price * item.Quantity;
        } else {
            guestTotalPrice += price * item.Quantity;
        }
        


        cartItems.push(item);
    });

    if (cartItems.length > 0) {
        var model = {
            OrderProductsData: cartItems,
            EmployeeTotalPrice: employeeTotalPrice,
            GuestTotalPrice :guestTotalPrice,
        };

        $.ajax({
            url: '/Order/Checkout',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(model),
            success: function (response) {
                if (response.code === 1) {
                    alert("Your Order Created successfully");
                    localStorage.removeItem("cartItems");
                    renderCartItems();
                } else if (response.code === 0) {
                    alert("Validation errors occurred:\n" + response.errors.join("\n"));
                } else if (response.code === -1) {
                    alert("Error occurred during order creation.");
                } else if (response.code === -5) {
                    alert("We are sorry we cant take your order there is no current office boys ");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error Sending the order:", error);
            }
        });
    } else {
        if (errors.length > 0) {
            alert(errors.join("\n"));
        } else {
            alert("Your cart is empty. Please add items before proceeding to checkout.");
        }
    }
}

function removeItem(index) {
    var confirmed = confirm("Are You sure?");
    if (confirmed) {
        var cartItems = JSON.parse(localStorage.getItem("cartItems"));

        cartItems.splice(index, 1);

        localStorage.setItem("cartItems", JSON.stringify(cartItems));

        renderCartItems();
    }


}

document.addEventListener("DOMContentLoaded", function () {
    renderCartItems();
  
});








