// 登入及註冊使用
var LINE_login_state_timer;
var LINE_login_state_timer_fun;

function registerPost(memberJson) {
    $.ajax({
        url: route+'/api/member/insertMember',
        data: JSON.stringify(memberJson),
        contentType: "application/json;charset=utf-8",
        type: "POST",
        success: function (response) {
            
            if (response.msg == '已經註冊過了') {
                $.get(route + '/api/member/' + memberJson.id).done(function (result, textStatus, jqXHR) {
                    var memberData = result.data;
                    memberData.pictureUrl = route + memberData.pictureUrl;
                    member.setData(memberData);
                    alert(response.msg);
                    top.location.href = '/';
                });
            }
            else {
                member.setData(memberJson);
                alert(response.msg);
                top.location.href = '/';
            }

        }
    });
}
function LINE_sql_load(memberJson) {  
    if (location.pathname == '/register')
        registerPost(memberJson)
    else if (location.pathname == '/login') {
       
        $.ajax({
            url: route + '/api/member/apilogin',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(memberJson),
            success: function (result) {
                if (result.status == 0) {
                    alert(result.msg);
                    if (result.msg == "請去註冊") {
                        top.location.href = '/register';
                    }
                }
                else {
                    var memberData = result.data;
                    memberData.pictureUrl = route + memberData.pictureUrl;
                    member.setData(memberData);
                    alert(response.msg);
                    top.location.href = document.referrer;
                }
            },
            error: function () {
                alert("登入發生錯誤，請稍後再試");
            }
        });
    }
}
function LINE_Offline_login(memberJson) {
    member.setData(memberJson);
    alert("登入成功");
    top.location.href = document.referrer;

    
}
function LINE_Offline_doSignOut() {
    
    member.dataClear();

    if (liff.isLoggedIn()) {
        liff.logout();      
        top.location.href = '/';
    }
    
}
function LineInit(liffId) {
    liff.init({
        liffId: liffId

    }).then(function () {        
        LINE_login_state_timer_fun = function () {
            if (liff.isLoggedIn()) {
                clearInterval(LINE_login_state_timer);
                var user = liff.getDecodedIDToken();
                console.log(user);
                try {
                    
                    var memberJson = { 'id': 'line' + user.sub, 'name': user.name, 'cartNum': 0, 'platform': 'line', 'gender': '0', 'email': user.email, 'birthday': '1935/01/01', 'pictureUrl': user.picture };

                    if (enableLocalLogin)
                        LINE_Offline_login(memberJson);
                    else
                        LINE_sql_load(memberJson);
                    
                    console.log(memberJson);
                }
                catch
                {
                    liff.logout();
                    alert("抓不到資料");
                   
                }                              
            }
        }
    }).catch(function (error) {
        console.log(error);
    });
}
function doLogin() {
    if (location.pathname == '/register') {
        
        if ($('#register_iPwd').val() != $('#register_iPwd2').val())
            alert('密碼確認不一致')
        else if (!validateEmail($('#register_iEmail').val()) || !validatePhone($('#register_iPhone').val()))
            alert('請檢查email或電話格式')
        else {
            var user_id = $('#register_iUid').val();
            var name = $('#register_iName').val();
            var email = $('#register_iEmail').val();
            var pwd = $('#register_iPwd').val();
            var phone = $('#register_iPhone').val();
            var memberJson = { 'id': user_id, 'name': name, 'cartNum': 0, 'platform': 'tripView', 'gender': '0', 'email': email, 'birthday': '1935/01/01', 'pictureUrl': '', 'password': pwd, 'phone': phone };
            registerPost(memberJson)
        }       
    }
    else if (location.pathname == '/login') {
        var user_id = $('#login_id').val();
        var pwd = $('#login_Pwd').val();     
        $.ajax({
            url: route + '/api/member/login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ id: user_id, password: pwd }),
            success: function (result) {
                if (result.status == 0) {
                    alert(result.msg);
                    if (result.msg == "請去註冊") {
                        top.location.href = '/register';
                    }
                }
                else {
                    var memberData = result.data;
                    memberData.pictureUrl = route + memberData.pictureUrl;
                    member.setData(memberData);
                    alert("登入成功");
                    top.location.href = document.referrer;
                }
            },
            error: function () {
                alert("登入發生錯誤，請稍後再試");
            }
        });
    }

}
function doFBLogin() {
        FB.login(function (response) {
            console.log(response);
            if (response.status === 'connected') {
                if (location.pathname == '/register') {
                    console.log(response.authResponse.accessToken);
                    FB.api("/me", "GET", {
                        fields: 'last_name,first_name,name,email,picture,gender,birthday,photos,link'
                    }, function (user) {
                        if (user.error) {
                            console.log(response);
                        } else {
                            var gender = '0'
                            var birthdayY = '1935'
                            var birthdayM = '01'
                            var birthdayD = '01'
                            if (user['gender'])
                                gender = (user['gender'] == 'male') ? '0' : '1';
                            if (user['birthday']) {
                                var birthdayArray = user['birthday'].split("/");
                                if (birthdayArray.length == 3)
                                    birthdayY = birthdayArray[2];
                                birthdayM = birthdayArray[0];
                                birthdayD = birthdayArray[1];
                            }

                            var memberJson = { 'id': 'fb' + user['id'], 'name': user['name'], 'cartNum': 0, 'platform': 'fb', 'gender': gender, 'email': user['email'], 'birthday': birthdayY + '/' + birthdayM + '/' + birthdayD, 'pictureUrl': user['picture']['data']['url'] };
                            registerPost(memberJson)
                        }
                    });
                    FB.api(
                        "/1826861391034700/picture",
                        {
                            "redirect": false,
                            "height": "50",
                            "type": "normal",
                            "width": "50"
                        },
                        function (response) {
                            if (response && !response.error) {
                                console.log(response);
                            }
                        }
                    );

                }
                else if (location.pathname == '/login') {
                    $.get(route +'/api/member/fb' + response["authResponse"]["userID"]).done(function (result, textStatus, jqXHR) {
                        console.log(result);
                        if (result.msg == "請去註冊") {
                            alert("你還沒註冊");
                            FB.api("/me/permissions", "DELETE", function (response) {
                                FB.getLoginStatus(function (res) { top.location.href = '/register'; }, true);
                            });

                        }
                        else {
                            var memberData = result.data;
                            memberData.pictureUrl = route + memberData.pictureUrl;
                            member.setData(memberData);
                            alert("登入成功");
                            top.location.href = document.referrer;
                        }
                    });


                }
            }
            else {
            }
        }, { scope: 'public_profile,email,user_gender,user_birthday,user_photos,user_link' });
    
}
function doFBLoginTest() {
    FB.login(function (response) {
        console.log(response);
        if (response.status === 'connected') {
            if (location.pathname == '/register') {
                console.log(response.authResponse.accessToken);
                FB.api("/me", "GET", {
                    fields: 'name,email'
                }, function (user) {
                    if (user.error) {
                        console.log(response);
                    } else {
                        console.log('dd');
                        console.log(response);
                        /*var gender = '0'
                        var birthdayY = '1935'
                        var birthdayM = '01'
                        var birthdayD = '01'
                        if (user['gender'])
                            gender = (user['gender'] == 'male') ? '0' : '1';
                        if (user['birthday']) {
                            var birthdayArray = user['birthday'].split("/");
                            if (birthdayArray.length == 3)
                                birthdayY = birthdayArray[2];
                            birthdayM = birthdayArray[0];
                            birthdayD = birthdayArray[1];
                        }

                        var memberJson = { 'id': 'fb' + user['id'], 'name': user['name'], 'cartNum': 0, 'platform': 'fb', 'gender': gender, 'email': user['email'], 'birthday': birthdayY + '/' + birthdayM + '/' + birthdayD, 'pictureUrl': user['picture']['data']['url'] };
                        registerPost(memberJson)*/
                    }
                });
                /*FB.api(
                    "/1826861391034700/picture",
                    {
                        "redirect": false,
                        "height": "50",
                        "type": "normal",
                        "width": "50"
                    },
                    function (response) {
                        if (response && !response.error) {
                            console.log(response);
                        }
                    }
                );*/

            }
            else if (location.pathname == '/login') {
                $.get(route + '/api/member/fb' + response["authResponse"]["userID"]).done(function (result, textStatus, jqXHR) {
                    console.log(result);
                    if (result.msg == "請去註冊") {
                        alert("你還沒註冊");
                        FB.api("/me/permissions", "DELETE", function (response) {
                            FB.getLoginStatus(function (res) { top.location.href = '/register'; }, true);
                        });

                    }
                    else {
                        member.setData(result.data);
                        alert("登入成功");
                        top.location.href = document.referrer;
                    }
                });


            }
        }
        else {
        }
    }, { scope: 'public_profile,email,user_gender,user_birthday,user_photos,user_link' });

}
function doGGLogin() {

        let auth2 = gapi.auth2.getAuthInstance();//取得GoogleAuth物件
        auth2.signIn().then(function (GoogleUser) {
            console.log("Google登入成功");
            let user_id = GoogleUser.getId();//取得user id，不過要發送至Server端的話，為了資安請使用id_token，本人另一篇文章有範例：https://dotblogs.com.tw/shadow/2019/01/31/113026
            console.log(`user_id:${user_id}`);
            let AuthResponse = GoogleUser.getAuthResponse(true);//true會回傳包含access token ，false則不會
            let id_token = AuthResponse.id_token;//取得id_token
            //people.get方法參考：https://developers.google.com/people/api/rest/v1/people/get
            if (location.pathname == '/register') {
                gapi.client.people.people.get({
                    'resourceName': 'people/me',
                    //通常你會想要知道的用戶個資↓
                    'personFields': 'names,phoneNumbers,emailAddresses,photos,genders,birthdays',
                }).then(function (res) {
                    //success
                    let str = JSON.stringify(res.result);//將物件列化成string，方便顯示結果在畫面上
                    var picture = res.result['photos'][0]['url']
                    var name = res.result['names'][0]['displayName']
                    var email = res.result['emailAddresses'][0]['value']
                    var gender = '0'
                    var birthdayY = '1935'
                    var birthdayM = '01'
                    var birthdayD = '01'
                    if (res.result.genders)
                        gender = (res.result['genders'][0]['value'] == 'male') ? '0' : '1';
                    if (res.result.birthdays) {
                        $.each(res.result.birthdays, function (index, value) {
                            if (value['date']['year'] && value['date']['month'] && value['date']['day']) {

                                birthdayY = value['date']['year'];
                                birthdayM = (value['date']['month'] < 10) ? '0' + value['date']['month'] : value['date']['month'];
                                birthdayD = (value['date']['day'] < 10) ? '0' + value['date']['day'] : value['date']['day'];
                                return false;
                            }
                        });
                    }
                    var memberJson = { 'id': 'google' + user_id, 'name': name, 'cartNum': 0, 'platform': 'google', 'gender': gender, 'email': email, 'birthday': birthdayY + '/' + birthdayM + '/' + birthdayD, 'pictureUrl': picture };
                    registerPost(memberJson)
                });
            }
            else if (location.pathname == '/login') {
                $.get(route + '/api/member/google' + user_id).done(function (result, textStatus, jqXHR) {
                    console.log(result);
                    if (result.msg == "請去註冊") {
                        alert("你還沒註冊");
                        top.location.href = '/register';

                    }
                    else {
                        var memberData = result.data;
                        memberData.pictureUrl = route + memberData.pictureUrl;
                        member.setData(memberData);
                        alert("登入成功");
                        top.location.href = document.referrer;
                    }
                });
            }
        },
            function (error) {
                console.log("Google登入失敗");
                console.log(error);
            });

   
}//end function GoogleLogin

function doLineLogin() {
    if (LINE_login_state_timer === undefined) {
        LINE_login_state_timer = setInterval(LINE_login_state_timer_fun, 1000);
    }    
    let newWindow = open('/LINE_login.html', '_blank', getWindowSizeString());
    newWindow.focus();
}

function doSignOut() {
    loading.Startup()
    var platform = member.platform;
    member.dataClear();
    if (enableLocalLogin)
    {
        if (liff.isLoggedIn()) {
            liff.logout();
            loading.TaskSub()
            top.location.href = '/';
        }
    }
     else {
        if (platform == "fb") {
            FB.getLoginStatus(function (response) {//取得目前user是否登入FB網站
                console.log(response);
                if (response.status === 'connected') {

                    FB.api("/me/permissions", "DELETE", function (response) {
                        console.log("刪除結果");
                        console.log(response); //gives true on app delete success
                        //最後一個參數傳遞true避免cache
                        FB.getLoginStatus(function (res) {
                            loading.TaskSub()
                            top.location.href = '/';
                        }, true);//強制刷新cache避免login status下次誤判

                    });

                } else {
                    console.log("無法刪除FB App");
                }

            });
        }
        else if (platform == "line") {
            liff.init({
                liffId: '2002691476-KlYvJ5j8'

            }).then(function () {

                if (liff.isLoggedIn()) {
                    liff.logout();
                    loading.TaskSub()
                    top.location.href = '/';
                }

            }).catch(function (error) {
                console.log(error);
            });

        }
        else {
            loading.TaskSub()
            top.location.href = '/';
        }
    }
}

function getWindowSizeString() {
    const w = "600";
    const h = "678";
    // When the user clicks on a link that opens a new window using window.open. Make the window appear on the same monitor as its' parent.

    // window.screenX will give the position of the current monitor screen.
    // suppose monitor width is 1360
    // for monitor 1 window.screenX = 0;
    // for monitor 2 window.screenX = 1360;
    const dualScreenLeft =
        window.screenLeft != undefined ? window.screenLeft : window.screenX;
    const dualScreenTop =
        window.screenTop != undefined ? window.screenTop : window.screenY;

    const width = window.innerWidth
        ? window.innerWidth
        : document.documentElement.clientWidth
            ? document.documentElement.clientWidth
            : screen.width;
    const height = window.innerHeight
        ? window.innerHeight
        : document.documentElement.clientHeight
            ? document.documentElement.clientHeight
            : screen.height;

    // same monitor, the center of its parent.
    const left = width / 2 - w / 2 + dualScreenLeft;
    const top = height / 2 - h / 2 + dualScreenTop;
    return `width=${w}, height=${h}, top=${top}, left=${left}`;
}
