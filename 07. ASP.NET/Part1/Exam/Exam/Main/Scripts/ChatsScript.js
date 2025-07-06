function ShowLeftMenu() {
  $('.LeftMenu').css('display', 'block');
  $('.LeftMenu').animate({
    width: '250px'
  },
    {
      duration: 500
    });
}

function HideLeftMenu() {
  $('.LeftMenu').animate({
    width: '0px'
  },
    {
      duration: 500,
      complete: () => {
        $('.LeftMenu').css('display', 'none');
      }
    });
}

function ClearFocus() {
  event.target.blur();
}

function SendMessage() {
  var msg = $('.MessageSenderBox input').val().trim();
  if (msg.length > 0) {
    $.ajax({
      url: '/Home/SendMsg',
      method: "POST",
      data: JSON.stringify({
        chatID: $('#currentChatID').val(),
        msg: msg
      }),
      dataType: 'JSON',
      contentType: 'application/json',
      cache: false,
      processData: false
    });
  }
  $('.MessageSenderBox input').val('');
}

function OnMessageInputKeyDown() {
  if (event.keyCode == 13) {
    SendMessage();
    return false;
  }
}


