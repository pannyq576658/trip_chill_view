var profileJson = { 'backgroundImg': member.picture, 'email': member.email, 'name': member.name, 'sex': member.gender, 'birth': { 'year': member.birthdayY, 'month': member.birthdayM, 'day': member.birthdayD } };
function mainProfileRun() {
    var less10 = '';
    for (var year = 1936; year <= 2020; year++)
        $('#birthSelect').append("<option value='" + year + "' class='border-b border-solid border-hi-input-gray pl-3 leading-10 hover:bg-hi-input-gray' >" + year + "</option>");
    for (var month = 1; month <= 12; month++) {
        less10 = (month < 10) ? '0' : '';
        $('#monthSelect').append("<option value='" + less10 + "" + month + "' class='border-b border-solid border-hi-input-gray pl-3 leading-10 hover:bg-hi-input-gray' >" + less10 + month + "</option>");
    }
    $('#btnSendVerifyEmail').click(function () {
        var $btn = $(this);
        // 2. 真正禁用按鈕：同時設定屬性與 CSS
        $btn.prop('disabled', true)
            // 教育性見解：使用 setProperty 帶上 !important，才能徹底蓋掉您原本 CSS 裡的 !important
            .each(function () {
                this.style.setProperty('pointer-events', 'none', 'important');
            }).addClass('bg-gray-400')
            .css('cursor', 'not-allowed'); // 顯示「禁止」游標
           

        var email = $('#input_email').val();

        // 1. 使用您專案現有的格式驗證
        if (!validateEmail(email)) {
            alert('請輸入正確的 Email 格式');
            return;
        }
      
        $.ajax({
            url: route + '/api/member/sendVerifyEmail',
            data: JSON.stringify({ email: email, userId: member.id }),
            contentType: "application/json;charset=utf-8",
            type: "POST",
            success: function (response) {
                alert('驗證碼已發送至您的信箱');
                $('#verifyCodeArea').show();
                
                // 啟動 3 分鐘倒數計時
                var totalSeconds = 180; // 3 分鐘
                var $timer = $('#verifyCodeTimer');

                // 清除舊的計時器（如果有的話）
                if (window.verifyTimerInterval) {
                    clearInterval(window.verifyTimerInterval);
                }

                window.verifyTimerInterval = setInterval(function () {
                    totalSeconds--;

                    var minutes = Math.floor(totalSeconds / 60);
                    var seconds = totalSeconds % 60;

                    // 格式化為 00:00
                    var displayTime = (minutes < 10 ? '0' + minutes : minutes) + ':' + (seconds < 10 ? '0' + seconds : seconds);
                    $timer.text(displayTime);

                    if (totalSeconds <= 0) {
                        clearInterval(window.verifyTimerInterval);
                        $timer.text('03:00');
                       // $btn.prop('disabled', false).removeClass('bg-gray-400').addClass('btn-outline-primary');
                        alert('驗證碼已過期，請重新發送');
                        $('#verifyCodeArea').hide();
                        $('#btnSendVerifyEmail').prop('disabled', false)
                            .each(function () {
                                // 將 pointer-events 恢復為 auto，並帶上 !important 確保成功打開
                                this.style.setProperty('pointer-events', 'auto', 'important');
                            })
                            .removeClass('bg-gray-400')
                            .css('cursor', 'pointer');
                    }
                }, 1000);
            }
        });
       
    });

    $('#btnConfirmVerify').click(function () {
        var code = $('#verify_code').val();      
        $.ajax({
            url: route + '/api/member/confirmEmail',
            data: JSON.stringify({ userId: member.id, code: code }),
            contentType: "application/json;charset=utf-8",
            type: "POST",
            success: function (response) {
                console.log(response);
                if (response.data) {
                    alert('驗證成功！');
                    member.setDataProperty('verifyApproved', true);                   
                } else 
                    alert('驗證碼錯誤或已過期');
                $('#verifyCodeTimer').text('03:00');
                $('#verify_code').val('');
                $('#verifyCodeArea').hide();
                location.reload();
            }               
        });
    });
    $('#monthSelect').change(function () {

        var birth = $('#birthSelect :selected').text();
        var month = $('#monthSelect :selected').text();
        $('#daySelect').empty();
        var d = new Date(birth, month, 0);
        for (var day = 1; day <= d.getDate(); day++) {
            less10 = (day < 10) ? '0' : '';
            $('#daySelect').append("<option value='" + less10 + "" + day + "' class='border-b border-solid border-hi-input-gray pl-3 leading-10 hover:bg-hi-input-gray' >" + less10 + day + "</option>");
        }
    });
    $('#input_email').val(profileJson.email);
    $('.acount-user-image').attr('style', "background-image: url(" + member.picture + ");");
    //$('.acount-user-image').attr('style', 'background-image: url("' + json.backgroundImg + '/picture?width=200&height=200"); background-size: cover; background-position: center center; background-repeat: no-repeat;');
    $('#userName').val(profileJson.name);
    $('#sexSelect input[name="sex"]')[profileJson.sex].checked = true;
    $('#birthSelect  option[value="' + profileJson.birth.year + '"]').prop("selected", true);
    $('#monthSelect  option[value="' + profileJson.birth.month + '"]').prop("selected", true);
    $('#monthSelect').change();
    $('#daySelect  option[value="' + profileJson.birth.day + '"]').prop("selected", true);
    var selectedFile = null; // 用於暫存使用者選取的檔案物件
    // 點擊頭像容器觸發隱藏的檔案選擇器
    $('#profileImageBtn').click(function () {
        $('#profilePicInput').click();
    });

    // 處理頭像檔案選擇與預覽
    $('#profilePicInput').change(function (e) {
        var file = e.target.files[0];
        if (file) {
            // 驗證檔案大小 (1MB = 1048576 bytes)
            if (file.size > 1048576) {
                alert('檔案大小不能超過 1MB');
                $(this).val('');
                return;
            }

            selectedFile = file; // 暫存檔案物件，等按下儲存時再上傳

            var reader = new FileReader();
            reader.onload = function (event) {
                var base64String = event.target.result;
                // 僅更新頁面上的預覽圖（大圖與導覽列小圖）
                $('.acount-user-image').css('background-image', 'url(' + base64String + ')');              
            };
            reader.readAsDataURL(file);

           /* $.ajax({
                url: route + '/api/member/uploadPicture',
                type: 'POST',
                data: formData,
                processData: false, // 告訴 jQuery 不要處理資料
                contentType: false, // 告訴 jQuery 不要設置內容類型
                success: function (response) {
                    if (response.status == 1) {
                        var pictureUrl = route + response.data; // 拼接完整後端路徑

                        // 2. 更新頁面上的大圖預覽
                        $('.acount-user-image').css('background-image', 'url(' + pictureUrl + ')');
                        // 同步更新導覽列的小圖
                        $('.navbar-cards .user-icon').attr('style', "background-image: url(" + pictureUrl + ");");
                        $('.userSignin .user-icon').attr('style', "background-image: url(" + pictureUrl + ");");

                        // 3. 更新 member 物件中的圖片資料 (儲存短網址即可)
                        member.setDataProperty('picture', pictureUrl);

                        alert('照片上傳成功！請記得點擊下方的儲存按鈕以更新會員資料。');
                    } else {
                        alert('上傳失敗：' + response.msg);
                    }
                },
                error: function () {
                    alert('伺服器連線失敗，請稍後再試。');
                }
            });*/

        }
    });

    if (member.platform == "fb") {
        $('.fb-bind-picture').attr('style', "background-image: url(" + member.picture + ");");
        $('.fb-bind-name').text(member.name);
        $('.fb-bind-name').attr('class', "fb-bind-name ml-5 flex-grow break-all text-[16px] sn-500:text-[14px] sm:ml-2 text-gray-700");
        $('.fb-bind-btn').text('取消綁定');
        $('.fb-bind-btn').attr('class', "fb-bind-btn btn-text-underline-gray-600 text-[14px] text-gray-600");
    }
    else {
        $('.fb-bind-picture').attr('style', "display: none;");
        $('.fb-bind-name').text("未綁定帳號");
        $('.fb-bind-name').attr('class', "fb-bind-name ml-5 flex-grow break-all text-[16px] sn-500:text-[14px] sm:ml-2 text-gray-500");
        $('.fb-bind-btn').text('進行設定');
        $('.fb-bind-btn').attr('class', "fb-bind-btn btn-text-underline-gray-600 text-[14px]");
    }

    if (member.platform == "google") {
        $('.google-bind-picture').attr('style', "background-image: url(" + member.picture + ");");
        $('.google-bind-name').text(member.name);
        $('.google-bind-name').attr('class', "google-bind-name ml-5 flex-grow break-all text-[16px] sn-500:text-[14px] sm:ml-2 text-gray-700");
        $('.google-bind-btn').text('取消綁定');
        $('.google-bind-btn').attr('class', "google-bind-btn btn-text-underline-gray-600 text-[14px] text-gray-600");
    }
    else {
        $('.google-bind-picture').attr('style', "display: none;");
        $('.google-bind-name').text("未綁定帳號");
        $('.google-bind-name').attr('class', "fb-bind-name ml-5 flex-grow break-all text-[16px] sn-500:text-[14px] sm:ml-2 text-gray-500");
        $('.google-bind-btn').text('進行設定');
        $('.google-bind-btn').attr('class', "google-bind-btn btn-text-underline-gray-600 text-[14px]");
    }  
    if (member.verifyApproved == 'true') {
        $('#emailVerify').text('已驗證').removeClass('text-red-1').addClass('text-success');
        $('#btnSendVerifyEmail').prop('disabled', true)
            // 教育性見解：使用 setProperty 帶上 !important，才能徹底蓋掉您原本 CSS 裡的 !important
            .each(function () {
                this.style.setProperty('pointer-events', 'none', 'important');
            }).addClass('bg-gray-400')
            .css('cursor', 'not-allowed'); // 顯示「禁止」游標
        
    }
    else {
        $('#emailVerify').text('未驗證').removeClass('text-success').addClass('text-red-1');
        $('#btnSendVerifyEmail').prop('disabled', false)
            .each(function () {
                // 將 pointer-events 恢復為 auto，並帶上 !important 確保成功打開
                this.style.setProperty('pointer-events', 'auto', 'important');
            })
            .removeClass('bg-gray-400')
            .css('cursor', 'pointer');
    }
    $('#saveBtn').click(function () {
        var gender = $('#sexSelect input[name="sex"]:checked').val();
        // 定義最終執行的儲存函數
        var doSave = function (finalPictureUrl) {
            var memberJson = {
                'id': member.id,
                'name': $('#userName').val(),
                'cartNum': parseInt(member.cartNum),
                'platform': member.platform,
                'gender': gender,
                'email': member.email,
                'birthday': $('#birthSelect :selected').text() + '/' + $('#monthSelect :selected').text() + '/' + $('#daySelect :selected').text(),
                'pictureUrl': finalPictureUrl
            };

            $.ajax({
                url: route + '/api/member/updateMember',
                data: JSON.stringify(memberJson),
                contentType: "application/json;charset=utf-8",
                type: "POST",
                success: function (response) {
                    alert(response.msg);
                    if (response.msg == "修改成功") {
                        member.setDataProperty('name', $('#userName').val());
                        member.setDataProperty('picture', finalPictureUrl); // 更新 Cookie 中的圖片網址
                        $('.' + member.platform + '-bind-name').text($('#userName').val());
                        $('.navbar-cards .username').text($('#userName').val());
                        member.setDataProperty('birthdayY', $('#birthSelect :selected').text());
                        member.setDataProperty('birthdayM', $('#monthSelect :selected').text());
                        member.setDataProperty('birthdayD', $('#daySelect :selected').text());
                        member.setDataProperty('gender', gender);
                        selectedFile = null; // 儲存完畢清空暫存檔案
                    }
                }
            });
        };

        
        // 判斷是否有新選擇的檔案需要上傳
        if (selectedFile) {
            var formData = new FormData();
            formData.append('file', selectedFile);
            formData.append('userId', member.id);
            $.ajax({
                url: route + '/api/member/uploadPicture',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.status == 1) {
                        var pictureUrl = route + response.data;                       
                        doSave(pictureUrl); // 上傳成功後，執行資料儲存
                        $('.acount-user-image').css('background-image', 'url(' + pictureUrl + ')');
                        $('.navbar-cards .user-icon').attr('style', "background-image: url(" + pictureUrl + ");");
                        $('.userSignin .user-icon').attr('style', "background-image: url(" + pictureUrl + ");");
                    } else {
                        alert('圖片上傳失敗：' + response.msg);
                    }
                },
                error: function () {
                    alert('圖片上傳失敗，請稍後再試。');
                }
            });
        } else {
            // 如果沒有新檔案，直接使用原本的圖片網址進行儲存
            doSave(member.picture);
        }

    });
}