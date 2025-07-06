$(window).ready(() => {
	document.body.onresize = () => {
    for (var i = 0; i < BublesArr.length; i++) {
      var _b = BublesArr[i];
      if (_b.Elem == MovedBuble) MovedBuble = null;

      if (document.documentElement.clientWidth - 10 < parseFloat($(_b.Elem).css('width')) ||
        document.documentElement.clientHeight - 10 < parseFloat($(_b.Elem).css('height'))) {
        $(_b.Elem).css('display', 'none');
      }
      else {
        $(_b.Elem).css('display', 'block');
        if (parseFloat($(_b.Elem).css('left')) < 5) $(_b.Elem).css('left', '5px');
        if (document.documentElement.clientWidth - parseFloat($(_b.Elem).css('left')) - _b.Elem.offsetWidth < 5) $(_b.Elem).css('left', (document.documentElement.clientWidth - _b.Elem.offsetWidth - 5) + 'px');
        if (parseFloat($(_b.Elem).css('top')) < 5) $(_b.Elem).css('top', '5px')
        if (document.documentElement.clientHeight - parseFloat($(_b.Elem).css('top')) - _b.Elem.offsetHeight < 5) $(_b.Elem).css('top', (document.documentElement.clientHeight - _b.Elem.offsetHeight - 5) + 'px');
      }
    }
	};
});