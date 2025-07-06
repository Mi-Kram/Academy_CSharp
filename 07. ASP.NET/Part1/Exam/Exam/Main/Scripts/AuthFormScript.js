var colors = [
  '#EA0037',
  '#FF7400',
  '#9FEE00',
  '#00B25C',
  '#B70094',
  '#04819E'
];

class BubleDivInfo {
  constructor(_elem, _stepX, _stepY) {
    this.elem = _elem;
    this.stepX = _stepX;
    this.stepY = _stepY;
  }

  get Elem() {
    return this.elem;
  }

  get StepX() {
    return this.stepX;
  }

  set StepX(val) {
    this.stepX = val;
  }

  get StepY() {
    return this.stepY;
  }

  set StepY(val) {
    this.stepY = val;
  }
}
var BublesArr = [];
var MovedBuble = null;
var BubleOffset = { x: 0, y: 0 };
var timer = null;

function rand(min, max) {
  return (Math.random() * (max - min + 1)) + min;
}
function randSign(val) {
  return val * (Math.ceil(rand(0, 100)) % 2 == 0 ? 1 : -1);
}

window.onload = () => {
  document.body.onmousemove = OnMouseMove;

  for (var i = 0; i < colors.length; i++) {
    var elem = document.createElement('div');
    elem.classList.add('BubleDiv');

    var size = rand(150, 400);
    $(elem).css({
      background: colors[i],
      width: size + 'px',
      height: size + 'px',
      animationDelay: '-' + rand(0, 7) + 's',
      left: rand(15, document.documentElement.clientWidth - size - 15) + 'px',
      top: rand(15, document.documentElement.clientHeight - size - 15) + 'px'
    });
    elem.onmousedown = OnBubleMouseDown;
    elem.onmouseup = OnBubleMouseUp;

    BublesArr.push(new BubleDivInfo(elem, randSign(rand(0.1, 0.3)), randSign(rand(0.1, 0.3))));
    $('body').append(elem);
  }

  timer = setInterval(() => {
    for (var i = 0; i < BublesArr.length; i++) {
      var _b = BublesArr[i];
      if (_b.Elem == MovedBuble) continue;

      $(_b.Elem).css({
        left: (parseFloat($(_b.Elem).css('left')) + _b.StepX) + 'px',
        top: (parseFloat($(_b.Elem).css('top')) + _b.StepY) + 'px'
      });

      if (parseFloat($(_b.Elem).css('left')) < 5) _b.StepX = Math.abs(_b.StepX);
      if (document.documentElement.clientWidth - parseFloat($(_b.Elem).css('left')) - _b.Elem.offsetWidth < 5) _b.StepX = -Math.abs(_b.StepX);
      if (parseFloat($(_b.Elem).css('top')) < 5) _b.StepY = Math.abs(_b.StepY);
      if (document.documentElement.clientHeight - parseFloat($(_b.Elem).css('top')) - _b.Elem.clientHeight < 5) _b.StepY = -Math.abs(_b.StepY);
    }
  }, 10);
};

function OnBubleMouseDown() {
  MovedBuble = event.target;
  BubleOffset.x = event.clientX - event.target.offsetLeft;
  BubleOffset.y = event.clientY - event.target.offsetTop;
  $(MovedBuble).css('z-index', '1');
}
function OnBubleMouseUp() {
  $(MovedBuble).css('z-index', '0');
  MovedBuble = null;
}
function OnMouseMove() {
  if (MovedBuble != null) {
    $(MovedBuble).css({
      left: (event.clientX - BubleOffset.x) + 'px',
      top: (event.clientY - BubleOffset.y) + 'px'
    });
  }
}

