// using jQuery Mask Plugin v1.7.5
// http://jsfiddle.net/d29m6enx/2/

var maskBehavior = (val) => {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
};

var maskBehaviorOptions = {
    onKeyPress: function (val, e, field, options) {
        field.mask(maskBehavior.apply({}, arguments), options);
    }
};

var setMaskInput = () => {
    $('[mask=date]').mask('11/11/1111');
    $('[mask=time]').mask('00:00:00');
    $('[mask=date_time]').mask('00/00/0000 00:00:00');
    $('[mask=cep]').mask('00000-000');
    $('[mask=phone]').mask(maskBehavior, maskBehaviorOptions);
    $('[mask=cpf]').mask('000.000.000-00', { reverse: true });
    $('[mask=money]').mask('000.000.000.000.000,00', { reverse: true });
};