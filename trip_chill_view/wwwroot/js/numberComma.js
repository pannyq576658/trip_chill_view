function AppendComma(n) {
    if (!/^((-*\d+)|(0))$/.test(n)) {
        var newValue = /^((-*\d+)|(0))$/.exec(n);
        if (newValue != null) {
            if (parseInt(newValue, 10)) {
                n = newValue;
            }
            else {
                n = '0';
            }
        }
        else {
            n = '0';
        }
    }
    if (parseInt(n, 10) == 0) {
        n = '0';
    }
    else {
        n = parseInt(n, 10).toString();
    }

    n += '';
    var arr = n.split('.');
    var re = /(\d{1,3})(?=(\d{3})+$)/g;
    return arr[0].replace(re, '$1,') + (arr.length == 2 ? '.' + arr[1] : '');
}
function RemoveComma(n) {
    return n.replace(/[,]+/g, '');
}