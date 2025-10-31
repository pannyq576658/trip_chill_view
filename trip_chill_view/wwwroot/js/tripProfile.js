var profileJson = { 'backgroundImg': member.picture, 'email': member.email, 'name': member.name, 'sex': member.gender, 'birth': { 'year': member.birthdayY, 'month': member.birthdayM, 'day': member.birthdayD } };
function mainProfileRun() {
    var less10 = '';
    for (var year = 1936; year <= 2020; year++)
        $('#birthSelect').append("<option value='" + year + "' class='border-b border-solid border-hi-input-gray pl-3 leading-10 hover:bg-hi-input-gray' >" + year + "</option>");
    for (var month = 1; month <= 12; month++) {
        less10 = (month < 10) ? '0' : '';
        $('#monthSelect').append("<option value='" + less10 + "" + month + "' class='border-b border-solid border-hi-input-gray pl-3 leading-10 hover:bg-hi-input-gray' >" + less10 + month + "</option>");
    }

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
    $('#saveBtn').click(function () {
        var gender = $('#sexSelect input[name="sex"]:checked').val();
        var memberJson = { 'id': member.id, 'name': $('#userName').val(), 'cartNum': parseInt(member.cartNum), 'platform': member.platform, 'gender': gender, 'email': member.email, 'birthday': $('#birthSelect :selected').text() + '/' + $('#monthSelect :selected').text() + '/' + $('#daySelect :selected').text(), 'pictureUrl': member.picture };
        console.log(memberJson);
        $.ajax({
            url: route+'/api/member/updateMember',
            data: JSON.stringify(memberJson),
            contentType: "application/json;charset=utf-8",
            type: "POST",
            success: function (response) {
                alert(response.msg);
                if (response.msg == "修改成功") {
                    member.setDataProperty('name', $('#userName').val())
                    $('.' + member.platform + '-bind-name').text($('#userName').val());
                    $('.navbar-cards .username').text($('#userName').val());
                    member.setDataProperty('birthdayY', $('#birthSelect :selected').text())
                    member.setDataProperty('birthdayM', $('#monthSelect :selected').text())
                    member.setDataProperty('birthdayD', $('#daySelect :selected').text())
                    member.setDataProperty('gender', $('#sexSelect input[name="sex"]:checked').val())
                }
            }
        });

       /* const vm = Vue.createApp(
            data(){
                return {

                }
            }
        );*/

    });
}