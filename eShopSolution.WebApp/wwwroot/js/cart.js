var CartController = function () {
    this.initialize = function () {
        loadData();

        registerEvents();
    }

    function registerEvents() {
        $('body').on('click', '.btn_plus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#quantity_' + id).val()) + 1;

           
            console.log(quantity)
            updateCart(id, quantity);
        });

        $('body').on('click', '.btn_minus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#quantity_' + id).val()) -1 ;
            console.log(quantity)
            updateCart(id, quantity);
        });
        $('body').on('click', '.btn_remove', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            updateCart(id, 0);
        });
    }

    function updateCart(id, quantity) {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "POST",
            url: "/" + culture + '/Cart/UpdateCart',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                $('#lbl_number_items_header').text(res.length);
                loadData();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function loadData() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "GET",
            url: "/" + culture + "/Cart/GetListItems",
            success: function (res) {
                console.log(res)
                var html = '';
                var total = 0;
                $.each(res, function (i, item) {
                    var amount = item.price * item.quantity;
                    html += " <tr>"
                        + "<td class=\"product-thumbnail\" >"
                        + "<img src=\"" + item.image + "\" alt=\"Image\" class=\"img-fluid\">"
                        + "</td>"
                        + "<td class=\"product-name\">"
                        + "<h2 class=\"h5 text-black\">" + item.name + "</h2>"
                        + "</td>"
                        + " <td>" + item.price + "</td>"
                        + "<td>"

                        + " <div class=\"input-group mb-3\" style=\"max-width:120px;\">"
                        + "<div class=\"input-group-prepend\">"
                        + " <button class=\"btn btn-outline-primary js-btn-minus btn_minus\" data-id=\""+ item.productId + "\" type=\"button\" >&minus;</button>"
                        + "</div>"
                        + "<input type=\"text\" id=\"quantity_" + item.productId + "\" class=\"form-control text-center\" value=\"" + item.quantity + "\" placeholder =\"\" aria-label=\"Example text with button addon\" aria-describedby=\"button-addon1\">"
                        + "<div class=\"input-group-append\">"
                        + " <button class=\"btn btn-outline-primary js-btn-plus btn_plus\" data-id=\"" + item.productId + "\" type=\"button\">&plus;</button>"
                        + "</div>"
                        + " </div>"
                        + "  </td>"
                        + " <td>" + amount + "</td>"
                        + "<td><a href=\"#\" class=\"btn btn-primary btn-sm btn_remove\"  data-id=\"" + item.productId + "\">X</a></td>"
                        + "</tr > "
                    total += amount
                });
                $('#card_body').html(html);
                $('#lbl_total').text(total);
                $('#lbl_number_of_items').text(res.length);
            }
        })
    }
}