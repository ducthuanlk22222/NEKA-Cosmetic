$('body').on('click', '.btn-add-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const culture = $('#hidCulture').val();
    $.ajax({
        type: "POST",
        dataType: 'json',
        url: "/" + culture + '/Cart/AddToCart',
        data: {
            id: id,
            languageId: culture
        },
        success: function (res) {
            console.log(res)
        },
    })
})