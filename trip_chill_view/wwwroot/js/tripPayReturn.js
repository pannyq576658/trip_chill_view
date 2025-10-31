var orderTable = `<div class="purchase-history-border-width3">

    <div class="purchase-history-border-width3 d-sm-flex d-none">
        <div class="purchase-history-border3 p-1 text-center">
            <h6 style="color: #469ba2; font-weight: 400;">課程名稱</h6>
        </div>
        <div class="purchase-history-border4 text-center">
            <h6 style="color: #469ba2; font-weight: 400;">售 價</h6>
        </div>
        <div class="purchase-history-border4 pr-3 text-right">
            <h6 style="color: #469ba2; font-weight: 400;">實 付</h6>
        </div>
    </div>

    <hr class="my-0 d-sm-block d-none">
        [element]
      <hr class="my-sm-0 my-3">
      <hr class="mt-sm-0">
        <div class="purchase-history-border-width3 d-flex mb-3">
            <div class="purchase-history-border3 p-1 text-center"></div>
             <div class="purchase-history-border4 text-center">
               <h6>總 計</h6>
             </div>
       <div class="purchase-history-border4 pr-sm-3 text-right">
                <h6>
                   $[totalPrice]
                </h6>
             </div>
        </div>
     </div>`
var shopItem = `<div class="d-flex flex-sm-row flex-column align-items-sm-start align-items-end">
    <!-- 購買課程 -->
    <div class="purchase-history-border3 p-sm-3  d-flex align-items-start">
        <img src="[background]" alt="" class="rounded">
            <h6 class="pl-md-3 pl-2 mb-sm-0 mb-3">
                [name]
            </h6>
  </div>
        <!-- 售價 -->
        <div class="purchase-history-border4 p-sm-3  text-center d-sm-block d-none">
            <h6>
                $[price]
            </h6>
        </div>
        <!-- 實付 -->
        <div class="purchase-history-border4 p-sm-3 text-right d-flex flex-column mr-md-auto">
            <h6>
                [actually_price]
            </h6>

            <!-- 折扣碼 -->

        </div>
    </div>`;
var returnError = `<div id="ctl00_ContentPlaceHolder1_errorPanel" class="col-12">

    <div class="d-flex justify-content-center successful-height2 flex-column">
        <div class="d-flex align-items-center justify-content-center mb-3">
            <svg class="checkmark error mr-3" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 52 52">
                <circle class="checkmark_circle_error" cx="26" cy="26" r="25" fill="none"></circle>
                <path class="checkmark_check" stroke-linecap="round" fill="none" d="M16 16 36 36 M36 16 16 36"></path>
            </svg>
            <h1 class="mb-0 mt-1" style="color: rgb(0 0 0 / .87);">授權失敗!</h1>
        </div>
        <h6 class="text-center" style="color: rgb(0 0 0 / .7);">Transaction Failed! </h6>
        <h6 class="text-center" style="color: rgb(0 0 0 / .7);">很抱歉付款不成功</h6>

        <table class="table table-bordered mt-5">
            <thead class="text-white" style="background-color: #e94941">
                <tr>
                    <th scope="col">使用付款方式</th>
                    <th scope="col">失敗原因</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row" style="color: rgba(0,0,0,.7);">
                        信用卡
                    </th>
                    <td>
                        ERROR
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</div>`
var returnSuccess = `<div id="ctl00_ContentPlaceHolder1_successPanel" class="d-flex mx-auto align-items-center">

    <div>
        <img src="/img/people.jpg" alt="" class="mr-4 d-md-block d-none">
                </div>
        <div class="d-flex justify-content-center successful-height flex-column">
            <div class="d-flex align-items-center justify-content-center mb-3">
                <svg class="checkmark success mr-3" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 52 52">
                    <circle class="checkmark_circle_success" cx="26" cy="26" r="25" fill="none"></circle>
                    <path class="checkmark_check" fill="none" d="M14.1 27.2l7.1 7.2 16.7-16.8" stroke-linecap="round"></path>
                </svg>
                <h1 class="mb-0 mt-1" style="color: rgb(0 0 0 / .87);">購買成功!</h1>
            </div>
            <h6 class="text-center" style="color: rgb(0 0 0 / .7);">Thanks for your support!</h6>
            <h6 class="text-center" style="color: rgb(0 0 0 / .7);">謝謝您的支持</h6>
        </div>
    </div>`
var payErrorText = `<h6 style="color:red">
    付款失敗
</h6>`
function mainPayReturnRun(retrunCode) {
    loading.TaskAdd()
    $('#purchase-history-table').empty();
    $('#return-result').empty();
    $.get(route +'/api/historyOrder/getPayOrderReturn/' + retrunCode).done(function (payOrderReturnResult, textStatus, jqXHR) {    
        if (payOrderReturnResult.data.hasData == true) {
            if (payOrderReturnResult.data.isPay == 'True') {
                $('#return-result').append(returnSuccess);
                
            }
            else {
                $('#return-result').append(returnError);
            }
            loading.TaskAdd()           
            $.get(route + '/api/historyOrder/getHisOrderProductList/' + payOrderReturnResult.data.payOrderID).done(function (hisOrderProdResult, textStatus, jqXHR) {
                var hisOrderProdElementArray = '';
                var totalPrice = 0;
                $.each(hisOrderProdResult.data, function (index, hisOrderProdVal) {
                     if (payOrderReturnResult.data.isPay == 'True') {
                         hisOrderProdElementArray += shopItem.replace('[name]', hisOrderProdVal.name).replace('[actually_price]', '$'+hisOrderProdVal.price).replace(/\[price]/g, hisOrderProdVal.price).replace('[background]', hisOrderProdVal.background)
                         totalPrice += hisOrderProdVal.price;
                     }
                     else
                         hisOrderProdElementArray += shopItem.replace('[name]', hisOrderProdVal.name).replace('[actually_price]', payErrorText).replace(/\[price]/g, hisOrderProdVal.price).replace('[background]', hisOrderProdVal.background)
                });
                orderTable = orderTable.replace('[element]', hisOrderProdElementArray).replace(/\[totalPrice]/g, totalPrice);

                 $('#purchase-history-table').append(orderTable);
                 
                loading.TaskSub()
            });
        }
        
        loading.TaskSub()
    });
    
}
