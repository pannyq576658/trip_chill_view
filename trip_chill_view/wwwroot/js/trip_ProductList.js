var productElement = `<div class="col-lg-3 col-sm-6 col-12 mt-sm-4 mt-3">
        <div class="col-group-video-card">
            <a href="/productList/[productID]" class="p-sm-0 p-2">
                <div class="col-group-video-card-head d-flex flex-sm-column flex-row">
                    <div class="col-group-video-card-img" style="background-image:url([background]);">
                        <!--<span class="video-ico"><i class="fas fa-play-circle"></i></span>-->
                    </div>
                    <div class="video-flex d-flex flex-column justify-content-sm-between pb-sm-0 px-lg-2 px-md-3 px-sm-2 pt-sm-2 pl-2">
                        <h3 class="col-group-video-card-title">[name]</h3>

                        <!--<div id="ctl00_ContentPlaceHolder1_ucHotCourse_Repeater1_ctl00_ctype3" class="col-group-video-card-tag d-inline-block"><span class="">影 音</span></div>-->

                    </div>
                </div>
                <div class="col-group-video-card-body pt-sm-0 px-lg-2 px-md-3 px-sm-2 pb-sm-2 pb-0 pt-2">
                    <div class="col-group-video-card-footer d-flex justify-content-between  align-items-lg-end align-items-sm-end align-items-center">
                        <div class="d-flex flex-sm-column">

                        </div>
                        <div class="d-flex justify-content-between flex-row align-items-baseline flex-sm-column">
                            <div class="col-group-video-card-text-discount invisiblee-320"></div>
                            <div class="col-group-video-card-text-price pl-sm-0 pl-2">NT$ [price]</div>
                        </div>
                    </div>
                </div>
            </a>
        </div>
    </div>`;
var productJson = [];
var cartAddHtml = `<a href="javascript:checkOutTracker(true);">
    <div class="more-button3"><i class="fas fa-shopping-cart pr-2"></i>已在購物車, 結帳去</div>
</a >`;
var total_page = 0;
var layer_page = 0;
var currentlyPage = 0;
var fa_angle_left = false;
var fa_angle_right = false;
var page_link_last = false;
var page_link_next = false;
function page(num) {
    if (loading.loadingTask == 0)
        loading.Startup();
    else
        loading.TaskAdd()
    $('.productListRow').empty();
    currentlyPage = num;
   var preNum = $('#ctl00_ContentPlaceHolder1_listDP .page-link-color').text();
    var html = $('#ctl00_ContentPlaceHolder1_listDP').html().replace('<span class="page-link-color">' + preNum + '</span>', '<a class="page-link" href="javascript:page(' + preNum + ')">' + preNum+'</a>').replace('<a class="page-link" href="javascript:page(' + num + ')">' + num + '</a>', '<span class="page-link-color">' + num +'</span>');
    $('#ctl00_ContentPlaceHolder1_listDP').html(html);
    updata_arrow_enable();
   // $("<a class='page-link' href='javascript:page(" + currentlyPage + ")' >" + currentlyPage+"</a>").toggleClass('disabled1');
    $.get(route + '/api/product/' + num).done(function (result, textStatus, jqXHR) {
        productJson = result.data;
        $.each(productJson, function (index, value) {
            var el = productElement.replace('[productID]', value['productID']).replace('[background]', value['background']).replace('[name]', value['name']).replace('[price]', AppendComma(value['price']));
            $('.productListRow').append(el);
        });
        loading.TaskSub()
    });
}
function page_left() {
    var num = Number($('#ctl00_ContentPlaceHolder1_listDP .page-link-color').text());
    if (currentlyPage % 5 == 1)
        listDP_element(--layer_page);
   // currentlyPage--;
    page(num-1);    
}
function page_right() {
    var num = Number($('#ctl00_ContentPlaceHolder1_listDP .page-link-color').text());
    if (currentlyPage % 5 == 0)
        listDP_element(++layer_page);
  //  currentlyPage++;
    page(num+1);    
}
function page_next() {   
    listDP_element(++layer_page);
    currentlyPage = layer_page * 5 + 1;
    page(currentlyPage);
}
function page_last() {
    listDP_element(--layer_page);
    currentlyPage = layer_page * 5 + 1;
    page(currentlyPage);
}
function listDP_element(layer_page) {
    var html =`<a class="page-link  fas fa-angle-left" href="javascript:page_left()"></a>&nbsp;
    <a class="page-link d-none d-md-inline-block page-link-last" href="javascript:page_last()">
    <i class="fas fa-angle-double-left d-none d-md-inline-block"></i></a>&nbsp;
    <span class="page-link-color">${layer_page * 5 + 1}</span>&nbsp;
    <a class="page-link" href="javascript:page(${layer_page * 5 + 2})">${layer_page * 5 + 2}</a>&nbsp;
    <a class="page-link" href="javascript:page(${layer_page * 5 + 3})">${layer_page * 5 + 3}</a>&nbsp;
    <a class="page-link" href="javascript:page(${layer_page * 5 + 4})">${layer_page * 5 + 4}</a>&nbsp;
    <a class="page-link" href="javascript:page(${layer_page * 5 + 5})">${layer_page * 5 + 5}</a>&nbsp;&nbsp;
    <a class="page-link d-none d-md-inline-block page-link-next" href="javascript:page_next()">
        <i class="fas fa-angle-double-right d-none d-md-inline-block"></i>
    </a>&nbsp;
    <a class="page-link fas fa-angle-right" href="javascript:page_right()"></a>&nbsp;`;
    $('#ctl00_ContentPlaceHolder1_listDP').html(html);
}
function arrow_set_enable() {
    if (currentlyPage == 1 && fa_angle_left)
        fa_angle_left = false;
    else if (currentlyPage > 1 && !fa_angle_left)
        fa_angle_left = true;

    if (layer_page == 0 && page_link_last)
        page_link_last = false;
    else if (layer_page > 0 && !page_link_last)
        page_link_last = true;


    if (currentlyPage >= total_page && fa_angle_right)
        fa_angle_right = false;
    else if (currentlyPage < total_page && !fa_angle_right)
        fa_angle_right = true;

    if (layer_page == (Math.ceil(total_page / 5) - 1) && page_link_next)
        page_link_next = false;

    else if (layer_page < (Math.ceil(total_page / 5) - 1) && !page_link_next)
        page_link_next = true;

    
}
function updata_arrow_enable() {
    arrow_set_enable();
    if (!page_link_next)
        $('#ctl00_ContentPlaceHolder1_listDP .page-link-next').addClass('disabled1');

    if (page_link_next)
        $('#ctl00_ContentPlaceHolder1_listDP .page-link-next').removeClass('disabled1');


    if (!page_link_last)
        $('#ctl00_ContentPlaceHolder1_listDP .page-link-last').addClass('disabled1');

    if (page_link_last)
        $('#ctl00_ContentPlaceHolder1_listDP .page-link-last').removeClass('disabled1');


    if (!fa_angle_right)
        $('#ctl00_ContentPlaceHolder1_listDP .fa-angle-right').addClass('disabled1');

    if (fa_angle_right)
        $('#ctl00_ContentPlaceHolder1_listDP .fa-angle-right').removeClass('disabled1');


    if (!fa_angle_left)
        $('#ctl00_ContentPlaceHolder1_listDP .fa-angle-left').addClass('disabled1');

    if (fa_angle_left)
        $('#ctl00_ContentPlaceHolder1_listDP .fa-angle-left').removeClass('disabled1');


    
}
function checkOutTracker(v) {
    if (v)
       top.location.href = '/cart';
}
function mainProductRun() {
    loading.TaskAdd()
    $('.productListRow').empty();
    listDP_element(layer_page);
    $.get(route + '/api/product/productNum').done(function (result, textStatus, jqXHR) {
        total_page = (Number(result.data) % 10 == 0) ? Math.floor(Number(result.data) / 10) : Math.floor(Number(result.data) / 10) + 1;
        currentlyPage = 1;
        fa_angle_left = false;
        fa_angle_right = true;
        page_link_last = false;
        page_link_next = true;
        page(currentlyPage);       
        loading.TaskSub()
    });        
}
function mainProductDetailRun(parameter) {
    loading.TaskAdd()
    $('#ctl00_ContentPlaceHolder1_UpdatePanel4 .xlinker').attr('href', 'https://tripview240306.azurewebsites.net/productList/' + parameter);
    
    $.get(route + '/api/product/getProduct/' + parameter).done(function (result, textStatus, jqXHR) {
        console.log(result.data);
        $('.intro-title h1').text(result.data.name);
        $('#productDetailBreadcrumb .active').text(result.data.name);
        $('#shareModalcoupon .modal-content .modal-body .fbShare').attr('href', "javascript:void(window.open('https://www.facebook.com/sharer/sharer.php?u='+encodeURIComponent('https://tripview240306.azurewebsites.net/productList/" + parameter + "?utm_medium=ShareButton&amp;utm_source=Facebook&amp;utm_campaign=" + parameter + "&amp;utm_content=" + result.data.name + "')));");
        $('#shareModalcoupon .modal-content .modal-body .lineShare').attr('href', "javascript:void(window.open('https://social-plugins.line.me/lineit/share?url='+encodeURIComponent('https://tripview240306.azurewebsites.net/productList/" + parameter + "?utm_medium=ShareButton&amp;utm_source=LINE&amp;utm_campaign=" + parameter + "&amp;utm_content=" + result.data.name + "')));");
        $('.price .pr-2').text(AppendComma(result.data.price));
        $('#productDetailImg img').attr('src', result.data.background);
        $('.w360_buyers div span').html('&nbsp;' + result.data.buyTimeNum + '&nbsp;');
        $.get(route + '/api/cart/CartItemExist/' + parameter + '/' + member.id).done(function (result1, textStatus, jqXHR) {
            if (result1.data == true)
                $('#ctl00_ContentPlaceHolder1_UpdatePanel2').html(cartAddHtml);
            loading.TaskSub()
        });
    });

    $('#ctl00_ContentPlaceHolder1_btnAdd2Cart4').click(function () {
        var memberJson = { 'productID': parameter, 'ownerID': member.id };
        $.ajax({
            url: route +'/api/cart/addCart',
            data: JSON.stringify(memberJson),
            contentType: "application/json;charset=utf-8",
            type: "POST",
            success: function (response) {
               // console.log(response)
                if (response.status == 1) {
                    if (response.data == false) {
                        member.setDataProperty('cartNum', (parseInt(member.cartNum) + 1));
                        $('.badge').text(member.cartNum);
                        alert('已經成功加入購物車了');
                        $('#ctl00_ContentPlaceHolder1_UpdatePanel2').html(cartAddHtml);
                    }
                    else
                        alert('已存在購物車裡');
                }
                else {//exception
                    alert(response.msg);
                }
            }
        });
    });

    $('#ctl00_ContentPlaceHolder1_btnAdd2Cart3').click(function () {
        var memberJson = { 'productID': parameter, 'ownerID': member.id };
        $.ajax({
            url: route +'/api/cart/addCart',
            data: JSON.stringify(memberJson),
            contentType: "application/json;charset=utf-8",
            type: "POST",
            success: function (response) {
                if (response.data == false) {
                    member.setDataProperty('cartNum', (parseInt(member.cartNum) + 1));
                    $('.badge').text(member.cartNum);
                    alert('已經成功加入購物車了');
                }
                else
                    alert('已存在購物車裡');
                top.location.href = '/cart';
            }
        });
    });

}


