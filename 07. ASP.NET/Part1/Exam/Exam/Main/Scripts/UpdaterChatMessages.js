var timer = setInterval(() => {
  UpdateMessages();
}, 1000);

function ScrollDown() {
  $("html, body").animate({
    scrollTop: $(document).height()
  }, 1000);
}

function UpdateMessages() {
  $.ajax({
    url: '/Home/GetMessages',
    method: "POST",
    data: JSON.stringify({
      chatID: $('#currentChatID').val(),
      lastMsgID: $('#lastMsgID').val()
    }),
    dataType: 'JSON',
    contentType: 'application/json',
    cache: false,
    processData: false,
    success: function (data) {
      if (data != null && !isNaN(data.length) && data.length > 0) {
        var ScrollToBottom = ($(document).height() - $(window).height()) - $(window).scrollTop() <= 50;

        $('#lastMsgID').val(data[data.length - 1].ID);
        var container = $('.MessageContainer');
        /*data.forEach(x => container.append(`
<div class="MessageBox ${(x.IsMyMsg == true ? "MyMessage" : "FriendMessage")}">
	<p class="SenderLogin">${x.Sender}</p>
	<p class="ContentMsg">${x.Content}</p>
</div>`));*/

        

        data.forEach(x => container.append(`
<div class="MessageBox ${(x.IsMyMsg == true ? "MyMessage" : "FriendMessage")}">
	<div class="MessageBoxDiv1">
		<div>
			<img src="/${x.ImgSrc}" alt="photo"/>
		</div>
	</div>
	<div class="MessageBoxDiv2">
		<div>
			<p class="SenderLogin">${x.Sender}</p>
			<div style="width: 100%; min-width: 20px;"></div>
			<p class="SendTime">${x.Time}</p>
		</div>
		<p class="ContentMsg">${x.Content}</p>
	</div>
</div>
`));

        if (ScrollToBottom) ScrollDown();
      }
    }
  });
}





