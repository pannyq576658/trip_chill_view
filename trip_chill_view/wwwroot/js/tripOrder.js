var orderElement = `<div class="col-12">
        <div class="purchase-history-bgcolor d-flex justify-content-md-between flex-md-row flex-column align-items-end">
            <div class="purchase-history-border-width-all d-flex flex-md-column flex-row ">
                <div class="purchase-history-border-width d-flex flex-md-row flex-column pb-md-3">
                    <div class="purchase-history-border p-1">
                        <h6>訂單編號</h6>
                    </div>
                    <div class="purchase-history-border p-1">
                        <h6>購買日期</h6>
                    </div>
                    <div class="purchase-history-border p-1">
                        <h6>狀 態</h6>
                    </div>
                    <div class="purchase-history-border p-1">
                        <h6>總金額</h6>
                    </div>
                </div>
                <div class="purchase-history-border-width2 d-flex flex-md-row flex-column">
                    <div class="purchase-history-border2 p-md-2 p-1">
                        <h6>[payOrderID]</h6>
                    </div>
                    <div class="purchase-history-border2 p-md-2 p-1">
                        <h6>[payDate]</h6>
                    </div>
                    <div class="purchase-history-border2 p-md-2 p-1">
                        <h6>已付款</h6>
                    </div>
                    <div class="purchase-history-border2 p-md-2 p-1 d-flex justify-content-between align-items-center">
                        <h6 style="color: rgba(0,0,0,.54)">$[totalPrice]</h6>
                        <p class="mb-0">
                            <a class="advanced-button order-detail collapsed" data-toggle="collapse" href="#collapseExample[payOrderID]" role="button" aria-expanded="false" aria-controls="collapseExample[payOrderID]" data-orderno="[payOrderID]">明細</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="showwidth collapse" id="collapseExample[payOrderID]" style="">
            <div class="card card-body">
                <div class="purchase-history-border-width-all">
                    <div class="purchase-history-border-width3">
                        <div class="purchase-history-border-width3 d-sm-flex d-none">
                            <div class="purchase-history-border3 p-1 text-center">
                                <h6 style="color: #469ba2; font-weight: 400;">產品名稱</h6>
                            </div>
                            <div class="purchase-history-border4 text-center">
                                <h6 style="color: #469ba2; font-weight: 400;">售 價</h6>
                            </div>
                            <div class="purchase-history-border4 pr-3 text-right">
                                <h6 style="color: #469ba2; font-weight: 400;">實 付</h6>
                            </div>
                        </div>
                        <hr class="my-0 d-sm-block d-none">
                            [historyPayOrderProduct]
                                    <hr class="mt-sm-0">
                                        <div class="purchase-history-border-width3 d-flex mb-1">
                                            <div class="purchase-history-border3 p-1 text-center">
                                            </div>
                                        </div>
                                        <div class="purchase-history-border-width3 d-flex mb-3">
                                            <div class="purchase-history-border3 p-1 text-center">
                                            </div>
                                            <div class="purchase-history-border4 text-center">
                                                <h6>總 計</h6>
                                            </div>
                                            <div class="purchase-history-border4 pr-sm-3 text-right">
                                                <h6>$‭[totalPrice]</h6>
                                            </div>
                                        </div>
                                        <div class="p-sm-3 d-flex justify-content-start flex-lg-row flex-column" style="color: rgba(0,0,0,.54);">
                                            <div class=" d-flxe">
                                                <p>
                                                    發票日期：<span>[payDate]</span>
                                                </p>
                                                <p>
                                                    發票號碼：<span>UF87217231</span>
                                                </p>
                                            </div>
                                            <div class=" pl-lg-5 d-flxe">
                                                <p>
                                                    統一編號：<span>
                                                    </span>
                                                </p>
                                                <p>
                                                    購 買 人：<span>[name]</span>
                                                </p>
                                            </div>
                                            <div class="pl-lg-5 d-flxe">
                                                <p>
                                                    地 址：<span>
                                                    </span>
                                                </p>
                                            </div>
                                        </div>
                                </div>
                                </div>
                        </div>
                    </div>
                    </div>`;
var hisOrderProdElement = `<span id="[productID]_detail">
    <div class="d-flex flex-sm-row flex-column align-items-sm-start align-items-end">
        <div class="purchase-history-border3 p-sm-3  d-flex align-items-start">
            <img src="[background]" alt="" class="rounded">
                <h6 class="pl-md-3 pl-2 mb-sm-0 mb-3 text-center">[name]</h6>
        </div>
            <div class="purchase-history-border4 p-sm-3  text-center d-sm-block d-none">
                <h6>[price]</h6>
            </div>
            <div class="purchase-history-border4 p-sm-3 text-right d-flex flex-column mr-md-auto">
                <h6>$[price]</h6>
            </div>
        </div>
        <hr class="my-sm-0 my-3"></span>`;
var orderJson = [{ 'id': '2021120500000012', 'name': '11111111', 'date': '2021/12/05', 'state': '付款(扣點)完成', 'price': '2,200' },
{ 'id': '2021120500000017', 'name': '2222222222', 'date': '2022/03/05', 'state': '付款(扣點)完成', 'price': '7,200' },
    { 'id': '2021120500000018', 'name': '44444444444', 'date': '2023/11/05', 'state': '付款(扣點)完成', 'price': '700' }];
function mainOrderRun() {    
    loading.TaskAdd()
    $('#OrderList .row').empty();
    $.get(route + '/api/historyOrder/' + member.id).done(function (historyOrderResult, textStatus, jqXHR) {
        orderJson = historyOrderResult.data;
        console.log(orderJson);
        $.each(orderJson, function (index, value) {
            var element = orderElement.replace('[name]', value.name).replace(/\[payOrderID]/g, value.payOrderID).replace(/\[payDate]/g, value.payDate);
            loading.TaskAdd()
            $.get(route + '/api/historyOrder/getHisOrderProductList/' + value.payOrderID).done(function (hisOrderProdResult, textStatus, jqXHR) {
                var hisOrderProdElementArray = '';
                var totalPrice = 0;
                $.each(hisOrderProdResult.data, function (index, hisOrderProdVal) {
                    hisOrderProdElementArray += hisOrderProdElement.replace('[productID]', hisOrderProdVal.productID).replace('[name]', hisOrderProdVal.name).replace(/\[price]/g, AppendComma(hisOrderProdVal.price)).replace('[background]', hisOrderProdVal.background)
                    totalPrice += hisOrderProdVal.price;
                });
                element = element.replace('[historyPayOrderProduct]', hisOrderProdElementArray).replace(/\[totalPrice]/g, AppendComma(totalPrice));
                
                $('#OrderList .row').append(element);
                loading.TaskSub()
            });
        });
        loading.TaskSub()
    });
}
