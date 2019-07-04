// Write your JavaScript code.
//הצגת מוצרים לפי שם מוצר מסוים בעת הקלדה
$("#searchBox").keyup(function () {
    var form = $('#searchForm')
    var url = form.attr('action')
    $.ajax({
        url: url,
        data: form.serialize(),
        success: function (data) {
            $('#Product').html(data);
            //$('#Product').html('');
            //for (var i = 0; i < data.length; i++) {
            //    $('#Product').append(' <div class="col-md-4"><div class="product-item"><img src="/imagesweb/' + data[i].imgId + '"/><div class="product-info"><a href="/Productes/Details/' + data[i].id + '">' + data[i].productName + '</a><p>' + data[i].price + '</p></div></div></div>') // show response from the php script.
            //}
        }
    });
});

//$.ajax({
//    url: "Colors/Search?id" + colorid,
//    method: "post",
//    async: false,
//    data: { colorid: colorid },
//    success: function (data) {
//        $("#Product").html(data)
//    }
//});