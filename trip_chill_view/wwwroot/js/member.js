class member {
    constructor() {
        this.id = $.cookie("id");
        this.cartNum = $.cookie("cartNum");
        this.name = $.cookie("name");
        this.email = $.cookie("email");
        this.picture = $.cookie("picture");
        this.platform = $.cookie("platform");
        this.birthdayY = $.cookie("birthdayY");
        this.birthdayM = $.cookie("birthdayM");
        this.birthdayD = $.cookie("birthdayD");
        this.gender = $.cookie("gender");
        this.verifyApproved = $.cookie("verifyApproved");
    }
    setData(memberJson) {
        for (var k in memberJson) {
            if (k == 'birthday') {
                var birthdayArray = memberJson[k].split("/");
                this.birthdayY = birthdayArray[0];
                this.birthdayM = birthdayArray[1];
                this.birthdayD = birthdayArray[2];
            }
            else if (k == 'pictureUrl')
            {
                this.picture = memberJson[k];
            }            
            else {
                this[k] = memberJson[k];
            }
        }
        this.setCookie(memberJson);
    }
    setCookie(memberJson) {
        for (var k in memberJson) {
            if (k == 'birthday') {
                var birthdayArray = memberJson[k].split("/");
                $.cookie("birthdayY", birthdayArray[0], { path: '/' });
                $.cookie("birthdayM", birthdayArray[1], { path: '/' });
                $.cookie("birthdayD", birthdayArray[2], { path: '/' });
            }
            else if (k == 'pictureUrl') {
                $.cookie("picture", memberJson[k], { path: '/' });
            }
            else {
                $.cookie(k, memberJson[k], { path: '/' });
            }
        }
    }
    dataClear() {
        $.cookie("id", "", { path: '/' });
        $.cookie("cartNum", "0", { path: '/' });
        $.cookie("name", "", { path: '/' });
        $.cookie("email", "", { path: '/' });
        $.cookie("picture", "", { path: '/' });
        $.cookie("birthdayY", "", { path: '/' });
        $.cookie("birthdayM", "", { path: '/' });
        $.cookie("birthdayD", "", { path: '/' });
        $.cookie("gender", "", { path: '/' });
        $.cookie("platform", "", { path: '/' });
        $.cookie("verifyApproved", "", { path: '/' });
        this.id = '';
        this.cartNum = '0';
        this.name = '';
        this.email = '';
        this.picture = '';
        this.platform = '';
        this.birthdayY = '';
        this.birthdayM = '';
        this.birthdayD = '';
        this.gender = '';
        this.verifyApproved = '';
    }
    setDataProperty(key,value) {
        this[key] = value;
        $.cookie(key, value, { path: '/' });
   }
    print() {
        console.log(this.id + '\n' + this.cartNum + '\n' + this.name + '\n' + this.email + '\n' + this.picture + '\n' + this.platform + '\n' + this.birthdayY + '\n' + this.birthdayM + '\n' + this.birthdayD + '\n' + this.gender + '\n' + this.verifyApproved + '\n')

    }
        
}

