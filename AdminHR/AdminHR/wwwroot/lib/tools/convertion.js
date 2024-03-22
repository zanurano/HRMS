const UppercaseObject = obj =>
    Object.keys(obj).reduce((acc, k) => {
        acc[k.charAt(0).toUpperCase() + k.substring(1)] = obj[k];
        return acc;
    }, {});

function NumberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}