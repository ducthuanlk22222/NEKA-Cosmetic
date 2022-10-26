var CartController = function () {
    this.initialize = function () {
        loadData();
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
                        + " <button class=\"btn btn-outline-primary js-btn-minus\" type=\"button\">&minus;</button>"
                        + "</div>"
                        + "<input type=\"text\" id=\"txt_quantity_" + item.id + "\" class=\"form-control text-center\" value=\"1\" placeholder=\"\" aria-label=\"Example text with button addon\" aria-describedby=\"button-addon1\">"
                            + "<div class=\"input-group-append\">"
                            + " <button class=\"btn btn-outline-primary js-btn-plus\" type=\"button\">&plus;</button>"
                            + "</div>"
                            + " </div>"
                            + "  </td>"
                            + " <td>" + amount + "</td>"
                            + "<td><a href=\"#\" class=\"btn btn-primary btn-sm\">X</a></td>"
                        + "</tr > "
                    total += amount
                });
                $('#card_body').html(html);
                $('#lbl_total').text(total);
            }
        })
    }
}