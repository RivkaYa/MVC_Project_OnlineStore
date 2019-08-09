// Write your JavaScript code.
var selectedCategory = 0;


//הצגת מוצרים לפי שם מוצר מסוים בעת הקלדה
$("#searchBox").keyup(function () {
    FilterProductsByParams();   
});

$('*[id^="Category-"]').click(function () {

    selectedCategory = $(this).attr('value');   
    FilterProductsByParams();
});


//$('#category-menu').menu(function () {
//      //items: "> :not(.ui-widget-header)",
//      //select: function(event, ui) {
//    //      ui.item.addClass("selected").siblings().removeClass("selected")

//    FilterProductsByParams();
// });


function FilterProductsByParams()
{
    var form = $('#searchForm')
    selectedCategory = selectedCategory;
    var searchBoxText = document.getElementById("searchBox").value;

    //var max = document.getElementById('slider-range-value2').value;
    //var min = document.getElementById('slider-range-value1').value;
    var maxPrice = document.getElementsByName('max-value').value;
    var minPrice = document.getElementsByName('min-value').value;
    $.ajax({
        url: '/Products/IndexPartial',
        data: { searchBox: searchBoxText, categoryID: selectedCategory, minPrice: minPrice, maxPrice: maxPrice },
        async:true,
        success: function (response) {
            $('#Product').html(response);
            //$('#Product').html('');
            //for (var i = 0; i < data.length; i++) {
            //    $('#Product').append(' <div class="col-md-4"><div class="product-item"><img src="/imagesweb/' + data[i].imgId + '"/><div class="product-info"><a href="/Productes/Details/' + data[i].id + '">' + data[i].productName + '</a><p>' + data[i].price + '</p></div></div></div>') // show response from the php script.
            //}
        },
        error: function (response) {
            alert("couldn't filter. ");
        }
        
    });
}

////var check = 0;
////צריכה לשנות ששתיהם ילכו לטבלת connect   כי אחרת אין קשר ביניהם.
//$('input[value=color]').change(function () {
//    if ($(this).prop("checked")) {
//        //do the stuff that you would do when 'checked'
//        var colorid = $(this).attr("id")
//        $.ajax({
//            url: "Colors/Search?id" + colorid,
//            method: "post",
//            async: false,
//            data: { colorid: colorid },
//            success: function (data) {
//                $("#Product").html(data)
//            }
//        });
//        return;
//    }

//$.ajax({
//    url: "Colors/Search?id" + colorid,
//    method: "post",
//    async: false,
//    data: { colorid: colorid },
//    success: function (data) {
//        $("#Product").html(data)
//    }
//});