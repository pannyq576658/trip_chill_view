var cart_purchase_history_body_element = `<div id="purchase-history-element[element_id]" class="shadow-sm rounded px-3 mb-4 paddinglr-320
                                                                " style="border: 1px solid rgba(46, 73, 76, 0.12); background-color: #fff">
        <div class="my-3 features-edit d-flex align-items-start">

            <div class="w-100 d-flex product-group ">

                <!-- 品項勾選 -->
                <label class="checkboxred">
                    <span id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_lbBox">
                        <input id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_cbCheck" type="checkbox" name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck" checked="checked" onclick="">
                            <span class="checkmark"></span>
                                                        </span>
                                                    </label>

                    <span id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_lbCheck" style="display: none;">Y</span>
                    <input type="hidden" name="ctl00$ContentPlaceHolder[element_id]$Group$ctl00$repShoppingCart$ctl00$hf_prod_00" id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_hf_prod_00" value="DiT000517">

                        <div class="purchase-history-border-width2 d-flex flex-md-row flex-column align-items-center">
                            <div class="purchase-history-border2-01 p-md-2">
                                <!-- 購買課程 -->
                                <div class="d-flex align-items-center">
                                    <div class="">
                                        <a href="[productID]">
                                            <img src="[image]" alt="" class="rounded">
                                                                    </a>
                                                                </div>
                                        <h6 class="px-lg-3 pr-md-2 pl-2 mb-sm-0">
                                            <a href="[productID]">
                                                [name]

                                            </a>
                                        </h6>
                                    </div>
                                </div>

                                <div class="purchase-history-border-width3 d-flex align-items-md-start align-items-end justify-content-between">

                                    <!-- 課程類型 -->
                                    <div class="purchase-history-border2-02 p-md-2 p-1 text-center d-lg-block d-none">
                                        <h6>[type]</h6>
                                    </div>                                   
                                    <div class="purchase-history-border2-04 p-md-2 d-flex align-items-start justify-content-end">
                                        <div class="d-flex align-items-start justify-content-end">

                                            <!-- 金額 -->
                                            <div class="text-right d-flex flex-column">
                                                <h6 class="mr-lg-5">
                                                    <span id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_lblPrice">$[price]</span>
                                                </h6>


                                            </div>

                                            <!-- 刪除 -->
                                            <a id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_lnkBtnRemove" data-toggle="tooltip" data-placement="bottom" title="刪 除" href="javascript:delete1([element_id])">
                                                <i class="fas fa-trash-alt pt-1 d-lg-block d-none"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- 刪除 (手機版按鈕) -->
                        <div class="d-lg-none d-flex position-absolute edit-button-group">
                            <a id="ctl00_ContentPlaceHolder[element_id]_Group_ctl00_repShoppingCart_ctl00_lnkBtnRemove2" class="edit-button">
                                刪 除
                            </a>
                        </div>
                                            </div>

                    <!-- 成功套用到折扣碼 -->
                    <div class="classbundlebgcolor text-secondary rounded d-flex p-2 mb-2" style="display:none!important;">
                        <p class="px-2 mb-0">
                            ★
                        </p>
                        <p class="mb-0">

                        </p>
                    </div>

                    <!-- 無法套用到折扣碼 -->
                    <div class="classbundlebgcolor_02 text-black-50 rounded d-flex p-2 mb-3" style="display:none!important;">
                        <p class="px-2 mb-0">
                            <i class="fas fa-exclamation-circle"></i>
                        </p>
                        <p class="mb-0">

                        </p>
                    </div>


                                        </div>
                <div class="liner" style="display:none!important;"></div>`;

var payOrderItemElement = `<div class="py-3 px-2 bundling_" style="background: #fff;">
    <div class=" features-edit d-flex align-items-start">
        <div class="purchase-history-border-width2 d-flex flex-md-row flex-column align-items-start">

            <div class="purchase-history-border2-01 orderform p-md-2 mb-sm-0 mb-2 mr-lg-0 mr-md-4 mr-0">
                <!-- 購買課程 -->
                <div class="d-flex align-items-start">
                    <div class="">
                        <img src="[pictureUrl]" alt="" class="rounded">
                                                            </div>
                        <h6 class="px-lg-3 pr-md-2  pl-3 mb-sm-0 font-weight-normal" style="font-size: 15px;">
                            [name]

                        </h6>
                    </div>
                </div>

                <div class="purchase-history-border-width3 d-flex align-items-start justify-content-between">

                    <!-- 課程類型 -->
                    <div class="purchase-history-border2-02 orderlist p-md-2 p-1 text-center d-lg-block d-none">
                        <h6>[type]</h6>
                    </div>
                   
                    <div class="purchase-history-border2-04 p-md-2 p-1 d-flex align-items-start justify-content-center">
                        <div class="d-flex align-items-start justify-content-end">

                            <!-- 金額 -->
                            <div class="text-right d-flex flex-column">
                                <h6 class="mr-lg-4 ">
                                    <span id="ctl00_ContentPlaceHolder[element_id]_repShoppingCart2_ctl00_lblPrice">$[price]</span>
                                </h6>

                            </div>

                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>`
var cartTotalPrice = 0;
var cartTotalProductNum = 0;
var cartJson = [];
var cartPayJson = []
function update_cart_total(TotalNum, TotalPrice) {
    $('#totalPrice').text('NT$' + AppendComma(TotalPrice));
    $('#totalProductNum').text(TotalNum);
}
function delete1(element_id) {
    $.ajax({
        url: route +'/api/cart',
        data: JSON.stringify(cartJson[element_id - 1]),
        contentType: "application/json;charset=utf-8",
        type: "POST",
        success: function (response) {
            if ($('#purchase-history-element' + element_id + ' input[name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck"]').prop("checked")) {
                cartTotalPrice -= cartJson[element_id - 1].price;
                update_cart_total(--cartTotalProductNum, cartTotalPrice);
                if (cartTotalProductNum == 0)
                  lnkBtnRemove_enable(false);
            }
            $('#purchase-history-element' + element_id).remove();            
            member.setDataProperty('cartNum', parseInt(member.cartNum) - 1)
            $('.badge').text(member.cartNum);
        }
    });
}
function deleteSelect() {
    var Json = [];
    $('.shadow-sm').each(function (i, d) {
        if ($('#' + d.id + ' input[name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck"]').prop("checked")) {         
            var element_id = d.id.replace('purchase-history-element', '');
            Json.push(cartJson[element_id - 1]);
            $('#purchase-history-element' + element_id).remove();
            member.setDataProperty('cartNum', parseInt(member.cartNum) - 1)
            $('.badge').text(member.cartNum);
        }
    });
    cartTotalProductNum = 0;
    cartTotalPrice=0;
    update_cart_total(cartTotalProductNum, cartTotalPrice);
    lnkBtnRemove_enable(false);
    $("#ctl00_ContentPlaceHolder1_cbAllCheck3").prop("checked", false);
    $.ajax({
        url: route +'/api/cart/deleteSlect',
        data: JSON.stringify(Json),
        contentType: "application/json;charset=utf-8",
        type: "POST",
        success: function (response) {
            
        }
    });
}
function lnkBtnRemove_enable(enable) {
    if (enable == true) {
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').removeAttr("disabled");
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').removeAttr("style");
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').attr("href", "javascript:deleteSelect('') ");

        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').removeAttr("disabled");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').removeAttr("style");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').attr("class", "more-button_check");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').attr("href", "javascript:lnkBtnGoPaymentClick() ");
    }
    else if (enable == false) {
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').attr("disabled", "disabled");
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').attr("style", "color:rgba(0, 0, 0, 0.12)");
        $('#ctl00_ContentPlaceHolder1_lnkBtnRemove3').removeAttr("href");

        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').attr("disabled", "disabled");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').attr("style", "color:#fff");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').attr("class", "more-button_check-not-work");
        $('#ctl00_ContentPlaceHolder1_lnkBtnGoPayment').removeAttr("href");
        
    }
}
function mainCartRun() {
    loading.TaskAdd()
    $.get(route + '/api/cart/' + member.id).done(function (result, textStatus, jqXHR) {
        cartJson = result.data;
        $.each(cartJson, function (index, value) {
            var element = cart_purchase_history_body_element.replace(/\[element_id]/g, index + 1).replace('[price]', AppendComma(value['price'])).replace('[image]', value['pictureUrl']).replace('[name]', value['name']).replace('[type]', value['type']).replace(/\[productID]/g, '/productList/' + value['productID']);
            $('#purchase-history-body').append(element);
            cartTotalPrice += value['price'];
        });
        cartTotalProductNum = cartJson.length;
        update_cart_total(cartTotalProductNum, cartTotalPrice)
        $('#ctl00_ContentPlaceHolder1_cbAllCheck3').click(function () {
            cartTotalPrice = 0
            cartTotalProductNum=0
            if ($("#ctl00_ContentPlaceHolder1_cbAllCheck3").prop("checked")) {
                $(".checkboxred span input[type='checkbox']").prop("checked", true);
                lnkBtnRemove_enable(true);
                $('.shadow-sm').each(function (i, d) {
                    var element_id = d.id.replace('purchase-history-element', '');
                    if ($('#' + d.id + ' input[name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck"]').prop("checked")) {
                        cartTotalPrice += cartJson[element_id - 1].price;
                        cartTotalProductNum += 1;
                    }
                });
                update_cart_total(cartTotalProductNum, cartTotalPrice);
            }
            else {
                $(".checkboxred span input[type='checkbox']").prop("checked", false);              
                lnkBtnRemove_enable(false);
                update_cart_total(cartTotalProductNum, cartTotalPrice);
            }
        });

        $(".checkboxred span input[type='checkbox']").click(function () {
            cartTotalPrice = 0
            cartTotalProductNum=0
            $('.shadow-sm').each(function (i, d) {
                var element_id = d.id.replace('purchase-history-element', '');
                if ($('#' + d.id + ' input[name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck"]').prop("checked")) {                  
                    cartTotalPrice += cartJson[element_id - 1].price;
                    cartTotalProductNum+=1;
                }
            });
            if (cartTotalProductNum == 0)
                lnkBtnRemove_enable(false);
            else
                lnkBtnRemove_enable(true);
            update_cart_total(cartTotalProductNum, cartTotalPrice);
        });

        loading.TaskSub()
    });
     
}
function lnkBtnGoPaymentClick() {

    var cart_checkSelectd_Json = [];
    $('.shadow-sm').each(function (i, d) {
        var purchase_history_element_id = d.id;
        var element_id = purchase_history_element_id.replace('purchase-history-element', '');
        if ($('#' + purchase_history_element_id + ' input[name="ctl00$ContentPlaceHolder1$Group$ctl00$repShoppingCart$ctl00$cbCheck"]').prop("checked"))           
            cart_checkSelectd_Json.push({ 'cartID': cartJson[element_id - 1].cartID, 'checkSelectd': '1' });
        else
            cart_checkSelectd_Json.push({ 'cartID': cartJson[element_id - 1].cartID, 'checkSelectd': '0' });
    });
    console.log(cart_checkSelectd_Json)
    $.ajax({
        url: route +'/api/cart/setCartSelectCheck',
        data: JSON.stringify(cart_checkSelectd_Json),
        contentType: "application/json;charset=utf-8",
        type: "POST",
        success: function (result) {           
            if (result.msg =='修改成功')
             top.location.href += '/payCheckout';
        }
    });
   
}
var orderNoFind = `<div class="container mb-5">
<div class="row">
    <div class="col-lg-5 col-12 mx-auto text-center">
        <div class="col-group-title-purchase-history-01">
            <h3 class="">找不到此訂單</h3>
        </div>
        <a class="more-button_check mt-sm-0 mt-4" href="/cart">去下單</a>
    </div>
</div>
</div>`
function mainCartPayRun() {

    loading.TaskAdd()
    $('.purchase-history-border-width-all').empty();
    $('#ctl00_ContentPlaceHolder1_txtName').val(member.name)
    $('#ctl00_ContentPlaceHolder1_txtPhone').val('')
    $('#ctl00_ContentPlaceHolder1_txtEmail').val(member.email)
    cartTotalPrice=0
    $.get(route + '/api/cart/getCartSelectCheck/' + member.id).done(function (result, textStatus, jqXHR) {
        cartJson = result.data;
        $.each(cartJson, function (index, value) {
            $('.purchase-history-border-width-all').append(payOrderItemElement.replace(/\[element_id]/g, index + 1).replace('[name]', value.name).replace('[pictureUrl]', value.pictureUrl).replace('[price]', AppendComma(value.price)).replace('[type]', value.type))
            cartTotalPrice += value['price'];
        });
        cartTotalProductNum = cartJson.length;
        $('#Price').text('NT$' + AppendComma(cartTotalPrice));
        $('#totalPrice').text('NT$' + AppendComma(cartTotalPrice));
        $('#totalProductNum').text(cartTotalProductNum);
        alert('貼心提醒!!\r因為這是測試版的綠界,所以需使用它提供的信用卡卡號和安全碼\r信用卡卡號:4311-9522-2222-2222 安全碼:222')
        loading.TaskSub()
    });

   $('#ctl00_ContentPlaceHolder1_lnkBtnPayment').click(function(){
       if (!validatePhone($('#ctl00_ContentPlaceHolder1_txtPhone').val()))
           alert('請檢查電話格式')
       else if (!$('#cboxAllow').prop("checked"))
           alert('請勾選【我已閱讀並接受個資保護聲明】...')
       else {
           var cartItem = [];
           var txtName = $('#ctl00_ContentPlaceHolder1_txtName').val()
           var txtPhone = $('#ctl00_ContentPlaceHolder1_txtPhone').val()
           var txtEmail = $('#ctl00_ContentPlaceHolder1_txtEmail').val()
           cartJson.forEach(function (cart) {
               cartItem.push(cart.cartID)
           });
           var dataJson = { 'ownerID': member.id, 'name': txtName, 'phone': txtPhone, 'email': txtEmail, 'cartItem': cartItem };
           $.ajax({
               url: route +'/api/payOrder/setContact_and_goToPay',
               data: JSON.stringify(dataJson),
               contentType: "application/json;charset=utf-8",
               type: "POST",
               success: function (Result) {
                   // top.location.href += LinkUrl;
                   //console.log(Result)
                   if (Result.status == 0)
                       alert(Result.message)
                   else if (Result.status == 1) {
                       location.href = Result.message;
                   }
               }
           });
       }
        
    });
    
  
}