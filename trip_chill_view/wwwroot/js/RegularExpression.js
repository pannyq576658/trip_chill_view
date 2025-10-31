function validateEmail(email) {
    var re = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

    return re.test(email);
}

function validatePhone(phone) {
    var re = /^09\d{8}$/;

    return re.test(phone);
}